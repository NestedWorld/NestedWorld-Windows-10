using NestedWorld.Classes.ElementsGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using NestedWorld.Classes.ElementsGame.Monsters;
using Windows.UI.Xaml.Media;

namespace NestedWorld.Utils
{
    public static class ColorUtils
    {
        internal static string GetTypeColor(TypeEnum type)
        {
            switch (type)
            {
                case (TypeEnum.FIRE):
                    return "#FFF44336";
                case (TypeEnum.WATHER):
                    return "#FF2196F3";
                case (TypeEnum.GRASS):
                    return "#FF4CAF50";
                case (TypeEnum.DIRT):
                    return "#FF795548";
                case (TypeEnum.ELEC):
                    return "#FFFFEB3B";

            }
            return "#FFFFFFFF";
        }

        internal static string GetTypeColor(TypeEnum type, bool trasp)
        {
            switch (type)
            {
                case (TypeEnum.FIRE):
                    return "#99F44336";
                case (TypeEnum.WATHER):
                    return "#992196F3";
                case (TypeEnum.GRASS):
                    return "#994CAF50";
                case (TypeEnum.DIRT):
                    return "#99795548";
                case (TypeEnum.ELEC):
                    return "#99FFEB3B";

            }
            return "#FFFFFFFF";
        }

        /*   internal static string GetTypeColor(AttackTypeEnum attackType)
           {
               switch (attackType)
               {
                   case (AttackTypeEnum.ATT):
                       return "#FFEF9A9A";
                   case (AttackTypeEnum.DEF):
                       return "#FFC5CAE9";
                   case (AttackTypeEnum.SPEATT):
                       return "#FFC62828";
                   case (AttackTypeEnum.SPEDEF):
                       return "#FF283593";
               }
               return "#FFFFFFFF";
           }*/

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
