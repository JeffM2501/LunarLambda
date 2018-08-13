using System;
using System.Collections.Generic;

using LunarLambda.API;

namespace LunarLambda.Campaigns.BasicScenarios
{
    public class Main : LLPlugin
    {
		public override void Load()
		{
			base.Load();

			Scenarios.ScenarioInfo quickBasic = new Scenarios.ScenarioInfo();

			quickBasic.Name = "Quick Basic";
			quickBasic.Description = "A version of a basic scenario. Intended to play out quicker. There is only a single small station to defend.";
			quickBasic.Type = "Sample";
			quickBasic.Variations.Add(new Scenarios.ScenarioInfo.VariationInfo("Advanced", "Advanced", "Give the players a stronger Atlantis instead of the Phobos. Which is more difficult to control, but has more firepower and defense. Increases enemy strength as well."));
			quickBasic.Variations.Add(new Scenarios.ScenarioInfo.VariationInfo("GMStart", "GM Start", "The scenario is not started until the GM gives the start sign. This gives some time for a new crew to get a feeling for the controls before the actual scenario starts."));
			quickBasic.Scenario = new QuickBasic();

			Scenarios.RegisterScenario(quickBasic);
		}
	}
}
