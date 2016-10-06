using NestedWorld.Classes.Network.MessagePack.Serveur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Network.MessagePack.Client.Answers
{
    public abstract class AnswerBase
    {
        public delegate void OnCompledCallBack(object value);

        public event OnCompledCallBack OnCompled;


        public AnswerBase()
        {
           
        }


        public abstract void SetValue(ResultRequest result);

        public void OnError(System.Exception ex)
        {
            Utils.Log.Warning(this.GetType().ToString(), ex);
        }
    }
}
