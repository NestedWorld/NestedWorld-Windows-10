using Network.MessagePack;

namespace MessagePack.Serveur
{
    public class ResultRequest : ResultBase
    {
        public const string ERROR = "error";
        public const string SUCCESS = "success";

        
        public ReceiveMessage Content;

        public ResultRequest()
            : base("result") { }

        public override void SetValue(ReceiveMessage receiveMessage)
        {

            try
            {
                this.result = receiveMessage.GetString("result");
                
                if (this.result == ERROR)
                    OnError(receiveMessage.GetString("kind"), receiveMessage.GetString("message"));
                id = receiveMessage.GetString("id");
                Content = receiveMessage;
                onCompled();
                return;
            }
            catch (Exception.NoAttributeFoundException ex)
            {
                OnError(ex);
            }
            catch (Exception.AttributeBadTypeException ex)
            {
                OnError(ex);
            }
            catch (Exception.AttributeEmptyException ex)
            {
                OnError(ex);
            }

        }
    }
}
