using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API
{
    public interface ILLPlugin
    {
        void Load();
        void Unload();
        void Startup();
        void Shutdown();
    }

    public class LLPlugin : ILLPlugin
    {
        public virtual void Load()
        {
        }

        public virtual void Shutdown()
        {
        }

        public virtual void Startup()
        {
        }

        public virtual void Unload()
        {
        }
    }
}
