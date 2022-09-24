namespace net.minecraft.src
{
	public class BiomeGenBeach : BiomeGenBase
	{
		public BiomeGenBeach(int i1) : base(i1)
		{
			this.spawnableCreatureList.Clear();
			this.topBlock = (sbyte)Block.sand.blockID;
			this.fillerBlock = (sbyte)Block.sand.blockID;
			this.biomeDecorator.treesPerChunk = -999;
			this.biomeDecorator.deadBushPerChunk = 0;
			this.biomeDecorator.reedsPerChunk = 0;
			this.biomeDecorator.cactiPerChunk = 0;
		}
	}

}