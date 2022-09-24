using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockSnowBlock : Block
	{
		protected internal BlockSnowBlock(int i1, int i2) : base(i1, i2, Material.craftedSnow)
		{
			this.TickRandomly = true;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.snowball.shiftedIndex;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 4;
		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (world1.getSavedLightValue(EnumSkyBlock.Block, i2, i3, i4) > 11)
			{
				this.dropBlockAsItem(world1, i2, i3, i4, world1.getBlockMetadata(i2, i3, i4), 0);
				world1.setBlockWithNotify(i2, i3, i4, 0);
			}

		}
	}

}