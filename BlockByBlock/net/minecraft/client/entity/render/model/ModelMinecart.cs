using net.minecraft.client.entity;

namespace net.minecraft.client.entity.render.model
{
    public class ModelMinecart : ModelBase
    {
        public ModelRenderer[] sideModels = new ModelRenderer[7];

        public ModelMinecart()
        {
            sideModels[0] = new ModelRenderer(this, 0, 10);
            sideModels[1] = new ModelRenderer(this, 0, 0);
            sideModels[2] = new ModelRenderer(this, 0, 0);
            sideModels[3] = new ModelRenderer(this, 0, 0);
            sideModels[4] = new ModelRenderer(this, 0, 0);
            sideModels[5] = new ModelRenderer(this, 44, 10);
            sbyte b1 = 20;
            sbyte b2 = 8;
            sbyte b3 = 16;
            sbyte b4 = 4;
            sideModels[0].addBox(-b1 / 2, -b3 / 2, -1.0F, b1, b3, 2, 0.0F);
            sideModels[0].setRotationPoint(0.0F, b4, 0.0F);
            sideModels[5].addBox(-b1 / 2 + 1, -b3 / 2 + 1, -1.0F, b1 - 2, b3 - 2, 1, 0.0F);
            sideModels[5].setRotationPoint(0.0F, b4, 0.0F);
            sideModels[1].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            sideModels[1].setRotationPoint(-b1 / 2 + 1, b4, 0.0F);
            sideModels[2].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            sideModels[2].setRotationPoint(b1 / 2 - 1, b4, 0.0F);
            sideModels[3].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            sideModels[3].setRotationPoint(0.0F, b4, -b3 / 2 + 1);
            sideModels[4].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            sideModels[4].setRotationPoint(0.0F, b4, b3 / 2 - 1);
            sideModels[0].rotateAngleX = (float)Math.PI / 2F;
            sideModels[1].rotateAngleY = 4.712389F;
            sideModels[2].rotateAngleY = (float)Math.PI / 2F;
            sideModels[3].rotateAngleY = (float)Math.PI;
            sideModels[5].rotateAngleX = -1.5707964F;
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            sideModels[5].rotationPointY = 4.0F - f4;

            for (int i8 = 0; i8 < 6; ++i8)
            {
                sideModels[i8].render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
        }
    }

}