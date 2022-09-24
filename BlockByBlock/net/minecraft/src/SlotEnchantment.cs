namespace net.minecraft.src
{
	internal class SlotEnchantment : Slot
	{
		internal readonly ContainerEnchantment container;

		internal SlotEnchantment(ContainerEnchantment containerEnchantment1, IInventory iInventory2, int i3, int i4, int i5) : base(iInventory2, i3, i4, i5)
		{
			this.container = containerEnchantment1;
		}

		public override bool isItemValid(ItemStack itemStack1)
		{
			return true;
		}
	}

}