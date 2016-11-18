using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.ElementsGame.Item
{
    public class InventoryObject : Item
    {
        public int userID { get; set; }

        public int number { get; set; }

        public InventoryObject(Item item, int userId, int number)
            :base(item)
        {
            this.userID = userId;
            this.number = number;
        }

        public static InventoryObject LoadJson(JObject obj)
        {
            Item source = NewFromJson(obj["infos"].ToObject<JObject>());
            int userID = obj["id"].ToObject<int>();

            return new InventoryObject(source, userID, 1);
        }
    }
}
