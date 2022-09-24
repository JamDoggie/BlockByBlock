namespace net.minecraft.src
{
	public class BlockCloth : Block
	{
		public BlockCloth() : base(35, 64, Material.cloth)
		{
		}

		public override int getBlockTextureFromSideAndMetadata(int i1, int i2)
		{
			if (i2 == 0)
			{
				return blockIndexInTexture;
			}
			else
			{
				i2 = ~(i2 & 15);
				return 113 + ((i2 & 8) >> 3) + (i2 & 7) * 16;
			}
		}

		protected internal override int damageDropped(int i1)
		{
			return i1;
		}

		public static int getBlockFromDye(int i0)
		{
			return ~i0 & 15;
		}

		public static int getDyeFromBlock(int i0)
		{
			return ~i0 & 15;
		}
	}

}