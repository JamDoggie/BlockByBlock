using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class BlockNote : BlockContainer
	{
		public BlockNote(int i1) : base(i1, 74, Material.wood)
		{
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return this.blockIndexInTexture;
		}

		public override void onNeighborBlockChange(World world1, int i2, int i3, int i4, int i5)
		{
			if (i5 > 0)
			{
				bool z6 = world1.isBlockIndirectlyGettingPowered(i2, i3, i4);
				TileEntityNote tileEntityNote7 = (TileEntityNote)world1.getBlockTileEntity(i2, i3, i4);
				if (tileEntityNote7 != null && tileEntityNote7.previousRedstoneState != z6)
				{
					if (z6)
					{
						tileEntityNote7.triggerNote(world1, i2, i3, i4);
					}

					tileEntityNote7.previousRedstoneState = z6;
				}
			}

		}

		public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (world1.isRemote)
			{
				return true;
			}
			else
			{
				TileEntityNote tileEntityNote6 = (TileEntityNote)world1.getBlockTileEntity(i2, i3, i4);
				if (tileEntityNote6 != null)
				{
					tileEntityNote6.changePitch();
					tileEntityNote6.triggerNote(world1, i2, i3, i4);
				}

				return true;
			}
		}

		public override void onBlockClicked(World world1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			if (!world1.isRemote)
			{
				TileEntityNote tileEntityNote6 = (TileEntityNote)world1.getBlockTileEntity(i2, i3, i4);
				if (tileEntityNote6 != null)
				{
					tileEntityNote6.triggerNote(world1, i2, i3, i4);
				}

			}
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return new TileEntityNote();
			}
		}

		public override void powerBlock(World world1, int i2, int i3, int i4, int i5, int i6)
		{
			float f7 = (float)Math.Pow(2.0D, (double)(i6 - 12) / 12.0D);
			string string8 = "harp";
			if (i5 == 1)
			{
				string8 = "bd";
			}

			if (i5 == 2)
			{
				string8 = "snare";
			}

			if (i5 == 3)
			{
				string8 = "hat";
			}

			if (i5 == 4)
			{
				string8 = "bassattack";
			}

			world1.playSoundEffect((double)i2 + 0.5D, (double)i3 + 0.5D, (double)i4 + 0.5D, "note." + string8, 3.0F, f7);
			world1.spawnParticle("note", (double)i2 + 0.5D, (double)i3 + 1.2D, (double)i4 + 0.5D, (double)i6 / 24.0D, 0.0D, 0.0D);
		}
	}

}