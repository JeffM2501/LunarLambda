using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
