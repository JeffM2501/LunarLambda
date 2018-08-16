using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.GUI;

using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.GUI.Config;
using OpenTK;

namespace LunarLambda.GUI.Menus
{
	public class MainMenu : Menu
	{
		public override void Activate()
		{

			int layerIndex = 0;
			base.Activate();

			var background = new UIPanel(RelativeRect.Full, ColorConfig.background.Color);
			background.Children.Add(new UIPanel(RelativeRect.Full, Color.White, "ui/BackgroundCrosses.png"));
			AddElement(background, layerIndex);

			layerIndex++;

			var logo = new UIImage("ui/LL_logo_full.png", RelativePoint.UpperThirdCenter, OriginLocation.Center,RelativeSize.FullWidth);
			AddElement(logo, layerIndex);
		}
	}
}
