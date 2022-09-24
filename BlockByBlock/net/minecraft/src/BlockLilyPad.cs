namespace net.minecraft.src
{
	public class BlockLilyPad : BlockFlower
	{
		protected internal BlockLilyPad(int i1, int i2) : base(i1, i2)
		{
			float f3 = 0.5F;
			float f4 = 0.015625F;
			this.setBlockBounds(0.5F - f3, 0.0F, 0.5F - f3, 0.5F + f3, f4, 0.5F + f3);
		}

		public override int RenderType
		{
			get
			{
				return 23;
			}
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			return AxisAlignedBB.getBoundingBoxFromPool((double)i2 + this.minX, (double)i3 + this.minY, (double)i4 + this.minZ, (double)i2 + this.maxX, (double)i3 + this.maxY, (double)i4 + this.maxZ);
		}

		public override int BlockColor
		{
			get
			{
				return 2129968;
			}
		}

		public override int getRenderColor(int i1)
		{
			return 2129968;
		}

		public override bool canPlaceBlockAt(World world1, int i2, int i3, int i4)
		{
			return base.canPlaceBlockAt(world1, i2, i3, i4);
		}

		public override int colorMultiplier(IBlockAccess iBlockAccess1, int i2, int i3, int i4)
		{
			return 2129968;
		}

		protected internal override bool canThisPlantGrowOnThisBlockID(int i1)
		{
			return i1 == Block.waterStill.blockID;
		}

		public override bool canBlockStay(World world1, int i2, int i3, int i4)
		{
			return i3 >= 0 && i3 < 256 ? world1.getBlockMaterial(i2, i3 - 1, i4) == Material.water && world1.getBlockMetadata(i2, i3 - 1, i4) == 0 : false;
		}
	}

}