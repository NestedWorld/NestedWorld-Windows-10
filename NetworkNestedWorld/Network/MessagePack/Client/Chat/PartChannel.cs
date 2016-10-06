using MsgPack.Serialization;
using NestedWorld.Classes.MessagePack.Client;
using System.IO;

namespace NestedWorld.Classes.Network.MessagePack.Client.Chat
{
    public class PartChannel : RequestBase
    {
        public string token
        {
            get { return App.network.Token; }
            set { }
        }

        public string channel;

        public PartChannel() 
            : base("chat:part-channel") { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<PartChannel>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static PartChannel Part(string Channel)
        {
            return new PartChannel() { channel = Channel };
        }
    }
}
