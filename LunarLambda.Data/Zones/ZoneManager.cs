using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarLambda.Data.Entitites;
using OpenTK;

namespace LunarLambda.Data.Zones
{
	public static class ZoneManager
	{
		private static List<Zone> ZoneMaps = new List<Zone>();

		public static Zone[] GetZones() { return ZoneMaps.ToArray(); }

		public static Zone GetZone(string name, Vector3 position)
		{
			var z = FindZone(name);
			if (z != null)
				return z;

			z = new Zone();
			z.Name = name;
			z.Center = position;
			ZoneMaps.Add(z);
			return z;
		}

		public static Zone FindZone(string name)
		{
			return ZoneMaps.Find((x) => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
		}

		public static List<BaseEntity> GetAllThatMatch(Zone.EntityFilter filter)
		{
			List<BaseEntity> list = new List<BaseEntity>();
			foreach (var zone in ZoneMaps)
				list.AddRange(zone.GetAllThatMatch(filter).ToArray());

			return list;
		}

		public static List<BaseEntity> GetAllThatMatch(EntityFinder filter)
		{
			return GetAllThatMatch(filter.Filter);
		}

        public static BaseEntity GetFirstMatch(Zone.EntityFilter filter)
        {
            List<BaseEntity> list = GetAllThatMatch(filter);
            if (list.Count == 0)
                return null;

            return list[0];
        }

        public static BaseEntity GetFirstMatch(EntityFinder filter)
        {
            return GetFirstMatch(filter.Filter);
        }
    }
}
