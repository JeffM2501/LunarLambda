using System;
using System.Collections.Generic;
using System.IO;

using LudicrousElectron.Assets;
using LudicrousElectron.Engine.Audio;
using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.API;
using LunarLambda.GUI.Menus.Controls;
using LunarLambda.Preferences;

namespace LunarLambda.GUI.Menus
{
	public class OptionsMenu : MenuCommon
	{
		protected VerticalLayoutGroup[] Columns = new VerticalLayoutGroup[] { null, null };

		bool PlayingSong = false;
		internal OptionsMenu() : base()
		{
		}

		public override void Deactivate()
		{
			Hide();
		}

		public override void Hide()
		{
			base.Hide();
			if (PlayingSong)
				SoundManager.StopMusic();

			PlayingSong = false;
		}

		protected override void SetupControls()
		{
			PlayingSong = false;
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
			fsToggle.Clicked += FsToggle_Clicked;
			Columns[0].AddChild(fsToggle);

			SpinSelector fsaaSelector = new SpinSelector(new RelativeRect(), MenuRes.FSAA.Split(";".ToCharArray()), WindowManager.GetWindowInfo(WindowManager.MainWindowID).AntiAliasingFactor);
			fsaaSelector.ValueChanged += FsaaSelector_ValueChanged;
			Columns[0].AddChild(fsaaSelector);

			Columns[0].AddChild(new UIBlank(new RelativeRect()));

			PreferencesManager.GetValueD(PrefNames.SoundVolume, 50);

			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.SoundOptions));
			Columns[0].AddChild(new HSlider(new RelativeRect(), MenuRes.EffectsVolume, PreferencesManager.GetValueD(PrefNames.SoundVolume, 50)));
			Columns[0].AddChild(new HSlider(new RelativeRect(), MenuRes.MusicVolume, PreferencesManager.GetValueD(PrefNames.MusicVolume, 50)));

			Columns[0].AddChild(new UIBlank(new RelativeRect()));
			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.MusicPlayback));
			SpinSelector musicSelector = new SpinSelector(new RelativeRect(), MenuRes.MusicPlaybackModes.Split(";".ToCharArray()), 1);
			Columns[0].AddChild(musicSelector);

			AddElement(Columns[0], 2);
		}

		private void FsToggle_Clicked(object sender, UIButton e)
		{
			WindowManager.ToggleFullscreen();
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
			RelativeRect rect = new RelativeRect(RelativeLoc.XFirstThird + RelativeLoc.BorderOffset, RelativeLoc.YUpperBorder + RelativeLoc.BorderOffset, RelativeSize.ThirdWidth * 1.75f, RelativeSize.BorderInsetHeight, OriginLocation.UpperLeft);

			Columns[1] = new VerticalLayoutGroup(rect);
			Columns[1].ChildSpacing = ButtonSpacing.Paramater;
			Columns[1].MaxChildSize = ButtonHeight.Paramater;
			Columns[1].TopDown = true;
			Columns[1].FitChildToWidth = true;

			Columns[1].AddChild(new Header(new RelativeRect(), MenuRes.PreviewSoundtrack));

			foreach (var file in AssetManager.FindAssets(string.Empty))
			{
				string ext = Path.GetExtension(file).ToUpperInvariant();
				if (ext == ".OGG" || ext == ".MP3")
				{
					MenuButton songButton = new MenuButton(new RelativeRect(), Path.GetFileNameWithoutExtension(file));
					songButton.Tag = file;
					songButton.Clicked += SongButton_Clicked;
					Columns[1].AddChild(songButton);
				}
			}

			AddElement(Columns[1], 2);
		}

		private void SongButton_Clicked(object sender, UIButton e)
		{
			if (PlayingSong)
				SoundManager.StopMusic();

			string song = e.Tag as string;
			if (song == null)
				return;

			SoundManager.PlayMusic(song);

			PlayingSong = true;
		}
	}
}
