using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.API;
using LunarLambda.Data.Databases;

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
        private static void LoadStarhammer()
        {
            ShipTemplate template = TemplateDatabase.AddShip("Starhammer2");
			template.SetName(Resources.Corvette_StarhammerName);
            template.SetDescription(Resources.Corvette_StarhammerDescription);
            template.SetClass("Corvette", "Destroyer");
            template.SetModel("battleship_destroyer_4_upgraded");
            template.SetRadarTrace("radar_dread.png");
            template.SetHull(200);
            template.SetShields(new float[] { 450, 350, 150, 150, 350 });
            template.SetSpeed(335, 6, 10);
            template.SetJumpDrive();
            // Arc, Dir, Range, CycleTime, Dmg
            template.SetupBeamWeapon(0, 60, -10, 2000, 8, 11);
            template.SetupBeamWeapon(1, 60,  10, 2000, 8, 11);
            template.SetupBeamWeapon(2, 60, -20, 1500, 8, 11);
            template.SetupBeamWeapon(3, 60,  20, 1500, 8, 11);

            template.SetMissleTubeCount(2, 10);
            template.SetupMissileMagazine(MissileWeaponTypes.HVLI, 20);
            template.SetupMissileMagazine(MissileWeaponTypes.Homing, 4);
            template.SetupMissileMagazine(MissileWeaponTypes.EMP, 2);
            template.RemoveTubeLoadTypes(1, MissileWeaponTypes.EMP);
        }

        private static void LoadAtlantis()
        {
            ShipTemplate template = TemplateDatabase.AddShip("AtlantisAI");
			template.SetName(Resources.Corvette_AtlantisX32Name);
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

            template.AddRoom(1, 0, 2, 1, ShipSystemTypes.Maneuver);
            template.AddRoom(1, 1, 2, 1, ShipSystemTypes.Beams);
            template.AddRoom(2, 2, 2, 1);
            template.AddRoom(0, 3, 1, 2, ShipSystemTypes.Shields(1));
            template.AddRoom(1, 3, 2, 2, ShipSystemTypes.Reactor);
            template.AddRoom(3, 3, 2, 2, ShipSystemTypes.FTL);
            template.AddRoom(5, 3, 1, 2, ShipSystemTypes.Jump);
            template.AddRoom(6, 3, 2, 1);
            template.AddRoom(6, 4, 2, 1);
            template.AddRoom(8, 3, 1, 2, ShipSystemTypes.Shields(0));
            template.AddRoom(2, 5, 2, 1);
            template.AddRoom(1, 6, 2, 1, ShipSystemTypes.Missiles);
            template.AddRoom(1, 7, 2, 1, ShipSystemTypes.Sublight);

            template.AddDoor(1, 1, true);
            template.AddDoor(2, 2, true);
            template.AddDoor(3, 3, true);
            template.AddDoor(1, 3, false);
            template.AddDoor(3, 4, false);
            template.AddDoor(3, 5, true);
            template.AddDoor(2, 6, true);
            template.AddDoor(1, 7, true);
            template.AddDoor(5, 3, false);
            template.AddDoor(6, 3, false);
            template.AddDoor(6, 4, false);
            template.AddDoor(8, 3, false);
            template.AddDoor(8, 4, false);


            // player version
            var variation = template.CloneShip("AtlantisPlayer");
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
        }

        private static void LoadSupport()
        {
            ShipTemplate template = TemplateDatabase.AddShip("DefensePlatform");
			template.SetName(Resources.Corvette_DefensePlatformName);
            template.SetDescription(Resources.Corvette_DefensePlatformDescription);
            template.SetClass("Corvette", "Support");
            template.SetModel("space_station_4");
            template.SetRadarTrace("radartrace_smallstation.png");
            template.SetHull(150);
            template.SetShields(new float[] { 120, 120, 120, 120, 120, 120 });
            template.SetSpeed(0, 0.5f, 0);

            // Arc, Dir, Range, CycleTime, Dmg
            template.SetupBeamWeapon(0, 30,   0, 4000.0f, 1.5f, 20);
            template.SetupBeamWeapon(1, 30,  60, 4000.0f, 1.5f, 20);
            template.SetupBeamWeapon(2, 30, 120, 4000.0f, 1.5f, 20);
            template.SetupBeamWeapon(3, 30, 180, 4000.0f, 1.5f, 20);
            template.SetupBeamWeapon(4, 30, 240, 4000.0f, 1.5f, 20);
            template.SetupBeamWeapon(5, 30, 300, 4000.0f, 1.5f, 20);

            ShipTemplate.DockingPortInfo port = new ShipTemplate.DockingPortInfo();
            port.Legacy = true;
            port.MaxSize = DockingClasses.Small;
            template.DockingPorts.Add(port);

        }

        private static void AddFreighter(string haul, string displayName, string description, int index, int baseModelIndex, bool jump)
        {
            string name = displayName + (jump ? "JumpFreighter" : "Freighter")+ index.ToString();
			string display = displayName + " " + (jump ? Resources.JumpFreighterBaseName : Resources.FreighterBaseName) + " " + index.ToString();

			ShipTemplate template = TemplateDatabase.AddShip(name);
			template.SetName(display);
            template.SetDescription(description);
            template.SetClass("Corvette", "Freighter");
            template.SetModel("transport_" + baseModelIndex.ToString() + "_" + index.ToString());
            template.SetRadarTrace("radartrace_smallstation.png");
            template.SetHull(150);
            template.SetShields(new float[] { 50,50 });
            template.SetSpeed(60 - 5 * index, 6, 10);
            if (jump)
            {
                template.SetJumpDrive();
                template.SetJumpRanges(2000, 40000);
            }
        }

        private static void LoadFreighters()
        {
            string[] hauls = Resources.HaulTypes.Split(";".ToCharArray());
			string[] haulDisplayNames = Resources.HaulDisplayNames.Split(";".ToCharArray());
			string[] descriptions = Resources.HaulFreighterDescriptions.Split(";".ToCharArray());
            for (int i = 0; i < hauls.Length; i++)
            {
                string description = string.Empty;
                if (descriptions.Length < i)
                    description = descriptions[i];
                else
                    description = Resources.DefaultFreightHaulerDescription;

                for (int j = 0; j < 5; j++)
                    AddFreighter(hauls[i], haulDisplayNames[i], description, j + 1, i + 1, j < 2);
            }
        }

        private static void LoadJumpCarrier()
        {
            ShipTemplate template = TemplateDatabase.AddShip("JumpCarrier");
			template.SetName(Resources.Corvette_JumpCarrierName);
            template.SetDescription(Resources.Corvette_JumpCarrierDescription);
            template.SetClass("Corvette", "Freighter");
            template.SetModel("transport_4_2");
            template.SetRadarTrace("radar_transport.png");
            template.SetHull(100);
            template.SetShields(new float[] { 50, 50 });
            template.SetSpeed(50, 6, 10);
            template.SetJumpDrive();
            template.SetJumpRanges(5000, 100 * 50000);

            for (int i = 0; i < 20; i++)
            {
                ShipTemplate.DockingPortInfo port = new ShipTemplate.DockingPortInfo();
                port.Legacy = true;
                port.MaxSize = DockingClasses.Medium;
                template.DockingPorts.Add(port);
            }
        }

        internal static void Load()
        {
            LoadAtlantis();
            LoadStarhammer();
            LoadSupport();
            LoadFreighters();
            LoadJumpCarrier();
        }
    }
}
