using System;
using System.Collections.Generic;

using LunarLambda.Data.Entitites;
using LunarLambda.Data.Databases;
using LunarLambda.Data.Zones;

namespace LunarLambda.Campaigns.BasicScenarios
{
    public static class SystemUtils
    {

        public static void BuildRandomStarSystem(Zone zone)
        {
            AddStar(zone);
        }

        private static void AddStar(Zone zone)
        {
            Star star = new Star();
            star.Template = new BaseTemplate();
            star.Template.Name = "Primary";

            zone.Add(star);
        }
    }
}
