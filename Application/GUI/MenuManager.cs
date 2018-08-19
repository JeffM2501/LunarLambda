using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudicrousElectron.Engine.Graphics.Textures;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LunarLambda.API;
using LunarLambda.GUI.Config;
using LunarLambda.GUI.Menus;
using LunarLambda.GUI.Menus.Controls;

namespace LunarLambda.GUI
{
	public class Menu : Canvas
	{
		public string Name = string.Empty;
		public bool Active { get; protected set; }
		public virtual void Activate() { Active = true; MenuAPI.CallMenuChanged(Name); }
		public virtual void Deactivate() { Active = false; }

		public virtual LayoutContainer GetContainerForAPIButton(int row, int col)
		{
			return null;
		}

		public virtual UIButton GetAPIButton(RelativeRect rect, string label)
		{
			return new MenuButton(rect, label);
		}

		protected virtual void RegisterButton(MenuAPI.MenuAPIEventArgs buttonInfo)
		{
			LayoutContainer container = GetContainerForAPIButton(buttonInfo.Row, buttonInfo.Col);
			if (container == null)
				return;

			int col = buttonInfo.Col;
			if (col < 0)
				col = 0;
			if (col > 1)
				col = 1;

			RelativeRect rect = new RelativeRect();
			rect.Width = container.Rect.Width.Clone();
			buttonInfo.Button = GetAPIButton(rect, buttonInfo.MenuName);
			container.AddChild(buttonInfo.Button);
		}

		public virtual void AddAPIButtons(string name)
		{
			foreach (var buttonInfo in MenuAPI.GetAPIButtons(name))
				RegisterButton(buttonInfo);
		}

		protected virtual void SetupBackground(int layerIndex)
		{
			string bgRepeat = "ui/BackgroundCrosses.png";

			TextureManager.GetTexture(bgRepeat).SetTextureFormat(TextureInfo.TextureFormats.TextureMap); // force this to repeat

			var background = new UIPanel(RelativeRect.Full, ColorConfig.background.Color);
			background.Children.Add(new UIPanel(RelativeRect.Full, Color.White, bgRepeat));
			AddElement(background, layerIndex);
		}
	}

	public static class MenuManager
	{
		private static MainMenu Main = new MainMenu();

		private static Dictionary<string, Menu> MenuCache = new Dictionary<string, Menu>();

		public static int MainFont = -1;
		public static int BoldFont = -1;

		public static void Setup()
		{
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
		}

		public static void PushMenu(string name)
		{
			if (!MenuCache.ContainsKey(name.ToLowerInvariant()))
				return;

			Menu newMenu = MenuCache[name.ToLowerInvariant()];

			GUIManager.PeekCanvas<Menu>()?.Deactivate();
			GUIManager.PushCanvas(newMenu);
			newMenu.Activate();
		}
		
		public static void PopMenu()
		{
			GUIManager.PeekCanvas<Menu>()?.Deactivate();
			GUIManager.PopCanvas();
			GUIManager.PeekCanvas<Menu>()?.Activate();
		}
	}
}
