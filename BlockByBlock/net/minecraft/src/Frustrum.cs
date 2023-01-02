namespace net.minecraft.src
{
	public struct Frustrum : ICamera
	{
		private ClippingHelper clippingHelper = ClippingHelperImpl.Instance;
		private double xPosition;
		private double yPosition;
		private double zPosition;

        public Frustrum()
        {
        }

        public void setPosition(double d1, double d3, double d5)
		{
			this.xPosition = d1;
			this.yPosition = d3;
			this.zPosition = d5;
		}

		public bool isBoxInFrustum(double d1, double d3, double d5, double d7, double d9, double d11)
		{
			return this.clippingHelper.isBoxInFrustum(d1 - this.xPosition, d3 - this.yPosition, d5 - this.zPosition, d7 - this.xPosition, d9 - this.yPosition, d11 - this.zPosition);
		}

		public bool isBoundingBoxInFrustum(AxisAlignedBB axisAlignedBB1)
		{
			return this.isBoxInFrustum(axisAlignedBB1.minX, axisAlignedBB1.minY, axisAlignedBB1.minZ, axisAlignedBB1.maxX, axisAlignedBB1.maxY, axisAlignedBB1.maxZ);
		}
	}

}