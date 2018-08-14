using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.GUI;

using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

namespace LunarLambda.GUI.Menus
{
	public class MainMenu : Menu
	{
		public override void Activate()
		{
			base.Activate();

			var overlay = new Overlay();
            overlay.Rect = new RelativeRect(RelativeLoc.XCenter, RelativeLoc.YCenter, RelativeSize.HalfWidth, RelativeSize.HalfHeight);
			overlay.BaseColor = Color.Navy;
			AddElement(overlay);
		}
	}
}
