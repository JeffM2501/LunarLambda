using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.Host.Game
{
    internal class ShipServer
    {

        public ShipServer(int port)
        {
            if (port < 1024)
                throw new Exception("invalid port range");
        }
    }
}
