using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.Campaigns.Standard.Databases.Templates;

namespace LunarLambda.Campaigns.Standard.Databases
{
    internal static class StandardShips
    {
        internal static void Load(object sender, EventArgs e)
        {
            Stations.Load();
        }
    }
}
