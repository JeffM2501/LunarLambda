using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.Messges.Ship
{
    public class ShipMessage
    {
        public static readonly int ProtocolVersion = 1;

		public virtual bool UseSerialization { get; } = true;

		public virtual bool Unpack(NetIncomingMessage message)
		{
			return false;
		}

		public virtual bool Pack(NetOutgoingMessage buffer)
		{
			return false;
		}
	}
}
