using System;
using System.Collections.Generic;

using LunarLambda.API;

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
		}
	}
}
