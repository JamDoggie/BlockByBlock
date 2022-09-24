namespace net.minecraft.src
{
	public class ItemMetadata : ItemBlock
	{
		private Block blockObj;

		public ItemMetadata(int i1, Block block2) : base(i1)
		{
			this.blockObj = block2;
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

		public override int getIconFromDamage(int i1)
		{
			return this.blockObj.getBlockTextureFromSideAndMetadata(2, i1);
		}

		public override int getMetadata(int i1)
		{
			return i1;
		}
	}

}