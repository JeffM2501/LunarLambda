﻿
using LunarLambda.API;
using LunarLambda.Data.Databases.Factories;
using LunarLambda.Data.Zones;
using LunarLambda.Data.Databases;
using OpenTK;

namespace LunarLambda.Campaigns.BasicScenarios
{
    public class QuickBasic : LLScenario
	{
		public int EnemyGroupCount = 3;

		public override void Startup(string variation)
		{
			base.Startup(variation);

			if (variation == "Advanced")
				EnemyGroupCount *= 2;

			var zone = ZoneManager.GetZone("Test Zone", Vector3.Zero);

			var ship = zone.AddShip(ShipFactory.FromRandomTemplate(Finders.Destroyers));
            ship.Postion = new Vector3d(100, 100, 0);
		}
	}
}
