using BlockByBlock.net.minecraft.client.entity.render.model;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public abstract class Renderer
    {
        protected internal RenderManager renderManager;
        private ModelBase modelBase = new ModelBiped();
        protected internal RenderBlocks renderBlocks = new RenderBlocks();
        protected internal float shadowSize = 0.0F;
        protected internal float shadowOpaque = 1.0F;

        public abstract void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9);

        protected internal virtual void loadTexture(string string1)
        {
            TextureManager renderEngine2 = renderManager.renderEngine;
            renderEngine2.bindTexture(renderEngine2.getTexture(string1));
        }

        protected internal virtual bool loadDownloadableImageTexture(string string1, string string2)
        {
            TextureManager renderEngine3 = renderManager.renderEngine;
            int i4 = renderEngine3.getTextureForDownloadableImage(string1, string2);
            if (i4 >= 0)
            {
                renderEngine3.bindTexture(i4);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void renderEntityOnFire(Entity entity1, double d2, double d4, double d6, float f8)
        {
            Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
            int i9 = Block.fire.blockIndexInTexture;
            int i10 = (i9 & 15) << 4;
            int i11 = i9 & 240;
            float f12 = i10 / 256.0F;
            float f13 = (i10 + 15.99F) / 256.0F;
            float f14 = i11 / 256.0F;
            float f15 = (i11 + 15.99F) / 256.0F;
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            float f16 = entity1.width * 1.4F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f16, f16, f16);
            loadTexture("/terrain.png");
            Tessellator tessellator17 = Tessellator.instance;
            float f18 = 0.5F;
            float f19 = 0.0F;
            float f20 = entity1.height / f16;
            float f21 = (float)(entity1.posY - entity1.boundingBox.minY);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -0.3F + (int)f20 * 0.02F);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            float f22 = 0.0F;
            int i23 = 0;
            tessellator17.startDrawingQuads();

            while (f20 > 0.0F)
            {
                if (i23 % 2 == 0)
                {
                    f12 = i10 / 256.0F;
                    f13 = (i10 + 15.99F) / 256.0F;
                    f14 = i11 / 256.0F;
                    f15 = (i11 + 15.99F) / 256.0F;
                }
                else
                {
                    f12 = i10 / 256.0F;
                    f13 = (i10 + 15.99F) / 256.0F;
                    f14 = (i11 + 16) / 256.0F;
                    f15 = (i11 + 16 + 15.99F) / 256.0F;
                }

                if (i23 / 2 % 2 == 0)
                {
                    float f24 = f13;
                    f13 = f12;
                    f12 = f24;
                }

                tessellator17.AddVertexWithUV((double)(f18 - f19), (double)(0.0F - f21), (double)f22, (double)f13, (double)f15);
                tessellator17.AddVertexWithUV((double)(-f18 - f19), (double)(0.0F - f21), (double)f22, (double)f12, (double)f15);
                tessellator17.AddVertexWithUV((double)(-f18 - f19), (double)(1.4F - f21), (double)f22, (double)f12, (double)f14);
                tessellator17.AddVertexWithUV((double)(f18 - f19), (double)(1.4F - f21), (double)f22, (double)f13, (double)f14);
                f20 -= 0.45F;
                f21 -= 0.45F;
                f18 *= 0.9F;
                f22 += 0.03F;
                ++i23;
            }

            tessellator17.DrawImmediate();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
        }

        private void renderShadow(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            TextureManager renderEngine10 = renderManager.renderEngine;
            renderEngine10.bindTexture(renderEngine10.getTexture("%clamp%/misc/shadow.png"));
            World world11 = WorldFromRenderManager;
            GL.DepthMask(false);
            float f12 = shadowSize;
            if (entity1 is EntityLiving)
            {
                EntityLiving entityLiving13 = (EntityLiving)entity1;
                f12 *= entityLiving13.RenderSizeModifier;
                if (entityLiving13 is EntityAnimal)
                {
                    EntityAnimal entityAnimal14 = (EntityAnimal)entityLiving13;
                    if (entityAnimal14.Child)
                    {
                        f12 *= 0.5F;
                    }
                }
            }

            double d36 = entity1.lastTickPosX + (entity1.posX - entity1.lastTickPosX) * (double)f9;
            double d15 = entity1.lastTickPosY + (entity1.posY - entity1.lastTickPosY) * (double)f9 + (double)entity1.ShadowSize;
            double d17 = entity1.lastTickPosZ + (entity1.posZ - entity1.lastTickPosZ) * (double)f9;
            int i19 = MathHelper.floor_double(d36 - (double)f12);
            int i20 = MathHelper.floor_double(d36 + (double)f12);
            int i21 = MathHelper.floor_double(d15 - (double)f12);
            int i22 = MathHelper.floor_double(d15);
            int i23 = MathHelper.floor_double(d17 - (double)f12);
            int i24 = MathHelper.floor_double(d17 + (double)f12);
            double d25 = d2 - d36;
            double d27 = d4 - d15;
            double d29 = d6 - d17;
            Tessellator tessellator31 = Tessellator.instance;
            tessellator31.startDrawingQuads();

            for (int i32 = i19; i32 <= i20; ++i32)
            {
                for (int i33 = i21; i33 <= i22; ++i33)
                {
                    for (int i34 = i23; i34 <= i24; ++i34)
                    {
                        int i35 = world11.getBlockId(i32, i33 - 1, i34);
                        if (i35 > 0 && world11.getBlockLightValue(i32, i33, i34) > 3)
                        {
                            renderShadowOnBlock(Block.blocksList[i35], d2, d4 + (double)entity1.ShadowSize, d6, i32, i33, i34, f8, f12, d25, d27 + (double)entity1.ShadowSize, d29);
                        }
                    }
                }
            }

            tessellator31.DrawImmediate();
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            GL.Disable(EnableCap.Blend);
            GL.DepthMask(true);
        }

        private World WorldFromRenderManager
        {
            get
            {
                return renderManager.worldObj;
            }
        }

        private void renderShadowOnBlock(Block block1, double d2, double d4, double d6, int i8, int i9, int i10, float f11, float f12, double d13, double d15, double d17)
        {
            Tessellator tessellator19 = Tessellator.instance;
            if (block1.renderAsNormalBlock())
            {
                double d20 = ((double)f11 - (d4 - (i9 + d15)) / 2.0D) * 0.5D * (double)WorldFromRenderManager.getLightBrightness(i8, i9, i10);
                if (d20 >= 0.0D)
                {
                    if (d20 > 1.0D)
                    {
                        d20 = 1.0D;
                    }

                    tessellator19.setColorRGBA_F(1.0F, 1.0F, 1.0F, (float)d20);
                    double d22 = i8 + block1.minX + d13;
                    double d24 = i8 + block1.maxX + d13;
                    double d26 = i9 + block1.minY + d15 + 0.015625D;
                    double d28 = i10 + block1.minZ + d17;
                    double d30 = i10 + block1.maxZ + d17;
                    float f32 = (float)((d2 - d22) / 2.0D / (double)f12 + 0.5D);
                    float f33 = (float)((d2 - d24) / 2.0D / (double)f12 + 0.5D);
                    float f34 = (float)((d6 - d28) / 2.0D / (double)f12 + 0.5D);
                    float f35 = (float)((d6 - d30) / 2.0D / (double)f12 + 0.5D);
                    tessellator19.AddVertexWithUV(d22, d26, d28, (double)f32, (double)f34);
                    tessellator19.AddVertexWithUV(d22, d26, d30, (double)f32, (double)f35);
                    tessellator19.AddVertexWithUV(d24, d26, d30, (double)f33, (double)f35);
                    tessellator19.AddVertexWithUV(d24, d26, d28, (double)f33, (double)f34);
                }
            }
        }

        public static void renderOffsetAABB(AxisAlignedBB axisAlignedBB0, double d1, double d3, double d5)
        {
            Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
            Tessellator tessellator7 = Tessellator.instance;
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            tessellator7.startDrawingQuads();
            tessellator7.setTranslation(d1, d3, d5);
            tessellator7.SetNormal(0.0F, 0.0F, -1.0F);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator7.SetNormal(0.0F, 0.0F, 1.0F);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator7.SetNormal(0.0F, -1.0F, 0.0F);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator7.SetNormal(0.0F, 1.0F, 0.0F);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator7.SetNormal(-1.0F, 0.0F, 0.0F);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator7.SetNormal(1.0F, 0.0F, 0.0F);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator7.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator7.setTranslation(0.0D, 0.0D, 0.0D);
            tessellator7.DrawImmediate();
            Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
        }

        public static void RenderAABBImmediate(AxisAlignedBB axisAlignedBB0)
        {
            Tessellator tessellator1 = Tessellator.instance;
            tessellator1.startDrawingQuads();
            SetupAABBVerts(axisAlignedBB0);
            tessellator1.DrawImmediate();
        }

        public static VertexBuffer BuildAABBVBO(AxisAlignedBB bb)
        {
            Tessellator tessellator = Tessellator.instance;
            tessellator.StartBuildingVBO();
            SetupAABBVerts(bb);
            return tessellator.BuildCurrentVBO();
        }

        public static void SetupAABBVerts(AxisAlignedBB axisAlignedBB0)
        {
            Tessellator tessellator1 = Tessellator.instance;
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.minX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.minZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.maxY, axisAlignedBB0.maxZ);
            tessellator1.AddVertex(axisAlignedBB0.maxX, axisAlignedBB0.minY, axisAlignedBB0.maxZ);
        }

        public virtual RenderManager RenderManager
        {
            set
            {
                renderManager = value;
            }
        }

        public virtual void doRenderShadowAndFire(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            if (renderManager.options.fancyGraphics && shadowSize > 0.0F)
            {
                double d10 = renderManager.getDistanceToCamera(entity1.posX, entity1.posY, entity1.posZ);
                float f12 = (float)((1.0D - d10 / 256.0D) * shadowOpaque);
                if (f12 > 0.0F)
                {
                    renderShadow(entity1, d2, d4, d6, f12, f9);
                }
            }

            if (entity1.Burning)
            {
                renderEntityOnFire(entity1, d2, d4, d6, f9);
            }

        }

        public virtual FontRenderer FontRendererFromRenderManager
        {
            get
            {
                return renderManager.FontRenderer;
            }
        }
    }

}