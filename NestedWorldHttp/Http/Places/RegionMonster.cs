using System;

namespace NestedWorldHttp.Places
{
    public class RegionMonster : HttpRequest
    {
        public RegionMonster() : base("/places/regions/", RequestType.GET)
        {
        }

        public void SetParam(int regionID)
        {
            uri = new Uri(url + regionID + "/monsters");
        }
    }
}
