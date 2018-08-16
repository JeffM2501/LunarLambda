using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudicrousElectron.GUI;

using LunarLambda.GUI.Menus;

namespace LunarLambda.GUI
{
	public class Menu : Canvas
	{
		public virtual void Activate() { }
		public virtual void Deactivate() { }
	}

	public static class MenuManager
	{
		private static MainMenu Main = new MainMenu();

		public static int MainFont = -1;
		public static int BoldFont = -1;

		public static void Setup()
		{
			GUIManager.PushCanvas(Main);
			Main.Activate();
		}
	}
}
