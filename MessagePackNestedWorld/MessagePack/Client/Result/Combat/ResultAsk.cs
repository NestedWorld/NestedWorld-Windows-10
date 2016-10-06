using MessagePack.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MsgPack.Serialization;
using MessagePackNestedWorld.Utils;

namespace MessagePackNestedWorld.MessagePack.Client.Result.Combat
{
    /*public class CustomResultAskSuccessSerializer : MessagePackSerializer<ResultAskSuccess>
    {

    }*/

    public class ResultAskSuccess : RequestBase
    {
        public string result = "success";
        public bool accept { get; set; }

        public int[] monsters { get; set; }
       
        public ResultAskSuccess()
            : base("result")
        {

        }
         
        public ResultAskSuccess(string Id) 
            : base("result")
        {
            this.id = Id;
        }

        public override MemoryStream GetStream()
        {
            try
            {
                var context = new SerializationContext();
                context.SerializationMethod = SerializationMethod.Map;
                var serializer = MessagePackSerializer.Get<ResultAskSuccess>(context);

                MemoryStream stream = new MemoryStream();
                serializer.Pack(stream, this);
                stream.Position = 0;
                return stream;
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                Log.Error("GetStream", ex);
            }
            return null;
        }

        public static ResultAskSuccess Awnser(string Id, bool Accept, int[] Monsters)
        {
            return new ResultAskSuccess(Id) { accept = Accept , monsters = Monsters };
        }
    }
}
