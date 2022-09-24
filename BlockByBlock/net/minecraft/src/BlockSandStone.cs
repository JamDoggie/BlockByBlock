namespace net.minecraft.src
{
	public class BlockSandStone : Block
	{
		public BlockSandStone(int i1) : base(i1, 192, Material.rock)
		{
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			return i1 != 1 && (i1 != 0 || i2 != 1 && i2 != 2) ? (i1 == 0 ? 208 : (i2 == 1 ? 229 : (i2 == 2 ? 230 : 192))) : 176;
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return i1 == 1 ? this.blockIndexInTexture - 16 : (i1 == 0 ? this.blockIndexInTexture + 16 : this.blockIndexInTexture);
		}

		protected internal override int damageDropped(int i1)
		{
			return i1;
		}
	}
}