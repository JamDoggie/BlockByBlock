namespace net.minecraft.src
{
	public abstract class WorldSavedData
	{
		public readonly string mapName;
		private bool dirty;

		public WorldSavedData(string string1)
		{
			this.mapName = string1;
		}

		public abstract void readFromNBT(NBTTagCompound nBTTagCompound1);

		public abstract void writeToNBT(NBTTagCompound nBTTagCompound1);

		public virtual void markDirty()
		{
			this.Dirty = true;
		}

		public virtual bool Dirty
		{
			set
			{
				this.dirty = value;
			}
			get
			{
				return this.dirty;
			}
		}

	}

}