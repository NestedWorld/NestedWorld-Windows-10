using System;

namespace NestedWorldHttp.Monster
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
