using NestedWorld.Classes.MessagePack.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MsgPack.Serialization;

namespace NestedWorld.Classes.Network.MessagePack.Client.Combat
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
