namespace net.minecraft.src
{
	public class TileEntitySign : TileEntity
	{
		public string[] signText = new string[]{"", "", "", ""};
		public int lineBeingEdited = -1;
		private bool isEditable = true;

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			nBTTagCompound1.setString("Text1", this.signText[0]);
			nBTTagCompound1.setString("Text2", this.signText[1]);
			nBTTagCompound1.setString("Text3", this.signText[2]);
			nBTTagCompound1.setString("Text4", this.signText[3]);
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			this.isEditable = false;
			base.readFromNBT(nBTTagCompound1);

			for (int i2 = 0; i2 < 4; ++i2)
			{
				this.signText[i2] = nBTTagCompound1.getString("Text" + (i2 + 1));
				if (this.signText[i2].Length > 15)
				{
					this.signText[i2] = this.signText[i2].Substring(0, 15);
				}
			}

		}

		public virtual bool func_50007_a()
		{
			return this.isEditable;
		}

		public virtual void func_50006_a(bool z1)
		{
			this.isEditable = z1;
		}
	}

}