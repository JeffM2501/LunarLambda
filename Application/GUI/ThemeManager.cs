using LudicrousElectron.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.GUI
{
    public static class ThemeManager
    {
        private static readonly string DefaultTheme = "standard_theme";
        private static string CurrentTheme = DefaultTheme;

        public static void SetTheme(string theme)
        {
            CurrentTheme = theme;

            // TODO flush the GUI
        }

        public static string GetThemeAsset(string path)
        {
            string assetPath = "Themes/" + CurrentTheme + "/" + path;
            if (!AssetManager.AssetExists(assetPath))
                assetPath = "Themes/" + DefaultTheme + "/" + path;

            return assetPath;
        }
    }
}
