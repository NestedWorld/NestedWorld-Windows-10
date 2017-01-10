using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Exchange
{
    public class GetExchanges : HttpRequest
    {
        public GetExchanges() : base ("/exchange/", RequestType.GET) { }

        public void SetParam()
        {
            this.uri = new Uri(this.url);
        }
    }
}
