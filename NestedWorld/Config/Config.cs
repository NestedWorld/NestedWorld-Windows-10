using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage.Streams;

namespace NestedWorld.Config
{
    public class Config
    {
        public static async Task<string> GetKey(string configKey)
        {
            IRandomAccessStream data = await Utils.IO.GetDataAsStream("Config", "key.xml");

            XDocument doc = XDocument.Load(data.AsStream());
            foreach (XElement element in doc.Element("root").Elements("key"))
            {
                if ((string)element.Attribute("name") == configKey)
                    return element.Value;
            }
            return null;
        }
    }
}
