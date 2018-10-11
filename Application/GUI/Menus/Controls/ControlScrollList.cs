using System;
using System.Collections.Generic;
using LudicrousElectron.Engine.Input;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using OpenTK;

namespace LunarLambda.GUI.Menus.Controls
{
    public abstract class ControlScrollList : UIPanel
	{
		protected List<GUIElement> ItemControls = new List<GUIElement>();

		protected VSlider VScrollbar = null;

		protected bool LeaveRoomForScroll = false;

		public int DesiredRows = 6;
		public float MiniumElementHeight = 45;
		public float MiniumScrollSize = -1;
		public float MaxiumScrollSize = -1;

		public float BorderPadding = 0;

		protected class ControlInfo
		{
			public int Index = 0;
			public string Text = string.Empty;
			public object Tag = null;
		}

		protected List<ControlInfo> ItemList = new List<ControlInfo>();
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

		public int ScrollbarTop { get; protected set; } = 0;
		protected int VisibleButtonCount = 0;

		protected ControlScrollList(RelativeRect rect, string texture = null) : base(rect, texture)
		{
			NoDraw = true;
			IgnoreMouse = false;
			FillMode = UIFillModes.SmartStprite;
			NoDraw = string.IsNullOrEmpty(texture);

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
			VScrollbar.UseWheelInput = false;
			AddChild(VScrollbar);
			VScrollbar.ValueChanged += VScrollbar_ValueChanged;
		}

		private void VScrollbar_ValueChanged(object sender, BaseSlider e)
		{
			ScrollbarTop = e.CurrentValue;
			ForceRefresh();
		}

		protected abstract GUIElement GetElement(RelativeRect rect);
		protected abstract void SetupElement(GUIElement element, int index, string text);

		public int AddItem(string text, object tag = null)
		{
			ControlInfo info = new ControlInfo();
			info.Index = ItemList.Count;
			info.Text = text;
			info.Tag = tag;

			ItemList.Add(info);

			if (Inited)
				ForceRefresh();

			return info.Index;
		}

        public void ClearItems()
        {
            ItemList.Clear();

            if (Inited)
                ForceRefresh();
        }

        protected virtual void CheckItemCount()
		{
			// by default we assume the item list is good, but text areas may need to reflow.
		}

		protected bool Resizing = false;
		protected float ScrollWidth = 0;
		protected float ActualItemHeight = 0;
		protected float ActualItemWidth = 0;

		public override void Resize(int x, int y)
		{
			Resizing = true;

			// see how much room we have to work with
			Rect.Resize(x, y);

			Vector2 pixelSize = Rect.GetPixelSize();
			float avalableHeight = pixelSize.Y - (BorderPadding * 2);

			int rowCount = DesiredRows;
			ActualItemHeight = avalableHeight / DesiredRows;

			if (MiniumElementHeight > 0 && ActualItemHeight < MiniumElementHeight)
			{
				ActualItemHeight = MiniumElementHeight;
				rowCount = (int)(avalableHeight / ActualItemHeight);
			}

			if (ItemControls.Count != rowCount)
			{
				while (ItemControls.Count > rowCount)
				{
					RemoveChild(ItemControls[ItemControls.Count - 1]);
					ItemControls.RemoveAt(ItemControls.Count - 1);
				}
				while (ItemControls.Count < rowCount)
				{
					GUIElement newItem = GetElement(new RelativeRect());
					newItem.Tag = ItemControls.Count;
					newItem.Hide();
					ItemControls.Add(newItem);
					AddChild(newItem);
				}

				VScrollbar.MinValue = 0;
				VScrollbar.MaxValue = ItemList.Count - rowCount;
				VScrollbar.ResetScroll(false);
			}

			VisibleButtonCount = rowCount;

			ScrollWidth = ActualItemHeight;
			if (MiniumScrollSize > 0 && ScrollWidth < MiniumScrollSize)
				ScrollWidth = MiniumScrollSize;
			if (MaxiumScrollSize > 0 && ScrollWidth > MaxiumScrollSize)
				ScrollWidth = MaxiumScrollSize;

			CheckItemCount();

			bool showScrool = ItemList.Count > VisibleButtonCount;

			ActualItemWidth = pixelSize.X - (BorderPadding *2);
			if (showScrool || LeaveRoomForScroll)
				ActualItemWidth -= ScrollWidth - BorderPadding;

			// set the positions and visibility

			float buttonY = pixelSize.Y - BorderPadding;

			for (int i = 0; i < ItemControls.Count; i++)
			{
				ItemControls[i].Rect.X.Paramater = BorderPadding;
				ItemControls[i].Rect.X.RelativeTo = RelativeLoc.Edge.Raw;

				ItemControls[i].Rect.Y.Paramater = buttonY;
				ItemControls[i].Rect.Y.RelativeTo = RelativeLoc.Edge.Raw;

				ItemControls[i].Rect.Width.Paramater = ActualItemWidth;
				ItemControls[i].Rect.Width.Mode = RelativeSize.SizeModes.Raw;

				ItemControls[i].Rect.Height.Paramater = ActualItemHeight;
				ItemControls[i].Rect.Height.Mode = RelativeSize.SizeModes.Raw;

				ItemControls[i].Rect.AnchorLocation = OriginLocation.UpperLeft;

				ItemControls[i].SetVisibility(i < ItemList.Count);

				if (ItemControls[i].Shown)
				{
					int itemIndex = ScrollbarTop + i;
					SetupElement(ItemControls[i], itemIndex, ItemList[itemIndex].Text);
				}

				ItemControls[i].Resize(pixelSize);
				buttonY -= ActualItemHeight;
			}

			VScrollbar.Rect.Width.Paramater = ScrollWidth;
			VScrollbar.SetVisibility(showScrool);
			VScrollbar.Resize(pixelSize);

			base.Resize(x, y);

			Resizing = false;
		}

		public override void ProcessMouseEvent(Vector2 location, InputManager.LogicalButtonState buttons)
		{
			int wheelAbs = Math.Abs(buttons.WheelTick);
			if (wheelAbs > 0)
			{
				if (buttons.WheelTick > 0)
					VScrollbar.Retreat(Math.Abs(wheelAbs));
				else
					VScrollbar.Advance(Math.Abs(wheelAbs));
			}

			base.ProcessMouseEvent(location, buttons);
		}
	}
}
