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
	public class MainMenu : MenuCommon
	{
		protected VerticalLayoutGroup[] Columns = new VerticalLayoutGroup[] {null,null};


		internal MainMenu() : base()
		{
			
		}

		protected override void SetupControls()
		{
			int layerIndex = 0;
			SetupBackground(layerIndex++);
			SetupLogo(layerIndex++);
			SetupCredits(layerIndex++);
			SetupButons(layerIndex++);

			AddAPIButtons(MenuAPI.MainMenuName);
			base.SetupControls();
		}

		public override LayoutContainer GetContainerForAPIButton(int row, int col)
		{
			if (col == 0)
				return Columns[0];
			else
				return Columns[1];
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
			MenuManager.PushMenu(MenuAPI.OptionsMenuName);
        }

        private void Quit_Clicked(object sender, UIButton e)
        {
			Core.Exit();
        }

		protected void SetupButons(int layerIndex)
        {
			RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, RelativeSize.HalfHeight, OriginLocation.LowerLeft);

			Columns[0] = new VerticalLayoutGroup(rect);
			Columns[0].ChildSpacing = 5;
			Columns[0].MaxChildSize = 45;
			Columns[0].TopDown = false;
			Columns[0].FitChildToWidth = true;

			MenuButton quit = new MenuButton(new RelativeRect(), MenuRes.Quit);
			quit.Clicked += Quit_Clicked;
			Columns[0].AddChild(quit);
			
			MenuButton options = new MenuButton(new RelativeRect(), MenuRes.Options);
			options.Clicked += Options_Clicked;
			Columns[0].AddChild(options);
			
			MenuButton startClient = new MenuButton(new RelativeRect(), MenuRes.StartClient);
			startClient.Clicked += StartClient_Clicked;
			Columns[0].AddChild(startClient);
			
			MenuButton startServer = new MenuButton(new RelativeRect(), MenuRes.StartServer);
			startServer.Clicked += StartServer_Clicked;
			Columns[0].AddChild(startServer);

			AddElement(Columns[0], layerIndex);

			// column 2 buttons
			rect = new RelativeRect(RelativeLoc.XLeftBorder + (RelativeLoc.BorderOffset * 2 + ButtonWidth.Paramater), RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, RelativeSize.HalfHeight, OriginLocation.LowerLeft);

			Columns[1] = new VerticalLayoutGroup(rect);
			Columns[1].ChildSpacing = ButtonSpacing.Paramater;
			Columns[1].MaxChildSize = ButtonHeight.Paramater;
			Columns[1].TopDown = false;
			Columns[1].FitChildToWidth = true;

            MenuButton tutorials = new MenuButton(new RelativeRect(), MenuRes.Tutorials);
            tutorials.Clicked += Tutorials_Clicked;
            tutorials.Disable();
			Columns[1].AddChild(tutorials);
			AddElement(Columns[1], layerIndex);
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

				if (!string.IsNullOrEmpty(line))
				{
					RelativePoint p = new RelativePoint(RelativeLoc.XRightBorder, new RelativeLoc(startParam, RelativeLoc.Edge.Minimal));

					AddElement(new UILabel(bold ? MenuManager.BoldFont : MenuManager.MainFont, actualLine, Color.FromArgb(128, Color.WhiteSmoke), p, new RelativeSize(height * 1.15f, false), null, OriginLocation.LowerRight), layerIndex);
				}

				startParam += height * 0.75f;

			}
		}
	}
}
