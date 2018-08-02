using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.Preferences
{
	public static class PreferencesManager
	{
		public static void Set(string key, string value)
		{
			Values[key] = value;
		}
        public static void Set(string key, bool value)
        {
            Values[key] = value ? "1" : "0";
        }

        public static void Set(string key, int value)
        {
            Values[key] = value.ToString();
        }

        public static void Set(string key, float value)
        {
            Values[key] = value.ToString();
        }

        public static void Set(string key, double value)
        {
            Values[key] = value.ToString();
        }

        public static string Get(string key, string defaultValue = null)
		{
			if (Values.ContainsKey(key))
				return Values[key];

            return defaultValue == null ? string.Empty : defaultValue;
        }

		public static bool GetValueB(string key)
		{
			if (Values.ContainsKey(key))
				return Values[key] == "1";
			return false;
		}

        public static int GetValueI(string key, int defaultValue)
        {
            if (Values.ContainsKey(key))
            {
                int val = 0;
                if (int.TryParse(Values[key], out val))
                    return val;
            }
            return defaultValue;
        }

        public static float GetValueF(string key, float defaultValue)
        {
            if (Values.ContainsKey(key))
            {
                float val = 0;
                if (float.TryParse(Values[key], out val))
                    return val;
            }
            return defaultValue;
        }

        public static double GetValueD(string key, double defaultValue)
        {
            if (Values.ContainsKey(key))
            {
                double val = 0;
                if (double.TryParse(Values[key], out val))
                    return val;
            }
            return defaultValue;
        }



        public static void Load(string filename)
		{
            ConfigReader reader = new ConfigReader();
            if (reader.Read(filename))
                Values = reader.Values;
		}

		public static void Save(string filename)
		{
			if (File.Exists(filename))
				File.Delete(filename);

			List<string> lines = new List<string>();
			lines.Add("# Lunar Lambda Settings");
			lines.Add("# This file will be overwritten by the application.");
			lines.Add(string.Empty);

			foreach (var item in Values)
				lines.Add(item.Key + " = " + item.Value);

			File.WriteAllLines(filename, lines);
		}

		private static SortedDictionary<string, string> Values = new SortedDictionary<string, string>();

	}
}
