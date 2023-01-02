using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemExpBottle : Item
	{
		public ItemExpBottle(int i1) : base(i1)
		{
		}

		public override bool hasEffect(ItemStack itemStack1)
		{
			return true;
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
				world2.spawnEntityInWorld(new EntityExpBottle(world2, entityPlayer3));
			}

			return itemStack1;
		}
	}

}