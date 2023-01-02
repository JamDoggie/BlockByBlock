using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityBreakingFX : ParticleEffect
    {
        public EntityBreakingFX(World world1, double d2, double d4, double d6, Item item8) : base(world1, d2, d4, d6, 0.0D, 0.0D, 0.0D)
        {
            ParticleTextureIndex = item8.getIconFromDamage(0);
            particleRed = particleGreen = particleBlue = 1.0F;
            particleGravity = Block.blockSnow.blockParticleGravity;
            particleScale /= 2.0F;
        }

        public EntityBreakingFX(World world1, double d2, double d4, double d6, double d8, double d10, double d12, Item item14) : this(world1, d2, d4, d6, item14)
        {
            motionX *= 0.1F;
            motionY *= 0.1F;
            motionZ *= 0.1F;
            motionX += d8;
            motionY += d10;
            motionZ += d12;
        }

        public override int FXLayer
        {
            get
            {
                return 2;
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