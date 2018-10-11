using System.Drawing;

using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

namespace LunarLambda.GUI.Menus.Controls
{
    public class MenuTextEntry : UITextEntry
	{
		public MenuTextEntry(RelativeRect rect, string text) : base(rect, text)
		{
			DefaultMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"), Color.White);
			FocusedMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/TextEntryBackground.focused.png"), Color.White);
		}
	}
}
