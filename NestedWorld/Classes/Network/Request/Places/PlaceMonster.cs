using NestedWorld.Classes.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.Request.Places
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
