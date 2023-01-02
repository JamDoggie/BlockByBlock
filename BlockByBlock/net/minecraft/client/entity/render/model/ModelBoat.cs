using net.minecraft.client.entity;

namespace net.minecraft.client.entity.render.model
{
    public class ModelBoat : ModelBase
    {
        public ModelRenderer[] boatSides = new ModelRenderer[5];

        public ModelBoat()
        {
            boatSides[0] = new ModelRenderer(this, 0, 8);
            boatSides[1] = new ModelRenderer(this, 0, 0);
            boatSides[2] = new ModelRenderer(this, 0, 0);
            boatSides[3] = new ModelRenderer(this, 0, 0);
            boatSides[4] = new ModelRenderer(this, 0, 0);
            sbyte b1 = 24;
            sbyte b2 = 6;
            sbyte b3 = 20;
            sbyte b4 = 4;
            boatSides[0].addBox(-b1 / 2, -b3 / 2 + 2, -3.0F, b1, b3 - 4, 4, 0.0F);
            boatSides[0].setRotationPoint(0.0F, b4, 0.0F);
            boatSides[1].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            boatSides[1].setRotationPoint(-b1 / 2 + 1, b4, 0.0F);
            boatSides[2].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            boatSides[2].setRotationPoint(b1 / 2 - 1, b4, 0.0F);
            boatSides[3].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            boatSides[3].setRotationPoint(0.0F, b4, -b3 / 2 + 1);
            boatSides[4].addBox(-b1 / 2 + 2, -b2 - 1, -1.0F, b1 - 4, b2, 2, 0.0F);
            boatSides[4].setRotationPoint(0.0F, b4, b3 / 2 - 1);
            boatSides[0].rotateAngleX = (float)Math.PI / 2F;
            boatSides[1].rotateAngleY = 4.712389F;
            boatSides[2].rotateAngleY = (float)Math.PI / 2F;
            boatSides[3].rotateAngleY = (float)Math.PI;
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            for (int i8 = 0; i8 < 5; ++i8)
            {
                boatSides[i8].render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
        }
    }

}