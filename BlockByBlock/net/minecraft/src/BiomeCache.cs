using System.Collections;

namespace net.minecraft.src
{

	public class BiomeCache
	{
		private readonly WorldChunkManager chunkManager;
		private long lastCleanupTime = 0L;
		private LongHashMap cacheMap = new LongHashMap();
		private System.Collections.IList cache = new ArrayList();

		public BiomeCache(WorldChunkManager worldChunkManager1)
		{
			this.chunkManager = worldChunkManager1;
		}

		public virtual BiomeCacheBlock getBiomeCacheBlock(int i1, int i2)
		{
			i1 >>= 4;
			i2 >>= 4;
			long j3 = (long)i1 & 4294967295L | ((long)i2 & 4294967295L) << 32;
			BiomeCacheBlock biomeCacheBlock5 = (BiomeCacheBlock)this.cacheMap.getValueByKey(j3);
			if (biomeCacheBlock5 == null)
			{
				biomeCacheBlock5 = new BiomeCacheBlock(this, i1, i2);
				this.cacheMap.add(j3, biomeCacheBlock5);
				this.cache.Add(biomeCacheBlock5);
			}

			biomeCacheBlock5.lastAccessTime = DateTimeHelper.CurrentUnixTimeMillis();
			return biomeCacheBlock5;
		}

		public virtual BiomeGenBase getBiomeGenAt(int i1, int i2)
		{
			return this.getBiomeCacheBlock(i1, i2).getBiomeGenAt(i1, i2);
		}

		public virtual void cleanupCache()
		{
			long j1 = DateTimeHelper.CurrentUnixTimeMillis();
			long j3 = j1 - this.lastCleanupTime;
			if (j3 > 7500L || j3 < 0L)
			{
				this.lastCleanupTime = j1;

				for (int i5 = 0; i5 < this.cache.Count; ++i5)
				{
					BiomeCacheBlock biomeCacheBlock6 = (BiomeCacheBlock)this.cache[i5];
					long j7 = j1 - biomeCacheBlock6.lastAccessTime;
					if (j7 > 30000L || j7 < 0L)
					{
						this.cache.RemoveAt(i5--);
						long j9 = (long)biomeCacheBlock6.xPosition & 4294967295L | ((long)biomeCacheBlock6.zPosition & 4294967295L) << 32;
						this.cacheMap.remove(j9);
					}
				}
			}

		}

		public virtual BiomeGenBase[] getCachedBiomes(int i1, int i2)
		{
			return this.getBiomeCacheBlock(i1, i2).biomes;
		}

		internal static WorldChunkManager getChunkManager(BiomeCache biomeCache0)
		{
			return biomeCache0.chunkManager;
		}
	}

}