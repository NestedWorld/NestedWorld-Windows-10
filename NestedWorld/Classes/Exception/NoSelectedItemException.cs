using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Classes.Exception
{
    public class NoSelectedItemException : System.Exception
    {
        private const string message = "No Item Selected";

        public NoSelectedItemException() : base(message) { }
    }
}
