using System.Drawing;


using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

namespace LunarLambda.GUI.Menus.Controls
{
    public class MenuButton : UIButton
    {
        public MenuButton(RelativeRect rect) : base(rect)
        {
            Font = MenuManager.MainFont;
            FillMode = UIFillModes.StretchMiddle;
            Setup();
        }

        public MenuButton(RelativeRect rect, string text) : base(rect)
        {
            Font = MenuManager.MainFont;
            FillMode = UIFillModes.StretchMiddle;
            SetText(text);
            Setup();
        }

        protected override void Setup()
        {
			ClickSound = "effects/click1.wav";
			HoverSound = "effects/rollover3.wav";

            DefaultMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/ButtonBackground.png"), Color.White);
            ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/ButtonBackground.active.png"), Color.White);
            DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/ButtonBackground.disabled.png"), Color.White);
            HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/ButtonBackground.hover.png"), Color.White);

			ActiveTextColor = Color.Black;
			HoverTextColor = Color.LightSteelBlue;
        }
	}
}
