using System.Collections.Generic;

using LunarLambda.Data;
using LunarLambda.Data.Databases;

namespace LunarLambda.API
{
    public interface ILLScenario
	{
		void Init(string variation);

		void Startup(string variation);

		void Shutdown();

		string GetPlayerShipTemplate();

		void Update(double timeDelta);

        List<ShipTemplate> GetPlayableShips();
	}

	public class LLScenario : ILLScenario
	{
		public virtual void Init(string variation)
		{

		}

		public virtual void Startup(string variation)
		{
		}

		public virtual void Shutdown()
		{
		}

		public virtual string GetPlayerShipTemplate()
		{
			return string.Empty;
		}

		public virtual void Update(double timeDelta)
		{

		}

        public virtual List<ShipTemplate> GetPlayableShips()
        {
            return TemplateDatabase.GetAllShipsThatMatch((x) => x.IsPlayable);
        }
    }

	public static class Scenarios
	{
		private static List<ScenarioInfo> ScenarioList = new List<ScenarioInfo>();

		public static void RegisterScenario(ScenarioInfo info)
		{
			if(GetScenario(info.Name) == null)
				ScenarioList.Add(info);
		}

		public static ScenarioInfo GetScenario(string name)
		{
			return ScenarioList.Find((x) => x.Name == name);
		}

        public static ScenarioInfo[] GetScenarioList()
        {
            return ScenarioList.ToArray();
        }
    }
}
