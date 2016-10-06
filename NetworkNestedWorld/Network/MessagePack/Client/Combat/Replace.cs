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
    public class Replace : RequestBase
    {

        public int UserMonsterId { get; set; }

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

        public static Replace ReplaceMonster(int MonsterID)
        {
            return new Replace() { UserMonsterId = MonsterID };
        }
    }
}
