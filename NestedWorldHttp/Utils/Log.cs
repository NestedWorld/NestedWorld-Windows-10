using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorldHttp.Utils
{
    public class Log
    {
        internal static void Info(params object[] param)
        {
            Debug.Write("[" + DateTime.Now.ToLocalTime().ToString() + "][INFO]");
            foreach (object obj in param)
            {
                Debug.Write(obj, " ");
            }
            Debug.WriteLine("");
        }
        internal static void Warning(params object[] param)
        {

            Debug.Write("[" + DateTime.Now.ToLocalTime().ToString() + "][WARNING]");
            foreach (object obj in param)
            {
                Debug.Write(obj, " ");
            }
            Debug.WriteLine("");
        }
        internal static void Error(params object[] param)
        {
            Debug.Write("[" + DateTime.Now.ToLocalTime().ToString() + "][ERROR]");
            foreach (object obj in param)
            {
                Debug.Write(obj, " ");
            }
            Debug.WriteLine("");
        }

    }
}
