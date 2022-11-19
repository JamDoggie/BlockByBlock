using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlockByBlockShared.networking
{
    public class BinaryReaderNet : BinaryReader
    {
        private ByteOrder byteOrder;

        public BinaryReaderNet(Stream input, ByteOrder byteOrder) : base(input)
        {
            this.byteOrder = byteOrder;
        }

        public override short ReadInt16()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(2);
                Array.Reverse(bytes);
                return BitConverter.ToInt16(bytes, 0);
            }
            else
            {
                return base.ReadInt16();
            }
        }

        public override int ReadInt32()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(4);
                Array.Reverse(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }
            else
            {
                return base.ReadInt32();
            }
        }

        public override long ReadInt64()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            { 
                byte[] bytes = base.ReadBytes(8);
                Array.Reverse(bytes);
                return BitConverter.ToInt64(bytes, 0);
            }
            else
            {
                return base.ReadInt64();
            }
        }

        public override ushort ReadUInt16()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(2);
                Array.Reverse(bytes);
                return BitConverter.ToUInt16(bytes, 0);
            }
            else 
            { 
                return base.ReadUInt16();
            }
        }

        public override uint ReadUInt32()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(4);
                Array.Reverse(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }
            else
            {
                return base.ReadUInt32();
            }
        }

        public override ulong ReadUInt64()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(8);
                Array.Reverse(bytes);
                return BitConverter.ToUInt64(bytes, 0);
            }
            else
            {
                return base.ReadUInt64();
            }
        }

        public override float ReadSingle()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(4);
                Array.Reverse(bytes);
                return BitConverter.ToSingle(bytes, 0);
            }
            else
            {
                return base.ReadSingle();
            }
        }

        public override double ReadDouble()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(8);
                Array.Reverse(bytes);
                return BitConverter.ToDouble(bytes, 0);
            }
            else
            {
                return base.ReadDouble();
            }
        }

        public override char ReadChar()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(2);
                Array.Reverse(bytes);
                return BitConverter.ToChar(bytes, 0);
            }
            else
            {
                return base.ReadChar();
            }
        }

        /// <summary>
        /// Reads a string from the stream using Java's modified UTF-8 format.
        /// </summary>
        /// <returns>the string read from the stream.</returns>
        public override string ReadString()
        {
            return ReadUTF();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                int read = base.Read(buffer, index, count);
                for (int i = 0; i < count; i++)
                {
                    char ch = buffer[index + i];
                    char rev = (char)(((ch & 0xFF) << 8) | ((ch & 0xFF00) >> 8));
                    buffer[index + i] = rev;
                }
                return read;
            }
            else
            {
                return base.Read(buffer, index, count);
            }
        }

        public override int Read(byte[] buffer, int index, int count)
        {
            return base.Read(buffer, index, count);
        }

        public override int Read(Span<char> buffer)
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                int read = base.Read(buffer);
                for (int i = 0; i < read; i++)
                {
                    char ch = buffer[i];
                    char rev = (char)(((ch & 0xFF) << 8) | ((ch & 0xFF00) >> 8));
                    buffer[i] = rev;
                }
                return read;
            }
            else
            {
                return base.Read(buffer);
            }
        }

        public override int Read(Span<byte> buffer)
        {
            return base.Read(buffer);
        }

        public override byte ReadByte()
        {
            return base.ReadByte();
        }

        public override bool ReadBoolean()
        {
            return base.ReadBoolean();
        }

        public override int Read()
        {
            return base.Read();
        }

        public override byte[] ReadBytes(int count)
        {
            return base.ReadBytes(count);
        }

        public override char[] ReadChars(int count)
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                char[] chars = base.ReadChars(count);
                for (int i = 0; i < count; i++)
                {
                    char ch = chars[i];
                    char rev = (char)(((ch & 0xFF) << 8) | ((ch & 0xFF00) >> 8));
                    chars[i] = rev;
                }
                return chars;
            }
            else
            {
                return base.ReadChars(count);
            }
        }

        public override int PeekChar()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                int ch = base.PeekChar();
                if (ch == -1)
                {
                    return -1;
                }
                else
                {
                    return ((ch & 0xFF) << 8) | ((ch & 0xFF00) >> 8);
                }
            }
            else
            {
                return base.PeekChar();
            }
        }

        public override decimal ReadDecimal()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                int[] bits = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    bits[i] = ReadInt32();
                }
                return new decimal(bits);
            }

            return base.ReadDecimal();
        }

        public override Half ReadHalf()
        {
            if (byteOrder == ByteOrder.BIG_ENDIAN)
            {
                byte[] bytes = base.ReadBytes(2);
                Array.Reverse(bytes);
                return BitConverter.ToHalf(bytes, 0);
            }

            return base.ReadHalf();
        }

        /// <summary>
        /// Reads a UTF string that was written in java using DataOutputStream.writeUTF()
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public string ReadUTF()
        {
            ushort utflen = ReadUInt16();

            byte[] bytes = new byte[utflen];

            int c, char2, char3;
            int count = 0;
            int chararr_count = 0;

            for (int i = 0; i < utflen; i++)
            {
                bytes[i] = ReadByte();
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
    }
}
