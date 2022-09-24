using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockStone : Block
	{
		public BlockStone(int i1, int i2) : base(i1, i2, Material.rock)
		{
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Block.cobblestone.blockID;
		}
	}

}