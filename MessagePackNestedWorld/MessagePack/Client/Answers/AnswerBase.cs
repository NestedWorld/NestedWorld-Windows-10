using MessagePack.Serveur;
using MessagePackNestedWorld.Utils;

namespace MessagePack.Client.Answers
{
    public abstract class AnswerBase
    {
        public delegate void OnCompledCallBack(object value);

        public event OnCompledCallBack OnCompled;

        public const string SUCCESS = "success";
        public const string ERROR = "error";
        public string ID;
        public ClientMessageList clientMessageList { get; set; }

        public AnswerBase()
        {

        }

        public void onCompled()
        {
            Log.Info("onCompled", "AnswerBase");

          //  clientMessageList.Get(this.ID).Call();

            OnCompled?.Invoke(this);
        }


        public abstract void SetValue(ResultRequest result);

        public void OnError(System.Exception ex)
        {
            Log.Warning(this.GetType().ToString(), ex);
        }
    }
}
