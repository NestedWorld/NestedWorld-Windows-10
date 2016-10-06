using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Users
{
    public class Users : HttpRequest
    {
        public Users()
            : base("/users/", RequestType.GET)
        {

        }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
