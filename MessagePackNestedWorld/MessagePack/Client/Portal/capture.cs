using MessagePack.Client;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePackNestedWorld.MessagePack.Client.Portal
{
    public class Capture : RequestBase
    {
        public int portal_id { get; set; }
        public int monster { get; set; }

        public Capture()
            : base("portal:capture")
        {

        }

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

        public static Capture TryCapture(int portal_id, int monster)
        {
            return new Capture() { portal_id = portal_id, monster = monster };
        }
    }
}
