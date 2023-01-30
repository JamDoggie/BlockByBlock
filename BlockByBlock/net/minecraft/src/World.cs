using BlockByBlock.helpers;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace net.minecraft.src
{

    public class World : IBlockAccess
	{
		public bool scheduledUpdatesAreImmediate;
		public System.Collections.IList loadedEntityList;
		private System.Collections.IList unloadedEntityList;
		private IList scheduledTickTreeSet;
		private IList scheduledTickSet;
		public System.Collections.IList loadedTileEntityList;
		private System.Collections.IList addedTileEntityList;
		private System.Collections.IList entityRemoval;
		public System.Collections.IList playerEntities;
		public System.Collections.IList weatherEffects;
		private long cloudColour;
		public int skylightSubtracted;
		protected internal int updateLCG;
		protected internal readonly int DIST_HASH_MAGIC;
		protected internal float prevRainingStrength;
		protected internal float rainingStrength;
		protected internal float prevThunderingStrength;
		protected internal float thunderingStrength;
		protected internal int lastLightningBolt;
		public int lightningFlash;
		public bool editingBlocks;
		private long lockTimestamp;
		protected internal int autosavePeriod;
		public int difficultySetting;
		public RandomExtended rand;
		public bool isNewWorld;
		public readonly WorldProvider worldProvider;
		protected internal System.Collections.IList worldAccesses;
		protected internal IChunkProvider chunkProvider;
		protected internal readonly ISaveHandler saveHandler;
		protected internal WorldInfo worldInfo;
		public bool findingSpawnPoint;
		private bool allPlayersSleeping;
		public MapStorage mapStorage;
		public readonly VillageCollection villageCollectionObj;
		private readonly VillageSiege villageSiegeObj;
		private ArrayList collidingBoundingBoxes;
		private bool scanningTileEntities;
		protected internal bool spawnHostileMobs;
		protected internal bool spawnPeacefulMobs;
		protected internal ISet<object> activeChunkSet;
		private int ambientTickCountdown;
		internal int[] lightUpdateBlockList;
		private System.Collections.IList entitiesWithinAABBExcludingEntity;
		public bool isRemote;

		public virtual BiomeGenBase getBiomeGenForCoords(int i1, int i2)
		{
			if (this.blockExists(i1, 0, i2))
			{
				Chunk chunk3 = this.getChunkFromBlockCoords(i1, i2);
				if (chunk3 != null)
				{
					return chunk3.func_48490_a(i1 & 15, i2 & 15, this.worldProvider.worldChunkMgr);
				}
			}

			return this.worldProvider.worldChunkMgr.getBiomeGenAt(i1, i2);
		}

		public virtual WorldChunkManager WorldChunkManager
		{
			get
			{
				return this.worldProvider.worldChunkMgr;
			}
		}

		public World(ISaveHandler iSaveHandler1, string string2, WorldProvider worldProvider3, WorldSettings worldSettings4)
		{
			scheduledUpdatesAreImmediate = false;
			loadedEntityList = new ArrayList();
			unloadedEntityList = new ArrayList();
			scheduledTickTreeSet = new ArrayList();
			scheduledTickSet = new ArrayList();
			loadedTileEntityList = new ArrayList();
			addedTileEntityList = new ArrayList();
			entityRemoval = new ArrayList();
			playerEntities = new ArrayList();
			weatherEffects = new ArrayList();
			cloudColour = 16777215L;
			skylightSubtracted = 0;
			updateLCG = (new Random()).Next();
			DIST_HASH_MAGIC = 1013904223;
			lastLightningBolt = 0;
			lightningFlash = 0;
			editingBlocks = false;
			lockTimestamp = DateTimeHelper.CurrentUnixTimeMillis();
			autosavePeriod = 40;
			rand = new RandomExtended();
			isNewWorld = false;
			worldAccesses = new ArrayList();
			villageCollectionObj = new VillageCollection(this);
			villageSiegeObj = new VillageSiege(this);
			collidingBoundingBoxes = new ArrayList();
			spawnHostileMobs = true;
			spawnPeacefulMobs = true;
			activeChunkSet = new HashSet<object>();
			ambientTickCountdown = this.rand.Next(12000);
			lightUpdateBlockList = new int[32768];
			entitiesWithinAABBExcludingEntity = new ArrayList();
			isRemote = false;
			saveHandler = iSaveHandler1;
			worldInfo = new WorldInfo(worldSettings4, string2);
			worldProvider = worldProvider3;
			mapStorage = new MapStorage(iSaveHandler1);
			worldProvider3.registerWorld(this);
			chunkProvider = this.createChunkProvider();
			calculateInitialSkylight();
			calculateInitialWeather();
		}

		public World(World world1, WorldProvider worldProvider2)
		{
			scheduledUpdatesAreImmediate = false;
			loadedEntityList = new ArrayList();
			unloadedEntityList = new ArrayList();
			scheduledTickTreeSet = new ArrayList();
			scheduledTickSet = new ArrayList();
			loadedTileEntityList = new ArrayList();
			addedTileEntityList = new ArrayList();
			entityRemoval = new ArrayList();
			playerEntities = new ArrayList();
			weatherEffects = new ArrayList();
			cloudColour = 16777215L;
			skylightSubtracted = 0;
			updateLCG = (new Random()).Next();
			DIST_HASH_MAGIC = 1013904223;
			lastLightningBolt = 0;
			lightningFlash = 0;
			editingBlocks = false;
			lockTimestamp = DateTimeHelper.CurrentUnixTimeMillis();
			autosavePeriod = 40;
			rand = new RandomExtended();
			isNewWorld = false;
			worldAccesses = new ArrayList();
			villageCollectionObj = new VillageCollection(this);
			villageSiegeObj = new VillageSiege(this);
			collidingBoundingBoxes = new ArrayList();
			spawnHostileMobs = true;
			spawnPeacefulMobs = true;
			activeChunkSet = new HashSet<object>();
			ambientTickCountdown = this.rand.Next(12000);
			lightUpdateBlockList = new int[32768];
			entitiesWithinAABBExcludingEntity = new ArrayList();
			isRemote = false;
			lockTimestamp = world1.lockTimestamp;
			saveHandler = world1.saveHandler;
			worldInfo = new WorldInfo(world1.worldInfo);
			mapStorage = new MapStorage(this.saveHandler);
			worldProvider = worldProvider2;
			worldProvider2.registerWorld(this);
			chunkProvider = this.createChunkProvider();
			calculateInitialSkylight();
			calculateInitialWeather();
		}

		public World(ISaveHandler iSaveHandler1, string string2, WorldSettings worldSettings3) : this(iSaveHandler1, string2, (WorldSettings)worldSettings3, (WorldProvider)null)
		{
		}

		public World(ISaveHandler iSaveHandler1, string string2, WorldSettings worldSettings3, WorldProvider worldProvider4)
		{
			scheduledUpdatesAreImmediate = false;
			loadedEntityList = new ArrayList();
			unloadedEntityList = new ArrayList();
			scheduledTickTreeSet = new ArrayList();
			scheduledTickSet = new ArrayList();
			loadedTileEntityList = new ArrayList();
			addedTileEntityList = new ArrayList();
			entityRemoval = new ArrayList();
			playerEntities = new ArrayList();
			weatherEffects = new ArrayList();
			cloudColour = 16777215L;
			skylightSubtracted = 0;
			updateLCG = (new Random()).Next();
			DIST_HASH_MAGIC = 1013904223;
			lastLightningBolt = 0;
			lightningFlash = 0;
			editingBlocks = false;
			lockTimestamp = DateTimeHelper.CurrentUnixTimeMillis();
			autosavePeriod = 40;
			rand = new RandomExtended();
			isNewWorld = false;
			worldAccesses = new ArrayList();
			villageCollectionObj = new VillageCollection(this);
			villageSiegeObj = new VillageSiege(this);
			collidingBoundingBoxes = new ArrayList();
			spawnHostileMobs = true;
			spawnPeacefulMobs = true;
			activeChunkSet = new HashSet<object>();
			ambientTickCountdown = this.rand.Next(12000);
			lightUpdateBlockList = new int[32768];
			entitiesWithinAABBExcludingEntity = new ArrayList();
			isRemote = false;
			saveHandler = iSaveHandler1;
			mapStorage = new MapStorage(iSaveHandler1);
			worldInfo = iSaveHandler1.loadWorldInfo();
			isNewWorld = this.worldInfo == null;
			if (worldProvider4 != null)
			{
				this.worldProvider = worldProvider4;
			}
			else if (this.worldInfo != null && this.worldInfo.Dimension != 0)
			{
				this.worldProvider = WorldProvider.getProviderForDimension(this.worldInfo.Dimension);
			}
			else
			{
				this.worldProvider = WorldProvider.getProviderForDimension(0);
			}

			bool z5 = false;
			if (this.worldInfo == null)
			{
				this.worldInfo = new WorldInfo(worldSettings3, string2);
				z5 = true;
			}
			else
			{
				this.worldInfo.WorldName = string2;
			}

			this.worldProvider.registerWorld(this);
			this.chunkProvider = this.createChunkProvider();
			if (z5)
			{
				this.generateSpawnPoint();
			}

			this.calculateInitialSkylight();
			this.calculateInitialWeather();
		}

		protected internal virtual IChunkProvider createChunkProvider()
		{
			IChunkLoader iChunkLoader1 = this.saveHandler.getChunkLoader(this.worldProvider);
			return new ChunkProvider(this, iChunkLoader1, this.worldProvider.ChunkProvider);
		}

		protected internal virtual void generateSpawnPoint()
		{
			if (!this.worldProvider.canRespawnHere())
			{
				this.worldInfo.setSpawnPosition(0, this.worldProvider.AverageGroundLevel, 0);
			}
			else
			{
				this.findingSpawnPoint = true;
				WorldChunkManager worldChunkManager1 = this.worldProvider.worldChunkMgr;
				System.Collections.IList list2 = worldChunkManager1.BiomesToSpawnIn;
				RandomExtended random3 = new RandomExtended(this.Seed);
				ChunkPosition? chunkPosition4 = worldChunkManager1.findBiomePosition(0, 0, 256, list2, random3);
				int i5 = 0;
				int i6 = this.worldProvider.AverageGroundLevel;
				int i7 = 0;
				if (chunkPosition4 != null)
				{
					i5 = chunkPosition4.Value.x;
					i7 = chunkPosition4.Value.z;
				}
				else
				{
					Console.WriteLine("Unable to find spawn biome");
				}

				int i8 = 0;

				while (!this.worldProvider.canCoordinateBeSpawn(i5, i7))
				{
					i5 += random3.Next(64) - random3.Next(64);
					i7 += random3.Next(64) - random3.Next(64);
					++i8;
					if (i8 == 1000)
					{
						break;
					}
				}

				this.worldInfo.setSpawnPosition(i5, i6, i7);
				this.findingSpawnPoint = false;
			}
		}

		public virtual ChunkCoordinates EntrancePortalLocation
		{
			get
			{
				return this.worldProvider.EntrancePortalLocation;
			}
		}

		public virtual void setSpawnLocation()
		{
			if (this.worldInfo.SpawnY <= 0)
			{
				this.worldInfo.SpawnY = 64;
			}

			int i1 = this.worldInfo.SpawnX;
			int i2 = this.worldInfo.SpawnZ;
			int i3 = 0;

			while (this.getFirstUncoveredBlock(i1, i2) == 0)
			{
				i1 += this.rand.Next(8) - this.rand.Next(8);
				i2 += this.rand.Next(8) - this.rand.Next(8);
				++i3;
				if (i3 == 10000)
				{
					break;
				}
			}

			this.worldInfo.SpawnX = i1;
			this.worldInfo.SpawnZ = i2;
		}

		public virtual int getFirstUncoveredBlock(int i1, int i2)
		{
			int i3;
			for (i3 = 63; !this.isAirBlock(i1, i3 + 1, i2); ++i3)
			{
			}

			return this.getBlockId(i1, i3, i2);
		}

		public virtual void func_6464_c()
		{
		}

		public virtual void spawnPlayerWithLoadedChunks(EntityPlayer entityPlayer1)
		{
			try
			{
				NBTTagCompound nBTTagCompound2 = this.worldInfo.PlayerNBTTagCompound;
				if (nBTTagCompound2 != null)
				{
					entityPlayer1.readFromNBT(nBTTagCompound2);
					this.worldInfo.PlayerNBTTagCompound = (NBTTagCompound)null;
				}

				if (this.chunkProvider is ChunkProviderLoadOrGenerate)
				{
					ChunkProviderLoadOrGenerate chunkProviderLoadOrGenerate3 = (ChunkProviderLoadOrGenerate)this.chunkProvider;
					int i4 = MathHelper.floor_float((float)((int)entityPlayer1.posX)) >> 4;
					int i5 = MathHelper.floor_float((float)((int)entityPlayer1.posZ)) >> 4;
					chunkProviderLoadOrGenerate3.setCurrentChunkOver(i4, i5);
				}

				this.spawnEntityInWorld(entityPlayer1);
			}
			catch (Exception exception6)
			{
				Console.WriteLine(exception6.ToString());
				Console.Write(exception6.StackTrace);
			}

		}

		public virtual void saveWorld(bool z1, IProgressUpdate iProgressUpdate2)
		{
			if (this.chunkProvider.canSave())
			{
				if (iProgressUpdate2 != null)
				{
					iProgressUpdate2.displaySavingString("Saving level");
				}

				this.saveLevel();
				if (iProgressUpdate2 != null)
				{
					iProgressUpdate2.displayLoadingString("Saving chunks");
				}

				this.chunkProvider.saveChunks(z1, iProgressUpdate2);
			}
		}

		private void saveLevel()
		{
			this.checkSessionLock();
			this.saveHandler.saveWorldInfoAndPlayer(this.worldInfo, this.playerEntities);
			this.mapStorage.saveAllData();
		}

		public virtual bool quickSaveWorld(int i1)
		{
			if (!this.chunkProvider.canSave())
			{
				return true;
			}
			else
			{
				if (i1 == 0)
				{
					this.saveLevel();
				}

				return this.chunkProvider.saveChunks(false, (IProgressUpdate)null);
			}
		}

		public virtual int getBlockId(int i1, int i2, int i3)
		{
			return i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000 ? (i2 < 0 ? 0 : (i2 >= 256 ? 0 : this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4).getBlockID(i1 & 15, i2, i3 & 15))) : 0;
		}

		public virtual int func_48462_d(int i1, int i2, int i3)
		{
			return i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000 ? (i2 < 0 ? 0 : (i2 >= 256 ? 0 : this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4).getBlockLightOpacity(i1 & 15, i2, i3 & 15))) : 0;
		}

		public virtual bool isAirBlock(int i1, int i2, int i3)
		{
			return this.getBlockId(i1, i2, i3) == 0;
		}

		public virtual bool blockExists(int i1, int i2, int i3)
		{
			return i2 >= 0 && i2 < 256 ? this.chunkExists(i1 >> 4, i3 >> 4) : false;
		}

		public virtual bool doChunksNearChunkExist(int i1, int i2, int i3, int i4)
		{
			return this.checkChunksExist(i1 - i4, i2 - i4, i3 - i4, i1 + i4, i2 + i4, i3 + i4);
		}

		public virtual bool checkChunksExist(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			if (i5 >= 0 && i2 < 256)
			{
				i1 >>= 4;
				i3 >>= 4;
				i4 >>= 4;
				i6 >>= 4;

				for (int i7 = i1; i7 <= i4; ++i7)
				{
					for (int i8 = i3; i8 <= i6; ++i8)
					{
						if (!this.chunkExists(i7, i8))
						{
							return false;
						}
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		private bool chunkExists(int i1, int i2)
		{
			return this.chunkProvider.chunkExists(i1, i2);
		}

		public virtual Chunk getChunkFromBlockCoords(int i1, int i2)
		{
			return this.getChunkFromChunkCoords(i1 >> 4, i2 >> 4);
		}

		public virtual Chunk getChunkFromChunkCoords(int i1, int i2)
		{
			return this.chunkProvider.provideChunk(i1, i2);
		}

		public virtual bool setBlockAndMetadata(int i1, int i2, int i3, int i4, int i5)
		{
			if (i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000)
			{
				if (i2 < 0)
				{
					return false;
				}
				else if (i2 >= 256)
				{
					return false;
				}
				else
				{
					Chunk chunk6 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
					bool z7 = chunk6.setBlockIDWithMetadata(i1 & 15, i2, i3 & 15, i4, i5);
					Profiler.startSection("checkLight");
					this.updateAllLightTypes(i1, i2, i3);
					Profiler.endSection();
					return z7;
				}
			}
			else
			{
				return false;
			}
		}

		public virtual bool setBlock(int i1, int i2, int i3, int i4)
		{
			if (i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000)
			{
				if (i2 < 0)
				{
					return false;
				}
				else if (i2 >= 256)
				{
					return false;
				}
				else
				{
					Chunk chunk5 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
					bool z6 = chunk5.setBlockID(i1 & 15, i2, i3 & 15, i4);
					Profiler.startSection("checkLight");
					this.updateAllLightTypes(i1, i2, i3);
					Profiler.endSection();
					return z6;
				}
			}
			else
			{
				return false;
			}
		}

		public virtual Material getBlockMaterial(int i1, int i2, int i3)
		{
			int i4 = this.getBlockId(i1, i2, i3);
			return i4 == 0 ? Material.air : Block.blocksList[i4].blockMaterial;
		}

		public virtual int getBlockMetadata(int i1, int i2, int i3)
		{
			if (i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000)
			{
				if (i2 < 0)
				{
					return 0;
				}
				else if (i2 >= 256)
				{
					return 0;
				}
				else
				{
					Chunk chunk4 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
					i1 &= 15;
					i3 &= 15;
					return chunk4.getBlockMetadata(i1, i2, i3);
				}
			}
			else
			{
				return 0;
			}
		}

		public virtual void setBlockMetadataWithNotify(int i1, int i2, int i3, int i4)
		{
			if (this.setBlockMetadata(i1, i2, i3, i4))
			{
				int i5 = this.getBlockId(i1, i2, i3);
				if (Block.requiresSelfNotify[i5 & 4095])
				{
					this.notifyBlockChange(i1, i2, i3, i5);
				}
				else
				{
					this.notifyBlocksOfNeighborChange(i1, i2, i3, i5);
				}
			}

		}

		public virtual bool setBlockMetadata(int i1, int i2, int i3, int i4)
		{
			if (i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000)
			{
				if (i2 < 0)
				{
					return false;
				}
				else if (i2 >= 256)
				{
					return false;
				}
				else
				{
					Chunk chunk5 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
					i1 &= 15;
					i3 &= 15;
					return chunk5.setBlockMetadata(i1, i2, i3, i4);
				}
			}
			else
			{
				return false;
			}
		}

		public virtual bool setBlockWithNotify(int i1, int i2, int i3, int i4)
		{
			if (this.setBlock(i1, i2, i3, i4))
			{
				this.notifyBlockChange(i1, i2, i3, i4);
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual bool setBlockAndMetadataWithNotify(int i1, int i2, int i3, int i4, int i5)
		{
			if (this.setBlockAndMetadata(i1, i2, i3, i4, i5))
			{
				this.notifyBlockChange(i1, i2, i3, i4);
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual void markBlockNeedsUpdate(int i1, int i2, int i3)
		{
			for (int i4 = 0; i4 < this.worldAccesses.Count; ++i4)
			{
				((IWorldAccess)this.worldAccesses[i4]).markBlockNeedsUpdate(i1, i2, i3);
			}

		}

		public virtual void notifyBlockChange(int i1, int i2, int i3, int i4)
		{
			this.markBlockNeedsUpdate(i1, i2, i3);
			this.notifyBlocksOfNeighborChange(i1, i2, i3, i4);
		}

		public virtual void markBlocksDirtyVertical(int i1, int i2, int i3, int i4)
		{
			int i5;
			if (i3 > i4)
			{
				i5 = i4;
				i4 = i3;
				i3 = i5;
			}

			if (!this.worldProvider.hasNoSky)
			{
				for (i5 = i3; i5 <= i4; ++i5)
				{
					this.updateLightByType(EnumSkyBlock.Sky, i1, i5, i2);
				}
			}

			this.markBlocksDirty(i1, i3, i2, i1, i4, i2);
		}

		public virtual void markBlockAsNeedsUpdate(int i1, int i2, int i3)
		{
			for (int i4 = 0; i4 < this.worldAccesses.Count; ++i4)
			{
				((IWorldAccess)this.worldAccesses[i4]).markBlockRangeNeedsUpdate(i1, i2, i3, i1, i2, i3);
			}

		}

		public virtual void markBlocksDirty(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			for (int i7 = 0; i7 < this.worldAccesses.Count; ++i7)
			{
				((IWorldAccess)this.worldAccesses[i7]).markBlockRangeNeedsUpdate(i1, i2, i3, i4, i5, i6);
			}

		}

		public virtual void notifyBlocksOfNeighborChange(int i1, int i2, int i3, int i4)
		{
			this.notifyBlockOfNeighborChange(i1 - 1, i2, i3, i4);
			this.notifyBlockOfNeighborChange(i1 + 1, i2, i3, i4);
			this.notifyBlockOfNeighborChange(i1, i2 - 1, i3, i4);
			this.notifyBlockOfNeighborChange(i1, i2 + 1, i3, i4);
			this.notifyBlockOfNeighborChange(i1, i2, i3 - 1, i4);
			this.notifyBlockOfNeighborChange(i1, i2, i3 + 1, i4);
		}

		private void notifyBlockOfNeighborChange(int i1, int i2, int i3, int i4)
		{
			if (!this.editingBlocks && !this.isRemote)
			{
				Block block5 = Block.blocksList[this.getBlockId(i1, i2, i3)];
				if (block5 != null)
				{
					block5.onNeighborBlockChange(this, i1, i2, i3, i4);
				}

			}
		}

		public virtual bool canBlockSeeTheSky(int i1, int i2, int i3)
		{
			return this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4).canBlockSeeTheSky(i1 & 15, i2, i3 & 15);
		}

		public virtual int getFullBlockLightValue(int i1, int i2, int i3)
		{
			if (i2 < 0)
			{
				return 0;
			}
			else
			{
				if (i2 >= 256)
				{
					i2 = 255;
				}

				return this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4).getBlockLightValue(i1 & 15, i2, i3 & 15, 0);
			}
		}

		public virtual int getBlockLightValue(int i1, int i2, int i3)
		{
			return this.getBlockLightValue_do(i1, i2, i3, true);
		}

		public virtual int getBlockLightValue_do(int i1, int i2, int i3, bool z4)
		{
			if (i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000)
			{
				if (z4)
				{
					int i5 = this.getBlockId(i1, i2, i3);
					if (i5 == Block.stairSingle.blockID || i5 == Block.tilledField.blockID || i5 == Block.stairCompactCobblestone.blockID || i5 == Block.stairCompactPlanks.blockID)
					{
						int i6 = this.getBlockLightValue_do(i1, i2 + 1, i3, false);
						int i7 = this.getBlockLightValue_do(i1 + 1, i2, i3, false);
						int i8 = this.getBlockLightValue_do(i1 - 1, i2, i3, false);
						int i9 = this.getBlockLightValue_do(i1, i2, i3 + 1, false);
						int i10 = this.getBlockLightValue_do(i1, i2, i3 - 1, false);
						if (i7 > i6)
						{
							i6 = i7;
						}

						if (i8 > i6)
						{
							i6 = i8;
						}

						if (i9 > i6)
						{
							i6 = i9;
						}

						if (i10 > i6)
						{
							i6 = i10;
						}

						return i6;
					}
				}

				if (i2 < 0)
				{
					return 0;
				}
				else
				{
					if (i2 >= 256)
					{
						i2 = 255;
					}

					Chunk chunk11 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
					i1 &= 15;
					i3 &= 15;
					return chunk11.getBlockLightValue(i1, i2, i3, this.skylightSubtracted);
				}
			}
			else
			{
				return 15;
			}
		}

		public virtual int getHeightValue(int i1, int i2)
		{
			if (i1 >= -30000000 && i2 >= -30000000 && i1 < 30000000 && i2 < 30000000)
			{
				if (!this.chunkExists(i1 >> 4, i2 >> 4))
				{
					return 0;
				}
				else
				{
					Chunk chunk3 = this.getChunkFromChunkCoords(i1 >> 4, i2 >> 4);
					return chunk3.getHeightValue(i1 & 15, i2 & 15);
				}
			}
			else
			{
				return 0;
			}
		}

		private string blockBrightnessSection = "blockBrightness";
		private string section1 = "section1";
        private string section2 = "section2";
        private string section3 = "section3";
        private string section4 = "section4";
        private string section5 = "section5";
        private string section6 = "section6";

        public virtual int GetSkyBlockTypeBrightness(EnumSkyBlock enumSkyBlock1, int x, int y, int z)
		{
            if (this.worldProvider.hasNoSky && enumSkyBlock1 == EnumSkyBlock.Sky)
            {
                return 0;
            }
            else
            {
                if (y < 0)
                {
                    y = 0;
                }

                if (y >= 256)
                {
                    return enumSkyBlock1.defaultLightValue;
                }
                else if (x >= -30000000 && z >= -30000000 && x < 30000000 && z < 30000000)
                {
                    int i5 = x >> 4;
                    int i6 = z >> 4;
                    if (!this.chunkExists(i5, i6))
                    {
                        return enumSkyBlock1.defaultLightValue;
                    }
                    else if (Block.useNeighborBrightness[this.getBlockId(x, y, z)])
                    {
                        int i12 = this.getSavedLightValue(enumSkyBlock1, x, y + 1, z);
                        int i8 = this.getSavedLightValue(enumSkyBlock1, x + 1, y, z);
                        int i9 = this.getSavedLightValue(enumSkyBlock1, x - 1, y, z);
                        int i10 = this.getSavedLightValue(enumSkyBlock1, x, y, z + 1);
                        int i11 = this.getSavedLightValue(enumSkyBlock1, x, y, z - 1);
                        if (i8 > i12)
                        {
                            i12 = i8;
                        }

                        if (i9 > i12)
                        {
                            i12 = i9;
                        }

                        if (i10 > i12)
                        {
                            i12 = i10;
                        }

                        if (i11 > i12)
                        {
                            i12 = i11;
                        }

                        return i12;
                    }
                    else
                    {
                        Chunk chunk7 = this.getChunkFromChunkCoords(i5, i6);
                        return chunk7.getSavedLightValue(enumSkyBlock1, x & 15, y, z & 15);
                    }
                }
                else
                {
                    return enumSkyBlock1.defaultLightValue;
                }
            }
		}

		public virtual int getSavedLightValue(EnumSkyBlock enumSkyBlock1, int i2, int i3, int i4)
		{
			if (i3 < 0)
			{
				i3 = 0;
			}

			if (i3 >= 256)
			{
				i3 = 255;
			}

			if (i2 >= -30000000 && i4 >= -30000000 && i2 < 30000000 && i4 < 30000000)
			{
				int i5 = i2 >> 4;
				int i6 = i4 >> 4;
				if (!this.chunkExists(i5, i6))
				{
					return enumSkyBlock1.defaultLightValue;
				}
				else
				{
					Chunk chunk7 = this.getChunkFromChunkCoords(i5, i6);
					return chunk7.getSavedLightValue(enumSkyBlock1, i2 & 15, i3, i4 & 15);
				}
			}
			else
			{
				return enumSkyBlock1.defaultLightValue;
			}
		}

		public virtual void setLightValue(EnumSkyBlock enumSkyBlock1, int i2, int i3, int i4, int i5)
		{
			if (i2 >= -30000000 && i4 >= -30000000 && i2 < 30000000 && i4 < 30000000)
			{
				if (i3 >= 0)
				{
					if (i3 < 256)
					{
						if (this.chunkExists(i2 >> 4, i4 >> 4))
						{
							Chunk chunk6 = this.getChunkFromChunkCoords(i2 >> 4, i4 >> 4);
							chunk6.setLightValue(enumSkyBlock1, i2 & 15, i3, i4 & 15, i5);

							for (int i7 = 0; i7 < this.worldAccesses.Count; ++i7)
							{
								((IWorldAccess)this.worldAccesses[i7]).markBlockNeedsUpdate2(i2, i3, i4);
							}

						}
					}
				}
			}
		}

		public virtual void func_48464_p(int i1, int i2, int i3)
		{
			for (int i4 = 0; i4 < this.worldAccesses.Count; ++i4)
			{
				((IWorldAccess)this.worldAccesses[i4]).markBlockNeedsUpdate2(i1, i2, i3);
			}

		}

		public virtual int GetLightBrightnessForSkyBlocks(int x, int y, int z, int minimumBlockLight)
		{
            int skyLight = this.GetSkyBlockTypeBrightness(EnumSkyBlock.Sky, x, y, z);
			int blockLight = this.GetSkyBlockTypeBrightness(EnumSkyBlock.Block, x, y, z);
			if (blockLight < minimumBlockLight)
			{
				blockLight = minimumBlockLight;
			}

			return skyLight << 20 | blockLight << 4;
		}

		public virtual float getBrightness(int i1, int i2, int i3, int i4)
		{
			int i5 = this.getBlockLightValue(i1, i2, i3);
			if (i5 < i4)
			{
				i5 = i4;
			}

			return this.worldProvider.lightBrightnessTable[i5];
		}

		public virtual float getLightBrightness(int i1, int i2, int i3)
		{
			return this.worldProvider.lightBrightnessTable[this.getBlockLightValue(i1, i2, i3)];
		}

		public virtual bool Daytime
		{
			get
			{
				return this.skylightSubtracted < 4;
			}
		}

		public virtual MovingObjectPosition rayTraceBlocks(Vec3D vec3D1, Vec3D vec3D2)
		{
			return this.rayTraceBlocks_do_do(vec3D1, vec3D2, false, false);
		}

		public virtual MovingObjectPosition rayTraceBlocks_do(Vec3D vec3D1, Vec3D vec3D2, bool z3)
		{
			return this.rayTraceBlocks_do_do(vec3D1, vec3D2, z3, false);
		}

		public virtual MovingObjectPosition rayTraceBlocks_do_do(Vec3D vec3D1, Vec3D vec3D2, bool z3, bool z4)
		{
			if (!double.IsNaN(vec3D1.xCoord) && !double.IsNaN(vec3D1.yCoord) && !double.IsNaN(vec3D1.zCoord))
			{
				if (!double.IsNaN(vec3D2.xCoord) && !double.IsNaN(vec3D2.yCoord) && !double.IsNaN(vec3D2.zCoord))
				{
					int i5 = MathHelper.floor_double(vec3D2.xCoord);
					int i6 = MathHelper.floor_double(vec3D2.yCoord);
					int i7 = MathHelper.floor_double(vec3D2.zCoord);
					int i8 = MathHelper.floor_double(vec3D1.xCoord);
					int i9 = MathHelper.floor_double(vec3D1.yCoord);
					int i10 = MathHelper.floor_double(vec3D1.zCoord);
					int i11 = this.getBlockId(i8, i9, i10);
					int i12 = this.getBlockMetadata(i8, i9, i10);
					Block block13 = Block.blocksList[i11];
					if ((!z4 || block13 == null || block13.getCollisionBoundingBoxFromPool(this, i8, i9, i10) != null) && i11 > 0 && block13.canCollideCheck(i12, z3))
					{
						MovingObjectPosition movingObjectPosition14 = block13.collisionRayTrace(this, i8, i9, i10, vec3D1, vec3D2);
						if (movingObjectPosition14 != null)
						{
							return movingObjectPosition14;
						}
					}

					i11 = 200;

					while (i11-- >= 0)
					{
						if (double.IsNaN(vec3D1.xCoord) || double.IsNaN(vec3D1.yCoord) || double.IsNaN(vec3D1.zCoord))
						{
							return null;
						}

						if (i8 == i5 && i9 == i6 && i10 == i7)
						{
							return null;
						}

						bool z39 = true;
						bool z40 = true;
						bool z41 = true;
						double d15 = 999.0D;
						double d17 = 999.0D;
						double d19 = 999.0D;
						if (i5 > i8)
						{
							d15 = (double)i8 + 1.0D;
						}
						else if (i5 < i8)
						{
							d15 = (double)i8 + 0.0D;
						}
						else
						{
							z39 = false;
						}

						if (i6 > i9)
						{
							d17 = (double)i9 + 1.0D;
						}
						else if (i6 < i9)
						{
							d17 = (double)i9 + 0.0D;
						}
						else
						{
							z40 = false;
						}

						if (i7 > i10)
						{
							d19 = (double)i10 + 1.0D;
						}
						else if (i7 < i10)
						{
							d19 = (double)i10 + 0.0D;
						}
						else
						{
							z41 = false;
						}

						double d21 = 999.0D;
						double d23 = 999.0D;
						double d25 = 999.0D;
						double d27 = vec3D2.xCoord - vec3D1.xCoord;
						double d29 = vec3D2.yCoord - vec3D1.yCoord;
						double d31 = vec3D2.zCoord - vec3D1.zCoord;
						if (z39)
						{
							d21 = (d15 - vec3D1.xCoord) / d27;
						}

						if (z40)
						{
							d23 = (d17 - vec3D1.yCoord) / d29;
						}

						if (z41)
						{
							d25 = (d19 - vec3D1.zCoord) / d31;
						}

						bool z33 = false;
						sbyte b42;
						if (d21 < d23 && d21 < d25)
						{
							if (i5 > i8)
							{
								b42 = 4;
							}
							else
							{
								b42 = 5;
							}

							vec3D1.xCoord = d15;
							vec3D1.yCoord += d29 * d21;
							vec3D1.zCoord += d31 * d21;
						}
						else if (d23 < d25)
						{
							if (i6 > i9)
							{
								b42 = 0;
							}
							else
							{
								b42 = 1;
							}

							vec3D1.xCoord += d27 * d23;
							vec3D1.yCoord = d17;
							vec3D1.zCoord += d31 * d23;
						}
						else
						{
							if (i7 > i10)
							{
								b42 = 2;
							}
							else
							{
								b42 = 3;
							}

							vec3D1.xCoord += d27 * d25;
							vec3D1.yCoord += d29 * d25;
							vec3D1.zCoord = d19;
						}

						Vec3D vec3D34 = Vec3D.createVector(vec3D1.xCoord, vec3D1.yCoord, vec3D1.zCoord);
						i8 = (int)(vec3D34.xCoord = (double)MathHelper.floor_double(vec3D1.xCoord));
						if (b42 == 5)
						{
							--i8;
							++vec3D34.xCoord;
						}

						i9 = (int)(vec3D34.yCoord = (double)MathHelper.floor_double(vec3D1.yCoord));
						if (b42 == 1)
						{
							--i9;
							++vec3D34.yCoord;
						}

						i10 = (int)(vec3D34.zCoord = (double)MathHelper.floor_double(vec3D1.zCoord));
						if (b42 == 3)
						{
							--i10;
							++vec3D34.zCoord;
						}

						int i35 = this.getBlockId(i8, i9, i10);
						int i36 = this.getBlockMetadata(i8, i9, i10);
						Block block37 = Block.blocksList[i35];
						if ((!z4 || block37 == null || block37.getCollisionBoundingBoxFromPool(this, i8, i9, i10) != null) && i35 > 0 && block37.canCollideCheck(i36, z3))
						{
							MovingObjectPosition movingObjectPosition38 = block37.collisionRayTrace(this, i8, i9, i10, vec3D1, vec3D2);
							if (movingObjectPosition38 != null)
							{
								return movingObjectPosition38;
							}
						}
					}

					return null;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public virtual void playSoundAtEntity(Entity entity1, string string2, float f3, float f4)
		{
			for (int i5 = 0; i5 < this.worldAccesses.Count; ++i5)
			{
				((IWorldAccess)this.worldAccesses[i5]).playSound(string2, entity1.posX, entity1.posY - (double)entity1.yOffset, entity1.posZ, f3, f4);
			}

		}

		public virtual void playSoundEffect(double d1, double d3, double d5, string string7, float f8, float f9)
		{
			for (int i10 = 0; i10 < this.worldAccesses.Count; ++i10)
			{
				((IWorldAccess)this.worldAccesses[i10]).playSound(string7, d1, d3, d5, f8, f9);
			}

		}

		public virtual void playRecord(string string1, int i2, int i3, int i4)
		{
			for (int i5 = 0; i5 < this.worldAccesses.Count; ++i5)
			{
				((IWorldAccess)this.worldAccesses[i5]).playRecord(string1, i2, i3, i4);
			}

		}

		public virtual void spawnParticle(string string1, double d2, double d4, double d6, double d8, double d10, double d12)
		{
			for (int i14 = 0; i14 < this.worldAccesses.Count; ++i14)
			{
				((IWorldAccess)this.worldAccesses[i14]).spawnParticle(string1, d2, d4, d6, d8, d10, d12);
			}

		}

		public virtual bool addWeatherEffect(Entity entity1)
		{
			this.weatherEffects.Add(entity1);
			return true;
		}

		public virtual bool spawnEntityInWorld(Entity entity1)
		{
			int i2 = MathHelper.floor_double(entity1.posX / 16.0D);
			int i3 = MathHelper.floor_double(entity1.posZ / 16.0D);
			bool z4 = false;
			if (entity1 is EntityPlayer)
			{
				z4 = true;
			}

			if (!z4 && !this.chunkExists(i2, i3))
			{
				return false;
			}
			else
			{
				if (entity1 is EntityPlayer)
				{
					EntityPlayer entityPlayer5 = (EntityPlayer)entity1;
					this.playerEntities.Add(entityPlayer5);
					this.updateAllPlayersSleepingFlag();
				}

				this.getChunkFromChunkCoords(i2, i3).addEntity(entity1);
				this.loadedEntityList.Add(entity1);
				this.obtainEntitySkin(entity1);
				return true;
			}
		}

		protected internal virtual void obtainEntitySkin(Entity entity1)
		{
			for (int i2 = 0; i2 < this.worldAccesses.Count; ++i2)
			{
				((IWorldAccess)this.worldAccesses[i2]).obtainEntitySkin(entity1);
			}

		}

		protected internal virtual void releaseEntitySkin(Entity entity1)
		{
			for (int i2 = 0; i2 < this.worldAccesses.Count; ++i2)
			{
				((IWorldAccess)this.worldAccesses[i2]).releaseEntitySkin(entity1);
			}

		}
        
		public virtual void setEntityDead(Entity entity1)
		{
			if (entity1.riddenByEntity != null)
			{
				entity1.riddenByEntity.mountEntity(null);
			}

			if (entity1.ridingEntity != null)
			{
				entity1.mountEntity(null);
			}

			entity1.setDead();
			if (entity1 is EntityPlayer) {
				playerEntities.Remove((EntityPlayer)entity1);
				updateAllPlayersSleepingFlag();
			}

		}

		public virtual void addWorldAccess(IWorldAccess iWorldAccess1)
		{
			this.worldAccesses.Add(iWorldAccess1);
		}

		public virtual void removeWorldAccess(IWorldAccess iWorldAccess1)
		{
			this.worldAccesses.Remove(iWorldAccess1);
		}

		public virtual System.Collections.IList getCollidingBoundingBoxes(Entity entity1, AxisAlignedBB axisAlignedBB2)
		{
			this.collidingBoundingBoxes.Clear();
			int i3 = MathHelper.floor_double(axisAlignedBB2.minX);
			int i4 = MathHelper.floor_double(axisAlignedBB2.maxX + 1.0D);
			int i5 = MathHelper.floor_double(axisAlignedBB2.minY);
			int i6 = MathHelper.floor_double(axisAlignedBB2.maxY + 1.0D);
			int i7 = MathHelper.floor_double(axisAlignedBB2.minZ);
			int i8 = MathHelper.floor_double(axisAlignedBB2.maxZ + 1.0D);

			for (int i9 = i3; i9 < i4; ++i9)
			{
				for (int i10 = i7; i10 < i8; ++i10)
				{
					if (this.blockExists(i9, 64, i10))
					{
						for (int i11 = i5 - 1; i11 < i6; ++i11)
						{
							Block block12 = Block.blocksList[this.getBlockId(i9, i11, i10)];
							if (block12 != null)
							{
								block12.getCollidingBoundingBoxes(this, i9, i11, i10, axisAlignedBB2, this.collidingBoundingBoxes);
							}
						}
					}
				}
			}

			double d14 = 0.25D;
			System.Collections.IList list15 = this.getEntitiesWithinAABBExcludingEntity(entity1, axisAlignedBB2.expand(d14, d14, d14));

			for (int i16 = 0; i16 < list15.Count; ++i16)
			{
				AxisAlignedBB axisAlignedBB13 = ((Entity)list15[i16]).BoundingBox;
				if (axisAlignedBB13 != null && axisAlignedBB13.intersectsWith(axisAlignedBB2))
				{
					this.collidingBoundingBoxes.Add(axisAlignedBB13);
				}

				axisAlignedBB13 = entity1.getCollisionBox((Entity)list15[i16]);
				if (axisAlignedBB13 != null && axisAlignedBB13.intersectsWith(axisAlignedBB2))
				{
					this.collidingBoundingBoxes.Add(axisAlignedBB13);
				}
			}

			return this.collidingBoundingBoxes;
		}

		public virtual int calculateSkylightSubtracted(float f1)
		{
			float f2 = this.getCelestialAngle(f1);
			float f3 = 1.0F - (MathHelper.cos(f2 * (float)Math.PI * 2.0F) * 2.0F + 0.5F);
			if (f3 < 0.0F)
			{
				f3 = 0.0F;
			}

			if (f3 > 1.0F)
			{
				f3 = 1.0F;
			}

			f3 = 1.0F - f3;
			f3 = (float)((double)f3 * (1.0D - (double)(this.getRainStrength(f1) * 5.0F) / 16.0D));
			f3 = (float)((double)f3 * (1.0D - (double)(this.getWeightedThunderStrength(f1) * 5.0F) / 16.0D));
			f3 = 1.0F - f3;
			return (int)(f3 * 11.0F);
		}

		public virtual float getSkyBrightness(float f1)
		{
			float f2 = this.getCelestialAngle(f1);
			float f3 = 1.0F - (MathHelper.cos(f2 * (float)Math.PI * 2.0F) * 2.0F + 0.2F);
			if (f3 < 0.0F)
			{
				f3 = 0.0F;
			}

			if (f3 > 1.0F)
			{
				f3 = 1.0F;
			}

			f3 = 1.0F - f3;
			f3 = (float)((double)f3 * (1.0D - (double)(this.getRainStrength(f1) * 5.0F) / 16.0D));
			f3 = (float)((double)f3 * (1.0D - (double)(this.getWeightedThunderStrength(f1) * 5.0F) / 16.0D));
			return f3 * 0.8F + 0.2F;
		}

		public virtual Vec3D getSkyColor(Entity entity1, float f2)
		{
			float f3 = this.getCelestialAngle(f2);
			float f4 = MathHelper.cos(f3 * (float)Math.PI * 2.0F) * 2.0F + 0.5F;
			if (f4 < 0.0F)
			{
				f4 = 0.0F;
			}

			if (f4 > 1.0F)
			{
				f4 = 1.0F;
			}

			int i5 = MathHelper.floor_double(entity1.posX);
			int i6 = MathHelper.floor_double(entity1.posZ);
			BiomeGenBase biomeGenBase7 = this.getBiomeGenForCoords(i5, i6);
			float f8 = biomeGenBase7.FloatTemperature;
			int i9 = biomeGenBase7.getSkyColorByTemp(f8);
			float f10 = (float)(i9 >> 16 & 255) / 255.0F;
			float f11 = (float)(i9 >> 8 & 255) / 255.0F;
			float f12 = (float)(i9 & 255) / 255.0F;
			f10 *= f4;
			f11 *= f4;
			f12 *= f4;
			float f13 = this.getRainStrength(f2);
			float f14;
			float f15;
			if (f13 > 0.0F)
			{
				f14 = (f10 * 0.3F + f11 * 0.59F + f12 * 0.11F) * 0.6F;
				f15 = 1.0F - f13 * 0.75F;
				f10 = f10 * f15 + f14 * (1.0F - f15);
				f11 = f11 * f15 + f14 * (1.0F - f15);
				f12 = f12 * f15 + f14 * (1.0F - f15);
			}

			f14 = this.getWeightedThunderStrength(f2);
			if (f14 > 0.0F)
			{
				f15 = (f10 * 0.3F + f11 * 0.59F + f12 * 0.11F) * 0.2F;
				float f16 = 1.0F - f14 * 0.75F;
				f10 = f10 * f16 + f15 * (1.0F - f16);
				f11 = f11 * f16 + f15 * (1.0F - f16);
				f12 = f12 * f16 + f15 * (1.0F - f16);
			}

			if (this.lightningFlash > 0)
			{
				f15 = (float)this.lightningFlash - f2;
				if (f15 > 1.0F)
				{
					f15 = 1.0F;
				}

				f15 *= 0.45F;
				f10 = f10 * (1.0F - f15) + 0.8F * f15;
				f11 = f11 * (1.0F - f15) + 0.8F * f15;
				f12 = f12 * (1.0F - f15) + 1.0F * f15;
			}

            

			return Vec3D.createVector((double)f12, (double)f11, (double)f10);
		}

		public virtual float getCelestialAngle(float f1)
		{
			return this.worldProvider.calculateCelestialAngle(this.worldInfo.WorldTime, f1);
		}

		public virtual int getMoonPhase(float f1)
		{
			return this.worldProvider.getMoonPhase(this.worldInfo.WorldTime, f1);
		}

		public virtual float getCelestialAngleRadians(float f1)
		{
			float f2 = this.getCelestialAngle(f1);
			return f2 * (float)Math.PI * 2.0F;
		}

		public virtual Vec3D drawClouds(float f1)
		{
			float f2 = this.getCelestialAngle(f1);
			float f3 = MathHelper.cos(f2 * (float)Math.PI * 2.0F) * 2.0F + 0.5F;
			if (f3 < 0.0F)
			{
				f3 = 0.0F;
			}

			if (f3 > 1.0F)
			{
				f3 = 1.0F;
			}

			float f4 = (float)(this.cloudColour >> 16 & 255L) / 255.0F;
			float f5 = (float)(this.cloudColour >> 8 & 255L) / 255.0F;
			float f6 = (float)(this.cloudColour & 255L) / 255.0F;
			float f7 = this.getRainStrength(f1);
			float f8;
			float f9;
			if (f7 > 0.0F)
			{
				f8 = (f4 * 0.3F + f5 * 0.59F + f6 * 0.11F) * 0.6F;
				f9 = 1.0F - f7 * 0.95F;
				f4 = f4 * f9 + f8 * (1.0F - f9);
				f5 = f5 * f9 + f8 * (1.0F - f9);
				f6 = f6 * f9 + f8 * (1.0F - f9);
			}

			f4 *= f3 * 0.9F + 0.1F;
			f5 *= f3 * 0.9F + 0.1F;
			f6 *= f3 * 0.85F + 0.15F;
			f8 = this.getWeightedThunderStrength(f1);
			if (f8 > 0.0F)
			{
				f9 = (f4 * 0.3F + f5 * 0.59F + f6 * 0.11F) * 0.2F;
				float f10 = 1.0F - f8 * 0.95F;
				f4 = f4 * f10 + f9 * (1.0F - f10);
				f5 = f5 * f10 + f9 * (1.0F - f10);
				f6 = f6 * f10 + f9 * (1.0F - f10);
			}

			return Vec3D.createVector((double)f4, (double)f5, (double)f6);
		}

		public virtual Vec3D getFogColor(float f1)
		{
			float f2 = this.getCelestialAngle(f1);
			return this.worldProvider.getFogColor(f2, f1);
		}

		public virtual int getPrecipitationHeight(int i1, int i2)
		{
			return this.getChunkFromBlockCoords(i1, i2).getPrecipitationHeight(i1 & 15, i2 & 15);
		}

		public virtual int getTopSolidOrLiquidBlock(int i1, int i2)
		{
			Chunk chunk3 = this.getChunkFromBlockCoords(i1, i2);
			int i4 = chunk3.TopFilledSegment + 16;
			i1 &= 15;

			for (i2 &= 15; i4 > 0; --i4)
			{
				int i5 = chunk3.getBlockID(i1, i4, i2);
				if (i5 != 0 && Block.blocksList[i5].blockMaterial.blocksMovement() && Block.blocksList[i5].blockMaterial != Material.leaves)
				{
					return i4 + 1;
				}
			}

			return -1;
		}

		public virtual float getStarBrightness(float f1)
		{
			float f2 = this.getCelestialAngle(f1);
			float f3 = 1.0F - (MathHelper.cos(f2 * (float)Math.PI * 2.0F) * 2.0F + 0.75F);
			if (f3 < 0.0F)
			{
				f3 = 0.0F;
			}

			if (f3 > 1.0F)
			{
				f3 = 1.0F;
			}

			return f3 * f3 * 0.5F;
		}

		public virtual void scheduleBlockUpdate(int i1, int i2, int i3, int i4, int i5)
		{
			NextTickListEntry nextTickListEntry6 = new NextTickListEntry(i1, i2, i3, i4);
			sbyte b7 = 8;
			if (this.scheduledUpdatesAreImmediate)
			{
				if (this.checkChunksExist(nextTickListEntry6.xCoord - b7, nextTickListEntry6.yCoord - b7, nextTickListEntry6.zCoord - b7, nextTickListEntry6.xCoord + b7, nextTickListEntry6.yCoord + b7, nextTickListEntry6.zCoord + b7))
				{
					int i8 = this.getBlockId(nextTickListEntry6.xCoord, nextTickListEntry6.yCoord, nextTickListEntry6.zCoord);
					if (i8 == nextTickListEntry6.blockID && i8 > 0)
					{
						Block.blocksList[i8].updateTick(this, nextTickListEntry6.xCoord, nextTickListEntry6.yCoord, nextTickListEntry6.zCoord, this.rand);
					}
				}

			}
			else
			{
				if (this.checkChunksExist(i1 - b7, i2 - b7, i3 - b7, i1 + b7, i2 + b7, i3 + b7))
				{
					if (i4 > 0)
					{
						nextTickListEntry6.setScheduledTime((long)i5 + this.worldInfo.WorldTime);
					}

					if (!this.scheduledTickSet.Contains(nextTickListEntry6))
					{
						this.scheduledTickSet.Add(nextTickListEntry6);
						this.scheduledTickTreeSet.Add(nextTickListEntry6);
					}
				}

			}
		}

		public virtual void scheduleBlockUpdateFromLoad(int i1, int i2, int i3, int i4, int i5)
		{
			NextTickListEntry nextTickListEntry6 = new NextTickListEntry(i1, i2, i3, i4);
			if (i4 > 0)
			{
				nextTickListEntry6.setScheduledTime((long)i5 + this.worldInfo.WorldTime);
			}

			if (!this.scheduledTickSet.Contains(nextTickListEntry6))
			{
				this.scheduledTickSet.Add(nextTickListEntry6);
				this.scheduledTickTreeSet.Add(nextTickListEntry6);
			}

		}

		public virtual void updateEntities()
		{
			Profiler.startSection("entities");
			Profiler.startSection("global");

			int i1;
			Entity entity2;
			for (i1 = 0; i1 < this.weatherEffects.Count; ++i1)
			{
				entity2 = (Entity)this.weatherEffects[i1];
				entity2.onUpdate();
				if (entity2.isDead)
				{
					this.weatherEffects.RemoveAt(i1--);
				}
			}

			Profiler.endStartSection("remove");
			this.loadedEntityList.RemoveAll(this.unloadedEntityList);

			int i3;
			int i4;
			for (i1 = 0; i1 < this.unloadedEntityList.Count; ++i1)
			{
				entity2 = (Entity)this.unloadedEntityList[i1];
				i3 = entity2.chunkCoordX;
				i4 = entity2.chunkCoordZ;
				if (entity2.addedToChunk && this.chunkExists(i3, i4))
				{
					this.getChunkFromChunkCoords(i3, i4).removeEntity(entity2);
				}
			}

			for (i1 = 0; i1 < this.unloadedEntityList.Count; ++i1)
			{
				this.releaseEntitySkin((Entity)this.unloadedEntityList[i1]);
			}

			this.unloadedEntityList.Clear();
			Profiler.endStartSection("regular");

			for (i1 = 0; i1 < this.loadedEntityList.Count; ++i1)
			{
				entity2 = (Entity)this.loadedEntityList[i1];
				if (entity2.ridingEntity != null)
				{
					if (!entity2.ridingEntity.isDead && entity2.ridingEntity.riddenByEntity == entity2)
					{
						continue;
					}

					entity2.ridingEntity.riddenByEntity = null;
					entity2.ridingEntity = null;
				}

				if (!entity2.isDead)
				{
					this.updateEntity(entity2);
				}

				Profiler.startSection("remove");
				if (entity2.isDead)
				{
					i3 = entity2.chunkCoordX;
					i4 = entity2.chunkCoordZ;
					if (entity2.addedToChunk && this.chunkExists(i3, i4))
					{
						this.getChunkFromChunkCoords(i3, i4).removeEntity(entity2);
					}

					this.loadedEntityList.RemoveAt(i1--);
					this.releaseEntitySkin(entity2);
				}

				Profiler.endSection();
			}

			Profiler.endStartSection("tileEntities");
			this.scanningTileEntities = true;
			System.Collections.IEnumerator iterator10 = this.loadedTileEntityList.GetEnumerator();

			while (iterator10.MoveNext())
			{
				TileEntity tileEntity5 = (TileEntity)iterator10.Current;
				if (!tileEntity5.Invalid && tileEntity5.worldObj != null && this.blockExists(tileEntity5.xCoord, tileEntity5.yCoord, tileEntity5.zCoord))
				{
					tileEntity5.updateEntity();
				}
			}

			for (int i = loadedTileEntityList.Count - 1; i >= 0; i--)
            {
                TileEntity tileEntity = (TileEntity)loadedTileEntityList[i];

                if (tileEntity.Invalid)
				{
					loadedTileEntityList.RemoveAt(i);
					if (this.chunkExists(tileEntity.xCoord >> 4, tileEntity.zCoord >> 4))
					{
						Chunk chunk7 = this.getChunkFromChunkCoords(tileEntity.xCoord >> 4, tileEntity.zCoord >> 4);
						if (chunk7 != null)
						{
							chunk7.removeChunkBlockTileEntity(tileEntity.xCoord & 15, tileEntity.yCoord, tileEntity.zCoord & 15);
						}
					}
				}
			}

			this.scanningTileEntities = false;
			if (this.entityRemoval.Count > 0)
			{
				this.loadedTileEntityList.RemoveAll(this.entityRemoval);
				this.entityRemoval.Clear();
			}

			Profiler.endStartSection("pendingTileEntities");
			if (this.addedTileEntityList.Count > 0)
			{
				System.Collections.IEnumerator iterator6 = this.addedTileEntityList.GetEnumerator();

				while (iterator6.MoveNext())
				{
					TileEntity tileEntity8 = (TileEntity)iterator6.Current;
					if (!tileEntity8.Invalid)
					{
						if (!this.loadedTileEntityList.Contains(tileEntity8))
						{
							this.loadedTileEntityList.Add(tileEntity8);
						}

						if (this.chunkExists(tileEntity8.xCoord >> 4, tileEntity8.zCoord >> 4))
						{
							Chunk chunk9 = this.getChunkFromChunkCoords(tileEntity8.xCoord >> 4, tileEntity8.zCoord >> 4);
							if (chunk9 != null)
							{
								chunk9.setChunkBlockTileEntity(tileEntity8.xCoord & 15, tileEntity8.yCoord, tileEntity8.zCoord & 15, tileEntity8);
							}
						}

						this.markBlockNeedsUpdate(tileEntity8.xCoord, tileEntity8.yCoord, tileEntity8.zCoord);
					}
				}

				this.addedTileEntityList.Clear();
			}

			Profiler.endSection();
			Profiler.endSection();
		}

		public virtual void addTileEntity(System.Collections.ICollection collection1)
		{
			if (this.scanningTileEntities)
			{
				this.addedTileEntityList.AddRange(collection1);
			}
			else
			{
				this.loadedTileEntityList.AddRange(collection1);
			}

		}

		public virtual void updateEntity(Entity entity1)
		{
			this.updateEntityWithOptionalForce(entity1, true);
		}

		public virtual void updateEntityWithOptionalForce(Entity entity1, bool z2)
		{
			int i3 = MathHelper.floor_double(entity1.posX);
			int i4 = MathHelper.floor_double(entity1.posZ);
			sbyte b5 = 32;
			if (!z2 || this.checkChunksExist(i3 - b5, 0, i4 - b5, i3 + b5, 0, i4 + b5))
			{
				entity1.lastTickPosX = entity1.posX;
				entity1.lastTickPosY = entity1.posY;
				entity1.lastTickPosZ = entity1.posZ;
				entity1.prevRotationYaw = entity1.rotationYaw;
				entity1.prevRotationPitch = entity1.rotationPitch;
				if (z2 && entity1.addedToChunk)
				{
					if (entity1.ridingEntity != null)
					{
						entity1.updateRidden();
					}
					else
					{
						entity1.onUpdate();
					}
				}

				Profiler.startSection("chunkCheck");
				if (double.IsNaN(entity1.posX) || double.IsInfinity(entity1.posX))
				{
					entity1.posX = entity1.lastTickPosX;
				}

				if (double.IsNaN(entity1.posY) || double.IsInfinity(entity1.posY))
				{
					entity1.posY = entity1.lastTickPosY;
				}

				if (double.IsNaN(entity1.posZ) || double.IsInfinity(entity1.posZ))
				{
					entity1.posZ = entity1.lastTickPosZ;
				}

				if (double.IsNaN((double)entity1.rotationPitch) || double.IsInfinity((double)entity1.rotationPitch))
				{
					entity1.rotationPitch = entity1.prevRotationPitch;
				}

				if (double.IsNaN((double)entity1.rotationYaw) || double.IsInfinity((double)entity1.rotationYaw))
				{
					entity1.rotationYaw = entity1.prevRotationYaw;
				}

				int i6 = MathHelper.floor_double(entity1.posX / 16.0D);
				int i7 = MathHelper.floor_double(entity1.posY / 16.0D);
				int i8 = MathHelper.floor_double(entity1.posZ / 16.0D);
				if (!entity1.addedToChunk || entity1.chunkCoordX != i6 || entity1.chunkCoordY != i7 || entity1.chunkCoordZ != i8)
				{
					if (entity1.addedToChunk && this.chunkExists(entity1.chunkCoordX, entity1.chunkCoordZ))
					{
						this.getChunkFromChunkCoords(entity1.chunkCoordX, entity1.chunkCoordZ).removeEntityAtIndex(entity1, entity1.chunkCoordY);
					}

					if (this.chunkExists(i6, i8))
					{
						entity1.addedToChunk = true;
						this.getChunkFromChunkCoords(i6, i8).addEntity(entity1);
					}
					else
					{
						entity1.addedToChunk = false;
					}
				}

				Profiler.endSection();
				if (z2 && entity1.addedToChunk && entity1.riddenByEntity != null)
				{
					if (!entity1.riddenByEntity.isDead && entity1.riddenByEntity.ridingEntity == entity1)
					{
						this.updateEntity(entity1.riddenByEntity);
					}
					else
					{
						entity1.riddenByEntity.ridingEntity = null;
						entity1.riddenByEntity = null;
					}
				}

			}
		}

		public virtual bool checkIfAABBIsClear(AxisAlignedBB axisAlignedBB1)
		{
			System.Collections.IList list2 = this.getEntitiesWithinAABBExcludingEntity((Entity)null, axisAlignedBB1);

			for (int i3 = 0; i3 < list2.Count; ++i3)
			{
				Entity entity4 = (Entity)list2[i3];
				if (!entity4.isDead && entity4.preventEntitySpawning)
				{
					return false;
				}
			}

			return true;
		}

		public virtual bool isAnyLiquid(AxisAlignedBB axisAlignedBB1)
		{
			int i2 = MathHelper.floor_double(axisAlignedBB1.minX);
			int i3 = MathHelper.floor_double(axisAlignedBB1.maxX + 1.0D);
			int i4 = MathHelper.floor_double(axisAlignedBB1.minY);
			int i5 = MathHelper.floor_double(axisAlignedBB1.maxY + 1.0D);
			int i6 = MathHelper.floor_double(axisAlignedBB1.minZ);
			int i7 = MathHelper.floor_double(axisAlignedBB1.maxZ + 1.0D);
			if (axisAlignedBB1.minX < 0.0D)
			{
				--i2;
			}

			if (axisAlignedBB1.minY < 0.0D)
			{
				--i4;
			}

			if (axisAlignedBB1.minZ < 0.0D)
			{
				--i6;
			}

			for (int i8 = i2; i8 < i3; ++i8)
			{
				for (int i9 = i4; i9 < i5; ++i9)
				{
					for (int i10 = i6; i10 < i7; ++i10)
					{
						Block block11 = Block.blocksList[this.getBlockId(i8, i9, i10)];
						if (block11 != null && block11.blockMaterial.Liquid)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public virtual bool isBoundingBoxBurning(AxisAlignedBB axisAlignedBB1)
		{
			int i2 = MathHelper.floor_double(axisAlignedBB1.minX);
			int i3 = MathHelper.floor_double(axisAlignedBB1.maxX + 1.0D);
			int i4 = MathHelper.floor_double(axisAlignedBB1.minY);
			int i5 = MathHelper.floor_double(axisAlignedBB1.maxY + 1.0D);
			int i6 = MathHelper.floor_double(axisAlignedBB1.minZ);
			int i7 = MathHelper.floor_double(axisAlignedBB1.maxZ + 1.0D);
			if (this.checkChunksExist(i2, i4, i6, i3, i5, i7))
			{
				for (int i8 = i2; i8 < i3; ++i8)
				{
					for (int i9 = i4; i9 < i5; ++i9)
					{
						for (int i10 = i6; i10 < i7; ++i10)
						{
							int i11 = this.getBlockId(i8, i9, i10);
							if (i11 == Block.fire.blockID || i11 == Block.lavaMoving.blockID || i11 == Block.lavaStill.blockID)
							{
								return true;
							}
						}
					}
				}
			}

			return false;
		}

		public virtual bool handleMaterialAcceleration(AxisAlignedBB axisAlignedBB1, Material material2, Entity entity3)
		{
			int i4 = MathHelper.floor_double(axisAlignedBB1.minX);
			int i5 = MathHelper.floor_double(axisAlignedBB1.maxX + 1.0D);
			int i6 = MathHelper.floor_double(axisAlignedBB1.minY);
			int i7 = MathHelper.floor_double(axisAlignedBB1.maxY + 1.0D);
			int i8 = MathHelper.floor_double(axisAlignedBB1.minZ);
			int i9 = MathHelper.floor_double(axisAlignedBB1.maxZ + 1.0D);
			if (!this.checkChunksExist(i4, i6, i8, i5, i7, i9))
			{
				return false;
			}
			else
			{
				bool z10 = false;
				Vec3D vec3D11 = Vec3D.createVector(0.0D, 0.0D, 0.0D);

				for (int i12 = i4; i12 < i5; ++i12)
				{
					for (int i13 = i6; i13 < i7; ++i13)
					{
						for (int i14 = i8; i14 < i9; ++i14)
						{
							Block block15 = Block.blocksList[this.getBlockId(i12, i13, i14)];
							if (block15 != null && block15.blockMaterial == material2)
							{
								double d16 = (double)((float)(i13 + 1) - BlockFluid.getFluidHeightPercent(this.getBlockMetadata(i12, i13, i14)));
								if ((double)i7 >= d16)
								{
									z10 = true;
									block15.velocityToAddToEntity(this, i12, i13, i14, entity3, vec3D11);
								}
							}
						}
					}
				}

				if (vec3D11.lengthVector() > 0.0D)
				{
					vec3D11 = vec3D11.normalize();
					double d18 = 0.014D;
					entity3.motionX += vec3D11.xCoord * d18;
					entity3.motionY += vec3D11.yCoord * d18;
					entity3.motionZ += vec3D11.zCoord * d18;
				}

				return z10;
			}
		}

		public virtual bool isMaterialInBB(AxisAlignedBB axisAlignedBB1, Material material2)
		{
			int i3 = MathHelper.floor_double(axisAlignedBB1.minX);
			int i4 = MathHelper.floor_double(axisAlignedBB1.maxX + 1.0D);
			int i5 = MathHelper.floor_double(axisAlignedBB1.minY);
			int i6 = MathHelper.floor_double(axisAlignedBB1.maxY + 1.0D);
			int i7 = MathHelper.floor_double(axisAlignedBB1.minZ);
			int i8 = MathHelper.floor_double(axisAlignedBB1.maxZ + 1.0D);

			for (int i9 = i3; i9 < i4; ++i9)
			{
				for (int i10 = i5; i10 < i6; ++i10)
				{
					for (int i11 = i7; i11 < i8; ++i11)
					{
						Block block12 = Block.blocksList[this.getBlockId(i9, i10, i11)];
						if (block12 != null && block12.blockMaterial == material2)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public virtual bool isAABBInMaterial(AxisAlignedBB axisAlignedBB1, Material material2)
		{
			int i3 = MathHelper.floor_double(axisAlignedBB1.minX);
			int i4 = MathHelper.floor_double(axisAlignedBB1.maxX + 1.0D);
			int i5 = MathHelper.floor_double(axisAlignedBB1.minY);
			int i6 = MathHelper.floor_double(axisAlignedBB1.maxY + 1.0D);
			int i7 = MathHelper.floor_double(axisAlignedBB1.minZ);
			int i8 = MathHelper.floor_double(axisAlignedBB1.maxZ + 1.0D);

			for (int i9 = i3; i9 < i4; ++i9)
			{
				for (int i10 = i5; i10 < i6; ++i10)
				{
					for (int i11 = i7; i11 < i8; ++i11)
					{
						Block block12 = Block.blocksList[this.getBlockId(i9, i10, i11)];
						if (block12 != null && block12.blockMaterial == material2)
						{
							int i13 = this.getBlockMetadata(i9, i10, i11);
							double d14 = (double)(i10 + 1);
							if (i13 < 8)
							{
								d14 = (double)(i10 + 1) - (double)i13 / 8.0D;
							}

							if (d14 >= axisAlignedBB1.minY)
							{
								return true;
							}
						}
					}
				}
			}

			return false;
		}

		public virtual Explosion createExplosion(Entity entity1, double d2, double d4, double d6, float f8)
		{
			return this.newExplosion(entity1, d2, d4, d6, f8, false);
		}

		public virtual Explosion newExplosion(Entity entity1, double d2, double d4, double d6, float f8, bool z9)
		{
			Explosion explosion10 = new Explosion(this, entity1, d2, d4, d6, f8);
			explosion10.isFlaming = z9;
			explosion10.doExplosionA();
			explosion10.doExplosionB(true);
			return explosion10;
		}

		public virtual float getBlockDensity(Vec3D vec3D1, AxisAlignedBB axisAlignedBB2)
		{
			double d3 = 1.0D / ((axisAlignedBB2.maxX - axisAlignedBB2.minX) * 2.0D + 1.0D);
			double d5 = 1.0D / ((axisAlignedBB2.maxY - axisAlignedBB2.minY) * 2.0D + 1.0D);
			double d7 = 1.0D / ((axisAlignedBB2.maxZ - axisAlignedBB2.minZ) * 2.0D + 1.0D);
			int i9 = 0;
			int i10 = 0;

			for (float f11 = 0.0F; f11 <= 1.0F; f11 = (float)((double)f11 + d3))
			{
				for (float f12 = 0.0F; f12 <= 1.0F; f12 = (float)((double)f12 + d5))
				{
					for (float f13 = 0.0F; f13 <= 1.0F; f13 = (float)((double)f13 + d7))
					{
						double d14 = axisAlignedBB2.minX + (axisAlignedBB2.maxX - axisAlignedBB2.minX) * (double)f11;
						double d16 = axisAlignedBB2.minY + (axisAlignedBB2.maxY - axisAlignedBB2.minY) * (double)f12;
						double d18 = axisAlignedBB2.minZ + (axisAlignedBB2.maxZ - axisAlignedBB2.minZ) * (double)f13;
						if (this.rayTraceBlocks(Vec3D.createVector(d14, d16, d18), vec3D1) == null)
						{
							++i9;
						}

						++i10;
					}
				}
			}

			return (float)i9 / (float)i10;
		}

		public virtual bool func_48457_a(EntityPlayer entityPlayer1, int i2, int i3, int i4, int i5)
		{
			if (i5 == 0)
			{
				--i3;
			}

			if (i5 == 1)
			{
				++i3;
			}

			if (i5 == 2)
			{
				--i4;
			}

			if (i5 == 3)
			{
				++i4;
			}

			if (i5 == 4)
			{
				--i2;
			}

			if (i5 == 5)
			{
				++i2;
			}

			if (this.getBlockId(i2, i3, i4) == Block.fire.blockID)
			{
				this.playAuxSFXAtEntity(entityPlayer1, 1004, i2, i3, i4, 0);
				this.setBlockWithNotify(i2, i3, i4, 0);
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual Entity func_4085_a(Type class1)
		{
			return null;
		}

		public virtual string DebugLoadedEntities
		{
			get
			{
				return "All: " + this.loadedEntityList.Count;
			}
		}

		public virtual string ProviderName
		{
			get
			{
				return this.chunkProvider.makeString();
			}
		}

		public virtual TileEntity getBlockTileEntity(int i1, int i2, int i3)
		{
			if (i2 >= 256)
			{
				return null;
			}
			else
			{
				Chunk chunk4 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
				if (chunk4 == null)
				{
					return null;
				}
				else
				{
					TileEntity tileEntity5 = chunk4.getChunkBlockTileEntity(i1 & 15, i2, i3 & 15);
					if (tileEntity5 == null)
					{
						System.Collections.IEnumerator iterator6 = this.addedTileEntityList.GetEnumerator();

						while (iterator6.MoveNext())
						{
							TileEntity tileEntity7 = (TileEntity)iterator6.Current;
							if (!tileEntity7.Invalid && tileEntity7.xCoord == i1 && tileEntity7.yCoord == i2 && tileEntity7.zCoord == i3)
							{
								tileEntity5 = tileEntity7;
								break;
							}
						}
					}

					return tileEntity5;
				}
			}
		}

		public virtual void setBlockTileEntity(int i1, int i2, int i3, TileEntity tileEntity4)
		{
			if (tileEntity4 != null && !tileEntity4.Invalid)
			{
				if (this.scanningTileEntities)
				{
					tileEntity4.xCoord = i1;
					tileEntity4.yCoord = i2;
					tileEntity4.zCoord = i3;
					this.addedTileEntityList.Add(tileEntity4);
				}
				else
				{
					this.loadedTileEntityList.Add(tileEntity4);
					Chunk chunk5 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
					if (chunk5 != null)
					{
						chunk5.setChunkBlockTileEntity(i1 & 15, i2, i3 & 15, tileEntity4);
					}
				}
			}

		}

		public virtual void removeBlockTileEntity(int i1, int i2, int i3)
		{
			TileEntity tileEntity4 = this.getBlockTileEntity(i1, i2, i3);
			if (tileEntity4 != null && this.scanningTileEntities)
			{
				tileEntity4.invalidate();
				this.addedTileEntityList.Remove(tileEntity4);
			}
			else
			{
				if (tileEntity4 != null)
				{
					this.addedTileEntityList.Remove(tileEntity4);
					this.loadedTileEntityList.Remove(tileEntity4);
				}

				Chunk chunk5 = this.getChunkFromChunkCoords(i1 >> 4, i3 >> 4);
				if (chunk5 != null)
				{
					chunk5.removeChunkBlockTileEntity(i1 & 15, i2, i3 & 15);
				}
			}

		}

		public virtual void markTileEntityForDespawn(TileEntity tileEntity1)
		{
			this.entityRemoval.Add(tileEntity1);
		}

		public virtual bool isBlockOpaqueCube(int i1, int i2, int i3)
		{
			Block block4 = Block.blocksList[this.getBlockId(i1, i2, i3)];
			return block4 == null ? false : block4.OpaqueCube;
		}

		public virtual bool isBlockNormalCube(int i1, int i2, int i3)
		{
			return Block.isNormalCube(this.getBlockId(i1, i2, i3));
		}

		public virtual bool isBlockNormalCubeDefault(int i1, int i2, int i3, bool z4)
		{
			if (i1 >= -30000000 && i3 >= -30000000 && i1 < 30000000 && i3 < 30000000)
			{
				Chunk chunk5 = this.chunkProvider.provideChunk(i1 >> 4, i3 >> 4);
				if (chunk5 != null && !chunk5.Empty)
				{
					Block block6 = Block.blocksList[this.getBlockId(i1, i2, i3)];
					return block6 == null ? false : block6.blockMaterial.Opaque && block6.renderAsNormalBlock();
				}
				else
				{
					return z4;
				}
			}
			else
			{
				return z4;
			}
		}

		public virtual void saveWorldIndirectly(IProgressUpdate iProgressUpdate1)
		{
			this.saveWorld(true, iProgressUpdate1);

			try
			{
				ThreadedFileIOBase.threadedIOInstance.waitForFinish();
			}
			catch (ThreadInterruptedException interruptedException3)
			{
				Console.WriteLine(interruptedException3.ToString());
				Console.Write(interruptedException3.StackTrace);
			}

		}

		public virtual void calculateInitialSkylight()
		{
			int i1 = this.calculateSkylightSubtracted(1.0F);
			if (i1 != this.skylightSubtracted)
			{
				this.skylightSubtracted = i1;
			}

		}

		public virtual void setAllowedSpawnTypes(bool z1, bool z2)
		{
			this.spawnHostileMobs = z1;
			this.spawnPeacefulMobs = z2;
		}

		public virtual void tick()
		{
			if (this.WorldInfo.HardcoreModeEnabled && this.difficultySetting < 3)
			{
				this.difficultySetting = 3;
			}

			this.worldProvider.worldChunkMgr.cleanupCache();
			this.updateWeather();
			long j2;
			if (this.AllPlayersFullyAsleep)
			{
				bool z1 = false;
				if (this.spawnHostileMobs && this.difficultySetting >= 1)
				{
					;
				}

				if (!z1)
				{
					j2 = this.worldInfo.WorldTime + 24000L;
					this.worldInfo.WorldTime = j2 - j2 % 24000L;
					this.wakeUpAllPlayers();
				}
			}

			Profiler.startSection("mobSpawner");
			SpawnerAnimals.performSpawning(this, this.spawnHostileMobs, this.spawnPeacefulMobs && this.worldInfo.WorldTime % 400L == 0L);
			Profiler.endStartSection("chunkSource");
			this.chunkProvider.unload100OldestChunks();
			int i4 = this.calculateSkylightSubtracted(1.0F);
			if (i4 != this.skylightSubtracted)
			{
				this.skylightSubtracted = i4;
			}

			j2 = this.worldInfo.WorldTime + 1L;
			if (j2 % (long)this.autosavePeriod == 0L)
			{
				Profiler.endStartSection("save");
				this.saveWorld(false, (IProgressUpdate)null);
			}

			this.worldInfo.WorldTime = j2;
			Profiler.endStartSection("tickPending");
			this.tickUpdates(false);
			Profiler.endStartSection("tickTiles");
			this.tickBlocksAndAmbiance();
			Profiler.endStartSection("village");
			this.villageCollectionObj.tick();
			this.villageSiegeObj.tick();
			Profiler.endSection();
		}

		private void calculateInitialWeather()
		{
			if (this.worldInfo.Raining)
			{
				this.rainingStrength = 1.0F;
				if (this.worldInfo.Thundering)
				{
					this.thunderingStrength = 1.0F;
				}
			}

		}

		protected internal virtual void updateWeather()
		{
			if (!this.worldProvider.hasNoSky)
			{
				if (this.lastLightningBolt > 0)
				{
					--this.lastLightningBolt;
				}

				int i1 = this.worldInfo.ThunderTime;
				if (i1 <= 0)
				{
					if (this.worldInfo.Thundering)
					{
						this.worldInfo.ThunderTime = this.rand.Next(12000) + 3600;
					}
					else
					{
						this.worldInfo.ThunderTime = this.rand.Next(168000) + 12000;
					}
				}
				else
				{
					--i1;
					this.worldInfo.ThunderTime = i1;
					if (i1 <= 0)
					{
						this.worldInfo.Thundering = !this.worldInfo.Thundering;
					}
				}

				int i2 = this.worldInfo.RainTime;
				if (i2 <= 0)
				{
					if (this.worldInfo.Raining)
					{
						this.worldInfo.RainTime = this.rand.Next(12000) + 12000;
					}
					else
					{
						this.worldInfo.RainTime = this.rand.Next(168000) + 12000;
					}
				}
				else
				{
					--i2;
					this.worldInfo.RainTime = i2;
					if (i2 <= 0)
					{
						this.worldInfo.Raining = !this.worldInfo.Raining;
					}
				}

				this.prevRainingStrength = this.rainingStrength;
				if (this.worldInfo.Raining)
				{
					this.rainingStrength = (float)((double)this.rainingStrength + 0.01D);
				}
				else
				{
					this.rainingStrength = (float)((double)this.rainingStrength - 0.01D);
				}

				if (this.rainingStrength < 0.0F)
				{
					this.rainingStrength = 0.0F;
				}

				if (this.rainingStrength > 1.0F)
				{
					this.rainingStrength = 1.0F;
				}

				this.prevThunderingStrength = this.thunderingStrength;
				if (this.worldInfo.Thundering)
				{
					this.thunderingStrength = (float)((double)this.thunderingStrength + 0.01D);
				}
				else
				{
					this.thunderingStrength = (float)((double)this.thunderingStrength - 0.01D);
				}

				if (this.thunderingStrength < 0.0F)
				{
					this.thunderingStrength = 0.0F;
				}

				if (this.thunderingStrength > 1.0F)
				{
					this.thunderingStrength = 1.0F;
				}

			}
		}

		private void clearWeather()
		{
			this.worldInfo.RainTime = 0;
			this.worldInfo.Raining = false;
			this.worldInfo.ThunderTime = 0;
			this.worldInfo.Thundering = false;
		}

		protected internal virtual void func_48461_r()
		{
			this.activeChunkSet.Clear();
			Profiler.startSection("buildList");

			int i1;
			EntityPlayer entityPlayer2;
			int i3;
			int i4;
			for (i1 = 0; i1 < this.playerEntities.Count; ++i1)
			{
				entityPlayer2 = (EntityPlayer)this.playerEntities[i1];
				i3 = MathHelper.floor_double(entityPlayer2.posX / 16.0D);
				i4 = MathHelper.floor_double(entityPlayer2.posZ / 16.0D);
				sbyte b5 = 7;

				for (int i6 = -b5; i6 <= b5; ++i6)
				{
					for (int i7 = -b5; i7 <= b5; ++i7)
					{
						this.activeChunkSet.Add(new ChunkCoordIntPair(i6 + i3, i7 + i4));
					}
				}
			}

			Profiler.endSection();
			if (this.ambientTickCountdown > 0)
			{
				--this.ambientTickCountdown;
			}

			Profiler.startSection("playerCheckLight");
			if (this.playerEntities.Count > 0)
			{
				i1 = this.rand.Next(this.playerEntities.Count);
				entityPlayer2 = (EntityPlayer)this.playerEntities[i1];
				i3 = MathHelper.floor_double(entityPlayer2.posX) + this.rand.Next(11) - 5;
				i4 = MathHelper.floor_double(entityPlayer2.posY) + this.rand.Next(11) - 5;
				int i8 = MathHelper.floor_double(entityPlayer2.posZ) + this.rand.Next(11) - 5;
				this.updateAllLightTypes(i3, i4, i8);
			}

			Profiler.endSection();
		}

		protected internal virtual void func_48458_a(int i1, int i2, Chunk chunk3)
		{
			Profiler.endStartSection("tickChunk");
			chunk3.updateSkylight();
			Profiler.endStartSection("moodSound");
			if (this.ambientTickCountdown == 0)
			{
				this.updateLCG = this.updateLCG * 3 + 1013904223;
				int i4 = this.updateLCG >> 2;
				int i5 = i4 & 15;
				int i6 = i4 >> 8 & 15;
				int i7 = i4 >> 16 & 127;
				int i8 = chunk3.getBlockID(i5, i7, i6);
				i5 += i1;
				i6 += i2;
				if (i8 == 0 && this.getFullBlockLightValue(i5, i7, i6) <= this.rand.Next(8) && this.getSavedLightValue(EnumSkyBlock.Sky, i5, i7, i6) <= 0)
				{
					EntityPlayer entityPlayer9 = this.getClosestPlayer((double)i5 + 0.5D, (double)i7 + 0.5D, (double)i6 + 0.5D, 8.0D);
					if (entityPlayer9 != null && entityPlayer9.getDistanceSq((double)i5 + 0.5D, (double)i7 + 0.5D, (double)i6 + 0.5D) > 4.0D)
					{
						this.playSoundEffect((double)i5 + 0.5D, (double)i7 + 0.5D, (double)i6 + 0.5D, "ambient.cave.cave", 0.7F, 0.8F + this.rand.NextSingle() * 0.2F);
						this.ambientTickCountdown = this.rand.Next(12000) + 6000;
					}
				}
			}

			Profiler.endStartSection("checkLight");
			chunk3.enqueueRelightChecks();
		}

		protected internal virtual void tickBlocksAndAmbiance()
		{
			this.func_48461_r();
			int i1 = 0;
			int i2 = 0;
			System.Collections.IEnumerator iterator3 = this.activeChunkSet.GetEnumerator();

			Profiler.startSection("iterator");
			while (iterator3.MoveNext())
			{
				ChunkCoordIntPair chunkCoordIntPair4 = (ChunkCoordIntPair)iterator3.Current;
				int i5 = chunkCoordIntPair4.chunkXPos * 16;
				int i6 = chunkCoordIntPair4.chunkZPos * 16;
				Profiler.startSection("getChunk");
				Chunk chunk7 = this.getChunkFromChunkCoords(chunkCoordIntPair4.chunkXPos, chunkCoordIntPair4.chunkZPos);
				this.func_48458_a(i5, i6, chunk7);
				Profiler.endStartSection("thunder");
				int i8;
				int i9;
				int i10;
				int i11;
				if (this.rand.Next(100000) == 0 && this.Raining && this.Thundering)
				{
					this.updateLCG = this.updateLCG * 3 + 1013904223;
					i8 = this.updateLCG >> 2;
					i9 = i5 + (i8 & 15);
					i10 = i6 + (i8 >> 8 & 15);
					i11 = this.getPrecipitationHeight(i9, i10);
					if (this.canLightningStrikeAt(i9, i11, i10))
					{
						this.addWeatherEffect(new EntityLightningBolt(this, (double)i9, (double)i11, (double)i10));
						this.lastLightningBolt = 2;
					}
				}

				Profiler.endStartSection("iceandsnow");
				if (this.rand.Next(16) == 0)
				{
					this.updateLCG = this.updateLCG * 3 + 1013904223;
					i8 = this.updateLCG >> 2;
					i9 = i8 & 15;
					i10 = i8 >> 8 & 15;
					i11 = this.getPrecipitationHeight(i9 + i5, i10 + i6);
					if (this.isBlockHydratedIndirectly(i9 + i5, i11 - 1, i10 + i6))
					{
						this.setBlockWithNotify(i9 + i5, i11 - 1, i10 + i6, Block.ice.blockID);
					}

					if (this.Raining && this.canSnowAt(i9 + i5, i11, i10 + i6))
					{
						this.setBlockWithNotify(i9 + i5, i11, i10 + i6, Block.snow.blockID);
					}
				}

				Profiler.endStartSection("tickTiles");
				ExtendedBlockStorage[] extendedBlockStorage19 = chunk7.BlockStorageArray;
				i9 = extendedBlockStorage19.Length;

				for (i10 = 0; i10 < i9; ++i10)
				{
					ExtendedBlockStorage extendedBlockStorage20 = extendedBlockStorage19[i10];
					if (extendedBlockStorage20 != null && extendedBlockStorage20.NeedsRandomTick)
					{
						for (int i12 = 0; i12 < 3; ++i12)
						{
							this.updateLCG = this.updateLCG * 3 + 1013904223;
							int i13 = this.updateLCG >> 2;
							int i14 = i13 & 15;
							int i15 = i13 >> 8 & 15;
							int i16 = i13 >> 16 & 15;
							int i17 = extendedBlockStorage20.getExtBlockID(i14, i16, i15);
							++i2;
							Block block18 = Block.blocksList[i17];
							if (block18 != null && block18.TickRandomly)
							{
								++i1;
								// NOTE: this is slow sometimes. Investigate.
								block18.updateTick(this, i14 + i5, i16 + extendedBlockStorage20.YLocation, i15 + i6, this.rand);
							}
						}
					}
				}

				Profiler.endSection();
			}

			Profiler.endSection();
		}

		public virtual bool isBlockHydratedDirectly(int i1, int i2, int i3)
		{
			return this.isBlockHydrated(i1, i2, i3, false);
		}

		public virtual bool isBlockHydratedIndirectly(int i1, int i2, int i3)
		{
			return this.isBlockHydrated(i1, i2, i3, true);
		}

		public virtual bool isBlockHydrated(int i1, int i2, int i3, bool z4)
		{
			BiomeGenBase biomeGenBase5 = this.getBiomeGenForCoords(i1, i3);
			float f6 = biomeGenBase5.FloatTemperature;
			if (f6 > 0.15F)
			{
				return false;
			}
			else
			{
				if (i2 >= 0 && i2 < 256 && this.getSavedLightValue(EnumSkyBlock.Block, i1, i2, i3) < 10)
				{
					int i7 = this.getBlockId(i1, i2, i3);
					if ((i7 == Block.waterStill.blockID || i7 == Block.waterMoving.blockID) && this.getBlockMetadata(i1, i2, i3) == 0)
					{
						if (!z4)
						{
							return true;
						}

						bool z8 = true;
						if (z8 && this.getBlockMaterial(i1 - 1, i2, i3) != Material.water)
						{
							z8 = false;
						}

						if (z8 && this.getBlockMaterial(i1 + 1, i2, i3) != Material.water)
						{
							z8 = false;
						}

						if (z8 && this.getBlockMaterial(i1, i2, i3 - 1) != Material.water)
						{
							z8 = false;
						}

						if (z8 && this.getBlockMaterial(i1, i2, i3 + 1) != Material.water)
						{
							z8 = false;
						}

						if (!z8)
						{
							return true;
						}
					}
				}

				return false;
			}
		}

		public virtual bool canSnowAt(int i1, int i2, int i3)
		{
			BiomeGenBase biomeGenBase4 = this.getBiomeGenForCoords(i1, i3);
			float f5 = biomeGenBase4.FloatTemperature;
			if (f5 > 0.15F)
			{
				return false;
			}
			else
			{
				if (i2 >= 0 && i2 < 256 && this.getSavedLightValue(EnumSkyBlock.Block, i1, i2, i3) < 10)
				{
					int i6 = this.getBlockId(i1, i2 - 1, i3);
					int i7 = this.getBlockId(i1, i2, i3);
					if (i7 == 0 && Block.snow.canPlaceBlockAt(this, i1, i2, i3) && i6 != 0 && i6 != Block.ice.blockID && Block.blocksList[i6].blockMaterial.blocksMovement())
					{
						return true;
					}
				}

				return false;
			}
		}

		public virtual void updateAllLightTypes(int i1, int i2, int i3)
		{
			if (!this.worldProvider.hasNoSky)
			{
				this.updateLightByType(EnumSkyBlock.Sky, i1, i2, i3);
			}

			this.updateLightByType(EnumSkyBlock.Block, i1, i2, i3);
		}

		private int computeSkyLightValue(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			int i7 = 0;
			if (this.canBlockSeeTheSky(i2, i3, i4))
			{
				i7 = 15;
			}
			else
			{
				if (i6 == 0)
				{
					i6 = 1;
				}

				int i8 = this.getSavedLightValue(EnumSkyBlock.Sky, i2 - 1, i3, i4) - i6;
				int i9 = this.getSavedLightValue(EnumSkyBlock.Sky, i2 + 1, i3, i4) - i6;
				int i10 = this.getSavedLightValue(EnumSkyBlock.Sky, i2, i3 - 1, i4) - i6;
				int i11 = this.getSavedLightValue(EnumSkyBlock.Sky, i2, i3 + 1, i4) - i6;
				int i12 = this.getSavedLightValue(EnumSkyBlock.Sky, i2, i3, i4 - 1) - i6;
				int i13 = this.getSavedLightValue(EnumSkyBlock.Sky, i2, i3, i4 + 1) - i6;
				if (i8 > i7)
				{
					i7 = i8;
				}

				if (i9 > i7)
				{
					i7 = i9;
				}

				if (i10 > i7)
				{
					i7 = i10;
				}

				if (i11 > i7)
				{
					i7 = i11;
				}

				if (i12 > i7)
				{
					i7 = i12;
				}

				if (i13 > i7)
				{
					i7 = i13;
				}
			}

			return i7;
		}

		private int computeBlockLightValue(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			int i7 = Block.lightValue[i5];
			int i8 = this.getSavedLightValue(EnumSkyBlock.Block, i2 - 1, i3, i4) - i6;
			int i9 = this.getSavedLightValue(EnumSkyBlock.Block, i2 + 1, i3, i4) - i6;
			int i10 = this.getSavedLightValue(EnumSkyBlock.Block, i2, i3 - 1, i4) - i6;
			int i11 = this.getSavedLightValue(EnumSkyBlock.Block, i2, i3 + 1, i4) - i6;
			int i12 = this.getSavedLightValue(EnumSkyBlock.Block, i2, i3, i4 - 1) - i6;
			int i13 = this.getSavedLightValue(EnumSkyBlock.Block, i2, i3, i4 + 1) - i6;
			if (i8 > i7)
			{
				i7 = i8;
			}

			if (i9 > i7)
			{
				i7 = i9;
			}

			if (i10 > i7)
			{
				i7 = i10;
			}

			if (i11 > i7)
			{
				i7 = i11;
			}

			if (i12 > i7)
			{
				i7 = i12;
			}

			if (i13 > i7)
			{
				i7 = i13;
			}

			return i7;
		}

		public virtual void updateLightByType(EnumSkyBlock enumSkyBlock1, int i2, int i3, int i4)
		{
			if (this.doChunksNearChunkExist(i2, i3, i4, 17))
			{
				int i5 = 0;
				int i6 = 0;
				Profiler.startSection("getBrightness");
				int i7 = this.getSavedLightValue(enumSkyBlock1, i2, i3, i4);
				bool z8 = false;
				int i10 = this.getBlockId(i2, i3, i4);
				int i11 = this.func_48462_d(i2, i3, i4);
				if (i11 == 0)
				{
					i11 = 1;
				}

				bool z12 = false;
				int i25;
				if (enumSkyBlock1 == EnumSkyBlock.Sky)
				{
					i25 = this.computeSkyLightValue(i7, i2, i3, i4, i10, i11);
				}
				else
				{
					i25 = this.computeBlockLightValue(i7, i2, i3, i4, i10, i11);
				}

				int i9;
				int i13;
				int i14;
				int i15;
				int i16;
				int i17;
				if (i25 > i7)
				{
					this.lightUpdateBlockList[i6++] = 133152;
				}
				else if (i25 < i7)
				{
					if (enumSkyBlock1 != EnumSkyBlock.Block)
					{
						;
					}

					this.lightUpdateBlockList[i6++] = 133152 + (i7 << 18);

					while (true)
					{
						do
						{
							do
							{
								do
								{
									if (i5 >= i6)
									{
										i5 = 0;
										goto label133Break;
									}

									i9 = this.lightUpdateBlockList[i5++];
									i10 = (i9 & 63) - 32 + i2;
									i11 = (i9 >> 6 & 63) - 32 + i3;
									i25 = (i9 >> 12 & 63) - 32 + i4;
									i13 = i9 >> 18 & 15;
									i14 = this.getSavedLightValue(enumSkyBlock1, i10, i11, i25);
								} while (i14 != i13);

								this.setLightValue(enumSkyBlock1, i10, i11, i25, 0);
							} while (i13 <= 0);

							i15 = i10 - i2;
							i16 = i11 - i3;
							i17 = i25 - i4;
							if (i15 < 0)
							{
								i15 = -i15;
							}

							if (i16 < 0)
							{
								i16 = -i16;
							}

							if (i17 < 0)
							{
								i17 = -i17;
							}
						} while (i15 + i16 + i17 >= 17);

						for (int i18 = 0; i18 < 6; ++i18)
						{
							int i19 = i18 % 2 * 2 - 1;
							int i20 = i10 + i18 / 2 % 3 / 2 * i19;
							int i21 = i11 + (i18 / 2 + 1) % 3 / 2 * i19;
							int i22 = i25 + (i18 / 2 + 2) % 3 / 2 * i19;
							i14 = this.getSavedLightValue(enumSkyBlock1, i20, i21, i22);
							int i23 = Block.lightOpacity[this.getBlockId(i20, i21, i22)];
							if (i23 == 0)
							{
								i23 = 1;
							}

							if (i14 == i13 - i23 && i6 < this.lightUpdateBlockList.Length)
							{
								this.lightUpdateBlockList[i6++] = i20 - i2 + 32 + (i21 - i3 + 32 << 6) + (i22 - i4 + 32 << 12) + (i13 - i23 << 18);
							}
						}
						label133Continue:;
					}
					label133Break:;
				}

				Profiler.endSection();
				Profiler.startSection("tcp < tcc");

				while (i5 < i6)
				{
					i7 = this.lightUpdateBlockList[i5++];
					int i24 = (i7 & 63) - 32 + i2;
					i9 = (i7 >> 6 & 63) - 32 + i3;
					i10 = (i7 >> 12 & 63) - 32 + i4;
					i11 = this.getSavedLightValue(enumSkyBlock1, i24, i9, i10);
					i25 = this.getBlockId(i24, i9, i10);
					i13 = Block.lightOpacity[i25];
					if (i13 == 0)
					{
						i13 = 1;
					}

					bool z26 = false;
					if (enumSkyBlock1 == EnumSkyBlock.Sky)
					{
						i14 = this.computeSkyLightValue(i11, i24, i9, i10, i25, i13);
					}
					else
					{
						i14 = this.computeBlockLightValue(i11, i24, i9, i10, i25, i13);
					}

					if (i14 != i11)
					{
						this.setLightValue(enumSkyBlock1, i24, i9, i10, i14);
						if (i14 > i11)
						{
							i15 = i24 - i2;
							i16 = i9 - i3;
							i17 = i10 - i4;
							if (i15 < 0)
							{
								i15 = -i15;
							}

							if (i16 < 0)
							{
								i16 = -i16;
							}

							if (i17 < 0)
							{
								i17 = -i17;
							}

							if (i15 + i16 + i17 < 17 && i6 < this.lightUpdateBlockList.Length - 6)
							{
								if (this.getSavedLightValue(enumSkyBlock1, i24 - 1, i9, i10) < i14)
								{
									this.lightUpdateBlockList[i6++] = i24 - 1 - i2 + 32 + (i9 - i3 + 32 << 6) + (i10 - i4 + 32 << 12);
								}

								if (this.getSavedLightValue(enumSkyBlock1, i24 + 1, i9, i10) < i14)
								{
									this.lightUpdateBlockList[i6++] = i24 + 1 - i2 + 32 + (i9 - i3 + 32 << 6) + (i10 - i4 + 32 << 12);
								}

								if (this.getSavedLightValue(enumSkyBlock1, i24, i9 - 1, i10) < i14)
								{
									this.lightUpdateBlockList[i6++] = i24 - i2 + 32 + (i9 - 1 - i3 + 32 << 6) + (i10 - i4 + 32 << 12);
								}

								if (this.getSavedLightValue(enumSkyBlock1, i24, i9 + 1, i10) < i14)
								{
									this.lightUpdateBlockList[i6++] = i24 - i2 + 32 + (i9 + 1 - i3 + 32 << 6) + (i10 - i4 + 32 << 12);
								}

								if (this.getSavedLightValue(enumSkyBlock1, i24, i9, i10 - 1) < i14)
								{
									this.lightUpdateBlockList[i6++] = i24 - i2 + 32 + (i9 - i3 + 32 << 6) + (i10 - 1 - i4 + 32 << 12);
								}

								if (this.getSavedLightValue(enumSkyBlock1, i24, i9, i10 + 1) < i14)
								{
									this.lightUpdateBlockList[i6++] = i24 - i2 + 32 + (i9 - i3 + 32 << 6) + (i10 + 1 - i4 + 32 << 12);
								}
							}
						}
					}
				}

				Profiler.endSection();
			}
		}

		public virtual bool tickUpdates(bool z1)
		{
			int i2 = scheduledTickTreeSet.Count;
			if (i2 != scheduledTickSet.Count)
			{
				throw new System.InvalidOperationException("TickNextTick list out of synch");
			}
			else
			{
				if (i2 > 1000)
				{
					i2 = 1000;
				}

				for (int i3 = 0; i3 < i2; ++i3)
				{
                    if (scheduledTickTreeSet.Count > 0)
                    {
						NextTickListEntry nextTickListEntry4 = (NextTickListEntry)this.scheduledTickTreeSet[0];
						if (!z1 && nextTickListEntry4.scheduledTime > this.worldInfo.WorldTime)
						{
							break;
						}

						scheduledTickTreeSet.Remove(nextTickListEntry4);
						scheduledTickSet.Remove(nextTickListEntry4);
						sbyte b5 = 8;
						if (this.checkChunksExist(nextTickListEntry4.xCoord - b5, nextTickListEntry4.yCoord - b5, nextTickListEntry4.zCoord - b5, nextTickListEntry4.xCoord + b5, nextTickListEntry4.yCoord + b5, nextTickListEntry4.zCoord + b5))
						{
							int i6 = this.getBlockId(nextTickListEntry4.xCoord, nextTickListEntry4.yCoord, nextTickListEntry4.zCoord);
							if (i6 == nextTickListEntry4.blockID && i6 > 0)
							{
								Block.blocksList[i6].updateTick(this, nextTickListEntry4.xCoord, nextTickListEntry4.yCoord, nextTickListEntry4.zCoord, this.rand);
							}
						}
					}
				}

				return scheduledTickTreeSet.Count != 0;
			}
		}

		public virtual System.Collections.IList getPendingBlockUpdates(Chunk chunk1, bool z2)
		{
			ArrayList arrayList3 = null;
			ChunkCoordIntPair chunkCoordIntPair4 = chunk1.ChunkCoordIntPair;
			int i5 = chunkCoordIntPair4.chunkXPos << 4;
			int i6 = i5 + 16;
			int i7 = chunkCoordIntPair4.chunkZPos << 4;
			int i8 = i7 + 16;
            
			for (int i = scheduledTickSet.Count - 1; i >= 0; i--)
            {
                NextTickListEntry nextTickListEntry = (NextTickListEntry)scheduledTickSet[i];

				if (nextTickListEntry.xCoord >= i5 && nextTickListEntry.xCoord < i6 && nextTickListEntry.zCoord >= i7 && nextTickListEntry.zCoord < i8)
				{
					if (z2)
					{
						this.scheduledTickTreeSet.Remove(nextTickListEntry);
						scheduledTickSet.Remove(nextTickListEntry);
					}

					if (arrayList3 == null)
					{
						arrayList3 = new ArrayList();
					}

					arrayList3.Add(nextTickListEntry);
				}
			}

			return arrayList3;
		}

		public virtual void randomDisplayUpdates(int i1, int i2, int i3)
		{
			sbyte b4 = 16;
			RandomExtended random5 = new RandomExtended();

			for (int i6 = 0; i6 < 1000; ++i6)
			{
				int i7 = i1 + this.rand.Next(b4) - this.rand.Next(b4);
				int i8 = i2 + this.rand.Next(b4) - this.rand.Next(b4);
				int i9 = i3 + this.rand.Next(b4) - this.rand.Next(b4);
				int i10 = this.getBlockId(i7, i8, i9);
				if (i10 == 0 && this.rand.Next(8) > i8 && this.worldProvider.WorldHasNoSky)
				{
					this.spawnParticle("depthsuspend", (double)((float)i7 + this.rand.NextSingle()), (double)((float)i8 + this.rand.NextSingle()), (double)((float)i9 + this.rand.NextSingle()), 0.0D, 0.0D, 0.0D);
				}
				else if (i10 > 0)
				{
					Block.blocksList[i10].randomDisplayTick(this, i7, i8, i9, random5);
				}
			}

		}

		public virtual System.Collections.IList getEntitiesWithinAABBExcludingEntity(Entity entity1, AxisAlignedBB axisAlignedBB2)
		{
			this.entitiesWithinAABBExcludingEntity.Clear();
			int i3 = MathHelper.floor_double((axisAlignedBB2.minX - 2.0D) / 16.0D);
			int i4 = MathHelper.floor_double((axisAlignedBB2.maxX + 2.0D) / 16.0D);
			int i5 = MathHelper.floor_double((axisAlignedBB2.minZ - 2.0D) / 16.0D);
			int i6 = MathHelper.floor_double((axisAlignedBB2.maxZ + 2.0D) / 16.0D);

			for (int i7 = i3; i7 <= i4; ++i7)
			{
				for (int i8 = i5; i8 <= i6; ++i8)
				{
					if (this.chunkExists(i7, i8))
					{
						this.getChunkFromChunkCoords(i7, i8).getEntitiesWithinAABBForEntity(entity1, axisAlignedBB2, this.entitiesWithinAABBExcludingEntity);
					}
				}
			}

			return this.entitiesWithinAABBExcludingEntity;
		}

		public virtual System.Collections.IList getEntitiesWithinAABB(Type class1, AxisAlignedBB axisAlignedBB2)
		{
			int i3 = MathHelper.floor_double((axisAlignedBB2.minX - 2.0D) / 16.0D);
			int i4 = MathHelper.floor_double((axisAlignedBB2.maxX + 2.0D) / 16.0D);
			int i5 = MathHelper.floor_double((axisAlignedBB2.minZ - 2.0D) / 16.0D);
			int i6 = MathHelper.floor_double((axisAlignedBB2.maxZ + 2.0D) / 16.0D);
			ArrayList arrayList7 = new ArrayList();

			for (int i8 = i3; i8 <= i4; ++i8)
			{
				for (int i9 = i5; i9 <= i6; ++i9)
				{
					if (this.chunkExists(i8, i9))
					{
						this.getChunkFromChunkCoords(i8, i9).getEntitiesOfTypeWithinAAAB(class1, axisAlignedBB2, arrayList7);
					}
				}
			}

			return arrayList7;
		}

		public virtual Entity findNearestEntityWithinAABB(Type class1, AxisAlignedBB axisAlignedBB2, Entity entity3)
		{
			System.Collections.IList list4 = this.getEntitiesWithinAABB(class1, axisAlignedBB2);
			Entity entity5 = null;
			double d6 = double.MaxValue;
			System.Collections.IEnumerator iterator8 = list4.GetEnumerator();

			while (iterator8.MoveNext())
			{
				Entity entity9 = (Entity)iterator8.Current;
				if (entity9 != entity3)
				{
					double d10 = entity3.getDistanceSqToEntity(entity9);
					if (d10 <= d6)
					{
						entity5 = entity9;
						d6 = d10;
					}
				}
			}

			return entity5;
		}

		public virtual System.Collections.IList LoadedEntityList
		{
			get
			{
				return this.loadedEntityList;
			}
		}

		public virtual void updateTileEntityChunkAndDoNothing(int i1, int i2, int i3, TileEntity tileEntity4)
		{
			if (this.blockExists(i1, i2, i3))
			{
				this.getChunkFromBlockCoords(i1, i3).setChunkModified();
			}

			for (int i5 = 0; i5 < this.worldAccesses.Count; ++i5)
			{
				((IWorldAccess)this.worldAccesses[i5]).doNothingWithTileEntity(i1, i2, i3, tileEntity4);
			}

		}

		public virtual int countEntities(Type class1)
		{
			int i2 = 0;

			for (int i3 = 0; i3 < this.loadedEntityList.Count; ++i3)
			{
				Entity entity4 = (Entity)this.loadedEntityList[i3];
				if (class1.IsAssignableFrom(entity4.GetType()))
				{
					++i2;
				}
			}

			return i2;
		}

		public virtual void addLoadedEntities(System.Collections.IList list1)
		{
			this.loadedEntityList.AddRange(list1);

			for (int i2 = 0; i2 < list1.Count; ++i2)
			{
				this.obtainEntitySkin((Entity)list1[i2]);
			}

		}

		public virtual void unloadEntities(System.Collections.IList list1)
		{
			this.unloadedEntityList.AddRange(list1);
		}

		public virtual void dropOldChunks()
		{
			while (this.chunkProvider.unload100OldestChunks())
			{
			}

		}

		public virtual bool canBlockBePlacedAt(int i1, int i2, int i3, int i4, bool z5, int i6)
		{
			int i7 = this.getBlockId(i2, i3, i4);
			Block block8 = Block.blocksList[i7];
			Block block9 = Block.blocksList[i1];
			AxisAlignedBB axisAlignedBB10 = block9.getCollisionBoundingBoxFromPool(this, i2, i3, i4);
			if (z5)
			{
				axisAlignedBB10 = null;
			}

			if (axisAlignedBB10 != null && !this.checkIfAABBIsClear(axisAlignedBB10))
			{
				return false;
			}
			else
			{
				if (block8 != null && (block8 == Block.waterMoving || block8 == Block.waterStill || block8 == Block.lavaMoving || block8 == Block.lavaStill || block8 == Block.fire || block8.blockMaterial.GroundCover))
				{
					block8 = null;
				}

				return i1 > 0 && block8 == null && block9.canPlaceBlockOnSide(this, i2, i3, i4, i6);
			}
		}

		public virtual PathEntity getPathEntityToEntity(Entity entity1, Entity entity2, float f3, bool z4, bool z5, bool z6, bool z7)
		{
			Profiler.startSection("pathfind");
			int i8 = MathHelper.floor_double(entity1.posX);
			int i9 = MathHelper.floor_double(entity1.posY + 1.0D);
			int i10 = MathHelper.floor_double(entity1.posZ);
			int i11 = (int)(f3 + 16.0F);
			int i12 = i8 - i11;
			int i13 = i9 - i11;
			int i14 = i10 - i11;
			int i15 = i8 + i11;
			int i16 = i9 + i11;
			int i17 = i10 + i11;
			ChunkCache chunkCache18 = new ChunkCache(this, i12, i13, i14, i15, i16, i17);
			PathEntity pathEntity19 = (new PathFinder(chunkCache18, z4, z5, z6, z7)).createEntityPathTo(entity1, entity2, f3);
			Profiler.endSection();
			return pathEntity19;
		}

		public virtual PathEntity getEntityPathToXYZ(Entity entity1, int i2, int i3, int i4, float f5, bool z6, bool z7, bool z8, bool z9)
		{
			Profiler.startSection("pathfind");
			int i10 = MathHelper.floor_double(entity1.posX);
			int i11 = MathHelper.floor_double(entity1.posY);
			int i12 = MathHelper.floor_double(entity1.posZ);
			int i13 = (int)(f5 + 8.0F);
			int i14 = i10 - i13;
			int i15 = i11 - i13;
			int i16 = i12 - i13;
			int i17 = i10 + i13;
			int i18 = i11 + i13;
			int i19 = i12 + i13;
			ChunkCache chunkCache20 = new ChunkCache(this, i14, i15, i16, i17, i18, i19);
			PathEntity pathEntity21 = (new PathFinder(chunkCache20, z6, z7, z8, z9)).createEntityPathTo(entity1, i2, i3, i4, f5);
			Profiler.endSection();
			return pathEntity21;
		}

		public virtual bool isBlockProvidingPowerTo(int i1, int i2, int i3, int i4)
		{
			int i5 = this.getBlockId(i1, i2, i3);
			return i5 == 0 ? false : Block.blocksList[i5].isIndirectlyPoweringTo(this, i1, i2, i3, i4);
		}

		public virtual bool isBlockGettingPowered(int i1, int i2, int i3)
		{
			return this.isBlockProvidingPowerTo(i1, i2 - 1, i3, 0) ? true : (this.isBlockProvidingPowerTo(i1, i2 + 1, i3, 1) ? true : (this.isBlockProvidingPowerTo(i1, i2, i3 - 1, 2) ? true : (this.isBlockProvidingPowerTo(i1, i2, i3 + 1, 3) ? true : (this.isBlockProvidingPowerTo(i1 - 1, i2, i3, 4) ? true : this.isBlockProvidingPowerTo(i1 + 1, i2, i3, 5)))));
		}

		public virtual bool isBlockIndirectlyProvidingPowerTo(int i1, int i2, int i3, int i4)
		{
			if (this.isBlockNormalCube(i1, i2, i3))
			{
				return this.isBlockGettingPowered(i1, i2, i3);
			}
			else
			{
				int i5 = this.getBlockId(i1, i2, i3);
				return i5 == 0 ? false : Block.blocksList[i5].isPoweringTo(this, i1, i2, i3, i4);
			}
		}

		public virtual bool isBlockIndirectlyGettingPowered(int i1, int i2, int i3)
		{
			return this.isBlockIndirectlyProvidingPowerTo(i1, i2 - 1, i3, 0) ? true : (this.isBlockIndirectlyProvidingPowerTo(i1, i2 + 1, i3, 1) ? true : (this.isBlockIndirectlyProvidingPowerTo(i1, i2, i3 - 1, 2) ? true : (this.isBlockIndirectlyProvidingPowerTo(i1, i2, i3 + 1, 3) ? true : (this.isBlockIndirectlyProvidingPowerTo(i1 - 1, i2, i3, 4) ? true : this.isBlockIndirectlyProvidingPowerTo(i1 + 1, i2, i3, 5)))));
		}

		public virtual EntityPlayer getClosestPlayerToEntity(Entity entity1, double d2)
		{
			return this.getClosestPlayer(entity1.posX, entity1.posY, entity1.posZ, d2);
		}

		public virtual EntityPlayer getClosestPlayer(double d1, double d3, double d5, double d7)
		{
			double d9 = -1.0D;
			EntityPlayer entityPlayer11 = null;

			for (int i12 = 0; i12 < this.playerEntities.Count; ++i12)
			{
				EntityPlayer entityPlayer13 = (EntityPlayer)this.playerEntities[i12];
				double d14 = entityPlayer13.getDistanceSq(d1, d3, d5);
				if ((d7 < 0.0D || d14 < d7 * d7) && (d9 == -1.0D || d14 < d9))
				{
					d9 = d14;
					entityPlayer11 = entityPlayer13;
				}
			}

			return entityPlayer11;
		}

		public virtual EntityPlayer func_48456_a(double d1, double d3, double d5)
		{
			double d7 = -1.0D;
			EntityPlayer entityPlayer9 = null;

			for (int i10 = 0; i10 < this.playerEntities.Count; ++i10)
			{
				EntityPlayer entityPlayer11 = (EntityPlayer)this.playerEntities[i10];
				double d12 = entityPlayer11.getDistanceSq(d1, entityPlayer11.posY, d3);
				if ((d5 < 0.0D || d12 < d5 * d5) && (d7 == -1.0D || d12 < d7))
				{
					d7 = d12;
					entityPlayer9 = entityPlayer11;
				}
			}

			return entityPlayer9;
		}

		public virtual EntityPlayer getClosestVulnerablePlayerToEntity(Entity entity1, double d2)
		{
			return this.getClosestVulnerablePlayer(entity1.posX, entity1.posY, entity1.posZ, d2);
		}

		public virtual EntityPlayer getClosestVulnerablePlayer(double d1, double d3, double d5, double d7)
		{
			double d9 = -1.0D;
			EntityPlayer entityPlayer11 = null;

			for (int i12 = 0; i12 < this.playerEntities.Count; ++i12)
			{
				EntityPlayer entityPlayer13 = (EntityPlayer)this.playerEntities[i12];
				if (!entityPlayer13.capabilities.disableDamage)
				{
					double d14 = entityPlayer13.getDistanceSq(d1, d3, d5);
					if ((d7 < 0.0D || d14 < d7 * d7) && (d9 == -1.0D || d14 < d9))
					{
						d9 = d14;
						entityPlayer11 = entityPlayer13;
					}
				}
			}

			return entityPlayer11;
		}

		public virtual EntityPlayer getPlayerEntityByName(string string1)
		{
			for (int i2 = 0; i2 < this.playerEntities.Count; ++i2)
			{
				if (string1.Equals(((EntityPlayer)this.playerEntities[i2]).username))
				{
					return (EntityPlayer)this.playerEntities[i2];
				}
			}

			return null;
		}

		public virtual void sendQuittingDisconnectingPacket()
		{
		}

		public virtual void checkSessionLock()
		{
			this.saveHandler.checkSessionLock();
		}

		public virtual long WorldTime
		{
			set
			{
				this.worldInfo.WorldTime = value;
			}
			get
			{
				return this.worldInfo.WorldTime;
			}
		}

		public virtual long Seed
		{
			get
			{
				return this.worldInfo.Seed;
			}
		}


		public virtual ChunkCoordinates SpawnPoint
		{
			get
			{
				return new ChunkCoordinates(this.worldInfo.SpawnX, this.worldInfo.SpawnY, this.worldInfo.SpawnZ);
			}
			set
			{
				this.worldInfo.setSpawnPosition(value.posX, value.posY, value.posZ);
			}
		}


		public virtual void joinEntityInSurroundings(Entity entity1)
		{
			int i2 = MathHelper.floor_double(entity1.posX / 16.0D);
			int i3 = MathHelper.floor_double(entity1.posZ / 16.0D);
			sbyte b4 = 2;

			for (int i5 = i2 - b4; i5 <= i2 + b4; ++i5)
			{
				for (int i6 = i3 - b4; i6 <= i3 + b4; ++i6)
				{
					this.getChunkFromChunkCoords(i5, i6);
				}
			}

			if (!this.loadedEntityList.Contains(entity1))
			{
				this.loadedEntityList.Add(entity1);
			}

		}

		public virtual bool canMineBlock(EntityPlayer entityPlayer1, int i2, int i3, int i4)
		{
			return true;
		}

		public virtual void setEntityState(Entity entity1, sbyte b2)
		{
		}

		public virtual void updateEntityList()
		{
			this.loadedEntityList.RemoveAll(this.unloadedEntityList);

			int i1;
			Entity entity2;
			int i3;
			int i4;
			for (i1 = 0; i1 < this.unloadedEntityList.Count; ++i1)
			{
				entity2 = (Entity)this.unloadedEntityList[i1];
				i3 = entity2.chunkCoordX;
				i4 = entity2.chunkCoordZ;
				if (entity2.addedToChunk && this.chunkExists(i3, i4))
				{
					this.getChunkFromChunkCoords(i3, i4).removeEntity(entity2);
				}
			}

			for (i1 = 0; i1 < this.unloadedEntityList.Count; ++i1)
			{
				this.releaseEntitySkin((Entity)this.unloadedEntityList[i1]);
			}

			this.unloadedEntityList.Clear();

			for (i1 = 0; i1 < this.loadedEntityList.Count; ++i1)
			{
				entity2 = (Entity)this.loadedEntityList[i1];
				if (entity2.ridingEntity != null)
				{
					if (!entity2.ridingEntity.isDead && entity2.ridingEntity.riddenByEntity == entity2)
					{
						continue;
					}

					entity2.ridingEntity.riddenByEntity = null;
					entity2.ridingEntity = null;
				}

				if (entity2.isDead)
				{
					i3 = entity2.chunkCoordX;
					i4 = entity2.chunkCoordZ;
					if (entity2.addedToChunk && this.chunkExists(i3, i4))
					{
						this.getChunkFromChunkCoords(i3, i4).removeEntity(entity2);
					}

					this.loadedEntityList.RemoveAt(i1--);
					this.releaseEntitySkin(entity2);
				}
			}

		}

		public virtual IChunkProvider ChunkProvider
		{
			get
			{
				return this.chunkProvider;
			}
		}

		public virtual void playNoteAt(int i1, int i2, int i3, int i4, int i5)
		{
			int i6 = this.getBlockId(i1, i2, i3);
			if (i6 > 0)
			{
				Block.blocksList[i6].powerBlock(this, i1, i2, i3, i4, i5);
			}

		}

		public virtual ISaveHandler SaveHandler
		{
			get
			{
				return this.saveHandler;
			}
		}

		public virtual WorldInfo WorldInfo
		{
			get
			{
				return this.worldInfo;
			}
		}

		public virtual void updateAllPlayersSleepingFlag()
		{
			this.allPlayersSleeping = this.playerEntities.Count > 0;
			System.Collections.IEnumerator iterator1 = this.playerEntities.GetEnumerator();

			while (iterator1.MoveNext())
			{
				EntityPlayer entityPlayer2 = (EntityPlayer)iterator1.Current;
				if (!entityPlayer2.PlayerSleeping)
				{
					this.allPlayersSleeping = false;
					break;
				}
			}

		}

		protected internal virtual void wakeUpAllPlayers()
		{
			this.allPlayersSleeping = false;
			System.Collections.IEnumerator iterator1 = this.playerEntities.GetEnumerator();

			while (iterator1.MoveNext())
			{
				EntityPlayer entityPlayer2 = (EntityPlayer)iterator1.Current;
				if (entityPlayer2.PlayerSleeping)
				{
					entityPlayer2.wakeUpPlayer(false, false, true);
				}
			}

			this.clearWeather();
		}

		public virtual bool AllPlayersFullyAsleep
		{
			get
			{
				if (this.allPlayersSleeping && !this.isRemote)
				{
					System.Collections.IEnumerator iterator1 = this.playerEntities.GetEnumerator();
    
					EntityPlayer entityPlayer2;
					do
					{
						if (!iterator1.MoveNext())
						{
							return true;
						}
    
						entityPlayer2 = (EntityPlayer)iterator1.Current;
					} while (entityPlayer2.PlayerFullyAsleep);
    
					return false;
				}
				else
				{
					return false;
				}
			}
		}

		public virtual float getWeightedThunderStrength(float f1)
		{
			return (this.prevThunderingStrength + (this.thunderingStrength - this.prevThunderingStrength) * f1) * this.getRainStrength(f1);
		}

		public virtual float getRainStrength(float f1)
		{
			return this.prevRainingStrength + (this.rainingStrength - this.prevRainingStrength) * f1;
		}

		public virtual float RainStrength
		{
			set
			{
				this.prevRainingStrength = value;
				this.rainingStrength = value;
			}
		}

		public virtual bool Thundering
		{
			get
			{
				return (double)this.getWeightedThunderStrength(1.0F) > 0.9D;
			}
		}

		public virtual bool Raining
		{
			get
			{
				return (double)this.getRainStrength(1.0F) > 0.2D;
			}
		}

		public virtual bool canLightningStrikeAt(int i1, int i2, int i3)
		{
			if (!this.Raining)
			{
				return false;
			}
			else if (!this.canBlockSeeTheSky(i1, i2, i3))
			{
				return false;
			}
			else if (this.getPrecipitationHeight(i1, i3) > i2)
			{
				return false;
			}
			else
			{
				BiomeGenBase biomeGenBase4 = this.getBiomeGenForCoords(i1, i3);
				return biomeGenBase4.EnableSnow ? false : biomeGenBase4.canSpawnLightningBolt();
			}
		}

		public virtual bool isBlockHighHumidity(int i1, int i2, int i3)
		{
			BiomeGenBase biomeGenBase4 = this.getBiomeGenForCoords(i1, i3);
			return biomeGenBase4.HighHumidity;
		}

		public virtual void setItemData(string string1, WorldSavedData worldSavedData2)
		{
			this.mapStorage.setData(string1, worldSavedData2);
		}

		public virtual WorldSavedData loadItemData(Type class1, string string2)
		{
			return this.mapStorage.loadData(class1, string2);
		}

		public virtual int getUniqueDataId(string string1)
		{
			return this.mapStorage.getUniqueDataId(string1);
		}

		public virtual void playAuxSFX(int i1, int i2, int i3, int i4, int i5)
		{
			this.playAuxSFXAtEntity((EntityPlayer)null, i1, i2, i3, i4, i5);
		}

		public virtual void playAuxSFXAtEntity(EntityPlayer entityPlayer1, int i2, int i3, int i4, int i5, int i6)
		{
			for (int i7 = 0; i7 < this.worldAccesses.Count; ++i7)
			{
				((IWorldAccess)this.worldAccesses[i7]).playAuxSFX(entityPlayer1, i2, i3, i4, i5, i6);
			}

		}

		public virtual int Height
		{
			get
			{
				return 256;
			}
		}

		public virtual RandomExtended setRandomSeed(int i1, int i2, int i3)
		{
			long j4 = (long)i1 * 341873128712L + (long)i2 * 132897987541L + this.WorldInfo.Seed + (long)i3;
			rand.SetSeed(j4);
			return this.rand;
		}

		public virtual bool updatingLighting()
		{
			return false;
		}

		public virtual SpawnListEntry getRandomMob(EnumCreatureType enumCreatureType1, int i2, int i3, int i4)
		{
			System.Collections.IList list5 = this.ChunkProvider.getPossibleCreatures(enumCreatureType1, i2, i3, i4);
			return list5 != null && list5.Count > 0 ? (SpawnListEntry)WeightedRandom.getRandomItem(this.rand, (System.Collections.ICollection)list5) : null;
		}

		public virtual ChunkPosition? findClosestStructure(string string1, int i2, int i3, int i4)
		{
			return this.ChunkProvider.findClosestStructure(this, string1, i2, i3, i4);
		}

		public virtual bool getChunksEmpty_IDK()
		{
			return false;
		}

		public virtual double SeaLevel
		{
			get
			{
				return this.worldInfo.TerrainType == WorldType.FLAT ? 0.0D : 63.0D;
			}
		}
	}

}