using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;

namespace net.minecraft.client.entity.render
{
    public class RenderChicken : RenderLiving
    {
        public RenderChicken(ModelBase modelBase1, float f2) : base(modelBase1, f2)
        {
        }

        public virtual void renderChicken(EntityChicken entityChicken1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityChicken1, d2, d4, d6, f8, f9);
        }

        protected internal virtual float getWingRotation(EntityChicken entityChicken1, float f2)
        {
            float f3 = entityChicken1.field_756_e + (entityChicken1.field_752_b - entityChicken1.field_756_e) * f2;
            float f4 = entityChicken1.field_757_d + (entityChicken1.destPos - entityChicken1.field_757_d) * f2;
            return (MathHelper.sin(f3) + 1.0F) * f4;
        }

        protected internal override float handleRotationFloat(EntityLiving entityLiving1, float f2)
        {
            return getWingRotation((EntityChicken)entityLiving1, f2);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderChicken((EntityChicken)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderChicken((EntityChicken)entity1, d2, d4, d6, f8, f9);
        }
    }

}