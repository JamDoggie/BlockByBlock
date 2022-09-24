using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockClay : Block
	{
		public BlockClay(int i1, int i2) : base(i1, i2, Material.clay)
		{
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.clay.shiftedIndex;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 4;
		}
	}
}