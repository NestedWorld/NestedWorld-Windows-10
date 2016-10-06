using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Request.Monster
{
    public class Monsters : HttpRequest
    {

        public Monsters() : base("/monsters/", RequestType.GET){ }

        public void SetParam()
        {
            uri = new Uri(url);
        }
    }
}
