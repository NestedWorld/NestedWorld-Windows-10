using NestedWorld.Classes.ElementsGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace NestedWorld.Utils
{
    public class RandomValue
    {
        internal static int GetRandValue(int min, int max)
        {
            Random rand = new Random();

            return rand.Next(min, max);
        }

        internal static BasicGeoposition GetRandomPosition()
        {
            Random rand = new Random();
            return new Windows.Devices.Geolocation.BasicGeoposition()
            {
                Latitude = rand.Next(-89, 89),
                Longitude = rand.Next(-89, 89)
            };
        }


        internal static TypeEnum GetRandValueType()
        {
            return (TypeEnum)GetRandValue(0, 5);
        }

        internal static bool GetRandValueBool()
        {
            return Convert.ToBoolean(GetRandValue(0, 2));
        }
    }
}
