using System;
using System.Collections.Generic;

using LunarLambda.Messges.Ship;

namespace LunarLambda.Host.Game.Processors
{
	internal class ConnectionProcessor
	{
		private ShipServer Server = null;

		public ConnectionProcessor(ShipServer server)
		{
			Server = server;
		}
	}
}
