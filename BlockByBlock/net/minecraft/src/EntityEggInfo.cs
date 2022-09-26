namespace net.minecraft.src
{
	public class EntityEggInfo
	{
		public int spawnedID;
		public int primaryColor;
		public int secondaryColor;

		public EntityEggInfo(int i1, int color1, int color2)
		{
			this.spawnedID = i1;
			this.primaryColor = color1;
			this.secondaryColor = color2;
		}
	}

}