using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API
{
	public interface ILLScenario
	{
		void Init(string variation);

		void Startup(string variation);

		void Shutdown();

		string GetPlayerShipTemplate();

		void Update(double timeDelta);
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
	}

	public static class Scenarios
	{
		public class ScenarioInfo
		{
			public string Name = string.Empty;
			public string Description = string.Empty;
			public string Type = string.Empty;
			public string Author = string.Empty;

			public class VariationInfo
			{
				public string Name = string.Empty;
				public string DisplayName = string.Empty;
				public string Description = string.Empty;

				public VariationInfo( string name, string display, string desc)
				{
					Name = name;
					DisplayName = display;
					Description = desc;
				}
			}

			public List<VariationInfo> Variations = new List<VariationInfo>();
			public string IconImage = string.Empty;

			public ILLScenario Scenario = null;
		}

		public static List<ScenarioInfo> ScenarioList = new List<ScenarioInfo>();

		public static void RegisterScenario(ScenarioInfo info)
		{
			if(GetScenario(info.Name) == null)
				ScenarioList.Add(info);
		}

		public static ScenarioInfo GetScenario(string name)
		{
			return ScenarioList.Find((x) => x.Name == name);
		}
	}
}
