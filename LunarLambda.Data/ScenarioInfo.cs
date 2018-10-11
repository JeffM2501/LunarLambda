using System.Collections.Generic;

namespace LunarLambda.Data
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

            public VariationInfo(string name, string display, string desc)
            {
                Name = name;
                DisplayName = display;
                Description = desc;
            }
        }

        public List<VariationInfo> Variations = new List<VariationInfo>();
        public string IconImage = string.Empty;

        public object Scenario = null;
    }
}
