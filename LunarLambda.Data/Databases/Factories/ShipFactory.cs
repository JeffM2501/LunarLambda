using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.Data.Entitites;

namespace LunarLambda.Data.Databases.Factories
{
	public static class ShipFactory
	{
		public static Ship FromTemplate(ShipTemplate template)
		{
			Ship newShip = new Ship(template);

			return newShip;
		}

		public static Ship FromRandomTemplate(ShipCategoryFinder finder)
		{
			return FromTemplate(TemplateDatabase.RandomShip(TemplateDatabase.GetAllShipsThatMatch(finder)));
		}

	}
}
