using System;
using System.Collections.Generic;

namespace NestedWorldHttp.Users
{
    public class UserPut : HttpRequest
    {
        public UserPut()
            : base("/users/me/", RequestType.PUT)
        {

        }

        public void SetParam(params string[] param)
        {
            collection = new Dictionary<string, string>();
            for (int i = 0; i < param.Length; i += 2)
            {
                collection.Add(param[i], param[i + 1]);
            }
        }

        public void SetParam(string name, string avatar, string background)
        {
            collection = new Dictionary<string, string>();
            collection.Add("avatar", avatar);
            collection.Add("background", background);
            if (name != null)
                collection.Add("pseudo", name);
            uri = new Uri(url);

        }
    }
}
