using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class RenderSnowball : Render
	{
		private int itemIconIndex;

		public RenderSnowball(int i1)
		{
			this.itemIconIndex = i1;
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            Minecraft.newRenderer.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
			GL.Enable(EnableCap.RescaleNormal);
            Minecraft.newRenderer.ModelMatrix.Scale(0.5F, 0.5F, 0.5F);
			this.loadTexture("/gui/items.png");
			Tessellator tessellator10 = Tessellator.instance;
			if (this.itemIconIndex == 154)
			{
				int i11 = PotionHelper.func_40358_a(((EntityPotion)entity1).PotionDamage, false);
				float f12 = (float)(i11 >> 16 & 255) / 255.0F;
				float f13 = (float)(i11 >> 8 & 255) / 255.0F;
				float f14 = (float)(i11 & 255) / 255.0F;
				GL.Color3(f12, f13, f14);
                Minecraft.newRenderer.ModelMatrix.PushMatrix();
				this.func_40265_a(tessellator10, 141);
                Minecraft.newRenderer.ModelMatrix.PopMatrix();
				GL.Color3(1.0F, 1.0F, 1.0F);
			}

			this.func_40265_a(tessellator10, this.itemIconIndex);
			GL.Disable(EnableCap.RescaleNormal);
            Minecraft.newRenderer.ModelMatrix.PopMatrix();
		}

		private void func_40265_a(Tessellator tessellator1, int i2)
		{
			float f3 = (float)(i2 % 16 * 16 + 0) / 256.0F;
			float f4 = (float)(i2 % 16 * 16 + 16) / 256.0F;
			float f5 = (float)(i2 / 16 * 16 + 0) / 256.0F;
			float f6 = (float)(i2 / 16 * 16 + 16) / 256.0F;
			float f7 = 1.0F;
			float f8 = 0.5F;
			float f9 = 0.25F;
            Minecraft.newRenderer.ModelMatrix.Rotate(180.0F - this.renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
            Minecraft.newRenderer.ModelMatrix.Rotate(-this.renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
			tessellator1.startDrawingQuads();
			tessellator1.setNormal(0.0F, 1.0F, 0.0F);
			tessellator1.addVertexWithUV((double)(0.0F - f8), (double)(0.0F - f9), 0.0D, (double)f3, (double)f6);
			tessellator1.addVertexWithUV((double)(f7 - f8), (double)(0.0F - f9), 0.0D, (double)f4, (double)f6);
			tessellator1.addVertexWithUV((double)(f7 - f8), (double)(f7 - f9), 0.0D, (double)f4, (double)f5);
			tessellator1.addVertexWithUV((double)(0.0F - f8), (double)(f7 - f9), 0.0D, (double)f3, (double)f5);
			tessellator1.draw();
		}
	}

}