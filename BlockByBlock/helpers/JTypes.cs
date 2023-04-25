using sun.tools.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
            };

            return converter.IntValue;
        }

        public static byte SByteToRawByteBits(sbyte sb)
        {
            ByteSByteUnion converter = new()
            {
                sb = sb
            };
            
            return converter.b;
        }

        public static sbyte ByteToRawSByteBits(byte b)
        {
            ByteSByteUnion converter = new()
            {
                b = b
            };

            return converter.sb;
        }

        public static long JavaParseLong(string s, int radix)
        {
            if (s == null)
            {
                throw new FormatException("null");
            }

            if (radix < 2)
            {
                throw new FormatException("radix " + radix +
                                                " less than Character.MIN_RADIX");
            }
            if (radix > 36)
            {
                throw new FormatException("radix " + radix +
                                                " greater than Character.MAX_RADIX");
            }

            long result = 0;
            bool negative = false;
            int i = 0, len = s.Length;
            long limit = -long.MaxValue;
            long multmin;
            int digit;

            if (len > 0)
            {
                char firstChar = s[0];
                if (firstChar < '0')
                { // Possible leading "+" or "-"
                    if (firstChar == '-')
                    {
                        negative = true;
                        limit = long.MinValue;
                    }
                    else if (firstChar != '+')
                        throw new FormatException(s);

                    if (len == 1) // Cannot have lone "+" or "-"
                        throw new FormatException(s);
                    i++;
                }
                multmin = limit / radix;
                while (i < len)
                {
                    // Accumulating negatively avoids surprises near MAX_VALUE
                    digit = Convert.ToInt32(s.ElementAt(i++).ToString(), radix);
                    if (digit < 0)
                    {
                        throw new FormatException(s);
                    }
                    if (result < multmin)
                    {
                        throw new FormatException(s);
                    }
                    result *= radix;
                    if (result < limit + digit)
                    {
                        throw new FormatException(s);
                    }
                    result -= digit;
                }
            }
            else
            {
                throw new FormatException(s);
            }
            return negative ? result : -result;
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

    [StructLayout(LayoutKind.Explicit)]
    public struct ByteSByteUnion
    {
        [FieldOffset(0)]
        public byte b;
        [FieldOffset(0)]
        public sbyte sb;
    }
}
