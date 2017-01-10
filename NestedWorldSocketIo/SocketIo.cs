using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldSocketIo
{
    public class SocketIo
    {
        public delegate void OnReceiveEvent(object data);

        private Dictionary<string, OnReceiveEvent> callbacks;

        public void Add(string name, OnReceiveEvent callBack)
        {
            this.callbacks[name] = callBack;
        }

        public string url { get; private set; }
       
        public SocketIo(string url)
        {
            this.url = url;
            this.callbacks = new Dictionary<string, OnReceiveEvent>();
        }

        public void Connect()
        {
           
        }

        public void Send(string on, string data)
        {
        }
        public void Disconnect()
        {
            
        }
    }
}
