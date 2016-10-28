using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace NestedWorld.Classes.ElementsGame.Item
{
    public class ItemGroup
    {
        public List<Item> list { get; private set; }

        public string Name { get; private set; }
        public string Image { get; private set; }

        public ObservableCollection<Item> content
        {
            get
            {
                return new ObservableCollection<Item>(list);
            }
            set
            {
                int i = 0; i++;
            }
        }

        public ItemGroup(string name, string image)
        {
            list = new List<Item>();
            Name = name;
            Image = image;
        }

        public void Add(Item newItem)
        {
           // if (!list.Contains(newItem))
                list.Add(newItem);
        }

        public void Load(XElement element)
        {
            try
            {
                foreach(XElement item in element.Elements("Item"))
                {
                  // Add(new Item((string)item.Attribute("name"), (double) item.Attribute("price"),(string)item.Attribute("desc"),(string)item.Attribute("image"), (int)item.Attribute("att"), (int)item.Attribute("def"), (int)item.Attribute("life"), (int)item.Attribute("exp")));
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
