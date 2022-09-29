using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class WorldGenFire : WorldGenerator
	{
		public override bool generate(World world1, RandomExtended random2, int i3, int i4, int i5)
		{
			for (int i6 = 0; i6 < 64; ++i6)
			{
				int i7 = i3 + random2.Next(8) - random2.Next(8);
				int i8 = i4 + random2.Next(4) - random2.Next(4);
				int i9 = i5 + random2.Next(8) - random2.Next(8);
				if (world1.isAirBlock(i7, i8, i9) && world1.getBlockId(i7, i8 - 1, i9) == Block.netherrack.blockID)
				{
					world1.setBlockWithNotify(i7, i8, i9, Block.fire.blockID);
				}
			}

			return true;
		}
	}

}