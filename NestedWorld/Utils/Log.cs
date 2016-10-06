using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Utils
{
    public class Log
    {
        internal static void Info(string header, object message)
        {
            Debug.WriteLine("[" + DateTime.Now.ToLocalTime().ToString() + "][INFO][" + header + "] " + message);
        }
        internal static void Warning(string header, object message)
        {
            Debug.WriteLine("[" + DateTime.Now.ToLocalTime().ToString() + "][WARNING][" + header + "] " + message);
        }
        internal static void Error(string header, object message)
        {
            Debug.WriteLine("[" + DateTime.Now.ToLocalTime().ToString() + "][ERROR][" + header + "] " + message);
        }

    }
}
