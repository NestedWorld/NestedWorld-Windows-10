using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Item
{
    public class ItemList
    {
        private Dictionary<int, Item> _list;

        public ObservableCollection<Item> content { get { return new ObservableCollection<Item>(_list.Values); } set { } }

        public ItemList()
        {
            _list = new Dictionary<int, Item>();
        }

        public void Add(Item newItem)
        {
            _list[newItem.id] = newItem;
        }
        public void Add(int id, Item newItem)
        {
            _list[id] = newItem;
        }

        public Item GetValue(int key)
        {
            Item value;

            if (_list.TryGetValue(key, out value))
                return value;
            return null;
        }

        public Item this[int id]
        {
            get
            {
                return GetValue(id);
            }

            set
            {
                Add(id, value);
            }
        }

        static public ItemList NewJromJson(JObject obj)
        {
            ItemList ret = new ItemList();

            try
            {
                JArray array = obj["Objects"].ToObject<JArray>();

                foreach(JObject tmp in array)
                {
                    ret.Add(Item.NewFromJson(tmp));
                }
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error("ItemList::NewFromJson", ex);
            }
            return ret;
        }
    }
}
