
using System;

namespace NestedWorldHttp.Places
{
    public class PlaceGet : HttpRequest
    {
        public PlaceGet() 
            : base("/places/", RequestType.GET)
        {
        }


        public void SetParam(int IDRegion = -1)
        {
            if (IDRegion == -1)
                uri = new Uri(url);
            else
                uri = new Uri(url + "/" + IDRegion.ToString());
        }
    }
}
