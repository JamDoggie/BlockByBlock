using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class ChunkProviderFlat : IChunkProvider
	{
		private World worldObj;
		private RandomExtended random;
		private readonly bool useStructures;
		private MapGenVillage villageGen = new MapGenVillage(1);

		public ChunkProviderFlat(World world1, long j2, bool z4)
		{
			worldObj = world1;
			useStructures = z4;
			random = new RandomExtended(j2);
		}

		private void generate(sbyte[] b1)
		{
			int i2 = b1.Length / 256;

			for (int i3 = 0; i3 < 16; ++i3)
			{
				for (int i4 = 0; i4 < 16; ++i4)
				{
					for (int i5 = 0; i5 < i2; ++i5)
					{
						int i6 = 0;
						if (i5 == 0)
						{
							i6 = Block.bedrock.blockID;
						}
						else if (i5 <= 2)
						{
							i6 = Block.dirt.blockID;
						}
						else if (i5 == 3)
						{
							i6 = Block.grass.blockID;
						}

						b1[i3 << 11 | i4 << 7 | i5] = (sbyte)i6;
					}
				}
			}

		}

		public virtual Chunk loadChunk(int i1, int i2)
		{
			return this.provideChunk(i1, i2);
		}

		public virtual Chunk provideChunk(int i1, int i2)
		{
			sbyte[] b3 = new sbyte[32768];
			this.generate(b3);
			Chunk chunk4 = new Chunk(this.worldObj, b3, i1, i2);
			if (this.useStructures)
			{
				this.villageGen.generate(this, this.worldObj, i1, i2, b3);
			}

			BiomeGenBase[] biomeGenBase5 = this.worldObj.WorldChunkManager.loadBlockGeneratorData((BiomeGenBase[])null, i1 * 16, i2 * 16, 16, 16);
			sbyte[] b6 = chunk4.BiomeArray;

			for (int i7 = 0; i7 < b6.Length; ++i7)
			{
				b6[i7] = (sbyte)biomeGenBase5[i7].biomeID;
			}

			chunk4.generateSkylightMap();
			return chunk4;
		}

		public virtual bool chunkExists(int i1, int i2)
		{
			return true;
		}

		public virtual void populate(IChunkProvider iChunkProvider1, int i2, int i3)
		{
			this.random.SetSeed(this.worldObj.Seed); 
			random = new RandomExtended(this.worldObj.Seed);
            long j4 = this.random.NextInt64() / 2L * 2L + 1L;
			long j6 = this.random.NextInt64() / 2L * 2L + 1L;
			this.random.SetSeed((long)i2 * j4 + (long)i3 * j6 ^ this.worldObj.Seed);
			if (this.useStructures)
			{
				this.villageGen.generateStructuresInChunk(this.worldObj, this.random, i2, i3);
			}

		}

		public virtual bool saveChunks(bool z1, IProgressUpdate iProgressUpdate2)
		{
			return true;
		}

		public virtual bool unload100OldestChunks()
		{
			return false;
		}

		public virtual bool canSave()
		{
			return true;
		}

		public virtual string makeString()
		{
			return "FlatLevelSource";
		}

		public virtual System.Collections.IList getPossibleCreatures(EnumCreatureType enumCreatureType1, int i2, int i3, int i4)
		{
			BiomeGenBase biomeGenBase5 = this.worldObj.getBiomeGenForCoords(i2, i4);
			return biomeGenBase5 == null ? null : biomeGenBase5.getSpawnableList(enumCreatureType1);
		}

		public virtual ChunkPosition? findClosestStructure(World world1, string string2, int i3, int i4, int i5)
		{
			return null;
		}
	}

}