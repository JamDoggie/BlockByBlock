using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class MapGenBase
	{
		protected internal int range = 8;
		protected internal RandomExtended rand = new RandomExtended();
		protected internal World worldObj;

		public virtual void generate(IChunkProvider iChunkProvider1, World world2, int i3, int i4, sbyte[] b5)
		{
			int i6 = this.range;
			worldObj = world2;
			rand.SetSeed(world2.Seed);
			long j7 = rand.NextInt64();
			long j9 = rand.NextInt64();

			for (int i11 = i3 - i6; i11 <= i3 + i6; ++i11)
			{
				for (int i12 = i4 - i6; i12 <= i4 + i6; ++i12)
				{
					long j13 = (long)i11 * j7;
					long j15 = (long)i12 * j9;
                    rand.SetSeed(j13 ^ j15 ^ world2.Seed);
                    this.recursiveGenerate(world2, i11, i12, i3, i4, b5);
				}
			}

		}

		protected internal virtual void recursiveGenerate(World world1, int i2, int i3, int i4, int i5, sbyte[] b6)
		{
		}
	}

}