using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemSnowball : Item
	{
		public ItemSnowball(int i1) : base(i1)
		{
			this.maxStackSize = 16;
		}

		public override ItemStack onItemRightClick(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			if (!entityPlayer3.capabilities.isCreativeMode)
			{
				--itemStack1.stackSize;
			}

			world2.playSoundAtEntity(entityPlayer3, "random.bow", 0.5F, 0.4F / (itemRand.NextSingle() * 0.4F + 0.8F));
			if (!world2.isRemote)
			{
				world2.spawnEntityInWorld(new EntitySnowball(world2, entityPlayer3));
			}

			return itemStack1;
		}
	}

}