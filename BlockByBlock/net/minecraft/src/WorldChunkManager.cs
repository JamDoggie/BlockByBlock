using BlockByBlock.java_extensions;
using System;
using System.Collections;

namespace net.minecraft.src
{

	public class WorldChunkManager
	{
		private GenLayer genBiomes;
		private GenLayer biomeIndexLayer;
		private BiomeCache biomeCache;
		private System.Collections.IList biomesToSpawnIn;

		protected internal WorldChunkManager()
		{
			biomeCache = new BiomeCache(this);
			biomesToSpawnIn = new ArrayList();
			biomesToSpawnIn.Add(BiomeGenBase.forest);
			biomesToSpawnIn.Add(BiomeGenBase.plains);
			biomesToSpawnIn.Add(BiomeGenBase.taiga);
			biomesToSpawnIn.Add(BiomeGenBase.taigaHills);
			biomesToSpawnIn.Add(BiomeGenBase.forestHills);
			biomesToSpawnIn.Add(BiomeGenBase.jungle);
			biomesToSpawnIn.Add(BiomeGenBase.jungleHills);
		}

		public WorldChunkManager(long j1, WorldType worldType3) : this()
		{
			GenLayer[] genLayer4 = GenLayer.func_48425_a(j1, worldType3);
			genBiomes = genLayer4[0];
			biomeIndexLayer = genLayer4[1];
		}

		public WorldChunkManager(World world1) : this(world1.Seed, world1.WorldInfo.TerrainType)
		{
		}

		public virtual System.Collections.IList BiomesToSpawnIn
		{
			get
			{
				return this.biomesToSpawnIn;
			}
		}

		public virtual BiomeGenBase getBiomeGenAt(int i1, int i2)
		{
			return this.biomeCache.getBiomeGenAt(i1, i2);
		}

		public virtual float[] getRainfall(float[] f1, int i2, int i3, int i4, int i5)
		{
			IntCache.resetIntCache();
			if (f1 == null || f1.Length < i4 * i5)
			{
				f1 = new float[i4 * i5];
			}

			int[] i6 = this.biomeIndexLayer.getInts(i2, i3, i4, i5);

			for (int i7 = 0; i7 < i4 * i5; ++i7)
			{
				float f8 = (float)BiomeGenBase.biomeList[i6[i7]].IntRainfall / 65536.0F;
				if (f8 > 1.0F)
				{
					f8 = 1.0F;
				}

				f1[i7] = f8;
			}

			return f1;
		}

		public virtual float getTemperatureAtHeight(float f1, int i2)
		{
			return f1;
		}

		public virtual float[] getTemperatures(float[] f1, int i2, int i3, int i4, int i5)
		{
			IntCache.resetIntCache();
			if (f1 == null || f1.Length < i4 * i5)
			{
				f1 = new float[i4 * i5];
			}

			int[] i6 = this.biomeIndexLayer.getInts(i2, i3, i4, i5);

			for (int i7 = 0; i7 < i4 * i5; ++i7)
			{
				float f8 = (float)BiomeGenBase.biomeList[i6[i7]].IntTemperature / 65536.0F;
				if (f8 > 1.0F)
				{
					f8 = 1.0F;
				}

				f1[i7] = f8;
			}

			return f1;
		}

		public virtual BiomeGenBase[] getBiomesForGeneration(BiomeGenBase[] biomeGenBase1, int i2, int i3, int i4, int i5)
		{
			IntCache.resetIntCache();
			if (biomeGenBase1 == null || biomeGenBase1.Length < i4 * i5)
			{
				biomeGenBase1 = new BiomeGenBase[i4 * i5];
			}

			int[] i6 = this.genBiomes.getInts(i2, i3, i4, i5);

			for (int i7 = 0; i7 < i4 * i5; ++i7)
			{
				biomeGenBase1[i7] = BiomeGenBase.biomeList[i6[i7]];
			}

			return biomeGenBase1;
		}

		public virtual BiomeGenBase[] loadBlockGeneratorData(BiomeGenBase[] biomeGenBase1, int i2, int i3, int i4, int i5)
		{
			return this.getBiomeGenAt(biomeGenBase1, i2, i3, i4, i5, true);
		}

		public virtual BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] biomeGenBase1, int i2, int i3, int i4, int i5, bool z6)
		{
			IntCache.resetIntCache();
			if (biomeGenBase1 == null || biomeGenBase1.Length < i4 * i5)
			{
				biomeGenBase1 = new BiomeGenBase[i4 * i5];
			}

			if (z6 && i4 == 16 && i5 == 16 && (i2 & 15) == 0 && (i3 & 15) == 0)
			{
				BiomeGenBase[] biomeGenBase9 = this.biomeCache.getCachedBiomes(i2, i3);
				Array.Copy(biomeGenBase9, 0, biomeGenBase1, 0, i4 * i5);
				return biomeGenBase1;
			}
			else
			{
				int[] i7 = this.biomeIndexLayer.getInts(i2, i3, i4, i5);

				for (int i8 = 0; i8 < i4 * i5; ++i8)
				{
					biomeGenBase1[i8] = BiomeGenBase.biomeList[i7[i8]];
				}

				return biomeGenBase1;
			}
		}

		public virtual bool areBiomesViable(int i1, int i2, int i3, System.Collections.IList list4)
		{
			int i5 = i1 - i3 >> 2;
			int i6 = i2 - i3 >> 2;
			int i7 = i1 + i3 >> 2;
			int i8 = i2 + i3 >> 2;
			int i9 = i7 - i5 + 1;
			int i10 = i8 - i6 + 1;
			int[] i11 = this.genBiomes.getInts(i5, i6, i9, i10);

			for (int i12 = 0; i12 < i9 * i10; ++i12)
			{
				BiomeGenBase biomeGenBase13 = BiomeGenBase.biomeList[i11[i12]];
				if (!list4.Contains(biomeGenBase13))
				{
					return false;
				}
			}

			return true;
		}

		public virtual ChunkPosition? findBiomePosition(int i1, int i2, int i3, System.Collections.IList list4, RandomExtended random5)
		{
			int i6 = i1 - i3 >> 2;
			int i7 = i2 - i3 >> 2;
			int i8 = i1 + i3 >> 2;
			int i9 = i2 + i3 >> 2;
			int i10 = i8 - i6 + 1;
			int i11 = i9 - i7 + 1;
			int[] i12 = this.genBiomes.getInts(i6, i7, i10, i11);
			ChunkPosition? chunkPosition13 = null;
			int i14 = 0;

			for (int i15 = 0; i15 < i12.Length; ++i15)
			{
				int i16 = i6 + i15 % i10 << 2;
				int i17 = i7 + i15 / i10 << 2;
				BiomeGenBase biomeGenBase18 = BiomeGenBase.biomeList[i12[i15]];
				if (list4.Contains(biomeGenBase18) && (chunkPosition13 == null || random5.Next(i14 + 1) == 0))
				{
					chunkPosition13 = new ChunkPosition(i16, 0, i17);
					++i14;
				}
			}

			return chunkPosition13;
		}

		public virtual void cleanupCache()
		{
			this.biomeCache.cleanupCache();
		}
	}

}