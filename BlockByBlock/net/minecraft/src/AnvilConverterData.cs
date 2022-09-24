namespace net.minecraft.src
{
	public class AnvilConverterData
	{
		public long lastUpdated;
		public bool terrainPopulated;
		public sbyte[] heightmap;
		public NibbleArrayReader blockLight;
		public NibbleArrayReader skyLight;
		public NibbleArrayReader data;
		public sbyte[] blocks;
		public NBTTagList entities;
		public NBTTagList tileEntities;
		public NBTTagList tileTicks;
		public readonly int x;
		public readonly int z;

		public AnvilConverterData(int i1, int i2)
		{
			this.x = i1;
			this.z = i2;
		}
	}

}