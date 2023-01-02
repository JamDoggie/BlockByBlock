using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class BlockSoulSand : Block
	{
		public BlockSoulSand(int i1, int i2) : base(i1, i2, Material.sand)
		{
		}

		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World world1, int i2, int i3, int i4)
		{
			float f5 = 0.125F;
			return AxisAlignedBB.getBoundingBoxFromPool((double)i2, (double)i3, (double)i4, (double)(i2 + 1), (double)((float)(i3 + 1) - f5), (double)(i4 + 1));
		}

		public override void onEntityCollidedWithBlock(World world1, int i2, int i3, int i4, Entity entity5)
		{
			entity5.motionX *= 0.4D;
			entity5.motionZ *= 0.4D;
		}
	}

}