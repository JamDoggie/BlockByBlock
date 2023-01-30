using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderArrow : Renderer
    {
        public virtual void renderArrow(EntityArrow entityArrow1, double d2, double d4, double d6, float f8, float f9)
        {
            loadTexture("/item/arrows.png");
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            Minecraft.renderPipeline.ModelMatrix.Rotate(entityArrow1.prevRotationYaw + (entityArrow1.rotationYaw - entityArrow1.prevRotationYaw) * f9 - 90.0F, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(entityArrow1.prevRotationPitch + (entityArrow1.rotationPitch - entityArrow1.prevRotationPitch) * f9, 0.0F, 0.0F, 1.0F);
            Tessellator tessellator10 = Tessellator.instance;
            sbyte b11 = 0;
            float f12 = 0.0F;
            float f13 = 0.5F;
            float f14 = (0 + b11 * 10) / 32.0F;
            float f15 = (5 + b11 * 10) / 32.0F;
            float f16 = 0.0F;
            float f17 = 0.15625F;
            float f18 = (5 + b11 * 10) / 32.0F;
            float f19 = (10 + b11 * 10) / 32.0F;
            float f20 = 0.05625F;
            
            float f21 = entityArrow1.arrowShake - f9;
            if (f21 > 0.0F)
            {
                float f22 = -MathHelper.sin(f21 * 3.0F) * f21;
                Minecraft.renderPipeline.ModelMatrix.Rotate(f22, 0.0F, 0.0F, 1.0F);
            }

            Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 1.0F, 0.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Scale(f20, f20, f20);
            Minecraft.renderPipeline.ModelMatrix.Translate(-4.0F, 0.0F, 0.0F);
            Minecraft.renderPipeline.SetNormal(f20, 0.0F, 0.0F);
            tessellator10.startDrawingQuads();
            tessellator10.AddVertexWithUV(-7.0D, -2.0D, -2.0D, (double)f16, (double)f18);
            tessellator10.AddVertexWithUV(-7.0D, -2.0D, 2.0D, (double)f17, (double)f18);
            tessellator10.AddVertexWithUV(-7.0D, 2.0D, 2.0D, (double)f17, (double)f19);
            tessellator10.AddVertexWithUV(-7.0D, 2.0D, -2.0D, (double)f16, (double)f19);
            tessellator10.DrawImmediate();
            Minecraft.renderPipeline.SetNormal(-f20, 0.0F, 0.0F);
            tessellator10.startDrawingQuads();
            tessellator10.AddVertexWithUV(-7.0D, 2.0D, -2.0D, (double)f16, (double)f18);
            tessellator10.AddVertexWithUV(-7.0D, 2.0D, 2.0D, (double)f17, (double)f18);
            tessellator10.AddVertexWithUV(-7.0D, -2.0D, 2.0D, (double)f17, (double)f19);
            tessellator10.AddVertexWithUV(-7.0D, -2.0D, -2.0D, (double)f16, (double)f19);
            tessellator10.DrawImmediate();

            for (int i23 = 0; i23 < 4; ++i23)
            {
                Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 1.0F, 0.0F, 0.0F);
                Minecraft.renderPipeline.SetNormal(0.0F, 0.0F, f20);
                tessellator10.startDrawingQuads();
                tessellator10.AddVertexWithUV(-8.0D, -2.0D, 0.0D, (double)f12, (double)f14);
                tessellator10.AddVertexWithUV(8.0D, -2.0D, 0.0D, (double)f13, (double)f14);
                tessellator10.AddVertexWithUV(8.0D, 2.0D, 0.0D, (double)f13, (double)f15);
                tessellator10.AddVertexWithUV(-8.0D, 2.0D, 0.0D, (double)f12, (double)f15);
                tessellator10.DrawImmediate();
            }
            
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderArrow((EntityArrow)entity1, d2, d4, d6, f8, f9);
        }
    }

}