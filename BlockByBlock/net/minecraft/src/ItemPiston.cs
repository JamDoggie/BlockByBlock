namespace net.minecraft.src
{
	public class ItemPiston : ItemBlock
	{
		public ItemPiston(int i1) : base(i1)
		{
		}

		public override int getMetadata(int i1)
		{
			return 7;
		}
	}

}