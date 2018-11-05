using System;
using System.Collections.Generic;

namespace LunarLambda.Data.Databases
{
    public static class TemplateDatabase
    {
        internal static List<BaseTemplate> Templates = new List<BaseTemplate>();
        private static int LastTemplateID = 1;
		private static Random RNG = new Random();

        public static BaseTemplate AddTemplate(BaseTemplate template)
        {
            if (template == null || string.IsNullOrEmpty(template.Name))
                return null;

            template.ID = LastTemplateID++;

            Templates.Add(template);
            return template;
        }

        public static ShipTemplate AddStation(string name)
        {
            ShipTemplate station = new ShipTemplate();
            station.Type = BaseTemplate.TemplateTypes.Station;
            station.Name = name;
            return AddTemplate(station) as ShipTemplate;
        }

        public static ShipTemplate AddShip(string name)
        {
            ShipTemplate ship = new ShipTemplate();
            ship.Name = name;
            return AddTemplate(ship) as ShipTemplate;
        }

        public static ShipTemplate CloneShip(ShipTemplate template, string name)
        {
            ShipTemplate ship = template.CloneShip(name);
            return AddTemplate(ship) as ShipTemplate;
        }

        public static BaseTemplate GetTemplate(string name)
        {
            return Templates.Find((x) => x.Name == name);
        }

        public static BaseTemplate GetTemplate(int id)
        {
            return Templates.Find((x) => x.ID == id);
        }

        public static List<ShipTemplate> GetAllShips()
        {
            List<ShipTemplate> ships = new List<ShipTemplate>();
            foreach (var t in Templates)
            {
                if (t as ShipTemplate == null)
                    continue;

                ships.Add(t as ShipTemplate);
            }

            return ships;
        }

        public delegate bool ShipFilter(ShipTemplate template);

        public static List<ShipTemplate> GetAllShipsThatMatch(ShipFilter filter)
        {
            if (filter == null)
                return GetAllShips();

            return GetAllShips().FindAll((x)=>filter(x));
        }

		public static List<ShipTemplate> GetAllShipsThatMatch(ShipFinder filter)
		{
			if (filter == null)
				return GetAllShips();

			return GetAllShips().FindAll((x) => filter.Filter(x));
		}


		public static ShipTemplate RandomShip (List<ShipTemplate> templates)
		{
			if (templates.Count == 0)
				return null;
			if (templates.Count == 1)
				return templates[0];

			return templates[RNG.Next(templates.Count)];
		}
    }

	public abstract class ShipFinder
	{
		public abstract bool Filter(ShipTemplate template);
	}

	public class ShipCategoryFinder : ShipFinder
	{
		public string ShipClass = string.Empty;
		public string SubClass = string.Empty;
		public string Faction = string.Empty;

		public ShipCategoryFinder(string shipclass = null, string subtype = null, string faction = null)
		{
			if (shipclass != null)
			{
				ShipClass = shipclass.ToLowerInvariant();
				if (subtype != null)
					SubClass = subtype.ToLowerInvariant();
			}

			if (faction != null)
				Faction = faction.ToLowerInvariant();
		}

		public override bool Filter(ShipTemplate template)
		{
			if (ShipClass != string.Empty && template.ClassName.ToLowerInvariant() != ShipClass)
				return ShipClass == string.Empty || template.SubClassName.ToLowerInvariant() == SubClass;

			if (Faction != string.Empty && template.FactionName != string.Empty && template.FactionName.ToLowerInvariant() != Faction)
				return false;

			return true;
		}
	}

	public static class ShipTypes
	{
		public static readonly string CorvetteType = "Corvette";
		public static readonly string FigherType = "Fighter";

		public static readonly string DestroyerSubType = "Destroyer";
		public static readonly string SupportSubType = "Support";
		public static readonly string TransportType = "Transport";

	}

	public static class Finders
	{
		public static readonly ShipCategoryFinder Destroyers = new ShipCategoryFinder(ShipTypes.CorvetteType, ShipTypes.DestroyerSubType);
		public static readonly ShipCategoryFinder Fighters = new ShipCategoryFinder(ShipTypes.FigherType);
		public static readonly ShipCategoryFinder Transports = new ShipCategoryFinder(null, ShipTypes.TransportType);
	}
}
