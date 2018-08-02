using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LudicrousElectron.Engine.Audio;

namespace LunarLambda.API
{
    public static partial class Events
    {
        internal static void CallSetupModelData(object sender = null) { SetupModelData?.Invoke(sender, EventArgs.Empty); }
        internal static void CallSetupShipTemplates(object sender = null) { SetupShipTemplates?.Invoke(sender, EventArgs.Empty); }
        internal static void CallSetupFactions(object sender = null) { SetupFactions?.Invoke(sender, EventArgs.Empty); }
        internal static void CallSetupScienceDB(object sender = null) { SetupScienceDB?.Invoke(sender, EventArgs.Empty); }
    }
}
