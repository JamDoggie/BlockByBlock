namespace net.minecraft.src
{

	public class SaveHandlerMP : ISaveHandler
	{
		public virtual WorldInfo loadWorldInfo()
		{
			return null;
		}

		public virtual void checkSessionLock()
		{
		}

		public virtual IChunkLoader getChunkLoader(WorldProvider worldProvider1)
		{
			return null;
		}

		public virtual void saveWorldInfoAndPlayer(WorldInfo worldInfo1, System.Collections.IList list2)
		{
		}

		public virtual void saveWorldInfo(WorldInfo worldInfo1)
		{
		}

		public virtual FileInfo getMapFileFromName(string string1)
		{
			return null;
		}

		public virtual string SaveDirectoryName
		{
			get
			{
				return "none";
			}
		}
	}

}