namespace net.minecraft.src
{

	public class AnvilSaveHandler : SaveHandler
	{
		public AnvilSaveHandler(DirectoryInfo savesDir, string string2, bool z3) : base(savesDir, string2, z3)
		{
		}

		public override IChunkLoader getChunkLoader(WorldProvider worldProvider1)
		{
			DirectoryInfo saveDir = SaveDirectory;
			DirectoryInfo file3;
			if (worldProvider1 is WorldProviderHell)
			{
				file3 = new DirectoryInfo(saveDir + "/DIM-1");
				file3.Create();
				return new AnvilChunkLoader(file3);
			}
			else if (worldProvider1 is WorldProviderEnd)
			{
				file3 = new DirectoryInfo(saveDir + "/DIM1");
				file3.Create();
				return new AnvilChunkLoader(file3);
			}
			else
			{
				return new AnvilChunkLoader(saveDir);
			}
		}

		public override void saveWorldInfoAndPlayer(WorldInfo worldInfo1, System.Collections.IList list2)
		{
			worldInfo1.SaveVersion = 19133;
			base.saveWorldInfoAndPlayer(worldInfo1, list2);
		}
	}

}