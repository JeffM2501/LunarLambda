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
using LunarLambda.Messges.Ship.Game;
using System.Text;

namespace LunarLambda.GUI.Menus
{
    public class JoinGameMenu : MenuCommon
    {
        protected LayoutContainer[] Columns = new LayoutContainer[] { null, null, null };

        protected ButtonScrollList NewShipList = null;
        protected ButtonScrollList ActiveShipList = null;

        protected UILabel ShipNameLabel = null;
        protected UILabel ShipTypeLabel = null;
        protected TextArea ShipStatsTextArea = null;

        protected UpdateShipList.ShipInfo DefaultShipInfo = null;

        internal JoinGameMenu() : base()
        {
            DefaultShipInfo = new UpdateShipList.ShipInfo();
            DefaultShipInfo.Name = MenuRes.DefaultShipSelection;
            DefaultShipInfo.TypeName = MenuRes.DefaultShipTypeLabel;
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
            if (ActiveShipList == null || NewShipList == null)
                return;

            ActiveShipList.ClearItems();
            NewShipList.ClearItems();
            NewShipList.AddItem(MenuRes.DefaultShipSelection, DefaultShipInfo);

            // load the ship list
            foreach (var ship in e.Ships)
            {
                if (ship.Spawned)
                    ActiveShipList.AddItem(ship.Name,ship);
                else
                    NewShipList.AddItem(ship.Name, ship);
            }

            NewShipList.SetSelectedIndex(0);

			NewShipList.ForceRefresh();
			ActiveShipList.ForceRefresh();

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

            ActiveShipList = new ButtonScrollList(RelativeRect.Full, -1, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            ActiveShipList.DesiredRows = 8;


            ActiveShipList.FillMode = UIFillModes.Stretch4Quad;
            shipList.AddChild(ActiveShipList);

           
            shipList.AddChild(new Header(new RelativeRect(), MenuRes.NewShipHeader));

            NewShipList = new ButtonScrollList(RelativeRect.Full, -1, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            NewShipList.DesiredRows = 8;

            NewShipList.AddItem(MenuRes.DefaultShipSelection, DefaultShipInfo);

            NewShipList.FillMode = UIFillModes.Stretch4Quad;
            shipList.AddChild(NewShipList);


            AddElement(Columns[0], layerIndex + 1);

            NewShipList.SelectedIndexChanged += NewShipList_SelectedIndexChanged;

            NewShipList.SetSelectedIndex(0);

            return layerIndex + 1;
        }

        private void NewShipList_SelectedIndexChanged(object sender, ButtonScrollList e)
        {
            if (ShipNameLabel == null)
                return;

            if (e.SelectedItemTag as UpdateShipList.ShipInfo == null)
            {
                ShipNameLabel.Text = MenuRes.ShipNameLabel;
                ShipTypeLabel.Text = MenuRes.ShipTypeLabel;
                ShipStatsTextArea.SetText(string.Empty);
            }
            else
            {
                UpdateShipList.ShipInfo ship = e.SelectedItemTag as UpdateShipList.ShipInfo;
                ShipNameLabel.Text = MenuRes.ShipNameLabel + ship.Name;
                ShipTypeLabel.Text = MenuRes.ShipTypeLabel + ship.TypeName;

                StringBuilder sb = new StringBuilder();
                foreach (var stat in ship.Stats)
                    sb.AppendLine(stat.Item1 + " : " + stat.Item2);

                ShipStatsTextArea.SetText(sb.ToString());
            }
            ShipTypeLabel.ForceRefresh();
            ShipNameLabel.ForceRefresh();
        }

        protected int SetupShipInfo(int layerIndex)
        {
            // right side group
            RelativeRect rect = new RelativeRect(RelativeLoc.XCenter, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.ThreeColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperCenter);

            GridLayoutGroup shipInfo = new GridLayoutGroup(rect, 15, 1);

            Columns[1] = shipInfo;

            shipInfo.MaxChildSize = ButtonHeight.Paramater;

            shipInfo.SetColSpan(4, 5);

            // Scenario header
            shipInfo.AddChild(new Header(new RelativeRect(), MenuRes.ShipInfoHeader));

            ShipNameLabel =  MakeGridLabel(MenuRes.ShipNameLabel, true);
            shipInfo.AddChild(ShipNameLabel);

            ShipTypeLabel = MakeGridLabel(MenuRes.ShipTypeLabel, true);
            shipInfo.AddChild(ShipTypeLabel);

            shipInfo.AddChild(new Header(new RelativeRect(), MenuRes.ShipStatsLabel));

            ShipStatsTextArea = new TextArea(RelativeRect.Full, string.Empty, MenuManager.MainFont, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            ShipStatsTextArea.DefaultMaterial.Color = Color.Gray;
            ShipStatsTextArea.DesiredRows = 9;
            ShipStatsTextArea.BorderPadding = 4;
            ShipStatsTextArea.MiniumElementHeight = 12;

            shipInfo.AddChild(ShipStatsTextArea);

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
