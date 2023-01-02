using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace BlockByBlock.net.minecraft.client.entity.particle
{
    public class EntityPickupFX : ParticleEffect
    {
        private Entity entityToPickUp;
        private Entity entityPickingUp;
        private int age = 0;
        private int maxAge = 0;
        private float yOffs;

        public EntityPickupFX(World world1, Entity entity2, Entity entity3, float f4) : base(world1, entity2.posX, entity2.posY, entity2.posZ, entity2.motionX, entity2.motionY, entity2.motionZ)
        {
            entityToPickUp = entity2;
            entityPickingUp = entity3;
            maxAge = 3;
            yOffs = f4;
        }

        public override void renderParticle(Tessellator tessellator1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            float f8 = (age + f2) / maxAge;
            f8 *= f8;
            double d9 = entityToPickUp.posX;
            double d11 = entityToPickUp.posY;
            double d13 = entityToPickUp.posZ;
            double d15 = entityPickingUp.lastTickPosX + (entityPickingUp.posX - entityPickingUp.lastTickPosX) * (double)f2;
            double d17 = entityPickingUp.lastTickPosY + (entityPickingUp.posY - entityPickingUp.lastTickPosY) * (double)f2 + yOffs;
            double d19 = entityPickingUp.lastTickPosZ + (entityPickingUp.posZ - entityPickingUp.lastTickPosZ) * (double)f2;
            double d21 = d9 + (d15 - d9) * (double)f8;
            double d23 = d11 + (d17 - d11) * (double)f8;
            double d25 = d13 + (d19 - d13) * (double)f8;
            int i27 = MathHelper.floor_double(d21);
            int i28 = MathHelper.floor_double(d23 + (double)(yOffset / 2.0F));
            int i29 = MathHelper.floor_double(d25);
            int i30 = getBrightnessForRender(f2);
            int i31 = i30 % 65536;
            int i32 = i30 / 65536;
            LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, i31 / 1.0F, i32 / 1.0F);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            d21 -= interpPosX;
            d23 -= interpPosY;
            d25 -= interpPosZ;
            RenderManager.instance.renderEntityWithPosYaw(entityToPickUp, (double)(float)d21, (double)(float)d23, (double)(float)d25, entityToPickUp.rotationYaw, f2);
        }

        public override void onUpdate()
        {
            ++age;
            if (age == maxAge)
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