using System;
using Lidgren.Network;

namespace LunarLambda.Messges.Ship.Connect
{
    public class ConnectResponce : ShipMessage
	{
		public enum ResponceTypes
		{
			Accepted,
			Rejected,
		}
		public ResponceTypes Responce = ResponceTypes.Rejected;
		public string Reason = string.Empty;

        public string ServerKey = string.Empty;

        public string SerververWANAddress = string.Empty;
        public string ServerLANAddress = string.Empty;

		public string ScenarioName = string.Empty;
		public string ScenarioDescription = string.Empty;
		public string ScenarioType = string.Empty;
		public string ScenarioAuthor = string.Empty;
		public string ScenarioVariation = string.Empty;
		public string ScenarioIconImage = string.Empty;

        public override bool Pack(NetOutgoingMessage buffer)
        {
            buffer.Write((byte)Responce);
            buffer.Write(Reason);

            buffer.Write(ServerKey);
            buffer.Write(SerververWANAddress);
            buffer.Write(ServerLANAddress);
            buffer.Write(ScenarioName);
            buffer.Write(ScenarioDescription);
            buffer.Write(ScenarioType);
            buffer.Write(ScenarioAuthor);
            buffer.Write(ScenarioIconImage);

            return true;
        }

        public override bool Unpack(NetIncomingMessage message)
        {
            try
            {
                Responce = (ResponceTypes)message.ReadByte();
                Reason = message.ReadString();

                ServerKey = message.ReadString();

                SerververWANAddress = message.ReadString();
                ServerLANAddress = message.ReadString();

                ScenarioName = message.ReadString();
                ScenarioDescription = message.ReadString();
                ScenarioType = message.ReadString();
                ScenarioAuthor = message.ReadString();
                ScenarioVariation = message.ReadString();
                ScenarioIconImage = message.ReadString();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
