using System;

namespace net.minecraft.src
{

	public class MapGenBase
	{
		protected internal int range = 8;
		protected internal Random rand = new Random();
		protected internal World worldObj;

		public virtual void generate(IChunkProvider iChunkProvider1, World world2, int i3, int i4, sbyte[] b5)
		{
			int i6 = this.range;
			this.worldObj = world2;
			this.rand.setSeed(world2.Seed);
			long j7 = this.rand.nextLong();
			long j9 = this.rand.nextLong();

			for (int i11 = i3 - i6; i11 <= i3 + i6; ++i11)
			{
				for (int i12 = i4 - i6; i12 <= i4 + i6; ++i12)
				{
					long j13 = (long)i11 * j7;
					long j15 = (long)i12 * j9;
					this.rand.setSeed(j13 ^ j15 ^ world2.Seed);
					this.recursiveGenerate(world2, i11, i12, i3, i4, b5);
				}
			}

		}

		protected internal virtual void recursiveGenerate(World world1, int i2, int i3, int i4, int i5, sbyte[] b6)
		{
		}
	}

}