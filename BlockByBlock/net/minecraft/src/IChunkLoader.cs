namespace net.minecraft.src
{

	public interface IChunkLoader
	{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: Chunk loadChunk(World world1, int i2, int i3) throws java.io.IOException;
		Chunk loadChunk(World world1, int i2, int i3);

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void saveChunk(World world1, Chunk chunk2) throws java.io.IOException;
		void saveChunk(World world1, Chunk chunk2);

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: void saveExtraChunkData(World world1, Chunk chunk2) throws java.io.IOException;
		void saveExtraChunkData(World world1, Chunk chunk2);

		void chunkTick();

		void saveExtraData();
	}

}