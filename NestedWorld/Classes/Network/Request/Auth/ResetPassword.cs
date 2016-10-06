using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Auth
{
    public class ResetPassword : HttpRequest
    {
        public ResetPassword()
            : base("/users/auth/resetpassword", RequestType.POST)
        { }

        public void SetParam(string mail)
        {
            collection = new Dictionary<string, string>();
            collection.Add("email", mail);
            uri = new Uri(url);
        }
    }
}
