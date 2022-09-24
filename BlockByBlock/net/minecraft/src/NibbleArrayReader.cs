namespace net.minecraft.src
{
	public class NibbleArrayReader
	{
		public readonly sbyte[] data;
		private readonly int depthBits;
		private readonly int depthBitsPlusFour;

		public NibbleArrayReader(sbyte[] b1, int i2)
		{
			this.data = b1;
			this.depthBits = i2;
			this.depthBitsPlusFour = i2 + 4;
		}

		public virtual int get(int i1, int i2, int i3)
		{
			int i4 = i1 << this.depthBitsPlusFour | i3 << this.depthBits | i2;
			int i5 = i4 >> 1;
			int i6 = i4 & 1;
			return i6 == 0 ? this.data[i5] & 15 : this.data[i5] >> 4 & 15;
		}
	}

}