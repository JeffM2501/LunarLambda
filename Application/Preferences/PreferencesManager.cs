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
			values[key] = value;
		}

		public static string Get(string key)
		{
			if (values.ContainsKey(key))
				return values[key];
			return string.Empty;
		}

		public static bool GetValueB(string key)
		{
			if (values.ContainsKey(key))
				return values[key] != string.Empty;
			return false;
		}


		public static void Load(string filename)
		{
			if (!File.Exists(filename))
				return;

			foreach (var line in File.ReadAllLines(filename))
			{
				if (line.StartsWith("#"))
					continue;

				string[] parts = line.Split(new string[] { " = " }, 2, StringSplitOptions.RemoveEmptyEntries);
				if (parts.Length == 2)
					values[parts[0]] = parts[1];
			}
		}

		public static void Save(string filename)
		{
			if (File.Exists(filename))
				File.Delete(filename);

			List<string> lines = new List<string>();
			lines.Add("# Lunar Lambda Settings");
			lines.Add("# This file will be overwritten by the application.");
			lines.Add(string.Empty);

			foreach (var item in values)
				lines.Add(item.Key + " = " + item.Value);

			File.WriteAllLines(filename, lines);
		}

		private static SortedDictionary<string, string> values = new SortedDictionary<string, string>();

	}
}
