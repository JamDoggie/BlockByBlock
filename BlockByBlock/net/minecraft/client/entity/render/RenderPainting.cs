using System;
using BlockByBlock.java_extensions;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderPainting : Renderer
    {
        private RandomExtended rand = new RandomExtended();

        public virtual void func_158_a(EntityPainting entityPainting1, double d2, double d4, double d6, float f8, float f9)
        {
            rand.SetSeed(187L);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            Minecraft.renderPipeline.ModelMatrix.Rotate(f8, 0.0F, 1.0F, 0.0F);

            loadTexture("/art/kz.png");
            EnumArt enumArt10 = entityPainting1.art;
            float f11 = 0.0625F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f11, f11, f11);
            func_159_a(entityPainting1, enumArt10.sizeX, enumArt10.sizeY, enumArt10.offsetX, enumArt10.offsetY);
            
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        private void func_159_a(EntityPainting entityPainting1, int i2, int i3, int i4, int i5)
        {
            float f6 = -i2 / 2.0F;
            float f7 = -i3 / 2.0F;
            float f8 = -0.5F;
            float f9 = 0.5F;

            for (int i10 = 0; i10 < i2 / 16; ++i10)
            {
                for (int i11 = 0; i11 < i3 / 16; ++i11)
                {
                    float f12 = f6 + (i10 + 1) * 16;
                    float f13 = f6 + i10 * 16;
                    float f14 = f7 + (i11 + 1) * 16;
                    float f15 = f7 + i11 * 16;
                    func_160_a(entityPainting1, (f12 + f13) / 2.0F, (f14 + f15) / 2.0F);
                    float f16 = (i4 + i2 - i10 * 16) / 256.0F;
                    float f17 = (i4 + i2 - (i10 + 1) * 16) / 256.0F;
                    float f18 = (i5 + i3 - i11 * 16) / 256.0F;
                    float f19 = (i5 + i3 - (i11 + 1) * 16) / 256.0F;
                    float f20 = 0.75F;
                    float f21 = 0.8125F;
                    float f22 = 0.0F;
                    float f23 = 0.0625F;
                    float f24 = 0.75F;
                    float f25 = 0.8125F;
                    float f26 = 0.001953125F;
                    float f27 = 0.001953125F;
                    float f28 = 0.7519531F;
                    float f29 = 0.7519531F;
                    float f30 = 0.0F;
                    float f31 = 0.0625F;
                    Tessellator tessellator32 = Tessellator.instance;
                    tessellator32.startDrawingQuads();
                    tessellator32.SetNormal(0.0F, 0.0F, -1.0F);
                    tessellator32.AddVertexWithUV((double)f12, (double)f15, (double)f8, (double)f17, (double)f18);
                    tessellator32.AddVertexWithUV((double)f13, (double)f15, (double)f8, (double)f16, (double)f18);
                    tessellator32.AddVertexWithUV((double)f13, (double)f14, (double)f8, (double)f16, (double)f19);
                    tessellator32.AddVertexWithUV((double)f12, (double)f14, (double)f8, (double)f17, (double)f19);
                    tessellator32.SetNormal(0.0F, 0.0F, 1.0F);
                    tessellator32.AddVertexWithUV((double)f12, (double)f14, (double)f9, (double)f20, (double)f22);
                    tessellator32.AddVertexWithUV((double)f13, (double)f14, (double)f9, (double)f21, (double)f22);
                    tessellator32.AddVertexWithUV((double)f13, (double)f15, (double)f9, (double)f21, (double)f23);
                    tessellator32.AddVertexWithUV((double)f12, (double)f15, (double)f9, (double)f20, (double)f23);
                    tessellator32.SetNormal(0.0F, 1.0F, 0.0F);
                    tessellator32.AddVertexWithUV((double)f12, (double)f14, (double)f8, (double)f24, (double)f26);
                    tessellator32.AddVertexWithUV((double)f13, (double)f14, (double)f8, (double)f25, (double)f26);
                    tessellator32.AddVertexWithUV((double)f13, (double)f14, (double)f9, (double)f25, (double)f27);
                    tessellator32.AddVertexWithUV((double)f12, (double)f14, (double)f9, (double)f24, (double)f27);
                    tessellator32.SetNormal(0.0F, -1.0F, 0.0F);
                    tessellator32.AddVertexWithUV((double)f12, (double)f15, (double)f9, (double)f24, (double)f26);
                    tessellator32.AddVertexWithUV((double)f13, (double)f15, (double)f9, (double)f25, (double)f26);
                    tessellator32.AddVertexWithUV((double)f13, (double)f15, (double)f8, (double)f25, (double)f27);
                    tessellator32.AddVertexWithUV((double)f12, (double)f15, (double)f8, (double)f24, (double)f27);
                    tessellator32.SetNormal(-1.0F, 0.0F, 0.0F);
                    tessellator32.AddVertexWithUV((double)f12, (double)f14, (double)f9, (double)f29, (double)f30);
                    tessellator32.AddVertexWithUV((double)f12, (double)f15, (double)f9, (double)f29, (double)f31);
                    tessellator32.AddVertexWithUV((double)f12, (double)f15, (double)f8, (double)f28, (double)f31);
                    tessellator32.AddVertexWithUV((double)f12, (double)f14, (double)f8, (double)f28, (double)f30);
                    tessellator32.SetNormal(1.0F, 0.0F, 0.0F);
                    tessellator32.AddVertexWithUV((double)f13, (double)f14, (double)f8, (double)f29, (double)f30);
                    tessellator32.AddVertexWithUV((double)f13, (double)f15, (double)f8, (double)f29, (double)f31);
                    tessellator32.AddVertexWithUV((double)f13, (double)f15, (double)f9, (double)f28, (double)f31);
                    tessellator32.AddVertexWithUV((double)f13, (double)f14, (double)f9, (double)f28, (double)f30);
                    tessellator32.DrawImmediate();
                }
            }

        }

        private void func_160_a(EntityPainting entityPainting1, float f2, float f3)
        {
            int i4 = MathHelper.floor_double(entityPainting1.posX);
            int i5 = MathHelper.floor_double(entityPainting1.posY + (double)(f3 / 16.0F));
            int i6 = MathHelper.floor_double(entityPainting1.posZ);
            if (entityPainting1.direction == 0)
            {
                i4 = MathHelper.floor_double(entityPainting1.posX + (double)(f2 / 16.0F));
            }

            if (entityPainting1.direction == 1)
            {
                i6 = MathHelper.floor_double(entityPainting1.posZ - (double)(f2 / 16.0F));
            }

            if (entityPainting1.direction == 2)
            {
                i4 = MathHelper.floor_double(entityPainting1.posX - (double)(f2 / 16.0F));
            }

            if (entityPainting1.direction == 3)
            {
                i6 = MathHelper.floor_double(entityPainting1.posZ + (double)(f2 / 16.0F));
            }

            int i7 = renderManager.worldObj.GetLightBrightnessForSkyBlocks(i4, i5, i6, 0);
            int i8 = i7 % 65536;
            int i9 = i7 / 65536;
            LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, i8, i9);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_158_a((EntityPainting)entity1, d2, d4, d6, f8, f9);
        }
    }

}