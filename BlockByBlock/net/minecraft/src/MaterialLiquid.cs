namespace net.minecraft.src
{
	public class MaterialLiquid : Material
	{
		public MaterialLiquid(MapColor mapColor1) : base(mapColor1)
		{
			this.setGroundCover();
			this.setNoPushMobility();
		}

		public override bool Liquid
		{
			get
			{
				return true;
			}
		}

		public override bool blocksMovement()
		{
			return false;
		}

		public override bool Solid
		{
			get
			{
				return false;
			}
		}
	}

}