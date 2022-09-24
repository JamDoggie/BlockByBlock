namespace net.minecraft.src
{
	public class MaterialTransparent : Material
	{
		public MaterialTransparent(MapColor mapColor1) : base(mapColor1)
		{
			this.setGroundCover();
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