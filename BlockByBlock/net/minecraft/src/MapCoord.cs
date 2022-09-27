namespace net.minecraft.src
{
	public class MapCoord
	{
		public sbyte field_28217_a;
		public sbyte centerX;
		public sbyte centerZ;
		public sbyte iconRotation;
		internal readonly MapData data;

		public MapCoord(MapData mapData1, sbyte b2, sbyte b3, sbyte b4, sbyte b5)
		{
			data = mapData1;
			field_28217_a = b2;
			centerX = b3;
			centerZ = b4;
			iconRotation = b5;
		}
	}

}