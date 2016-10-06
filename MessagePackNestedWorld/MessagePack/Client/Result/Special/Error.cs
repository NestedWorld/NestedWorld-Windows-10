using MessagePack.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MsgPack.Serialization;

namespace MessagePackNestedWorld.MessagePack.Client.Result.Special
{
    public class Error : RequestBase
    {
        public string result = "error";

        public string kind;
        public string message;

        public Error() : base("result") { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<Error>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static Error New(string Kind, string Message)
        {
            return new Error() { kind = Kind, message = Message };
        }
    }
}
