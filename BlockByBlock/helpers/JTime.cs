using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.helpers
{
    public static class JTime
    {
        public static long NanoTime()
        {
            return DateTime.Now.Ticks * 100L;
        }
    }
}
