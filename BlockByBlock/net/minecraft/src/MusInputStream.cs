using System;
using System.IO;

namespace net.minecraft.src
{

	internal class MusInputStream : Stream
	{
		private int hash;
		private Stream inputStream;
		internal sbyte[] buffer;
		internal readonly CodecMus codec;

		public MusInputStream(CodecMus codecMus1, URL uRL2, Stream inputStream3)
		{
			this.codec = codecMus1;
			this.buffer = new sbyte[1];
			this.inputStream = inputStream3;
			string string4 = uRL2.getPath();
			string4 = string4.Substring(string4.LastIndexOf("/", StringComparison.Ordinal) + 1);
			this.hash = string4.GetHashCode();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public int read() throws java.io.IOException
		public virtual int read()
		{
			int i1 = this.Read(this.buffer, 0, 1);
			return i1 < 0 ? i1 : this.buffer[0];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public int read(byte[] b1, int i2, int i3) throws java.io.IOException
		public virtual int read(sbyte[] b1, int i2, int i3)
		{
			i3 = this.inputStream.Read(b1, i2, i3);

			for (int i4 = 0; i4 < i3; ++i4)
			{
				sbyte b5 = b1[i2 + i4] = (sbyte)(b1[i2 + i4] ^ this.hash >> 8);
				this.hash = this.hash * 498729871 + 85731 * b5;
			}

			return i3;
		}
	}

}