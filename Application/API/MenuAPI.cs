using System;
using System.Collections.Generic;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using static LunarLambda.API.Events;
using LunarLambda.GUI.Menus.Controls;

namespace LunarLambda.API
{
	public static class MenuAPI
	{
		public static readonly string MainMenuName = "LL.Main.Menu";
		public static readonly string OptionsMenuName = "LL.Options.Menu";
		public static readonly string StartServerMenuName = "LL.StartServer.Menu";
		public static readonly string StartClientMenuName = "LL.StartClient.Menu";

		public static event EventHandler SetupMenus = null;
		internal static void CallSetupMenus() { SetupMenus?.Invoke(null, EventArgs.Empty); }

		public static event EventHandler<StringDataEventArgs> MenuChanged = null;
		internal static void CallMenuChanged(string newName) { MenuChanged?.Invoke(null, new StringDataEventArgs(newName)); }

		public static UIButton AddButton(string _menuName, string displayName, int column, int row = -1)
		{
			var menuName = _menuName.ToLowerInvariant();

			if (!RegisteredAPIButtons.ContainsKey(menuName))
				RegisteredAPIButtons.Add(menuName, new List<MenuAPIEventArgs>());

			string dn = displayName.ToLowerInvariant();
			MenuAPIEventArgs info = RegisteredAPIButtons[menuName].Find((x) => x.Element.Name.ToLowerInvariant() == dn);

			if (info != null)
				return info.Element as UIButton;

			info = new MenuAPIEventArgs(new MenuButton(new RelativeRect()), menuName);

			info.Element.Name = displayName;
			(info.Element as UIButton)?.SetText(displayName);

			info.Row = row;
			info.Col = column;

			RegisteredAPIButtons[menuName].Add(info);

			ControllAdded?.Invoke(info.Element, info);

			return (info.Element as UIButton);
		}

		public static GUIElement AddGUIItem(string _menuName, GUIElement element, int column, int row = -1)
		{
			var menuName = _menuName.ToLowerInvariant();

			if (!RegisteredAPIButtons.ContainsKey(menuName))
				RegisteredAPIButtons.Add(menuName, new List<MenuAPIEventArgs>());

			string dn = element.Name.ToLowerInvariant();
			MenuAPIEventArgs info = RegisteredAPIButtons[menuName].Find((x) => x.Element.Name.ToLowerInvariant() == dn);

			if (info != null)
				return info.Element;

			info = new MenuAPIEventArgs(new MenuButton(new RelativeRect()), menuName);

			info.Element = element;

			info.Row = row;
			info.Col = column;

			RegisteredAPIButtons[menuName].Add(info);

			ControllAdded?.Invoke(info.Element, info);

			return info.Element;
		}

		public class MenuAPIEventArgs
		{
			public GUIElement Element = null;
			public string MenuName = string.Empty;

			public int Row = -1;
			public int Col = -1;

			internal MenuAPIEventArgs(GUIElement button, string menuName)
			{
				Element = button;
				MenuName = menuName;
			}
		}

		internal static event EventHandler<MenuAPIEventArgs> ControllAdded = null;

		internal static Dictionary<string, List<MenuAPIEventArgs>> RegisteredAPIButtons = new Dictionary<string, List<MenuAPIEventArgs>>();

		internal static List<MenuAPIEventArgs> GetAPICtls(string _menuName)
		{
			var menuName = _menuName.ToLowerInvariant();

			if (!RegisteredAPIButtons.ContainsKey(menuName))
				return new List<MenuAPIEventArgs>();

			return RegisteredAPIButtons[menuName];
		}
    }
}
