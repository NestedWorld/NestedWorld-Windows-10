using NestedWorld.Classes.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.Request.Places
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
