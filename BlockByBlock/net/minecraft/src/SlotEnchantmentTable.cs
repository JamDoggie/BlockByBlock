namespace net.minecraft.src
{
	internal class SlotEnchantmentTable : InventoryBasic
	{
		internal readonly ContainerEnchantment container;

		internal SlotEnchantmentTable(ContainerEnchantment containerEnchantment1, string string2, int i3) : base(string2, i3)
		{
			this.container = containerEnchantment1;
		}

		public override int InventoryStackLimit
		{
			get
			{
				return 1;
			}
		}

		public override void onInventoryChanged()
		{
			base.onInventoryChanged();
			this.container.onCraftMatrixChanged(this);
		}
	}

}