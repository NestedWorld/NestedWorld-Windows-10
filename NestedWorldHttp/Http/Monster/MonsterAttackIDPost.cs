using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Monster
{
    public class MonsterAttackIDPost : HttpRequest
    {
        MonsterAttackIDPost()
            : base ("/Monsters/", RequestType.POST)
        { }

        public void SetParam(int ID)
        {
            uri = new Uri(url + ID.ToString() + "/Attacks");
        }

    }
}
