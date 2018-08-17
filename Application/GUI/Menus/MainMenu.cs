using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

using LudicrousElectron.GUI;

using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.GUI.Config;
using OpenTK;
using LudicrousElectron.Engine;

namespace LunarLambda.GUI.Menus
{
	public class MainMenu : Menu
	{
		public static RelativeSize ButtonWidth = new RelativeSize(1.0f / 4.0f, true);
		public static RelativeSize ButtonHeight = new RelativeSize(1.0f / 18.0f, false);
		public override void Activate()
		{
			base.Activate();

			int layerIndex = 0;
			SetupBackground(layerIndex++);
			SetupLogo(layerIndex++);
			SetupCredits(layerIndex++);

			RelativePoint p = new RelativePoint(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset);
			RelativeRect rect = new RelativeRect(p, new RelativeSizeXY(ButtonWidth, ButtonHeight));
			rect.AnchorLocation = OriginLocation.LowerLeft;
			UIButton button = new UIButton(rect);
			button.DefaultTexture = "ui/ButtonBackground.png";

			button.Children.Add(new UILabel(MenuManager.MainFont, "Quit",RelativePoint.Center,RelativeSize.FullHeight + (0.3f)));

			AddElement(button, layerIndex);
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
			var background = new UIPanel(RelativeRect.Full, ColorConfig.background.Color);
			background.Children.Add(new UIPanel(RelativeRect.Full, Color.White, "ui/BackgroundCrosses.png"));
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
