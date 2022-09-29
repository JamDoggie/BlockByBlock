using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.helpers
{
    public static class JTypes
    {
        public static int FloatToRawIntBits(float f)
        {
            FloatToIntConverter converter = new()
            {
                FloatValue = f
            }; // Saw this on stackoverflow. Neat trick.

            return converter.IntValue;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct FloatToIntConverter
    {
        [FieldOffset(0)]
        public int IntValue;
        [FieldOffset(0)]
        public float FloatValue;
    }
}
