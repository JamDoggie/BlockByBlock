using BlockByBlock.java_extensions;
using System;
using System.Collections;
using System.Linq;

namespace net.minecraft.src
{

	public class MapGenStronghold : MapGenStructure
	{
		private BiomeGenBase[] allowedBiomeGenBases = new BiomeGenBase[]{BiomeGenBase.desert, BiomeGenBase.forest, BiomeGenBase.extremeHills, BiomeGenBase.swampland, BiomeGenBase.taiga, BiomeGenBase.icePlains, BiomeGenBase.iceMountains, BiomeGenBase.desertHills, BiomeGenBase.forestHills, BiomeGenBase.extremeHillsEdge, BiomeGenBase.jungle, BiomeGenBase.jungleHills};
		private bool ranBiomeCheck;
		private ChunkCoordIntPair[] structureCoords = new ChunkCoordIntPair[3];

		protected internal override bool canSpawnStructureAtCoords(int i1, int i2)
		{
			if (!this.ranBiomeCheck)
			{
				RandomExtended random3 = new RandomExtended(worldObj.Seed);

				double d4 = random3.NextDouble() * Math.PI * 2.0D;

				for (int i6 = 0; i6 < this.structureCoords.Length; ++i6)
				{
					double d7 = (1.25D + random3.NextDouble()) * 32.0D;
					int i9 = (int)(long)Math.Round(Math.Cos(d4) * d7, MidpointRounding.AwayFromZero);
					int i10 = (int)(long)Math.Round(Math.Sin(d4) * d7, MidpointRounding.AwayFromZero);
					ArrayList arrayList11 = new ArrayList();
					BiomeGenBase[] biomeGenBase12 = this.allowedBiomeGenBases;
					int i13 = biomeGenBase12.Length;

					for (int i14 = 0; i14 < i13; ++i14)
					{
						BiomeGenBase biomeGenBase15 = biomeGenBase12[i14];
						arrayList11.Add(biomeGenBase15);
					}

					ChunkPosition? chunkPosition19 = this.worldObj.WorldChunkManager.findBiomePosition((i9 << 4) + 8, (i10 << 4) + 8, 112, arrayList11, random3);
					if (chunkPosition19 != null)
					{
						i9 = chunkPosition19.Value.x >> 4;
						i10 = chunkPosition19.Value.z >> 4;
					}
					else
					{
						Console.WriteLine("Placed stronghold in INVALID biome at (" + i9 + ", " + i10 + ")");
					}

					this.structureCoords[i6] = new ChunkCoordIntPair(i9, i10);
					d4 += Math.PI * 2D / (double)this.structureCoords.Length;
				}

				this.ranBiomeCheck = true;
			}

			ChunkCoordIntPair[] chunkCoordIntPair16 = this.structureCoords;
			int i17 = chunkCoordIntPair16.Length;

			for (int i5 = 0; i5 < i17; ++i5)
			{
				ChunkCoordIntPair chunkCoordIntPair18 = chunkCoordIntPair16[i5];
				if (i1 == chunkCoordIntPair18.chunkXPos && i2 == chunkCoordIntPair18.chunkZPos)
				{
					Console.WriteLine(i1 + ", " + i2);
					return true;
				}
			}

			return false;
		}

		protected internal override System.Collections.IList func_40482_a()
		{
			ArrayList arrayList1 = new ArrayList();
			ChunkCoordIntPair[] chunkCoordIntPair2 = this.structureCoords;
			int i3 = chunkCoordIntPair2.Length;

			for (int i4 = 0; i4 < i3; ++i4)
			{
				ChunkCoordIntPair chunkCoordIntPair5 = chunkCoordIntPair2[i4];
				if (chunkCoordIntPair5 != null)
				{
					arrayList1.Add(chunkCoordIntPair5.getChunkPosition(64));
				}
			}

			return arrayList1;
		}

		protected internal override StructureStart getStructureStart(int i1, int i2)
		{
			StructureStrongholdStart structureStrongholdStart3;
			for (structureStrongholdStart3 = new StructureStrongholdStart(this.worldObj, this.rand, i1, i2); structureStrongholdStart3.Components.Count() == 0 || ((ComponentStrongholdStairs2)structureStrongholdStart3.Components.ToList()[0]).portalRoom == null; structureStrongholdStart3 = new StructureStrongholdStart(this.worldObj, this.rand, i1, i2))
			{
			}

			return structureStrongholdStart3;
		}
	}

}