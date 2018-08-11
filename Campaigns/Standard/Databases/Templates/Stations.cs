using System;

using LunarLambda.API;
using LunarLambda.API.Databases;

namespace LunarLambda.Campaigns.Standard.Databases.Templates
{
    internal static class Stations
    {
        internal static void Load()
        {
            if (StateData.Exists("StandardShips.Stations.Loaded") || StateData.GetB("StandardShips.Stations.Ignore"))
                return;

            StateData.Set("StandardShips.Stations.Loaded", true);

            ShipTemplate template = TemplateDatabase.AddStation("SmallStation");
			template.SetName(Resources.SmallStationName);
            template.SetModel("space_station_4");
            template.SetDescription(Resources.SmallStationDescription);
            template.SetHull(150);
            template.SetShields(300);
            template.SetRadarTrace("radartrace_smallstation.png");

            template = TemplateDatabase.AddStation("MediumStation");
			template.SetName(Resources.MediumStationName);
			template.SetModel("space_station_3");
            template.SetDescription(Resources.MediumStationDescription);
            template.SetHull(400);
            template.SetShields(800);
            template.SetRadarTrace("radartrace_mediumstation.png");

            template = TemplateDatabase.AddStation("LargeStation");
			template.SetName(Resources.LargeStationName);
			template.SetModel("space_station_2");
            template.SetDescription(Resources.LargeStationDescription);
            template.SetHull(500);
            template.SetShields(new float[] { 1000, 1000, 1000 });
            template.SetRadarTrace("radartrace_largestation.png");

            template = TemplateDatabase.AddStation("HugeStation");
			template.SetName(Resources.HugeStationName);
			template.SetModel("space_station_1");
            template.SetDescription(Resources.HugeStationDescription);
            template.SetHull(800);
            template.SetShields(new float[] { 1200, 1200, 1200, 1200 });
            template.SetRadarTrace("radartrace_hugestation.png");
        }
    }
}
