using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlockByBlockShared.networking
{
    public class BinaryWriterNet : BinaryWriter
    {
        public ByteOrder ByteOrder { get; set; } = ByteOrder.BIG_ENDIAN;

        public BinaryWriterNet(Stream output) : base(output)
        {
            
        }

        public override void Write(bool value)
        {
            base.Write(value);
        }

        public override void Write(byte value)
        {
            base.Write(value);
        }

        public override void Write(byte[] buffer)
        {
            base.Write(buffer);
        }

        public override void Write(byte[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
        }

        public override void Write(char ch)
        {
            base.Write(ch);
        }

        public override void Write(char[] chars)
        {
            unsafe
            {
                if (ByteOrder == ByteOrder.BIG_ENDIAN)
                {
                    for (int i = 0; i < chars.Length; i++)
                    {
                        char ch = chars[i];
                        char rev = (char)(((ch & 0xFF) << 8) | ((ch & 0xFF00) >> 8));
                        chars[i] = rev;
                    }
                }
            }
            
            base.Write(chars);
        }

        public override void Write(char[] chars, int index, int count)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                for (int i = 0; i < count; i++)
                {
                    char ch = chars[index + i];
                    char rev = (char)(((ch & 0xFF) << 8) | ((ch & 0xFF00) >> 8));
                    chars[index + i] = rev;
                }
            }

            base.Write(chars, index, count);
        }

        object decimalWriteLock = new();
        int[] decimalBits = new int[4];

        public override void Write(decimal value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                lock(decimalWriteLock) // Because we're using a precached array, we need to ensure two threads don't try to use the cached array at the same time.
                {
                    decimal.GetBits(value, decimalBits);
                    for (int i = 0; i < decimalBits.Length; i++)
                    {
                        decimalBits[i] = BinaryPrimitives.ReverseEndianness(decimalBits[i]);
                    }

                    // Convert bits back to a decimal
                    value = new decimal(decimalBits);
                }
            }

            base.Write(value);
        }

        public override void Write(double value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                long bits = BitConverter.DoubleToInt64Bits(value);
                bits = BinaryPrimitives.ReverseEndianness(bits); // Why is there no overload for doubles?!?!?!?
                value = BitConverter.Int64BitsToDouble(bits);
            }
            
            base.Write(value);
        }

        public override void Write(float value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                int bits = BitConverter.SingleToInt32Bits(value);
                bits = BinaryPrimitives.ReverseEndianness(bits);
                value = BitConverter.Int32BitsToSingle(bits);
            }

            base.Write(value);
        }

        public override void Write(int value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                value = BinaryPrimitives.ReverseEndianness(value);
            }

            base.Write(value);
        }

        public override void Write(long value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                value = BinaryPrimitives.ReverseEndianness(value);
            }

            base.Write(value);
        }

        public override void Write(sbyte value)
        {
            base.Write(value);
        }

        public override void Write(short value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                value = BinaryPrimitives.ReverseEndianness(value);
            }

            base.Write(value);
        }

        /// <summary>
        /// Writes a string to the stream using Java's modified UTF-8 encoding.
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            WriteUTF(value);
        }

        public override void Write(uint value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                value = BinaryPrimitives.ReverseEndianness(value);
            }

            base.Write(value);
        }

        public override void Write(ulong value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                value = BinaryPrimitives.ReverseEndianness(value);
            }

            base.Write(value);
        }

        public override void Write(ushort value)
        {
            if (ByteOrder == ByteOrder.BIG_ENDIAN)
            {
                value = BinaryPrimitives.ReverseEndianness(value);
            }

            base.Write(value);
        }

        /// <summary>
        /// Writes a UTF string in a format that can be read by java using DataInputStream.readUTF() or the ReadUTF() extension method.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public int WriteUTF(string str)
        {
            int strlen = str.Length;
            int utflen = 0;
            int c, count = 0;

            /* use charAt instead of copying String to char array */
            for (int j = 0; j < strlen; j++)
            {
                c = str[j];
                if ((c >= 0x0001) && (c <= 0x007F))
                {
                    utflen++;
                }
                else if (c > 0x07FF)
                {
                    utflen += 3;
                }
                else
                {
                    utflen += 2;
                }
            }

            if (utflen > 65535)
                throw new FormatException(
                    "encoded string too long: " + utflen + " bytes");

            byte[] bytearr = new byte[(utflen*2) + 2];

            bytearr[count++] = (byte)(((ushort)utflen >> 8) & 0xFF);
            bytearr[count++] = (byte)(((ushort)utflen >> 0) & 0xFF);

            int i;
            for (i = 0; i < strlen; i++)
            {
                c = str[i];
                if (!((c >= 0x0001) && (c <= 0x007F))) break;
                bytearr[count++] = (byte)c;
            }

            for (; i < strlen; i++)
            {
                c = str[i];
                if ((c >= 0x0001) && (c <= 0x007F))
                {
                    bytearr[count++] = (byte)c;

                }
                else if (c > 0x07FF)
                {
                    bytearr[count++] = (byte)(0xE0 | ((c >> 12) & 0x0F));
                    bytearr[count++] = (byte)(0x80 | ((c >>  6) & 0x3F));
                    bytearr[count++] = (byte)(0x80 | ((c >>  0) & 0x3F));
                }
                else
                {
                    bytearr[count++] = (byte)(0xC0 | ((c >>  6) & 0x1F));
                    bytearr[count++] = (byte)(0x80 | ((c >>  0) & 0x3F));
                }
            }
            Write(bytearr, 0, utflen + 2);
            return utflen + 2;
        }
    }
}
