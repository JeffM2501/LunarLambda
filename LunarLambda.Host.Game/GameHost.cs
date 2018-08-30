using System;
using System.Collections.Generic;

using LunarLambda.API;
using LunarLambda.Data;

namespace LunarLambda.Host.Game
{
    public class GameHost
    {
        private LLScenario ActiveScenario = null;

        private ShipServer ShipHost = null;

        public void Startup(ServerStartupInfo info)
        {
            ActiveScenario = info.SelectedScenario.Scenario as LLScenario;
            if (ActiveScenario == null)
                return;

            ActiveScenario.Init(info.SelectedVariation.Name);

            ShipHost = new ShipServer(info.Port);
        }

        public void Shutdown()
        {
            ActiveScenario.Shutdown();
            ActiveScenario = null;

            ShipHost.Shutdown();
            ShipHost = null;
        }
    }
}
