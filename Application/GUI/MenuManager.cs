using System.Collections.Generic;
using LudicrousElectron.Assets;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Text;
using LunarLambda.API;
using LunarLambda.GUI.Menus;
using LunarLambda.Preferences;

namespace LunarLambda.GUI
{
    public class Menu : Canvas
	{
		public string Name = string.Empty;
		public bool Active { get; protected set; }
		public virtual void Activate() { Active = true; MenuAPI.CallMenuChanged(Name); }
		public virtual void Deactivate() { Active = false; }

		public virtual void Hide() { Active = false; }
		public virtual void Show() { Active = true; }
	}

	public static class MenuManager
	{
		private static MainMenu Main = new MainMenu();

		private static Dictionary<string, Menu> MenuCache = new Dictionary<string, Menu>();

		public static int MainFont = -1;
		public static int BoldFont = -1;

		public static void Setup()
		{
            ThemeManager.SetTheme(PreferencesManager.Get(PrefNames.ThemeName));
            // load fonts

            string mainFont = string.Empty;
            if (AssetManager.AssetExists(ThemeManager.GetThemeAsset("ui/fonts/Regular.otf")))
                mainFont = ThemeManager.GetThemeAsset("ui/fonts/Regular.otf");
            else
                mainFont = ThemeManager.GetThemeAsset("ui/fonts/Regular.ttf");

            MainFont = FontManager.LoadFont(mainFont);

            string boldFont = string.Empty;
            if (AssetManager.AssetExists(ThemeManager.GetThemeAsset("ui/fonts/Bold.otf")))
                boldFont = ThemeManager.GetThemeAsset("ui/fonts/Bold.otf");
            else
                boldFont = ThemeManager.GetThemeAsset("ui/fonts/Bold.ttf");

            BoldFont = FontManager.LoadFont(ThemeManager.GetThemeAsset(boldFont));

            FontManager.DefaultFont = MainFont;

            LoadStandardMenus();
			MenuAPI.CallSetupMenus();

			GUIManager.PushCanvas(Main);
			Main.Activate();
		}

		public static void RegisterMenu(string name, Menu menu)
		{
			menu.Name = name;
			MenuCache.Add(name.ToLowerInvariant(), menu);
		}

		public static void LoadStandardMenus()
		{
			RegisterMenu(MenuAPI.MainMenuName, Main);
			RegisterMenu(MenuAPI.OptionsMenuName, new OptionsMenu());
			RegisterMenu(MenuAPI.StartGameMenuName, new StartServerMenu());
            RegisterMenu(MenuAPI.GameStatusMenu, new GameStatusMenu());
            RegisterMenu(MenuAPI.JoinGameMenuName, new JoinGameMenu());
        }

		public static void PushMenu(string name)
		{
			if (!MenuCache.ContainsKey(name.ToLowerInvariant()))
				return;

			Menu newMenu = MenuCache[name.ToLowerInvariant()];

			GUIManager.PeekCanvas<Menu>()?.Hide();
			GUIManager.PushCanvas(newMenu);
			newMenu.Activate();
		}

        /// <summary>
        ///  replaces the current top with a new menu, then pushes a new menu on top of that, used to insert the game status menu in the history.
        /// </summary>
        /// <param name="replacementMenu">The menu to replace the current top with</param>
        /// <param name="newMenu">the new menu to push to the top of the stack after the replacment menu</param>
        public static void ReplaceAndPushMenu(string replacementMenu, string newMenu)
        {
            GUIManager.PeekCanvas<Menu>()?.Deactivate();
            GUIManager.PopCanvas();

            Menu replaceMenu = MenuCache[replacementMenu.ToLowerInvariant()];
            Menu topMenu = MenuCache[newMenu.ToLowerInvariant()];

            GUIManager.PushCanvas(replaceMenu);
            replaceMenu.Activate();
            replaceMenu.Deactivate();

            GUIManager.PushCanvas(topMenu);
            topMenu.Activate();
        }
		
		public static void PopMenu()
		{
			GUIManager.PeekCanvas<Menu>()?.Deactivate();
			GUIManager.PopCanvas();
			GUIManager.PeekCanvas<Menu>()?.Show();
		}
	}
}
