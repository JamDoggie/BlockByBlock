namespace net.minecraft.src
{
	public class BlockSponge : Block
	{
		protected internal BlockSponge(int i1) : base(i1, Material.sponge)
		{
			this.blockIndexInTexture = 48;
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
		}
	}

}