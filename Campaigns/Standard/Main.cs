using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.API;
using LunarLambda.API.Databases;

using LunarLambda.Campaigns.Standard.Databases;


namespace LunarLambda.Campaigns.Standard
{
    public class Main : LLPlugin
    {
        public override void Load()
        {
            Events.SetupModelData += StandardModels.Load;
            Events.SetupShipTemplates += StandardShips.Load;
            Events.SetupFactions += StandardFactions.Load;
        }
    }
}