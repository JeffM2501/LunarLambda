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
using LunarLambda.Client.Ship;

namespace LunarLambda.GUI.Menus
{
    public class JoinGameMenu : MenuCommon
    {
        protected LayoutContainer[] Columns = new LayoutContainer[] { null, null, null };

        internal JoinGameMenu() : base()
        {

        }

        public override void Activate()
        {
            base.Activate();

            if (ShipClient.ActiveShipClient == null)
                return;

            ShipClient.ActiveShipClient.ConnectionAccepted += ActiveShipClient_ConnectionAccepted;
            ShipClient.ActiveShipClient.ShipListUpdated += this.ActiveShipClient_ShipListUpdated;

            if (ShipClient.ActiveShipClient.LastConnectResponce != null && ShipClient.ActiveShipClient.LastConnectResponce.Responce == Messges.Ship.Connect.ConnectResponce.ResponceTypes.Accepted)
                ActiveShipClient_ConnectionAccepted(ShipClient.ActiveShipClient, ShipClient.ActiveShipClient.LastConnectResponce);
            if (ShipClient.ActiveShipClient.LastShipList != null && ShipClient.ActiveShipClient.LastConnectResponce.Responce == Messges.Ship.Connect.ConnectResponce.ResponceTypes.Accepted)
                ActiveShipClient_ShipListUpdated(ShipClient.ActiveShipClient, ShipClient.ActiveShipClient.LastShipList);

        }

        private void ActiveShipClient_ConnectionAccepted(object sender, Messges.Ship.Connect.ConnectResponce e)
        {
            // setup the scenario info
        }

        private void ActiveShipClient_ShipListUpdated(object sender, LunarLambda.Messges.Ship.Game.UpdateShipList e)
        {
            // load the ship list
        }

        protected override void SetupControls()
        {
            int layerIndex = 0;
            SetupBackground(layerIndex++);
            SetupBackButton(layerIndex++);

            RelativeRect rect = new RelativeRect(RelativeLoc.XRightBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerRight);

            MenuButton join = new MenuButton(rect, MenuRes.JoinGameInProgress);
            join.Clicked += Join_Clicked;
            AddElement(join, layerIndex++);

            layerIndex = SetupShipList(layerIndex);
            layerIndex = SetupShipInfo(layerIndex);
            layerIndex = SetupConsoleSelector(layerIndex);

            AddAPIButtons(MenuAPI.GameStatusMenu);
            base.SetupControls();
        }

        protected int SetupShipList(int layerIndex)
        {
            // right side group
            RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.ThreeColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperLeft);

            GridLayoutGroup shipList = new GridLayoutGroup(rect, 15, 2);

            Columns[0] = shipList;

            shipList.MaxChildSize = MenuCommon.ButtonHeight.Paramater;

            shipList.SetColSpan(0, 1);
            shipList.SetColSpan(1, 6);
            shipList.SetColSpan(7, 1);
            shipList.SetColSpan(8, 5);

            // Scenario header
            shipList.AddChild(new Header(new RelativeRect(), MenuRes.ShipListHeader));

            ButtonScrollList currentShipList = new ButtonScrollList(RelativeRect.Full, -1, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            currentShipList.DesiredRows = 8;


            currentShipList.FillMode = UIFillModes.Stretch4Quad;
            shipList.AddChild(currentShipList);

           
            shipList.AddChild(new Header(new RelativeRect(), MenuRes.NewShipHeader));

            ButtonScrollList newShipList = new ButtonScrollList(RelativeRect.Full, -1, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            newShipList.DesiredRows = 8;

            newShipList.AddItem(MenuRes.DefaultShipSelection);

            newShipList.FillMode = UIFillModes.Stretch4Quad;
            shipList.AddChild(newShipList);


            AddElement(Columns[0], layerIndex + 1);


            newShipList.SetSelectedIndex(0);

            return layerIndex + 1;
        }

        protected int SetupShipInfo(int layerIndex)
        {
            // right side group
            RelativeRect rect = new RelativeRect(RelativeLoc.XCenter, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.ThreeColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperCenter);

            GridLayoutGroup shipInfo = new GridLayoutGroup(rect, 15, 2);

            Columns[1] = shipInfo;

            shipInfo.MaxChildSize = ButtonHeight.Paramater;

            shipInfo.SetColSpan(0, 1);

            // Scenario header
            shipInfo.AddChild(new Header(new RelativeRect(), MenuRes.ShipInfoHeader));


            AddElement(Columns[1], layerIndex + 1);


            return layerIndex + 1;
        }

        protected int SetupConsoleSelector(int layerIndex)
        {
            // right side group
            RelativeRect rect = new RelativeRect(RelativeLoc.XRightBorder , RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.ThreeColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperRight);

            GridLayoutGroup consoleList = new GridLayoutGroup(rect, 15, 2);

            Columns[2] = consoleList;

            consoleList.MaxChildSize = ButtonHeight.Paramater;

            consoleList.SetColSpan(0, 1);

            // Scenario header
            consoleList.AddChild(new Header(new RelativeRect(), MenuRes.ConsoleSelectorHeader));


            AddElement(Columns[2], layerIndex + 1);


            return layerIndex + 1;
        }


        private void Join_Clicked(object sender, UIButton e)
        {
            MenuManager.PushMenu(MenuAPI.JoinGameMenuName);
        }

        protected override void Back_Clicked(object sender, UIButton e)
        {
            if (ShipClient.ActiveShipClient != null)
            {
                ShipClient.ActiveShipClient.Shutdown();
                ShipClient.ActiveShipClient = null;
            }
            base.Back_Clicked(sender, e);
        }
    }
}
