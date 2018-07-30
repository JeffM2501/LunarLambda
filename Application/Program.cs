using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using LudicrousElectron.Types;
using LudicrousElectron.Engine;
using LudicrousElectron.Assets;
using LudicrousElectron.Assets.Providers;

using LunarLambda.Preferences;

namespace LunarLambda
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			LoadPreferences(args);
			LoadResources();
			LoadMods();

			SetupCore();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
		}

		static void SetupCore()
		{
			Core.Setup();

			Core.Textures.DefaultSmooth = true;
			Core.Textures.DefaultRepeat = true;
			Core.Textures.AutoSprite = false;

			Core.Textures.GetTexture("Tokka_WalkingMan.png", new Vector2i(6, 1)); //Setup the sprite mapping.
		}

		static void LoadPreferences(string[] args)
		{
			PreferencesManager.Load(FileLocations.GetOptionsFile());
			foreach(var arg in args)
			{
				string[] parts = arg.Split(new string[] { "=" }, 2, StringSplitOptions.RemoveEmptyEntries);
				if (parts.Length == 2 && parts[0] != string.Empty)
					PreferencesManager.Set(parts[0], parts[1]);
			}
		}

		static void LoadResources()
		{
			FileLocations.AddUserAndApplicationSubDirAssets("assets");
			FileLocations.AddUserAndApplicationSubDirAssets("scripts");
			
			foreach (var file in FileLocations.GetAllSubFiles(FileLocations.GetApplicationDataDir("packs"), "*.pack"))
				AssetManager.AddProvider(new PackAssetProvider(file));

			foreach (var file in FileLocations.GetAllSubFiles(FileLocations.GetUserDataSubDir("packs"), "*.pack"))
				AssetManager.AddProvider(new PackAssetProvider(file));
		}

		static void LoadMods()
		{
			if (!PreferencesManager.GetValueB(PrefNames.ModName))
				return;

			FileLocations.AddUserAndApplicationSubDirAssets("mods/" + PreferencesManager.Get(PrefNames.ModName));	
		}
	}

}
