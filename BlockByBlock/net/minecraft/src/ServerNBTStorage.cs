namespace net.minecraft.src
{
	public class ServerNBTStorage
	{
		public string name;
		public string host;
		public string playerCount;
		public string motd;
		public long lag;
		public bool polled = false;

		public ServerNBTStorage(string string1, string string2)
		{
			this.name = string1;
			this.host = string2;
		}

		public virtual NBTTagCompound CompoundTag
		{
			get
			{
				NBTTagCompound nBTTagCompound1 = new NBTTagCompound();
				nBTTagCompound1.setString("name", this.name);
				nBTTagCompound1.setString("ip", this.host);
				return nBTTagCompound1;
			}
		}

		public static ServerNBTStorage createServerNBTStorage(NBTTagCompound nBTTagCompound0)
		{
			return new ServerNBTStorage(nBTTagCompound0.getString("name"), nBTTagCompound0.getString("ip"));
		}
	}

}