using System;

using LunarLambda.Data.Databases;

namespace LunarLambda.Campaigns.Standard.Databases
{
    internal static class StandardScienceDB
	{
		internal static void Load(object sender, EventArgs e)
		{
			var space_objects = ScienceDatabase.GetItem(ScienceDataStrings.NaturalCategory);

			var item = space_objects.GetItem(ScienceDataStrings.Asteroid);
			item.Description = ScienceDataStrings.AsteroidDesc;

			item = space_objects.GetItem(ScienceDataStrings.Nebula);
			item.Description = ScienceDataStrings.NebulaDesc;

			item = space_objects.GetItem(ScienceDataStrings.BlackHole);
			item.Description = ScienceDataStrings.BlackHoleDesc;

			item = space_objects.GetItem(ScienceDataStrings.Wormhole);
			item.Description = ScienceDataStrings.WormholeDesc;

			var weapons = ScienceDatabase.GetItem(ScienceDataStrings.WeaponsCategory);

			item = weapons.GetItem(ScienceDataStrings.HomingMissile);
			item.Description = ScienceDataStrings.HomingMissileDesc;
			item.AddDataItem(ScienceDataStrings.Range, "5.4u");
			item.AddDataItem(ScienceDataStrings.Damage, "35");

			item = weapons.GetItem(ScienceDataStrings.Nuke);
			item.Description = ScienceDataStrings.NukeDesc;
			item.AddDataItem(ScienceDataStrings.Range, "5.4u");
			item.AddDataItem(ScienceDataStrings.BlastRadius, "1u");
			item.AddDataItem(ScienceDataStrings.CenterDamage, "160");
			item.AddDataItem(ScienceDataStrings.EdgeDamage, "30");

			item = weapons.GetItem(ScienceDataStrings.Mine);
			item.Description = ScienceDataStrings.MineDesc;
			item.AddDataItem(ScienceDataStrings.DropDistance, "1.2u");
			item.AddDataItem(ScienceDataStrings.TriggerDistance, "0.6u");
			item.AddDataItem(ScienceDataStrings.BlastRadius, "1u");
			item.AddDataItem(ScienceDataStrings.CenterDamage, "160");
			item.AddDataItem(ScienceDataStrings.EdgeDamage, "30");

			item = weapons.GetItem(ScienceDataStrings.EMP);
			item.Description = ScienceDataStrings.EMPDesc;
			item.AddDataItem(ScienceDataStrings.Range, "5.4u");
			item.AddDataItem(ScienceDataStrings.BlastRadius, "1u");
			item.AddDataItem(ScienceDataStrings.CenterDamage, "160");
			item.AddDataItem(ScienceDataStrings.EdgeDamage, "30");

			item = weapons.GetItem(ScienceDataStrings.HVLI);
			item.Description = ScienceDataStrings.HVLIDesc;
			item.AddDataItem(ScienceDataStrings.Range, "5.4u");
			item.AddDataItem(ScienceDataStrings.Damage, "7 " + ScienceDataStrings.Each + ", " + 35 + " " + ScienceDataStrings.Total);
			item.AddDataItem(ScienceDataStrings.Burst, "5");
		}
	}
}
