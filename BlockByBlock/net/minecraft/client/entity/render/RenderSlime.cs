using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    public class RenderSlime : RenderLiving
    {
        private ModelBase scaleAmount;

        public RenderSlime(ModelBase modelBase1, ModelBase modelBase2, float f3) : base(modelBase1, f3)
        {
            scaleAmount = modelBase2;
        }

        protected internal virtual int func_40287_a(EntitySlime entitySlime1, int i2, float f3)
        {
            if (i2 == 0)
            {
                RenderPassModel = scaleAmount;
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                return 1;
            }
            else
            {
                if (i2 == 1)
                {
                    GL.Disable(EnableCap.Blend);
                    Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                }

                return -1;
            }
        }

        protected internal virtual void scaleSlime(EntitySlime entitySlime1, float f2)
        {
            int i3 = entitySlime1.SlimeSize;
            float f4 = (entitySlime1.field_767_b + (entitySlime1.field_768_a - entitySlime1.field_767_b) * f2) / (i3 * 0.5F + 1.0F);
            float f5 = 1.0F / (f4 + 1.0F);
            float f6 = i3;
            Minecraft.renderPipeline.ModelMatrix.Scale(f5 * f6, 1.0F / f5 * f6, f5 * f6);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            scaleSlime((EntitySlime)entityLiving1, f2);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return func_40287_a((EntitySlime)entityLiving1, i2, f3);
        }
    }

}