using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

namespace EELuaEval
{
    class Program
    {
        static void Main(string[] args)
        {
            Script.WarmUp();

            try
            {
                Script script = new Script();

                ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = new string[] { "E:\\Projects\\EmptyEpsilon\\EmptyEpsilon\\scripts\\" };
                script.DoFile("test.lua");

                DynValue res = script.Call(script.Globals["init"]);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
