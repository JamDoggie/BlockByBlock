using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelBlaze : ModelBase
    {
        private ModelRenderer[] field_40323_a = new ModelRenderer[12];
        private ModelRenderer field_40322_b;

        public ModelBlaze()
        {
            for (int i1 = 0; i1 < field_40323_a.Length; ++i1)
            {
                field_40323_a[i1] = new ModelRenderer(this, 0, 16);
                field_40323_a[i1].addBox(0.0F, 0.0F, 0.0F, 2, 8, 2);
            }

            field_40322_b = new ModelRenderer(this, 0, 0);
            field_40322_b.addBox(-4.0F, -4.0F, -4.0F, 8, 8, 8);
        }

        public virtual int func_40321_a()
        {
            return 8;
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            field_40322_b.render(f7);

            for (int i8 = 0; i8 < field_40323_a.Length; ++i8)
            {
                field_40323_a[i8].render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            float f7 = f3 * (float)Math.PI * -0.1F;

            int i8;
            for (i8 = 0; i8 < 4; ++i8)
            {
                field_40323_a[i8].rotationPointY = -2.0F + MathHelper.cos((i8 * 2 + f3) * 0.25F);
                field_40323_a[i8].rotationPointX = MathHelper.cos(f7) * 9.0F;
                field_40323_a[i8].rotationPointZ = MathHelper.sin(f7) * 9.0F;
                ++f7;
            }

            f7 = 0.7853982F + f3 * (float)Math.PI * 0.03F;

            for (i8 = 4; i8 < 8; ++i8)
            {
                field_40323_a[i8].rotationPointY = 2.0F + MathHelper.cos((i8 * 2 + f3) * 0.25F);
                field_40323_a[i8].rotationPointX = MathHelper.cos(f7) * 7.0F;
                field_40323_a[i8].rotationPointZ = MathHelper.sin(f7) * 7.0F;
                ++f7;
            }

            f7 = 0.47123894F + f3 * (float)Math.PI * -0.05F;

            for (i8 = 8; i8 < 12; ++i8)
            {
                field_40323_a[i8].rotationPointY = 11.0F + MathHelper.cos((i8 * 1.5F + f3) * 0.5F);
                field_40323_a[i8].rotationPointX = MathHelper.cos(f7) * 5.0F;
                field_40323_a[i8].rotationPointZ = MathHelper.sin(f7) * 5.0F;
                ++f7;
            }

            field_40322_b.rotateAngleY = f4 / 57.295776F;
            field_40322_b.rotateAngleX = f5 / 57.295776F;
        }
    }

}