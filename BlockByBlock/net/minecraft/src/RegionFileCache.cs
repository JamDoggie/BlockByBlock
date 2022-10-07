using System;
using System.Collections;

namespace net.minecraft.src
{

	public class RegionFileCache
	{
		private static readonly IDictionary<FileInfo,RegionFile> regionsByFilename = new Dictionary<FileInfo, RegionFile>();

		public static RegionFile createOrLoadRegionFile(DirectoryInfo file0, int i1, int i2)
		{
			lock (typeof(RegionFileCache))
			{
				DirectoryInfo file3 = new DirectoryInfo(file0 + "/region");
				FileInfo file4 = new FileInfo(file3 + "/r." + (i1 >> 5) + "." + (i2 >> 5) + ".mca");
                
				regionsByFilename.TryGetValue(file4, out RegionFile? regionFile6);
                
				if (regionFile6 != null)
				{
					return regionFile6;
				}
        
				if (!file3.Exists)
				{
					file3.Create();
				}
        
				if (regionsByFilename.Count >= 256)
				{
					clearRegionFileReferences();
				}
        
				regionFile6 = new RegionFile(file4);
				regionsByFilename[file4] = regionFile6;
				return regionFile6;
			}
		}

		public static void clearRegionFileReferences()
		{
			// PORTING TODO: Locking on a static type like this is not best practice. Come back to this later.
			lock (typeof(RegionFileCache))
			{
				IEnumerator<RegionFile> iterator0 = regionsByFilename.Values.GetEnumerator();
        
				while (iterator0.MoveNext())
				{
					try
					{
						RegionFile regionFile2 = iterator0.Current;
						if (regionFile2 != null)
						{
							regionFile2.close();
						}
					}
					catch (IOException iOException3)
					{
						Console.WriteLine(iOException3.ToString());
						Console.Write(iOException3.StackTrace);
					}
				}
        
				regionsByFilename.Clear();
			}
		}

		public static BinaryReader getChunkInputStream(DirectoryInfo file0, int i1, int i2)
		{
			RegionFile regionFile3 = createOrLoadRegionFile(file0, i1, i2);
			return regionFile3.getChunkDataInputStream(i1 & 31, i2 & 31);
		}

		public static BinaryWriter? getChunkOutputStream(DirectoryInfo file0, int i1, int i2)
		{
			RegionFile regionFile3 = createOrLoadRegionFile(file0, i1, i2);
			return regionFile3.getChunkDataOutputStream(i1 & 31, i2 & 31);
		}
	}

}