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

        protected List<NetPeer> ConnectedClients = new List<NetPeer>();

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
            ConnectedClients.Add(peer);
        }

        protected virtual void RemovePeer(NetPeer peer)
        {
            ConnectedClients.Remove(peer);
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
                            // process the data here
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
