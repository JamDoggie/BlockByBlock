namespace net.minecraft.src
{
	public class BlockBreakable : Block
	{
		private bool localFlag;

		protected internal BlockBreakable(int i1, int i2, Material material3, bool z4) : base(i1, i2, material3)
		{
			this.localFlag = z4;
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool shouldSideBeRendered(IBlockAccess iBlockAccess1, int i2, int i3, int i4, int i5)
		{
			int i6 = iBlockAccess1.getBlockId(i2, i3, i4);
			return !this.localFlag && i6 == this.blockID ? false : base.shouldSideBeRendered(iBlockAccess1, i2, i3, i4, i5);
		}
	}

}