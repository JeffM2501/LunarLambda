using LudicrousElectron.Assets;
using LudicrousElectron.GUI;

namespace LunarLambda.GUI
{
    public static class ThemeManager
    {
        private static readonly string DefaultTheme = "standard_theme";
        private static string CurrentTheme = DefaultTheme;

        public static void SetTheme(string theme)
        {
            string newTheme = theme;
            if (string.IsNullOrEmpty(newTheme))
                newTheme = DefaultTheme;
            if (CurrentTheme == newTheme)
                return;

            CurrentTheme = newTheme;
            GUIManager.Reset();
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
