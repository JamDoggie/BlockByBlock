using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;

namespace net.minecraft.client.entity.render
{
    public class RenderPig : RenderLiving
    {
        public RenderPig(ModelBase modelBase1, ModelBase modelBase2, float f3) : base(modelBase1, f3)
        {
            RenderPassModel = modelBase2;
        }

        protected internal virtual int renderSaddledPig(EntityPig entityPig1, int i2, float f3)
        {
            loadTexture("/mob/saddle.png");
            return i2 == 0 && entityPig1.Saddled ? 1 : -1;
        }

        public virtual void func_40286_a(EntityPig entityPig1, double d2, double d4, double d6, float f8, float f9)
        {
            base.doRenderLiving(entityPig1, d2, d4, d6, f8, f9);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return renderSaddledPig((EntityPig)entityLiving1, i2, f3);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            func_40286_a((EntityPig)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            func_40286_a((EntityPig)entity1, d2, d4, d6, f8, f9);
        }
    }

}