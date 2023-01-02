using System.Collections;
using System.Collections.Generic;
using System.Linq;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class WorldClient : World
	{
		private IList blocksToReceive = new ArrayList();
		private NetClientHandler sendQueue;
		private ChunkProviderClient field_20915_C;
		private IntHashMap entityHashSet = new IntHashMap();
		private ISet<object> entityList = new HashSet<object>();
		private ISet<object> entitySpawnQueue = new HashSet<object>();

		public WorldClient(NetClientHandler netClientHandler1, WorldSettings worldSettings2, int i3, int i4) : base(new SaveHandlerMP(), "MpServer", (WorldProvider)WorldProvider.getProviderForDimension(i3), (WorldSettings)worldSettings2)
		{
			this.sendQueue = netClientHandler1;
			this.difficultySetting = i4;
			this.SpawnPoint = new ChunkCoordinates(8, 64, 8);
			this.mapStorage = netClientHandler1.mapStorage;
		}

		public override void tick()
		{
			this.WorldTime = this.WorldTime + 1L;

			int i1;
			for (i1 = 0; i1 < 10 && this.entitySpawnQueue.Count > 0; ++i1)
			{
				IEnumerator iterator = entitySpawnQueue.GetEnumerator();
				iterator.MoveNext();

				Entity entity2 = (Entity)iterator.Current;
				this.entitySpawnQueue.Remove(entity2);
				if (!this.loadedEntityList.Contains(entity2))
				{
					this.spawnEntityInWorld(entity2);
				}
			}

			this.sendQueue.processReadPackets();

			for (i1 = 0; i1 < this.blocksToReceive.Count; ++i1)
			{
				WorldBlockPositionType worldBlockPositionType3 = (WorldBlockPositionType)this.blocksToReceive[i1];
				if (--worldBlockPositionType3.acceptCountdown == 0)
				{
					base.setBlockAndMetadata(worldBlockPositionType3.posX, worldBlockPositionType3.posY, worldBlockPositionType3.posZ, worldBlockPositionType3.blockID, worldBlockPositionType3.metadata);
					base.markBlockNeedsUpdate(worldBlockPositionType3.posX, worldBlockPositionType3.posY, worldBlockPositionType3.posZ);
					this.blocksToReceive.RemoveAt(i1--);
				} // PORTING TODO: This logic is confusing to me. Maybe something's different because blocksToReceieve used to be a linked list? IDK, check back on this later.
			}

			this.field_20915_C.unload100OldestChunks();
			this.tickBlocksAndAmbiance();
		}

		public virtual void invalidateBlockReceiveRegion(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			for (int i7 = 0; i7 < this.blocksToReceive.Count; ++i7)
			{
				WorldBlockPositionType worldBlockPositionType8 = (WorldBlockPositionType)this.blocksToReceive[i7];
				if (worldBlockPositionType8.posX >= i1 && worldBlockPositionType8.posY >= i2 && worldBlockPositionType8.posZ >= i3 && worldBlockPositionType8.posX <= i4 && worldBlockPositionType8.posY <= i5 && worldBlockPositionType8.posZ <= i6)
				{
					this.blocksToReceive.RemoveAt(i7--);
				}
			}

		}

		protected internal override IChunkProvider createChunkProvider()
		{
			this.field_20915_C = new ChunkProviderClient(this);
			return this.field_20915_C;
		}

		public override void setSpawnLocation()
		{
			this.SpawnPoint = new ChunkCoordinates(8, 64, 8);
		}

		protected internal override void tickBlocksAndAmbiance()
		{
			this.func_48461_r();
			System.Collections.IEnumerator iterator1 = this.activeChunkSet.GetEnumerator();

			while (iterator1.MoveNext())
			{
				ChunkCoordIntPair chunkCoordIntPair2 = (ChunkCoordIntPair)iterator1.Current;
				int i3 = chunkCoordIntPair2.chunkXPos * 16;
				int i4 = chunkCoordIntPair2.chunkZPos * 16;
				Profiler.startSection("getChunk");
				Chunk chunk5 = this.getChunkFromChunkCoords(chunkCoordIntPair2.chunkXPos, chunkCoordIntPair2.chunkZPos);
				this.func_48458_a(i3, i4, chunk5);
				Profiler.endSection();
			}

		}

		public override void scheduleBlockUpdate(int i1, int i2, int i3, int i4, int i5)
		{
		}

		public override bool tickUpdates(bool z1)
		{
			return false;
		}

		public virtual void doPreChunk(int i1, int i2, bool z3)
		{
			if (z3)
			{
				this.field_20915_C.loadChunk(i1, i2);
			}
			else
			{
				this.field_20915_C.func_539_c(i1, i2);
			}

			if (!z3)
			{
				this.markBlocksDirty(i1 * 16, 0, i2 * 16, i1 * 16 + 15, 256, i2 * 16 + 15);
			}

		}

		public override bool spawnEntityInWorld(Entity entity1)
		{
			bool z2 = base.spawnEntityInWorld(entity1);
			this.entityList.Add(entity1);
			if (!z2)
			{
				this.entitySpawnQueue.Add(entity1);
			}

			return z2;
		}

		public override void setEntityDead(Entity entity1)
		{
			base.setEntityDead(entity1);
			this.entityList.Remove(entity1);
		}

		protected internal override void obtainEntitySkin(Entity entity1)
		{
			base.obtainEntitySkin(entity1);
			if (this.entitySpawnQueue.Contains(entity1))
			{
				this.entitySpawnQueue.Remove(entity1);
			}

		}

		protected internal override void releaseEntitySkin(Entity entity1)
		{
			base.releaseEntitySkin(entity1);
			if (this.entityList.Contains(entity1))
			{
				if (entity1.EntityAlive)
				{
					this.entitySpawnQueue.Add(entity1);
				}
				else
				{
					this.entityList.Remove(entity1);
				}
			}

		}

		public virtual void addEntityToWorld(int i1, Entity entity2)
		{
			Entity entity3 = this.getEntityByID(i1);
			if (entity3 != null)
			{
				setEntityDead(entity3);
			}

			this.entityList.Add(entity2);
			entity2.entityId = i1;
			if (!this.spawnEntityInWorld(entity2))
			{
				this.entitySpawnQueue.Add(entity2);
			}

			this.entityHashSet.addKey(i1, entity2);
		}

		public virtual Entity getEntityByID(int i1)
		{
			return (Entity)this.entityHashSet.lookup(i1);
		}

		public virtual Entity removeEntityFromWorld(int i1)
		{
			Entity entity2 = (Entity)this.entityHashSet.removeObject(i1);
			if (entity2 != null)
			{
				this.entityList.Remove(entity2);
				setEntityDead(entity2);
			}

			return entity2;
		}

		public virtual bool setBlockAndMetadataAndInvalidate(int i1, int i2, int i3, int i4, int i5)
		{
			this.invalidateBlockReceiveRegion(i1, i2, i3, i1, i2, i3);
			return base.setBlockAndMetadataWithNotify(i1, i2, i3, i4, i5);
		}

		public override void sendQuittingDisconnectingPacket()
		{
			this.sendQueue.quitWithPacket(new Packet255KickDisconnect("Quitting"));
		}

		protected internal override void updateWeather()
		{
			if (!this.worldProvider.hasNoSky)
			{
				if (this.lastLightningBolt > 0)
				{
					--this.lastLightningBolt;
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
	}

}