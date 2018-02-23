using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public static class DateTimeHelper
    {

        public static DateTime GetDateWithTimeNow(DateTime date)
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;
            
            DateTime combined = date.Add(timeNow);

            return combined;
        }
    }
}
