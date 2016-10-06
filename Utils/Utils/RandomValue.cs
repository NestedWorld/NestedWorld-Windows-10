using System;
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


       
        internal static bool GetRandValueBool()
        {
            return Convert.ToBoolean(GetRandValue(0, 2));
        }
    }
}
