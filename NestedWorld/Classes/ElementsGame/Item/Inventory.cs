using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NestedWorld.Classes.ElementsGame.Item
{
    public class Inventory
    {
        public Dictionary<int, InventoryObject> _list;

        public ObservableCollection<InventoryObject> content { get { return new ObservableCollection<InventoryObject>(_list.Values); } set { } }

        public Inventory()
        {
            _list = new Dictionary<int, InventoryObject>();
        }

        public void Add(InventoryObject newItem)
        {
            _list[newItem.id] = newItem;
        }
        public void Add(int id, InventoryObject newItem)
        {
            _list[id] = newItem;
        }

        public InventoryObject GetValue(int key)
        {
            InventoryObject value;

            if (_list.TryGetValue(key, out value))
                return value;
            return null;
        }

        public InventoryObject this[int id]
        {
            get
            {
                return GetValue(id);
            }

            set
            {
                _list[id] = value;
            }
        }

        static public Inventory NewJromJson(JObject obj)
        {
            Inventory ret = new Inventory();

            try
            {
                JArray array = obj["inventory"].ToObject<JArray>();
                foreach (JObject tmp in array)
                {
                    var io = InventoryObject.LoadJson(tmp);
                    ret[io.userID] = io;
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
