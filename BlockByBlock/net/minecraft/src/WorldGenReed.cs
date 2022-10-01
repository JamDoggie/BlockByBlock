using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class WorldGenReed : WorldGenerator
	{
		public override bool generate(World world1, RandomExtended random2, int i3, int i4, int i5)
		{
			for (int i6 = 0; i6 < 20; ++i6)
			{
				int i7 = i3 + random2.Next(4) - random2.Next(4);
				int i8 = i4;
				int i9 = i5 + random2.Next(4) - random2.Next(4);
				if (world1.isAirBlock(i7, i4, i9) && (world1.getBlockMaterial(i7 - 1, i4 - 1, i9) == Material.water || world1.getBlockMaterial(i7 + 1, i4 - 1, i9) == Material.water || world1.getBlockMaterial(i7, i4 - 1, i9 - 1) == Material.water || world1.getBlockMaterial(i7, i4 - 1, i9 + 1) == Material.water))
				{
					int i10 = 2 + random2.Next(random2.Next(3) + 1);

					for (int i11 = 0; i11 < i10; ++i11)
					{
						if (Block.reed.canBlockStay(world1, i7, i8 + i11, i9))
						{
							world1.setBlock(i7, i8 + i11, i9, Block.reed.blockID);
						}
					}
				}
			}

			return true;
		}
	}

}