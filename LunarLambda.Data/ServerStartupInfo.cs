using System;

namespace LunarLambda.Data
{
    public class ServerStartupInfo : EventArgs
    {
        public string Name = string.Empty;
        public string Password = string.Empty;
        public bool Public = false;

        public string ServerWANHost = string.Empty;
        public string ServerLANAddress = string.Empty;

        public int Port = 1701;

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

        public ScenarioInfo SelectedScenario = null;
        public ScenarioInfo.VariationInfo SelectedVariation = null;
    }

}
