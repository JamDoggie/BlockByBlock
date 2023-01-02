using System;
using net.minecraft.client.entity;

namespace net.minecraft.client.entity.render.model
{
    public class ModelSquid : ModelBase
    {
        internal ModelRenderer squidBody;
        internal ModelRenderer[] squidTentacles = new ModelRenderer[8];

        public ModelSquid()
        {
            sbyte b1 = -16;
            squidBody = new ModelRenderer(this, 0, 0);
            squidBody.addBox(-6.0F, -8.0F, -6.0F, 12, 16, 12);
            squidBody.rotationPointY += 24 + b1;

            for (int i2 = 0; i2 < squidTentacles.Length; ++i2)
            {
                squidTentacles[i2] = new ModelRenderer(this, 48, 0);
                double d3 = i2 * Math.PI * 2.0D / squidTentacles.Length;
                float f5 = (float)Math.Cos(d3) * 5.0F;
                float f6 = (float)Math.Sin(d3) * 5.0F;
                squidTentacles[i2].addBox(-1.0F, 0.0F, -1.0F, 2, 18, 2);
                squidTentacles[i2].rotationPointX = f5;
                squidTentacles[i2].rotationPointZ = f6;
                squidTentacles[i2].rotationPointY = 31 + b1;
                d3 = i2 * Math.PI * -2.0D / squidTentacles.Length + Math.PI / 2D;
                squidTentacles[i2].rotateAngleY = (float)d3;
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            ModelRenderer[] modelRenderer7 = squidTentacles;
            int i8 = modelRenderer7.Length;

            for (int i9 = 0; i9 < i8; ++i9)
            {
                ModelRenderer modelRenderer10 = modelRenderer7[i9];
                modelRenderer10.rotateAngleX = f3;
            }

        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);
            squidBody.render(f7);

            for (int i8 = 0; i8 < squidTentacles.Length; ++i8)
            {
                squidTentacles[i8].render(f7);
            }

        }
    }

}