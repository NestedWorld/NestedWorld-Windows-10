using NestedWorld.Classes.Network.MessagePack;
using System;
using System.Diagnostics;
using System.IO;

namespace NestedWorld.Classes.MessagePack.Client
{
    public abstract class RequestBase : MessageBase
    {
        public Type AnswerType { get; private set; }


        public RequestBase(string type, Type AnswerType)
            : base(type, NewGuid()) {
            this.AnswerType = AnswerType;
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
