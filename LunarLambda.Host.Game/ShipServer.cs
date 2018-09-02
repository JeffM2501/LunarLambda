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

        private readonly object Locker = new object();

		public class ShipPeer : EventArgs
		{
			protected NetServer Server = null;

			public NetConnection Connection = null;
			public List<ShipMessage> PendingInbound = new List<ShipMessage>();

			public object Tag = null;

			internal ShipPeer(NetConnection connection, NetServer server)
			{
				Server = server;
				Connection = connection;
			}

			public void Send(ShipMessage message)
			{
				var outbound = Server.CreateMessage();
				if (Serialization.Pack(message, outbound))
					Server.SendMessage(outbound, Connection, NetDeliveryMethod.ReliableOrdered);
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
			WorkerThread.Start();

		}

        public void Shutdown()
        {
            Running = false;

            lock(Locker)
            {
                WorkerThread?.Abort();
                WorkerThread = null;
            }

            Server.Shutdown("BYE!");
        }

        private bool IsRunning()
        {
            lock (Locker)
                return Running;
        }

        protected virtual void ProcessNewPeer(NetConnection connection)
        {
			ShipPeer newPeer = new ShipPeer(connection, Server);

			lock (Locker)
				ConnectedClients.Add(connection.Peer.UniqueIdentifier,newPeer);

			PeerConnected?.Invoke(this, newPeer);
		}

        protected virtual void RemovePeer(NetPeer peer)
        {
			if (!ConnectedClients.ContainsKey(peer.UniqueIdentifier))
				return;

			PeerDisconnected?.Invoke(this, ConnectedClients[peer.UniqueIdentifier]);
			lock(Locker)
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
                                    ProcessNewPeer(peerStateMsg.SenderConnection);
                                    break;

                                case NetConnectionStatus.Disconnected:
                                    RemovePeer(peerStateMsg.SenderConnection.Peer);
                                    break;
                            }
                            break;

                        case NetIncomingMessageType.Data:
							ProcessMessage(peerStateMsg);
							break;

                        case NetIncomingMessageType.Error:
                            // log error
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
