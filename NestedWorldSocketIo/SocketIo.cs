using Quobject.SocketIoClientDotNet.Client;
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
        
        public void On(string name, OnReceiveEvent callBack)
        {
            this.callbacks[name] = callBack;
        }

        public string url { get; private set; }

        private Quobject.SocketIoClientDotNet.Client.Socket Socket;

        public SocketIo(string url)
        {
            this.url = url;
            Socket = IO.Socket(url);
            this.callbacks = new Dictionary<string, OnReceiveEvent>();
        }

        public void Connect()
        {
            foreach (KeyValuePair<string, OnReceiveEvent> callback in callbacks)
            {
                Socket.On(callback.Key, (data) =>
                {
                    Log.Info("Socket.IO", callback.Key, " => ", data);
                    callback.Value?.Invoke(data);
                });
            }

            Socket.Connect();
        }

        public void Send(string on, string data)
        {
            Socket.Send(on, data);
        }
        public void Disconnect()
        {
            
        }
    }
}
