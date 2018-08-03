using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.API.Databases;

namespace LunarLambda.Campaigns.Standard.Databases.Templates
{
    internal static class Stations
    {
        internal static void Load()
        {
            StationTemplate template = TemplateDatabase.AddStation("Small Station");
            template.SetModel("space_station_4");
            template.SetDescription("Stations of this size are often used as research outposts, listening stations, and security checkpoints.Crews turn over frequently in a small station's cramped accommodatations, but they are small enough to look like ships on many long-range sensors, and organized raiders sometimes take advantage of this by placing small stations in nebulae to serve as raiding bases. They are lightly shielded and vulnerable to swarming assaults.");
            template.SetHull(150);
            template.SetShields(300);
            template.SetRadarTrace("radartrace_smallstation.png");

            template = TemplateDatabase.AddStation("Medium Station");
            template.SetModel("space_station_3");
            template.SetDescription("Large enough to accommodate small crews for extended periods of times, stations of this size are often trading posts, refuelling bases, mining operations, and forward military bases.While their shields are strong, concerted attacks by many ships can bring them down quickly.");
            template.SetHull(400);
            template.SetShields(800);
            template.SetRadarTrace("radartrace_mediumstation.png");

            template = TemplateDatabase.AddStation("Large Station");
            template.SetModel("space_station_2");
            template.SetDescription("These spaceborne communities often represent permanent bases in a sector.Stations of this size can be military installations, commercial hubs, deep - space settlements, and small shipyards.Only a concentrated attack can penetrate a large station's shields, and its hull can withstand all but the most powerful weaponry.");
            template.SetHull(500);
            template.SetShields(new float[] { 1000, 1000, 1000 });
            template.SetRadarTrace("radartrace_largestation.png");

            template = TemplateDatabase.AddStation("Huge Station");
            template.SetModel("space_station_1");
            template.SetDescription("[[The size of a sprawling town, stations at this scale represent a faction's center of spaceborne power in a region. They serve many functions at once and represent an extensive investment of time, money, and labor. A huge station's shields and thick hull can keep it intact long enough for reinforcements to arrive, even when faced with an ongoing siege or massive, perfectly coordinated assault.");
            template.SetHull(800);
            template.SetShields(new float[] { 1200, 1200, 1200, 1200 });
            template.SetRadarTrace("radartrace_hugestation.png");
        }
    }
}
