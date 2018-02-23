using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
  public  static class General
    {
        public static int IntParse(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return 0;
        }
    }

}
