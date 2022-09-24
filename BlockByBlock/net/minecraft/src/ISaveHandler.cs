namespace net.minecraft.src
{

	public interface ISaveHandler
	{
		WorldInfo loadWorldInfo();

		void checkSessionLock();

		IChunkLoader getChunkLoader(WorldProvider worldProvider1);

		void saveWorldInfoAndPlayer(WorldInfo worldInfo1, System.Collections.IList list2);

		void saveWorldInfo(WorldInfo worldInfo1);

		FileInfo getMapFileFromName(string string1);

		string SaveDirectoryName {get;}
	}

}