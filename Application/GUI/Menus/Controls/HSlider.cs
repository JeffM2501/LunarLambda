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
	public class HSlider : UIPanel
	{
		protected string TextLabelPrefix = string.Empty;
		protected int Font = -1;

		protected UILabel LabelControl = null;
		protected UIButton LeftButton = null;
		protected UIButton RightButton = null;

		protected UIButton ThumbButton = null;

		public bool ShowPercentage = true;

		public double CurrentValue { get; protected set; } = 0;

		public double ValueStep = 10;

		public event EventHandler ValueChanged = null;

		public HSlider(RelativeRect rect, string labelPrefix, double value, int font = -1) : base(rect, "ui/SliderBackground.png")
		{
			IgnoreMouse = false;
			FillMode = UIFillModes.Stretch4Quad;
			Font = font;
			CurrentValue = value;
			SetLabels(labelPrefix);

			SetupButtons();
		}

		public virtual void SetLabels(string prefix)
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

		public string GetText()
		{
			return TextLabelPrefix + (ShowPercentage ? (" " + ((int)(CurrentValue + 0.499)).ToString() + "%") : string.Empty);
		}

		protected virtual void SetupLabel()
		{
			if (!WindowManager.Inited())
				return;

			if (Font == -1)
				Font = FontManager.DefaultFont;

			LabelControl = new UILabel(Font, GetText(), RelativePoint.Center, RelativeSize.FullHeight + (0.35f), RelativeSize.FullWidth * 0.75f);
			LabelControl.SetFittingMode(UILabel.TextFittingModes.ByWidth);
			LabelControl.DefaultMaterial.Color = Color.White;
			AddChild(LabelControl);
		}

		protected virtual void SetupButtons()
		{
			RelativeRect leftRect = new RelativeRect(RelativePoint.MiddleLeft, new RelativeSizeXY(RelativeSize.FullHeight * 0.5f, RelativeSize.FullHeight), OriginLocation.MiddleLeft);

			LeftButton = new UIButton(leftRect, string.Empty);
			LeftButton.FillMode = UIFillModes.Stretch;

			LeftButton.DefaultMaterial.Texture = "ui/SliderMinusButton.png";
			LeftButton.DefaultMaterial.Color = Color.White;
			LeftButton.Hover = new GUIMaterial("ui/SliderMinusButton.png", Color.LightSteelBlue);
			LeftButton.Active = new GUIMaterial("ui/SliderMinusButton.png", Color.Gray);
			LeftButton.Disabled = new GUIMaterial("ui/SliderMinusButton.png", Color.Transparent);
			LeftButton.ClickSound = "button.wav";

			LeftButton.Clicked += LeftButton_Clicked;

			if (CurrentValue <= 0.499)
				LeftButton.Disable();

			AddChild(LeftButton);

			RelativeRect rightRect = new RelativeRect(RelativePoint.MiddleRight, new RelativeSizeXY(RelativeSize.FullHeight * 0.5f, RelativeSize.FullHeight), OriginLocation.MiddleRight);

			RightButton = new UIButton(rightRect, string.Empty);
			RightButton.FillMode = UIFillModes.Stretch;

			RightButton.DefaultMaterial.Texture = "ui/SliderPlusButtons.png";
			RightButton.DefaultMaterial.Color = Color.White;
			RightButton.Hover = new GUIMaterial("ui/SliderPlusButtons.png", Color.LightSteelBlue);
			RightButton.Active = new GUIMaterial("ui/SliderPlusButtons.png", Color.Gray);
			RightButton.Disabled = new GUIMaterial("ui/SliderPlusButtons.png", Color.Transparent);

			RightButton.ClickSound = "button.wav";

			RightButton.Clicked += RightButton_Clicked;

			if (CurrentValue >= 99.5)
				RightButton.Disable();

			AddChild(RightButton);

			RelativeRect thumbRect = new RelativeRect(RelativePoint.Center, new RelativeSizeXY(RelativeSize.FullHeight, RelativeSize.FullHeight), OriginLocation.Center);

			ThumbButton = new UIButton(thumbRect, string.Empty);
			ThumbButton.FillMode = UIFillModes.Stretch;

			ThumbButton.DefaultMaterial.Texture = "ui/SliderKnob.png";
			ThumbButton.DefaultMaterial.Color = Color.FromArgb(128,Color.WhiteSmoke);
			ThumbButton.Hover = new GUIMaterial("ui/SliderKnob.png", Color.White);
			ThumbButton.Active = new GUIMaterial("ui/SliderKnob.png", Color.FromArgb(128, Color.LightGray));
			ThumbButton.Disabled = new GUIMaterial("ui/SliderKnob.png", Color.Transparent);

			AddChild(ThumbButton);
		}

		private void RightButton_Clicked(object sender, UIButton e)
		{
			if (CurrentValue >= 100)
				return;

			CurrentValue += ValueStep;
			if (CurrentValue > 100)
				CurrentValue = 100;

			LabelControl.Text = GetText();
			if (ShowPercentage)
				LabelControl.ForceRefresh();
			ValueChanged?.Invoke(this, EventArgs.Empty);

			if (CurrentValue >= 99.5)
				RightButton.Disable();

			LeftButton.Enable();
			SetThumbPos();
		}

		private void LeftButton_Clicked(object sender, UIButton e)
		{
			if(CurrentValue <= 0)
				return;

			CurrentValue -= ValueStep;
			if (CurrentValue < 0)
				CurrentValue = 0;

			LabelControl.Text = GetText();
			if (ShowPercentage)
				LabelControl.ForceRefresh();

			ValueChanged?.Invoke(this, EventArgs.Empty);

			if (CurrentValue < 0.49)
				LeftButton.Disable();

			RightButton.Enable();
			SetThumbPos();
		}

		public override void ProcessMouseEvent(Vector2 location, InputManager.LogicalButtonState buttons)
		{
			if (!buttons.PrimaryDown)
				return;

			float width = Rect.GetPixelSize().X;
			float height = Rect.GetPixelSize().Y;

			float availableDist = width - (height * 2);

			if (location.X < height || location.X > availableDist + height)
				return;

			if (availableDist <= 0)
				return;

			float param = (location.X - height) / availableDist;
			CurrentValue = Math.Abs(param) * 100;

			LabelControl.Text = GetText();
			if (ShowPercentage)
				LabelControl.ForceRefresh();

			ValueChanged?.Invoke(this, EventArgs.Empty);

			if (CurrentValue < 0.49)
				LeftButton.Disable();
			else
				LeftButton.Enable();

			if (CurrentValue >= 99.5)
				RightButton.Disable();
			else
				RightButton.Enable();

			SetThumbPos();
		}

		public override void FlushMaterial()
		{
			base.FlushMaterial();
			if (LabelControl != null)
			{
				LabelControl.DefaultMaterial.Color = Color.White; ;
				LabelControl.FlushMaterial();
			}
		}

		protected void SetThumbPos(bool forceThumbResize = true)
		{
			float width = Rect.GetPixelSize().X;
			float height = Rect.GetPixelSize().Y;

			float availableDist = width - (height * 2);
			float paramDist = availableDist * (float)(CurrentValue / 100.0f);

			ThumbButton.Rect.X.Paramater = (height * 1) + paramDist;
			ThumbButton.Rect.X.RelativeTo = RelativeLoc.Edge.Raw;

			if (forceThumbResize)
				ThumbButton.ForceRefresh();
		}

		public override void Resize(int x, int y)
		{
			if ((TextLabelPrefix == string.Empty || ShowPercentage) && LabelControl == null)
				SetupLabel();

			// see how big this is in pixel space so we can position the thumb
			this.Rect.Resize(x, y);
			SetThumbPos(false);

			base.Resize(x, y);
		}
	}
}
