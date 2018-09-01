using System;
using System.Collections.Generic;

using LunarLambda.API;
using LunarLambda.Data;

namespace LunarLambda.Host.Game
{
    public class GameHost
    {
        public static GameHost ActiveGameHost = null;

        public static void StartGame(ServerStartupInfo info)
        {
            if (ActiveGameHost != null)
                ActiveGameHost.Shutdown();

            ActiveGameHost = new GameHost();
            ActiveGameHost.Startup(info);
        }

        public static void StopGame()
        {
            if (ActiveGameHost != null)
                ActiveGameHost.Shutdown();

            ActiveGameHost = null;
        }

        private LLScenario ActiveScenario = null;

        private ShipServer ShipHost = null;

        public void Startup(ServerStartupInfo info)
        {
            ActiveScenario = info.SelectedScenario.Scenario as LLScenario;
            if (ActiveScenario == null)
                return;

            string variationName = string.Empty;
            if (info.SelectedVariation != null)
                variationName = info.SelectedVariation.Name;

            ActiveScenario.Init(variationName);

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
