namespace net.minecraft.src
{
	internal class SlotArmor : Slot
	{
		internal readonly int armorType;
		internal readonly ContainerPlayer parent;

		internal SlotArmor(ContainerPlayer containerPlayer1, IInventory iInventory2, int i3, int i4, int i5, int i6) : base(iInventory2, i3, i4, i5)
		{
			this.parent = containerPlayer1;
			this.armorType = i6;
		}

		public override int SlotStackLimit
		{
			get
			{
				return 1;
			}
		}

		public override bool isItemValid(ItemStack itemStack1)
		{
			return itemStack1.Item is ItemArmor ? ((ItemArmor)itemStack1.Item).armorType == this.armorType : (itemStack1.Item.shiftedIndex == Block.pumpkin.blockID ? this.armorType == 0 : false);
		}

		public override int BackgroundIconIndex
		{
			get
			{
				return 15 + this.armorType * 16;
			}
		}
	}

}