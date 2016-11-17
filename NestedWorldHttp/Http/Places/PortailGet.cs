using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Places
{
    public class PortailGet : HttpRequest
    {
        public PortailGet() 
            : base ("/geo/portals", RequestType.GET) { }

        public void SetParam(double lat, double lon)
        {
            this.uri = new Uri(url + "/" +  lat + "/" + lon);
        }
    }
}
