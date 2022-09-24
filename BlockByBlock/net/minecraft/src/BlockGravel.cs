using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockGravel : BlockSand
	{
		public BlockGravel(int i1, int i2) : base(i1, i2)
		{
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return random2.Next(10 - i3 * 3) == 0 ? Item.flint.shiftedIndex : this.blockID;
		}
	}

}