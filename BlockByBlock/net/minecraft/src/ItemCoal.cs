namespace net.minecraft.src
{
	public class ItemCoal : Item
	{
		public ItemCoal(int i1) : base(i1)
		{
			this.HasSubtypes = true;
			this.MaxDamage = 0;
		}

		public override string getItemNameIS(ItemStack itemStack1)
		{
			return itemStack1.ItemDamage == 1 ? "item.charcoal" : "item.coal";
		}
	}

}