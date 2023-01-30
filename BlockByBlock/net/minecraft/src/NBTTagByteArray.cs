using BlockByBlock.java_extensions;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace net.minecraft.src
{

	public class NBTTagByteArray : NBTBase
	{
		public sbyte[] byteArray;

		public NBTTagByteArray(string string1) : base(string1)
		{
		}

		public NBTTagByteArray(string string1, sbyte[] b2) : base(string1)
		{
			this.byteArray = b2;
		}

		internal override void write(BinaryWriter dataOutput1)
		{
			dataOutput1.WriteBigEndian(this.byteArray.Length);

			ReadOnlySpan<sbyte> signedBytes = byteArray;
			ReadOnlySpan<byte> unsignedBytes = MemoryMarshal.Cast<sbyte, byte>(signedBytes);

			dataOutput1.Write(unsignedBytes);
		}
        
		internal override void load(BinaryReader dataInput1)
		{
			int len = dataInput1.ReadInt32BigEndian();
			sbyte[] sbytes = new sbyte[len];
			
			for(int i = 0; i < len; i++)
            {
				sbytes[i] = dataInput1.ReadSByte();
            }

			byteArray = sbytes;
		}

		public override sbyte Id
		{
			get
			{
				return 7;
			}
		}

		public override string ToString()
		{
			return "[" + this.byteArray.Length + " bytes]";
		}

		public override NBTBase copy()
		{
			sbyte[] b1 = new sbyte[this.byteArray.Length];
			Array.Copy(this.byteArray, 0, b1, 0, this.byteArray.Length);
			return new NBTTagByteArray(this.Name, b1);
		}

		public override bool Equals(object object1)
		{
			return base.Equals(object1) ? this.byteArray.SequenceEqual(((NBTTagByteArray)object1).byteArray) : false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ byteArray.GetHashCode();
		}
	}

}