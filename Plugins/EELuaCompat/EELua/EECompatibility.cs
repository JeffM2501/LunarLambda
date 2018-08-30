using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunarLambda.API;

namespace EELua
{
    public class EECompatibility : LLPlugin
    {
        public override void Load()
        {
            // scan scripts and register scenarios
        }

        public override void Shutdown()
        {
            // clean up lua
        }

        public override void Startup()
        {
            // warm up scripting
        }

        public override void Unload()
        {
            // probably not needed
        }
    }
}
