using NestedWorld.Classes.ElementsGame.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Windows.Storage.Streams;

namespace NestedWorld.Classes.ElementsGame.Shop
{
    public class Shop
    {
        public List<ItemGroup> list { get; set; }

        public ObservableCollection<ItemGroup> content
        {
            get
            {
                return new ObservableCollection<ItemGroup>(list);
            }
            set
            {
                int i = 0; i++;
            }
        }

        public Shop()
        {
            list = new List<ItemGroup>();
            init();
        }

        public async void init()
        {
            try
            {
                IRandomAccessStream data = await Utils.IO.GetDataAsStream("DataModel", "ShopData.xml");


                XDocument doc = XDocument.Load(data.AsStream());
                ItemGroup groupetmp;
                foreach (XElement element in doc.Element("Root").Elements("Groupe"))
                {
                    groupetmp = new ItemGroup((string)element.Attribute("name"), (string)element.Attribute("image"));
                    groupetmp.Load(element);
                    list.Add(groupetmp);
                }


            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }
}
