using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemSoup : ItemFood
	{
		public ItemSoup(int i1, int i2) : base(i1, i2, false)
		{
			setMaxStackSize(1);
		}

		public override ItemStack onFoodEaten(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			base.onFoodEaten(itemStack1, world2, entityPlayer3);
			return new ItemStack(Item.bowlEmpty);
		}
	}

}