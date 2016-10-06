using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Users
{
    public class UserFriendsPost : HttpRequest
    {
        public UserFriendsPost() 
            : base("/users/monsters/", RequestType.GET)
        { }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
