using NestedWorld.Classes.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.Request.Places
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
