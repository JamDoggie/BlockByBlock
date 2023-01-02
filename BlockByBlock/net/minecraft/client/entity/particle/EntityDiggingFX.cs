using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityDiggingFX : ParticleEffect
    {
        private Block blockInstance;

        public EntityDiggingFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12, Block block14, int i15, int i16) : base(world1, d2, d4, d6, d8, d10, d12)
        {
            blockInstance = block14;
            ParticleTextureIndex = block14.getBlockTextureFromSideAndMetadata(0, i16);
            particleGravity = block14.blockParticleGravity;
            particleRed = particleGreen = particleBlue = 0.6F;
            particleScale /= 2.0F;
        }

        public virtual EntityDiggingFX func_4041_a(int i1, int i2, int i3)
        {
            if (blockInstance == Block.grass)
            {
                return this;
            }
            else
            {
                int i4 = blockInstance.colorMultiplier(worldObj, i1, i2, i3);
                particleRed *= (i4 >> 16 & 255) / 255.0F;
                particleGreen *= (i4 >> 8 & 255) / 255.0F;
                particleBlue *= (i4 & 255) / 255.0F;
                return this;
            }
        }

        public override int FXLayer
        {
            get
            {
                return 1;
            }
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (ParticleTextureIndex % 16 + particleTextureJitterX / 4.0F) / 16.0F;
            float f9 = f8 + 0.015609375F;
            float f10 = (ParticleTextureIndex / 16 + particleTextureJitterY / 4.0F) / 16.0F;
            float f11 = f10 + 0.015609375F;
            float f12 = 0.1F * particleScale;
            float f13 = (float)(prevPosX + (posX - prevPosX) * (double)f2 - interpPosX);
            float f14 = (float)(prevPosY + (posY - prevPosY) * (double)f2 - interpPosY);
            float f15 = (float)(prevPosZ + (posZ - prevPosZ) * (double)f2 - interpPosZ);
            float f16 = 1.0F;
            tessellator1.setColorOpaque_F(f16 * particleRed, f16 * particleGreen, f16 * particleBlue);
            tessellator1.AddVertexWithUV((double)(f13 - f3 * f12 - f6 * f12), (double)(f14 - f4 * f12), (double)(f15 - f5 * f12 - f7 * f12), (double)f8, (double)f11);
            tessellator1.AddVertexWithUV((double)(f13 - f3 * f12 + f6 * f12), (double)(f14 + f4 * f12), (double)(f15 - f5 * f12 + f7 * f12), (double)f8, (double)f10);
            tessellator1.AddVertexWithUV((double)(f13 + f3 * f12 + f6 * f12), (double)(f14 + f4 * f12), (double)(f15 + f5 * f12 + f7 * f12), (double)f9, (double)f10);
            tessellator1.AddVertexWithUV((double)(f13 + f3 * f12 - f6 * f12), (double)(f14 - f4 * f12), (double)(f15 + f5 * f12 - f7 * f12), (double)f9, (double)f11);
        }
    }

}