using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.GUI.Menus
{
	public class StartServerMenu : MenuCommon
	{
		protected override void SetupControls()
		{
			SetupBackground(0);
			SetupBackButton(1);

			AddAPIButtons(Name);
			base.SetupControls();
		}
	}
}
