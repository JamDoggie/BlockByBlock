namespace net.minecraft.src
{
	public class MaterialPortal : Material
	{
		public MaterialPortal(MapColor mapColor1) : base(mapColor1)
		{
		}

		public override bool Solid
		{
			get
			{
				return false;
			}
		}

		public override bool CanBlockGrass
		{
			get
			{
				return false;
			}
		}

		public override bool blocksMovement()
		{
			return false;
		}
	}

}