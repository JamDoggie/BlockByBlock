using System;
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
            int length = reader.ReadInt16();
            byte[] bytes = reader.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Writes a UTF string in a format that can be read by java using DataInputStream.readUTF() or the ReadUTF() extension method.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string WriteUTF(this BinaryWriter writer, string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            writer.Write((short)bytes.Length);
            writer.Write(bytes);
            return value;
        }
    }
}
