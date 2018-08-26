using System;
using System.Collections.Generic;
using System.Drawing;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using OpenTK;

namespace LunarLambda.GUI.Menus.Controls
{
	public class ButtonScrollList : UIPanel
	{
		protected List<MenuCheckButton> ItemButtons = new List<MenuCheckButton>();

		protected UIButton UpButton = null;
		protected UIButton DownButton = null;

		protected UIPanel ScrollBar = null;
		protected UIButton ThumbButton = null;

		public int DesiredRows = 6;
		public float MiniumElementHeight = 45;

		public float CellPadding = 0;

		protected class ButtonInfo
		{
			public int Index = 0;
			public string Text = string.Empty;
			public object Tag = null;
		}

		protected List<ButtonInfo> ItemList = new List<ButtonInfo>();
		public IEnumerable<string> Items
		{
			get
			{
				List<string> l = new List<string>();
				foreach (var b in ItemList)
					l.Add(b.Text);
				return l;
			}
		}

		public int SelectedIndex { get; protected set; } = - 1;

		public int ScrollbarTop { get; protected set; } = 0;
		protected int VisibleButtonCount = 0;

		public int MiniumButtonHeight = 45;

		public event EventHandler<ButtonScrollList> SelectedIndexChanged = null;

		public ButtonScrollList(RelativeRect rect, int selectedIndex = -1, string texture = null) : base(rect, texture)
		{
			SelectedIndex = selectedIndex;
			IgnoreMouse = false;
			if (string.IsNullOrEmpty(texture))
				Show = false;

			SetupButtons();
		}

		protected virtual void SetupButtons()
		{
			// this will get put into a vscroll component

			ScrollBar = new UIPanel(new RelativeRect(), ThemeManager.GetThemeAsset("ui/SliderBackground.png"));

			ScrollBar.IgnoreMouse = false;
			ScrollBar.Rect.X = RelativeLoc.XRight;
			ScrollBar.Rect.Y = RelativeLoc.YUpper;
			ScrollBar.Rect.Width.Mode = RelativeSize.SizeModes.Raw;
			ScrollBar.Rect.Height = RelativeSize.FullHeight;
			ScrollBar.Rect.AnchorLocation = OriginLocation.UpperRight;

			ScrollBar.FillMode = LudicrousElectron.GUI.UIFillModes.StretchMiddle;
			ScrollBar.Show = false;

			// add buttons
 
 			RelativeRect upperRect = new RelativeRect(RelativePoint.UpperCenter, new RelativeSizeXY(RelativeSize.FullWidth, RelativeSize.FullWidth * 0.5f), OriginLocation.UpperCenter);
			UpButton = new UIButton(upperRect, string.Empty);
			UpButton.FillMode = UIFillModes.Stretch;
			UpButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderUpButton.png");
			UpButton.DefaultMaterial.Color = Color.White;
			UpButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderUpButton.png"), Color.LightSteelBlue);
			UpButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderUpButton.png"), Color.Gray);
			UpButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderUpButton.png"), Color.Transparent);
 			UpButton.Show = false;
 			ScrollBar.AddChild(UpButton);

			RelativeRect lowerRect = new RelativeRect(RelativePoint.LowerCenter, new RelativeSizeXY(RelativeSize.FullWidth, RelativeSize.FullWidth * 0.5f), OriginLocation.LowerCenter);
 			DownButton = new UIButton(lowerRect, string.Empty);
			DownButton.FillMode = UIFillModes.Stretch;
			DownButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderDownButton.png");
			DownButton.DefaultMaterial.Color = Color.White;
			DownButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderDownButton.png"), Color.LightSteelBlue);
			DownButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderDownButton.png"), Color.Gray);
			DownButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderDownButton.png"), Color.Transparent);

			DownButton.Show = false;
 			ScrollBar.AddChild(DownButton);



			RelativeRect thumbRect = new RelativeRect(RelativePoint.Center, new RelativeSizeXY(RelativeSize.FullWidth, RelativeSize.FullWidth), OriginLocation.Center);

			ThumbButton = new UIButton(thumbRect, string.Empty);
			ThumbButton.FillMode = UIFillModes.Stretch;

			ThumbButton.DefaultMaterial.Texture = ThemeManager.GetThemeAsset("ui/SliderKnob.png");
			ThumbButton.DefaultMaterial.Color = Color.FromArgb(128, Color.WhiteSmoke);
			ThumbButton.HoverMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.White);
			ThumbButton.ActiveMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.FromArgb(128, Color.LightGray));
			ThumbButton.DisabledMaterial = new GUIMaterial(ThemeManager.GetThemeAsset("ui/SliderKnob.png"), Color.Transparent);
			ThumbButton.Show = false;

			ScrollBar.AddChild(ThumbButton);

			AddChild(ScrollBar);
			
			if (ScrollbarTop == 0)
				UpButton.Disable();
			else if (ScrollbarTop + VisibleButtonCount == ItemList.Count)
				DownButton.Disable();

			UpButton.Clicked += UpButton_Clicked;
			DownButton.Clicked += DownButton_Clicked;
		}

		protected double GetCurrentValue()
		{
			if (ItemList.Count == 0 || ItemList.Count <= VisibleButtonCount) ;

			return ((double)ScrollbarTop / (double)(ItemList.Count - VisibleButtonCount)) * 100;
		}

		protected void SetThumbPos(bool forceThumbResize = true)
		{
			float width = ScrollBar.Rect.GetPixelSize().X;
			float height = ScrollBar.Rect.GetPixelSize().Y;

			float availableDist = height - (width * 2);
			float paramDist = availableDist * (float)(GetCurrentValue() / 100.0f);

			float top = height - (width);

			ThumbButton.Rect.Y.Paramater = top - paramDist;
			ThumbButton.Rect.Y.RelativeTo = RelativeLoc.Edge.Raw;

			if (forceThumbResize)
				ThumbButton.ForceRefresh();
		}

		private void DownButton_Clicked(object sender, UIButton e)
		{
			ScrollbarTop++;
			if (ScrollbarTop + VisibleButtonCount > ItemList.Count)
				ScrollbarTop--;
			else
			{
				if (ScrollbarTop + VisibleButtonCount == ItemList.Count)
					DownButton.Disable();
				else
					DownButton.Enable();
				UpButton.Enable();
				SetThumbPos();
				ForceRefresh();
			}
		}

		private void UpButton_Clicked(object sender, UIButton e)
		{
			ScrollbarTop--;
			if (ScrollbarTop < 0)
				ScrollbarTop++;
			else
			{
				if (ScrollbarTop == 0)
					UpButton.Disable();
				else
					UpButton.Enable();
				DownButton.Enable();
				SetThumbPos();
				ForceRefresh();
			}
		}

		public void SetSelectedIndex(int index)
		{
			SelectedIndex = index;
			if (Inited)
				ForceRefresh();
		}

		public int AddItem(string text, object tag = null)
		{
			ButtonInfo info = new ButtonInfo();
			info.Index = ItemList.Count;
			info.Text = text;
			info.Tag = tag;

			ItemList.Add(info);

			if (Inited)
				ForceRefresh();

			return info.Index;
		}

		private bool Resizing = false;

		public override void Resize(int x, int y)
		{
			Resizing = true;

			// see how much room we have to work with
			Rect.Resize(x, y);

			Vector2 pixelSize = Rect.GetPixelSize();

			int rowCount = DesiredRows;
			float desiredSize = pixelSize.Y / DesiredRows;
			if (MiniumElementHeight > 0 && desiredSize < MiniumElementHeight)
			{
				desiredSize = MiniumElementHeight;
				rowCount = (int)(pixelSize.Y / desiredSize);
			}

			if (ItemButtons.Count != rowCount)
			{
				while (ItemButtons.Count > rowCount)
				{
					RemoveChild(ItemButtons[ItemButtons.Count - 1]);
					ItemButtons.RemoveAt(ItemButtons.Count - 1);
				}
				while (ItemButtons.Count < rowCount)
				{
					MenuCheckButton newButton = new MenuCheckButton(new RelativeRect(),"Button");
					newButton.Tag = ItemButtons.Count;
					newButton.Show = false;
					newButton.ButtonCheckChanged += ListButton_ButtonCheckChanged;
					ItemButtons.Add(newButton);
					AddChild(newButton);
				}
			}

			VisibleButtonCount = rowCount;

			bool showScrool = ItemList.Count > VisibleButtonCount;

			float scrollWidth = desiredSize;
			float buttonWidth = pixelSize.X;
			if (showScrool)
				buttonWidth -= scrollWidth - CellPadding;

			// set the postions and visibility;

			int count = VisibleButtonCount;
			float buttonY = pixelSize.Y;

			for (int i = 0; i < ItemButtons.Count; i++)
			{
				ItemButtons[i].Show = i < ItemList.Count;

				ItemButtons[i].Rect.X.Paramater = 0;
				ItemButtons[i].Rect.X.RelativeTo = RelativeLoc.Edge.Raw;

				ItemButtons[i].Rect.Y.Paramater = buttonY;
				ItemButtons[i].Rect.Y.RelativeTo = RelativeLoc.Edge.Raw;

				ItemButtons[i].Rect.Width.Paramater = buttonWidth;
				ItemButtons[i].Rect.Width.Mode = RelativeSize.SizeModes.Raw;

				ItemButtons[i].Rect.Height.Paramater = desiredSize;
				ItemButtons[i].Rect.Height.Mode = RelativeSize.SizeModes.Raw;

				ItemButtons[i].Rect.AnchorLocation = OriginLocation.UpperLeft;

				ItemButtons[i].Show = i < ItemList.Count;

				if (ItemButtons[i].Show)
				{
					int itemIndex = ScrollbarTop + i;
					ItemButtons[i].SetText(ItemList[itemIndex].Text);

					if (itemIndex == SelectedIndex)
						ItemButtons[i].Check();
					else
						ItemButtons[i].UnCheck();
				}

				ItemButtons[i].Resize((int)pixelSize.X, (int)pixelSize.Y);
				buttonY -= desiredSize;
			}

			// position the scrollbar
			ScrollBar.Rect.Width.Paramater = scrollWidth;
			ScrollBar.Show = showScrool;
			DownButton.Show = showScrool;
			UpButton.Show = showScrool;
			ThumbButton.Show = showScrool;

			ScrollBar.Resize((int)pixelSize.X, (int)pixelSize.Y);
			SetThumbPos();

			base.Resize(x, y);

			Resizing = false;
		}

		private void ListButton_ButtonCheckChanged(object sender, UIButton e)
		{
			if (Resizing || e == null || e.Tag == null || !e.IsChecked())
				return;

			int slot = (int)e.Tag;

			int index = slot + ScrollbarTop;
			for (int i = 0; i < ItemButtons.Count; i++)
			{
				if (i != slot)
					ItemButtons[i].UnCheck();
			}

			SelectedIndex = index;
			SelectedIndexChanged?.Invoke(this, this);
		}
	}
}
