using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;


using System.Drawing;
using LudicrousElectron.Assets;
using LudicrousElectron.Engine.IO;

using LunarLambda.Preferences;

namespace LunarLambda.GUI.Config
{
    public class ColorInfo
    {
        public string Text = string.Empty;
        public Color Color = Color.Empty;

        public void Set(string _text)
        {
            string text = _text;
            if (text.StartsWith("#"))
                text = text.Substring(1);

            Text = text;

            byte[] bytes =ParseUtils.HexStringToBytes(text);

			if (bytes.Length == 3)
				Color = Color.FromArgb(bytes[0], bytes[1], bytes[2]);
			else if (bytes.Length == 4)
				Color = Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);
			else
				Color = Color.White;
		}
    }

    public class ColorSet
    {
        public ColorInfo normal = new ColorInfo();
        public ColorInfo hover = new ColorInfo();
        public ColorInfo focus = new ColorInfo();
        public ColorInfo disabled = new ColorInfo();
        public ColorInfo active = new ColorInfo();
    }

    public class WidgetColorSet
    {
        public ColorSet background = new ColorSet();
        public ColorSet forground = new ColorSet();
    }

    public static class ColorConfig
    {
        public static ColorInfo background = new ColorInfo();
        public static ColorInfo radar_outline = new ColorInfo();

        public static ColorInfo log_generic = new ColorInfo();
        public static ColorInfo log_send = new ColorInfo();
        public static ColorInfo log_receive_friendly = new ColorInfo();
        public static ColorInfo log_receive_enemy = new ColorInfo();
        public static ColorInfo log_receive_neutral = new ColorInfo();

        public static WidgetColorSet button = new WidgetColorSet();
        public static WidgetColorSet label = new WidgetColorSet();
        public static WidgetColorSet text_entry = new WidgetColorSet();
        public static WidgetColorSet slider = new WidgetColorSet();
        public static WidgetColorSet textbox = new WidgetColorSet();

        public static ColorInfo overlay_damaged = new ColorInfo();
        public static ColorInfo overlay_jammed = new ColorInfo();
        public static ColorInfo overlay_hacked = new ColorInfo();
        public static ColorInfo overlay_no_power = new ColorInfo();
        public static ColorInfo overlay_low_energy = new ColorInfo();
        public static ColorInfo overlay_low_power = new ColorInfo();
        public static ColorInfo overlay_overheating = new ColorInfo();
     
        public static ColorInfo ship_waypoint_background = new ColorInfo();
        public static ColorInfo ship_waypoint_text = new ColorInfo();

        private static Dictionary<string, ColorInfo> ColorMap = new Dictionary<string, ColorInfo>();

        static ColorConfig()
        {
            foreach (var fieldInfo in typeof(ColorConfig).GetFields())
            {
                if (fieldInfo.FieldType == typeof(ColorInfo))
                {
                    ColorInfo info = fieldInfo.GetValue(null) as ColorInfo;
                    ColorMap.Add(fieldInfo.Name, info);
                }
                else if (fieldInfo.FieldType == typeof(WidgetColorSet))
                {
                    WidgetColorSet set = fieldInfo.GetValue(null) as WidgetColorSet;
                    ColorMap.Add(fieldInfo.Name + ".forground.normal", set.forground.normal);
                    ColorMap.Add(fieldInfo.Name + ".forground.hover", set.forground.hover);
                    ColorMap.Add(fieldInfo.Name + ".forground.focus", set.forground.focus);
                    ColorMap.Add(fieldInfo.Name + ".forground.disabled", set.forground.disabled);
                    ColorMap.Add(fieldInfo.Name + ".forground.active", set.forground.active);
                    ColorMap.Add(fieldInfo.Name + ".background.normal", set.background.normal);
                    ColorMap.Add(fieldInfo.Name + ".background.hover", set.background.hover);
                    ColorMap.Add(fieldInfo.Name + ".background.focus", set.background.focus);
                    ColorMap.Add(fieldInfo.Name + ".background.disabled", set.background.disabled);
                    ColorMap.Add(fieldInfo.Name + ".background.active", set.background.active);
                }
            }
        }

        public static bool Load()
        {
            ConfigReader reader = new ConfigReader();
            if (reader.Read(AssetManager.GetAssetStream("ui/colors.ini")))
            {
                foreach (var item in reader.Values)
                {
                    if (ColorMap.ContainsKey(item.Key))
                    {
                        ColorMap[item.Key].Set(item.Value);
                    }
                }

                return true;
            }

            return false;
        }
    }
}
