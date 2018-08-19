﻿using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LudicrousElectron.Engine.Graphics.Textures;

using LunarLambda.API;
using LunarLambda.GUI.Menus.Controls;
using LunarLambda.GUI.Config;


namespace LunarLambda.GUI.Menus
{
	public class MenuCommon : Menu
	{
		public static RelativeSize ButtonWidth = new RelativeSize(1.0f / 4.0f, true);
		public static RelativeSize ButtonHeight = new RelativeSize(45);
		public static RelativeSize ButtonSpacing = new RelativeSize(5);

		protected bool Setup = false;
		public override void Activate()
		{
			base.Activate();

			if (!Setup)
				SetupControls();

		}

		protected virtual void SetupControls()
		{
			Setup = true;
		}

		public virtual LayoutContainer GetContainerForAPIButton(int row, int col)
		{
			return null;
		}

		public virtual UIButton GetAPIButton(RelativeRect rect, string label)
		{
			return new MenuButton(rect, label);
		}

		protected virtual void RegisterButton(MenuAPI.MenuAPIEventArgs buttonInfo)
		{
			LayoutContainer container = GetContainerForAPIButton(buttonInfo.Row, buttonInfo.Col);
			if (container == null)
				return;

			int col = buttonInfo.Col;
			if (col < 0)
				col = 0;
			if (col > 1)
				col = 1;

			RelativeRect rect = new RelativeRect();
			rect.Width = container.Rect.Width.Clone();
			buttonInfo.Button = GetAPIButton(rect, buttonInfo.MenuName);
			container.AddChild(buttonInfo.Button);
		}

		public virtual void AddAPIButtons(string name)
		{
			foreach (var buttonInfo in MenuAPI.GetAPIButtons(name))
				RegisterButton(buttonInfo);
		}

		protected virtual void SetupBackground(int layerIndex)
		{
			string bgRepeat = "ui/BackgroundCrosses.png";

			TextureManager.GetTexture(bgRepeat).SetTextureFormat(TextureInfo.TextureFormats.TextureMap); // force this to repeat

			var background = new UIPanel(RelativeRect.Full, ColorConfig.background.Color);
			background.Children.Add(new UIPanel(RelativeRect.Full, Color.White, bgRepeat));
			AddElement(background, layerIndex);
		}

		protected virtual void SetupBackButton(int layerIndex)
		{
			RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerLeft);

			MenuButton back = new MenuButton(rect, MenuRes.Back);
			back.Clicked += Back_Clicked;
			AddElement(back, layerIndex);
		}

		protected virtual void Back_Clicked(object sender, UIButton e)
		{
			MenuManager.PopMenu();
		}
	}
}