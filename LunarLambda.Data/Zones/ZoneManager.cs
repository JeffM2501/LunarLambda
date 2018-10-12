using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace LunarLambda.Data.Zones
{
	public static class ZoneManager
	{
		private static List<Zone> ZoneMaps = new List<Zone>();

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
	}
}
