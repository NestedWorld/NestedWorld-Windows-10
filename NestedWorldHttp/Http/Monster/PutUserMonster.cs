using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Monster
{
    public class PutUserMonster : HttpRequest
    {
        public PutUserMonster() : base("/users/monsters/{0}", RequestType.PUT) { }

        public void SetParam(int id, string surname)
        {
            collection = new Dictionary<string, string>();
            collection.Add("surname", surname);

            uri = new Uri(string.Format(url, id.ToString()));
        }
    }
}
