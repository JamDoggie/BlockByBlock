using BlockByBlock.net.minecraft.client.entity.render.model;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;
using System;

namespace net.minecraft.client.entity.render
{

    public class RenderMinecart : Renderer
    {
        protected internal ModelBase modelMinecart;

        public RenderMinecart()
        {
            shadowSize = 0.5F;
            modelMinecart = new ModelMinecart();
        }

        public virtual void Render(EntityMinecart entityMinecart1, double d2, double d4, double d6, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            long j10 = entityMinecart1.entityId * 493286711L;
            j10 = j10 * j10 * 4392167121L + j10 * 98761L;
            float f12 = (((j10 >> 16 & 7L) + 0.5F) / 8.0F - 0.5F) * 0.004F;
            float f13 = (((j10 >> 20 & 7L) + 0.5F) / 8.0F - 0.5F) * 0.004F;
            float f14 = (((j10 >> 24 & 7L) + 0.5F) / 8.0F - 0.5F) * 0.004F;
            Minecraft.renderPipeline.ModelMatrix.Translate(f12, f13, f14);
            double d15 = entityMinecart1.lastTickPosX + (entityMinecart1.posX - entityMinecart1.lastTickPosX) * (double)f9;
            double d17 = entityMinecart1.lastTickPosY + (entityMinecart1.posY - entityMinecart1.lastTickPosY) * (double)f9;
            double d19 = entityMinecart1.lastTickPosZ + (entityMinecart1.posZ - entityMinecart1.lastTickPosZ) * (double)f9;
            double d21 = (double)0.3F;
            Vec3D vec3D23 = entityMinecart1.func_514_g(d15, d17, d19);
            float f24 = entityMinecart1.prevRotationPitch + (entityMinecart1.rotationPitch - entityMinecart1.prevRotationPitch) * f9;
            if (vec3D23 != null)
            {
                Vec3D vec3D25 = entityMinecart1.func_515_a(d15, d17, d19, d21);
                Vec3D vec3D26 = entityMinecart1.func_515_a(d15, d17, d19, -d21);
                if (vec3D25 == null)
                {
                    vec3D25 = vec3D23;
                }

                if (vec3D26 == null)
                {
                    vec3D26 = vec3D23;
                }

                d2 += vec3D23.xCoord - d15;
                d4 += (vec3D25.yCoord + vec3D26.yCoord) / 2.0D - d17;
                d6 += vec3D23.zCoord - d19;
                Vec3D vec3D27 = vec3D26.addVector(-vec3D25.xCoord, -vec3D25.yCoord, -vec3D25.zCoord);
                if (vec3D27.lengthVector() != 0.0D)
                {
                    vec3D27 = vec3D27.normalize();
                    f8 = (float)(Math.Atan2(vec3D27.zCoord, vec3D27.xCoord) * 180.0D / Math.PI);
                    f24 = (float)(Math.Atan(vec3D27.yCoord) * 73.0D);
                }
            }

            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - f8, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-f24, 0.0F, 0.0F, 1.0F);
            float f28 = entityMinecart1.func_41023_l() - f9;
            float f29 = entityMinecart1.func_41025_i() - f9;
            if (f29 < 0.0F)
            {
                f29 = 0.0F;
            }

            if (f28 > 0.0F)
            {
                Minecraft.renderPipeline.ModelMatrix.Rotate(MathHelper.sin(f28) * f28 * f29 / 10.0F * entityMinecart1.func_41030_m(), 1.0F, 0.0F, 0.0F);
            }

            if (entityMinecart1.minecartType != 0)
            {
                loadTexture("/terrain.png");
                float f30 = 0.75F;
                Minecraft.renderPipeline.ModelMatrix.Scale(f30, f30, f30);
                if (entityMinecart1.minecartType == 1)
                {
                    Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, 0.0F, 0.5F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 1.0F, 0.0F);
                    new RenderBlocks().renderBlockAsItem(Block.chest, 0, entityMinecart1.getBrightness(f9));
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.0F, -0.5F);
                    Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                }
                else if (entityMinecart1.minecartType == 2)
                {
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.3125F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 1.0F, 0.0F);
                    new RenderBlocks().renderBlockAsItem(Block.stoneOvenIdle, 0, entityMinecart1.getBrightness(f9));
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.3125F, 0.0F);
                    Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                }

                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f30, 1.0F / f30, 1.0F / f30);
            }

            loadTexture("/item/cart.png");
            Minecraft.renderPipeline.ModelMatrix.Scale(-1.0F, -1.0F, 1.0F);
            modelMinecart.render(entityMinecart1, 0.0F, 0.0F, -0.1F, 0.0F, 0.0F, 0.0625F);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            Render((EntityMinecart)entity1, d2, d4, d6, f8, f9);
        }
    }

}