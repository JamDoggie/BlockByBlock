namespace net.minecraft.src
{
	public abstract class BlockContainer : Block
	{
		protected internal BlockContainer(int i1, Material material2) : base(i1, material2)
		{
			this.isBlockContainer = true;
		}

		protected internal BlockContainer(int i1, int i2, Material material3) : base(i1, i2, material3)
		{
			this.isBlockContainer = true;
		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			base.onBlockAdded(world1, i2, i3, i4);
			world1.setBlockTileEntity(i2, i3, i4, this.BlockEntity);
		}

		public override void onBlockRemoval(World world1, int i2, int i3, int i4)
		{
			base.onBlockRemoval(world1, i2, i3, i4);
			world1.removeBlockTileEntity(i2, i3, i4);
		}

		public abstract TileEntity BlockEntity {get;}

		public override void powerBlock(World world1, int i2, int i3, int i4, int i5, int i6)
		{
			base.powerBlock(world1, i2, i3, i4, i5, i6);
			TileEntity tileEntity7 = world1.getBlockTileEntity(i2, i3, i4);
			if (tileEntity7 != null)
			{
				tileEntity7.onTileEntityPowered(i5, i6);
			}

		}
	}

}