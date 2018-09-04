using System;
using System.Collections.Generic;

using LunarLambda.API;
using LunarLambda.Data;

using GameDiscoveryServices;

namespace LunarLambda.Host.Game
{
    public class GameHost
    {
        public static GameHost ActiveGameHost = null;
		protected HostedService ServiceInfo = null;
		protected ServerStartupInfo StartupInfo = null;

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
			StartupInfo = info;

			ActiveScenario = info.SelectedScenario.Scenario as LLScenario;
            if (ActiveScenario == null)
                return;

            string variationName = string.Empty;
            if (info.SelectedVariation != null)
                variationName = info.SelectedVariation.Name;

            ActiveScenario.Init(variationName);

			Register();

			ShipHost = new ShipServer(info.Port);
        }

        public void Shutdown()
        {
            ActiveScenario.Shutdown();
            ActiveScenario = null;

            ShipHost.Shutdown();
            ShipHost = null;

			if (ServiceInfo != null)
				ServiceList.RemoveLocalService(ServiceInfo);

			ServiceInfo = null;

		}

		protected void Register()
		{
			ServiceInfo = new HostedService();
			ServiceInfo.Name = StartupInfo.Name;
			ServiceInfo.Port = StartupInfo.Port;
			ServiceInfo.WANAddress = StartupInfo.ServerWANHost;
			ServiceInfo.LANAddress = StartupInfo.ServerLANAddress;
			ServiceInfo.Secured = StartupInfo.Password != string.Empty;

			ServiceInfo.Properties.Add(new Tuple<string, string>("FTL", StartupInfo.FTL.ToString()));
			ServiceInfo.Properties.Add(new Tuple<string, string>("SensorRange", StartupInfo.SensorRange.ToString()));
			ServiceInfo.Properties.Add(new Tuple<string, string>("TacticalRadarMSD", StartupInfo.TacticalRadarMSD.ToString()));
			ServiceInfo.Properties.Add(new Tuple<string, string>("LongRangeRadarMSD", StartupInfo.LongRangeRadarMSD.ToString()));
			ServiceInfo.Properties.Add(new Tuple<string, string>("Scans", StartupInfo.Scans.ToString()));
			ServiceInfo.Properties.Add(new Tuple<string, string>("UseWeaponFrequencies", StartupInfo.UseWeaponFrequencies.ToString()));
			ServiceInfo.Properties.Add(new Tuple<string, string>("UseSystemDamage", StartupInfo.UseSystemDamage.ToString()));

			if (StartupInfo.SelectedScenario != null)
			{
				ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioName", StartupInfo.SelectedScenario.Name));
				ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioDescription", StartupInfo.SelectedScenario.Description));
				ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioAuthor", StartupInfo.SelectedScenario.Author));
				ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioIconImage", StartupInfo.SelectedScenario.IconImage));
				ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioType", StartupInfo.SelectedScenario.Type));
				if (StartupInfo.SelectedVariation != null)
				{
					ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioVariationName", StartupInfo.SelectedVariation.DisplayName));
					ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioVariationDescription", StartupInfo.SelectedVariation.Description));
				}
			}
				
			else
				ServiceInfo.Properties.Add(new Tuple<string, string>("ScenarioName", "NONE"));

			ServiceList.RegisterLocalService(ServiceInfo, StartupInfo.Public);
		}
    }
}
