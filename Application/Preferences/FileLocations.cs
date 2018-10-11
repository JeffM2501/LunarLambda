using LudicrousElectron.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LunarLambda.Preferences
{
    public static class FileLocations
	{
		public static string GetUserDir()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lunar_lambda");
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			return path;
		}

		public static DirectoryInfo GetUserDataSubDir(string path)
		{
			return new DirectoryInfo(Path.Combine(GetUserDir(), path));
		}

		public static string GetApplicationDataDir()
		{
			string dir = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "../data")).FullName;
			if (!Directory.Exists(dir))
			{
				dir = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "../../../data")).FullName;
			}
			return dir;
		}

		public static DirectoryInfo GetApplicationDataDir(string path)
		{
			return new DirectoryInfo(Path.Combine(GetApplicationDataDir(), path));
		}

		public static string GetOptionsFile()
		{
			return Path.Combine(GetUserDir(), "options.ini");
		}

		public static List<string> GetAllSubFiles(string path, string filter)
		{
			return GetAllSubFiles(new DirectoryInfo(path), filter);
		}

		public static List<string> GetAllSubFiles(DirectoryInfo dir, string _filter)
		{
            string filter = _filter;
			if (string.IsNullOrEmpty(filter))
				filter = "*.*";

			List<string> files = new List<string>();
			if (dir.Exists)
			{
				foreach (FileInfo file in dir.GetFiles(filter))
					files.Add(file.FullName);

				foreach (var subDir in dir.GetDirectories())
					files.AddRange(GetAllSubFiles(subDir, filter));
			}

			return files;
		}

		public static void AddUserAndApplicationSubDirAssets(string path)
		{
			AssetManager.AddDir(FileLocations.GetApplicationDataDir(path));
			AssetManager.AddDir(FileLocations.GetUserDataSubDir(path));
		}
	}
}
