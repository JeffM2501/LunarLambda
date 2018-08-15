using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.GUI;

using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.GUI.Config;

namespace LunarLambda.GUI.Menus
{
	public class MainMenu : Menu
	{
		public override void Activate()
		{
			base.Activate();

			var background = new Panel(RelativeRect.Full, ColorConfig.background.Color);
			background.Children.Add(new Panel(RelativeRect.Full, Color.White, "ui/BackgroundCrosses.png"));
			AddElement(background);

 //			overlay = new Panel(new RelativeRect(RelativeLoc.XCenter, RelativeLoc.YCenter, RelativeSize.HalfWidth, RelativeSize.HalfHeight),Color.White, "ui/BackgroundCrosses.png");
 //			AddElement(overlay,1);
		}
	}
}
