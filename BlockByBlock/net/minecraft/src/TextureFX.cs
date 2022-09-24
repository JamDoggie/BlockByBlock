namespace net.minecraft.src
{
	using GL11 = org.lwjgl.opengl.GL11;

	public class TextureFX
	{
		public sbyte[] imageData = new sbyte[1024];
		public int iconIndex;
		public bool anaglyphEnabled = false;
		public int textureId = 0;
		public int tileSize = 1;
		public int tileImage = 0;

		public TextureFX(int i1)
		{
			this.iconIndex = i1;
		}

		public virtual void onTick()
		{
		}

		public virtual void bindImage(RenderEngine renderEngine1)
		{
			if (this.tileImage == 0)
			{
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, renderEngine1.getTexture("/terrain.png"));
			}
			else if (this.tileImage == 1)
			{
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, renderEngine1.getTexture("/gui/items.png"));
			}

		}
	}

}