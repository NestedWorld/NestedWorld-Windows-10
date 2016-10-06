using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Exception
{
    public class InvalideMapKeyException : System.Exception
    {
        private const string MESSAGE = "Invalid Key ({0}), item not found on Dictionary";

        public InvalideMapKeyException(string key) 
            : base(string.Format(MESSAGE, key)) { }
    }
}
