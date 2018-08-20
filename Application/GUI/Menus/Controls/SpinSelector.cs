using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.Engine.Window;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LudicrousElectron.GUI.Text;

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

		public event EventHandler ValueChanged = null;

		public SpinSelector(RelativeRect rect, IEnumerable<string> labels, int defaultIndex, int font = -1) : base(rect, "ui/SelectorBackground.png")
		{
			IgnoreMouse = false;
			FillMode = UIFillModes.Stretch4Quad;
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

			LeftButton.DefaultMaterial.Texture = "ui/SelectorArrow.png";
			LeftButton.DefaultMaterial.Color = Color.White;
			LeftButton.Hover = new GUIMaterial("ui/SelectorArrow.png", Color.LightSteelBlue);
			LeftButton.Active = new GUIMaterial("ui/SelectorArrow.png", Color.Gray);
			LeftButton.Disabled = new GUIMaterial("ui/SelectorArrow.png", Color.Transparent);
			LeftButton.ClickSound = "button.wav";

			LeftButton.Clicked += LeftButton_Clicked;

			if (SelectedIndex == 0)
				LeftButton.Disable();

			AddChild(LeftButton);

 			RelativeRect rightRect = new RelativeRect(RelativePoint.MiddleRight, new RelativeSizeXY(RelativeSize.FullHeight, RelativeSize.FullHeight), OriginLocation.MiddleRight);
 
 			RightButton = new UIButton(rightRect, string.Empty);
 			RightButton.FillMode = UIFillModes.Stretch;
 			RightButton.FlipTextureAxes(-1,1);
 
 			RightButton.DefaultMaterial.Texture = "ui/SelectorArrow.png";
 			RightButton.DefaultMaterial.Color = Color.White;
 			RightButton.Hover = new GUIMaterial("ui/SelectorArrow.png", Color.LightSteelBlue);
 			RightButton.Active = new GUIMaterial("ui/SelectorArrow.png", Color.Gray);
			RightButton.Disabled = new GUIMaterial("ui/SelectorArrow.png", Color.Transparent);
			RightButton.ClickSound = "button.wav";

			RightButton.Clicked += RightButton_Clicked;

			if (SelectedIndex == TextLabels.Count - 1)
				RightButton.Disable();

			AddChild(RightButton);
		}

		private void RightButton_Clicked(object sender, UIButton e)
		{
			if (SelectedIndex == TextLabels.Count - 1)
				return;

			SelectedIndex++;
			LabelControl.Text = TextLabels[SelectedIndex];
			LabelControl.ForceRefresh();
			ValueChanged?.Invoke(this, EventArgs.Empty);

			if (SelectedIndex == TextLabels.Count - 1)
				RightButton.Disable();

			LeftButton.Enable();
		}

		private void LeftButton_Clicked(object sender, UIButton e)
		{
			if (SelectedIndex == 0)
				return;

			SelectedIndex--;
			LabelControl.Text = TextLabels[SelectedIndex];
			LabelControl.ForceRefresh();
			ValueChanged?.Invoke(this, EventArgs.Empty);

			if (SelectedIndex == 0)
				LeftButton.Disable();

			RightButton.Enable();
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

		public override void Resize(int x, int y)
		{
			if (TextLabels.Count > 0 && LabelControl == null)
				SetupLabel();

			base.Resize(x, y);
		}
	}
}
