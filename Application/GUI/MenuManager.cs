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
		public bool Active { get; protected set; }
		public virtual void Activate() { Active = true; }
		public virtual void Deactivate() { Active = false; }
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
