using System;

namespace NestedWorldHttp.Users
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
