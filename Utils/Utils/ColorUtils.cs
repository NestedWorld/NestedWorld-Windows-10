using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace NestedWorld.Utils
{
    public static class ColorUtils
    {
       
        internal static Color GetColorFromHex(string hexString)
        {
            if (hexString.StartsWith("#"))
            {
                hexString = hexString.Substring(1, 8);
            }
            var a = Convert.ToByte(Int32.Parse(hexString.Substring(0, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            var r = Convert.ToByte(Int32.Parse(hexString.Substring(2, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            var g = Convert.ToByte(Int32.Parse(hexString.Substring(4, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            var b = Convert.ToByte(Int32.Parse(hexString.Substring(6, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            return Color.FromArgb(a, r, g, b);
        }

        public static Color[] ToArrayColor(this SolidColorBrush colorBrush, int height, int width)
        {
            Color[] ret = new Color[height * width];

            for (int i = 0; i < height * width; i++)
                ret[i] = colorBrush.Color;
            return ret;
        }
    }
}
