using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.helpers
{
    public static class DebugHelpers
    {
        public static void PrintStackTrace()
        {
            /*var stackTrace = new System.Diagnostics.StackTrace();
            var stackFrames = stackTrace.GetFrames();
            foreach (var stackFrame in stackFrames)
            {
                Console.WriteLine(stackFrame.GetMethod().Name);
            }*/ // This code may or may not be better. PORTING TODO: investigate this.

            Console.WriteLine($"StackTrace: {Environment.StackTrace}");
        }
    }
}
