using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API.Databases
{
    public class BaseTemplate
    {
        public enum TemplateTypes
        {
            Other,
            Ship,
            PlayerShip,
            Station
        }

        public TemplateTypes Type = TemplateTypes.Other;

        public string Name = string.Empty;
		public string DisplayName = string.Empty;
		public string ClassName = string.Empty;
        public string SubClassName = string.Empty;
        public string Description = string.Empty;
        public string ModelName = string.Empty;
        public string RadarTrace = string.Empty;
        public string DefaultAIName = string.Empty;

		public bool Known = true;

        public float Hull = 0;
        public List<float> Shields = new List<float>();

        protected ModelData _Model = null;

        public ModelData Model { get { if (_Model == null) _Model = ModelDatabase.GetModel(ModelName); return _Model; } }

        public virtual BaseTemplate Clone(string newName)
        {
            BaseTemplate obj = Create();
            CopyTo(obj);
			obj.Name = newName;
            return obj;
        }

        protected virtual void CopyTo(BaseTemplate newTemplate)
        {
            newTemplate.Name = Name;
			newTemplate.DisplayName = DisplayName;
			newTemplate.ClassName = ClassName;
            newTemplate.SubClassName = SubClassName;
            newTemplate.Description = Description;
            newTemplate.ModelName = ModelName;
            newTemplate.RadarTrace = RadarTrace;
            newTemplate.DefaultAIName = DefaultAIName;

            newTemplate.Hull = Hull;
            newTemplate.Shields = new List<float>(Shields.ToArray());
        }

        protected virtual BaseTemplate Create()
        {
            return new BaseTemplate();
        }

        public BaseTemplate SetName(string name)
        {
            DisplayName = name;
            return this;
        }

        public BaseTemplate SetClass(string class_name, string sub_class_name)
        {
            ClassName = class_name;
            SubClassName = sub_class_name;
            return this;
        }

        public BaseTemplate SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public BaseTemplate SetModel(string model_name)
        {
            ModelName = model_name;
            return this;
        }

        public BaseTemplate SetHull(float amount)
        {
            Hull = amount;
            return this;
        }

        public BaseTemplate SetShields(IEnumerable<float> values)
        {
            Shields.Clear();
            Shields.AddRange(values.ToArray());
            return this;
        }

        public BaseTemplate SetShields(float values)
        {
            Shields.Clear();
            Shields.Add(values);
            return this;
        }

        public BaseTemplate SetRadarTrace(string trace)
        {
            RadarTrace = trace;
            return this;
        }

        public BaseTemplate SetDefaultAI(string default_ai_name)
        {
            DefaultAIName = default_ai_name;
            return this;
        }
    }
}
