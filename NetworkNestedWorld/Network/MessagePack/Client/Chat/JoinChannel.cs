using MsgPack.Serialization;
using NestedWorld.Classes.MessagePack.Client;
using NestedWorld.Classes.Network.MessagePack.Client.Answers.Chat;
using System.IO;

namespace NestedWorld.Classes.Network.MessagePack.Client.Chat
{
    public class JoinChannel : RequestBase
    {
        public string token
        {
            get { return App.network.Token; }
            set { }
        }

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
