namespace net.minecraft.src
{
	public class BlockOreStorage : Block
	{
		public BlockOreStorage(int i1, int i2) : base(i1, Material.iron)
		{
			this.blockIndexInTexture = i2;
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return this.blockIndexInTexture;
		}
	}

}