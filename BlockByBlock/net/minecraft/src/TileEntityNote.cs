namespace net.minecraft.src
{
	public class TileEntityNote : TileEntity
	{
		public sbyte note = 0;
		public bool previousRedstoneState = false;

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			nBTTagCompound1.setByte("note", this.note);
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			this.note = nBTTagCompound1.getByte("note");
			if (this.note < 0)
			{
				this.note = 0;
			}

			if (this.note > 24)
			{
				this.note = 24;
			}

		}

		public virtual void changePitch()
		{
			this.note = (sbyte)((this.note + 1) % 25);
			this.onInventoryChanged();
		}

		public virtual void triggerNote(World world1, int i2, int i3, int i4)
		{
			if (world1.getBlockMaterial(i2, i3 + 1, i4) == Material.air)
			{
				Material material5 = world1.getBlockMaterial(i2, i3 - 1, i4);
				sbyte b6 = 0;
				if (material5 == Material.rock)
				{
					b6 = 1;
				}

				if (material5 == Material.sand)
				{
					b6 = 2;
				}

				if (material5 == Material.glass)
				{
					b6 = 3;
				}

				if (material5 == Material.wood)
				{
					b6 = 4;
				}

				world1.playNoteAt(i2, i3, i4, b6, this.note);
			}
		}
	}

}