using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemBucketMilk : Item
	{
		public ItemBucketMilk(int i1) : base(i1)
		{
			this.setMaxStackSize(1);
		}

		public override ItemStack onFoodEaten(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			--itemStack1.stackSize;
			if (!world2.isRemote)
			{
				entityPlayer3.clearActivePotions();
			}

			return itemStack1.stackSize <= 0 ? new ItemStack(Item.bucketEmpty) : itemStack1;
		}

		public override int getMaxItemUseDuration(ItemStack itemStack1)
		{
			return 32;
		}

		public override EnumAction getItemUseAction(ItemStack itemStack1)
		{
			return EnumAction.drink;
		}

		public override ItemStack onItemRightClick(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			entityPlayer3.setItemInUse(itemStack1, this.getMaxItemUseDuration(itemStack1));
			return itemStack1;
		}
	}

}