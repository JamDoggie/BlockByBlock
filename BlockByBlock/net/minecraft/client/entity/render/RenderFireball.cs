using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderFireball : Renderer
    {
        private float field_40269_a;

        public RenderFireball(float f1)
        {
            field_40269_a = f1;
        }

        public virtual void doRenderFireball(EntityFireball entityFireball1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            float f10 = field_40269_a;
            Minecraft.renderPipeline.ModelMatrix.Scale(f10 / 1.0F, f10 / 1.0F, f10 / 1.0F);
            sbyte b11 = 46;
            loadTexture("/gui/items.png");
            Tessellator tessellator12 = Tessellator.instance;
            float f13 = (b11 % 16 * 16 + 0) / 256.0F;
            float f14 = (b11 % 16 * 16 + 16) / 256.0F;
            float f15 = (b11 / 16 * 16 + 0) / 256.0F;
            float f16 = (b11 / 16 * 16 + 16) / 256.0F;
            float f17 = 1.0F;
            float f18 = 0.5F;
            float f19 = 0.25F;
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
            tessellator12.startDrawingQuads();
            tessellator12.SetNormal(0.0F, 1.0F, 0.0F);
            tessellator12.AddVertexWithUV((double)(0.0F - f18), (double)(0.0F - f19), 0.0D, (double)f13, (double)f16);
            tessellator12.AddVertexWithUV((double)(f17 - f18), (double)(0.0F - f19), 0.0D, (double)f14, (double)f16);
            tessellator12.AddVertexWithUV((double)(f17 - f18), (double)(1.0F - f19), 0.0D, (double)f14, (double)f15);
            tessellator12.AddVertexWithUV((double)(0.0F - f18), (double)(1.0F - f19), 0.0D, (double)f13, (double)f15);
            tessellator12.DrawImmediate();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            doRenderFireball((EntityFireball)entity1, d2, d4, d6, f8, f9);
        }
    }

}