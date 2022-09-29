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
			regionFile = regionFile1;
			chunkX = i2;
			chunkZ = i3;
		}

        public override void Close()
        {
			regionFile.write(chunkX, chunkZ, GetBuffer(), (int)Position);
			base.Close();
        }
	}

}