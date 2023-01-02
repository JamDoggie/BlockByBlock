using net.minecraft.client.entity;

namespace net.minecraft.src
{
    internal class SlotBrewingStandPotion : Slot
	{
		private EntityPlayer player;
		internal readonly ContainerBrewingStand container;

		public SlotBrewingStandPotion(ContainerBrewingStand containerBrewingStand1, EntityPlayer entityPlayer2, IInventory iInventory3, int i4, int i5, int i6) : base(iInventory3, i4, i5, i6)
		{
			this.container = containerBrewingStand1;
			this.player = entityPlayer2;
		}

		public override bool isItemValid(ItemStack itemStack1)
		{
			return itemStack1 != null && (itemStack1.itemID == Item.potion.shiftedIndex || itemStack1.itemID == Item.glassBottle.shiftedIndex);
		}

		public override int SlotStackLimit
		{
			get
			{
				return 1;
			}
		}

		public override void onPickupFromSlot(ItemStack itemStack1)
		{
			if (itemStack1.itemID == Item.potion.shiftedIndex && itemStack1.ItemDamage > 0)
			{
				this.player.addStat(AchievementList.potion, 1);
			}

			base.onPickupFromSlot(itemStack1);
		}
	}

}