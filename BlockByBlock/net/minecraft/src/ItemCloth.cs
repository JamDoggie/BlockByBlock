namespace net.minecraft.src
{
	public class ItemCloth : ItemBlock
	{
		public ItemCloth(int i1) : base(i1)
		{
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

		public override int getIconFromDamage(int i1)
		{
			return Block.cloth.getBlockTextureFromSideAndMetadata(2, BlockCloth.getBlockFromDye(i1));
		}

		public override int getMetadata(int i1)
		{
			return i1;
		}

		public override string getItemNameIS(ItemStack itemStack1)
		{
			return base.ItemName + "." + ItemDye.dyeColorNames[BlockCloth.getBlockFromDye(itemStack1.ItemDamage)];
		}
	}

}