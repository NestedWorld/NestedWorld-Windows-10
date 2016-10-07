using System;

namespace NestedWorldHttp.Inventory
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
