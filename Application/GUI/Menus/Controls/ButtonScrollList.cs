using System;
using System.Collections.Generic;
using System.Drawing;
using LudicrousElectron.Engine.Input;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using OpenTK;

namespace LunarLambda.GUI.Menus.Controls
{
	public class ButtonScrollList : ControlScrollList
	{
		public int SelectedIndex { get; protected set; } = - 1;
		public event EventHandler<ButtonScrollList> SelectedIndexChanged = null;

		public ButtonScrollList(RelativeRect rect, int selectedIndex = -1, string texture = null) : base(rect, texture)
		{
			NoDraw = texture == null;
			SelectedIndex = selectedIndex;
		}

		public void SetSelectedIndex(int index)
		{
			SelectedIndex = index;
			if (Inited)
				ForceRefresh();

			SelectedIndexChanged?.Invoke(this, this);
		}

		protected override GUIElement GetElement(RelativeRect rect)
		{
			var btn = new MenuCheckButton(rect);
			btn.ButtonCheckChanged += ListButton_ButtonCheckChanged;
			return btn;
		}

		protected override void SetupElement(GUIElement element, int index, string text)
		{
			MenuCheckButton btn = element as MenuCheckButton;
			if (btn == null)
				return;

			btn.SetText(text);
			if (index == SelectedIndex)
				btn.Check();
			else
				btn.UnCheck();
		}

		private void ListButton_ButtonCheckChanged(object sender, UIButton e)
		{
			if (Resizing || e == null || e.Tag == null || !e.IsChecked())
				return;

			int slot = (int)e.Tag;

			int index = slot + ScrollbarTop;
			for (int i = 0; i < ItemControls.Count; i++)
			{
				if (i != slot && ItemControls[i] as UIButton != null)
					(ItemControls[i] as UIButton).UnCheck();
			}

			SelectedIndex = index;
			SelectedIndexChanged?.Invoke(this, this);
		}
	}
}
