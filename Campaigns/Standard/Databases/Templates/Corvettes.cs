using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.API;
using LunarLambda.API.Databases;

/* Corvette
Corvettes are the common large ships.Larger then a frigate, smaller then a dreadnaught.
They generally have 4 or more shield sections.Run with a crew of 20 to 250.
This class generally has jumpdrives or warpdrives.But lack the manouverbility that is seen in frigates.

They come in 3 different subclasses:
* Destroyer: Combat oriented ships.No science, no transport. Just death in a large package.
* Support: Large scale support roles. Drone carriers fall in this category.As well as mobile repair centers.
* Freighter: Large scale transport ships. Most common here are the jump freighters, using specialized jumpdrives to cross large distances with large amounts of cargo.
----------------------------------------------------------*/


namespace LunarLambda.Campaigns.Standard.Databases.Templates
{
    internal static class Corvettes
    {
        private static void LoadAtlantis()
        {
            ShipTemplate template = TemplateDatabase.AddShip(Resources.Corvette_AtlantisX32Name);
            template.SetDescription(Resources.Corvette_AtlantisX32Description);
            template.SetClass("Corvette", "Destroyer");
            template.SetModel("battleship_destroyer_1_upgraded");
            template.SetRadarTrace("radar_dread.png");
            template.SetHull(100);
            template.SetShields(new float[] { 200, 200, 200, 200 });
            template.SetSpeed(30, 3.5f, 5);
            template.SetJumpDrive();
                                    // Arc, Dir, Range, CycleTime, Dmg
            template.SetupBeamWeapon(0, 100, -20, 1500.0f, 6.0f, 8);
            template.SetupBeamWeapon(1, 100,  20, 1500.0f, 6.0f, 8);
            template.SetupBeamWeapon(2, 100, 180, 1500.0f, 6.0f, 8);
            template.SetMissleTubeCount(4, 10);
            template.SetupMissileMagazine(MissileWeaponTypes.HVLI, 20);
            template.SetupMissileMagazine(MissileWeaponTypes.Homing, 20);
            template.SetMissleWeaponDirection(0, -90);
            template.SetMissleWeaponDirection(1, -90);
            template.SetMissleWeaponDirection(2, 90);
            template.SetMissleWeaponDirection(3, 90);

            var variation = template.CloneShip();
            variation.SetName(Resources.Corvette_AtlantisX32PName);
            variation.SetPlayable();
            variation.SetDescription(Resources.Corvette_AtlantisX32PDescription);
            variation.SetShields(new float[] { 200, 200 });
            variation.SetHull(250);
            variation.SetSpeed(90, 10, 20);
            variation.SetCombatManeuvers(400, 250);

            variation.RemoveBeamWeapon(2);
            variation.SetupMissileMagazine(MissileWeaponTypes.HVLI, 12);
            variation.SetupMissileMagazine(MissileWeaponTypes.Homing, 12);
            variation.SetupMissileMagazine(MissileWeaponTypes.Nuke, 4);
            variation.SetupMissileMagazine(MissileWeaponTypes.Mine, 8);
            variation.SetupMissileMagazine(MissileWeaponTypes.EMP, 6);

            variation.SetMissleTubeCount(5, 8);
            for (int i = 0; i < 4; i++)
                variation.RemoveTubeLoadTypes(i, MissileWeaponTypes.Mine);

            variation.SetMissileWeaponLoadingTypes(4, MissileWeaponTypes.Mine);

            variation.AddRoom(1, 0, 2, 1, ShipSystemTypes.Maneuver);
            variation.AddRoom(1, 1, 2, 1, ShipSystemTypes.Beams);
            variation.AddRoom(2, 2, 2, 1);
            variation.AddRoom(0, 3, 1, 2, ShipSystemTypes.Shields(1));
            variation.AddRoom(1, 3, 2, 2, ShipSystemTypes.Reactor);
            variation.AddRoom(3, 3, 2, 2, ShipSystemTypes.FTL);
            variation.AddRoom(5, 3, 1, 2, ShipSystemTypes.Jump);
            variation.AddRoom(6, 3, 2, 1);
            variation.AddRoom(6, 4, 2, 1);
            variation.AddRoom(8, 3, 1, 2, ShipSystemTypes.Shields(0));
            variation.AddRoom(2, 5, 2, 1);
            variation.AddRoom(1, 6, 2, 1, ShipSystemTypes.Missiles);
            variation.AddRoom(1, 7, 2, 1, ShipSystemTypes.Sublight);

            variation.AddDoor(1, 1, true);
            variation.AddDoor(2, 2, true);
            variation.AddDoor(3, 3, true);
            variation.AddDoor(1, 3, false);
            variation.AddDoor(3, 4, false);
            variation.AddDoor(3, 5, true);
            variation.AddDoor(2, 6, true);
            variation.AddDoor(1, 7, true);
            variation.AddDoor(5, 3, false);
            variation.AddDoor(6, 3, false);
            variation.AddDoor(6, 4, false);
            variation.AddDoor(8, 3, false);
            variation.AddDoor(8, 4, false);
        }

        internal static void Load()
        {
            LoadAtlantis();
        }
    }
}
