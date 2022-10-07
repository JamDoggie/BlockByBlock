using System;
using System.IO;

namespace net.minecraft.src
{

	/*internal class MusInputStream
	{
		private int hash;
		private Stream inputStream;
		internal byte[] buffer;
		internal readonly CodecMus codec;

		public MusInputStream(CodecMus codecMus1, Uri uRL2, Stream inputStream3)
		{
			codec = codecMus1;
			buffer = new byte[1];
			inputStream = inputStream3;
			string string4 = uRL2.AbsolutePath;
			string4 = string4.Substring(string4.LastIndexOf("/", StringComparison.Ordinal) + 1);
			this.hash = string4.GetHashCode();
		}
        
		public virtual int read()
		{
			int i1 = read(buffer, 0, 1);
			return i1 < 0 ? i1 : buffer[0];
		}
        
		public virtual int read(byte[] b1, int i2, int i3)
		{
			i3 = this.inputStream.Read(b1, i2, i3);

			for (int i4 = 0; i4 < i3; ++i4)
			{
				byte b5 = b1[i2 + i4] = (byte)(b1[i2 + i4] ^ this.hash >> 8);
				this.hash = this.hash * 498729871 + 85731 * b5;
			}

			return i3;
		}
	}*/

}