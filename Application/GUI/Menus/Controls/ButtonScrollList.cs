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

		protected VSlider VScrollbar = null;

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
			NoDraw = true;

			SelectedIndex = selectedIndex;
			IgnoreMouse = false;
			if (string.IsNullOrEmpty(texture))
				Hide();

			SetupButtons();
		}

		protected virtual void SetupButtons()
		{
			VScrollbar = new VSlider(new RelativeRect(), 0);
			VScrollbar.Rect.X = RelativeLoc.XRight;
			VScrollbar.Rect.Y = RelativeLoc.YUpper;
			VScrollbar.Rect.Width.Mode = RelativeSize.SizeModes.Raw;
			VScrollbar.Rect.Height = RelativeSize.FullHeight;
			VScrollbar.Rect.AnchorLocation = OriginLocation.UpperRight;
			AddChild(VScrollbar);
			VScrollbar.ValueChanged += VScrollbar_ValueChanged;
		}

		private void VScrollbar_ValueChanged(object sender, BaseSlider e)
		{
			ScrollbarTop = e.CurrentValue;
			ForceRefresh();
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
					newButton.Hide();
					newButton.ButtonCheckChanged += ListButton_ButtonCheckChanged;
					ItemButtons.Add(newButton);
					AddChild(newButton);
				}

				VScrollbar.MinValue = 0;
				VScrollbar.MaxValue = ItemList.Count - rowCount;
				VScrollbar.ResetScroll(false);
			}

			VisibleButtonCount = rowCount;

			bool showScrool = ItemList.Count > VisibleButtonCount;

			float scrollWidth = desiredSize;
			float buttonWidth = pixelSize.X;
			if (showScrool)
				buttonWidth -= scrollWidth - CellPadding;

			// set the positions and visibility

			float buttonY = pixelSize.Y;

			for (int i = 0; i < ItemButtons.Count; i++)
			{
				ItemButtons[i].Rect.X.Paramater = 0;
				ItemButtons[i].Rect.X.RelativeTo = RelativeLoc.Edge.Raw;

				ItemButtons[i].Rect.Y.Paramater = buttonY;
				ItemButtons[i].Rect.Y.RelativeTo = RelativeLoc.Edge.Raw;

				ItemButtons[i].Rect.Width.Paramater = buttonWidth;
				ItemButtons[i].Rect.Width.Mode = RelativeSize.SizeModes.Raw;

				ItemButtons[i].Rect.Height.Paramater = desiredSize;
				ItemButtons[i].Rect.Height.Mode = RelativeSize.SizeModes.Raw;

				ItemButtons[i].Rect.AnchorLocation = OriginLocation.UpperLeft;

				ItemButtons[i].SetVisibility(i < ItemList.Count);

				if (ItemButtons[i].Shown)
				{
					int itemIndex = ScrollbarTop + i;
					ItemButtons[i].SetText(ItemList[itemIndex].Text);

					if (itemIndex == SelectedIndex)
						ItemButtons[i].Check();
					else
						ItemButtons[i].UnCheck();
				}

				ItemButtons[i].Resize(pixelSize);
				buttonY -= desiredSize;
			}

			VScrollbar.Rect.Width.Paramater = scrollWidth;
			VScrollbar.SetVisibility(showScrool);
			VScrollbar.Resize(pixelSize);

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
