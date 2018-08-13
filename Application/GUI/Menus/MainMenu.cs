using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.GUI;

using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI.Elements;

namespace LunarLambda.GUI.Menus
{
	public class MainMenu : Menu
	{
		public override void Activate()
		{
			base.Activate();
			var overlay = new Overlay();
			overlay.BaseColor = Color.Navy;
			this.GUIElements.Add(overlay);
		}
	}
}
