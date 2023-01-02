using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;

namespace net.minecraft.client.entity.render
{
    public class RenderBlaze : RenderLiving
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            field_40278_c = ((ModelBlaze)mainModel).func_40321_a();
        }

        private int field_40278_c;

        public RenderBlaze() : base(new ModelBlaze(), 0.5F)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
        }

        public virtual void renderBlaze(EntityBlaze entityBlaze1, double d2, double d4, double d6, float f8, float f9)
        {
            int i10 = ((ModelBlaze)mainModel).func_40321_a();
            if (i10 != field_40278_c)
            {
                field_40278_c = i10;
                mainModel = new ModelBlaze();
            }

            base.doRenderLiving(entityBlaze1, d2, d4, d6, f8, f9);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderBlaze((EntityBlaze)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderBlaze((EntityBlaze)entity1, d2, d4, d6, f8, f9);
        }
    }

}