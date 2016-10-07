using System;
using System.Collections.Generic;

namespace NestedWorldHttp.Monster
{
    public class UsersMonster : HttpRequest
    {
        public UsersMonster() 
            : base ("/users/monsters/", RequestType.GET)
        {

        }

        public void SetParam(string Token)
        {
            collection = new Dictionary<string, string>();
            collection.Add("token", Token);
            uri = new Uri(url);
        }
    }
}
