using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class WorldGenTallGrass : WorldGenerator
	{
		private int tallGrassID;
		private int tallGrassMetadata;

		public WorldGenTallGrass(int i1, int i2)
		{
			this.tallGrassID = i1;
			this.tallGrassMetadata = i2;
		}

		public override bool generate(World world1, RandomExtended random2, int i3, int i4, int i5)
		{
			int i11;
			for (bool z6 = false; ((i11 = world1.getBlockId(i3, i4, i5)) == 0 || i11 == Block.leaves.blockID) && i4 > 0; --i4)
			{
			}

			for (int i7 = 0; i7 < 128; ++i7)
			{
				int i8 = i3 + random2.Next(8) - random2.Next(8);
				int i9 = i4 + random2.Next(4) - random2.Next(4);
				int i10 = i5 + random2.Next(8) - random2.Next(8);
				if (world1.isAirBlock(i8, i9, i10) && ((BlockFlower)Block.blocksList[this.tallGrassID]).canBlockStay(world1, i8, i9, i10))
				{
					world1.setBlockAndMetadata(i8, i9, i10, this.tallGrassID, this.tallGrassMetadata);
				}
			}

			return true;
		}
	}

}