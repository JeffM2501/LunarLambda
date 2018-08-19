using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LunarLambda.API;
using LunarLambda.GUI.Menus.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.GUI.Menus
{
	public class OptionsMenu : Menu
	{
		protected VerticalLayoutGroup[] Columns = new VerticalLayoutGroup[] { null, null };

		internal OptionsMenu()
		{
			MenuAPI.ButtonAdded += MenuAPI_ButtonAdded;
		}

		private void MenuAPI_ButtonAdded(object sender, MenuAPI.MenuAPIEventArgs e)
		{
			if (e.MenuName != MenuAPI.OptionsMenuName || !Active)
				return;

			RegisterButton(e);
		}

		public override void Activate()
		{
			base.Activate();
			SetupBackground(0);

			SetupOptions();
			SetupMusicSamples();


			AddAPIButtons(MenuAPI.MainMenuName);
		}

		protected void SetupOptions()
		{
			RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpperBorder + RelativeLoc.BorderOffset, MainMenu.ButtonWidth, RelativeSize.ThreeQuarterHeight, OriginLocation.UpperLeft);

			Columns[0] = new VerticalLayoutGroup(rect);
			Columns[0].ChildSpacing = 5;
			Columns[0].MaxChildSize = 45;
			Columns[0].TopDown = true;
			Columns[0].FitChildToWidth = true;

			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.Graphics));

			MenuButton fsToggle = new MenuButton(new RelativeRect(), MenuRes.FullscreenToggle);
			Columns[0].AddChild(fsToggle);

			AddElement(Columns[0], 1);
		}

		protected void SetupMusicSamples()
		{

		}
	}
}
