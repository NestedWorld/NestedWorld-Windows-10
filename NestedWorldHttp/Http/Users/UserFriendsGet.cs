using System;

namespace NestedWorldHttp.Users
{
    public class UserFriendsGet : HttpRequest
    {
        public UserFriendsGet() 
            : base("/users/friends/", RequestType.GET)
        { }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
