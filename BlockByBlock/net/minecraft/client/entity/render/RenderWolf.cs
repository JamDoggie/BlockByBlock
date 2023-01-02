using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;

namespace net.minecraft.client.entity.render
{
    public class RenderWolf : RenderLiving
    {
        public RenderWolf(ModelBase modelBase1, float f2) : base(modelBase1, f2)
        {
        }

        public virtual void renderWolf(EntityWolf entityWolf1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityWolf1, d2, d4, d6, f8, f9);
        }

        protected internal virtual float getTailRotation(EntityWolf entityWolf1, float f2)
        {
            return entityWolf1.TailRotation;
        }

        protected internal virtual void func_25006_b(EntityWolf entityWolf1, float f2)
        {
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            func_25006_b((EntityWolf)entityLiving1, f2);
        }

        protected internal override float handleRotationFloat(EntityLiving entityLiving1, float f2)
        {
            return getTailRotation((EntityWolf)entityLiving1, f2);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderWolf((EntityWolf)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderWolf((EntityWolf)entity1, d2, d4, d6, f8, f9);
        }
    }

}