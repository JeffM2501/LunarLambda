using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Lidgren.Network;
using LunarLambda.Messges.Ship;
using LunarLambda.Messges.Ship.Connect;
using LunarLambda.Messges.Ship.Game;

namespace LunarLambda.Client.Ship
{
    public partial class ShipClient
    {
        public static ShipClient ActiveShipClient = null;

        protected NetClient Client = null;
        protected Thread WorkerThread = null;
        private object Locker = new object();

        protected bool Running = false;

        public event EventHandler Connected = null;
        public event EventHandler Disconnected = null;

        protected MessageDispatcher Dispatcher = new MessageDispatcher();

        public ShipClient(string host, int port)
        {
            SetupMessages();

            NetPeerConfiguration config = new NetPeerConfiguration("LL_ShipHost_" + ShipMessage.ProtocolVersion.ToString());
            config.Port = port;
            config.AutoFlushSendQueue = true;
            Client = new NetClient(config);

            Client.Connect(host, port);

            WorkerThread = new Thread(new ThreadStart(ProcessNetwork));
            WorkerThread.Start();
        }

        public void SetupMessages()
        {
            Dispatcher.RegisterHandler(typeof(ConnectResponce), HandleConnectResponce);
            Dispatcher.RegisterHandler(typeof(UpdateShipList), HandleUpdateShipList);
        }

        public void Shutdown()
        {
            Client.Disconnect("shutdown");

            if (WorkerThread != null)
                WorkerThread.Abort();
            WorkerThread = null;
        }

        private bool IsRunning()
        {
            lock (Locker)
                return Running;
        }

        public void Send(ShipMessage message)
        {
            var outbound = Client.CreateMessage();

            if (Serialization.Pack(message, outbound))
                Client.SendMessage(outbound, NetDeliveryMethod.ReliableOrdered);
        }

        public virtual void ProcessNetwork()
        {
            NetIncomingMessage peerStateMsg = null;

            while (IsRunning())
            {
                peerStateMsg = null;
                while ((peerStateMsg = Client.ReadMessage()) != null)
                {
                    switch (peerStateMsg.MessageType)
                    {
                        case NetIncomingMessageType.StatusChanged:
                            switch (peerStateMsg.SenderConnection.Status)
                            {
                                case NetConnectionStatus.Connected:
                                    Send(new ConnectRequest());
                                    Connected?.Invoke(this, EventArgs.Empty);
                                    break;

                                case NetConnectionStatus.Disconnected:
                                    Disconnected?.Invoke(this, EventArgs.Empty);
                                    break;

                                default:
                                    //log other statuses
                                    break;
                            }
                            break;

                        case NetIncomingMessageType.Data:
                            Dispatcher.Dispatch(this, Serialization.Unpack(peerStateMsg));
                            break;

                        default:
                            // log error
                            break;
                    }
                    Client.Recycle(peerStateMsg);
                }
                Client.WaitMessage(100);
            }

            lock (Locker)
                WorkerThread = null;
        }
    }
}
