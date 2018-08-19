using LudicrousElectron.Engine.Window;
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
	public class OptionsMenu : MenuCommon
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

		protected override void SetupControls()
		{
			SetupBackground(0);
			SetupBackButton(1);

			SetupOptions();
			SetupMusicSamples();

			AddAPIButtons(MenuAPI.OptionsMenuName);
			base.SetupControls();
		}

		protected void SetupOptions()
		{
			RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpperBorder + RelativeLoc.BorderOffset, ButtonWidth, RelativeSize.ThreeQuarterHeight, OriginLocation.UpperLeft);

			Columns[0] = new VerticalLayoutGroup(rect);
			Columns[0].ChildSpacing = ButtonSpacing.Paramater;
			Columns[0].MaxChildSize = ButtonHeight.Paramater;
			Columns[0].TopDown = true;
			Columns[0].FitChildToWidth = true;

			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.Graphics));

			MenuButton fsToggle = new MenuButton(new RelativeRect(), MenuRes.FullscreenToggle);
			Columns[0].AddChild(fsToggle);

			SpinSelector fsaaSelector = new SpinSelector(new RelativeRect(), MenuRes.FSAA.Split(";".ToCharArray()), WindowManager.GetWindowInfo(WindowManager.MainWindowID).AntiAliasingFactor);
			fsaaSelector.ValueChanged += FsaaSelector_ValueChanged;
			Columns[0].AddChild(fsaaSelector);

			AddElement(Columns[0], 2);
		}

		private void FsaaSelector_ValueChanged(object sender, EventArgs e)
		{
			SpinSelector selector = sender as SpinSelector;
			if (selector == null)
				return;

			int newFSAA = selector.SelectedIndex * 2;

			WindowManager.SetFSAALevel(newFSAA);
		}

		protected void SetupMusicSamples()
		{

		}
	}
}
