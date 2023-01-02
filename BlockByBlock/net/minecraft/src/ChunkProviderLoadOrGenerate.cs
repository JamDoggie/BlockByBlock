using System;

namespace net.minecraft.src
{

	public class ChunkProviderLoadOrGenerate : IChunkProvider
	{
		private Chunk blankChunk;
		private IChunkProvider chunkProvider;
		private IChunkLoader chunkLoader;
		private Chunk[] chunks;
		private World worldObj;
		internal int lastQueriedChunkXPos;
		internal int lastQueriedChunkZPos;
		private Chunk lastQueriedChunk;
		private int curChunkX;
		private int curChunkY;

		public virtual void setCurrentChunkOver(int i1, int i2)
		{
			this.curChunkX = i1;
			this.curChunkY = i2;
		}

		public virtual bool canChunkExist(int i1, int i2)
		{
			sbyte b3 = 15;
			return i1 >= this.curChunkX - b3 && i2 >= this.curChunkY - b3 && i1 <= this.curChunkX + b3 && i2 <= this.curChunkY + b3;
		}

		public virtual bool chunkExists(int i1, int i2)
		{
			if (!this.canChunkExist(i1, i2))
			{
				return false;
			}
			else if (i1 == this.lastQueriedChunkXPos && i2 == this.lastQueriedChunkZPos && this.lastQueriedChunk != null)
			{
				return true;
			}
			else
			{
				int i3 = i1 & 31;
				int i4 = i2 & 31;
				int i5 = i3 + i4 * 32;
				return this.chunks[i5] != null && (this.chunks[i5] == this.blankChunk || this.chunks[i5].isAtLocation(i1, i2));
			}
		}

		public virtual Chunk loadChunk(int i1, int i2)
		{
			return this.provideChunk(i1, i2);
		}

		public virtual Chunk provideChunk(int i1, int i2)
		{
            if (i1 == this.lastQueriedChunkXPos && i2 == this.lastQueriedChunkZPos && this.lastQueriedChunk != null)
            {
                return this.lastQueriedChunk;
            }
            else if (!this.worldObj.findingSpawnPoint && !this.canChunkExist(i1, i2))
            {
                return this.blankChunk;
            }
            else
            {
                int i3 = i1 & 31;
                int i4 = i2 & 31;
                int i5 = i3 + i4 * 32;
                if (!this.chunkExists(i1, i2))
                {
                    if (this.chunks[i5] != null)
                    {
                        this.chunks[i5].onChunkUnload();
                        this.saveChunk(this.chunks[i5]);
                        this.saveExtraChunkData(this.chunks[i5]);
                    }
                    Chunk chunk6 = this.func_542_c(i1, i2);
                    if (chunk6 == null)
                    {
                        if (this.chunkProvider == null)
                        {
                            chunk6 = this.blankChunk;
                        }
                        else
                        {
                            chunk6 = this.chunkProvider.provideChunk(i1, i2);
                            chunk6.removeUnknownBlocks();
                        }
                    }

                    this.chunks[i5] = chunk6;
                    chunk6.func_4143_d();
                    if (this.chunks[i5] != null)
                    {
                        this.chunks[i5].onChunkLoad();
                    }
                    this.chunks[i5].populateChunk(this, this, i1, i2);
                }

                this.lastQueriedChunkXPos = i1;
                this.lastQueriedChunkZPos = i2;
                this.lastQueriedChunk = this.chunks[i5];
                return this.chunks[i5];
            }
		}

		private Chunk func_542_c(int i1, int i2)
		{
			if (this.chunkLoader == null)
			{
				return this.blankChunk;
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
					return this.blankChunk;
				}
			}
		}

		private void saveExtraChunkData(Chunk chunk1)
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

		private void saveChunk(Chunk chunk1)
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
			int i4 = 0;
			int i5;
			if (iProgressUpdate2 != null)
			{
				for (i5 = 0; i5 < this.chunks.Length; ++i5)
				{
					if (this.chunks[i5] != null && this.chunks[i5].needsSaving(z1))
					{
						++i4;
					}
				}
			}

			i5 = 0;

			for (int i6 = 0; i6 < this.chunks.Length; ++i6)
			{
				if (this.chunks[i6] != null)
				{
					if (z1)
					{
						this.saveExtraChunkData(this.chunks[i6]);
					}

					if (this.chunks[i6].needsSaving(z1))
					{
						this.saveChunk(this.chunks[i6]);
						this.chunks[i6].isModified = false;
						++i3;
						if (i3 == 2 && !z1)
						{
							return false;
						}

						if (iProgressUpdate2 != null)
						{
							++i5;
							if (i5 % 10 == 0)
							{
								iProgressUpdate2.LoadingProgress = i5 * 100 / i4;
							}
						}
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
			return "ChunkCache: " + this.chunks.Length;
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