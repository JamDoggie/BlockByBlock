namespace net.minecraft.src
{
	public class ItemSapling : ItemBlock
	{
		public ItemSapling(int i1) : base(i1)
		{
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

		public override int getMetadata(int i1)
		{
			return i1;
		}

		public override int getIconFromDamage(int i1)
		{
			return Block.sapling.getBlockTextureFromSideAndMetadata(0, i1);
		}
	}

}