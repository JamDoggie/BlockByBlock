using System;
using System.Collections;
using System.Collections.Generic;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class ChunkProvider : IChunkProvider
	{
		private ISet<object> droppedChunksSet = new HashSet<object>();
		private Chunk emptyChunk;
		private IChunkProvider chunkProvider;
		private IChunkLoader chunkLoader;
		private LongHashMap chunkMap = new LongHashMap();
		private System.Collections.IList chunkList = new ArrayList();
		private World worldObj;
		private int field_35392_h;

		public ChunkProvider(World world1, IChunkLoader iChunkLoader2, IChunkProvider iChunkProvider3)
		{
			this.emptyChunk = new EmptyChunk(world1, 0, 0);
			this.worldObj = world1;
			this.chunkLoader = iChunkLoader2;
			this.chunkProvider = iChunkProvider3;
		}

		public virtual bool chunkExists(int i1, int i2)
		{
			return this.chunkMap.containsItem(ChunkCoordIntPair.chunkXZ2Int(i1, i2));
		}

		public virtual void dropChunk(int i1, int i2)
		{
			ChunkCoordinates chunkCoordinates3 = this.worldObj.SpawnPoint;
			int i4 = i1 * 16 + 8 - chunkCoordinates3.posX;
			int i5 = i2 * 16 + 8 - chunkCoordinates3.posZ;
			short s6 = 128;
			if (i4 < -s6 || i4 > s6 || i5 < -s6 || i5 > s6)
			{
				this.droppedChunksSet.Add(ChunkCoordIntPair.chunkXZ2Int(i1, i2));
			}

		}

		public virtual Chunk loadChunk(int i1, int i2)
		{
			long j3 = ChunkCoordIntPair.chunkXZ2Int(i1, i2);
			droppedChunksSet.Remove(j3);
			Chunk chunk5 = (Chunk)this.chunkMap.getValueByKey(j3);
			if (chunk5 == null)
			{
				int i6 = 1875004;
				if (i1 < -i6 || i2 < -i6 || i1 >= i6 || i2 >= i6)
				{
					return this.emptyChunk;
				}

                Profiler.startSection("getChunk");
                chunk5 = this.loadChunkFromFile(i1, i2);
				if (chunk5 == null)
				{
					if (this.chunkProvider == null)
					{
						chunk5 = this.emptyChunk;
					}
					else
					{
						chunk5 = this.chunkProvider.provideChunk(i1, i2);
					}
				}
				Profiler.endSection();

				this.chunkMap.add(j3, chunk5);
				this.chunkList.Add(chunk5);
				if (chunk5 != null)
				{
					chunk5.func_4143_d();
					chunk5.onChunkLoad();
				}

				chunk5.populateChunk(this, this, i1, i2);
			}

			return chunk5;
		}

		public virtual Chunk provideChunk(int i1, int i2)
		{
			Chunk chunk3 = (Chunk)this.chunkMap.getValueByKey(ChunkCoordIntPair.chunkXZ2Int(i1, i2));
			return chunk3 == null ? this.loadChunk(i1, i2) : chunk3;
		}

		private Chunk loadChunkFromFile(int i1, int i2)
		{
			if (this.chunkLoader == null)
			{
				return null;
			}
			else
			{
				try
				{
					Chunk chunk3 = this.chunkLoader.loadChunk(this.worldObj, i1, i2);
					if (chunk3 != null)
					{
						chunk3.lastSaveTime = this.worldObj.WorldTime;
					}

					return chunk3;
				}
				catch (Exception exception4)
				{
					Console.WriteLine(exception4.ToString());
					Console.Write(exception4.StackTrace);
					return null;
				}
			}
		}

		private void saveChunkExtraData(Chunk chunk1)
		{
			if (this.chunkLoader != null)
			{
				try
				{
					this.chunkLoader.saveExtraChunkData(this.worldObj, chunk1);
				}
				catch (Exception exception3)
				{
					Console.WriteLine(exception3.ToString());
					Console.Write(exception3.StackTrace);
				}

			}
		}

		private void saveChunkData(Chunk chunk1)
		{
			if (this.chunkLoader != null)
			{
				try
				{
					chunk1.lastSaveTime = this.worldObj.WorldTime;
					this.chunkLoader.saveChunk(this.worldObj, chunk1);
				}
				catch (IOException iOException3)
				{
					Console.WriteLine(iOException3.ToString());
					Console.Write(iOException3.StackTrace);
				}

			}
		}

		public virtual void populate(IChunkProvider iChunkProvider1, int i2, int i3)
		{
			Chunk chunk4 = this.provideChunk(i2, i3);
			if (!chunk4.isTerrainPopulated)
			{
				chunk4.isTerrainPopulated = true;
				if (this.chunkProvider != null)
				{
					this.chunkProvider.populate(iChunkProvider1, i2, i3);
					chunk4.setChunkModified();
				}
			}

		}

		public virtual bool saveChunks(bool z1, IProgressUpdate iProgressUpdate2)
		{
			int i3 = 0;

			for (int i4 = 0; i4 < this.chunkList.Count; ++i4)
			{
				Chunk chunk5 = (Chunk)this.chunkList[i4];
				if (z1)
				{
					this.saveChunkExtraData(chunk5);
				}

				if (chunk5.needsSaving(z1))
				{
					this.saveChunkData(chunk5);
					chunk5.isModified = false;
					++i3;
					if (i3 == 24 && !z1)
					{
						return false;
					}
				}
			}

			if (z1)
			{
				if (this.chunkLoader == null)
				{
					return true;
				}

				this.chunkLoader.saveExtraData();
			}

			return true;
		}

		public virtual bool unload100OldestChunks()
		{
			int i;
			for (i = 0; i < 100; ++i)
			{
				if (this.droppedChunksSet.Count > 0)
				{
					bool hadNext = droppedChunksSet.GetEnumerator().MoveNext();
					long? long2 = null;

					if (hadNext)
                    {
						long2 = (long?)droppedChunksSet.GetEnumerator().Current;

						if (long2 != null)
                        {
							Chunk? chunk3 = (Chunk?)chunkMap.getValueByKey(long2.Value);

							if (chunk3 != null)
							{
								chunk3.onChunkUnload();
								this.saveChunkData(chunk3);
								this.saveChunkExtraData(chunk3);
								this.droppedChunksSet.Remove(long2);
								this.chunkMap.remove(long2.Value);
								this.chunkList.Remove(chunk3);
							}
						}
					}
				}
			}

			for (i = 0; i < 10; ++i)
			{
				if (this.field_35392_h >= this.chunkList.Count)
				{
					this.field_35392_h = 0;
					break;
				}

				Chunk chunk4 = (Chunk)this.chunkList[this.field_35392_h++];
				EntityPlayer entityPlayer5 = this.worldObj.func_48456_a((double)(chunk4.xPosition << 4) + 8.0D, (double)(chunk4.zPosition << 4) + 8.0D, 288.0D);
				if (entityPlayer5 == null)
				{
					this.dropChunk(chunk4.xPosition, chunk4.zPosition);
				}
			}

			if (this.chunkLoader != null)
			{
				this.chunkLoader.chunkTick();
			}

			return this.chunkProvider.unload100OldestChunks();
		}

		public virtual bool canSave()
		{
			return true;
		}

		public virtual string makeString()
		{
			return "ServerChunkCache: " + this.chunkMap.NumHashElements + " Drop: " + this.droppedChunksSet.Count;
		}

		public virtual System.Collections.IList getPossibleCreatures(EnumCreatureType enumCreatureType1, int i2, int i3, int i4)
		{
			return this.chunkProvider.getPossibleCreatures(enumCreatureType1, i2, i3, i4);
		}

		public virtual ChunkPosition? findClosestStructure(World world1, string string2, int i3, int i4, int i5)
		{
			return this.chunkProvider.findClosestStructure(world1, string2, i3, i4, i5);
		}
	}

}