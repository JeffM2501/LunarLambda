using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API
{
    public static partial class Events
    {
        public class APIEventArgs : EventArgs
        {
            public bool Handled = false;
        }

        // event arg classes
        public class StringDataEventArgs : APIEventArgs
        {
            public string Data = string.Empty;

            public StringDataEventArgs(string d) : base()
            {
                Data = d;
            }
        }

        // events
        // database load events
        public static event EventHandler SetupModelData = null;
        public static event EventHandler SetupShipTemplates = null;
        public static event EventHandler SetupFactions = null;
        public static event EventHandler SetupScienceDB = null;

		public static event EventHandler FinalizeDatabases = null;

		public static event EventHandler MissionStart = null;

    }
}
