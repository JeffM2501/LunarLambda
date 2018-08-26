using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LudicrousElectron.GUI.Text;

namespace LunarLambda.GUI.Menus.Controls
{
	public class TextArea : ControlScrollList
	{
		public int Font = -1;
		public Color TextColor = Color.White;
		protected string CurrentText = string.Empty;

		public TextArea(RelativeRect rect, string text, int font = -1, string backgroundTexture = null) : base(rect, backgroundTexture)
		{
			Font = font;
			IgnoreMouse = false;
			CurrentText = text;
			LeaveRoomForScroll = true;
			BorderPadding = 5;

			MiniumScrollSize = 45;
		}

		public void SetText(string text)
		{
			CurrentText = text;
			if (Inited)
				ForceRefresh();
		}

		protected override GUIElement GetElement(RelativeRect rect)
		{
			return new UILabel(Font, "XXX", rect, TextColor, UILabel.TextFittingModes.ByHeightTrim);
		}

		protected override void SetupElement(GUIElement element, int index, string text)
		{
			UILabel lbl = element as UILabel;
			if (lbl == null)
				return;

			lbl.Text = text;
		}

		protected override void CheckItemCount()
		{
			base.CheckItemCount();
			Paginate();
		}

		protected virtual void Paginate()
		{
			if (ItemControls.Count == 0)
				return;
			UILabel lbl = ItemControls[0] as UILabel;
			if (lbl == null)
				return;

			float pixelWidth = Rect.GetPixelSize().X - ScrollWidth - (BorderPadding*2); // we can't ask the label, since it may not have max text in it.

			int fontSize = lbl.GetNominalFontHeight(ActualItemHeight);

			string[] lines = CurrentText.Replace("\n", string.Empty).Split("\r".ToCharArray());

			ItemList.Clear();
			foreach (var line in lines)
			{
				string text = line;
				while (text.Length > 0)
				{
					string thisText = string.Empty;
					var size = FontManager.MeasureText(Font, fontSize, text);
					if (size.X <= pixelWidth)
						thisText = text;
					else
					{
						thisText = text;
						bool done = false;
						while (!done)
						{
							int prevWhitespace = thisText.LastIndexOfAny(" \t".ToCharArray());
							if (prevWhitespace == -1)
							{
								done = true;
								// the word is too big, let it clip
							}
							else
							{
								if (prevWhitespace == 0)
									done = true;
								else
								{
									thisText = thisText.Substring(0, prevWhitespace);

									size = FontManager.MeasureText(Font, fontSize, thisText);
									done = size.X < (pixelWidth);
								}
							}
						}
					}
					thisText = thisText.Trim();

					if (thisText != string.Empty)
					{
						ControlInfo info = new ControlInfo();
						info.Text = thisText;
						info.Index = ItemList.Count;
						ItemList.Add(info);
					}

					if (thisText == text)
						text = string.Empty;
					else
						text = text.Substring(thisText.Length).Trim();
				}
			}
		}
	}
}
