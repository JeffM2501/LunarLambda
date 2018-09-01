using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

using LudicrousElectron.GUI;
using LudicrousElectron.Engine;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.GUI.Config;
using LunarLambda.GUI.Menus.Controls;
using LudicrousElectron.Engine.Graphics.Textures;
using LunarLambda.API;
using LunarLambda.Host.Game;

namespace LunarLambda.GUI.Menus
{
    public class GameStatusMenu : MenuCommon
    {
        internal GameStatusMenu() : base()
        {

        }

        protected override void SetupControls()
        {
            int layerIndex = 0;
            SetupBackground(layerIndex++);
            SetupBackButton(layerIndex++);

            RelativeRect rect = new RelativeRect(RelativeLoc.XRightBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerRight);

            MenuButton join = new MenuButton(rect, MenuRes.JoinGameInProgress);
            join.Clicked += Join_Clicked;
            AddElement(join, layerIndex);


            RelativeRect centerRect = new RelativeRect(RelativeLoc.XCenter, RelativeLoc.YCenter, RelativeSize.HalfWidth, RelativeSize.HalfHeight, OriginLocation.Center);

            UILabel label = new UILabel(MenuManager.MainFont, "Online...",centerRect);
            AddElement(label, layerIndex);

            AddAPIButtons(MenuAPI.GameStatusMenu);
            base.SetupControls();
        }

        protected override void Back_Clicked(object sender, UIButton e)
        {
            GameHost.StopGame();
            base.Back_Clicked(sender, e);
        }

        private void Join_Clicked(object sender, UIButton e)
        {
            MenuManager.PushMenu(MenuAPI.JoinGameMenuName);
        }
    }
}
