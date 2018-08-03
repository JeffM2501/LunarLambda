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
        public string ClassName = string.Empty;
        public string SubClassName = string.Empty;
        public string Description = string.Empty;
        public string ModelName = string.Empty;
        public string RadarTrace = string.Empty;

        public float Hull = 0;
        public List<float> Shields = new List<float>();

        protected ModelData _Model = null;

        public ModelData Model { get { if (_Model == null) _Model = ModelDatabase.GetModel(ModelName); return _Model; } }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetClass(string class_name, string sub_class_name)
        {
            ClassName = class_name;
            SubClassName = sub_class_name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetModel(string model_name)
        {
            ModelName = model_name;
        }

        public void SetHull(float amount)
        {
            Hull = amount;
        }

        public void SetShields(IEnumerable<float> values)
        {
            Shields.Clear();
            Shields.AddRange(values.ToArray());
        }

        public void SetShields(float values)
        {
            Shields.Clear();
            Shields.Add(values);
        }

        public void SetRadarTrace(string trace)
        {
            RadarTrace = trace;
        }
    }

    public class StationTemplate : BaseTemplate
    {
        public StationTemplate()
        {
            Type = TemplateTypes.Station;
        }
    }

    [Flags]
    public enum EMissileWeapons
    {
        None,
        Homing,
        Nuke,
        Mine,
        EMP,
        HVLI,
        Count
    }

    public class ShipTemplate : BaseTemplate
    {
        public class TubeTemplate
        {
            public float LoadTIme = 0;
            public EMissileWeapons AllowedLoadings = EMissileWeapons.None;
            public float Direction = 0;
        }

        public List<TubeTemplate> WeaponTubes = new List<TubeTemplate>();

        public ShipTemplate()
        {
            Type = TemplateTypes.Ship;
        }
    }

    public static class TemplateDatabase
    {
        internal static List<BaseTemplate> Templates = new List<BaseTemplate>();

        public static BaseTemplate AddTemplate(BaseTemplate template)
        {
            if (template == null || template.Name == string.Empty)
                return null;

            Templates.Add(template);
            return template;
        }

        public static StationTemplate AddStation(string name)
        {
            StationTemplate station = new StationTemplate();
            station.Name = name;
            return AddTemplate(station) as StationTemplate;
        }

        public static ShipTemplate AddShip(string name)
        {
            ShipTemplate ship = new ShipTemplate();
            ship.Name = name;
            return AddTemplate(ship) as ShipTemplate;
        }

        public static BaseTemplate GetTemplate(string name)
        {
            return Templates.Find((x) => x.Name == name);
        }
    }
}
