namespace net.minecraft.src
{

	public interface IChunkProvider
	{
		bool chunkExists(int i1, int i2);

		Chunk provideChunk(int i1, int i2);

		Chunk loadChunk(int i1, int i2);

		void populate(IChunkProvider iChunkProvider1, int i2, int i3);

		bool saveChunks(bool z1, IProgressUpdate iProgressUpdate2);

		bool unload100OldestChunks();

		bool canSave();

		string makeString();

		System.Collections.IList getPossibleCreatures(EnumCreatureType enumCreatureType1, int i2, int i3, int i4);

		ChunkPosition? findClosestStructure(World world1, string string2, int i3, int i4, int i5);
	}

}