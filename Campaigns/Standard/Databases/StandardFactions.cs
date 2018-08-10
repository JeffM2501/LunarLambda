using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.API.Databases;

namespace LunarLambda.Campaigns.Standard.Databases
{
    internal static class StandardFactions
    {
        internal static void Load(object sender, EventArgs e)
        {
            var neutral = FactionDatabase.AddFaction(FactionStrings.Independent);
            neutral.SetDescription(FactionStrings.IndependentDesc);
            neutral.SetColor(128, 128, 128);
            neutral.Playable = true;

            var human = FactionDatabase.AddFaction(FactionStrings.HuNav);
            human.SetDescription(FactionStrings.HuNavDesc);
            human.SetColor(255,255,255);
            human.Playable = true;

            var kraylor = FactionDatabase.AddFaction(FactionStrings.Kraylor);
            kraylor.SetDescription(FactionStrings.KraylorDesc);
            kraylor.SetColor(255, 0, 0);
            kraylor.SetRelationShip(human, FactionInfo.Relations.Hostile, true);

            var arlenians = FactionDatabase.AddFaction(FactionStrings.Arlenian);
            arlenians.SetDescription(FactionStrings.ArlenianDesc);
            arlenians.SetColor(2255, 128, 0);
            arlenians.SetRelationShip(kraylor, FactionInfo.Relations.Hostile, true);

            var exuari = FactionDatabase.AddFaction(FactionStrings.Exuari);
            exuari.SetDescription(FactionStrings.ExuariDesc);
            exuari.SetColor(255, 0, 128);
            exuari.SetRelationShip(neutral, FactionInfo.Relations.Hostile, true);
            exuari.SetRelationShip(human, FactionInfo.Relations.Hostile, true);
            exuari.SetRelationShip(kraylor, FactionInfo.Relations.Hostile, true);
            exuari.SetRelationShip(arlenians, FactionInfo.Relations.Hostile, true);

            var GITM = FactionDatabase.AddFaction(FactionStrings.Ghosts);
            GITM.SetDescription(FactionStrings.GhostsDesc);
            GITM.SetColor(0, 255, 0);
            GITM.SetRelationShip(human, FactionInfo.Relations.Hostile, true);

            var Hive = FactionDatabase.AddFaction(FactionStrings.Ktlitan);
            Hive.SetDescription(FactionStrings.KtlitansDesc);
            Hive.SetColor(128, 255, 0);
            Hive.SetRelationShip(human, FactionInfo.Relations.Hostile, true);
            Hive.SetRelationShip(kraylor, FactionInfo.Relations.Hostile, true);
            Hive.SetRelationShip(exuari, FactionInfo.Relations.Hostile, true);
        }
    }
}
