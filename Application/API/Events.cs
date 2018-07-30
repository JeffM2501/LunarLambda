using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API
{
    public static class Events
    {
        // events
        public static event EventHandler SetupModelData = null;
        public static event EventHandler SetupShipTemplates = null;
        public static event EventHandler SetupFactions = null;
        public static event EventHandler SetupScienceDB = null;


        // calls
        internal static void CallSetupModelData(object sender) { SetupModelData?.Invoke(sender, EventArgs.Empty); }
        internal static void CallSetupShipTemplates(object sender) { SetupShipTemplates?.Invoke(sender, EventArgs.Empty); }
        internal static void CallSetupFactions(object sender) { SetupFactions?.Invoke(sender, EventArgs.Empty); }
        internal static void CallSetupScienceDB(object sender) { SetupScienceDB?.Invoke(sender, EventArgs.Empty); }
    }
}
