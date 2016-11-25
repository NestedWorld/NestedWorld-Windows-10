using MessagePack.Client.Answers;
using MessagePack.Exception;
using MessagePack.Serveur;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;

namespace MessagePack.Client
{
    public class ClientMessageList
    {
        private Dictionary<string, RequestBase> map;

        private Dictionary<string, AnswerBase> answerDictionary;

        private bool offline;

        private Dictionary<string, RequestBase> idhistory;

        public SocketLib.StreamConnection stream { get; set; }
        public ClientMessageList(bool Offline = false)
        {
            offline = Offline;
            map = new Dictionary<string, RequestBase>();
            answerDictionary = new Dictionary<string, AnswerBase>();
            idhistory = new Dictionary<string, RequestBase>();
        }

        public void Add(RequestBase request)
        {
            map[request.type] = request;
        }

        public RequestBase Get(string type)
        {
            RequestBase value;
            if (idhistory.TryGetValue(type, out value))
                return value;
            throw new NoTypeFoundException(type);
        }

        /*   private AnswerBase GetType(Type type)
           {
               AnswerBase value;

               if (jamesbrown.TryGetValue(type, out value))
                   return value;
               throw new NoTypeFoundException(type.ToString());
           }
           */
        public async void SendRequest(RequestBase request)
        {
            idhistory[request.id] = request;
            try
            {
                if (!offline)
                    stream.Send(request.GetStream());
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (System.Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                await new MessageDialog("Sorry but the server is not online", "Server error").ShowAsync();
            }
        }

        public void ReceiveRequest(object value)
        {
            ResultRequest rR = value as ResultRequest;

            RequestBase rB = Get(rR.id);
            rB.Call(rR);

            /* AnswerBase answer = null;
             if (!answerDictionary.TryGetValue(rR.id, out answer))
                 return;
             answer.clientMessageList = this;
             answer.SetValue(rR);*/
        }


        public RequestBase this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                // answerDictionary(key, value);
            }
        }
    }
}
