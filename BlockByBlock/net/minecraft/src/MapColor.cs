namespace net.minecraft.src
{
	public class MapColor
	{
		public static readonly MapColor[] mapColorArray = new MapColor[16];
		public static readonly MapColor airColor = new MapColor(0, 0);
		public static readonly MapColor grassColor = new MapColor(1, 8368696);
		public static readonly MapColor sandColor = new MapColor(2, 16247203);
		public static readonly MapColor clothColor = new MapColor(3, 10987431);
		public static readonly MapColor tntColor = new MapColor(4, 16711680);
		public static readonly MapColor iceColor = new MapColor(5, 10526975);
		public static readonly MapColor ironColor = new MapColor(6, 10987431);
		public static readonly MapColor foliageColor = new MapColor(7, 31744);
		public static readonly MapColor snowColor = new MapColor(8, 0xFFFFFF);
		public static readonly MapColor clayColor = new MapColor(9, 10791096);
		public static readonly MapColor dirtColor = new MapColor(10, 12020271);
		public static readonly MapColor stoneColor = new MapColor(11, 7368816);
		public static readonly MapColor waterColor = new MapColor(12, 4210943);
		public static readonly MapColor woodColor = new MapColor(13, 6837042);
		public readonly int colorValue;
		public readonly int colorIndex;

		private MapColor(int i1, int i2)
		{
			colorIndex = i1;
			colorValue = i2;
			mapColorArray[i1] = this;
		}
	}

}