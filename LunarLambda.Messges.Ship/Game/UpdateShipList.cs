using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.Messges.Ship.Game
{
    public class UpdateShipList : ShipMessage
    {
        public class ShipInfo
        {
            public int ID = -1;
            public string Name = string.Empty;
            public string TypeName = string.Empty;
            public string ModelName = string.Empty;
            public string IconImage = string.Empty;
            public List<Tuple<string,string>> Stats = new List<Tuple<string, string>>();

            public bool Spawned = false;

            public string ServerAddress = string.Empty;
            public string ServerLANAddress = string.Empty;

            public bool Protected = false;
            public int CrewCount = 0;
            public List<string> CrewedConsoles = new List<string>();
            public List<string> AvailableConsoles = new List<string>();
        }

        public List<ShipInfo> Ships = new List<ShipInfo>();
    }
}
