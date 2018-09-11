using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace LunarLambda.Messges.Ship.Connect
{
	public class ConnectRequest : ShipMessage
	{
		public enum ClientTypes
		{
			Ship = 1,
			GameManager = 2,
			API = 3,
		}
		public ClientTypes ClientType = ClientTypes.Ship;
		public string Name = string.Empty;
		public string Credentials = string.Empty;

        public override bool Pack(NetOutgoingMessage buffer)
        {
            buffer.Write((byte)ClientType);
            buffer.Write(Name);
            buffer.Write(Credentials);
            return true;
        }

        public override bool Unpack(NetIncomingMessage message)
        {
            try
            {
                ClientType = (ClientTypes)message.ReadByte();
                Name = message.ReadString();
                Credentials = message.ReadString();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
