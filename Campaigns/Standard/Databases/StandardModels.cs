using System;

using LunarLambda.API;
using LunarLambda.API.Databases;

namespace LunarLambda.Campaigns.Standard.Databases
{
    internal static class StandardModels
    {
        internal static void Load(object sender, EventArgs e)
        {
            if (StateData.Exists("StandardModels.Loaded") || StateData.GetB("StandardModels.Ignore"))
                return;

            StateData.Set("StandardModels.Loaded", true);

            var model = ModelDatabase.AddModel("space_station_4");
            model.SetMesh("space_station_4/space_station_4.model");
            model.SetTexture("space_station_4/space_station_4_color.jpg");
            model.SetSpecular("space_station_4/space_station_4_specular.jpg");
            model.SetIllumination("space_station_4/space_station_4_illumination.jpg");
            model.SetRenderOffset(0, 0, 5);
            model.SetScale(10);
            model.SetRadius(300);
            model.SetCollisionBox(400, 400);

            model = ModelDatabase.AddModel("space_station_3");
            model.SetMesh("space_station_3/space_station_3.model");
            model.SetTexture("space_station_3/space_station_3_color.jpg");
            model.SetSpecular("space_station_3/space_station_3_specular.jpg");
            model.SetIllumination("space_station_3/space_station_3_illumination.jpg");
            model.SetRenderOffset(10, 0, 5);
            model.SetScale(20);
            model.SetRadius(1000);
            model.SetCollisionBox(1200, 1000);

            model = ModelDatabase.AddModel("space_station_2");
            model.SetMesh("space_station_2/space_station_2.model");
            model.SetTexture("space_station_2/space_station_2_color.jpg");
            model.SetSpecular("space_station_2/space_station_2_specular.jpg");
            model.SetIllumination("space_station_2/space_station_2_illumination.jpg");
            model.SetRenderOffset(10, 0, 5);
            model.SetScale(20);
            model.SetRadius(1300);
            model.SetCollisionBox(1400, 1000);

            model = ModelDatabase.AddModel("space_station_1");
            model.SetMesh("space_station_1/space_station_1.model");
            model.SetTexture("space_station_1/space_station_1_color.jpg");
            model.SetSpecular("space_station_1/space_station_1_specular.jpg");
            model.SetIllumination("space_station_1/space_station_1_illumination.jpg");
            model.SetRenderOffset(0, 0, 5);
            model.SetScale(20);
            model.SetRadius(1500);
            model.SetCollisionBox(2000, 1800);

            model = ModelDatabase.AddModel("small_fighter_1");
            model.SetMesh("small_fighter_1.model");
            model.SetTexture("small_fighter_1_color.jpg");
            model.SetSpecular("small_fighter_1_specular.jpg");
            model.SetIllumination("small_fighter_1_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(40);

            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(23, 0, -1.8);
            model.AddEngineEmitter(-8, 0, 0.5, 1.0, 0.2, 0.2, 1.5);

            model = ModelDatabase.AddModel("space_tug");
            model.SetMesh("space_tug.model");
            model.SetTexture("space_tug_color.jpg");
            // SetSpecular "space_tug_illumination.jpg"
            model.SetIllumination("space_tug_illumination.jpg");
            model.SetScale(6);
            model.SetRadius(80);

            model.AddEngineEmitter(-13, -2.1500, 0.3, 0.2, 0.2, 1.0, 3.0);
            model.AddEngineEmitter(-13, 2.1500, 0.3, 0.2, 0.2, 1.0, 3.0);

            model = ModelDatabase.AddModel("space_frigate_6");
            model.SetMesh("space_frigate_6.model");
            model.SetTexture("space_frigate_6_color.png");
            model.SetSpecular("space_frigate_6_specular.png");
            model.SetIllumination("space_frigate_6_illumination.png");
            model.SetScale(6);
            model.SetRadius(100);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(-1.6, -8, -2);
            model.AddBeamPosition(-1.6, 8, -2);
            model.AddTubePosition(18, 0, -3.5);
            model.AddTubePosition(18, 0, -3.5);
            model.AddEngineEmitter(-18, 0, -1, 0.2, 0.2, 1.0, 6.0);

            model = ModelDatabase.AddModel("space_cruiser_4");
            model.SetMesh("space_cruiser_4.model");
            model.SetTexture("space_cruiser_4_color.jpg");
            // SetSpecular "space_cruiser_4_illumination.jpg"
            model.SetIllumination("space_cruiser_4_illumination.jpg");
            model.SetScale(8);
            model.SetRadius(100);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddTubePosition(2, -10, -2.3);
            model.AddTubePosition(2, 10, -2.3);
            model.AddEngineEmitter(-13, -2.1500, 0.3, 0.2, 0.2, 1.0, 3.0);
            model.AddEngineEmitter(-13, 2.1500, 0.3, 0.2, 0.2, 1.0, 3.0);

            model = ModelDatabase.AddModel("dark_fighter_6");
            model.SetMesh("dark_fighter_6.model");
            model.SetTexture("dark_fighter_6_color.png");
            model.SetSpecular("dark_fighter_6_specular.png");
            model.SetIllumination("dark_fighter_6_illumination.png");
            model.SetScale(5);
            model.SetRadius(140);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(21, -28.2, -2);
            model.AddBeamPosition(21, 28.2, -2);
            model.AddEngineEmitter(-28, -1.5, -5, 1.0, 0.2, 0.2, 3.0);
            model.AddEngineEmitter(-28, 1.5, -5, 1.0, 0.2, 0.2, 3.0);

            model = ModelDatabase.AddModel("battleship_destroyer_1_upgraded");
            model.SetMesh("battleship_destroyer_1_upgraded/battleship_destroyer_1_upgraded.model");
            model.SetTexture("battleship_destroyer_1_upgraded/battleship_destroyer_1_upgraded_color.jpg");
            model.SetSpecular("battleship_destroyer_1_upgraded/battleship_destroyer_1_upgraded_specular.jpg");
            model.SetIllumination("battleship_destroyer_1_upgraded/battleship_destroyer_1_upgraded_illumination.jpg");
            model.SetScale(4);
            model.SetRadius(200);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(34, -17, -7);
            model.AddBeamPosition(34, 17, -7);
            model.AddBeamPosition(-5, -33, -1);
            model.AddBeamPosition(-5, 33, -1);
            model.AddTubePosition(5, -7, -11);
            model.AddTubePosition(5, 7, -11);
            model.AddEngineEmitter(-30, 0, -3, 1.0, 0.2, 0.1, 14.0);
            model.AddEngineEmitter(-33, 12, -1, 1.0, 0.2, 0.1, 17.0);
            model.AddEngineEmitter(-33, -12, -1, 1.0, 0.2, 0.1, 17.0);
            model.AddEngineEmitter(-33, 22, -1, 1.0, 0.2, 0.1, 14.0);
            model.AddEngineEmitter(-33, -22, -1, 1.0, 0.2, 0.1, 14.0);


            model = ModelDatabase.AddModel("battleship_destroyer_2_upgraded");
            model.SetMesh("battleship_destroyer_2_upgraded/battleship_destroyer_2_upgraded.model");
            model.SetTexture("battleship_destroyer_2_upgraded/battleship_destroyer_2_upgraded_color.jpg");
            model.SetSpecular("battleship_destroyer_2_upgraded/battleship_destroyer_2_upgraded_specular.jpg");
            model.SetIllumination("battleship_destroyer_2_upgraded/battleship_destroyer_2_upgraded_illumination.jpg");
            model.SetScale(4);
            model.SetRadius(200);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(37, -14.5, -3.5);
            model.AddBeamPosition(37, 14.5, -3.5);
            model.AddBeamPosition(10, -12, -15);
            model.AddBeamPosition(10, 12, -15);
            model.AddBeamPosition(-9, -28, -3);
            model.AddBeamPosition(-9, 28, -3);
            model.AddEngineEmitter(-33, 0, -3, 1.0, 0.2, 0.1, 14.0);
            model.AddEngineEmitter(-36, 14, -4, 1.0, 0.2, 0.1, 17.0);
            model.AddEngineEmitter(-36, -14, -4, 1.0, 0.2, 0.1, 17.0);


            model = ModelDatabase.AddModel("battleship_destroyer_3_upgraded");
            model.SetMesh("battleship_destroyer_3_upgraded/battleship_destroyer_3_upgraded.model");
            model.SetTexture("battleship_destroyer_3_upgraded/battleship_destroyer_3_upgraded_color.jpg");
            model.SetSpecular("battleship_destroyer_3_upgraded/battleship_destroyer_3_upgraded_specular.jpg");
            model.SetIllumination("battleship_destroyer_3_upgraded/battleship_destroyer_3_upgraded_illumination.jpg");
            model.SetScale(4);
            model.SetRadius(200);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(22, 22, -4.5);
            model.AddBeamPosition(22, -22, -4.5);
            model.AddBeamPosition(12, 22, -4.5);
            model.AddBeamPosition(12, -22, -4.5);

            model.AddEngineEmitter(-34, 0, 0, 1.0, 0.2, 0.1, 14.0);
            model.AddEngineEmitter(-36, 13, 0, 1.0, 0.2, 0.1, 17.0);
            model.AddEngineEmitter(-36, -13, 0, 1.0, 0.2, 0.1, 17.0);


            model = ModelDatabase.AddModel("battleship_destroyer_4_upgraded");
            model.SetMesh("battleship_destroyer_4_upgraded/battleship_destroyer_4_upgraded.model");
            model.SetTexture("battleship_destroyer_4_upgraded/battleship_destroyer_4_upgraded_color.jpg");
            model.SetSpecular("battleship_destroyer_4_upgraded/battleship_destroyer_4_upgraded_specular.jpg");
            model.SetIllumination("battleship_destroyer_4_upgraded/battleship_destroyer_4_upgraded_illumination.jpg");
            model.SetScale(4);
            model.SetRadius(200);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(4, -27, -0.5);
            model.AddBeamPosition(4, 27, -0.5);
            model.AddTubePosition(30, -11, -0.5);
            model.AddTubePosition(30, 11, -0.5);
            model.AddEngineEmitter(-27, 0, -4, 1.0, 0.2, 0.1, 14.0);
            model.AddEngineEmitter(-32, 11.5, -1.5, 1.0, 0.2, 0.1, 13.0);
            model.AddEngineEmitter(-32, -11.5, -1.5, 1.0, 0.2, 0.1, 13.0);
            model.AddEngineEmitter(-30, 20, -1.5, 1.0, 0.2, 0.1, 10.0);
            model.AddEngineEmitter(-32, -20, -1.5, 1.0, 0.2, 0.1, 10.0);


            model = ModelDatabase.AddModel("battleship_destroyer_5_upgraded");
            model.SetMesh("battleship_destroyer_5_upgraded/battleship_destroyer_5_upgraded.model");
            model.SetTexture("battleship_destroyer_5_upgraded/battleship_destroyer_5_upgraded_color.jpg");
            model.SetSpecular("battleship_destroyer_5_upgraded/battleship_destroyer_5_upgraded_specular.jpg");
            model.SetIllumination("battleship_destroyer_5_upgraded/battleship_destroyer_5_upgraded_illumination.jpg");
            model.SetScale(4);
            model.SetRadius(200);

            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(4, -27, -0.5);
            model.AddBeamPosition(4, 27, -0.5);
            model.AddTubePosition(27, 0, -0.5);
            model.AddTubePosition(27, 0, -0.5);
            model.AddEngineEmitter(-33, 0, 0, 1.0, 0.2, 0.1, 16.0);
            model.AddEngineEmitter(-28, 13, -0, 1.0, 0.2, 0.1, 13.0);
            model.AddEngineEmitter(-28, -13, -0, 1.0, 0.2, 0.1, 13.0);
            model.AddEngineEmitter(-27, 24, 0, 1.0, 0.2, 0.1, 5.0);
            model.AddEngineEmitter(-27, -24, 0, 1.0, 0.2, 0.1, 5.0);



            model = ModelDatabase.AddModel("Ender Battlecruiser");
            model.SetMesh("Ender Battlecruiser.obj");
            model.SetTexture("Ender Battlecruiser.png");
            model.SetSpecular("Ender Battlecruiser_illumination.png");
            model.SetIllumination("Ender Battlecruiser_illumination.png");
            model.SetScale(5);
            model.SetRadius(1000);
            model.SetCollisionBox(2000, 600);
            //Visual positions of the beams / missiletubes in blender: -X, Y, Z
            model.AddBeamPosition(66, -71, 12);
            model.AddBeamPosition(66, -71, -12);
            model.AddBeamPosition(66, 71, 12);
            model.AddBeamPosition(66, 71, -12);
            model.AddBeamPosition(-32, -71, 12);
            model.AddBeamPosition(-32, -71, -12);
            model.AddBeamPosition(-32, 71, 12);
            model.AddBeamPosition(-32, 71, -12);
            model.AddBeamPosition(-112, -71, 12);
            model.AddBeamPosition(-112, -71, -12);
            model.AddBeamPosition(-112, 71, 12);
            model.AddBeamPosition(-112, 71, -12);
            model.AddEngineEmitter(-180, -30, 1.2, 0.2, 0.2, 1.0, 30.0);
            model.AddEngineEmitter(-180, 30, 1.2, 0.2, 0.2, 1.0, 30.0);



            model = ModelDatabase.AddModel("sci_fi_alien_ship_1");
            model.SetMesh("sci_fi_alien_ship_1/sci_fi_alien_ship_1.model");
            model.SetTexture("sci_fi_alien_ship_1/sci_fi_alien_ship_1_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_1/sci_fi_alien_ship_1_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_1/sci_fi_alien_ship_1_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(180);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_2");
            model.SetMesh("sci_fi_alien_ship_2/sci_fi_alien_ship_2.model");
            model.SetTexture("sci_fi_alien_ship_2/sci_fi_alien_ship_2_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_2/sci_fi_alien_ship_2_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_2/sci_fi_alien_ship_2_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(180);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_3");
            model.SetMesh("sci_fi_alien_ship_3/sci_fi_alien_ship_3.model");
            model.SetTexture("sci_fi_alien_ship_3/sci_fi_alien_ship_3_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_3/sci_fi_alien_ship_3_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_3/sci_fi_alien_ship_3_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(150);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_4");
            model.SetMesh("sci_fi_alien_ship_4/sci_fi_alien_ship_4.model");
            model.SetTexture("sci_fi_alien_ship_4/sci_fi_alien_ship_4_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_4/sci_fi_alien_ship_4_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_4/sci_fi_alien_ship_4_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(150);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_5");
            model.SetMesh("sci_fi_alien_ship_5/sci_fi_alien_ship_5.model");
            model.SetTexture("sci_fi_alien_ship_5/sci_fi_alien_ship_5_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_5/sci_fi_alien_ship_5_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_5/sci_fi_alien_ship_5_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(150);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_6");
            model.SetMesh("sci_fi_alien_ship_6/sci_fi_alien_ship_6.model");
            model.SetTexture("sci_fi_alien_ship_6/sci_fi_alien_ship_6_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_6/sci_fi_alien_ship_6_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_6/sci_fi_alien_ship_6_illumination.jpg");
            model.SetScale(3);
            model.SetRadius(150);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_7");
            model.SetMesh("sci_fi_alien_ship_7/sci_fi_alien_ship_7.model");
            model.SetTexture("sci_fi_alien_ship_7/sci_fi_alien_ship_7_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_7/sci_fi_alien_ship_7_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_7/sci_fi_alien_ship_7_illumination.jpg");
            model.SetScale(6);
            model.SetRadius(330);


            model = ModelDatabase.AddModel("sci_fi_alien_ship_8");
            model.SetMesh("sci_fi_alien_ship_8/sci_fi_alien_ship_8.model");
            model.SetTexture("sci_fi_alien_ship_8/sci_fi_alien_ship_8_color.jpg");
            model.SetSpecular("sci_fi_alien_ship_8/sci_fi_alien_ship_8_specular.jpg");
            model.SetIllumination("sci_fi_alien_ship_8/sci_fi_alien_ship_8_illumination.jpg");
            model.SetScale(6);
            model.SetRadius(350);


            model = ModelDatabase.AddModel("ammo_box");
            model.SetRadius(50);
            model.SetMesh("ammo_box.obj");
            model.SetTexture("ammo_box.png");
            model.SetSpecular("ammo_box_specular.png");
            model.SetIllumination("ammo_box_illumination.png");


            model = ModelDatabase.AddModel("shield_generator");
            model.SetRadius(150);
            model.SetScale(3);
            model.SetMesh("Shield bubble generator.obj");
            model.SetTexture("Shield bubble generator.jpg");
            model.SetSpecular("Shield bubble generator specular.jpg");


            model = ModelDatabase.AddModel("small_frigate_1");
            model.SetMesh("small_frigate_1/small_frigate_1.model");
            model.SetTexture("small_frigate_1/small_frigate_1_color.png");
            model.SetSpecular("small_frigate_1/small_frigate_1_specular.png");
            model.SetIllumination("small_frigate_1/small_frigate_1_illumination.png");
            model.SetScale(1);
            model.SetRadius(100);

            model.AddEngineEmitter(-85, 33, 2, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-85, 33, 14, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-85, -33, 2, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-85, -33, 14, 0.2, 0.2, 1.0, 8.0);


            model = ModelDatabase.AddModel("small_frigate_2");
            model.SetMesh("small_frigate_2/small_frigate_2.model");
            model.SetTexture("small_frigate_2/small_frigate_2_color.png");
            model.SetSpecular("small_frigate_2/small_frigate_2_specular.png");
            model.SetIllumination("small_frigate_2/small_frigate_2_illumination.png");
            model.SetScale(1);
            model.SetRadius(80);

            model.AddEngineEmitter(-75, 53, -27, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-75, -53, -27, 0.2, 0.2, 1.0, 8.0);


            model = ModelDatabase.AddModel("small_frigate_3");
            model.SetMesh("small_frigate_3/small_frigate_3.model");
            model.SetTexture("small_frigate_3/small_frigate_3_color.png");
            model.SetSpecular("small_frigate_3/small_frigate_3_specular.png");
            model.SetIllumination("small_frigate_3/small_frigate_3_illumination.png");
            model.SetScale(0.8f);
            model.SetRadius(80);

            model.AddEngineEmitter(-91, 32, -4, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-95, 32, -17, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-91, -32, -4, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-95, -32, -17, 0.2, 0.2, 1.0, 8.0);


            model = ModelDatabase.AddModel("small_frigate_4");
            model.SetMesh("small_frigate_4/small_frigate_4.model");
            model.SetTexture("small_frigate_4/small_frigate_4_color.png");
            model.SetSpecular("small_frigate_4/small_frigate_4_specular.png");
            model.SetIllumination("small_frigate_4/small_frigate_4_illumination.png");
            model.SetScale(1);
            model.SetRadius(100);

            model.AddEngineEmitter(-81, 10, -4, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-81, 0, -4, 0.2, 0.2, 1.0, 8.0);
            model.AddEngineEmitter(-81, -10, -4, 0.2, 0.2, 1.0, 8.0);


            model = ModelDatabase.AddModel("small_frigate_5");
            model.SetMesh("small_frigate_5/small_frigate_5.model");
            model.SetTexture("small_frigate_5/small_frigate_5_color.png");
            model.SetSpecular("small_frigate_5/small_frigate_5_specular.png");
            model.SetIllumination("small_frigate_5/small_frigate_5_illumination.png");
            model.SetScale(1);
            model.SetRadius(80);

            model.AddEngineEmitter(-95, 30, 8, 0.2, 0.2, 1.0, 5.0);
            model.AddEngineEmitter(-95, 30, 0, 0.2, 0.2, 1.0, 5.0);
            model.AddEngineEmitter(-95, 30, -8, 0.2, 0.2, 1.0, 5.0);
            model.AddEngineEmitter(-95, -30, 8, 0.2, 0.2, 1.0, 5.0);
            model.AddEngineEmitter(-95, -30, 0, 0.2, 0.2, 1.0, 5.0);
            model.AddEngineEmitter(-95, -30, -8, 0.2, 0.2, 1.0, 5.0);


            foreach (string color in new string[] { "Blue", "Green", "Grey", "Red", "White", "Yellow" })
            {
                model = ModelDatabase.AddModel("AdlerLongRangeScout" + color);
                model.SetMesh("AdlerLongRangeScout/AdlerLongRangeSoutHull.model");
                model.SetTexture("AdlerLongRangeScout/AlbedoAO/AdlerLongRangeScout" + color + "AlbedoAO.png");
                model.SetSpecular("AdlerLongRangeScout/AdlerLongRangeScoutPBRSpecular.png");
                model.SetIllumination("AdlerLongRangeScout/AdlerLongRangeScoutIllumination.png");
                model.SetScale(20);
                model.SetRadius(30);
                // Visual positions of the beams / missiletubes blender: -X, Y, Z
                model.AddBeamPosition(1.8, 0, 0.03);
                model.AddBeamPosition(1.8, 0.13, 0.03);
                model.AddBeamPosition(1.8, -0.13, 0.03);
                model.AddTubePosition(1.8, 0, 0.03);
                model.AddEngineEmitter(-1.5, 0.42, -0.13, 0.5, 0.5, 1.0, 0.5);
                model.AddEngineEmitter(-1.5, -0.42, -0.13, 0.5, 0.5, 1.0, 0.5);
                model.AddEngineEmitter(-1.5, 0.42, 0.33, 0.5, 0.5, 1.0, 0.5);
                model.AddEngineEmitter(-1.5, -0.42, 0.33, 0.5, 0.5, 1.0, 0.5);


                model = ModelDatabase.AddModel("AtlasHeavyFighter" + color);
                model.SetMesh("AtlasHeavyFighter/AtlasHeavyFighterHull.model");
                model.SetTexture("AtlasHeavyFighter/AlbedoAO/AtlasHeavyFighter" + color + "AlbedoAO.png");
                model.SetSpecular("AtlasHeavyFighter/AtlasHeavyFighterPBRSpecular.png");
                model.SetIllumination("AtlasHeavyFighter/AtlasHeavyFighterIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                // Visual positions of the beams / missiletubes blender: -X, Y, Z
                model.AddBeamPosition(2.4, 0.1, -0.25);
                model.AddBeamPosition(2.4, -0.1, -0.25);
                model.AddTubePosition(1, 0.4, 0);
                model.AddTubePosition(1, -0.4, 0);
                model.AddTubePosition(-1, 0, 0);//Mine tube
                model.AddEngineEmitter(-1.1, 0.7, 0.0, 0.5, 0.5, 1.0, 0.4);
                model.AddEngineEmitter(-1.1, 1.05, 0.0, 0.5, 0.5, 1.0, 0.4);
                model.AddEngineEmitter(-1.1, -0.7, 0.0, 0.5, 0.5, 1.0, 0.4);
                model.AddEngineEmitter(-1.1, -1.05, 0.0, 0.5, 0.5, 1.0, 0.4);


                model = ModelDatabase.AddModel("LindwurmFighter" + color);
                model.SetMesh("LindwurmFighter/LindwurmFighterHull.model");
                model.SetTexture("LindwurmFighter/AlbedoAO/LindwurmFighter" + color + "AlbedoAO.png");
                model.SetSpecular("LindwurmFighter/LindwurmFighterPBRSpecular.png");
                model.SetIllumination("LindwurmFighter/LindwurmFighterIllumination.png");
                model.SetScale(20);
                model.SetRadius(30);
                model.AddTubePosition(1.4, 0, 0.06);
                model.AddTubePosition(0.3, 0.5, 0.06);
                model.AddTubePosition(0.3, -0.5, 0.06);
                model.AddEngineEmitter(-1, 0.54, 0.0, 0.5, 0.5, 1.0, 0.4);
                model.AddEngineEmitter(-1, -0.54, 0.0, 0.5, 0.5, 1.0, 0.4);


                model = ModelDatabase.AddModel("WespeScout" + color);
                model.SetMesh("WespeScout/WespeScoutHull.model");
                model.SetTexture("WespeScout/AlbedoAO/WespeScout" + color + "AlbedoAO.png");
                model.SetSpecular("WespeScout/WespeScoutPBRSpecular.png");
                model.SetIllumination("WespeScout/WespeScoutIllumination.png");
                model.SetScale(20);
                model.SetRadius(30);
                model.AddBeamPosition(1.15, 0.13, -0.03);
                model.AddBeamPosition(1.15, -0.13, -0.03);
                model.AddEngineEmitter(-0.2, 0.44, -0.03, 0.5, 0.5, 1.0, 0.4);
                model.AddEngineEmitter(-0.2, -0.44, -0.03, 0.5, 0.5, 1.0, 0.4);


                model = ModelDatabase.AddModel("HeavyCorvette" + color);
                model.SetMesh("HeavyCorvette/HeavyCorvette.model");
                model.SetTexture("HeavyCorvette/AlbedoAO/HeavyCorvette" + color + "AlbedoAO.png");
                model.SetSpecular("HeavyCorvette/HeavyCorvettePBRSpecular.png");
                model.SetIllumination("HeavyCorvette/HeavyCorvetteIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                model.AddEngineEmitter(-1.4, 0.4, 0.0, 0.5, 0.5, 1.0, 0.6);
                model.AddEngineEmitter(-1.4, 0.0, 0.0, 0.5, 0.5, 1.0, 0.6);
                model.AddEngineEmitter(-1.4, -0.4, 0.0, 0.5, 0.5, 1.0, 0.6);


                model = ModelDatabase.AddModel("LaserCorvette" + color);
                model.SetMesh("LaserCorvette/LaserCorvette.model");
                model.SetTexture("LaserCorvette/AlbedoAO/LaserCorvette" + color + "AlbedoAO.png");
                model.SetSpecular("LaserCorvette/LaserCorvettePBRSpecular.png");
                model.SetIllumination("LaserCorvette/LaserCorvetteIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                model.AddEngineEmitter(-1.67, 0.1, 0.0, 0.5, 0.5, 1.0, 0.2);
                model.AddEngineEmitter(-1.67, -0.1, 0.0, 0.5, 0.5, 1.0, 0.2);


                model = ModelDatabase.AddModel("LightCorvette" + color);
                model.SetMesh("LightCorvette/LightCorvette.model");
                model.SetTexture("LightCorvette/AlbedoAO/LightCorvette" + color + "AlbedoAO.png");
                model.SetSpecular("LightCorvette/LightCorvettePBRSpecular.png");
                model.SetIllumination("LightCorvette/LightCorvetteIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                model.AddEngineEmitter(-1.3, 0.22, 0.15, 0.5, 0.5, 1.0, 0.3);
                model.AddEngineEmitter(-1.3, 0.00, 0.15, 0.5, 0.5, 1.0, 0.3);
                model.AddEngineEmitter(-1.3, -0.22, 0.15, 0.5, 0.5, 1.0, 0.3);


                model = ModelDatabase.AddModel("MineLayerCorvette" + color);
                model.SetMesh("MineLayerCorvette/MineLayerCorvette.model");
                model.SetTexture("MineLayerCorvette/AlbedoAO/MineLayerCorvette" + color + "AlbedoAO.png");
                model.SetSpecular("MineLayerCorvette/MineLayerCorvettePBRSpecular.png");
                model.SetIllumination("MineLayerCorvette/MineLayerCorvetteIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                model.AddEngineEmitter(-0.65, 0.70, 0.0, 0.5, 0.5, 1.0, 0.2);
                model.AddEngineEmitter(-0.65, -0.70, 0.0, 0.5, 0.5, 1.0, 0.2);


                model = ModelDatabase.AddModel("MissileCorvette" + color);
                model.SetMesh("MissileCorvette/MissileCorvette.model");
                model.SetTexture("MissileCorvette/AlbedoAO/MissileCorvette" + color + "AlbedoAO.png");
                model.SetSpecular("MissileCorvette/MissileCorvettePBRSpecular.png");
                model.SetIllumination("MissileCorvette/MissileCorvetteIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                model.AddEngineEmitter(-0.75, 0.24, -0.1, 0.5, 0.5, 1.0, 0.2);
                model.AddEngineEmitter(-0.75, 0.00, -0.1, 0.5, 0.5, 1.0, 0.2);
                model.AddEngineEmitter(-0.75, -0.24, -0.1, 0.5, 0.5, 1.0, 0.2);


                model = ModelDatabase.AddModel("MultiGunCorvette" + color);
                model.SetMesh("MultiGunCorvette/MultiGunCorvette.model");
                model.SetTexture("MultiGunCorvette/AlbedoAO/MultiGunCorvette" + color + "AlbedoAO.png");
                model.SetSpecular("MultiGunCorvette/MultiGunCorvettePBRSpecular.png");
                model.SetIllumination("MultiGunCorvette/MultiGunCorvetteIllumination.png");
                model.SetScale(50);
                model.SetRadius(80);

                model.AddEngineEmitter(-0.75, 0.2, -0.03, 0.5, 0.5, 1.0, 0.2);
                model.AddEngineEmitter(-0.75, 0.0, -0.03, 0.5, 0.5, 1.0, 0.2);
                model.AddEngineEmitter(-0.75, -0.2, -0.03, 0.5, 0.5, 1.0, 0.2);

            }

            model = ModelDatabase.AddModel("SensorBuoyMKI");
            model.SetMesh("SensorBuoy/SensorBuoyMKI.model");
            model.SetTexture("SensorBuoy/SensorBuoyAlbedoAO.png");
            model.SetSpecular("SensorBuoy/SensorBuoyPBRSpecular.png");
            model.SetScale(300);
            model.SetRadius(15);


            model = ModelDatabase.AddModel("SensorBuoyMKII");
            model.SetMesh("SensorBuoy/SensorBuoyMKII.model");
            model.SetTexture("SensorBuoy/SensorBuoyAlbedoAO.png");
            model.SetSpecular("SensorBuoy/SensorBuoyPBRSpecular.png");
            model.SetScale(300);
            model.SetRadius(15);


            model = ModelDatabase.AddModel("SensorBuoyMKIII");
            model.SetMesh("SensorBuoy/SensorBuoyMKIII.model");
            model.SetTexture("SensorBuoy/SensorBuoyAlbedoAO.png");
            model.SetSpecular("SensorBuoy/SensorBuoyPBRSpecular.png");
            model.SetScale(300);
            model.SetRadius(15);

            ModelDatabase.AddModel("artifact1").SetScale(3).SetRadius(50).SetMesh("Artifact1.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact2").SetScale(3).SetRadius(50).SetMesh("Artifact2.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact3").SetScale(3).SetRadius(50).SetMesh("Artifact3.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact4").SetScale(3).SetRadius(50).SetMesh("Artifact4.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact5").SetScale(3).SetRadius(50).SetMesh("Artifact5.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact6").SetScale(3).SetRadius(50).SetMesh("Artifact6.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact7").SetScale(3).SetRadius(50).SetMesh("Artifact7.obj").SetTexture("electric_sphere_texture.png");
            ModelDatabase.AddModel("artifact8").SetScale(3).SetRadius(50).SetMesh("Artifact8.obj").SetTexture("electric_sphere_texture.png");

            for (int type = 1; type <= 5; type++)
            {
                for (int cnt = 1; cnt <= 5; cnt++)
                {
                    model = ModelDatabase.AddModel("transport_" + type + "_" + cnt);
                    model.SetScale(0.5f);
                    model.SetRadius(100 + cnt * 50);
                    model.SetCollisionBox(200 + cnt * 100, 200);
                    model.SetMesh("transport_space_ship_" + type + "/transport_space_ship_" + type + "_cargo_" + cnt + ".model");
                    model.SetTexture("transport_space_ship_" + type + "/transport_space_ship_" + type + "_color.png");
                    model.SetSpecular("transport_space_ship_" + type + "/transport_space_ship_" + type + "_specular.png");
                    model.SetIllumination("transport_space_ship_" + type + "/transport_space_ship_" + type + "_illumination.png");
                }
            }
        }
    }
}
