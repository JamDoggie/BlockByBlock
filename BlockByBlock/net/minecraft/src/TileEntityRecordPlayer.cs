namespace net.minecraft.src
{
	public class TileEntityRecordPlayer : TileEntity
	{
		public int record;

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			this.record = nBTTagCompound1.getInteger("Record");
		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			if (this.record > 0)
			{
				nBTTagCompound1.setInteger("Record", this.record);
			}

		}
	}

}