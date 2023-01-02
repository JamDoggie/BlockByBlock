using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class WorldInfo
	{
		private long randomSeed;
		private WorldType terrainType = WorldType.DEFAULT;
		private int spawnX;
		private int spawnY;
		private int spawnZ;
		private long worldTime;
		private long lastTimePlayed;
		private long sizeOnDisk;
		private NBTTagCompound playerTag;
		private int dimension;
		private string levelName;
		private int saveVersion;
		private bool raining;
		private int rainTime;
		private bool thundering;
		private int thunderTime;
		private int gameType;
		private bool mapFeaturesEnabled;
		private bool hardcore = false;

		public WorldInfo(NBTTagCompound nBTTagCompound1)
		{
			this.randomSeed = nBTTagCompound1.getLong("RandomSeed");
			if (nBTTagCompound1.hasKey("generatorName"))
			{
				string string2 = nBTTagCompound1.getString("generatorName");
				this.terrainType = WorldType.parseWorldType(string2);
				if (this.terrainType == null)
				{
					this.terrainType = WorldType.DEFAULT;
				}
				else if (this.terrainType.func_48626_e())
				{
					int i3 = 0;
					if (nBTTagCompound1.hasKey("generatorVersion"))
					{
						i3 = nBTTagCompound1.getInteger("generatorVersion");
					}

					this.terrainType = this.terrainType.func_48629_a(i3);
				}
			}

			this.gameType = nBTTagCompound1.getInteger("GameType");
			if (nBTTagCompound1.hasKey("MapFeatures"))
			{
				this.mapFeaturesEnabled = nBTTagCompound1.getBoolean("MapFeatures");
			}
			else
			{
				this.mapFeaturesEnabled = true;
			}

			this.spawnX = nBTTagCompound1.getInteger("SpawnX");
			this.spawnY = nBTTagCompound1.getInteger("SpawnY");
			this.spawnZ = nBTTagCompound1.getInteger("SpawnZ");
			this.worldTime = nBTTagCompound1.getLong("Time");
			this.lastTimePlayed = nBTTagCompound1.getLong("LastPlayed");
			this.sizeOnDisk = nBTTagCompound1.getLong("SizeOnDisk");
			this.levelName = nBTTagCompound1.getString("LevelName");
			this.saveVersion = nBTTagCompound1.getInteger("version");
			this.rainTime = nBTTagCompound1.getInteger("rainTime");
			this.raining = nBTTagCompound1.getBoolean("raining");
			this.thunderTime = nBTTagCompound1.getInteger("thunderTime");
			this.thundering = nBTTagCompound1.getBoolean("thundering");
			this.hardcore = nBTTagCompound1.getBoolean("hardcore");
			if (nBTTagCompound1.hasKey("Player"))
			{
				this.playerTag = nBTTagCompound1.getCompoundTag("Player");
				this.dimension = this.playerTag.getInteger("Dimension");
			}

		}

		public WorldInfo(WorldSettings worldSettings1, string string2)
		{
			this.randomSeed = worldSettings1.Seed;
			this.gameType = worldSettings1.GameType;
			this.mapFeaturesEnabled = worldSettings1.MapFeaturesEnabled;
			this.levelName = string2;
			this.hardcore = worldSettings1.HardcoreEnabled;
			this.terrainType = worldSettings1.TerrainType;
		}

		public WorldInfo(WorldInfo worldInfo1)
		{
			this.randomSeed = worldInfo1.randomSeed;
			this.terrainType = worldInfo1.terrainType;
			this.gameType = worldInfo1.gameType;
			this.mapFeaturesEnabled = worldInfo1.mapFeaturesEnabled;
			this.spawnX = worldInfo1.spawnX;
			this.spawnY = worldInfo1.spawnY;
			this.spawnZ = worldInfo1.spawnZ;
			this.worldTime = worldInfo1.worldTime;
			this.lastTimePlayed = worldInfo1.lastTimePlayed;
			this.sizeOnDisk = worldInfo1.sizeOnDisk;
			this.playerTag = worldInfo1.playerTag;
			this.dimension = worldInfo1.dimension;
			this.levelName = worldInfo1.levelName;
			this.saveVersion = worldInfo1.saveVersion;
			this.rainTime = worldInfo1.rainTime;
			this.raining = worldInfo1.raining;
			this.thunderTime = worldInfo1.thunderTime;
			this.thundering = worldInfo1.thundering;
			this.hardcore = worldInfo1.hardcore;
		}

		public virtual NBTTagCompound NBTTagCompound
		{
			get
			{
				NBTTagCompound nBTTagCompound1 = new NBTTagCompound();
				this.updateTagCompound(nBTTagCompound1, this.playerTag);
				return nBTTagCompound1;
			}
		}

		public virtual NBTTagCompound getNBTTagCompoundWithPlayers(System.Collections.IList list1)
		{
			NBTTagCompound nBTTagCompound2 = new NBTTagCompound();
			EntityPlayer entityPlayer3 = null;
			NBTTagCompound nBTTagCompound4 = null;
			if (list1.Count > 0)
			{
				entityPlayer3 = (EntityPlayer)list1[0];
			}

			if (entityPlayer3 != null)
			{
				nBTTagCompound4 = new NBTTagCompound();
				entityPlayer3.writeToNBT(nBTTagCompound4);
			}

			this.updateTagCompound(nBTTagCompound2, nBTTagCompound4);
			return nBTTagCompound2;
		}

		private void updateTagCompound(NBTTagCompound nBTTagCompound1, NBTTagCompound nBTTagCompound2)
		{
			nBTTagCompound1.setLong("RandomSeed", this.randomSeed);
			nBTTagCompound1.setString("generatorName", this.terrainType.func_48628_a());
			nBTTagCompound1.setInteger("generatorVersion", this.terrainType.GeneratorVersion);
			nBTTagCompound1.setInteger("GameType", this.gameType);
			nBTTagCompound1.setBoolean("MapFeatures", this.mapFeaturesEnabled);
			nBTTagCompound1.setInteger("SpawnX", this.spawnX);
			nBTTagCompound1.setInteger("SpawnY", this.spawnY);
			nBTTagCompound1.setInteger("SpawnZ", this.spawnZ);
			nBTTagCompound1.setLong("Time", this.worldTime);
			nBTTagCompound1.setLong("SizeOnDisk", this.sizeOnDisk);
			nBTTagCompound1.setLong("LastPlayed", DateTimeHelper.CurrentUnixTimeMillis());
			nBTTagCompound1.setString("LevelName", this.levelName);
			nBTTagCompound1.setInteger("version", this.saveVersion);
			nBTTagCompound1.setInteger("rainTime", this.rainTime);
			nBTTagCompound1.setBoolean("raining", this.raining);
			nBTTagCompound1.setInteger("thunderTime", this.thunderTime);
			nBTTagCompound1.setBoolean("thundering", this.thundering);
			nBTTagCompound1.setBoolean("hardcore", this.hardcore);
			if (nBTTagCompound2 != null)
			{
				nBTTagCompound1.setCompoundTag("Player", nBTTagCompound2);
			}

		}

		public virtual long Seed
		{
			get
			{
				return this.randomSeed;
			}
		}

		public virtual int SpawnX
		{
			get
			{
				return this.spawnX;
			}
			set
			{
				this.spawnX = value;
			}
		}

		public virtual int SpawnY
		{
			get
			{
				return this.spawnY;
			}
			set
			{
				this.spawnY = value;
			}
		}

		public virtual int SpawnZ
		{
			get
			{
				return this.spawnZ;
			}
			set
			{
				this.spawnZ = value;
			}
		}

		public virtual long WorldTime
		{
			get
			{
				return this.worldTime;
			}
			set
			{
				this.worldTime = value;
			}
		}

		public virtual long SizeOnDisk
		{
			get
			{
				return this.sizeOnDisk;
			}
		}

		public virtual NBTTagCompound PlayerNBTTagCompound
		{
			get
			{
				return this.playerTag;
			}
			set
			{
				this.playerTag = value;
			}
		}

		public virtual int Dimension
		{
			get
			{
				return this.dimension;
			}
		}






		public virtual void setSpawnPosition(int i1, int i2, int i3)
		{
			this.spawnX = i1;
			this.spawnY = i2;
			this.spawnZ = i3;
		}

		public virtual string WorldName
		{
			get
			{
				return this.levelName;
			}
			set
			{
				this.levelName = value;
			}
		}


		public virtual int SaveVersion
		{
			get
			{
				return this.saveVersion;
			}
			set
			{
				this.saveVersion = value;
			}
		}


		public virtual long LastTimePlayed
		{
			get
			{
				return this.lastTimePlayed;
			}
		}

		public virtual bool Thundering
		{
			get
			{
				return this.thundering;
			}
			set
			{
				this.thundering = value;
			}
		}


		public virtual int ThunderTime
		{
			get
			{
				return this.thunderTime;
			}
			set
			{
				this.thunderTime = value;
			}
		}


		public virtual bool Raining
		{
			get
			{
				return this.raining;
			}
			set
			{
				this.raining = value;
			}
		}


		public virtual int RainTime
		{
			get
			{
				return this.rainTime;
			}
			set
			{
				this.rainTime = value;
			}
		}


		public virtual int GameType
		{
			get
			{
				return this.gameType;
			}
		}

		public virtual bool MapFeaturesEnabled
		{
			get
			{
				return this.mapFeaturesEnabled;
			}
		}

		public virtual bool HardcoreModeEnabled
		{
			get
			{
				return this.hardcore;
			}
		}

		public virtual WorldType TerrainType
		{
			get
			{
				return this.terrainType;
			}
			set
			{
				this.terrainType = value;
			}
		}

	}

}