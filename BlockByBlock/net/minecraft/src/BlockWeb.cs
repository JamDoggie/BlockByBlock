using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class BlockWeb : Block
	{
		public BlockWeb(int i1, int i2) : base(i1, i2, Material.web)
		{
		}

		public override void onEntityCollidedWithBlock(World world1, int i2, int i3, int i4, Entity entity5)
		{
			entity5.setInWeb();
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			return null;
		}

		public override int RenderType
		{
			get
			{
				return 1;
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return Item.silk.shiftedIndex;
		}
	}

}