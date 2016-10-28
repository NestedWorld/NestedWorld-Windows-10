using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Users
{
    public class Me : HttpRequest
    {
        public Me() : base("/users/me/", RequestType.GET) { }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
