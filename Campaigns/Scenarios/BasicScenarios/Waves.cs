
using LunarLambda.API;

namespace LunarLambda.Campaigns.BasicScenarios
{
    public class Waves : LLScenario
	{
		protected int StartingWave = 0;
		protected float WaveShipCountFactor = 1;

		public override void Startup(string variation)
		{
			base.Startup(variation);

			if (variation == "Hard")
				StartingWave = 5;
			else if (variation == "Easy")
				WaveShipCountFactor = 0.5f;
		}
	}
}
