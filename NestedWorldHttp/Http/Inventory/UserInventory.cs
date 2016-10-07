using System;

namespace NestedWorldHttp.Inventory
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
