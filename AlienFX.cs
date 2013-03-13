using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LightFX;

namespace AlienFXWinTheme
{
    class AlienFX
    {
        private static LightFXController lightFX;
        private static uint numDevs;

        private static void Initialize()
        {
            lightFX = new LightFXController();
            var result = lightFX.LFX_Initialize();

            if (result == LFX_Result.LFX_Success)
            {
                lightFX.LFX_Reset();
                lightFX.LFX_GetNumDevices(out numDevs);
            }
        }

        public static void SetColor(Color themeColor)
        {
            if(lightFX == null)
                Initialize();
            
            var color = new LFX_ColorStruct(255, themeColor.R, themeColor.G, themeColor.B);
            lightFX.LFX_Light(LFX_Position.LFX_All, color);
            lightFX.LFX_Update();
        }

        public static void Release()
        {
            if (lightFX != null)
            {
                lightFX.LFX_Release();
                lightFX = null;
            }
        }
    }
}
