using System;
using BlockByBlock.java_extensions;	

namespace net.minecraft.src
{

	public class WorldGenVines : WorldGenerator
	{
		public override bool generate(World world1, RandomExtended random2, int i3, int i4, int i5)
		{
			int i6 = i3;

			for (int i7 = i5; i4 < 128; ++i4)
			{
				if (world1.isAirBlock(i3, i4, i5))
				{
					for (int i8 = 2; i8 <= 5; ++i8)
					{
						if (Block.vine.canPlaceBlockOnSide(world1, i3, i4, i5, i8))
						{
							world1.setBlockAndMetadata(i3, i4, i5, Block.vine.blockID, 1 << Direction.vineGrowth[Facing.faceToSide[i8]]);
							break;
						}
					}
				}
				else
				{
					i3 = i6 + random2.Next(4) - random2.Next(4);
					i5 = i7 + random2.Next(4) - random2.Next(4);
				}
			}

			return true;
		}
	}

}