using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderXPOrb : Renderer
    {
        private RenderBlocks field_35439_b = new RenderBlocks();
        public bool field_35440_a = true;

        public RenderXPOrb()
        {
            shadowSize = 0.15F;
            shadowOpaque = 0.75F;
        }

        public virtual void func_35438_a(EntityXPOrb entityXPOrb1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            int i10 = entityXPOrb1.TextureByXP;
            loadTexture("/item/xporb.png");
            Tessellator tessellator11 = Tessellator.instance;
            float f12 = (i10 % 4 * 16 + 0) / 64.0F;
            float f13 = (i10 % 4 * 16 + 16) / 64.0F;
            float f14 = (i10 / 4 * 16 + 0) / 64.0F;
            float f15 = (i10 / 4 * 16 + 16) / 64.0F;
            float f16 = 1.0F;
            float f17 = 0.5F;
            float f18 = 0.25F;
            int i19 = entityXPOrb1.getBrightnessForRender(f9);
            int i20 = i19 % 65536;
            int i21 = i19 / 65536;
            LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, i20 / 1.0F, i21 / 1.0F);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            float f26 = 255.0F;
            float f27 = (entityXPOrb1.xpColor + f9) / 2.0F;
            i21 = (int)((MathHelper.sin(f27 + 0.0F) + 1.0F) * 0.5F * f26);
            int i22 = (int)f26;
            int i23 = (int)((MathHelper.sin(f27 + 4.1887903F) + 1.0F) * 0.1F * f26);
            int i24 = i21 << 16 | i22 << 8 | i23;
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
            float f25 = 0.3F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f25, f25, f25);
            tessellator11.startDrawingQuads();
            tessellator11.setColorRGBA_I(i24, 128);
            tessellator11.SetNormal(0.0F, 1.0F, 0.0F);
            tessellator11.AddVertexWithUV((double)(0.0F - f17), (double)(0.0F - f18), 0.0D, (double)f12, (double)f15);
            tessellator11.AddVertexWithUV((double)(f16 - f17), (double)(0.0F - f18), 0.0D, (double)f13, (double)f15);
            tessellator11.AddVertexWithUV((double)(f16 - f17), (double)(1.0F - f18), 0.0D, (double)f13, (double)f14);
            tessellator11.AddVertexWithUV((double)(0.0F - f17), (double)(1.0F - f18), 0.0D, (double)f12, (double)f14);
            tessellator11.DrawImmediate();
            GL.Disable(EnableCap.Blend);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_35438_a((EntityXPOrb)entity1, d2, d4, d6, f8, f9);
        }
    }

}