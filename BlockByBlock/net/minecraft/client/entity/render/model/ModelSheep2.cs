using net.minecraft.client.entity;

namespace net.minecraft.client.entity.render.model
{
    public class ModelSheep2 : ModelQuadruped
    {
        private float field_44017_o;

        public ModelSheep2() : base(12, 0.0F)
        {
            head = new ModelRenderer(this, 0, 0);
            head.addBox(-3.0F, -4.0F, -6.0F, 6, 6, 8, 0.0F);
            head.setRotationPoint(0.0F, 6.0F, -8.0F);
            body = new ModelRenderer(this, 28, 8);
            body.addBox(-4.0F, -10.0F, -7.0F, 8, 16, 6, 0.0F);
            body.setRotationPoint(0.0F, 5.0F, 2.0F);
        }

        public override void setLivingAnimations(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            base.setLivingAnimations(entityLiving1, f2, f3, f4);
            head.rotationPointY = 6.0F + ((EntitySheep)entityLiving1).func_44003_c(f4) * 9.0F;
            field_44017_o = ((EntitySheep)entityLiving1).func_44002_d(f4);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
            head.rotateAngleX = field_44017_o;
        }
    }

}