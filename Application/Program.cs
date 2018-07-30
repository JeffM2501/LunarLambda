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

using LunarLambda.Preferences;
using LunarLambda.GUI.Config;
using LunarLambda.Common;
using LudicrousElectron.Engine.RenderChain.Effects;
using LudicrousElectron.Engine.Window;

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
            SetupConfigs();
            SetupWindows();
            SetupSounds();

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
		}

        static void SetupSounds()
        {

        }

        static void SetupWindows()
        {
            if (PreferencesManager.GetValueB(PrefNames.Headless))
                return;

            Graphics.BackgroundLayer = new RenderLayer();
            Graphics.ObjectLayer = new RenderLayer(Graphics.BackgroundLayer);
            Graphics.EffectLayer = new RenderLayer(Graphics.ObjectLayer);
            Graphics.HudLayer = new RenderLayer(Graphics.EffectLayer);
            Graphics.MouseLayer = new RenderLayer(Graphics.HudLayer);
            Graphics.GlitchPostProcessor = new PostProcessor("glitch", Graphics.MouseLayer);
            Graphics.GlitchPostProcessor.Enable(false);
            Graphics.WarpPostProcessor = new PostProcessor("warp", Graphics.GlitchPostProcessor);
            Graphics.WarpPostProcessor.Enable(false);
            RenderLayer.DefaultLayer = Graphics.ObjectLayer;


            WindowManager.WindowInfo info = new WindowManager.WindowInfo();
            info.Size.x = 1200;
            info.Size.y = 900;
            info.AntiAliasingFactor = PreferencesManager.GetValueI(PrefNames.FSAA, 0);
            info.Fullscreen = PreferencesManager.GetValueB(PrefNames.Fullscreen);

            if (info.AntiAliasingFactor > 0)
            {
                if (info.AntiAliasingFactor < 2)
                    info.AntiAliasingFactor = 2;
            }
            if (info.Fullscreen)
            {
                info.Size.x = OpenTK.DisplayDevice.Default.Width;
                info.Size.y = OpenTK.DisplayDevice.Default.Height;
            }

            WindowManager.Init(info, Graphics.WarpPostProcessor);


            if (PreferencesManager.GetValueB(PrefNames.DisableShader))
                PostProcessor.EnableEffects(false);
        }

        static void  SetupConfigs()
        {
            ColorConfig.Load();
            HotKeys.Config.Load();
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
