using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockGlowStone : Block
	{
		public BlockGlowStone(int i1, int i2, Material material3) : base(i1, i2, material3)
		{
		}

		public override int quantityDroppedWithBonus(int i1, RandomExtended random2)
		{
			return MathHelper.clamp_int(this.quantityDropped(random2) + random2.Next(i1 + 1), 1, 4);
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 2 + random1.Next(3);
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.lightStoneDust.shiftedIndex;
		}
	}

}