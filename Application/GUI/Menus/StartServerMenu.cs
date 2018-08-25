using LudicrousElectron.Engine;
using LudicrousElectron.GUI;
using LudicrousElectron.GUI.Elements;
using LudicrousElectron.GUI.Geometry;
using LunarLambda.GUI.Menus.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
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

		protected UILabel MakeGridLabel(string text)
		{
			var label = new UILabel(MenuManager.MainFont, text, RelativeRect.FullRight, UILabel.TextFittingModes.ByHeightTrim);
			label.MaxTextSize = (int)(MenuCommon.ButtonHeight.Paramater * 0.5f);
			return label;
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

            serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerName));
            var te = new MenuTextEntry(RelativeRect.FullLeft, MenuRes.DefaultServerName);
			te.TextChanged += ServerAddress_TextChanged;
			serverSetupGrid.AddChild(te);

			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerPassword));
			te = new MenuTextEntry(RelativeRect.FullLeft, string.Empty);
			te.TextChanged += Password_TextChanged;
			serverSetupGrid.AddChild(te);

			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerIP));
			var ip = MakeGridLabel(Core.GetLocalIPString());
			ip.Rect.AnchorLocation = OriginLocation.MiddleLeft;
			serverSetupGrid.AddChild(ip);

			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ServerVis));
			SpinSelector selector = new SpinSelector(new RelativeRect(), MenuRes.ServerVisModes.Split(";".ToCharArray()), 0);
			selector.ValueChanged += ServerVis_ValueChanged;
			serverSetupGrid.AddChild(selector);

			serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.PlayerShipOptions));

			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.FTLType));
			selector = new SpinSelector(new RelativeRect(), MenuRes.FTLTypes.Split(";".ToCharArray()), 1);
			selector.ValueChanged += FTLType_ValueChanged;
			serverSetupGrid.AddChild(selector);

			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.RadarRange));
			List<string> radarRanges = new List<string>();
			for (int i = 10; i <= 50; i += 5)
				radarRanges.Add(i.ToString() + "u");

			selector = new SpinSelector(new RelativeRect(), radarRanges, 4);
			selector.ValueChanged += RadarRange_ValueChanged;
			serverSetupGrid.AddChild(selector);

			serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.MainScreenOptions));

			var cb = new MenuCheckButton(RelativeRect.Full, MenuRes.TacticalRadar);
			cb.ButtonCheckChanged += TacRadar_CheckChanged;
			cb.Check();
			serverSetupGrid.AddChild(cb);

			cb = new MenuCheckButton(RelativeRect.Full, MenuRes.LongRangeRadar);
			cb.ButtonCheckChanged += LRRadar_CheckChanged;
			cb.Check();
			serverSetupGrid.AddChild(cb);


			serverSetupGrid.AddChild(new Header(new RelativeRect(), MenuRes.GameRules));

			serverSetupGrid.AddChild(MakeGridLabel(MenuRes.ScanComplexity));
			selector = new SpinSelector(new RelativeRect(), MenuRes.ScanTypes.Split(";".ToCharArray()), 1);
			selector.ValueChanged += Scan_ValueChanged;
			serverSetupGrid.AddChild(selector);

			cb = new MenuCheckButton(RelativeRect.Full, MenuRes.WeaponFrequencies);
			cb.ButtonCheckChanged += WeaponFreq_CheckChanged;
			serverSetupGrid.AddChild(cb);

			cb = new MenuCheckButton(RelativeRect.Full, MenuRes.PerSystemDamage);
			cb.ButtonCheckChanged += SystemDamage_CheckChanged;
			cb.Check();
			serverSetupGrid.AddChild(cb);

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

		private void ServerAddress_TextChanged(object sender, UITextEntry e)
		{
			
		}

		private void Password_TextChanged(object sender, UITextEntry e)
		{

		}
		private void ServerVis_ValueChanged(object sender, SpinSelector e)
		{

		}
		private void FTLType_ValueChanged(object sender, SpinSelector e)
		{

		}
		private void RadarRange_ValueChanged(object sender, SpinSelector e)
		{

		}
		private void TacRadar_CheckChanged(object sender, UIButton e)
		{

		}
		private void LRRadar_CheckChanged(object sender, UIButton e)
		{

		}
		private void Scan_ValueChanged(object sender, SpinSelector e)
		{

		}
		private void WeaponFreq_CheckChanged(object sender, UIButton e)
		{

		}

		private void SystemDamage_CheckChanged(object sender, UIButton e)
		{

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
