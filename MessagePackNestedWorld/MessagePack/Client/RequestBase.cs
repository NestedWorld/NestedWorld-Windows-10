using MessagePack.Serveur;
using MessagePackNestedWorld.Utils;
using Network.MessagePack;
using System;
using System.Diagnostics;
using System.IO;

namespace MessagePack.Client
{
    public abstract class RequestBase : MessageBase
    {
        public delegate void OnCompledCallBack(ResultRequest result);

        public event OnCompledCallBack OnSuccess;
        public event OnCompledCallBack OnError;


        public Type AnswerType { get; private set; }


        public RequestBase(string type, Type AnswerType)
            : base(type, NewGuid()) {
            this.AnswerType = AnswerType;
        }

        public void Call(ResultRequest result)
        {
            Log.Info("Call result", result.result);

            if (result.result == "success")
                this.OnSuccess?.Invoke(result);
            else
                this.OnError?.Invoke(result);
        }

        public RequestBase(string type)
            : base(type, NewGuid())
        {
            this.AnswerType = null;
        }
        internal static string NewGuid()
        {
            Guid g = Guid.NewGuid();
            try
            {
                return g.ToString("B");
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return "null";
        }

         public abstract MemoryStream GetStream();
    }
}
