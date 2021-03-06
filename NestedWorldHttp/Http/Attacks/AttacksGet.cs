﻿using System;

namespace NestedWorldHttp.Attacks
{
    public class AttacksGet : HttpRequest
    {
        public AttacksGet() : base("/attacks/", RequestType.GET) { }

        public void SetParam(int id = -1)
        {
            if (id >= 0)
                uri = new Uri(url + id.ToString());
            else
                uri = new Uri(url);
        }
    }
}
