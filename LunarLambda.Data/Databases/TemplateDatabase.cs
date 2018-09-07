using System;
using System.Collections.Generic;

namespace LunarLambda.Data.Databases
{
    public static class TemplateDatabase
    {
        internal static List<BaseTemplate> Templates = new List<BaseTemplate>();
        private static int LastTemplateID = 1;

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

        public static BaseTemplate GetTemplate(string name)
        {
            return Templates.Find((x) => x.Name == name);
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
    }
}
