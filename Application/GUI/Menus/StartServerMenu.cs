using System;
using System.Collections.Generic;
using System.Drawing;

using LudicrousElectron.Engine;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;

using LunarLambda.API;
using LunarLambda.GUI.Menus.Controls;
using LunarLambda.Data;
using LunarLambda.Preferences;
using LunarLambda.Host.Game;
using LunarLambda.Client.Ship;

namespace LunarLambda.GUI.Menus
{
	public class StartServerMenu : MenuCommon
	{
        protected LayoutContainer[] Columns = new LayoutContainer[] { null, null };

		protected ServerStartupInfo StartupInfo = new ServerStartupInfo();

		protected TextArea SecenarioText = null;
		protected TextArea VariationText = null;
		protected SpinSelector VariationList = null;

		protected override void SetupControls()
		{
			Events.CallGetDefaultServerInfo(StartupInfo);
			SetupBackground(0);
			SetupBackButton(1);

            int layerIndex = 2;
            layerIndex = SetupServerConfig(layerIndex);
            layerIndex = SetupScenarioList(layerIndex);

            SetupStartServerButton(layerIndex++);

            AddAPIButtons(Name);
			base.SetupControls();
		}

        protected int SetupScenarioList(int layerIndex)
        {
            // right side group
            RelativeRect rect = new RelativeRect(RelativeLoc.XRightBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.TwoColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperRight);

            GridLayoutGroup scenarioGrid = new GridLayoutGroup(rect, 15, 2);

            Columns[1] = scenarioGrid;

            scenarioGrid.MaxChildSize = MenuCommon.ButtonHeight.Paramater;

            scenarioGrid.SetColSpan(0, 1);
            scenarioGrid.SetColSpan(1, 6);
            scenarioGrid.SetColSpan(7, 4);
            scenarioGrid.SetColSpan(12, 3);

            // Scenario header
            scenarioGrid.AddChild(new Header(new RelativeRect(), MenuRes.Scenario));

            ButtonScrollList scenarioList = new ButtonScrollList(RelativeRect.Full);
            scenarioList.DesiredRows = 6;

            foreach (var scenario in Scenarios.GetScenarioList())
                scenarioList.AddItem(scenario.Name, scenario);

            scenarioList.SelectedIndexChanged += ScenarioList_SelectedIndexChanged;
            scenarioList.FillMode = UIFillModes.Stretch4Quad;
            scenarioGrid.AddChild(scenarioList);


            SecenarioText = new TextArea(RelativeRect.Full, string.Empty, MenuManager.MainFont, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            SecenarioText.DefaultMaterial.Color = Color.Gray;
            SecenarioText.DesiredRows = 8;
            SecenarioText.BorderPadding = 4;
            SecenarioText.MiniumElementHeight = 20;

            scenarioGrid.AddChild(SecenarioText);

            // sensor scan complexity
            // get data from selected scenario
            scenarioGrid.AddChild(MakeGridLabel(MenuRes.Variation));
            VariationList = new SpinSelector(new RelativeRect(), MenuRes.DefaultVariation.Split(";".ToCharArray()), 0);
            VariationList.ValueChanged += ScenarioVariation_ValueChanged;
            scenarioGrid.AddChild(VariationList);

            // replace with variation info
            VariationText = new TextArea(RelativeRect.Full, string.Empty, MenuManager.MainFont, ThemeManager.GetThemeAsset("ui/TextEntryBackground.png"));
            VariationText.DefaultMaterial.Color = Color.Gray;
            VariationText.DesiredRows = 6;
            VariationText.BorderPadding = 4;
            VariationText.MiniumElementHeight = 20;
            scenarioGrid.AddChild(VariationText);

            AddElement(Columns[1], layerIndex + 1);

            scenarioList.SetSelectedIndex(0);

            return layerIndex + 1;
        }

        protected int SetupServerConfig(int layerIndex)
        {
            RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YUpper + RelativeLoc.BorderOffset, RelativeSize.TwoColumnWidth, RelativeSize.SevenEightsHeight, OriginLocation.UpperLeft);

            GridLayoutGroup serverSetupGrid = new GridLayoutGroup(rect, 15, 2);

            Columns[0] = serverSetupGrid;

            serverSetupGrid.MaxChildSize = MenuCommon.ButtonHeight.Paramater;

            serverSetupGrid.SetColSpan(0);
            serverSetupGrid.SetColSpan(6);
            serverSetupGrid.SetColSpan(9);
            serverSetupGrid.SetColSpan(11);

			// server config header
            serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.ServerConfiguration));

			// name
            serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerName));
            var te = new MenuTextEntry(RelativeRect.FullLeft, StartupInfo.Name);
			te.TextChanged += ServerName_TextChanged;
			serverSetupGrid.AddChild(te);

			// password
			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerPassword));
			te = new MenuTextEntry(RelativeRect.FullLeft, StartupInfo.Password);
			te.TextChanged += Password_TextChanged;
			serverSetupGrid.AddChild(te);

			// ip address
			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerLanIP));
			var ip = MakeGridLabel(Core.GetLocalIPString());
			ip.Rect.AnchorLocation = OriginLocation.MiddleLeft;
			serverSetupGrid.AddChild(ip);

            serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerWANIP));
            string wan = Core.GetWANIPString();
            if(PreferencesManager.GetValueB(PrefNames.PublicHostName))
                wan = PreferencesManager.Get(PrefNames.PublicHostName);
            var wanIP = MakeGridLabel(wan);
            wanIP.Rect.AnchorLocation = OriginLocation.MiddleLeft;
            serverSetupGrid.AddChild(wanIP);


            // public visibility (should this be a checkbox?)
            serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerVis));
			SpinSelector selector = new SpinSelector(new RelativeRect(), MenuRes.ServerVisModes.Split(";".ToCharArray()), StartupInfo.Public ? 1 : 0);
			selector.ValueChanged += ServerVis_ValueChanged;
			serverSetupGrid.AddChild(selector);

			// ship options section
			serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.PlayerShipOptions));

			// FTL Drive type
			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.FTLType));
			selector = new SpinSelector(new RelativeRect(), MenuRes.FTLTypes.Split(";".ToCharArray()), (int)StartupInfo.FTL);
			selector.ValueChanged += FTLType_ValueChanged;
			serverSetupGrid.AddChild(selector);

			// sensor range
			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.RadarRange));
			List<string> radarRanges = new List<string>();
			for (int i = 10; i <= 50; i += 5)
				radarRanges.Add(i.ToString() + "u");

			selector = new SpinSelector(new RelativeRect(), radarRanges, ((int)StartupInfo.SensorRange/5) - 2);
			selector.ValueChanged += RadarRange_ValueChanged;
			serverSetupGrid.AddChild(selector);

			// main screen display options (why are these not dynamic?)
			serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.MainScreenOptions));

			// tactical radar button
			var cb = new MenuCheckButton(RelativeRect.Full, MenuRes.TacticalRadar);
			cb.ButtonCheckChanged += TacRadar_CheckChanged;
			if (StartupInfo.TacticalRadarMSD)
				cb.Check();
			serverSetupGrid.AddChild(cb);

			// long range radar button
			cb = new MenuCheckButton(RelativeRect.Full, MenuRes.LongRangeRadar);
			cb.ButtonCheckChanged += LRRadar_CheckChanged;
			if (StartupInfo.LongRangeRadarMSD)
				cb.Check();
			serverSetupGrid.AddChild(cb);

			// game rules section
			serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.GameRules));

			// sensor scan complexity
			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ScanComplexity));
			selector = new SpinSelector(new RelativeRect(), MenuRes.ScanTypes.Split(";".ToCharArray()), (int)StartupInfo.Scans);
			selector.ValueChanged += Scan_ValueChanged;

			serverSetupGrid.AddChild(selector);

			// use weapon frequencies
			cb = new MenuCheckButton(RelativeRect.Full, MenuRes.WeaponFrequencies);
			cb.ButtonCheckChanged += WeaponFreq_CheckChanged;
			if (StartupInfo.UseWeaponFrequencies)
				cb.Check();
			serverSetupGrid.AddChild(cb);

			// use individual system damage
			cb = new MenuCheckButton(RelativeRect.Full, MenuRes.PerSystemDamage);
			cb.ButtonCheckChanged += SystemDamage_CheckChanged;
			if (StartupInfo.UseSystemDamage)
				cb.Check();
			serverSetupGrid.AddChild(cb);

			// add the left side group to the main layout
			AddElement(Columns[0], layerIndex);

            return layerIndex + 1;
        }

		private void ScenarioList_SelectedIndexChanged(object sender, ButtonScrollList e)
		{
			if (e.SelectedIndex < 0)
				return;

			StartupInfo.SelectedScenario = Scenarios.GetScenarioList()[e.SelectedIndex];
			StartupInfo.SelectedVariation = null;

			API.Events.CallGetServerInfoForScenario(StartupInfo);

			SecenarioText.SetText(StartupInfo.SelectedScenario.Description);

			List<string> names = new List<string>();
			names.Add(MenuRes.NoVariatioName);
			foreach (var vars in StartupInfo.SelectedScenario.Variations)
				names.Add(vars.DisplayName);

			VariationList.SetLabels(names);
			VariationList.SetSelectedIndex(0);
		}

		private void ScenarioVariation_ValueChanged(object sender, SpinSelector e)
		{
			if (e.SelectedIndex == 0)
			{
				StartupInfo.SelectedVariation = null;
				VariationText.SetText(MenuRes.NoVariationDescription);
			}
			else
			{
				StartupInfo.SelectedVariation = StartupInfo.SelectedScenario.Variations[e.SelectedIndex - 1];
				VariationText.SetText(StartupInfo.SelectedVariation.Description);
			}
		}

		private void ServerName_TextChanged(object sender, UITextEntry e)
		{
			StartupInfo.Name = e.GetCurrentText();
		}

		private void Password_TextChanged(object sender, UITextEntry e)
		{
			StartupInfo.Password = e.GetCurrentText();
		}

		private void ServerVis_ValueChanged(object sender, SpinSelector e)
		{
			StartupInfo.Public = e.SelectedIndex != 0;
		}

		private void FTLType_ValueChanged(object sender, SpinSelector e)
		{
			StartupInfo.FTL = (ServerStartupInfo.FTLSettings)e.SelectedIndex;
		}

		private void RadarRange_ValueChanged(object sender, SpinSelector e)
		{
			StartupInfo.SensorRange = ((e.SelectedIndex + 2) * 5);
		}

		private void TacRadar_CheckChanged(object sender, UIButton e)
		{
			StartupInfo.TacticalRadarMSD = e.IsChecked();
		}

		private void LRRadar_CheckChanged(object sender, UIButton e)
		{
			StartupInfo.LongRangeRadarMSD = e.IsChecked();
		}

		private void Scan_ValueChanged(object sender, SpinSelector e)
		{
			StartupInfo.Scans = (ServerStartupInfo.ScanSettings)e.SelectedIndex;
		}

		private void WeaponFreq_CheckChanged(object sender, UIButton e)
		{
			StartupInfo.UseWeaponFrequencies = e.IsChecked();
		}

		private void SystemDamage_CheckChanged(object sender, UIButton e)
		{
			StartupInfo.UseSystemDamage = e.IsChecked();
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
            StartupInfo.ServerLANAddress = Core.GetLocalIPString();
            if (PreferencesManager.GetValueB(PrefNames.PublicHostName))
                StartupInfo.ServerWANHost = PreferencesManager.Get(PrefNames.PublicHostName);
            else
                StartupInfo.ServerWANHost = Core.GetWANIPString();

            if (GameHost.ActiveGameHost != null)
                GameHost.ActiveGameHost.Shutdown();

            GameHost.StartGame(StartupInfo);
            ShipClient.ActiveShipClient = new ShipClient("localhost", StartupInfo.Port);

            MenuManager.ReplaceAndPushMenu(MenuAPI.GameStatusMenu, MenuAPI.JoinGameMenuName);
        }
    }
}
