using MessagePackNestedWorld.Utils;
using MsgPack.Serialization;
using System.IO;

namespace MessagePack.Client.Special
{
    public class Authenticate : RequestBase
    {
        public string token;
        public Authenticate() : base("authenticate") { }

        public override MemoryStream GetStream()
        {
            Log.Info("token", token);
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<Authenticate>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static Authenticate GetAuthenticate(string Token)
        {
            return new Authenticate() { token = Token };
        }
    }
}
