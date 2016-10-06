using NestedWorld.Classes.Network.MessagePack.Client.Answers;
using NestedWorld.Classes.Network.MessagePack.Exception;
using NestedWorld.Classes.Network.MessagePack.Serveur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.MessagePack.Client
{
    public class ClientMessageList
    {
        private Dictionary<string, RequestBase> map;

        private Dictionary<string, AnswerBase> answerDictionary;

        public SocketLib.StreamConnection stream { get; set; }
        public ClientMessageList()
        {
            map = new Dictionary<string, RequestBase>();
            answerDictionary = new Dictionary<string, AnswerBase>();
        }

        public void Add(RequestBase request)
        {
            map[request.type] = request;
        }

        public RequestBase Get(string type)
        {
            RequestBase value;
            if (map.TryGetValue(type, out value))
                return value;
            throw new NoTypeFoundException(type);
        }

        public void SendRequest(RequestBase request)
        {
            if (request.AnswerType != null)
                answerDictionary[request.id] = Activator.CreateInstance(request.AnswerType) as AnswerBase;
            if (!App.core.Offline)
                stream.Send(request.GetStream());
        }

        public void ReceiveRequest(object value)
        {
            ResultRequest rR = value as ResultRequest;

            AnswerBase answer = null;

            if (!answerDictionary.TryGetValue(rR.id, out answer))
                return;
            answer.SetValue(rR);
        }
    }
}
