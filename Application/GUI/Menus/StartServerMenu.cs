using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LunarLambda.GUI.Menus.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.GUI.Menus
{
	public class StartServerMenu : MenuCommon
	{
        protected LayoutContainer[] Columns = new LayoutContainer[] { null, null };

        protected override void SetupControls()
		{
			SetupBackground(0);
			SetupBackButton(1);

            int layerIndex = 2;
            layerIndex = SetupServerConfig(layerIndex);

            SetupStartServerButton(layerIndex++);

            AddAPIButtons(Name);
			base.SetupControls();
		}

        protected int SetupServerConfig(int layerIndex)
        {
            RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.TwoColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperLeft);

            GridLayoutGroup serverSetupGrid = new GridLayoutGroup(rect, 15, 2);

            Columns[0] = serverSetupGrid;

            serverSetupGrid.MaxChildSize = MenuCommon.ButtonHeight.Paramater;

            serverSetupGrid.SetColSpan(0);
            serverSetupGrid.SetColSpan(5);
            serverSetupGrid.SetColSpan(8);
            serverSetupGrid.SetColSpan(10);

            serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.ServerConfiguration));
            var label = new UILabel(MenuManager.MainFont, "Server Name:", RelativeRect.FullRight, UILabel.TextFittingModes.ByHeightTrim);
            label.MaxTextSize = (int)(MenuCommon.ButtonHeight.Paramater * 0.5f);

            serverSetupGrid.AddChild(label);
            serverSetupGrid.AddChild(new MenuButton(RelativeRect.FullLeft,"Test button"));

            // config header
            AddElement(Columns[0], layerIndex);

            rect = new RelativeRect(RelativeLoc.XRightBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.TwoColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperRight);

            GridLayoutGroup scenarioGrid = new GridLayoutGroup(rect, 15, 2);

            Columns[1] = scenarioGrid;

            scenarioGrid.MaxChildSize = MenuCommon.ButtonHeight.Paramater;

            scenarioGrid.SetColSpan(0);

            // 15 total slots
            // config header
            scenarioGrid.AddChild(new Header(new RelativeRect(), MenuRes.Scenario));

            AddElement(Columns[1], layerIndex+1);


            return layerIndex + 2;
        }

        protected virtual void SetupStartServerButton(int layerIndex)
        {
            RelativeRect rect = new RelativeRect(RelativeLoc.XRightBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, ButtonWidth, ButtonHeight, OriginLocation.LowerRight);

            MenuButton start = new MenuButton(rect, MenuRes.StartScenario);
            start.Clicked += Start_Clicked;
            AddElement(start, layerIndex);
        }

        protected void Start_Clicked (object server, EventArgs e)
        {
            // call start server
        }
    }
}
