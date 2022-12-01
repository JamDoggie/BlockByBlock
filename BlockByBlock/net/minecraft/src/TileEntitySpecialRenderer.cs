namespace net.minecraft.src
{
	public abstract class TileEntitySpecialRenderer
	{
		protected internal TileEntityRenderer tileEntityRenderer;

		public abstract void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8);

		protected internal virtual void bindTextureByName(string string1)
		{
			TextureManager renderEngine2 = this.tileEntityRenderer.renderEngine;
			if (renderEngine2 != null)
			{
				renderEngine2.bindTexture(renderEngine2.getTexture(string1));
			}

		}

		public virtual TileEntityRenderer TileEntityRenderer
		{
			set
			{
				this.tileEntityRenderer = value;
			}
		}

		public virtual void cacheSpecialRenderInfo(World world1)
		{
		}

		public virtual FontRenderer FontRenderer
		{
			get
			{
				return this.tileEntityRenderer.FontRenderer;
			}
		}
	}

}