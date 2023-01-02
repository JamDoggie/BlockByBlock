namespace net.minecraft.client.entity.render.model
{
    public class ModelSkeleton : ModelZombie
    {
        public ModelSkeleton()
        {
            float f1 = 0.0F;
            bipedRightArm = new ModelRenderer(this, 40, 16);
            bipedRightArm.addBox(-1.0F, -2.0F, -1.0F, 2, 12, 2, f1);
            bipedRightArm.setRotationPoint(-5.0F, 2.0F, 0.0F);
            bipedLeftArm = new ModelRenderer(this, 40, 16);
            bipedLeftArm.mirror = true;
            bipedLeftArm.addBox(-1.0F, -2.0F, -1.0F, 2, 12, 2, f1);
            bipedLeftArm.setRotationPoint(5.0F, 2.0F, 0.0F);
            bipedRightLeg = new ModelRenderer(this, 0, 16);
            bipedRightLeg.addBox(-1.0F, 0.0F, -1.0F, 2, 12, 2, f1);
            bipedRightLeg.setRotationPoint(-2.0F, 12.0F, 0.0F);
            bipedLeftLeg = new ModelRenderer(this, 0, 16);
            bipedLeftLeg.mirror = true;
            bipedLeftLeg.addBox(-1.0F, 0.0F, -1.0F, 2, 12, 2, f1);
            bipedLeftLeg.setRotationPoint(2.0F, 12.0F, 0.0F);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            aimedBow = true;
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
        }
    }

}