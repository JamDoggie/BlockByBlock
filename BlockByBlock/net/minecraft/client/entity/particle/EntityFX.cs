using net.minecraft.client.entity;
using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class ParticleEffect : Entity
    {
        private int particleTextureIndex;
        protected internal float particleTextureJitterX;
        protected internal float particleTextureJitterY;
        protected internal int particleAge = 0;
        protected internal int particleMaxAge = 0;
        protected internal float particleScale;
        protected internal float particleGravity;
        protected internal float particleRed;
        protected internal float particleGreen;
        protected internal float particleBlue;
        public static double interpPosX;
        public static double interpPosY;
        public static double interpPosZ;

        public ParticleEffect(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1)
        {
            SetSize(0.2F, 0.2F);
            yOffset = height / 2.0F;
            SetPosition(d2, d4, d6);
            particleRed = particleGreen = particleBlue = 1.0F;
            motionX = d8 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.4F);
            motionY = d10 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.4F);
            motionZ = d12 + (double)((float)(portinghelpers.MathHelper.NextDouble * 2.0D - 1.0D) * 0.4F);
            float f14 = (float)(portinghelpers.MathHelper.NextDouble + portinghelpers.MathHelper.NextDouble + 1.0D) * 0.15F;
            float f15 = MathHelper.sqrt_double(motionX * motionX + motionY * motionY + motionZ * motionZ);
            motionX = motionX / (double)f15 * (double)f14 * (double)0.4F;
            motionY = motionY / (double)f15 * (double)f14 * (double)0.4F + (double)0.1F;
            motionZ = motionZ / (double)f15 * (double)f14 * (double)0.4F;
            particleTextureJitterX = rand.NextSingle() * 3.0F;
            particleTextureJitterY = rand.NextSingle() * 3.0F;
            particleScale = (rand.NextSingle() * 0.5F + 0.5F) * 2.0F;
            particleMaxAge = (int)(4.0F / (rand.NextSingle() * 0.9F + 0.1F));
            particleAge = 0;
        }

        public virtual ParticleEffect MultiplyVelocity(float num)
        {
            motionX *= num;
            motionY = (motionY - (double)0.1F) * (double)num + (double)0.1F;
            motionZ *= num;
            return this;
        }

        public virtual ParticleEffect Scale(float scalar)
        {
            SetSize(0.2F * scalar, 0.2F * scalar);
            particleScale *= scalar;
            return this;
        }

        public virtual void SetColor(float f1, float f2, float f3)
        {
            particleRed = f1;
            particleGreen = f2;
            particleBlue = f3;
        }

        public virtual float GetParticleColorR()
        {
            return particleRed;
        }

        public virtual float GetParticleColorG()
        {
            return particleGreen;
        }

        public virtual float GetParticleColorB()
        {
            return particleBlue;
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        protected internal override void entityInit()
        {
        }

        public override void onUpdate()
        {
            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            if (particleAge++ >= particleMaxAge)
            {
                setDead();
            }

            motionY -= 0.04D * particleGravity;
            moveEntity(motionX, motionY, motionZ);
            motionX *= 0.98F;
            motionY *= 0.98F;
            motionZ *= 0.98F;
            if (onGround)
            {
                motionX *= 0.7F;
                motionZ *= 0.7F;
            }

        }

        public virtual void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = particleTextureIndex % 16 / 16.0F;
            float f9 = f8 + 0.0624375F;
            float f10 = particleTextureIndex / 16 / 16.0F;
            float f11 = f10 + 0.0624375F;
            float f12 = 0.1F * particleScale;
            float f13 = (float)(prevPosX + (posX - prevPosX) * (double)f2 - interpPosX);
            float f14 = (float)(prevPosY + (posY - prevPosY) * (double)f2 - interpPosY);
            float f15 = (float)(prevPosZ + (posZ - prevPosZ) * (double)f2 - interpPosZ);
            float f16 = 1.0F;
            tessellator1.setColorOpaque_F(particleRed * f16, particleGreen * f16, particleBlue * f16);
            tessellator1.AddVertexWithUV((double)(f13 - f3 * f12 - f6 * f12), (double)(f14 - f4 * f12), (double)(f15 - f5 * f12 - f7 * f12), (double)f9, (double)f11);
            tessellator1.AddVertexWithUV((double)(f13 - f3 * f12 + f6 * f12), (double)(f14 + f4 * f12), (double)(f15 - f5 * f12 + f7 * f12), (double)f9, (double)f10);
            tessellator1.AddVertexWithUV((double)(f13 + f3 * f12 + f6 * f12), (double)(f14 + f4 * f12), (double)(f15 + f5 * f12 + f7 * f12), (double)f8, (double)f10);
            tessellator1.AddVertexWithUV((double)(f13 + f3 * f12 - f6 * f12), (double)(f14 - f4 * f12), (double)(f15 + f5 * f12 - f7 * f12), (double)f8, (double)f11);
        }

        public virtual int FXLayer
        {
            get
            {
                return 0;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public virtual int ParticleTextureIndex
        {
            set
            {
                particleTextureIndex = value;
            }
            get
            {
                return particleTextureIndex;
            }
        }


        public override bool canAttackWithItem()
        {
            return false;
        }
    }

}