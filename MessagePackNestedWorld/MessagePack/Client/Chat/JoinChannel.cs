using MessagePack.Client.Answers.Chat;
using MsgPack.Serialization;
using System.IO;

namespace MessagePack.Client.Chat
{
    public class JoinChannel : RequestBase
    {
       

        public string channel;

        public JoinChannel()
            : base("chat:join-channe", typeof(JoinChannelAnswers)) { }

        public override MemoryStream GetStream()
        {
            var context = new SerializationContext();
            context.SerializationMethod = SerializationMethod.Map;
            var serializer = MessagePackSerializer.Get<JoinChannel>(context);
            MemoryStream stream = new MemoryStream();
            serializer.Pack(stream, this);
            stream.Position = 0;
            return stream;
        }

        public static JoinChannel Join(string Channel)
        {
            return new JoinChannel() { channel = Channel };
        }
    }
}
