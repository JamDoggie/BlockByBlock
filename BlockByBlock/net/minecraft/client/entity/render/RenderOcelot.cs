using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderOcelot : RenderLiving
    {
        public RenderOcelot(ModelBase modelBase1, float f2) : base(modelBase1, f2)
        {
        }

        public virtual void func_48424_a(EntityOcelot entityOcelot1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityOcelot1, d2, d4, d6, f8, f9);
        }

        protected internal virtual void func_48423_a(EntityOcelot entityOcelot1, float f2)
        {
            base.preRenderCallback(entityOcelot1, f2);
            if (entityOcelot1.Tamed)
            {
                Minecraft.renderPipeline.ModelMatrix.Scale(0.8F, 0.8F, 0.8F);
            }

        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            func_48423_a((EntityOcelot)entityLiving1, f2);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            func_48424_a((EntityOcelot)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_48424_a((EntityOcelot)entity1, d2, d4, d6, f8, f9);
        }
    }

}