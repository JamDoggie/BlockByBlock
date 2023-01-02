using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityLightningBolt : EntityWeatherEffect
    {
        private int lightningState;
        public long boltVertex = 0L;
        private int boltLivingTime;

        public EntityLightningBolt(World world1, double d2, double d4, double d6) : base(world1)
        {
            setLocationAndAngles(d2, d4, d6, 0.0F, 0.0F);
            lightningState = 2;
            boltVertex = rand.NextInt64();
            boltLivingTime = rand.Next(3) + 1;
            if (world1.difficultySetting >= 2 && world1.doChunksNearChunkExist(MathHelper.floor_double(d2), MathHelper.floor_double(d4), MathHelper.floor_double(d6), 10))
            {
                int i8 = MathHelper.floor_double(d2);
                int i9 = MathHelper.floor_double(d4);
                int i10 = MathHelper.floor_double(d6);
                if (world1.getBlockId(i8, i9, i10) == 0 && Block.fire.canPlaceBlockAt(world1, i8, i9, i10))
                {
                    world1.setBlockWithNotify(i8, i9, i10, Block.fire.blockID);
                }

                for (i8 = 0; i8 < 4; ++i8)
                {
                    i9 = MathHelper.floor_double(d2) + rand.Next(3) - 1;
                    i10 = MathHelper.floor_double(d4) + rand.Next(3) - 1;
                    int i11 = MathHelper.floor_double(d6) + rand.Next(3) - 1;
                    if (world1.getBlockId(i9, i10, i11) == 0 && Block.fire.canPlaceBlockAt(world1, i9, i10, i11))
                    {
                        world1.setBlockWithNotify(i9, i10, i11, Block.fire.blockID);
                    }
                }
            }

        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (lightningState == 2)
            {
                worldObj.playSoundEffect(posX, posY, posZ, "ambient.weather.thunder", 10000.0F, 0.8F + rand.NextSingle() * 0.2F);
                worldObj.playSoundEffect(posX, posY, posZ, "random.explode", 2.0F, 0.5F + rand.NextSingle() * 0.2F);
            }

            --lightningState;
            if (lightningState < 0)
            {
                if (boltLivingTime == 0)
                {
                    setDead();
                }
                else if (lightningState < -rand.Next(10))
                {
                    --boltLivingTime;
                    lightningState = 1;
                    boltVertex = rand.NextInt64();
                    if (worldObj.doChunksNearChunkExist(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ), 10))
                    {
                        int i1 = MathHelper.floor_double(posX);
                        int i2 = MathHelper.floor_double(posY);
                        int i3 = MathHelper.floor_double(posZ);
                        if (worldObj.getBlockId(i1, i2, i3) == 0 && Block.fire.canPlaceBlockAt(worldObj, i1, i2, i3))
                        {
                            worldObj.setBlockWithNotify(i1, i2, i3, Block.fire.blockID);
                        }
                    }
                }
            }

            if (lightningState >= 0)
            {
                double d6 = 3.0D;
                System.Collections.IList list7 = worldObj.getEntitiesWithinAABBExcludingEntity(this, AxisAlignedBB.getBoundingBoxFromPool(posX - d6, posY - d6, posZ - d6, posX + d6, posY + 6.0D + d6, posZ + d6));

                for (int i4 = 0; i4 < list7.Count; ++i4)
                {
                    Entity entity5 = (Entity)list7[i4];
                    entity5.onStruckByLightning(this);
                }

                worldObj.lightningFlash = 2;
            }

        }

        protected internal override void entityInit()
        {
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
        }

        public override bool isInRangeToRenderVec3D(Vec3D vec3D1)
        {
            return lightningState >= 0;
        }
    }

}