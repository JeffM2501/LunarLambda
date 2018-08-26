using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.Engine.Input;
using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LudicrousElectron.GUI.Text;

using OpenTK;

namespace LunarLambda.GUI.Menus.Controls
{
	public class VSlider : BaseSlider
	{
		public VSlider(RelativeRect rect, int value, int min = 0, int max = 100) : base(rect,value, min, max, ThemeManager.GetThemeAsset("ui/SliderBackground.png"))
		{
			IgnoreMouse = false;
			Vertical = true;
			ValueStep = 1;
		}

		protected override void SetupButtons()
		{
			this.FillMode = LudicrousElectron.GUI.UIFillModes.StretchMiddle;

			// add buttons

			RelativeRect upperRect = new RelativeRect(RelativePoint.UpperCenter, new RelativeSizeXY(RelativeSize.FullWidth, RelativeSize.FullWidth * 0.5f), OriginLocation.UpperCenter);
			RetreatButton = new UIButton(upperRect, string.Empty);
			RetreatButton.FillMode = UIFillModes.Stretch;
			RetreatButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderUpButton.png");
			RetreatButton.DefaultMaterial.Color = Color.White;
			RetreatButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderUpButton.png"), Color.LightSteelBlue);
			RetreatButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderUpButton.png"), Color.Gray);
			RetreatButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderUpButton.png"), Color.Transparent);

			RelativeRect lowerRect = new RelativeRect(RelativePoint.LowerCenter, new RelativeSizeXY(RelativeSize.FullWidth, RelativeSize.FullWidth * 0.5f), OriginLocation.LowerCenter);
			AdvanceButton = new UIButton(lowerRect, string.Empty);
			AdvanceButton.FillMode = UIFillModes.Stretch;
			AdvanceButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderDownButton.png");
			AdvanceButton.DefaultMaterial.Color = Color.White;
			AdvanceButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderDownButton.png"), Color.LightSteelBlue);
			AdvanceButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderDownButton.png"), Color.Gray);
			AdvanceButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderDownButton.png"), Color.Transparent);


			RelativeRect thumbRect = new RelativeRect(RelativePoint.Center, new RelativeSizeXY(RelativeSize.FullWidth, RelativeSize.FullWidth), OriginLocation.Center);

			ThumbButton = new UIButton(thumbRect, string.Empty);
			ThumbButton.FillMode = UIFillModes.Stretch;

			ThumbButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderKnob.png");
			ThumbButton.DefaultMaterial.Color = Color.FromArgb(128, Color.WhiteSmoke);
			ThumbButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.White);
			ThumbButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.FromArgb(128, Color.LightGray));
			ThumbButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.Transparent);
		}
	}
}
