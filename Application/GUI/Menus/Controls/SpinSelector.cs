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
	public class SpinSelector : UIPanel
	{
		protected List<string> TextLabels = new List<string>();
		protected int Font = -1;

		protected UILabel LabelControl = null;
		protected UIButton LeftButton = null;
		protected UIButton RightButton = null;

		public int SelectedIndex { get; protected set; } = 0;

		public event EventHandler<SpinSelector> ValueChanged = null;

		public SpinSelector(RelativeRect rect, IEnumerable<string> labels, int defaultIndex, int font = -1) : base(rect, ThemeManager.GetThemeAsset("ui/SelectorBackground.png"))
		{
			IgnoreMouse = false;
			FillMode = UIFillModes.SmartStprite;
			Font = font;
			SelectedIndex = defaultIndex;
			SetLabels(labels);

			SetupButtons();
		}

		public virtual void SetLabels(IEnumerable<string> labels)
		{
			TextLabels.Clear();
			TextLabels.AddRange(labels);

			if (LabelControl == null)
				SetupLabel();
			else
			{
				LabelControl.Text = TextLabels[SelectedIndex];
				SetDirty();
			}
		}

		public string GetText()
		{
			return TextLabels[SelectedIndex];
		}

        public int IndexFromText(string text)
        {
            return TextLabels.FindIndex((x) => text == x);
        }

		protected virtual void SetupLabel()
		{
			if (!WindowManager.Inited())
				return;

			if (Font == -1)
				Font = FontManager.DefaultFont;

			LabelControl = new UILabel(Font, TextLabels[SelectedIndex], RelativePoint.Center, RelativeSize.FullHeight + (0.35f), RelativeSize.FullWidth * 0.75f);
			LabelControl.SetFittingMode(UILabel.TextFittingModes.ByHeightTrim);
			LabelControl.DefaultMaterial.Color = Color.White;
			AddChild(LabelControl);
		}

		protected virtual void SetupButtons()
		{
			RelativeRect leftRect = new RelativeRect(RelativePoint.MiddleLeft, new RelativeSizeXY(RelativeSize.FullHeight, RelativeSize.FullHeight),OriginLocation.MiddleLeft);

			LeftButton = new UIButton(leftRect, string.Empty);
			LeftButton.FillMode = UIFillModes.Stretch;

			LeftButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SelectorArrow.png");
			LeftButton.DefaultMaterial.Color = Color.White;
			LeftButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SelectorArrow.png"), Color.LightSteelBlue);
			LeftButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SelectorArrow.png"), Color.Gray);
			LeftButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SelectorArrow.png"), Color.Transparent);
			LeftButton.ClickSound = "button.wav";

			LeftButton.Clicked += LeftButton_Clicked;

			if (SelectedIndex == 0)
				LeftButton.Disable();

			AddChild(LeftButton);

 			RelativeRect rightRect = new RelativeRect(RelativePoint.MiddleRight, new RelativeSizeXY(RelativeSize.FullHeight, RelativeSize.FullHeight), OriginLocation.MiddleRight);
 
 			RightButton = new UIButton(rightRect, string.Empty);
 			RightButton.FillMode = UIFillModes.Stretch;
 			RightButton.FlipTextureAxes(-1,1);
 
 			RightButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SelectorArrow.png");
 			RightButton.DefaultMaterial.Color = Color.White;
 			RightButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SelectorArrow.png"), Color.LightSteelBlue);
 			RightButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SelectorArrow.png"), Color.Gray);
			RightButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SelectorArrow.png"), Color.Transparent);
			RightButton.ClickSound = "button.wav";

			RightButton.Clicked += RightButton_Clicked;

			if (SelectedIndex == TextLabels.Count - 1)
				RightButton.Disable();

			AddChild(RightButton);
		}

		private void RightButton_Clicked(object sender, UIButton e)
		{
            SetSelectedIndex(SelectedIndex + 1);
		}

		private void LeftButton_Clicked(object sender, UIButton e)
		{
            SetSelectedIndex(SelectedIndex - 1);
		}

        public void SetSelectedIndex(int index)
        {
            SelectedIndex = index;

            if (index <= 0)
                SelectedIndex = 0;
            else if (index >= TextLabels.Count - 1)
                SelectedIndex = TextLabels.Count - 1;

            LabelControl.Text = TextLabels[SelectedIndex];
            LabelControl.ForceRefresh();
            ValueChanged?.Invoke(this, this);

            if (SelectedIndex == 0)
                LeftButton.Disable();
            else
                LeftButton.Enable();

            if (SelectedIndex == TextLabels.Count - 1)
                RightButton.Disable();
            else
                RightButton.Enable();
        }

		public override void FlushMaterials( bool children = false)
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
			if (TextLabels.Count > 0 && LabelControl == null)
				SetupLabel();

			base.Resize(x, y);
		}


        public override void ProcessMouseEvent(Vector2 location, InputManager.LogicalButtonState buttons)
        {
			if (Math.Abs(buttons.WheelTick) > 0 && TextLabels.Count > 1)
				SetSelectedIndex(SelectedIndex + buttons.WheelTick);

			if (!buttons.PrimaryClick || ParentCanvas == null || ParentCanvas.PopupEnabled() || TextLabels.Count < 2)
                return;

            float width = Rect.GetPixelSize().X;
            float height = Rect.GetPixelSize().Y;

			Vector2 origin = Rect.GetPixelOrigin();

            float availableDist = width - (height * 2);

            if (location.X < origin.X + height || location.X > origin.X + availableDist + height)
                return;

            Vector2 thisOrigin = GetScreenOrigin();

            float thisCenterY = thisOrigin.Y + (height * 0.5f);

            float totalheight = (MenuCommon.ButtonSpacing.Paramater + (height * 1)) * TextLabels.Count;
            totalheight += MenuCommon.ButtonSpacing.Paramater * 1;

            float halfHeight = totalheight * 0.5f;

            float screenHeight = ParentCanvas.BoundWindow.Height;

            // see where the popup will land on the screen.

            OriginLocation originAllignment = OriginLocation.LowerLeft;
            if (totalheight > screenHeight)  // it won't fit, center it
                originAllignment = OriginLocation.MiddleLeft;
            else
            {
                if (thisCenterY - halfHeight > 0 && thisCenterY + halfHeight <= screenHeight)
                    originAllignment = OriginLocation.MiddleLeft; // it'll fit centered, that looks better
                else
                {
                    // it won't fit centered, so put it on the other side of the screen from where the button is
                    if (thisCenterY > halfHeight)
                        originAllignment = OriginLocation.UpperLeft;
                    else
                        originAllignment = OriginLocation.LowerLeft;
                }
            }

            if (originAllignment == OriginLocation.UpperLeft)
                thisOrigin.Y += height;
            else if (originAllignment == OriginLocation.MiddleLeft)
                thisOrigin.Y += height * 0.5f;

            RelativeRect rect = new RelativeRect(new RelativeLoc(thisOrigin.X, RelativeLoc.Edge.Raw), new RelativeLoc(thisOrigin.Y, RelativeLoc.Edge.Raw), RelativeSize.FixedPixelSize(width), RelativeSize.FixedPixelSize(totalheight), originAllignment);

            var popup = new UIPanel(rect, ThemeManager.GetThemeAsset("ui/SelectorPopupBackground.png"));
            popup.FillMode = UIFillModes.SmartStprite;
            popup.IgnoreMouse = false;

            VerticalLayoutGroup vertgroup = MenuCommon.SetupCommonColumn(new RelativeRect(RelativeLoc.XCenter,RelativeLoc.YCenter,RelativeSize.ThreeQuarterWidth, rect.Height, OriginLocation.Center));
            vertgroup.FirstElementHasSpacing = true;

            foreach (var label in TextLabels)
            {
                MenuButton button = new MenuButton(new RelativeRect(), label);
                button.Tag = label;
                button.Clicked += PopupButton_Clicked;
                if (label == GetText())
                    button.Check();

                vertgroup.AddChild(button);
            }
            popup.AddChild(vertgroup);

            ParentCanvas.SetPopupElement(popup);
        }

        private void PopupButton_Clicked(object sender, UIButton e)
        {
            SetSelectedIndex(IndexFromText(e.Tag as string));
            ParentCanvas.SetPopupElement(null);
        }
    }
}
