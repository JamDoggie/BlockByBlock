using BlockByBlock.net.minecraft.client.entity.render.model;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderCreeper : RenderLiving
    {
        private ModelBase field_27008_a = new ModelCreeper(2.0F);

        public RenderCreeper() : base(new ModelCreeper(), 0.5F)
        {
        }

        protected internal virtual void updateCreeperScale(EntityCreeper entityCreeper1, float f2)
        {
            float f4 = entityCreeper1.setCreeperFlashTime(f2);
            float f5 = 1.0F + MathHelper.sin(f4 * 100.0F) * f4 * 0.01F;
            if (f4 < 0.0F)
            {
                f4 = 0.0F;
            }

            if (f4 > 1.0F)
            {
                f4 = 1.0F;
            }

            f4 *= f4;
            f4 *= f4;
            float f6 = (1.0F + f4 * 0.4F) * f5;
            float f7 = (1.0F + f4 * 0.1F) / f5;
            Minecraft.renderPipeline.ModelMatrix.Scale(f6, f7, f6);
        }

        protected internal virtual int updateCreeperColorMultiplier(EntityCreeper entityCreeper1, float f2, float f3)
        {
            float f5 = entityCreeper1.setCreeperFlashTime(f3);
            if ((int)(f5 * 10.0F) % 2 == 0)
            {
                return 0;
            }
            else
            {
                int i6 = (int)(f5 * 0.2F * 255.0F);
                if (i6 < 0)
                {
                    i6 = 0;
                }

                if (i6 > 255)
                {
                    i6 = 255;
                }

                short s7 = 255;
                short s8 = 255;
                short s9 = 255;
                return i6 << 24 | s7 << 16 | s8 << 8 | s9;
            }
        }

        protected internal virtual int func_27006_a(EntityCreeper entityCreeper1, int i2, float f3)
        {
            if (entityCreeper1.Powered)
            {
                if (i2 == 1)
                {
                    float f4 = entityCreeper1.ticksExisted + f3;
                    loadTexture("/armor/power.png");
                    Minecraft.renderPipeline.TextureMatrix.LoadIdentity();
                    float f5 = f4 * 0.01F;
                    float f6 = f4 * 0.01F;
                    Minecraft.renderPipeline.TextureMatrix.Translate(f5, f6, 0.0F);
                    RenderPassModel = field_27008_a;
                    GL.Enable(EnableCap.Blend);
                    float f7 = 0.5F;
                    Minecraft.renderPipeline.SetColor(f7, f7, f7, 1.0F);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
                    return 1;
                }

                if (i2 == 2)
                {
                    Minecraft.renderPipeline.TextureMatrix.LoadIdentity();
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                    GL.Disable(EnableCap.Blend);
                }
            }

            return -1;
        }

        protected internal virtual int func_27007_b(EntityCreeper entityCreeper1, int i2, float f3)
        {
            return -1;
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            updateCreeperScale((EntityCreeper)entityLiving1, f2);
        }

        protected internal override int getColorMultiplier(EntityLiving entityLiving1, float f2, float f3)
        {
            return updateCreeperColorMultiplier((EntityCreeper)entityLiving1, f2, f3);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return func_27006_a((EntityCreeper)entityLiving1, i2, f3);
        }

        protected internal override int inheritRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return func_27007_b((EntityCreeper)entityLiving1, i2, f3);
        }
    }

}