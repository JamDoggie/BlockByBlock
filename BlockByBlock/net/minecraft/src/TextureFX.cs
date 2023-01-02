using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class TextureFX
	{
		public byte[] imageData { get; set; } = new byte[1024];
		public int iconIndex;
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

		public virtual void bindImage(TextureManager renderEngine1)
		{
			if (this.tileImage == 0)
			{
				GL.BindTexture(TextureTarget.Texture2D, renderEngine1.getTexture("/terrain.png"));
			}
			else if (this.tileImage == 1)
			{
				GL.BindTexture(TextureTarget.Texture2D, renderEngine1.getTexture("/gui/items.png"));
			}

		}
	}

}