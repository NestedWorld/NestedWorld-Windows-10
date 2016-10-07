using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedWorld.Utils
{
    public static class DateTimeExtend
    {
        public static double ToTimestamp(this DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static DateTime FromTimestamp(this DateTime dateTime, double timestamp)
        {
            dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            dateTime.AddSeconds(timestamp);
            return dateTime;
        }
    }
}
