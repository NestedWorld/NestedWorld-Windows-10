using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Auth
{
    public class Login : HttpRequest
    {
        private const string App_token = "test";
        public Login()
            : base("/users/auth/login/simple", RequestType.POST)
        { }

        public void SetParam(string password, string mail)
        {
            collection = new Dictionary<string, string>();
            collection.Add("password", password);
            collection.Add("email", mail);
            collection.Add("app_token", App_token);
            uri = new Uri(url);
            
        }
    }
}
