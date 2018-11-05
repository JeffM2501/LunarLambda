using System;
using Lidgren.Network;

namespace LunarLambda.Messges.Ship.Connect
{
    public class JoinShipResponce : ShipMessage
    {
        public bool Error = false;
        public string Message = string.Empty;

        public int ShipID = -1;

        public override bool Pack(NetOutgoingMessage buffer)
        {
            buffer.Write(Error);
            buffer.Write(Message);
            buffer.Write(ShipID);
            return true;
        }

        public override bool Unpack(NetIncomingMessage message)
        {
            try
            {
                Error = message.ReadBoolean();
                Message = message.ReadString();
                ShipID = message.ReadInt32();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
