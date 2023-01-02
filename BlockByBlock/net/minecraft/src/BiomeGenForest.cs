using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BiomeGenForest : BiomeGenBase
	{
		public BiomeGenForest(int i1) : base(i1)
		{
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityWolf), 5, 4, 4));
			this.biomeDecorator.treesPerChunk = 10;
			this.biomeDecorator.grassPerChunk = 2;
		}

		public override WorldGenerator getRandomWorldGenForTrees(RandomExtended random1)
		{
			return (WorldGenerator)(random1.Next(5) == 0 ? this.worldGenForest : (random1.Next(10) == 0 ? this.worldGenBigTree : this.worldGenTrees));
		}
	}

}