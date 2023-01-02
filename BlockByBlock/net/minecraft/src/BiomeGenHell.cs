using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class BiomeGenHell : BiomeGenBase
	{
		public BiomeGenHell(int i1) : base(i1)
		{
			this.spawnableMonsterList.Clear();
			this.spawnableCreatureList.Clear();
			this.spawnableWaterCreatureList.Clear();
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityGhast), 50, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityPigZombie), 100, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityMagmaCube), 1, 4, 4));
		}
	}

}