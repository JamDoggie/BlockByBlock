using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelSnowMan : ModelBase
    {
        public ModelRenderer field_40306_a;
        public ModelRenderer field_40304_b;
        public ModelRenderer field_40305_c;
        public ModelRenderer field_40302_d;
        public ModelRenderer field_40303_e;

        public ModelSnowMan()
        {
            float f1 = 4.0F;
            float f2 = 0.0F;
            field_40305_c = new ModelRenderer(this, 0, 0).setTextureSize(64, 64);
            field_40305_c.addBox(-4.0F, -8.0F, -4.0F, 8, 8, 8, f2 - 0.5F);
            field_40305_c.setRotationPoint(0.0F, 0.0F + f1, 0.0F);
            field_40302_d = new ModelRenderer(this, 32, 0).setTextureSize(64, 64);
            field_40302_d.addBox(-1.0F, 0.0F, -1.0F, 12, 2, 2, f2 - 0.5F);
            field_40302_d.setRotationPoint(0.0F, 0.0F + f1 + 9.0F - 7.0F, 0.0F);
            field_40303_e = new ModelRenderer(this, 32, 0).setTextureSize(64, 64);
            field_40303_e.addBox(-1.0F, 0.0F, -1.0F, 12, 2, 2, f2 - 0.5F);
            field_40303_e.setRotationPoint(0.0F, 0.0F + f1 + 9.0F - 7.0F, 0.0F);
            field_40306_a = new ModelRenderer(this, 0, 16).setTextureSize(64, 64);
            field_40306_a.addBox(-5.0F, -10.0F, -5.0F, 10, 10, 10, f2 - 0.5F);
            field_40306_a.setRotationPoint(0.0F, 0.0F + f1 + 9.0F, 0.0F);
            field_40304_b = new ModelRenderer(this, 0, 36).setTextureSize(64, 64);
            field_40304_b.addBox(-6.0F, -12.0F, -6.0F, 12, 12, 12, f2 - 0.5F);
            field_40304_b.setRotationPoint(0.0F, 0.0F + f1 + 20.0F, 0.0F);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            base.setRotationAngles(f1, f2, f3, f4, f5, f6);
            field_40305_c.rotateAngleY = f4 / 57.295776F;
            field_40305_c.rotateAngleX = f5 / 57.295776F;
            field_40306_a.rotateAngleY = f4 / 57.295776F * 0.25F;
            float f7 = MathHelper.sin(field_40306_a.rotateAngleY);
            float f8 = MathHelper.cos(field_40306_a.rotateAngleY);
            field_40302_d.rotateAngleZ = 1.0F;
            field_40303_e.rotateAngleZ = -1.0F;
            field_40302_d.rotateAngleY = 0.0F + field_40306_a.rotateAngleY;
            field_40303_e.rotateAngleY = (float)Math.PI + field_40306_a.rotateAngleY;
            field_40302_d.rotationPointX = f8 * 5.0F;
            field_40302_d.rotationPointZ = -f7 * 5.0F;
            field_40303_e.rotationPointX = -f8 * 5.0F;
            field_40303_e.rotationPointZ = f7 * 5.0F;
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            field_40306_a.render(f7);
            field_40304_b.render(f7);
            field_40305_c.render(f7);
            field_40302_d.render(f7);
            field_40303_e.render(f7);
        }
    }

}