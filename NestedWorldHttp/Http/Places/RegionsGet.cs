using System;

namespace NestedWorldHttp.Places
{
    public class RegionsGet : HttpRequest
    {
        public RegionsGet() : base("/places/regions", RequestType.GET )
        {
        }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
