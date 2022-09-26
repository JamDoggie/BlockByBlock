namespace net.minecraft.src
{
	public class ItemMetadata : ItemBlock
	{
		private Block blockObj;

		public ItemMetadata(int i1, Block block2) : base(i1)
		{
			blockObj = block2;
			setMaxDamage(0);
			setHasSubtypes(true);
		}

		public override int getIconFromDamage(int i1)
		{
			return blockObj.getBlockTextureFromSideAndMetadata(2, i1);
		}

		public override int getMetadata(int i1)
		{
			return i1;
		}
	}

}