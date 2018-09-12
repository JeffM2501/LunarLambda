using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

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

        public override bool Pack(NetOutgoingMessage buffer)
        {
            buffer.Write(Ships.Count);
            foreach(var ship in Ships)
            {
                buffer.Write(ship.ID);
                buffer.Write(ship.Name);
                buffer.Write(ship.TypeName);
                buffer.Write(ship.ModelName);
                buffer.Write(ship.IconImage);

                buffer.Write(ship.Stats.Count);
                foreach(var stat in ship.Stats)
                {
                    buffer.Write(stat.Item1);
                    buffer.Write(stat.Item2);
                }
                buffer.Write(ship.Spawned);
                buffer.Write(ship.ServerAddress);
                buffer.Write(ship.ServerLANAddress);

                buffer.Write(ship.Protected);
                buffer.Write(ship.CrewCount);

                buffer.Write(ship.CrewedConsoles.Count);
                foreach (var crew in ship.CrewedConsoles)
                    buffer.Write(crew);

                buffer.Write(ship.AvailableConsoles.Count);
                foreach (var console in ship.AvailableConsoles)
                    buffer.Write(console);
            }
            return true;
        }

        public override bool Unpack(NetIncomingMessage message)
        {
            Ships.Clear();
            try
            {
                int count = message.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    ShipInfo info = new ShipInfo();
                    info.ID = message.ReadInt32();
                    info.Name = message.ReadString();
                    info.TypeName = message.ReadString();
                    info.ModelName = message.ReadString();
                    info.IconImage = message.ReadString();

                    int s = message.ReadInt32();
                    for (int j = 0; j < s; j++)
                        info.Stats.Add(new Tuple<string, string>(message.ReadString(), message.ReadString()));

                    info.Spawned = message.ReadBoolean();

                    info.ServerAddress = message.ReadString();
                    info.ServerLANAddress = message.ReadString();

                    info.Protected = message.ReadBoolean();
                    info.CrewCount = message.ReadInt32();

                    int p = message.ReadInt32();
                    for (int j = 0; j < p; j++)
                        info.CrewedConsoles.Add(message.ReadString());

                    int c = message.ReadInt32();
                    for (int j = 0; j < c; j++)
                        info.AvailableConsoles.Add(message.ReadString());

                    Ships.Add(info);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
