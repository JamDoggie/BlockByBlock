namespace net.minecraft.src
{
	public class BiomeGenPlains : BiomeGenBase
	{
		protected internal BiomeGenPlains(int i1) : base(i1)
		{
			this.biomeDecorator.treesPerChunk = -999;
			this.biomeDecorator.flowersPerChunk = 4;
			this.biomeDecorator.grassPerChunk = 10;
		}
	}

}