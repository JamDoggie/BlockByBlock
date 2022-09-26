namespace net.minecraft.src
{

	public interface IChunkLoader
	{
		Chunk loadChunk(World world1, int i2, int i3);

		void saveChunk(World world1, Chunk chunk2);

		void saveExtraChunkData(World world1, Chunk chunk2);

		void chunkTick();

		void saveExtraData();
	}

}