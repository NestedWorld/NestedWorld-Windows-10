using System;

namespace NestedWorldHttp.Monster
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
