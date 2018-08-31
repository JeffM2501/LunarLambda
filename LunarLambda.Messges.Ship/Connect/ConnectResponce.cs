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

		public string ScenarioName = string.Empty;
		public string ScenarioDescription = string.Empty;
		public string ScenarioType = string.Empty;
		public string ScenarioAuthor = string.Empty;
		public string ScenarioVariation = string.Empty;
		public string ScenarioIconImage = string.Empty;

		public class ShipInfo
		{
			public int ID = -1;
			public string Name = string.Empty;
			public string Type = string.Empty;
			public string ModelName = string.Empty;
			public string IconImage = string.Empty;
			public List<string> Stats = new List<string>();

			public bool Spawned = false;
			public int CrewCount = 0;
			public List<string> CrewedConsoles = new List<string>();
			public List<string> AvailableConsoles = new List<string>();
		}

		public List<ShipInfo> Ships = new List<ShipInfo>();
	}
}
