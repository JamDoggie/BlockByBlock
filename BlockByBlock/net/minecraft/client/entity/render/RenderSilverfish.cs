using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;

namespace net.minecraft.client.entity.render
{
    public class RenderSilverfish : RenderLiving
    {
        public RenderSilverfish() : base(new ModelSilverfish(), 0.3F)
        {
        }

        protected internal virtual float getSilverfishDeathRotation(EntitySilverfish entitySilverfish1)
        {
            return 180.0F;
        }

        public virtual void renderSilverfish(EntitySilverfish entitySilverfish1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entitySilverfish1, d2, d4, d6, f8, f9);
        }

        protected internal virtual int shouldSilverfishRenderPass(EntitySilverfish entitySilverfish1, int i2, float f3)
        {
            return -1;
        }

        protected internal override float getDeathMaxRotation(EntityLiving entityLiving1)
        {
            return getSilverfishDeathRotation((EntitySilverfish)entityLiving1);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return shouldSilverfishRenderPass((EntitySilverfish)entityLiving1, i2, f3);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderSilverfish((EntitySilverfish)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderSilverfish((EntitySilverfish)entity1, d2, d4, d6, f8, f9);
        }
    }

}