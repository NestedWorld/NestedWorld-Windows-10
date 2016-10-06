using MessagePack.Client.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack.Serveur;
using MessagePack.Exception;
using MessagePackNestedWorld.Utils;

namespace MessagePackNestedWorld.MessagePack.Client.Answers.Combat
{
    public class CombatAskResult : AnswerBase
    {

        public string result { get; set; }
        public override void SetValue(ResultRequest Result)
        {
            try
            {
                this.result = Result.Content.GetString("result");
                this.ID = Result.Content.GetString("id");
                Log.Info("CombatAskResult", "setvalue");
                onCompled();
            }
            catch (NoAttributeFoundException ex)
            {
                OnError(ex);
            }
            catch (AttributeBadTypeException ex)
            {
                OnError(ex);
            }
            catch (AttributeEmptyException ex)
            {
                OnError(ex);
            }
        }
    }
}
