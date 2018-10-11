using System;

using Lidgren.Network;

namespace LunarLambda.Messges.Ship
{
    public static class Serialization
	{
		public static ShipMessage Unpack(NetIncomingMessage message)
		{
			Type t = MessageManager.GetMessageType(message.ReadString());
			if (t == null)
				return null;

			ShipMessage msg = Activator.CreateInstance(t) as ShipMessage;
			if (!msg.Unpack(message))
				return null;
			
			return msg;
		}

        public static bool Pack(ShipMessage msg, NetOutgoingMessage buffer)
        {
            if (msg == null)
                return false;

            msg.WriteMessageHeader(buffer);
            return msg.Pack(buffer);
        }
	}
}
