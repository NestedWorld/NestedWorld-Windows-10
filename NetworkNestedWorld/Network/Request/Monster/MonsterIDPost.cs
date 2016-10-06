using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Monster
{
    public class MonsterIDPost : HttpRequest
    {
        MonsterIDPost()
            : base("/Monsters/", RequestType.POST)
        { }

        public void SetParam(int ID)
        {
            uri = new Uri(url + ID.ToString());
        }
    }
}
