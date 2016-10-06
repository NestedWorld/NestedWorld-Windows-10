using NestedWorld.Classes.Request;
using System;

namespace NestedWorld.Classes.Network.Request.Inventory
{
    public class ObjectGet : HttpRequest
    {
        public ObjectGet() :
            base("/objects/", RequestType.GET)
        {
        }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
