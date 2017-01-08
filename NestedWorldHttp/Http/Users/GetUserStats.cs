using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Users
{
    public class GetUserStats : HttpRequest
    {
        public GetUserStats()
            : base("/users/{0}/stats", RequestType.GET) { }

        public void SetParam()
        {
            uri = new Uri(string.Format(this.url, "me"));
        }

        public void SetParam(int id)
        {
            uri = new Uri(string.Format(this.url, id.ToString()));
        }
    }
}
