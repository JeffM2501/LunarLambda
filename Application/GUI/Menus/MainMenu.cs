using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

using OpenTK;

using LudicrousElectron.GUI;
using LudicrousElectron.Engine;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.GUI.Config;
using LunarLambda.GUI.Menus.Controls;
using LudicrousElectron.Engine.Graphics.Textures;
using LunarLambda.API;

namespace LunarLambda.GUI.Menus
{
	public class MainMenu : Menu
	{
		public static RelativeSize ButtonWidth = new RelativeSize(1.0f / 4.0f, true);
		public static RelativeSize ButtonHeight = new RelativeSize(1.0f / 18.0f, false);

		protected int[] ColCounts = new int[] {0,0};

		protected float ButtonShift = 1.25f;

		protected int ButtonLayerIndex = 0;

		internal MainMenu()
		{
			MenuAPI.ButtonAdded += MenuAPI_ButtonAdded;
		}

		private void MenuAPI_ButtonAdded(object sender, MenuAPI.MenuAPIEventArgs e)
		{
			if (e.MenuName != MenuAPI.MainMenuName || !Active)
				return;

			RegisterButton(e);
		}


		public override void Activate()
		{
			base.Activate();

			int layerIndex = 0;
			SetupBackground(layerIndex++);
			SetupLogo(layerIndex++);
			SetupCredits(layerIndex++);
            SetupButons(layerIndex++);

			SetupAPIButtons();
        }

        private void Tutorials_Clicked(object sender, UIButton e)
        {
        }

        private void StartServer_Clicked(object sender, UIButton e)
        {
        }

        private void StartClient_Clicked(object sender, UIButton e)
        {
        }

        private void Options_Clicked(object sender, UIButton e)
        {
        }


        private void Quit_Clicked(object sender, UIButton e)
        {
			Core.Exit();
        }

		protected void RegisterButton(MenuAPI.MenuAPIEventArgs buttonInfo)
		{
			int col = buttonInfo.Col;
			if (col < 0)
				col = 0;
			if (col > 1)
				col = 1;

			RelativeRect rect = null;

			if (col == 0)
				rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerLeft);
			else
				rect = new RelativeRect(RelativeLoc.XLeftBorder + (RelativeLoc.BorderOffset * 2 + ButtonWidth.Paramater), RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerLeft);

			rect.Y.Shift(ButtonHeight * ButtonShift * ColCounts[col]);
			buttonInfo.Row = ColCounts[col];
			ColCounts[col]++;

			buttonInfo.Button.Rect = rect;
			MenuButton quit = new MenuButton(rect, MenuRes.Quit);
			quit.Clicked += Quit_Clicked;
			AddElement(buttonInfo.Button, ButtonLayerIndex);
		}

		protected void SetupAPIButtons()
		{
			foreach (var buttonInfo in MenuAPI.GetAPIButtons(MenuAPI.MainMenuName))
				RegisterButton(buttonInfo);
		}

		protected void SetupButons(int layerIndex)
        {
			ButtonLayerIndex = layerIndex;

			// column 1 buttons
			RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerLeft);

            MenuButton quit = new MenuButton(rect, MenuRes.Quit);
            quit.Clicked += Quit_Clicked;
            AddElement(quit, layerIndex);

            rect.Y.Shift(ButtonHeight * ButtonShift);
            MenuButton options = new MenuButton(rect, MenuRes.Options);
            options.Clicked += Options_Clicked;
            AddElement(options, layerIndex);

            rect.Y.Shift(ButtonHeight * ButtonShift);
            MenuButton startClient = new MenuButton(rect, MenuRes.StartClient);
            startClient.Clicked += StartClient_Clicked;
            AddElement(startClient, layerIndex);

            rect.Y.Shift(ButtonHeight * ButtonShift);
            MenuButton startServer = new MenuButton(rect, MenuRes.StartServer);
            startServer.Clicked += StartServer_Clicked;
            AddElement(startServer, layerIndex);

			ColCounts[0] = 4;

			// column 2 buttons
			rect = new RelativeRect(RelativeLoc.XLeftBorder + (RelativeLoc.BorderOffset * 2 + ButtonWidth.Paramater), RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerLeft);

            MenuButton tutorials = new MenuButton(rect, MenuRes.Tutorials);
            tutorials.Clicked += Tutorials_Clicked;
            tutorials.Disable();
            AddElement(tutorials, layerIndex);

			ColCounts[1] = 1;
		}

        protected void SetupLogo(int layerIndex)
		{
			var logo = new UIImage("ui/LL_logo_full.png", RelativePoint.UpperThirdCenter, OriginLocation.Center, RelativeSize.FullWidth);

			AddElement(logo, layerIndex);

			layerIndex++;
			string version = FileVersionInfo.GetVersionInfo(this.GetType().Assembly.Location).FileVersion;
			string engine = FileVersionInfo.GetVersionInfo(typeof(Core).Assembly.Location).FileVersion;
			string versString = Resources.VersionLabel.Replace("$V", version).Replace("$E", engine);

			AddElement(new UILabel(MenuManager.MainFont, versString, RelativePoint.Center, RelativeSize.BorderHeight), layerIndex);
		}

		protected void SetupBackground(int layerIndex)
		{
			string bgRepeat = "ui/BackgroundCrosses.png";

			TextureManager.GetTexture(bgRepeat).SetTextureFormat(TextureInfo.TextureFormats.TextureMap); // force this to repeat

			var background = new UIPanel(RelativeRect.Full, ColorConfig.background.Color);
			background.Children.Add(new UIPanel(RelativeRect.Full, Color.White, bgRepeat));
			AddElement(background, layerIndex);
		}

		protected void SetupCredits(int layerIndex)
		{
			float startParam = RelativeSize.BorderHeight.Paramater;

			float baseCharHeight = RelativeSize.BorderHeight.Paramater * 0.75f;

			string[] lines = Resources.Credits.Split("\r\n".ToCharArray());
			Array.Reverse(lines);
			foreach (var line in lines)
			{
				bool bold = false;
				string actualLine = line;
				if (line.StartsWith("$"))
				{
					bold = true;
					actualLine = line.Substring(1);
				}

				float height = baseCharHeight;
				if (bold)
					height *= 1.25f;

				if (line != string.Empty)
				{
					RelativePoint p = new RelativePoint(RelativeLoc.XRightBorder, new RelativeLoc(startParam, RelativeLoc.Edge.Minimal));

					AddElement(new UILabel(bold ? MenuManager.BoldFont : MenuManager.MainFont, actualLine, Color.FromArgb(128, Color.WhiteSmoke), p, new RelativeSize(height * 1.15f, false), null, OriginLocation.LowerRight), layerIndex);
				}

				startParam += height * 0.75f;

			}
		}
	}
}
