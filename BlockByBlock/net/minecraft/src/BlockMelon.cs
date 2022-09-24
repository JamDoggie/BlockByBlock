using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockMelon : Block
	{
		protected internal BlockMelon(int i1) : base(i1, Material.pumpkin)
		{
			this.blockIndexInTexture = 136;
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i1 != 1 && i1 != 0 ? 136 : 137;
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return i1 != 1 && i1 != 0 ? 136 : 137;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.melon.shiftedIndex;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 3 + random1.Next(5);
		}

		public override int quantityDroppedWithBonus(int i1, RandomExtended random2)
		{
			int i3 = this.quantityDropped(random2) + random2.Next(1 + i1);
			if (i3 > 9)
			{
				i3 = 9;
			}

			return i3;
		}
	}

}