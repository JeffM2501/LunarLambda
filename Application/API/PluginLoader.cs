using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LunarLambda.API
{
    internal static class PluginLoader
    {
        internal static List<ILLPlugin> LoadedPlugins = new List<ILLPlugin>();

        private static bool Inited = false;
        private static bool Started = false;

        private static void LoadPlugin(ILLPlugin plugin)
        {
            plugin.Load();
        }
        private static void UnloadPlugin(ILLPlugin plugin)
        {
            plugin.Unload();
        }

        private static void StartPlugin(ILLPlugin plugin)
        {
            plugin.Startup();
        }

        private static void StopPlugin(ILLPlugin plugin)
        {
            plugin.Shutdown();
        }

        internal static void LoadAllPlugins()
        {
            if (Inited)
                return;

            Inited = true;
            foreach (var p in LoadedPlugins)
                LoadPlugin(p);
        }

        internal static void StartAllPlugins()
        {
            if (!Inited || Started)
                return;

            Started = true;
            foreach (var p in LoadedPlugins)
                StartPlugin(p);
        }

        internal static void StopAllPlugins()
        {
            if (!Inited || !Started)
                return;

            Started = false;
            foreach (var p in LoadedPlugins)
                StopPlugin(p);
        }

        internal static void UnloadAllPlugins()
        {
            if (!Inited )
                return;

            if (Started)
                StopAllPlugins();

            Inited = false;
            foreach (var p in LoadedPlugins)
                UnloadPlugin(p);

            LoadedPlugins.Clear();
        }

        internal static void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
                return;

            foreach (var t in assembly.GetTypes())
            {
                try
                {
                    if (t.GetInterface(typeof(ILLPlugin).Name) != null)
                    {
                        ILLPlugin plugin = Activator.CreateInstance(t) as ILLPlugin;
                        if (plugin != null)
                        {
                            LoadedPlugins.Add(plugin);
                            if (Inited)
                                LoadPlugin(plugin);

                            if (Started)
                                StartPlugin(plugin);
                        }
                    }
                }
                catch (Exception)
                {
                    // it can't load, skip it
                }
            }
        }

        internal static void AddDir(DirectoryInfo dir, bool recursive)
        {
            if (!dir.Exists)
                return;

            foreach (var f in dir.GetFiles("*.dll"))
            {
                try
                {
                    AddAssembly(Assembly.LoadFile(f.FullName));
                }
                catch (Exception)
                {
                    // it can't load, skip it
                }
            }

            if (recursive)
            {
                foreach (var subDir in dir.GetDirectories())
                    AddDir(subDir, true);
            }
        }
    }
}
