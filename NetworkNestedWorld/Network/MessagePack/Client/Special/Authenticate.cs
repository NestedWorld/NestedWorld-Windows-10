using MsgPack.Serialization;
using NestedWorld.Classes.MessagePack.Client;
using NestedWorld.Utils;
using System.IO;

namespace NestedWorld.Classes.Network.MessagePack.Client.Special
{
    public class Authenticate : RequestBase
    {
        public string token = App.network.Token;
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

        public static Authenticate GetAuthenticate()
        {
            return new Authenticate();
        }
    }
}
