using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockObsidian : BlockStone
	{
		public BlockObsidian(int i1, int i2) : base(i1, i2)
		{
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 1;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Block.obsidian.blockID;
		}
	}

}