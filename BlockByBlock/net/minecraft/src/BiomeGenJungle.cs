using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BiomeGenJungle : BiomeGenBase
	{
		public BiomeGenJungle(int i1) : base(i1)
		{
			this.biomeDecorator.treesPerChunk = 50;
			this.biomeDecorator.grassPerChunk = 25;
			this.biomeDecorator.flowersPerChunk = 4;
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityOcelot), 2, 1, 1));
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityChicken), 10, 4, 4));
		}

		public override WorldGenerator getRandomWorldGenForTrees(RandomExtended random1)
		{
			return (WorldGenerator)(random1.Next(10) == 0 ? this.worldGenBigTree : (random1.Next(2) == 0 ? new WorldGenShrub(3, 0) : (random1.Next(3) == 0 ? new WorldGenHugeTrees(false, 10 + random1.Next(20), 3, 3) : new WorldGenTrees(false, 4 + random1.Next(7), 3, 3, true))));
		}

		public override WorldGenerator func_48410_b(RandomExtended random1)
		{
			return random1.Next(4) == 0 ? new WorldGenTallGrass(Block.tallGrass.blockID, 2) : new WorldGenTallGrass(Block.tallGrass.blockID, 1);
		}

		public override void decorate(World world1, RandomExtended random2, int i3, int i4)
		{
			base.decorate(world1, random2, i3, i4);
			WorldGenVines worldGenVines5 = new WorldGenVines();

			for (int i6 = 0; i6 < 50; ++i6)
			{
				int i7 = i3 + random2.Next(16) + 8;
				sbyte b8 = 64;
				int i9 = i4 + random2.Next(16) + 8;
				worldGenVines5.generate(world1, random2, i7, b8, i9);
			}

		}
	}

}