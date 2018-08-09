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
        private static void LoadHorrents()
        {
            ShipTemplate template = TemplateDatabase.AddShip(Resources.Fighter_HornetName);
            template.SetDescription(Resources.Fighter_HornetDescription);
            template.SetClass("Starfighter", "Interceptor");
            template.SetModel("WespeScoutYellow");
            template.SetRadarTrace("radar_fighter.png");
            template.SetHull(30);
            template.SetShields(20);
            template.SetSpeed(120, 30, 25);
            template.SetDefaultAI("fighter");
            // Arc, Dir, Range, CycleTime, Dmg
            template.SetupBeamWeapon(0, 30, 0, 700.0f, 4.0f, 2);


            // mk2 upgrade
            ShipTemplate variation = template.CloneShip();
            variation.SetName(Resources.Fighter_HornetMk2Name);
            variation.SetDescription(Resources.Fighter_HornetMk2Description);
            variation.SetModel("WespeScoutRed");
            variation.SetHull(35);
            variation.SetShields(22);
            variation.SetSpeed(125, 32, 25);
            variation.SetupBeamWeapon(0, 30, 0, 900.0f, 4.0f, 2.5f);


            // mk 3 player variant
            variation = template.CloneShip();
            variation.SetName(Resources.Fighter_HornetMk3Name);
            variation.SetDescription(Resources.Fighter_HornetMk3Description);

            variation.SetHull(70);
            variation.SetShields(60);
            variation.SetSpeed(125, 32, 25);
            variation.SetupBeamWeapon(0, 30, 0, 900.0f, 4.0f, 2.5f);
            variation.SetupBeamWeapon(1, 30, -5, 900.0f, 4.0f, 2.5f);
            variation.SetFuelCapcity(400);

            variation.SetReairCrew(1);

            variation.AddRoom(3, 0, 1, 1, ShipSystemTypes.Maneuver);
            variation.AddRoom(1, 0, 2, 1, ShipSystemTypes.Beams);
            variation.AddRoom(0, 1, 1, 2, ShipSystemTypes.Shields(2));
            variation.AddRoom(1, 1, 2, 2, ShipSystemTypes.Reactor);
            variation.AddRoom(3, 1, 2, 1, ShipSystemTypes.FTL);
            variation.AddRoom(3, 2, 2, 1, ShipSystemTypes.Jump);
            variation.AddRoom(5, 1, 1, 2, ShipSystemTypes.Shields(1));
            variation.AddRoom(1, 3, 2, 1, ShipSystemTypes.Missiles);
            variation.AddRoom(3, 3, 1, 1, ShipSystemTypes.Sublight);

            variation.AddDoor(2, 1, true);
            variation.AddDoor(3, 1, true);
            variation.AddDoor(1, 1, false);
            variation.AddDoor(3, 1, false);
            variation.AddDoor(3, 2, false);
            variation.AddDoor(3, 3, true);
            variation.AddDoor(2, 3, true);
            variation.AddDoor(5, 1, false);
            variation.AddDoor(5, 2, false);
        }

        private static void LoadAdders()
        {
            // MK5
            ShipTemplate template = TemplateDatabase.AddShip(Resources.Fighter_AdderMk5Name);
            template.SetDescription(Resources.Fighter_AdderMk5Description);
            template.SetClass("Starfighter", "Gunship");
            template.SetModel("AdlerLongRangeScoutYellow");
            template.SetRadarTrace("radar_cruiser.png");
            template.SetHull(50);
            template.SetShields(30);
            template.SetSpeed(80, 28, 25);
            // Arc, Dir, Range, CycleTime, Dmg
            template.SetupBeamWeapon(0, 35, 0, 800, 5.0f, 2.0f);
            template.SetupBeamWeapon(1, 70, 30, 600, 5.0f, 2.0f);
            template.SetupBeamWeapon(2, 70, -35, 600, 5.0f, 2.0f);
            template.SetMissleTubeCount(1, 15);
            template.SetupMissileMagazine(MissileWeaponTypes.HVLI, 4);


            // MK4
            ShipTemplate variation = template.CloneShip();
            variation.SetName(Resources.Fighter_AdderMk4Name);
            variation.SetDescription(Resources.Fighter_AdderMk4Description);
            variation.SetModel("AdlerLongRangeScoutBlue");
            variation.SetHull(40);
            variation.SetShields(20);
            variation.SetSpeed(60, 20, 20);
            variation.SetMissleTubeCount(1, 20);
            variation.ClearMagazines();
            variation.SetupMissileMagazine(MissileWeaponTypes.HVLI, 2);


            // MK6
            variation = template.CloneShip();
            variation.SetName(Resources.Fighter_AdderMk6Name);
            variation.SetDescription(Resources.Fighter_AdderMk6Description);
            variation.SetupBeamWeapon(3, 35, 180, 600, 6.0f, 2.0f);
            variation.ClearMagazines();
            variation.SetupMissileMagazine(MissileWeaponTypes.HVLI,8);

        }

        private static void LoadLindworm()
        {
            ShipTemplate template = TemplateDatabase.AddShip(Resources.Fighter_LindwormName);
            template.SetDescription(Resources.Fighter_LindwormDescription);
            template.SetClass("Starfighter", "Bomber");
            template.SetModel("LindwurmFighterYellow");
            template.SetRadarTrace("radar_fighter.png");
            template.SetHull(50);
            template.SetShields(20);
            template.SetSpeed(50, 15, 25);
   
            template.SetMissleTubeCount(3, 15);
            template.SetupMissileMagazine(MissileWeaponTypes.HVLI, 4);
            template.SetupMissileMagazine(MissileWeaponTypes.Homing, 1);

            template.SetMissileWeaponLoadingTypes(1, MissileWeaponTypes.HVLI);
            template.SetMissileWeaponLoadingTypes(2, MissileWeaponTypes.HVLI);

            template.SetMissleWeaponDirection(1, 1);
            template.SetMissleWeaponDirection(2, -1);
        }


        internal static void Load()
        {
            if (StateData.Exists("StandardShips.StarFighters.Loaded") || StateData.GetB("StandardShips.StarFighters.Ignore"))
                return;

            StateData.Set("StandardShips.StarFighters.Loaded", true);

            LoadHorrents();
            LoadAdders();
            LoadLindworm();
        }
    }
}
