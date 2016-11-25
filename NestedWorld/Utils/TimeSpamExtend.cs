using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Utils
{
    public static class TimeSpamExtend
    {
        public static string ToPretyString(this TimeSpan time)
        {
            var ret = "";

            ret += time.Seconds + " sec";
            if (time.Minutes != 0)
            {
                ret = time.Minutes + " min " + ret;
                if (time.Hours != 0)
                {
                    ret = time.Hours + " h " + ret ;
                }
            }
            return ret;
        }
    }
}
