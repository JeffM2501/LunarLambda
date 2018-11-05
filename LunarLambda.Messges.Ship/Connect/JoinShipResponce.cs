using System;
using Lidgren.Network;

namespace LunarLambda.Messges.Ship.Connect
{
    public class JoinShipResponce : ShipMessage
    {
        public bool Error = false;
        public string Message = string.Empty;

        public int ShipID = -1;

        public string ShipName = string.Empty;
        public string ShipClass = string.Empty;

        public override bool Pack(NetOutgoingMessage buffer)
        {
            buffer.Write(Error);
            buffer.Write(Message);
            buffer.Write(ShipID);
            buffer.Write(ShipName);
            buffer.Write(ShipClass);
            return true;
        }

        public override bool Unpack(NetIncomingMessage message)
        {
            try
            {
                Error = message.ReadBoolean();
                Message = message.ReadString();
                ShipID = message.ReadInt32();
                ShipName = message.ReadString();
                ShipClass = message.ReadString();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
