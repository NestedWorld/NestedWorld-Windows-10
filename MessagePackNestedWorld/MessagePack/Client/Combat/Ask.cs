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
    public class Ask : RequestBase
    {
        public string opponent { get; set; } 

        public Ask() : base("combat:ask", typeof(Answers.Combat.CombatAskResult)) {

        }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<Ask>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static Ask StartFightWhit(string oppomentName)
        {
            return new Ask() { opponent = oppomentName };
        }
    }
}
