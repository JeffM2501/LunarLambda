using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.Messges.Ship.Connect
{
	public class ConnectRequest : ShipMessage
	{
		public enum ClientTypes
		{
			Ship = 1,
			GameManager = 2,
			API = 3,
		}
		public ClientTypes ClientType = ClientTypes.Ship;
		public string Name = string.Empty;
		public string Credentials = string.Empty;
	}
}
