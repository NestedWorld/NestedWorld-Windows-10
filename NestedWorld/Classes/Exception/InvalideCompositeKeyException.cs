using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Exception
{
    public class InvalideCompositeKeyException : System.Exception
    {
        private const string message = "Invalide key ({0}), can not find this composti on App Local Data";

        public InvalideCompositeKeyException(string key) :
            base(string.Format(message, key))
        {

        }
    }
}
