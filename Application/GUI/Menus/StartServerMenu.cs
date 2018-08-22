using LudicrousElectron.GUI;
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
        protected VerticalLayoutGroup[] Columns = new VerticalLayoutGroup[] { null, null };

        protected override void SetupControls()
		{
			SetupBackground(0);
			SetupBackButton(1);

            int layerIndex = 2;
            layerIndex = SetupServerConfig(layerIndex);

            AddAPIButtons(Name);
			base.SetupControls();
		}

        protected int SetupServerConfig(int layerIndex)
        {
            RelativeRect rect = new RelativeRect(RelativeLoc.XLeftBorder + RelativeLoc.BorderOffset, RelativeLoc.YLowerBorder + RelativeLoc.BorderOffset, RelativeSize.HalfBorderInsetWidth, RelativeSize.SevenEightsHeight, OriginLocation.LowerLeft);

            Columns[0] = SetupCommonColumn(rect);

            // 15 total slots
            // config header
            Columns[0].AddChild(new Header(new RelativeRect(), MenuRes.ServerConfiguration));

            AddElement(Columns[0], layerIndex);


            return layerIndex + 1;
        }
	}
}
