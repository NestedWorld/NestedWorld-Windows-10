using MessagePackNestedWorld.Utils;
using Network.MessagePack;

namespace MessagePack.Serveur
{
    public abstract class ResultBase : MessageBase
    {
      
        public delegate void OnCompledCallBack(object value);

        public event OnCompledCallBack OnCompled;

        public string result;

        public ResultBase()
            : base() { }

        public ResultBase(string type)
           : base(type, "") { }


        public abstract void SetValue(ReceiveMessage receiveMessage);

        protected void onCompled()
        {
            OnCompled?.Invoke(this);
        }

        public void OnError(System.Exception ex)
        {
            Log.Warning(this.GetType().ToString(), ex);
        }

        public void OnError(string kind)
        {
            Log.Error("Receive Message", kind);
        }

        public void OnError(string kind, string message)
        {
            Log.Error("Resutl", kind + " : " + message);
        }
    }
}
