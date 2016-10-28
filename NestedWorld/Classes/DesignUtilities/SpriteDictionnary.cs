using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Windows.Storage.Streams;

namespace NestedWorld.Classes.DesignUtilities
{
    public class SpriteDictionnary
    {
        public Dictionary<string, Sprite> spriteMap;

        public XDocument doc;

        public SpriteDictionnary()
        {
            spriteMap = new Dictionary<string, Sprite>();
        }

        public async void Load(string filename)
        {
            try
            {
                IRandomAccessStream data = await Utils.IO.GetDataAsStream("DataModel", filename);
                doc = XDocument.Load(data.AsStream());
                XElement root = doc.Element("Sprites");

                foreach (XElement element in root.Elements("Sprite"))
                    spriteMap[(string)element.Attribute("name")] = Sprite.LoadSprite(element);
            }
            catch (System.Exception ex)
            {
                Log.Error("SpriteDisctionnay, Load", ex);
            }
        }

        public static SpriteDictionnary GetSpriteDictionnary(string name)
        {
            SpriteDictionnary ret = new SpriteDictionnary();
            ret.Load(name);
            return ret;
        }

        public Sprite this[string key]
        {
            get
            {
                return GetSprite(key);
            }
            set
            {

            }
        }

        public Sprite GetSprite(string name)
        {
            Sprite value;
            if (spriteMap.TryGetValue(name, out value))
                return value;
            return null;
        }
    }
}
