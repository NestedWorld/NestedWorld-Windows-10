using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Monster
{
    public class MonsterAttackIDGet : HttpRequest
    {
        public MonsterAttackIDGet()
              : base("/monsters/", RequestType.GET)
        { }

        public void SetParam(int ID)
        {
            uri = new Uri(url + ID.ToString() + "/attacks");
        }
    }
}
