namespace net.minecraft.src
{
	public class BiomeGenBeach : BiomeGenBase
	{
		public BiomeGenBeach(int i1) : base(i1)
		{
			spawnableCreatureList.Clear();
			topBlock = (sbyte)Block.sand.blockID;
			fillerBlock = (sbyte)Block.sand.blockID;
			biomeDecorator.treesPerChunk = -999;
			biomeDecorator.deadBushPerChunk = 0;
			biomeDecorator.reedsPerChunk = 0;
			biomeDecorator.cactiPerChunk = 0;
		}
	}

}