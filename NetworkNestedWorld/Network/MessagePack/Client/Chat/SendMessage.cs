using NestedWorld.Classes.MessagePack.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MsgPack.Serialization;

namespace NestedWorld.Classes.Network.MessagePack.Client.Chat
{
    public class SendMessage : RequestBase
    {
        public string token
        {
            get { return App.network.Token; }
            set { }
        }

        public string channel;

        public string message;

        public SendMessage()
            : base("chat:send-message") { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<SendMessage>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static SendMessage Send(string Channel, string Message)
        {
            return new SendMessage() { channel = Channel, message = Message };
        }
    }
}
