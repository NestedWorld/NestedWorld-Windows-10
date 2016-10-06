using MsgPack.Serialization;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class SendAttack : RequestBase
    {
        public int target { get; set; }
        public int attack { get; set; }
        public int combat { get; set; }
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

        public static SendAttack Attack(int combatId, int attack, int target)
        {
            return new SendAttack() { attack = attack, target = target, combat = combatId };
        }
    }
}
