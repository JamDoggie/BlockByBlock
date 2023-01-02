using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class BiomeGenMushroomIsland : BiomeGenBase
	{
		public BiomeGenMushroomIsland(int i1) : base(i1)
		{
			this.biomeDecorator.treesPerChunk = -100;
			this.biomeDecorator.flowersPerChunk = -100;
			this.biomeDecorator.grassPerChunk = -100;
			this.biomeDecorator.mushroomsPerChunk = 1;
			this.biomeDecorator.bigMushroomsPerChunk = 1;
			this.topBlock = (sbyte)Block.mycelium.blockID;
			this.spawnableMonsterList.Clear();
			this.spawnableCreatureList.Clear();
			this.spawnableWaterCreatureList.Clear();
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityMooshroom), 8, 4, 8));
		}
	}

}