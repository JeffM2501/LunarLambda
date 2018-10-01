using System;
using System.Collections.Generic;

using LunarLambda.API;
using LunarLambda.Data;

using GameDiscoveryServices;
using LunarLambda.Messges.Ship.Game;
using LunarLambda.Data.Databases;
using LunarLambda.Data.Zone;

namespace LunarLambda.Host.Game
{
    public partial class GameHost
    {
        public static GameHost ActiveGameHost = null;
		protected HostedService ServiceInfo = null;
		public  ServerStartupInfo StartupInfo { get; protected set; } = null;

        public List<Map> Zones = new List<Map>();

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

			Listen(info.Port);
        }

        public void Shutdown()
        {
            ActiveScenario.Shutdown();
            ActiveScenario = null;

            ShutdownNetwork();

			if (ServiceInfo != null)
				ServiceList.RemoveLocalService();

            LANDiscoveryHost.Shutdown();

			ServiceInfo = null;
        }

		protected void Register()
		{
			ServiceInfo = new HostedService();

            ServiceInfo.IDKey = HostedService.GenerateKey();

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

            LANDiscoveryHost.Startup();
		}


        private void ShipServerAdded(ShipPeer e)
        {
            var existing = ServiceInfo.SubServices.Find((x) => e.ShipHostInformation.IDKey == x.Item2.IDKey);
            if (existing != null)
                ServiceInfo.SubServices.Remove(existing);

            ServiceInfo.SubServices.Add(existing);
        }

        private void GetShipList(UpdateShipList list)
        {
            foreach (var avalabileShips in ActiveScenario.GetPlayableShips())
                list.Ships.Add(InfoFromTempalte(avalabileShips));
        }

        protected virtual UpdateShipList.ShipInfo InfoFromTempalte(ShipTemplate template)
        {
            UpdateShipList.ShipInfo info = new UpdateShipList.ShipInfo();
            info.Spawned = false;
            info.CrewCount = 0;
            info.ID = template.ID;
            info.Name = template.DisplayName;
            info.TypeName = template.ClassName + " " + template.SubClassName;
            info.ModelName = template.ModelName;
            info.IconImage = template.IconImage;
            info.Protected = false;

            info.Stats.Add(new Tuple<string,string>(ShipInfoStrings.ImpulseSpeed, template.ImpulseSpeed.ToString()));
            info.Stats.Add(new Tuple<string, string>(ShipInfoStrings.ImpulseAcceleration, template.ImpulseAcceleration.ToString()));
            info.Stats.Add(new Tuple<string, string>(ShipInfoStrings.ManuverSpeed, template.TurnSpeed.ToString()));
            info.Stats.Add(new Tuple<string, string>(ShipInfoStrings.CombatBoost, template.CombatManuverBoostSpeed.ToString() +"/" + template.CombatManuverStrafeSpeed.ToString()));


            info.Stats.Add(new Tuple<string, string>(ShipInfoStrings.FTLType, ShipInfoStrings.FTLTypes.Split(",".ToCharArray())[(int)template.DriveType]));


            info.Stats.Add(new Tuple<string, string>(ShipInfoStrings.FuelCapcity, template.FuelCapacity.ToString()));

            return info;
        }
    }
}
