namespace net.minecraft.src
{
	public class ColorizerWater
	{
		private static int[] waterBuffer = new int[65536];

		public static int[] WaterBiomeColorizer
		{
			set
			{
				waterBuffer = value;
			}
		}
	}

}