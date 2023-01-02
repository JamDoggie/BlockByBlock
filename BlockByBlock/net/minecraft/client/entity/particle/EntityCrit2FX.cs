using net.minecraft.client.entity;
using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityCrit2FX : ParticleEffect
    {
        private Entity field_35134_a;
        private int currentLife;
        private int maximumLife;
        private string particleName;

        public EntityCrit2FX(World world1, Entity entity2) : this(world1, entity2, "crit")
        {
        }

        public EntityCrit2FX(World world1, Entity entity2, string string3) : base(world1, entity2.posX, entity2.boundingBox.minY + (double)(entity2.height / 2.0F), entity2.posZ, entity2.motionX, entity2.motionY, entity2.motionZ)
        {
            currentLife = 0;
            maximumLife = 0;
            field_35134_a = entity2;
            maximumLife = 3;
            particleName = string3;
            onUpdate();
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
        }

        public override void onUpdate()
        {
            for (int i1 = 0; i1 < 16; ++i1)
            {
                double d2 = (double)(rand.NextSingle() * 2.0F - 1.0F);
                double d4 = (double)(rand.NextSingle() * 2.0F - 1.0F);
                double d6 = (double)(rand.NextSingle() * 2.0F - 1.0F);
                if (d2 * d2 + d4 * d4 + d6 * d6 <= 1.0D)
                {
                    double d8 = field_35134_a.posX + d2 * field_35134_a.width / 4.0D;
                    double d10 = field_35134_a.boundingBox.minY + (double)(field_35134_a.height / 2.0F) + d4 * field_35134_a.height / 4.0D;
                    double d12 = field_35134_a.posZ + d6 * field_35134_a.width / 4.0D;
                    worldObj.spawnParticle(particleName, d8, d10, d12, d2, d4 + 0.2D, d6);
                }
            }

            ++currentLife;
            if (currentLife >= maximumLife)
            {
                setDead();
            }

        }

        public override int FXLayer
        {
            get
            {
                return 3;
            }
        }
    }

}