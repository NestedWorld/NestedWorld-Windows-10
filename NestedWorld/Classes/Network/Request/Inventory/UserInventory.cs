using NestedWorld.Classes.Request;
using System;

namespace NestedWorld.Classes.Network.Request.Inventory
{
    public class UserInventory : HttpRequest
    {
        public UserInventory() 
            : base("/users/inventory/", RequestType.GET)
        {
        }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
