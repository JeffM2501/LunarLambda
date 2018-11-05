using System;
using System.Collections.Generic;
using Lidgren.Network;

namespace LunarLambda.Messges.Ship.Connect
{
    public class ShipRequest : ShipMessage
    {
        public bool Join = false;
        public int RequestedShipID = 0;
        public string Name = string.Empty;
        public string Password = string.Empty;

        public override bool Pack(NetOutgoingMessage buffer)
        {
            buffer.Write(Join);
            buffer.Write(RequestedShipID);
            buffer.Write(Name);
            buffer.Write(Password);
            return true;
        }

        public override bool Unpack(NetIncomingMessage message)
        {
            try
            {
                Join = message.ReadBoolean();
                RequestedShipID = message.ReadInt32();
                Name = message.ReadString();
                Password = message.ReadString();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
