using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudicrousElectron.Engine.RenderChain;
using LudicrousElectron.Engine.RenderChain.Effects;
using LudicrousElectron.Types;

namespace LunarLambda.Common
{
    public static class Graphics
    {
        public static Vector3f CameraPosition = new Vector3f();
        public static float CameraYaw = 0;
        public static float CameraPitch = 0;

        public static int MainFontID = int.MinValue;
        public static int BoldFontID = int.MinValue;

        public static RenderLayer BackgroundLayer = null;
        public static RenderLayer ObjectLayer = null;
        public static RenderLayer EffectLayer = null;
        public static RenderLayer HudLayer = null;
        public static RenderLayer MouseLayer = null;
        public static PostProcessor GlitchPostProcessor = null;
        public static PostProcessor WarpPostProcessor = null;
    }
}
