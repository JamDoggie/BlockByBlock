using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class MapGenVillage : MapGenStructure
	{
		public static System.Collections.IList villageSpawnBiomes = new BiomeGenBase[]{BiomeGenBase.plains, BiomeGenBase.desert}.ToList();
		private readonly int terrainType;

		public MapGenVillage(int i1)
		{
			this.terrainType = i1;
		}

		protected internal override bool canSpawnStructureAtCoords(int i1, int i2)
		{
			sbyte b3 = 32;
			sbyte b4 = 8;
			int i5 = i1;
			int i6 = i2;
			if (i1 < 0)
			{
				i1 -= b3 - 1;
			}

			if (i2 < 0)
			{
				i2 -= b3 - 1;
			}

			int i7 = i1 / b3;
			int i8 = i2 / b3;
			RandomExtended random9 = this.worldObj.setRandomSeed(i7, i8, 10387312);
			i7 *= b3;
			i8 *= b3;
			i7 += random9.Next(b3 - b4);
			i8 += random9.Next(b3 - b4);
			if (i5 == i7 && i6 == i8)
			{
				bool z10 = this.worldObj.WorldChunkManager.areBiomesViable(i5 * 16 + 8, i6 * 16 + 8, 0, villageSpawnBiomes);
				if (z10)
				{
					return true;
				}
			}

			return false;
		}

		protected internal override StructureStart getStructureStart(int i1, int i2)
		{
			return new StructureVillageStart(this.worldObj, this.rand, i1, i2, this.terrainType);
		}
	}

}