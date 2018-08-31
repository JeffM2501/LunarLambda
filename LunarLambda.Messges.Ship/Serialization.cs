using System;
using System.Collections.Generic;

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

			message.ReadAllFields(msg);
			
			return msg;
		}

		public static bool Pack(ShipMessage msg, NetOutgoingMessage buffer)
		{
			if (msg == null)
				return false;

			buffer.Write(msg.GetType().Name);
			buffer.WriteAllFields(msg);
			return true;
		}
	}
}
