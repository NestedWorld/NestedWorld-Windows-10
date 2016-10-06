using NestedWorld.Classes.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.Request.Places
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
