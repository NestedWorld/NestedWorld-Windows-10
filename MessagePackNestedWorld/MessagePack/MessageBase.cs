using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.MessagePack
{
    public abstract class MessageBase
    {
        public string type;
        public string id;

        public MessageBase(string type, string id)
        {
            this.type = type;
            this.id = id;
        }
        public MessageBase()
        {

        }
    }
}

