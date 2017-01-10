using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Http.Exchange
{
    public class PostExchanges : HttpRequest
    {
        public PostExchanges() : base("/exchange/", RequestType.POST) { }


    
        public void SetParam(int monsterIdSend, int monsterIdAsk, int userMonsterIdSend)
        {
            collection = new Dictionary<string, string>();
            collection.Add("monster_sended", monsterIdSend.ToString());
            collection.Add("monster_asked", monsterIdAsk.ToString());
            collection.Add("umonster_sended", userMonsterIdSend.ToString());
            collection.Add("id", "0");
            uri = new Uri(url);
        }
    }
}
