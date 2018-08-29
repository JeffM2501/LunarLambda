using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API
{
    public static partial class Events
    {
        public static void CallSetupModelData(object sender = null) { SetupModelData?.Invoke(sender, EventArgs.Empty); }
        public static void CallSetupShipTemplates(object sender = null) { SetupShipTemplates?.Invoke(sender, EventArgs.Empty); }
        public static void CallSetupFactions(object sender = null) { SetupFactions?.Invoke(sender, EventArgs.Empty); }
        public static void CallSetupScienceDB(object sender = null) { SetupScienceDB?.Invoke(sender, EventArgs.Empty); }

        public static void CallFinalizeDatabases(object sender = null) { FinalizeDatabases?.Invoke(sender, EventArgs.Empty); }
        public static void CallMissionStart(object sender = null) { MissionStart?.Invoke(sender, EventArgs.Empty); }

		public static void CallGetDefaultServerInfo(ServerStartupInfo info = null) { GetDefaultServerInfo?.Invoke(null, info); }
		public static void CallGetServerInfoForScenario(ServerStartupInfo info = null) { GetServerInfoForScenario?.Invoke(null, info); }
	}
}
