using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class BlockBookshelf : Block
	{
		public BlockBookshelf(int i1, int i2) : base(i1, i2, Material.wood)
		{
		}

		public override int getBlockTextureFromSide(int i1)
		{
			return i1 <= 1 ? 4 : this.blockIndexInTexture;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 3;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.book.shiftedIndex;
		}
	}

}