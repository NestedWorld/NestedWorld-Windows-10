using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NestedWorld.Utils
{
    public class StorageApplication
    {
        internal static string GetValue(string key, string defaultValue)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                return (string)(ApplicationData.Current.LocalSettings.Values[key]);
            }
            return defaultValue;
        }

        internal static void SetValue(string key, string value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }
    }
}
