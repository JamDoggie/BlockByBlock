using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class RenderTNTPrimed : Render
	{
		private RenderBlocks blockRenderer = new RenderBlocks();

		public RenderTNTPrimed()
		{
			this.shadowSize = 0.5F;
		}

		public virtual void func_153_a(EntityTNTPrimed entityTNTPrimed1, double d2, double d4, double d6, float f8, float f9)
		{
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            Minecraft.newRenderer.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
			float f10;
			if ((float)entityTNTPrimed1.fuse - f9 + 1.0F < 10.0F)
			{
				f10 = 1.0F - ((float)entityTNTPrimed1.fuse - f9 + 1.0F) / 10.0F;
				if (f10 < 0.0F)
				{
					f10 = 0.0F;
				}

				if (f10 > 1.0F)
				{
					f10 = 1.0F;
				}

				f10 *= f10;
				f10 *= f10;
				float f11 = 1.0F + f10 * 0.3F;
                Minecraft.newRenderer.ModelMatrix.Scale(f11, f11, f11);
			}

			f10 = (1.0F - ((float)entityTNTPrimed1.fuse - f9 + 1.0F) / 100.0F) * 0.8F;
			this.loadTexture("/terrain.png");
			this.blockRenderer.renderBlockAsItem(Block.tnt, 0, entityTNTPrimed1.getBrightness(f9));
			if (entityTNTPrimed1.fuse / 5 % 2 == 0)
			{
				GL.Disable(EnableCap.Texture2D);
				GL.Disable(EnableCap.Lighting);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.DstAlpha);
				GL.Color4(1.0F, 1.0F, 1.0F, f10);
				this.blockRenderer.renderBlockAsItem(Block.tnt, 0, 1.0F);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				GL.Disable(EnableCap.Blend);
				GL.Enable(EnableCap.Lighting);
				GL.Enable(EnableCap.Texture2D);
			}

            Minecraft.newRenderer.ModelMatrix.PopMatrix();
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.func_153_a((EntityTNTPrimed)entity1, d2, d4, d6, f8, f9);
		}
	}

}