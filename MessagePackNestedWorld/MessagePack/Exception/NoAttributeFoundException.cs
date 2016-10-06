namespace MessagePack.Exception
{
    public class NoAttributeFoundException : System.Exception
    {
        private const string messagebase = "the Attribute ({0}) researching is not found ";

        public NoAttributeFoundException(string attributeName) :
            base (string.Format(messagebase, attributeName))
        { }
    }
}
