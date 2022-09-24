using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockGlass : BlockBreakable
	{
		public BlockGlass(int i1, int i2, Material material3, bool z4) : base(i1, i2, material3, z4)
		{
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override int RenderBlockPass
		{
			get
			{
				return 0;
			}
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		protected internal override bool func_50074_q()
		{
			return true;
		}
	}

}