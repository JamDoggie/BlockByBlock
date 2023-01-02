using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityFootStepFX : ParticleEffect
    {
        private int field_27018_a = 0;
        private int field_27020_o = 0;
        private TextureManager currentFootSteps;

        public EntityFootStepFX(TextureManager renderEngine1, World world2, double d3, double d5, double d7) : base(world2, d3, d5, d7, 0.0D, 0.0D, 0.0D)
        {
            currentFootSteps = renderEngine1;
            motionX = motionY = motionZ = 0.0D;
            field_27020_o = 200;
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (field_27018_a + f2) / field_27020_o;
            f8 *= f8;
            float f9 = 2.0F - f8 * 2.0F;
            if (f9 > 1.0F)
            {
                f9 = 1.0F;
            }

            f9 *= 0.2F;
            Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
            float f10 = 0.125F;
            float f11 = (float)(posX - interpPosX);
            float f12 = (float)(posY - interpPosY);
            float f13 = (float)(posZ - interpPosZ);
            float f14 = worldObj.getLightBrightness(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
            currentFootSteps.bindTexture(currentFootSteps.getTexture("/misc/footprint.png"));
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            tessellator1.startDrawingQuads();
            tessellator1.setColorRGBA_F(f14, f14, f14, f9);
            tessellator1.AddVertexWithUV((double)(f11 - f10), (double)f12, (double)(f13 + f10), 0.0D, 1.0D);
            tessellator1.AddVertexWithUV((double)(f11 + f10), (double)f12, (double)(f13 + f10), 1.0D, 1.0D);
            tessellator1.AddVertexWithUV((double)(f11 + f10), (double)f12, (double)(f13 - f10), 1.0D, 0.0D);
            tessellator1.AddVertexWithUV((double)(f11 - f10), (double)f12, (double)(f13 - f10), 0.0D, 0.0D);
            tessellator1.DrawImmediate();
            GL.Disable(EnableCap.Blend);
            Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
        }

        public override void onUpdate()
        {
            ++field_27018_a;
            if (field_27018_a == field_27020_o)
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