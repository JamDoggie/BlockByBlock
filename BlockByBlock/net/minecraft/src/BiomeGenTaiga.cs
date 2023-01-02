using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BiomeGenTaiga : BiomeGenBase
	{
		public BiomeGenTaiga(int i1) : base(i1)
		{
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityWolf), 8, 4, 4));
			this.biomeDecorator.treesPerChunk = 10;
			this.biomeDecorator.grassPerChunk = 1;
		}

		public override WorldGenerator getRandomWorldGenForTrees(RandomExtended random1)
		{
			return (WorldGenerator)(random1.Next(3) == 0 ? new WorldGenTaiga1() : new WorldGenTaiga2(false));
		}
	}

}