using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Users
{
    public class GetUserId : HttpRequest
    {
        public GetUserId()
            : base("/users/{0}", RequestType.GET) { }

        public void SetParam(int userID)
        {
            this.uri = new Uri(string.Format(this.url, userID));
        }
    }
}
