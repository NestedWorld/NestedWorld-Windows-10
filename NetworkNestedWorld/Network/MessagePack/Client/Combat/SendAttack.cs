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
    public class SendAttack : RequestBase
    {
        public int target { get; set; }
        public int attack { get; set; }

        public SendAttack() : base("combat:send-attack") { }
        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<SendAttack>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static SendAttack Attack(int attack, int target)
        {
            return new SendAttack() { attack = attack, target = target };
        }
    }
}
