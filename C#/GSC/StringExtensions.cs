using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSC
{
    internal static class StringExtensions
    {
        public static string DateTimeToString(this DateTime datetime, bool withHours = false)
        {
            return datetime.ToString(withHours ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd");
        }
    }
}
