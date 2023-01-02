using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class WorldChunkManagerHell : WorldChunkManager
	{
		private BiomeGenBase biomeGenerator;
		private float hellTemperature;
		private float rainfall;

		public WorldChunkManagerHell(BiomeGenBase biomeGenBase1, float f2, float f3)
		{
			this.biomeGenerator = biomeGenBase1;
			this.hellTemperature = f2;
			this.rainfall = f3;
		}

		public override BiomeGenBase getBiomeGenAt(int i1, int i2)
		{
			return this.biomeGenerator;
		}

		public override BiomeGenBase[] getBiomesForGeneration(BiomeGenBase[] biomeGenBase1, int i2, int i3, int i4, int i5)
		{
			if (biomeGenBase1 == null || biomeGenBase1.Length < i4 * i5)
			{
				biomeGenBase1 = new BiomeGenBase[i4 * i5];
			}

			Arrays.Fill(biomeGenBase1, 0, i4 * i5, this.biomeGenerator);
			return biomeGenBase1;
		}

		public override float[] getTemperatures(float[] f1, int i2, int i3, int i4, int i5)
		{
			if (f1 == null || f1.Length < i4 * i5)
			{
				f1 = new float[i4 * i5];
			}

			Arrays.Fill(f1, 0, i4 * i5, this.hellTemperature);
			return f1;
		}

		public override float[] getRainfall(float[] f1, int i2, int i3, int i4, int i5)
		{
			if (f1 == null || f1.Length < i4 * i5)
			{
				f1 = new float[i4 * i5];
			}

			Arrays.Fill(f1, 0, i4 * i5, this.rainfall);
			return f1;
		}

		public override BiomeGenBase[] loadBlockGeneratorData(BiomeGenBase[] biomeGenBase1, int i2, int i3, int i4, int i5)
		{
			if (biomeGenBase1 == null || biomeGenBase1.Length < i4 * i5)
			{
				biomeGenBase1 = new BiomeGenBase[i4 * i5];
			}

			Arrays.Fill(biomeGenBase1, 0, i4 * i5, this.biomeGenerator);
			return biomeGenBase1;
		}

		public override BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] biomeGenBase1, int i2, int i3, int i4, int i5, bool z6)
		{
			return this.loadBlockGeneratorData(biomeGenBase1, i2, i3, i4, i5);
		}

		public override ChunkPosition? findBiomePosition(int i1, int i2, int i3, System.Collections.IList list4, RandomExtended random5)
		{
			return list4.Contains(this.biomeGenerator) ? new ChunkPosition(i1 - i3 + random5.Next(i3 * 2 + 1), 0, i2 - i3 + random5.Next(i3 * 2 + 1)) : null;
		}

		public override bool areBiomesViable(int i1, int i2, int i3, System.Collections.IList list4)
		{
			return list4.Contains(this.biomeGenerator);
		}
	}

}