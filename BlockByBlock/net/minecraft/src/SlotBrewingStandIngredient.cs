namespace net.minecraft.src
{
	internal class SlotBrewingStandIngredient : Slot
	{
		internal readonly ContainerBrewingStand container;

		public SlotBrewingStandIngredient(ContainerBrewingStand containerBrewingStand1, IInventory iInventory2, int i3, int i4, int i5) : base(iInventory2, i3, i4, i5)
		{
			this.container = containerBrewingStand1;
		}

		public override bool isItemValid(ItemStack itemStack1)
		{
			return itemStack1 != null ? Item.itemsList[itemStack1.itemID].PotionIngredient : false;
		}

		public override int SlotStackLimit
		{
			get
			{
				return 64;
			}
		}
	}

}