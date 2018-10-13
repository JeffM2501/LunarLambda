using System.Collections.Generic;

using LunarLambda.Data;
using LunarLambda.Data.Databases;
using LunarLambda.Data.Entitites;

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

		void SpawnPlayableShip(string requester, string requestedType, string requestedName);

		void OnCrewJoinShip(string crewName, string[] crewConsoles, Ship joinedShip);
	}

	public class LLScenario : ILLScenario
	{
		public virtual void Init(string variation)
		{
			// setup any globals, or modify any menus
		}

		public virtual void Startup(string variation)
		{
			// start the simulation
			// load the map
		}

		public virtual void Shutdown()
		{
			// stop the simulation
		}

		public virtual string GetPlayerShipTemplate()
		{
			return string.Empty;
		}

		public virtual void Update(double timeDelta)
		{
			// called every sim fame
		}

		public virtual List<ShipTemplate> GetPlayableShips()
		{
			return TemplateDatabase.GetAllShipsThatMatch((x) => x.IsPlayable);
		}

		public virtual void SpawnPlayableShip(string requester, string requestedType, string requestedName)
		{
			// something wants a new player ship to be spawned
		}

		public virtual void OnCrewJoinShip(string crewName, string[] crewConsoles, Ship joinedShip)
		{
			// called when a crew member joins a ship
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
