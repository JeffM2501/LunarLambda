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

		public static string Get(string key)
		{
			if (Values.ContainsKey(key))
				return Values[key];
			return string.Empty;
		}

		public static bool GetValueB(string key)
		{
			if (Values.ContainsKey(key))
				return Values[key] != string.Empty;
			return false;
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
