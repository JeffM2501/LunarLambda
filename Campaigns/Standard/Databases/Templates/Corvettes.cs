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



        }

        internal static void Load()
        {

        }
    }
}
