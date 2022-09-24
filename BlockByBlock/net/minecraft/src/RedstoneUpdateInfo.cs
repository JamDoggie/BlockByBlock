namespace net.minecraft.src
{
	internal class RedstoneUpdateInfo
	{
		internal int x;
		internal int y;
		internal int z;
		internal long updateTime;

		public RedstoneUpdateInfo(int i1, int i2, int i3, long j4)
		{
			this.x = i1;
			this.y = i2;
			this.z = i3;
			this.updateTime = j4;
		}
	}

}