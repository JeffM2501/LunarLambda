using System.Collections.Generic;
using LunarLambda.Data;
using LunarLambda.Data.Databases;
using LunarLambda.Data.Entitites;
using LunarLambda.Data.Zones;
using OpenTK;

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

		int SpawnPlayableShip(string requester, string requestedType, string requestedName);

		void OnCrewJoinShip(string crewName, string[] crewConsoles, Ship joinedShip);

        string GetDefaultZoneName();
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

		public virtual int SpawnPlayableShip(string requester, string requestedType, string requestedName)
		{
            string typeToUse = requestedType;
            // something wants a new player ship to be spawned
            if (typeToUse == string.Empty)
                typeToUse = GetPlayerShipTemplate();

            ShipTemplate template = TemplateDatabase.GetTemplate(typeToUse) as ShipTemplate;
            if (!GetPlayableShips().Contains(template) && GetPlayableShips().Count > 0)
                template = GetPlayableShips()[0];

            Zone spawnZone = ZoneManager.FindZone(GetDefaultZoneName());
            if (spawnZone == null)
            {
                var zones = ZoneManager.GetZones();
                if (zones.Length == 0)
                    return -1; // no zones to spawn a ship in

                spawnZone = zones[0];
            }

            var ship = new Ship(template);
           ship.Name = FilterShipName(requestedName, requestedType);

            spawnZone.Add(ship);

            return ship.GUID;
		}

        protected virtual string FilterShipName(string requestedName, string templateName)
        {
            return requestedName == string.Empty ? templateName : requestedName;
        }

		public virtual void OnCrewJoinShip(string crewName, string[] crewConsoles, Ship joinedShip)
		{
			// called when a crew member joins a ship
		}

        public virtual string GetDefaultZoneName()
        {
            var zones = ZoneManager.GetZones();
            if (zones.Length == 0)
                return string.Empty;

            return ZoneManager.GetZones()[0].Name;
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
