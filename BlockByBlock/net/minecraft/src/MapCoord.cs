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
			this.data = mapData1;
			this.field_28217_a = b2;
			this.centerX = b3;
			this.centerZ = b4;
			this.iconRotation = b5;
		}
	}

}