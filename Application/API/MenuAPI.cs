using System;
using System.Collections.Generic;

using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LunarLambda.GUI.Menus.Controls;

namespace LunarLambda.API
{
	public static class MenuAPI
	{
		public static readonly string MainMenuName = "LL.Main.Menu";

		public static UIButton AddButton(string menuName, string displayName, int column, int row = -1)
		{
			menuName = menuName.ToLowerInvariant();

			if (!RegisteredAPIButtons.ContainsKey(menuName))
				RegisteredAPIButtons.Add(menuName, new List<MenuAPIEventArgs>());

			string dn = displayName.ToLowerInvariant();
			MenuAPIEventArgs info = RegisteredAPIButtons[menuName].Find((x) => x.Button.Name.ToLowerInvariant() == dn);

			if (info != null)
				return info.Button;

			info = new MenuAPIEventArgs(new MenuButton(new RelativeRect()), menuName);

			info.Button.Name = displayName;
			info.Button.SetText(displayName);

			info.Row = row;
			info.Col = column;

			RegisteredAPIButtons[menuName].Add(info);

			ButtonAdded?.Invoke(info.Button, info);

			return info.Button;
		}

		public class MenuAPIEventArgs
		{
			public UIButton Button = null;
			public string MenuName = string.Empty;

			public int Row = -1;
			public int Col = -1;

			internal MenuAPIEventArgs(UIButton button, string menuName)
			{
				Button = button;
				MenuName = menuName;
			}
		}

		internal static event EventHandler<MenuAPIEventArgs> ButtonAdded = null;

		internal static Dictionary<string, List<MenuAPIEventArgs>> RegisteredAPIButtons = new Dictionary<string, List<MenuAPIEventArgs>>();

		internal static List<MenuAPIEventArgs> GetAPIButtons(string menuName)
		{
			menuName = menuName.ToLowerInvariant();

			if (!RegisteredAPIButtons.ContainsKey(menuName))
				return new List<MenuAPIEventArgs>();

			return RegisteredAPIButtons[menuName];
		}

	}
}
