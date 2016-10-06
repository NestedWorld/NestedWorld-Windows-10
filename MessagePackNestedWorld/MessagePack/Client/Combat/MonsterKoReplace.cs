using MessagePack.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MsgPack.Serialization;

namespace MessagePackNestedWorld.MessagePack.Client.Combat
{
    public class MonsterKoReplace : RequestBase
    {
        public int user_monster_id { get; set; }
        public int combat { get; set; }
        public MonsterKoReplace()
            : base("combat:monster-ko:replace") { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<MonsterKoReplace>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static MonsterKoReplace Replace(int combatID, int monsterId)
        {
            return new MonsterKoReplace()
            {
                user_monster_id = monsterId,
                combat = combatID
            };
        }
    }
}
