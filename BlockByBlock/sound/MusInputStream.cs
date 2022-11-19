using BlockByBlock.helpers;
using com.sun.org.apache.xml.@internal.dtm.@ref;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlock.sound
{
    public class MusInputStream : FileStream
    {
        private int _hash;
        private int _originalHash;

        public MusInputStream(string path, FileMode mode) : base(path, mode)
        {
            FileInfo fileInfo = new(path);
            _originalHash = new JString(fileInfo.Name).hashCode();
            _hash = _originalHash;
        }

        public override int ReadByte()
        {
            int b = base.ReadByte();
            
            if (b == -1)
                return b;

            ByteSByteUnion union = new() { b = (byte)b };

            sbyte result = Decode(union.sb);
            union.sb = result;
            return union.b;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = base.Read(buffer, offset, count);
            for (int i = 0; i < read; i++)
            {
                ByteSByteUnion union = new() { b = buffer[offset + i] };
                sbyte result = Decode(union.sb);
                union.sb = result;
                buffer[offset + i] = union.b;
            }

            return read;
        }

        public override int Read(Span<byte> buffer)
        {
            int read = base.Read(buffer);
            for (int i = 0; i < read; i++)
            {
                ByteSByteUnion union = new() { b = buffer[i] };
                sbyte result = Decode(union.sb);
                union.sb = result;
                buffer[i] = union.b;
            }

            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long oldPos = Position;

            long newPos = oldPos;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    newPos = offset;
                    break;
                case SeekOrigin.Current:
                    newPos = oldPos + offset;
                    break;
                case SeekOrigin.End:
                    newPos = Length + offset;
                    break;
            }

            if (newPos < 0)
                newPos = 0;
            else if (newPos > Length)
                newPos = Length;
            
            if (newPos > oldPos)
            {
                for (int i = 0; i < newPos - oldPos; i++)
                {
                    ReadByte();
                }
            }
            else if (newPos < oldPos)
            {
                base.Seek(0, SeekOrigin.Begin);
                _hash = _originalHash;
                
                for (int i = 0; i < newPos; i++)
                {
                    ReadByte();
                }
            }

            return newPos;
        }

        private sbyte Decode(sbyte b)
        {
            b = (sbyte)(b ^ _hash >> 8);
            _hash = _hash * 498729871 + 85731 * b;
            return b;
        }
    }
}
