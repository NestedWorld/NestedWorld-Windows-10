using System;

namespace NestedWorldHttp.Places
{
    public class PlaceMonster : HttpRequest
    {
        public PlaceMonster() : base("/places/", RequestType.GET)
        {
        }

        public void SetParam(int placeID)
        {
            uri = new Uri(url + placeID + "/monsters");
        }
    }
}
