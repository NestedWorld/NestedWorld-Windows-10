using System;

namespace NestedWorldHttp.Monster
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
