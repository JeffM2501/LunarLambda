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
			// lsefside setup
			RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpperBorder + RelativeLoc.BorderOffset, ButtonWidth, RelativeSize.ThreeQuarterHeight, OriginLocation.UpperLeft);

			Columns[0] = SetupCommonColumn(rect);

			// graphics header
			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.Graphics));

			// fullscreen toggle
			SpinSelector windowSizeSelector = new SpinSelector(new RelativeRect(), MenuRes.FullscreenModes.Split(";".ToCharArray()), 0);
			windowSizeSelector.ValueChanged += WindowSizeSelector_ValueChanged;
			Columns[0].AddChild(windowSizeSelector);

			// anti-alias selector
			int aa = WindowManager.GetWindowInfo(WindowManager.MainWindowID).AntiAliasingFactor;
			if (aa < 0)
				aa = 0;
			if (aa > 8)
				aa = 8;

			if (aa != 0)
				aa = (int)Math.Log(aa, 2);

			SpinSelector fsaaSelector = new SpinSelector(new RelativeRect(), MenuRes.FSAA.Split(";".ToCharArray()), aa);
			fsaaSelector.ValueChanged += FsaaSelector_ValueChanged;
			Columns[0].AddChild(fsaaSelector);


			// sound options header
			Columns[0].AddChild(new UIBlank(new RelativeRect()));
			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.SoundOptions));

			// sound volume
			double volume = PreferencesManager.GetValueI(PrefNames.SoundVolume, 50);
			var soundSlider = new HSlider(new RelativeRect(), MenuRes.EffectsVolume, volume);
			soundSlider.ValueChanged += SoundSlider_ValueChanged;
			Columns[0].AddChild(soundSlider);

			// music volume
			var musicSlider = new HSlider(new RelativeRect(), MenuRes.MusicVolume, PreferencesManager.GetValueI(PrefNames.MusicVolume, 50));
			musicSlider.ValueChanged += MusicSlider_ValueChanged;
			Columns[0].AddChild(musicSlider);

			// Music playback header
			Columns[0].AddChild(new UIBlank(new RelativeRect()));
			Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.MusicPlayback));

			// music mode selector
			SpinSelector musicSelector = new SpinSelector(new RelativeRect(), MenuRes.MusicPlaybackModes.Split(";".ToCharArray()), PreferencesManager.GetValueI(PrefNames.MusicEnabled, 2));
			musicSelector.ValueChanged += MusicSelector_ValueChanged;
			Columns[0].AddChild(musicSelector);

			AddElement(Columns[0], 2);
		}

		private void MusicSelector_ValueChanged(object sender, EventArgs e)
		{
			SpinSelector selector = sender as SpinSelector;
			if (selector == null)
				return;

			PreferencesManager.Set(PrefNames.MusicEnabled, selector.SelectedIndex);
		}

		private void MusicSlider_ValueChanged(object sender, EventArgs e)
		{
			HSlider slider = sender as HSlider;
			if (slider == null)
				return;

			SoundManager.SetMusicVolume((float)slider.CurrentValue);
			PreferencesManager.Set(PrefNames.MusicVolume, (int)slider.CurrentValue);
		}

		private void SoundSlider_ValueChanged(object sender, EventArgs e)
		{
			HSlider slider = sender as HSlider;
			if (slider == null)
				return;

			SoundManager.SetMasterSoundVolume((float)slider.CurrentValue);
			PreferencesManager.Set(PrefNames.SoundVolume, (int)slider.CurrentValue);
		}
		private void WindowSizeSelector_ValueChanged(object sender, EventArgs e)
		{
			SpinSelector selector = sender as SpinSelector;
			if (selector == null)
				return;

			switch(selector.SelectedIndex)
			{
				case 0:
				default:
					WindowManager.SetNormal();
					break;

				case 1:
					WindowManager.SetMaximized();
					break;

				case 2:
					WindowManager.SetFullscreen();
					break;
			}
		}

		private void FsaaSelector_ValueChanged(object sender, EventArgs e)
		{
			SpinSelector selector = sender as SpinSelector;
			if (selector == null)
				return;

			int newFSAA = (int)(Math.Pow(2, selector.SelectedIndex));

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
