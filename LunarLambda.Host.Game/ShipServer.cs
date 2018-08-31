using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Lidgren.Network;
using LunarLambda.Messges.Ship;

namespace LunarLambda.Host.Game
{
    internal class ShipServer
    {
        protected NetServer Server = null;
        protected Thread WorkerThread = null;

        protected bool Running = false;

		public class ShipPeer : EventArgs
		{
			public NetPeer Connection = null;
			public List<ShipMessage> PendingInbound = new List<ShipMessage>();

			public object Tag = null;

			internal ShipPeer(NetPeer peer)
			{
				Connection = peer;
			}
		}
		protected Dictionary<long,ShipPeer> ConnectedClients = new Dictionary<long, ShipPeer>();

		public event EventHandler<ShipPeer> PeerConnected = null;
		public event EventHandler<ShipPeer> PeerDisconnected = null;

		public ShipServer(int port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("LL_ShipHost_" + ShipMessage.ProtocolVersion.ToString());
            config.Port = port;
            config.AcceptIncomingConnections = true;
            config.AutoFlushSendQueue = true;
            Server = new NetServer(config);

            Running = true;
            Server.Start();

            WorkerThread = new Thread(new ThreadStart(ProcessNetwork));
        }

        public void Shutdown()
        {
            Running = false;

            lock(this)
            {
                WorkerThread?.Abort();
                WorkerThread = null;
            }

            Server.Shutdown("BYE!");
        }

        private bool IsRunning()
        {
            lock (this)
                return Running;
        }

        protected virtual void ProcessNewPeer(NetPeer peer)
        {
			ShipPeer newPeer = new ShipPeer(peer);

			lock (this)
				ConnectedClients.Add(peer.UniqueIdentifier,newPeer);

			PeerConnected?.Invoke(this, newPeer);
		}

        protected virtual void RemovePeer(NetPeer peer)
        {
			if (!ConnectedClients.ContainsKey(peer.UniqueIdentifier))
				return;

			PeerConnected?.Invoke(this, ConnectedClients[peer.UniqueIdentifier]);
			lock(this)
				ConnectedClients.Remove(peer.UniqueIdentifier);
        }

		protected virtual void ProcessMessage(NetIncomingMessage message)
		{
			if (!ConnectedClients.ContainsKey(message.SenderConnection.Peer.UniqueIdentifier))
				return;

			var peer = ConnectedClients[message.SenderConnection.Peer.UniqueIdentifier];

			ShipMessage msg = Serialization.Unpack(message);
			if (msg != null)
			{
				lock (peer)
					peer.PendingInbound.Add(msg);
			}
		}

        private void ProcessNetwork()
        {
            NetIncomingMessage peerStateMsg = null;

            while (IsRunning())
            {
                peerStateMsg = null;
                while ((peerStateMsg = Server.ReadMessage()) != null)
                {
                    switch (peerStateMsg.MessageType)
                    {
                        case NetIncomingMessageType.StatusChanged:
                            switch (peerStateMsg.SenderConnection.Status)
                            {
                                case NetConnectionStatus.Connected:
                                    ProcessNewPeer(peerStateMsg.SenderConnection.Peer);
                                    break;

                                case NetConnectionStatus.Disconnected:
                                    RemovePeer(peerStateMsg.SenderConnection.Peer);
                                    break;
                            }

                            break;

                        case NetIncomingMessageType.Data:
							ProcessMessage(peerStateMsg);
							break;
                    }
                    Server.Recycle(peerStateMsg);
                }
            }

            lock (this)
                WorkerThread = null;
        }
    }
}
