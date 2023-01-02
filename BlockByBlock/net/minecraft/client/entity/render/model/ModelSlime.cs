using net.minecraft.client.entity;

namespace net.minecraft.client.entity.render.model
{
    public class ModelSlime : ModelBase
    {
        internal ModelRenderer slimeBodies;
        internal ModelRenderer slimeRightEye;
        internal ModelRenderer slimeLeftEye;
        internal ModelRenderer slimeMouth;

        public ModelSlime(int i1)
        {
            slimeBodies = new ModelRenderer(this, 0, i1);
            slimeBodies.addBox(-4.0F, 16.0F, -4.0F, 8, 8, 8);
            if (i1 > 0)
            {
                slimeBodies = new ModelRenderer(this, 0, i1);
                slimeBodies.addBox(-3.0F, 17.0F, -3.0F, 6, 6, 6);
                slimeRightEye = new ModelRenderer(this, 32, 0);
                slimeRightEye.addBox(-3.25F, 18.0F, -3.5F, 2, 2, 2);
                slimeLeftEye = new ModelRenderer(this, 32, 4);
                slimeLeftEye.addBox(1.25F, 18.0F, -3.5F, 2, 2, 2);
                slimeMouth = new ModelRenderer(this, 32, 8);
                slimeMouth.addBox(0.0F, 21.0F, -3.5F, 1, 1, 1);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            slimeBodies.render(f7);
            if (slimeRightEye != null)
            {
                slimeRightEye.render(f7);
                slimeLeftEye.render(f7);
                slimeMouth.render(f7);
            }

        }
    }

}