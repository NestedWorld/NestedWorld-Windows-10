using MsgPack.Serialization;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class Capture : RequestBase
    {
        public bool capture { get; set; }
        public string name { get; set; }

        public Capture()
            : base("combat:monster-ko:capture")
        { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<Capture>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }
    }
}
