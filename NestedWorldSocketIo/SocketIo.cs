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

        public void Add(string name, OnReceiveEvent callBack)
        {
            this.callbacks[name] = callBack;
        }

        public string url { get; private set; }
        private Socket socket;
        public SocketIo(string url)
        {
            this.url = url;
            this.socket = null;
            this.callbacks = new Dictionary<string, OnReceiveEvent>();
        }

        public void Connect()
        {
           this.socket = IO.Socket(this.url);

            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit("hi");

            });

            foreach(KeyValuePair<string, OnReceiveEvent> callback in callbacks)
            {
                socket.On(callback.Key, (data) =>
                {
                    Log.Info("SocketIo", "receive", callback.Key, " : ", data);
                    callback.Value?.Invoke(data);
                });
            }
        }

        public void Send(string on, string data)
        {
            if (socket != null)
            {
                socket.Emit(on, data);
            }
        }
        public void Disconnect()
        {
            if (socket != null)
            {
                socket.Disconnect();
            }
        }
    }
}
