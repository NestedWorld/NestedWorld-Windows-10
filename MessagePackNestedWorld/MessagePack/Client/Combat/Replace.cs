using MsgPack.Serialization;
using System.IO;

namespace MessagePack.Client.Combat
{
    public class Replace : RequestBase
    {

        public int UserMonsterId { get; set; }
        public int combat { get; set; }
        public Replace() 
            : base("combat:monster-ko:replace")
        {
        }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<Replace>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static Replace ReplaceMonster(int combatID, int MonsterID)
        {
            return new Replace() { UserMonsterId = MonsterID, combat = combatID };
        }
    }
}
