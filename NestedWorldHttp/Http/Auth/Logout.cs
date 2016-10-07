using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Auth
{
   public class Logout : HttpRequest
    {
        public Logout ()
            : base("/users/auth/logout", RequestType.POST)
        { }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
