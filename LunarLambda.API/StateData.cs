using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API
{
    public static class StateData
    {
        internal static Dictionary<string, string> StateItems = new Dictionary<string, string>();

        public static void Set(string name, string data)
        {
            StateItems[name] = data;
        }

        public static void Set(string name, bool data)
        {
            StateItems[name] = data ? "1" : "0";
        }

        public static void Set(string name, int data)
        {
            StateItems[name] = data.ToString();
        }

        public static void Set(string name, double data)
        {
            StateItems[name] = data.ToString(CultureInfo.InvariantCulture);
        }

        public static bool Exists(string name)
        {
            return StateItems.ContainsKey(name);
        }

        public static void Clear(string name)
        {
            if (Exists(name))
                StateItems.Remove(name);
        }

        public static string Get(string name)
        {
            if (StateItems.ContainsKey(name))
                return StateItems[name];
            return string.Empty;
        }

        public static bool GetB(string name)
        {
            if (StateItems.ContainsKey(name))
                return StateItems[name] != "0";
            return false;
        }

        public static int GetI(string name)
        {
            int value = 0;
            if (StateItems.ContainsKey(name))
                int.TryParse(StateItems[name], out value);
            return value;
        }

        public static double GetD(string name)
        {
            double value = 0;
            if (StateItems.ContainsKey(name))
                double.TryParse(StateItems[name], out value);
            return value;
        }
    }
}
