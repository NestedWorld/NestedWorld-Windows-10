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
            : base("/users/friends/", RequestType.POST)
        { }

        public void SetParam(string login)
        {
            collection = new Dictionary<string, string>();
            collection.Add("pseudo", login);
            uri = new Uri(url);
        }
    }
}
