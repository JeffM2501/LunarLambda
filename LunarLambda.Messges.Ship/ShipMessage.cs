using Lidgren.Network;
using System;

namespace LunarLambda.Messges.Ship
{
    public class ShipMessage : EventArgs
    {
        public static readonly int ProtocolVersion = 1;

		public virtual bool Unpack(NetIncomingMessage message)
		{
            // by default we serialize
            message.ReadAllFields(this, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return true;
		}

        public void WriteMessageHeader (NetOutgoingMessage buffer)
        {
            buffer.Write(GetType().Name);
        }

		public virtual bool Pack(NetOutgoingMessage buffer)
		{
            // by default we serialize
            buffer.WriteAllFields(this, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            return true;
		}
	}
}
