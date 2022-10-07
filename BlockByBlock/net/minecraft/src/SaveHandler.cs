using BlockByBlock.java_extensions;
using BlockByBlock.logging;
using System;
using System.IO;

namespace net.minecraft.src
{
	public class SaveHandler : ISaveHandler
	{
		private static readonly Logger logger = Logger.getLogger("Minecraft");
		private readonly DirectoryInfo saveDirectory;
		private readonly DirectoryInfo playersDirectory;
		private readonly DirectoryInfo mapDataDir;
		private readonly long initializationTime = DateTimeHelper.CurrentUnixTimeMillis();
		private readonly string saveDirectoryName;

		public SaveHandler(DirectoryInfo savesDir, string worldName, bool createPlayerDir)
		{
			saveDirectory = new DirectoryInfo(savesDir.FullName + '/' + worldName);
			saveDirectory.Create();
            
			playersDirectory = new DirectoryInfo(saveDirectory + "/players");
			mapDataDir = new DirectoryInfo(saveDirectory + "/data");
			mapDataDir.Create();
			saveDirectoryName = worldName;

			if (createPlayerDir)
			{
				playersDirectory.Create();
			}

			setSessionLock();
		}

		private void setSessionLock()
		{
			try
			{
				FileInfo sessionFile = new FileInfo(saveDirectory + "/session.lock");
				
				using (BinaryWriter dataOutputStream2 = new BinaryWriter(new FileStream(sessionFile.FullName, FileMode.Create, FileAccess.Write)))
                {
					dataOutputStream2.WriteBigEndian(initializationTime);
				}
			}
			catch (IOException iOException7)
			{
				Console.WriteLine(iOException7.ToString());
				Console.Write(iOException7.StackTrace);
				throw new Exception("Failed to check session lock, aborting");
			}
		}

		protected internal virtual DirectoryInfo SaveDirectory
		{
			get
			{
				return saveDirectory;
			}
		}

		public virtual void checkSessionLock()
		{
			try
			{
				FileInfo file1 = new FileInfo(this.saveDirectory + "/session.lock");
				
				using (BinaryReader dataInputStream2 = new BinaryReader(new FileStream(file1.FullName, FileMode.Open, FileAccess.Read)))
                {
					if (dataInputStream2.ReadInt64BigEndian() != this.initializationTime)
					{
						throw new MinecraftException("The save is being accessed from another location, aborting");
					}
				}

			}
			catch (IOException)
			{
				throw new MinecraftException("Failed to check session lock, aborting");
			}
		}

		public virtual IChunkLoader getChunkLoader(WorldProvider worldProvider1)
		{
			throw new Exception("Old Chunk Storage is no longer supported.");
		}

		public virtual WorldInfo loadWorldInfo()
		{
			FileInfo levelFile = new FileInfo(saveDirectory + "/level.dat");
			NBTTagCompound nBTTagCompound2;
			NBTTagCompound nBTTagCompound3;
			if (levelFile.Exists)
			{
				try
				{
					nBTTagCompound2 = CompressedStreamTools.readCompressed(new FileStream(levelFile.FullName, FileMode.Open, FileAccess.Read));
					nBTTagCompound3 = nBTTagCompound2.getCompoundTag("Data");
					return new WorldInfo(nBTTagCompound3);
				}
				catch (Exception exception5)
				{
					Console.WriteLine(exception5.ToString());
					Console.Write(exception5.StackTrace);
				}
			}

			levelFile = new FileInfo(saveDirectory + "/level.dat_old");
			if (levelFile.Exists)
			{
				try
				{
					nBTTagCompound2 = CompressedStreamTools.readCompressed(new FileStream(levelFile.FullName, FileMode.Open, FileAccess.Read));
					nBTTagCompound3 = nBTTagCompound2.getCompoundTag("Data");
					return new WorldInfo(nBTTagCompound3);
				}
				catch (Exception exception4)
				{
					Console.WriteLine(exception4.ToString());
					Console.Write(exception4.StackTrace);
				}
			}

			return null;
		}

		public virtual void saveWorldInfoAndPlayer(WorldInfo worldInfo1, System.Collections.IList list2)
		{
			NBTTagCompound nBTTagCompound3 = worldInfo1.getNBTTagCompoundWithPlayers(list2);
			NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
			nBTTagCompound4.setTag("Data", nBTTagCompound3);

			try
			{
				string file5 = saveDirectory + "/level.dat_new";
				string file6 = saveDirectory + "/level.dat_old";
				string file7 = saveDirectory + "/level.dat";
                
				CompressedStreamTools.writeCompressed(nBTTagCompound4, new FileStream(file5, FileMode.OpenOrCreate, FileAccess.ReadWrite));

				if (File.Exists(file7))
                {
					File.Move(file7, file6, true);
                }

				File.Move(file5, file7, true);
			}
			catch (Exception exception8)
			{
				Console.WriteLine(exception8.ToString());
				Console.Write(exception8.StackTrace);
			}

		}

		public virtual void saveWorldInfo(WorldInfo worldInfo1)
		{
			NBTTagCompound nBTTagCompound2 = worldInfo1.NBTTagCompound;
			NBTTagCompound nBTTagCompound3 = new NBTTagCompound();
			nBTTagCompound3.setTag("Data", nBTTagCompound2);

			try
			{
				string file5 = saveDirectory + "/level.dat_new";
				string file6 = saveDirectory + "/level.dat_old";
				string file7 = saveDirectory + "/level.dat";

				CompressedStreamTools.writeCompressed(nBTTagCompound3, new FileStream(file5, FileMode.Create, FileAccess.Write));

				if (File.Exists(file7))
				{
					File.Move(file7, file6, true);
				}

				File.Move(file5, file7, true);
			}
			catch (Exception exception7)
			{
				Console.WriteLine(exception7.ToString());
				Console.Write(exception7.StackTrace);
			}

		}

		public virtual FileInfo getMapFileFromName(string name)
		{
			return new FileInfo(mapDataDir.FullName + '/' + name + ".dat");
		}

		public virtual string SaveDirectoryName
		{
			get
			{
				return this.saveDirectoryName;
			}
		}
	}

}