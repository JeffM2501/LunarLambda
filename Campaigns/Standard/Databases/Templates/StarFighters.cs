using System;

using LunarLambda.API;
using LunarLambda.API.Databases;

// Starfighters

/*
Starfighters are single to 3 person small ships. These are most commonly used as light firepower roles.
They are common in larger groups. And need a close by station or support ship, as they lack long time life support.
It's rare to see starfighters with more then 1 shield section.

One of the most well known starfighters at earth is the X-Wing.

Starfighters come in 3 subclasses:
* Interceptors: Fast, low on firepower, high on maneuverability
* Gunship: Equipped with more weapons, but hands in maneuverability because of it.
* Bomber: Slowest of all starfighters, but pack a large punch in a small package. Usually come without any lasers, but the larger bombers have been known to deliver nukes.
*/

namespace LunarLambda.Campaigns.Standard.Databases.Templates
{
    internal static class StarFighters
    {
        internal static void Load()
        {
            if (StateData.Exists("StandardShips.StarFighters.Loaded") || StateData.GetB("StandardShips.StarFighters.Ignore"))
                return;

            StateData.Set("StandardShips.StarFighters.Loaded", true);

            ShipTemplate template = TemplateDatabase.AddShip("MT52 Hornet");
            template.SetClass("Starfighter", "Interceptor");
            template.SetModel("WespeScoutYellow");
            template.SetRadarTrace("radar_fighter.png");
            template.SetHull(30);
            template.SetShields(20);
            template.SetSpeed(120, 30, 25);
            template.SetDefaultAI('fighter');
            // Arc, Dir, Range, CycleTime, Dmg
            template.SetBeam(0, 30, 0, 700.0, 4.0, 2)

        }
    }
}
