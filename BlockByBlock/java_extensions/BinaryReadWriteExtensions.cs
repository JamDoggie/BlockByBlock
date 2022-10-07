using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.java_extensions
{
    public static class BinaryReadWriteExtensions
    {
        
        /// <summary>
        /// Reads a UTF string that was written in java using DataOutputStream.writeUTF()
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static string ReadUTF(this BinaryReader reader)
        {
            ushort utflen = reader.ReadUInt16BigEndian();

            byte[] bytes = new byte[utflen];

            int c, char2, char3;
            int count = 0;
            int chararr_count = 0;
            
            for (int i = 0; i < utflen; i++)
            {
                bytes[i] = reader.ReadByte();
            }

            char[] chararr = new char[utflen * 2];

            while (count < utflen)
            {
                c = (int)bytes[count] & 0xff;
                if (c > 127) break;
                count++;
                chararr[chararr_count++]=(char)c;
            }

            while (count < utflen)
            {
                c = (int)bytes[count] & 0xff;
                switch (c >> 4)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        /* 0xxxxxxx*/
                        count++;
                        chararr[chararr_count++]=(char)c;
                        break;
                    case 12:
                    case 13:
                        /* 110x xxxx   10xx xxxx*/
                        count += 2;
                        if (count > utflen)
                            throw new FormatException(
                                "malformed input: partial character at end");
                        char2 = (int)bytes[count-1];
                        if ((char2 & 0xC0) != 0x80)
                            throw new FormatException(
                                "malformed input around byte " + count);
                        chararr[chararr_count++]=(char)(((c & 0x1F) << 6) |
                                                        (char2 & 0x3F));
                        break;
                    case 14:
                        /* 1110 xxxx  10xx xxxx  10xx xxxx */
                        count += 3;
                        if (count > utflen)
                            throw new FormatException(
                                "malformed input: partial character at end");
                        char2 = (int)bytes[count-2];
                        char3 = (int)bytes[count-1];
                        if (((char2 & 0xC0) != 0x80) || ((char3 & 0xC0) != 0x80))
                            throw new FormatException(
                                "malformed input around byte " + (count-1));
                        chararr[chararr_count++]=(char)(((c     & 0x0F) << 12) |
                                                        ((char2 & 0x3F) << 6)  |
                                                        ((char3 & 0x3F) << 0));
                        break;
                    default:
                        /* 10xx xxxx,  1111 xxxx */
                        throw new FormatException(
                            "malformed input around byte " + count);
                }
            }
            string str = new string(chararr, 0, chararr_count);
            return str;
        }

        /// <summary>
        /// Writes a UTF string in a format that can be read by java using DataInputStream.readUTF() or the ReadUTF() extension method.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int WriteUTF(this BinaryWriter writer, string str)
        {
            int strlen = str.Length;
            int utflen = 0;
            int c, count = 0;

            /* use charAt instead of copying String to char array */
            for ( int j = 0; j < strlen; j++)
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
            writer.Write(bytearr, 0, utflen + 2);
            return utflen + 2;
        }


        /// READING BIG ENDIAN \\\
        // Float
        public static unsafe float ReadSingleBigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[4];

            for (int i = 0; i < 4; i++)
                bytes[i] = reader.ReadByte();
            
            return BinaryPrimitives.ReadSingleBigEndian(new Span<byte>(bytes, 4));
        }

        // Double
        public static unsafe double ReadDoubleBigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[8];
            
            for (int i = 0; i < 8; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadDoubleBigEndian(new Span<byte>(bytes, 8));
        }

        // Int16
        public static unsafe short ReadInt16BigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[2];

            for (int i = 0; i < 2; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadInt16BigEndian(new Span<byte>(bytes, 2));
        }

        // Int32
        public static unsafe int ReadInt32BigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[4];
            
            for (int i = 0; i < 4; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadInt32BigEndian(new Span<byte>(bytes, 4));
        }

        // Int64
        public static unsafe long ReadInt64BigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[8];

            for (int i = 0; i < 8; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadInt64BigEndian(new Span<byte>(bytes, 8));
        }

        // UInt16
        public static unsafe ushort ReadUInt16BigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[2];

            for (int i = 0; i < 2; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadUInt16BigEndian(new Span<byte>(bytes, 2));
        }

        // UInt32
        public static unsafe uint ReadUInt32BigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[4];

            for (int i = 0; i < 4; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadUInt32BigEndian(new Span<byte>(bytes, 4));
        }

        // UInt64
        public static unsafe ulong ReadUInt64BigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[8];

            for (int i = 0; i < 8; i++)
                bytes[i] = reader.ReadByte();

            return BinaryPrimitives.ReadUInt64BigEndian(new Span<byte>(bytes, 8));
        }

        // Char
        public static unsafe char ReadCharBigEndian(this BinaryReader reader)
        {
            byte* bytes = stackalloc byte[2];
            
            for (int i = 0; i < 2; i++)
                bytes[i] = reader.ReadByte();

            byte b0 = bytes[0];
            byte b1 = bytes[1];

            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = b1;
                bytes[1] = b0;
            }

            return BitConverter.ToChar(new ReadOnlySpan<byte>(bytes, 2));
        }

        // Chars
        public static unsafe char[] ReadCharsBigEndian(this BinaryReader reader, int count)
        {
            char[] chars = new char[count];
            
            for(int i = 0; i < count; i++)
            {
                chars[i] = reader.ReadCharBigEndian();
            }

            return chars;
        }

        /// WRITING BIG ENDIAN \\\
        // Float
        public static unsafe void WriteBigEndian(this BinaryWriter writer, float value)
        {
            byte* bytes = stackalloc byte[4];
            BinaryPrimitives.WriteSingleBigEndian(new Span<byte>(bytes, 4), value);

            for (int i = 0; i < 4; i++)
                writer.Write(bytes[i]);
        }

        // Double
        public static unsafe void WriteBigEndian(this BinaryWriter writer, double value)
        {
            byte* bytes = stackalloc byte[8];
            BinaryPrimitives.WriteDoubleBigEndian(new Span<byte>(bytes, 8), value);

            for (int i = 0; i < 8; i++)
                writer.Write(bytes[i]);
        }

        // Int16
        public static unsafe void WriteBigEndian(this BinaryWriter writer, short value)
        {
            byte* bytes = stackalloc byte[2];
            BinaryPrimitives.WriteInt16BigEndian(new Span<byte>(bytes, 2), value);

            for (int i = 0; i < 2; i++)
                writer.Write(bytes[i]);
        }

        // Int32
        public static unsafe void WriteBigEndian(this BinaryWriter writer, int value)
        {
            byte* bytes = stackalloc byte[4];
            BinaryPrimitives.WriteInt32BigEndian(new Span<byte>(bytes, 4), value);

            for (int i = 0; i < 4; i++)
                writer.Write(bytes[i]);
        }

        // Int64
        public static unsafe void WriteBigEndian(this BinaryWriter writer, long value)
        {
            byte* bytes = stackalloc byte[8];
            BinaryPrimitives.WriteInt64BigEndian(new Span<byte>(bytes, 8), value);

            for (int i = 0; i < 8; i++)
                writer.Write(bytes[i]);
        }

        // UInt16
        public static unsafe void WriteBigEndian(this BinaryWriter writer, ushort value)
        {
            byte* bytes = stackalloc byte[2];
            BinaryPrimitives.WriteUInt16BigEndian(new Span<byte>(bytes, 2), value);

            for (int i = 0; i < 2; i++)
                writer.Write(bytes[i]);
        }

        // UInt32
        public static unsafe void WriteBigEndian(this BinaryWriter writer, uint value)
        {
            byte* bytes = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(new Span<byte>(bytes, 4), value);

            for (int i = 0; i < 4; i++)
                writer.Write(bytes[i]);
        }

        // UInt64
        public static unsafe void WriteBigEndian(this BinaryWriter writer, ulong value)
        {
            byte* bytes = stackalloc byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(new Span<byte>(bytes, 8), value);

            for (int i = 0; i < 8; i++)
                writer.Write(bytes[i]);
        }

        // Char
        public static unsafe void WriteBigEndian(this BinaryWriter writer, char value)
        {
            byte* bytes = stackalloc byte[2];
            char* chr = stackalloc char[1];
            chr[0] = value;
            Encoding.Unicode.GetBytes(chr, 1, bytes, 2);

            byte b0 = bytes[0];
            byte b1 = bytes[1];

            if (BitConverter.IsLittleEndian)
            {
                bytes[0] = b1;
                bytes[1] = b0;
            }

            for (int i = 0; i < 2; i++)
            {
                byte b = bytes[i];
                writer.Write(b);
            }
                
        }

        // Chars
        public static unsafe void WriteBigEndian(this BinaryWriter writer, char[] value)
        {
            for (int i = 0; i < value.Length; i++)
                writer.WriteBigEndian(value[i]);
        }

        private static ushort ReverseBytes(ushort value)
        {
            return (ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }
    }
}
