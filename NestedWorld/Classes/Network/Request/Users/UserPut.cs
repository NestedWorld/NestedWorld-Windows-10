using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Users
{
    public class UserPut : HttpRequest
    {
        public UserPut()
            : base("/users/", RequestType.PUT)
        {

        }

        public void SetParam(string name, string avatar, string background)
        {
            collection = new Dictionary<string, string>();
            collection.Add("avatar", avatar);
            collection.Add("background", background);
            collection.Add("pseudo", name);
            uri = new Uri(url);

        }
    }
}
