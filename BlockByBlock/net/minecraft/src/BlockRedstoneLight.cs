using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockRedstoneLight : Block
	{
		private readonly bool powered;

		public BlockRedstoneLight(int i1, bool z2) : base(i1, 211, Material.redstoneLight)
		{
			this.powered = z2;
			if (z2)
			{
				setLightValue(1.0F);
				++this.blockIndexInTexture;
			}

		}

		public override void onBlockAdded(World world1, int i2, int i3, int i4)
		{
			if (!world1.isRemote)
			{
				if (this.powered && !world1.isBlockIndirectlyGettingPowered(i2, i3, i4))
				{
					world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, 4);
				}
				else if (!this.powered && world1.isBlockIndirectlyGettingPowered(i2, i3, i4))
				{
					world1.setBlockWithNotify(i2, i3, i4, Block.redstoneLampActive.blockID);
				}
			}

		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (!world1.isRemote)
			{
				if (this.powered && !world1.isBlockIndirectlyGettingPowered(i2, i3, i4))
				{
					world1.scheduleBlockUpdate(i2, i3, i4, this.blockID, 4);
				}
				else if (!this.powered && world1.isBlockIndirectlyGettingPowered(i2, i3, i4))
				{
					world1.setBlockWithNotify(i2, i3, i4, Block.redstoneLampActive.blockID);
				}
			}

		}

		public override void updateTick(World world1, int i2, int i3, int i4, RandomExtended random5)
		{
			if (!world1.isRemote && this.powered && !world1.isBlockIndirectlyGettingPowered(i2, i3, i4))
			{
				world1.setBlockWithNotify(i2, i3, i4, Block.redstoneLampIdle.blockID);
			}

		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Block.redstoneLampIdle.blockID;
		}
	}

}