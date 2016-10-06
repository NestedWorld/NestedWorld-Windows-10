using MessagePack.Client;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePackNestedWorld.MessagePack.Client.Combat
{
    public class MonsterKoCapture : RequestBase
    {
        public bool capture { get; set; }
        public string name { get; set; }
        public int combat { get; set; }
        public MonsterKoCapture()
            : base("combat:monster-ko:replace") { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<MonsterKoCapture>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static MonsterKoCapture Capture(int combatID, bool Capture, string nickname = null)
        {
            return new MonsterKoCapture() { combat = combatID, capture = Capture, name = nickname};
        }
    }
}
