using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderSnowball : Renderer
    {
        private int itemIconIndex;

        public RenderSnowball(int i1)
        {
            itemIconIndex = i1;
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            
            Minecraft.renderPipeline.ModelMatrix.Scale(0.5F, 0.5F, 0.5F);
            loadTexture("/gui/items.png");
            Tessellator tessellator10 = Tessellator.instance;
            if (itemIconIndex == 154)
            {
                int i11 = PotionHelper.func_40358_a(((EntityPotion)entity1).PotionDamage, false);
                float f12 = (i11 >> 16 & 255) / 255.0F;
                float f13 = (i11 >> 8 & 255) / 255.0F;
                float f14 = (i11 & 255) / 255.0F;
                Minecraft.renderPipeline.SetColor(f12, f13, f14);
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                func_40265_a(tessellator10, 141);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F);
            }

            func_40265_a(tessellator10, itemIconIndex);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        private void func_40265_a(Tessellator tessellator1, int i2)
        {
            float f3 = (i2 % 16 * 16 + 0) / 256.0F;
            float f4 = (i2 % 16 * 16 + 16) / 256.0F;
            float f5 = (i2 / 16 * 16 + 0) / 256.0F;
            float f6 = (i2 / 16 * 16 + 16) / 256.0F;
            float f7 = 1.0F;
            float f8 = 0.5F;
            float f9 = 0.25F;
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
            tessellator1.startDrawingQuads();
            tessellator1.SetNormal(0.0F, 1.0F, 0.0F);
            tessellator1.AddVertexWithUV((double)(0.0F - f8), (double)(0.0F - f9), 0.0D, (double)f3, (double)f6);
            tessellator1.AddVertexWithUV((double)(f7 - f8), (double)(0.0F - f9), 0.0D, (double)f4, (double)f6);
            tessellator1.AddVertexWithUV((double)(f7 - f8), (double)(f7 - f9), 0.0D, (double)f4, (double)f5);
            tessellator1.AddVertexWithUV((double)(0.0F - f8), (double)(f7 - f9), 0.0D, (double)f3, (double)f5);
            tessellator1.DrawImmediate();
        }
    }

}