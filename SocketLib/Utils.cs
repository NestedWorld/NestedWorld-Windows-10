using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketLib
{
    public static class Utils
    {
        public static void PrintBytes(byte[] obj)
        {
            foreach (byte b in obj)
            {
                Debug.Write(b);
                Debug.Write(" ");
            }
            Debug.Write("\n");
        }
    }
}
