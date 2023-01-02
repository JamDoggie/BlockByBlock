using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;

namespace net.minecraft.client.entity.render
{
    public class RenderCow : RenderLiving
    {
        public RenderCow(ModelBase modelBase1, float f2) : base(modelBase1, f2)
        {
        }

        public virtual void renderCow(EntityCow entityCow1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityCow1, d2, d4, d6, f8, f9);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderCow((EntityCow)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderCow((EntityCow)entity1, d2, d4, d6, f8, f9);
        }
    }

}