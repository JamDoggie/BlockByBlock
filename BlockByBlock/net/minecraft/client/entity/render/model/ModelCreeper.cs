using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{

    //			██████████████████████████
    //			█▓▓▓▒▒▒▓▓▓░░░▒▒▒░░░▓▓▓▒▒▒█
    //			█▓▓▓▒▒▒▓▓▓░░░▒▒▒░░░▓▓▓▒▒▒█
    //			█░░░▓▓▓▒▒▒▓▓▓░░░▒▒▒░░░▓▓▓█
    //			█░░░▓▓▓▒▒▒▓▓▓░░░▒▒▒░░░▓▓▓█
    //			█▓▓▓██████▒▒▒▓▓▓██████░░░█
    //			█▓▓▓██████▒▒▒▓▓▓██████░░░█
    //			█▒▒▒██████▓▓▓░░░██████▓▓▓█
    //			█▒▒▒██████▓▓▓░░░██████▓▓▓█
    //			█▓▓▓▒▒▒░░░██████▓▓▓▒▒▒░░░█
    //			█▓▓▓▒▒▒░░░██████▓▓▓▒▒▒░░░█
    //			█▒▒▒░░░████████████░░░▓▓▓█
    //			█▒▒▒░░░████████████░░░▓▓▓█
    //			█░░░▓▓▓████████████▒▒▒░░░█
    //			█░░░▓▓▓████████████▒▒▒░░░█
    //			█▒▒▒░░░███░░░▒▒▒███▓▓▓▒▒▒█
    //			█▒▒▒░░░███░░░▒▒▒███▓▓▓▒▒▒█
    //			█▓▓▓▒▒▒▓▓▓▒▒▒░░░▓▓▓▒▒▒░░░█
    //			█▓▓▓▒▒▒▓▓▓▒▒▒░░░▓▓▓▒▒▒░░░█
    //			██████████████████████████

    public class ModelCreeper : ModelBase
    {
        public ModelRenderer head;
        public ModelRenderer field_1270_b;
        public ModelRenderer body;
        public ModelRenderer leg1;
        public ModelRenderer leg2;
        public ModelRenderer leg3;
        public ModelRenderer leg4;

        public ModelCreeper() : this(0.0F)
        {
        }

        public ModelCreeper(float f1)
        {
            sbyte b2 = 4;
            head = new ModelRenderer(this, 0, 0);
            head.addBox(-4.0F, -8.0F, -4.0F, 8, 8, 8, f1);
            head.setRotationPoint(0.0F, b2, 0.0F);
            field_1270_b = new ModelRenderer(this, 32, 0);
            field_1270_b.addBox(-4.0F, -8.0F, -4.0F, 8, 8, 8, f1 + 0.5F);
            field_1270_b.setRotationPoint(0.0F, b2, 0.0F);
            body = new ModelRenderer(this, 16, 16);
            body.addBox(-4.0F, 0.0F, -2.0F, 8, 12, 4, f1);
            body.setRotationPoint(0.0F, b2, 0.0F);
            leg1 = new ModelRenderer(this, 0, 16);
            leg1.addBox(-2.0F, 0.0F, -2.0F, 4, 6, 4, f1);
            leg1.setRotationPoint(-2.0F, 12 + b2, 4.0F);
            leg2 = new ModelRenderer(this, 0, 16);
            leg2.addBox(-2.0F, 0.0F, -2.0F, 4, 6, 4, f1);
            leg2.setRotationPoint(2.0F, 12 + b2, 4.0F);
            leg3 = new ModelRenderer(this, 0, 16);
            leg3.addBox(-2.0F, 0.0F, -2.0F, 4, 6, 4, f1);
            leg3.setRotationPoint(-2.0F, 12 + b2, -4.0F);
            leg4 = new ModelRenderer(this, 0, 16);
            leg4.addBox(-2.0F, 0.0F, -2.0F, 4, 6, 4, f1);
            leg4.setRotationPoint(2.0F, 12 + b2, -4.0F);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            head.render(f7);
            body.render(f7);
            leg1.render(f7);
            leg2.render(f7);
            leg3.render(f7);
            leg4.render(f7);
        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            head.rotateAngleY = f4 / 57.295776F;
            head.rotateAngleX = f5 / 57.295776F;
            leg1.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.4F * f2;
            leg2.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.4F * f2;
            leg3.rotateAngleX = MathHelper.cos(f1 * 0.6662F + (float)Math.PI) * 1.4F * f2;
            leg4.rotateAngleX = MathHelper.cos(f1 * 0.6662F) * 1.4F * f2;
        }
    }

}