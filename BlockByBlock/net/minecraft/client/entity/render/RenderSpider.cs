using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    public class RenderSpider : RenderLiving
    {
        public RenderSpider() : base(new ModelSpider(), 1.0F)
        {
            RenderPassModel = new ModelSpider();
        }

        protected internal virtual float setSpiderDeathMaxRotation(EntitySpider entitySpider1)
        {
            return 180.0F;
        }

        protected internal virtual int setSpiderEyeBrightness(EntitySpider entitySpider1, int i2, float f3)
        {
            if (i2 != 0)
            {
                return -1;
            }
            else
            {
                loadTexture("/mob/spider_eyes.png");
                float f4 = 1.0F;
                GL.Enable(EnableCap.Blend);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
                int i5 = 61680;
                int i6 = i5 % 65536;
                int i7 = i5 / 65536;
                LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, i6 / 1.0F, i7 / 1.0F);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, f4);
                return 1;
            }
        }

        protected internal virtual void scaleSpider(EntitySpider entitySpider1, float f2)
        {
            float f3 = entitySpider1.spiderScaleAmount();
            Minecraft.renderPipeline.ModelMatrix.Scale(f3, f3, f3);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            scaleSpider((EntitySpider)entityLiving1, f2);
        }

        protected internal override float getDeathMaxRotation(EntityLiving entityLiving1)
        {
            return setSpiderDeathMaxRotation((EntitySpider)entityLiving1);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return setSpiderEyeBrightness((EntitySpider)entityLiving1, i2, f3);
        }
    }

}