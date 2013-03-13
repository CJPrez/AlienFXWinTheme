using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;

namespace AlienFXWinTheme
{
    internal static class NativeMethods
    {
        [DllImport("dwmapi.dll", EntryPoint = "#127")]
        internal static extern void DwmGetColorizationParameters(ref DWMCOLORIZATIONPARAMS param);
    }

    public struct DWMCOLORIZATIONPARAMS
    {
        public uint ColorizationColor,
            ColorizationAfterglow,
            ColorizationColorBalance,
            ColorizationAfterglowBalance,
            ColorizationBlurBalance,
            ColorizationGlassReflectionIntensity,
            ColorizationOpaqueBlend;
    }

    class WindowsTheme
    {
        private AlienFX lights;

        public WindowsTheme()
        {
            AlienFX.SetColor(GetWindowColorizationColor(false));
        }

        const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;

        public static Color GetWindowColorizationColor(bool opaque)
        {
            DWMCOLORIZATIONPARAMS param = new DWMCOLORIZATIONPARAMS();
            NativeMethods.DwmGetColorizationParameters(ref param);

            return Color.FromArgb(
                (byte)(opaque ? 255 : param.ColorizationColor >> 24),
                (byte)(param.ColorizationColor >> 16),
                (byte)(param.ColorizationColor >> 8),
                (byte)param.ColorizationColor
            );
        }

        public void updateColor()
        {
            AlienFX.SetColor(GetWindowColorizationColor(false));
        }
    }
}
