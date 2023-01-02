using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemShears : Item
	{
		public ItemShears(int i1) : base(i1)
		{
			setMaxStackSize(1);
			setMaxDamage(238);
		}

		public override bool onBlockDestroyed(ItemStack itemStack1, int i2, int i3, int i4, int i5, EntityLiving entityLiving6)
		{
			if (i2 != Block.leaves.blockID && i2 != Block.web.blockID && i2 != Block.tallGrass.blockID && i2 != Block.vine.blockID)
			{
				return base.onBlockDestroyed(itemStack1, i2, i3, i4, i5, entityLiving6);
			}
			else
			{
				itemStack1.damageItem(1, entityLiving6);
				return true;
			}
		}

		public override bool canHarvestBlock(Block block1)
		{
			return block1.blockID == Block.web.blockID;
		}

		public override float getStrVsBlock(ItemStack itemStack1, Block block2)
		{
			return block2.blockID != Block.web.blockID && block2.blockID != Block.leaves.blockID ? (block2.blockID == Block.cloth.blockID ? 5.0F : base.getStrVsBlock(itemStack1, block2)) : 15.0F;
		}
	}

}