using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BiomeGenSwamp : BiomeGenBase
	{
		protected internal BiomeGenSwamp(int i1) : base(i1)
		{
			this.biomeDecorator.treesPerChunk = 2;
			this.biomeDecorator.flowersPerChunk = -999;
			this.biomeDecorator.deadBushPerChunk = 1;
			this.biomeDecorator.mushroomsPerChunk = 8;
			this.biomeDecorator.reedsPerChunk = 10;
			this.biomeDecorator.clayPerChunk = 1;
			this.biomeDecorator.waterlilyPerChunk = 4;
			this.waterColorMultiplier = 14745518;
		}

		public override WorldGenerator getRandomWorldGenForTrees(RandomExtended random1)
		{
			return this.worldGenSwamp;
		}

		public override int BiomeGrassColor
		{
			get
			{
				double d1 = (double)this.FloatTemperature;
				double d3 = (double)this.FloatRainfall;
				return ((ColorizerGrass.getGrassColor(d1, d3) & 16711422) + 5115470) / 2;
			}
		}

		public override int BiomeFoliageColor
		{
			get
			{
				double d1 = (double)this.FloatTemperature;
				double d3 = (double)this.FloatRainfall;
				return ((ColorizerFoliage.getFoliageColor(d1, d3) & 16711422) + 5115470) / 2;
			}
		}
	}

}