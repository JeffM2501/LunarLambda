using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace LunarLambda.API.Databases
{
   

    public static class TemplateDatabase
    {
        internal static List<BaseTemplate> Templates = new List<BaseTemplate>();

        public static BaseTemplate AddTemplate(BaseTemplate template)
        {
            if (template == null || template.Name == string.Empty)
                return null;

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
    }
}
