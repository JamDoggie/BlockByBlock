namespace net.minecraft.src
{
	public interface ICamera
	{
		bool isBoundingBoxInFrustum(AxisAlignedBB axisAlignedBB1);

		void setPosition(double d1, double d3, double d5);
	}

}