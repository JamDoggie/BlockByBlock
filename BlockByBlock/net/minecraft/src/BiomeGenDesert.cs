using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BiomeGenDesert : BiomeGenBase
	{
		public BiomeGenDesert(int i1) : base(i1)
		{
			this.spawnableCreatureList.Clear();
			this.topBlock = (sbyte)Block.sand.blockID;
			this.fillerBlock = (sbyte)Block.sand.blockID;
			this.biomeDecorator.treesPerChunk = -999;
			this.biomeDecorator.deadBushPerChunk = 2;
			this.biomeDecorator.reedsPerChunk = 50;
			this.biomeDecorator.cactiPerChunk = 10;
		}

		public override void decorate(World world1, RandomExtended random2, int i3, int i4)
		{
			base.decorate(world1, random2, i3, i4);
			if (random2.Next(1000) == 0)
			{
				int i5 = i3 + random2.Next(16) + 8;
				int i6 = i4 + random2.Next(16) + 8;
				WorldGenDesertWells worldGenDesertWells7 = new WorldGenDesertWells();
				worldGenDesertWells7.generate(world1, random2, i5, world1.getHeightValue(i5, i6) + 1, i6);
			}

		}
	}

}