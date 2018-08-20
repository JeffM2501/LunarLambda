using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using LudicrousElectron.Types;
using LudicrousElectron.Engine;
using LudicrousElectron.Assets;
using LudicrousElectron.Assets.Providers;
using LudicrousElectron.Engine.RenderChain;
using LudicrousElectron.Engine.Audio;
using LudicrousElectron.GUI;


using LunarLambda.Preferences;
using LunarLambda.GUI.Config;
using LunarLambda.Common;
using LudicrousElectron.Engine.RenderChain.Effects;
using LudicrousElectron.Engine.Window;
using LunarLambda.API;
using LunarLambda.GUI;
using LudicrousElectron.GUI.Text;
using LudicrousElectron.Engine.Graphics.Textures;

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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			LoadPreferences(args);
            FindPlugins();
			LoadResources();
			LoadMods();

			SetupCore();
            SetupConfigs();
            SetupWindows();
            SetupSounds();

            SetupDatabases();

			MenuManager.Setup();

			Core.Run();

			SavePrefs();
            CleanUpPlugins();
		}

        static void SetupDatabases()
        {
            Events.CallSetupModelData(null);
            Events.CallSetupShipTemplates(null);
            Events.CallSetupFactions(null);
            Events.CallSetupScienceDB(null);

			Events.CallFinalizeDatabases(null);
		}

		static void SavePrefs()
		{
			// save off the last good FSAA value
			PreferencesManager.Set(PrefNames.FSAA, WindowManager.MainWindowAAFactor);
		}

        static void CleanUpPlugins()
        {
            PluginLoader.UnloadAllPlugins();
        }

        static void FindPlugins()
        {
            PluginLoader.AddDir(FileLocations.GetApplicationDataDir("plugins"), true);
            PluginLoader.AddDir(FileLocations.GetUserDataSubDir("plugins"), true);
        }

        static void SetupSounds()
        {
            SoundManager.SetMusicVolume(PreferencesManager.GetValueF(PrefNames.MusicVolume, 50));
            SoundManager.SetMasterSoundVolume(PreferencesManager.GetValueF(PrefNames.SoundVolume, 50));

            // save the actual sounds
            PreferencesManager.Set(PrefNames.MusicVolume, SoundManager.GetMusicVolume());
            PreferencesManager.Set(PrefNames.SoundVolume, SoundManager.GetMasterSoundVolume());
        }

        static void SetupWindows()
        {
			WindowManager.WindowTitleText = Resources.WindowTitle;

			if (PreferencesManager.GetValueB(PrefNames.Headless))
                return;

            WindowManager.WindowInfo info = new WindowManager.WindowInfo();
            info.Size.x = PreferencesManager.GetValueI(PrefNames.MainWindowWidth, 1280);
            info.Size.y = PreferencesManager.GetValueI(PrefNames.MainWindowHeight, 900);

			info.AntiAliasingFactor = PreferencesManager.GetValueI(PrefNames.FSAA, 0);
            info.SizeType = (WindowManager.WindowInfo.WindowSizeTypes)PreferencesManager.GetValueI(PrefNames.Fullscreen, 0);

            if (info.AntiAliasingFactor > 0)
            {
                if (info.AntiAliasingFactor < 2)
                    info.AntiAliasingFactor = 2;
            }
            if (info.SizeType == WindowManager.WindowInfo.WindowSizeTypes.Fullscreen)
            {
                info.Size.x = OpenTK.DisplayDevice.Default.Width;
                info.Size.y = OpenTK.DisplayDevice.Default.Height;
            }

            WindowManager.Init(info);

			WindowManager.ClearRenderLayers();

			Graphics.BackgroundLayer = WindowManager.AddRenderLayer(new RenderLayer());
			Graphics.ObjectLayer = WindowManager.AddRenderLayer(new RenderLayer());
			Graphics.EffectLayer = WindowManager.AddRenderLayer(new RenderLayer());
			Graphics.HudLayer = WindowManager.AddRenderLayer(GUIManager.GetGUILayer(WindowManager.MainWindowID));
			Graphics.MouseLayer = WindowManager.AddRenderLayer(new RenderLayer());

			RenderLayer.DefaultLayer = Graphics.ObjectLayer;

			Graphics.GlitchPostProcessor = WindowManager.AddRenderLayer(new PostProcessor("glitch")) as PostProcessor;
			Graphics.GlitchPostProcessor.Enable(false);
			Graphics.WarpPostProcessor = WindowManager.AddRenderLayer(new PostProcessor("warp")) as PostProcessor;
			Graphics.WarpPostProcessor.Enable(false);

			if (PreferencesManager.GetValueB(PrefNames.DisableShader))
                PostProcessor.EnableEffects(false);

            MenuManager.MainFont = FontManager.LoadFont("ui/fonts/BebasNeue Regular.otf");
            MenuManager.BoldFont = FontManager.LoadFont("ui/fonts/BebasNeue Bold.otf");
            FontManager.DefaultFont = MenuManager.MainFont;
        }

        static void  SetupConfigs()
        {
            ColorConfig.Load();
            HotKeys.Config.Load();
        }

		static void SetupCore()
		{
			Core.Setup();

			TextureManager.AutoSprite = false;

            TextureManager.GetTexture("Tokka_WalkingMan.png", new Vector2i(6, 1), TextureInfo.TextureFormats.Sprite); //Setup the sprite mapping.

            PluginLoader.LoadAllPlugins();
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
            FileLocations.AddUserAndApplicationSubDirAssets("music");

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
