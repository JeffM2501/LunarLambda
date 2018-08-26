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
	public class HSlider : BaseSlider
	{
		protected string TextLabelPrefix = string.Empty;
		protected int Font = -1;
		protected UILabel LabelControl = null;

		public bool ShowPercentage = true;
		public bool ShowLabel = true;

		public HSlider(RelativeRect rect, string labelPrefix, int value, int font = -1, int min = 0, int max = 100) : base(rect, value, min, max, ThemeManager.GetThemeAsset("ui/SliderBackground.png"))
		{
			Vertical = false;
			IgnoreMouse = false;
			SetLabels(labelPrefix);

			ValueChanged += HSlider_ValueChanged;
		}

		public virtual void SetLabels(string prefix)
		{
			if (ShowLabel)
			{
				TextLabelPrefix = prefix;
				if (LabelControl == null)
					SetupLabel();
				else
				{
					LabelControl.Text = GetText();
					SetDirty();
				}
			}
		}

		public string GetText()
		{
			return TextLabelPrefix + (ShowPercentage ? (" " + ((int)(CurrentValue + 0.499)).ToString() + "%") : string.Empty);
		}

		protected virtual void SetupLabel()
		{
			if (!WindowManager.Inited() || !ShowLabel)
				return;

			if (Font == -1)
				Font = FontManager.DefaultFont;

			LabelControl = new UILabel(Font, GetText(), RelativePoint.Center, RelativeSize.FullHeight + (0.35f), RelativeSize.FullWidth * 0.75f);
			LabelControl.SetFittingMode(UILabel.TextFittingModes.ByWidth);
			LabelControl.DefaultMaterial.Color = Color.White;
			AddChild(LabelControl);
		}

		protected override void SetupButtons()
		{
			RelativeRect leftRect = new RelativeRect(RelativePoint.MiddleLeft, new RelativeSizeXY(RelativeSize.FullHeight * 0.5f, RelativeSize.FullHeight), OriginLocation.MiddleLeft);

			RetreatButton = new UIButton(leftRect, string.Empty);
			RetreatButton.FillMode = UIFillModes.Stretch;

			RetreatButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderMinusButton.png");
			RetreatButton.DefaultMaterial.Color = Color.White;
			RetreatButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderMinusButton.png"), Color.LightSteelBlue);
			RetreatButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderMinusButton.png"), Color.Gray);
			RetreatButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderMinusButton.png"), Color.Transparent);
			RetreatButton.ClickSound = "button.wav";


			RelativeRect rightRect = new RelativeRect(RelativePoint.MiddleRight, new RelativeSizeXY(RelativeSize.FullHeight * 0.5f, RelativeSize.FullHeight), OriginLocation.MiddleRight);

			AdvanceButton = new UIButton(rightRect, string.Empty);
			AdvanceButton.FillMode = UIFillModes.Stretch;

			AdvanceButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderPlusButtons.png");
			AdvanceButton.DefaultMaterial.Color = Color.White;
			AdvanceButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderPlusButtons.png"), Color.LightSteelBlue);
			AdvanceButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderPlusButtons.png"), Color.Gray);
			AdvanceButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderPlusButtons.png"), Color.Transparent);

			AdvanceButton.ClickSound = "button.wav";


			RelativeRect thumbRect = new RelativeRect(RelativePoint.Center, new RelativeSizeXY(RelativeSize.FullHeight, RelativeSize.FullHeight), OriginLocation.Center);

			ThumbButton = new UIButton(thumbRect, string.Empty);
			ThumbButton.FillMode = UIFillModes.Stretch;

			ThumbButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderKnob.png");
			ThumbButton.DefaultMaterial.Color = Color.FromArgb(128,Color.WhiteSmoke);
			ThumbButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.White);
			ThumbButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.FromArgb(128, Color.LightGray));
			ThumbButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.Transparent);
		}

		private void HSlider_ValueChanged(object sender, BaseSlider e)
		{
			if (!ShowLabel)
				return;

			LabelControl.Text = GetText();
			if (ShowPercentage)
				LabelControl.ForceRefresh();
		}

		public override void FlushMaterials(bool children = false)
		{
			base.FlushMaterials(children);
			if (LabelControl != null)
			{
				LabelControl.DefaultMaterial.Color = Color.White;
				LabelControl.FlushMaterials(children);
			}
		}

		public override void Resize(int x, int y)
		{
			if (ShowLabel && (string.IsNullOrEmpty(TextLabelPrefix) || ShowPercentage) && LabelControl == null)
				SetupLabel();

			// see how big this is in pixel space so we can position the thumb
			this.Rect.Resize(x, y);
			SetThumbPos(false);

			base.Resize(x, y);
		}
	}
}
