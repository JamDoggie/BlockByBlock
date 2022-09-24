namespace net.minecraft.src
{
	internal sealed class MaterialWeb : Material
	{
		internal MaterialWeb(MapColor mapColor1) : base(mapColor1)
		{
		}

		public override bool blocksMovement()
		{
			return false;
		}
	}

}