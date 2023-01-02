using System;
using net.minecraft.client.entity;
using net.minecraft.src;

namespace net.minecraft.client.entity.render.model
{
    public class ModelSilverfish : ModelBase
    {
        private ModelRenderer[] silverfishBodyParts = new ModelRenderer[7];
        private ModelRenderer[] silverfishWings;
        private float[] field_35399_c = new float[7];
        private static readonly int[][] silverfishBoxLength = new int[][]
        {
            new int[] {3, 2, 2},
            new int[] {4, 3, 2},
            new int[] {6, 4, 3},
            new int[] {3, 3, 3},
            new int[] {2, 2, 3},
            new int[] {2, 1, 2},
            new int[] {1, 1, 2}
        };
        private static readonly int[][] silverfishTexturePositions = new int[][]
        {
            new int[] {0, 0},
            new int[] {0, 4},
            new int[] {0, 9},
            new int[] {0, 16},
            new int[] {0, 22},
            new int[] {11, 0},
            new int[] {13, 4}
        };

        public ModelSilverfish()
        {
            float f1 = -3.5F;

            for (int i2 = 0; i2 < silverfishBodyParts.Length; ++i2)
            {
                silverfishBodyParts[i2] = new ModelRenderer(this, silverfishTexturePositions[i2][0], silverfishTexturePositions[i2][1]);
                silverfishBodyParts[i2].addBox(silverfishBoxLength[i2][0] * -0.5F, 0.0F, silverfishBoxLength[i2][2] * -0.5F, silverfishBoxLength[i2][0], silverfishBoxLength[i2][1], silverfishBoxLength[i2][2]);
                silverfishBodyParts[i2].setRotationPoint(0.0F, 24 - silverfishBoxLength[i2][1], f1);
                field_35399_c[i2] = f1;
                if (i2 < silverfishBodyParts.Length - 1)
                {
                    f1 += (silverfishBoxLength[i2][2] + silverfishBoxLength[i2 + 1][2]) * 0.5F;
                }
            }

            silverfishWings = new ModelRenderer[3];
            silverfishWings[0] = new ModelRenderer(this, 20, 0);
            silverfishWings[0].addBox(-5.0F, 0.0F, silverfishBoxLength[2][2] * -0.5F, 10, 8, silverfishBoxLength[2][2]);
            silverfishWings[0].setRotationPoint(0.0F, 16.0F, field_35399_c[2]);
            silverfishWings[1] = new ModelRenderer(this, 20, 11);
            silverfishWings[1].addBox(-3.0F, 0.0F, silverfishBoxLength[4][2] * -0.5F, 6, 4, silverfishBoxLength[4][2]);
            silverfishWings[1].setRotationPoint(0.0F, 20.0F, field_35399_c[4]);
            silverfishWings[2] = new ModelRenderer(this, 20, 18);
            silverfishWings[2].addBox(-3.0F, 0.0F, silverfishBoxLength[4][2] * -0.5F, 6, 5, silverfishBoxLength[1][2]);
            silverfishWings[2].setRotationPoint(0.0F, 19.0F, field_35399_c[1]);
        }

        public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            setRotationAngles(f2, f3, f4, f5, f6, f7);

            int i8;
            for (i8 = 0; i8 < silverfishBodyParts.Length; ++i8)
            {
                silverfishBodyParts[i8].render(f7);
            }

            for (i8 = 0; i8 < silverfishWings.Length; ++i8)
            {
                silverfishWings[i8].render(f7);
            }

        }

        public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            for (int i7 = 0; i7 < silverfishBodyParts.Length; ++i7)
            {
                silverfishBodyParts[i7].rotateAngleY = MathHelper.cos(f3 * 0.9F + i7 * 0.15F * (float)Math.PI) * (float)Math.PI * 0.05F * (1 + Math.Abs(i7 - 2));
                silverfishBodyParts[i7].rotationPointX = MathHelper.sin(f3 * 0.9F + i7 * 0.15F * (float)Math.PI) * (float)Math.PI * 0.2F * Math.Abs(i7 - 2);
            }

            silverfishWings[0].rotateAngleY = silverfishBodyParts[2].rotateAngleY;
            silverfishWings[1].rotateAngleY = silverfishBodyParts[4].rotateAngleY;
            silverfishWings[1].rotationPointX = silverfishBodyParts[4].rotationPointX;
            silverfishWings[2].rotateAngleY = silverfishBodyParts[1].rotateAngleY;
            silverfishWings[2].rotationPointX = silverfishBodyParts[1].rotationPointX;
        }
    }

}