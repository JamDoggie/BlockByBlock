using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityLargeExplodeFX : ParticleEffect
    {
        private int field_35130_a = 0;
        private int field_35129_ay = 0;
        private TextureManager field_35128_az;
        private float field_35131_aA;

        public EntityLargeExplodeFX(TextureManager renderEngine1, World world2, double d3, double d5, double d7, double d9, double d11, double d13) : base(world2, d3, d5, d7, 0.0D, 0.0D, 0.0D)
        {
            field_35128_az = renderEngine1;
            field_35129_ay = 6 + rand.Next(4);
            particleRed = particleGreen = particleBlue = rand.NextSingle() * 0.6F + 0.4F;
            field_35131_aA = 1.0F - (float)d9 * 0.5F;
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            int i8 = (int)((field_35130_a + f2) * 15.0F / field_35129_ay);
            if (i8 <= 15)
            {
                field_35128_az.bindTexture(field_35128_az.getTexture("/misc/explosion.png"));
                float f9 = i8 % 4 / 4.0F;
                float f10 = f9 + 0.24975F;
                float f11 = i8 / 4 / 4.0F;
                float f12 = f11 + 0.24975F;
                float f13 = 2.0F * field_35131_aA;
                float f14 = (float)(prevPosX + (posX - prevPosX) * (double)f2 - interpPosX);
                float f15 = (float)(prevPosY + (posY - prevPosY) * (double)f2 - interpPosY);
                float f16 = (float)(prevPosZ + (posZ - prevPosZ) * (double)f2 - interpPosZ);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                GameLighting.DisableMeshLighting();
                tessellator1.startDrawingQuads();
                tessellator1.setColorRGBA_F(particleRed, particleGreen, particleBlue, 1.0F);
                tessellator1.SetNormal(0.0F, 1.0F, 0.0F);
                tessellator1.Brightness = 240;
                tessellator1.AddVertexWithUV((double)(f14 - f3 * f13 - f6 * f13), (double)(f15 - f4 * f13), (double)(f16 - f5 * f13 - f7 * f13), (double)f10, (double)f12);
                tessellator1.AddVertexWithUV((double)(f14 - f3 * f13 + f6 * f13), (double)(f15 + f4 * f13), (double)(f16 - f5 * f13 + f7 * f13), (double)f10, (double)f11);
                tessellator1.AddVertexWithUV((double)(f14 + f3 * f13 + f6 * f13), (double)(f15 + f4 * f13), (double)(f16 + f5 * f13 + f7 * f13), (double)f9, (double)f11);
                tessellator1.AddVertexWithUV((double)(f14 + f3 * f13 - f6 * f13), (double)(f15 - f4 * f13), (double)(f16 + f5 * f13 - f7 * f13), (double)f9, (double)f12);
                tessellator1.DrawImmediate();
                GL.PolygonOffset(0.0F, 0.0F);
                Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
            }
        }

        public override int getBrightnessForRender(float f1)
        {
            return 61680;
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            ++field_35130_a;
            if (field_35130_a == field_35129_ay)
            {
                setDead();
            }

        }

        public override int FXLayer
        {
            get
            {
                return 3;
            }
        }
    }

}