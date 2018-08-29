using System;

using LunarLambda.API;

using LunarLambda.Campaigns.Standard.Databases.Templates;

namespace LunarLambda.Campaigns.Standard.Databases
{
    internal static class StandardShips
    {
        internal static void Load(object sender, EventArgs e)
        {
            if (StateData.Exists("StandardShips.Loaded") || StateData.GetB("StandardShips.Ignore"))
                return;

            StateData.Set("StandardShips.Loaded", true);

            Stations.Load();
            StarFighters.Load();
            Corvettes.Load();
        }
    }
}
