using MsgPack.Serialization;
using System.IO;

namespace MessagePack.Client.Chat
{
    public class SendMessage : RequestBase
    {
      

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
