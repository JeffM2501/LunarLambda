using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lidgren.Network;
using LunarLambda.Messges.Ship;

namespace LunarLambda.Host.Game
{
    internal class ShipServer
    {
        protected NetServer Server = null;

        public ShipServer(int port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("LL_ShipHost_" + ShipMessage.ProtocolVersion.ToString());
            config.Port = port;
            Server = new NetServer(config);

            Server.Start();
        }

        public void Shutdown()
        {
            Server.Shutdown("BYE!");
        }
    }
}
