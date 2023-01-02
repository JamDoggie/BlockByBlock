using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class BiomeGenEnd : BiomeGenBase
	{
		public BiomeGenEnd(int i1) : base(i1)
		{
			this.spawnableMonsterList.Clear();
			this.spawnableCreatureList.Clear();
			this.spawnableWaterCreatureList.Clear();
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityEnderman), 10, 4, 4));
			this.topBlock = (sbyte)Block.dirt.blockID;
			this.fillerBlock = (sbyte)Block.dirt.blockID;
			this.biomeDecorator = new BiomeEndDecorator(this);
		}

		public override int getSkyColorByTemp(float f1)
		{
			return 0;
		}
	}

}