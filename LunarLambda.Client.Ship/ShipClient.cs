using System;

using Lidgren.Network;
using LunarLambda.Messges.Ship;
using LunarLambda.Messges.Ship.Connect;
using LunarLambda.Messges.Ship.Game;
using LudicrousElectron.Engine;

namespace LunarLambda.Client.Ship
{
    public partial class ShipClient
    {
        public static ShipClient ActiveShipClient = null;

        protected NetClient Client = null;

        public event EventHandler Connected = null;
        public event EventHandler Disconnected = null;

        protected MessageDispatcher Dispatcher = new MessageDispatcher();

        public ShipClient(string host, int port)
        {
            SetupMessages();

            NetPeerConfiguration config = new NetPeerConfiguration("LL_ShipHost_" + ShipMessage.ProtocolVersion.ToString());
   
            config.AutoFlushSendQueue = true;
            config.AcceptIncomingConnections = false;
            Client = new NetClient(config);

            Client.Start();
            Client.Connect(host, port);
            Core.Update += Core_Update;
        }

        private void Core_Update(object sender, Core.EngineState e)
        {
            PocesssMessages();
        }

        public void SetupMessages()
        {
            Dispatcher.RegisterHandler(typeof(ConnectResponce), HandleConnectResponce);
            Dispatcher.RegisterHandler(typeof(UpdateShipList), HandleUpdateShipList);
        }

        public void Shutdown()
        {
            Core.Update -= Core_Update;
            Client.Disconnect("shutdown");
        }

        public void Send(ShipMessage message)
        {
            var outbound = Client.CreateMessage();

            if (Serialization.Pack(message, outbound))
                Client.SendMessage(outbound, NetDeliveryMethod.ReliableOrdered);
        }

        protected NetIncomingMessage peerStateMsg = null;
        protected void PocesssMessages()
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
        }
    }
}
