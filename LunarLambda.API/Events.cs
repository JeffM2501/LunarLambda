using System;
using System.Collections.Generic;
using System.Linq;

namespace LunarLambda.API
{
    public class ServerStartupInfo : EventArgs
    {
        public string Name = string.Empty;
        public string Password = string.Empty;
        public bool Public = false;

        public enum FTLSettings
        {
            None = 0,
            Default,
            Warp,
            Jump,
            All,
        }
        public FTLSettings FTL = FTLSettings.Default;
        public double SensorRange = 30;
        public bool TacticalRadarMSD = true;
        public bool LongRangeRadarMSD = true;
        public enum ScanSettings
        {
            None = 0,
            Normal,
            Advanced,
        }
        public ScanSettings Scans = ScanSettings.Normal;
        public bool UseWeaponFrequencies = false;
        public bool UseSystemDamage = true;

        public Scenarios.ScenarioInfo SelectedScenario = null;
        public Scenarios.ScenarioInfo.VariationInfo SelectedVariation = null;
    }

    public static partial class Events
    {
        public class APIEventArgs : EventArgs
        {
            public bool Handled = false;
        }

        // event arg classes
        public class StringDataEventArgs : APIEventArgs
        {
            public string Data = string.Empty;

            public StringDataEventArgs(string d) : base()
            {
                Data = d;
            }
        }

        // events
        // database load events
        public static event EventHandler SetupModelData = null;
        public static event EventHandler SetupShipTemplates = null;
        public static event EventHandler SetupFactions = null;
        public static event EventHandler SetupScienceDB = null;

		public static event EventHandler FinalizeDatabases = null;

		public static event EventHandler MissionStart = null;

		public static event EventHandler<ServerStartupInfo> GetDefaultServerInfo = null;
		public static event EventHandler<ServerStartupInfo> GetServerInfoForScenario = null;
	}
}
