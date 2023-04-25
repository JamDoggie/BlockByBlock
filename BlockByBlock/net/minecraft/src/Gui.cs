using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class Gui
	{
		protected internal float zLevel = 0.0F;

		protected internal virtual void drawHorizontalLine(int i1, int i2, int i3, int i4)
		{
			if (i2 < i1)
			{
				int i5 = i1;
				i1 = i2;
				i2 = i5;
			}

			drawRect(i1, i3, i2 + 1, i3 + 1, i4);
		}

		protected internal virtual void drawVerticalLine(int i1, int i2, int i3, int i4)
		{
			if (i3 < i2)
			{
				int i5 = i2;
				i2 = i3;
				i3 = i5;
			}

			drawRect(i1, i2 + 1, i1 + 1, i3, i4);
		}

		public static void drawRect(int i0, int i1, int i2, int i3, int i4)
		{
			int i5;
			if (i0 < i2)
			{
				i5 = i0;
				i0 = i2;
				i2 = i5;
			}

			if (i1 < i3)
			{
				i5 = i1;
				i1 = i3;
				i3 = i5;
			}

			float f10 = (float)(i4 >> 24 & 255) / 255.0F;
			float f6 = (float)(i4 >> 16 & 255) / 255.0F;
			float f7 = (float)(i4 >> 8 & 255) / 255.0F;
			float f8 = (float)(i4 & 255) / 255.0F;
			Tessellator tessellator9 = Tessellator.instance;
			GL.Enable(EnableCap.Blend);
            Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Minecraft.renderPipeline.SetColor(f6, f7, f8, f10);
			tessellator9.startDrawingQuads();
			tessellator9.AddVertex((double)i0, (double)i3, 0.0D);
			tessellator9.AddVertex((double)i2, (double)i3, 0.0D);
			tessellator9.AddVertex((double)i2, (double)i1, 0.0D);
			tessellator9.AddVertex((double)i0, (double)i1, 0.0D);
			tessellator9.DrawImmediate();
			Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
			GL.Disable(EnableCap.Blend);
		}

		protected internal virtual void drawGradientRect(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			float f7 = (float)(i5 >> 24 & 255) / 255.0F;
			float f8 = (float)(i5 >> 16 & 255) / 255.0F;
			float f9 = (float)(i5 >> 8 & 255) / 255.0F;
			float f10 = (float)(i5 & 255) / 255.0F;
			float f11 = (float)(i6 >> 24 & 255) / 255.0F;
			float f12 = (float)(i6 >> 16 & 255) / 255.0F;
			float f13 = (float)(i6 >> 8 & 255) / 255.0F;
			float f14 = (float)(i6 & 255) / 255.0F;
			Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
			GL.Enable(EnableCap.Blend);
			Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, true);
			Tessellator tessellator15 = Tessellator.instance;
			tessellator15.startDrawingQuads();
			tessellator15.setColorRGBA_F(f8, f9, f10, f7);
			tessellator15.AddVertex((double)i3, (double)i2, (double)this.zLevel);
			tessellator15.AddVertex((double)i1, (double)i2, (double)this.zLevel);
			tessellator15.setColorRGBA_F(f12, f13, f14, f11);
			tessellator15.AddVertex((double)i1, (double)i4, (double)this.zLevel);
			tessellator15.AddVertex((double)i3, (double)i4, (double)this.zLevel);
			tessellator15.DrawImmediate();
			Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, false);
			GL.Disable(EnableCap.Blend);
			Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
			Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
		}

		public virtual void drawCenteredString(FontRenderer fontRenderer1, string string2, int x, int y, int i5)
		{
			fontRenderer1.drawStringWithShadow(string2, x - fontRenderer1.getStringWidth(string2) / 2, y, i5);
		}

		public virtual void drawString(FontRenderer fontRenderer1, string string2, int i3, int i4, int i5)
		{
			fontRenderer1.drawStringWithShadow(string2, i3, i4, i5);
		}

		public virtual void drawTexturedModalRect(int x, int y, int x1, int y1, int width, int height)
		{
			float f7 = 0.00390625F;
			float f8 = 0.00390625F;
			Tessellator tessellator9 = Tessellator.instance;
			tessellator9.startDrawingQuads();
			tessellator9.AddVertexWithUV((double)(x + 0), (double)(y + height), (double)this.zLevel, (double)((float)(x1 + 0) * f7), (double)((float)(y1 + height) * f8));
			tessellator9.AddVertexWithUV((double)(x + width), (double)(y + height), (double)this.zLevel, (double)((float)(x1 + width) * f7), (double)((float)(y1 + height) * f8));
			tessellator9.AddVertexWithUV((double)(x + width), (double)(y + 0), (double)this.zLevel, (double)((float)(x1 + width) * f7), (double)((float)(y1 + 0) * f8));
			tessellator9.AddVertexWithUV((double)(x + 0), (double)(y + 0), (double)this.zLevel, (double)((float)(x1 + 0) * f7), (double)((float)(y1 + 0) * f8));
			tessellator9.DrawImmediate();
		}
	}

}