using System.IO;

namespace net.minecraft.src
{

	internal class RegionFileChunkBuffer : MemoryStream
	{
		private int chunkX;
		private int chunkZ;
		internal readonly RegionFile regionFile;

		public RegionFileChunkBuffer(RegionFile regionFile1, int i2, int i3) : base(8096)
		{
			this.regionFile = regionFile1;
			this.chunkX = i2;
			this.chunkZ = i3;
		}

		public virtual void close()
		{
			this.regionFile.write(this.chunkX, this.chunkZ, this.buf, this.count);
		}
	}

}