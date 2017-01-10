using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Exchange
{
    public class PostEchangesAccept : HttpRequest
    {
        public PostEchangesAccept() : base("/exchange/{0}", RequestType.POST) { }

        public void SetParam(int id, int monster)
        {
            collection = new Dictionary<string, string>();
            collection.Add("sended", monster.ToString());
            uri = new Uri(string.Format(url, id.ToString()));
        }
    }
}
