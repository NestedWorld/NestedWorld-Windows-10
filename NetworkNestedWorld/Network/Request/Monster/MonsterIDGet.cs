using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Monster
{
    public class MonsterIDGet : HttpRequest
    {
        public MonsterIDGet()
            : base("/Monsters/", RequestType.GET)
        { }

        public void SetParam(int ID)
        {
            uri = new Uri(url + ID.ToString());
        }
    }
}
