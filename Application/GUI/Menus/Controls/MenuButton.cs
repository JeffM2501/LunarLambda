using System;
using System.Collections.Generic;
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
			ClickSound = "button.wav";
			HoverSound = "effects/click.wav";

            DefaultMaterial = new GUIMaterial("ui/ButtonBackground.png", Color.White);
            ActiveMaterial = new GUIMaterial("ui/ButtonBackground.active.png", Color.White);
            DisabledMaterial = new GUIMaterial("ui/ButtonBackground.disabled.png", Color.White);
            HoverMaterial = new GUIMaterial("ui/ButtonBackground.hover.png", Color.White);

			ActiveTextColor = Color.Black;
			HoverTextColor = Color.LightSteelBlue;
        }
	}
}
