﻿
using LunarLambda.API;
using LunarLambda.Data;

namespace LunarLambda.Campaigns.BasicScenarios
{
    public class Main : LLPlugin
    {
		public override void Load()
		{
			base.Load();

			ScenarioInfo quickBasic = new ScenarioInfo();

			quickBasic.Name = "Quick Basic";
			quickBasic.Description = "A version of a basic scenario. Intended to play out quicker. There is only a single small station to defend.";
			quickBasic.Type = "Sample";
			quickBasic.Variations.Add(new ScenarioInfo.VariationInfo("Advanced", "Advanced", "Give the players a stronger Atlantis instead of the Phobos. Which is more difficult to control, but has more firepower and defense. Increases enemy strength as well."));
			quickBasic.Variations.Add(new ScenarioInfo.VariationInfo("GMStart", "GM Start", "The scenario is not started until the GM gives the start sign. This gives some time for a new crew to get a feeling for the controls before the actual scenario starts."));
			quickBasic.Scenario = new QuickBasic();

			Scenarios.RegisterScenario(quickBasic);

			ScenarioInfo waves = new ScenarioInfo();

			waves.Name = "Waves";
			waves.Description = "Waves of increasingly difficult enemies.";
			waves.Type = "Basic";
			waves.Variations.Add(new ScenarioInfo.VariationInfo("Hard", "Hard", "Difficulty starts at wave 5 and increases by 1.5 after the players defeat each wave. (Players are more quickly overwhelmed, leading to shorter games.)"));
			waves.Variations.Add(new ScenarioInfo.VariationInfo("Easy", "Easy", "Makes each wave easier by decreasing the number of ships in each wave. (Takes longer for the players to be overwhelmed; good for new players.)"));
			waves.Scenario = new QuickBasic();

			Scenarios.RegisterScenario(waves);
		}
	}
}
