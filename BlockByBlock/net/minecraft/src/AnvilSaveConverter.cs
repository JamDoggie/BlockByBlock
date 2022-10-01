using System;
using System.Collections;

namespace net.minecraft.src
{

	public class AnvilSaveConverter : SaveFormatOld
	{
		public AnvilSaveConverter(DirectoryInfo file1) : base(file1)
		{
		}

		public override string FormatName
		{
			get
			{
				return "Anvil";
			}
		}

		public override List<SaveFormatComparator> SaveList
		{
			get
			{
				List<SaveFormatComparator> arrayList1 = new();

				DirectoryInfo[] file2 = savesDirectory.GetDirectories();
                
				int i4 = file2.Length;
    
				for (int i = 0; i < i4; ++i)
				{
					DirectoryInfo saveFolder = file2[i];
                    
					string saveName = saveFolder.Name;
					WorldInfo? worldInfo8 = getWorldInfo(saveName);
					if (worldInfo8 != null && (worldInfo8.SaveVersion == 19132 || worldInfo8.SaveVersion == 19133))
					{
						bool z9 = worldInfo8.SaveVersion != this.func_48431_c();
						string string10 = worldInfo8.WorldName;
						if (ReferenceEquals(string10, null) || MathHelper.stringNullOrLengthZero(string10))
						{
							string10 = saveName;
						}
    
						long j11 = 0L;
						arrayList1.Add(new SaveFormatComparator(saveName, string10, worldInfo8.LastTimePlayed, j11, worldInfo8.GameType, z9, worldInfo8.HardcoreModeEnabled));
					}
				}
    
				return arrayList1;
			}
		}

		protected internal virtual int func_48431_c()
		{
			return 19133;
		}

		public override void flushCache()
		{
			RegionFileCache.clearRegionFileReferences();
		}

		public override ISaveHandler getSaveLoader(string string1, bool z2)
		{
			return new AnvilSaveHandler(savesDirectory, string1, z2);
		}

		public override bool isOldMapFormat(string string1)
		{
			WorldInfo worldInfo2 = this.getWorldInfo(string1);
			return worldInfo2 != null && worldInfo2.SaveVersion != this.func_48431_c();
		}

		public override bool convertMapFormat(string string1, IProgressUpdate iProgressUpdate2)
		{
			iProgressUpdate2.LoadingProgress = 0;
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			ArrayList arrayList5 = new ArrayList();
			DirectoryInfo file6 = new DirectoryInfo(savesDirectory.FullName + '/' + string1);
			DirectoryInfo file7 = new DirectoryInfo(file6 + "/DIM-1");
			DirectoryInfo file8 = new DirectoryInfo(file6 + "/DIM1");
            
			Console.WriteLine("Scanning folders...");
			FillArrayWithWorldRegions(file6, arrayList3);

			if (file7.Exists)
			{
				this.FillArrayWithWorldRegions(file7, arrayList4);
			}

			if (file8.Exists)
			{
				this.FillArrayWithWorldRegions(file8, arrayList5);
			}

			int i9 = arrayList3.Count + arrayList4.Count + arrayList5.Count;
			Console.WriteLine("Total conversion count is " + i9);
			WorldInfo worldInfo10 = this.getWorldInfo(string1);
			object object11 = null;
			if (worldInfo10.TerrainType == WorldType.FLAT)
			{
				object11 = new WorldChunkManagerHell(BiomeGenBase.plains, 0.5F, 0.5F);
			}
			else
			{
				object11 = new WorldChunkManager(worldInfo10.Seed, worldInfo10.TerrainType);
			}

			this.convertDimensionRegions(new DirectoryInfo(file6 + "/region"), arrayList3, (WorldChunkManager)object11, 0, i9, iProgressUpdate2);
			this.convertDimensionRegions(new DirectoryInfo(file7 + "/region"), arrayList4, new WorldChunkManagerHell(BiomeGenBase.hell, 1.0F, 0.0F), arrayList3.Count, i9, iProgressUpdate2);
			this.convertDimensionRegions(new DirectoryInfo(file8 + "/region"), arrayList5, new WorldChunkManagerHell(BiomeGenBase.sky, 0.5F, 0.0F), arrayList3.Count + arrayList4.Count, i9, iProgressUpdate2);
			worldInfo10.SaveVersion = 19133;
			if (worldInfo10.TerrainType == WorldType.DEFAULT_1_1)
			{
				worldInfo10.TerrainType = WorldType.DEFAULT;
			}

			this.tryBackupWorld(string1);
			ISaveHandler iSaveHandler12 = this.getSaveLoader(string1, false);
			iSaveHandler12.saveWorldInfo(worldInfo10);
			return true;
		}
        
		private void tryBackupWorld(string worldName)
		{
			DirectoryInfo worldDir = new DirectoryInfo(savesDirectory.FullName + '/' + worldName);
			if (!worldDir.Exists)
			{
				Console.WriteLine("Warning: Unable to create level.dat_mcr backup");
			}
			else
			{
				FileInfo file3 = new FileInfo(worldDir + "/level.dat");
				if (!file3.Exists)
				{
					Console.WriteLine("Warning: Unable to create level.dat_mcr backup");
				}
				else
				{
					FileInfo file4 = new FileInfo(worldDir + "/level.dat_mcr");

                    try
                    {
						file3.MoveTo(file4.FullName);
					}
					catch (IOException e)
					{
						Console.WriteLine("Warning: Unable to create level.dat_mcr backup.");
						Console.WriteLine(e.Message);
						Console.WriteLine(e.StackTrace);
					}

				}
			}
		}

		// PORTING TODO: Custom method name. Make sure this is accurate.
		private void convertDimensionRegions(DirectoryInfo dimensionDir, ArrayList arrayList2, WorldChunkManager worldChunkManager3, int i4, int i5, IProgressUpdate progressBar)
		{
			IEnumerator iterator7 = arrayList2.GetEnumerator();

			while (iterator7.MoveNext())
			{
				FileInfo file8 = (FileInfo)iterator7.Current;
				convertRegion(dimensionDir, file8, worldChunkManager3, i4, i5, progressBar);
				++i4;
				int progress = (int)Math.Round(100.0D * (double)i4 / (double)i5, MidpointRounding.AwayFromZero);
				progressBar.LoadingProgress = progress;
			}

		}

        // PORTING TODO: Custom method name. Make sure this is accurate.
        private void convertRegion(DirectoryInfo regionsDir, FileInfo region, WorldChunkManager worldChunkManager3, int i4, int i5, IProgressUpdate iProgressUpdate6)
		{
			try
			{
				string regionName = region.Name;
				RegionFile regionFile8 = new RegionFile(region);
				RegionFile regionFile9 = new RegionFile(new FileInfo(regionsDir.FullName + '/' + regionName.Substring(0, regionName.Length - ".mcr".Length) + ".mca"));

				for (int x = 0; x < 32; ++x)
				{
					int z;
					for (z = 0; z < 32; ++z)
					{
						if (regionFile8.isChunkSaved(x, z) && !regionFile9.isChunkSaved(x, z))
						{
							BinaryReader dataInputStream12 = regionFile8.getChunkDataInputStream(x, z);
                            
							if (dataInputStream12 == null)
							{
								Console.WriteLine("SAVE CONVERTER: Failed to fetch input stream");
							}
							else
							{
								NBTTagCompound nBTTagCompound13 = CompressedStreamTools.read(dataInputStream12);
                                
								dataInputStream12.Dispose();
                                
								NBTTagCompound nBTTagCompound14 = nBTTagCompound13.getCompoundTag("Level");
								AnvilConverterData anvilConverterData15 = ChunkLoader.load(nBTTagCompound14);
								NBTTagCompound nBTTagCompound16 = new NBTTagCompound();
								NBTTagCompound nBTTagCompound17 = new NBTTagCompound();
								nBTTagCompound16.setTag("Level", nBTTagCompound17);
								ChunkLoader.convertToAnvilFormat(anvilConverterData15, nBTTagCompound17, worldChunkManager3);
								BinaryWriter? dataOutputStream18 = regionFile9.getChunkDataOutputStream(x, z);
								CompressedStreamTools.write(nBTTagCompound16, dataOutputStream18);
								dataOutputStream18.Dispose();
							}
						}
					}

					z = (int)(long)Math.Round(100.0D * (double)(i4 * 1024) / (double)(i5 * 1024), MidpointRounding.AwayFromZero);
					int i20 = (int)(long)Math.Round(100.0D * (double)((x + 1) * 32 + i4 * 1024) / (double)(i5 * 1024), MidpointRounding.AwayFromZero);
					if (i20 > z)
					{
						iProgressUpdate6.LoadingProgress = i20;
					}
				}

				regionFile8.close();
				regionFile9.close();
			}
			catch (IOException iOException19)
			{
				Console.WriteLine(iOException19.ToString());
				Console.Write(iOException19.StackTrace);
			}

		}

		private void FillArrayWithWorldRegions(DirectoryInfo file1, ArrayList arrayList2)
		{
			DirectoryInfo file3 = new DirectoryInfo(file1 + "/region");
			FileInfo[] file4 = file3.GetFiles("*.mcr");
			if (file4 != null)
			{
				int i6 = file4.Length;

				for (int i = 0; i < i6; ++i)
				{
					FileInfo file8 = file4[i];
					arrayList2.Add(file8);
				}
			}

		}
	}

}