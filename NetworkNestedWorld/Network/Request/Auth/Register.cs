using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Auth
{
    public class Register : HttpRequest
    {
        public Register()
            : base("/users/auth/register", RequestType.POST)
        { }

        public void SetParam(string mail, string password, string pseudo)
        {

            collection = new Dictionary<string, string>();
            collection.Add("password", password);
            collection.Add("email", mail);
            collection.Add("pseudo", pseudo);
            uri = new Uri(url);
        }
    }
}
