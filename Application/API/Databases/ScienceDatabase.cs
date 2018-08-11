using System;
using System.Collections.Generic;

using LudicrousElectron.Types;

namespace LunarLambda.API.Databases
{
	public static class Tools
	{
		public static string Display(bool val)
		{
			return val ? DefaultDatabaseStrings.True : DefaultDatabaseStrings.False;
		}
	}
	public class ScienceInfo
	{
		public string Name = string.Empty;
		public string Description = string.Empty;
		public bool IsCategory = true;

		public List<string> Images = new List<string>();
		public string ModelName = string.Empty;

		protected ModelData _Model = null;

		public virtual ModelData Model { get { if (_Model == null) _Model = ModelDatabase.GetModel(ModelName); return _Model; } }

		public virtual List<Tuple<string, string>> DataValues { get; } = new List<Tuple<string, string>>();

		public ScienceInfo ParrentItem = null;
		public virtual List<ScienceInfo> Items { get; } = new List<ScienceInfo>();

		public virtual ScienceInfo FindItemByName(string name)
		{
			return Items.Find((x) => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
		}

		public virtual ScienceInfo FindItemByName(string name, bool recursive)
		{
			var item = Items.Find((x) => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
			if (item == null && recursive)
			{
				foreach(var subItem in Items)
				{
					item = subItem.FindItemByName(name, true);
					if (item != null)
						return item;
				}
			}
			return item;
		}

		public virtual ScienceInfo GetItem(string name)
		{
			var item = FindItemByName(name);
			if (item == null)
			{
				item = new ScienceInfo();
				item.Name = name;
				item.ParrentItem = this;
				Items.Add(item);
			}
			return item;
		}

		public virtual void AddDataItem(string key, string value)
		{
			if (IsCategory)
				return;

			if (key != null)
				DataValues.Add(new Tuple<string, string>(key, value));
		}
	}

	public class DynamicFactionScienceGetter : ScienceInfo
	{
		public class FactionScienceInfo : ScienceInfo
		{
			protected FactionInfo Faction = null;

			public override List<Tuple<string, string>> DataValues
			{
				get
				{
					var data = new List<Tuple<string, string>>();

					foreach(var relation in Faction.Relationships)
					{
						var other = FactionDatabase.GetFaction(relation.Key);
						if (other != null)
							data.Add(new Tuple<string, string>(other.Name, FactionDatabase.GetLocalRelationString(relation.Value)));
					}

					return data;
				}
			}

			public FactionScienceInfo( FactionInfo faction)
			{
				Faction = faction;
				Name = Faction.Name;
				Description = faction.Description;
				
				if (faction.Logo != string.Empty)
					Images.Add(faction.Logo);
			}

			public override void AddDataItem(string key, string value)
			{
			}
		}

		public override List<ScienceInfo> Items
		{
			get
			{
				List<ScienceInfo> factions = new List<ScienceInfo>();
				foreach(var faction in FactionDatabase.Factions)
					factions.Add(new FactionScienceInfo(faction));
				return factions;
			}
		}

		public override ScienceInfo FindItemByName(string name, bool recursive)
		{
			foreach (var faciton in FactionDatabase.Factions)
			{
				if (faciton.Name.ToLowerInvariant() == name.ToLowerInvariant())
					return new FactionScienceInfo(faciton);
			}
			return null;
		}

		public override ScienceInfo FindItemByName(string name)
		{
			return FindItemByName(name, false);
		}

		public override ScienceInfo GetItem(string name)
		{
			return FindItemByName(name,false);
		}

		public DynamicFactionScienceGetter()
		{
			Name = DefaultDatabaseStrings.FactionDatabaseItemName;
			IsCategory = true;
		}
	}

	public class DynamicShipScienceGetter : ScienceInfo
	{
		public class ShipScienceInfo : ScienceInfo
		{
			protected ShipTemplate Ship = null;

			public override ModelData Model { get { return Ship.Model; } }

			public override List<Tuple<string, string>> DataValues
			{
				get
				{
					var data = new List<Tuple<string, string>>();

					if (Ship == null)
						return data;

					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipClassName, Ship.ClassName));
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipSubClassName, Ship.SubClassName));
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipSizeName, ((int)Ship.Model.Radius).ToString()));
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipShieldName, string.Join("/",Ship.Shields)));
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipHullName, ((int)Ship.Hull).ToString()));
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipMoveSpeedName, ((int)Ship.ImpulseSpeed).ToString()));
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipTurnSpeedName, ((int)Ship.TurnSpeed).ToString()));

					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipWarpDriveName, Tools.Display(Ship.DriveType.HasFlag(FTLDriveTypes.Warp))));
					if (Ship.DriveType.HasFlag(FTLDriveTypes.Warp))
						data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipWarpSpeedName, ((int)Ship.WarpSpeed).ToString()));

					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipJumpDriveName, Tools.Display(Ship.DriveType.HasFlag(FTLDriveTypes.Jump))));

					int tubeCount = 0;
					double loadTime = -1;
					foreach (var weapon in Ship.Weapons.Values)
					{
						if (weapon.MountedWeapon == null)
							continue;

						if (weapon.MountedWeapon.WeaponType != ShipTemplate.WeaponTemplate.WeaponTypes.Missile)
						{
							tubeCount++;

							if (loadTime < 0)
								loadTime = (weapon.MountedWeapon as ShipTemplate.TubeTemplate).LoadTime;

							continue;
						}
							

						if (weapon.MountedWeapon.WeaponType != ShipTemplate.WeaponTemplate.WeaponTypes.Beam)
							continue;

						ShipTemplate.BeamWeaponBankTemplate beam = weapon.MountedWeapon as ShipTemplate.BeamWeaponBankTemplate;
						string dir = DefaultDatabaseStrings.Unknown;
						if (Angles.Near(0, weapon.DefaultFacing, 45))
							dir = DefaultDatabaseStrings.ShipBeamFrontName;
						else if (Angles.Near(90, weapon.DefaultFacing, 45))
							dir = DefaultDatabaseStrings.ShipBeamRightName;
						else if (Angles.Near(-90, weapon.DefaultFacing, 45))
							dir = DefaultDatabaseStrings.ShipBeamLeftName;
						else if (Angles.Near(180, weapon.DefaultFacing, 45))
							dir = DefaultDatabaseStrings.ShipBeamRearName;

						float damage = beam.NominalDamage / beam.NominalCycleTime;

						data.Add(new Tuple<string, string>(dir + " " + DefaultDatabaseStrings.ShipBeamWeaponName, damage.ToString() + " " + DefaultDatabaseStrings.DamagePerSecond));
					}

					if (tubeCount > 0)
					{
						data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipMissleTubesName, tubeCount.ToString()));
						data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipMissileTubesLoadName, ((int)loadTime).ToString()));

						int stores = 0;
						foreach (var mag in Ship.Magazines)
							stores += mag.Capacity;

						if (stores > 0)
							data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipMissileStoresName, stores.ToString()));
					}
#if DEBUG
					data.Add(new Tuple<string, string>(DefaultDatabaseStrings.ShipModelDebugName, Ship.ModelName));
#endif

					return data;
				}
			}

			public override void AddDataItem(string key, string value)
			{
			}

			public ShipScienceInfo(ShipTemplate ship)
			{
				Ship = ship;
				Name = Ship.Name;
				Description = Ship.Description;

				if (Ship.RadarTrace != string.Empty)
					Images.Add(Ship.RadarTrace);

				ModelName = ship.ModelName;
			}
		}

		public override List<ScienceInfo> Items
		{
			get
			{
				List<ScienceInfo> ships = new List<ScienceInfo>();
				foreach (var ship in TemplateDatabase.Templates)
				{
					if (ship as ShipTemplate != null)
						ships.Add(new ShipScienceInfo(ship as ShipTemplate));
				}

				ships.Sort((x, y) => x.Name.CompareTo(y.Name));
				return ships;
			}
		}

		public override ScienceInfo FindItemByName(string name, bool recursive)
		{
			foreach (var ship in TemplateDatabase.Templates)
			{
				if (ship.Name.ToLowerInvariant() == name.ToLowerInvariant() && ship as ShipTemplate != null)
					return new ShipScienceInfo(ship as ShipTemplate);
			}
			return null;
		}

		public override ScienceInfo FindItemByName(string name)
		{
			return FindItemByName(name, false);
		}

		public override ScienceInfo GetItem(string name)
		{
			return FindItemByName(name, false);
		}

		public DynamicShipScienceGetter()
		{
			Name = DefaultDatabaseStrings.ShipDatabaseItemName;
			IsCategory = true;
		}
	}

	public static class ScienceDatabase
	{
		public static ScienceInfo Root = new ScienceInfo();

		public static ScienceInfo GetItem(string name)
		{
			return Root.GetItem(name);
		}

		static ScienceDatabase()
		{
			Root.IsCategory = true;

			DynamicFactionScienceGetter factionInfo = new DynamicFactionScienceGetter();
			factionInfo.ParrentItem = Root;
			Root.Items.Add(factionInfo);

			DynamicShipScienceGetter shipInfo = new DynamicShipScienceGetter();
			shipInfo.ParrentItem = Root;
			Root.Items.Add(shipInfo);
		}
	}
}
