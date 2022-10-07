using System;
using System.Collections;
using System.IO;

namespace net.minecraft.src
{

	public class SaveFormatOld : ISaveFormat
	{
		protected internal readonly DirectoryInfo savesDirectory;

		public SaveFormatOld(DirectoryInfo worldFolder)
		{
			if (!worldFolder.Exists)
			{
				worldFolder.Create();
			}

			savesDirectory = worldFolder;
		}

		public virtual string FormatName
		{
			get
			{
				return "Old Format";
			}
		}

		public virtual List<SaveFormatComparator> SaveList
		{
			get
			{
				List<SaveFormatComparator> arrayList1 = new();
    
				for (int i2 = 0; i2 < 5; ++i2)
				{
					string string3 = "World" + (i2 + 1);
					WorldInfo worldInfo4 = this.getWorldInfo(string3);
					if (worldInfo4 != null)
					{
						arrayList1.Add(new SaveFormatComparator(string3, "", worldInfo4.LastTimePlayed, worldInfo4.SizeOnDisk, worldInfo4.GameType, false, worldInfo4.HardcoreModeEnabled));
					}
				}
    
				return arrayList1;
			}
		}

		public virtual void flushCache()
		{
		}

		public virtual WorldInfo? getWorldInfo(string worldName)
		{
			DirectoryInfo file2 = new DirectoryInfo(savesDirectory + "/" + worldName);
            
			if (!file2.Exists)
			{
				return null;
			}
			else
			{
				FileInfo file3 = new FileInfo(file2 + "/level.dat");
				NBTTagCompound nBTTagCompound4;
				NBTTagCompound nBTTagCompound5;
				if (file3.Exists)
				{
					try
					{
						nBTTagCompound4 = CompressedStreamTools.readCompressed(new FileStream(file3.FullName, FileMode.Open, FileAccess.Read));
						nBTTagCompound5 = nBTTagCompound4.getCompoundTag("Data");
						return new WorldInfo(nBTTagCompound5);
					}
					catch (Exception exception7)
					{
						Console.WriteLine(exception7.ToString());
						Console.Write(exception7.StackTrace);
					}
				}

				file3 = new FileInfo(file2 + "/level.dat_old");
				if (file3.Exists)
				{
					try
					{
						nBTTagCompound4 = CompressedStreamTools.readCompressed(new FileStream(file3.FullName, FileMode.Open, FileAccess.Read));
						nBTTagCompound5 = nBTTagCompound4.getCompoundTag("Data");
						return new WorldInfo(nBTTagCompound5);
					}
					catch (Exception exception6)
					{
						Console.WriteLine(exception6.ToString());
						Console.Write(exception6.StackTrace);
					}
				}

				return null;
			}
		}

		public virtual void renameWorld(string string1, string string2)
		{
			FileInfo file3 = new FileInfo(savesDirectory.FullName + '/' + string1);
			if (file3.Exists)
			{
				FileInfo file4 = new FileInfo(file3 + "/level.dat");
				if (file4.Exists)
				{
					try
					{
						NBTTagCompound nBTTagCompound5 = CompressedStreamTools.readCompressed(new FileStream(file4.FullName, FileMode.Open, FileAccess.Read));
						NBTTagCompound nBTTagCompound6 = nBTTagCompound5.getCompoundTag("Data");
						nBTTagCompound6.setString("LevelName", string2);
						CompressedStreamTools.writeCompressed(nBTTagCompound5, new FileStream(file4.FullName, FileMode.Create, FileAccess.Write));
					}
					catch (Exception exception7)
					{
						Console.WriteLine(exception7.ToString());
						Console.Write(exception7.StackTrace);
					}
				}

			}
		}

		public virtual void deleteWorldDirectory(string world)
		{
			DirectoryInfo dir = new DirectoryInfo(savesDirectory.FullName + '/' + world);
			if (dir.Exists)
			{
				dir.Delete(true);
			}
		}

		public virtual ISaveHandler getSaveLoader(string string1, bool z2)
		{
			return new SaveHandler(savesDirectory, string1, z2);
		}

		public virtual bool isOldMapFormat(string string1)
		{
			return false;
		}

		public virtual bool convertMapFormat(string string1, IProgressUpdate iProgressUpdate2)
		{
			return false;
		}
	}

}