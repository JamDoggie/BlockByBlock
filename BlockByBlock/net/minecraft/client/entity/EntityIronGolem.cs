using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityIronGolem : EntityGolem
    {
        private int field_48119_b = 0;
        internal Village villageObj = null;
        private int field_48120_c;
        private int field_48118_d;

        public EntityIronGolem(World world1) : base(world1)
        {
            texture = "/mob/villager_golem.png";
            SetSize(1.4F, 2.9F);
            Navigator.func_48664_a(true);
            tasks.addTask(1, new EntityAIAttackOnCollide(this, 0.25F, true));
            tasks.addTask(2, new EntityAIMoveTowardsTarget(this, 0.22F, 32.0F));
            tasks.addTask(3, new EntityAIMoveThroughVillage(this, 0.16F, true));
            tasks.addTask(4, new EntityAIMoveTwardsRestriction(this, 0.16F));
            tasks.addTask(5, new EntityAILookAtVillager(this));
            tasks.addTask(6, new EntityAIWander(this, 0.16F));
            tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
            tasks.addTask(8, new EntityAILookIdle(this));
            targetTasks.addTask(1, new EntityAIDefendVillage(this));
            targetTasks.addTask(2, new EntityAIHurtByTarget(this, false));
            targetTasks.addTask(3, new EntityAINearestAttackableTarget(this, typeof(EntityMob), 16.0F, 0, false, true));
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, (sbyte)0);
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
        }

        protected internal override void updateAITick()
        {
            if (--field_48119_b <= 0)
            {
                field_48119_b = 70 + rand.Next(50);
                villageObj = worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ), 32);
                if (villageObj == null)
                {
                    detachHome();
                }
                else
                {
                    ChunkCoordinates chunkCoordinates1 = villageObj.Center;
                    setHomeArea(chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ, villageObj.VillageRadius);
                }
            }

            base.updateAITick();
        }

        public override int MaxHealth
        {
            get
            {
                return 100;
            }
        }

        protected internal override int decreaseAirSupply(int i1)
        {
            return i1;
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
            if (field_48120_c > 0)
            {
                --field_48120_c;
            }

            if (field_48118_d > 0)
            {
                --field_48118_d;
            }

            if (motionX * motionX + motionZ * motionZ > 2.500000277905201E-7D && rand.Next(5) == 0)
            {
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(posY - (double)0.2F - yOffset);
                int i3 = MathHelper.floor_double(posZ);
                int i4 = worldObj.getBlockId(i1, i2, i3);
                if (i4 > 0)
                {
                    worldObj.spawnParticle("tilecrack_" + i4, posX + ((double)rand.NextSingle() - 0.5D) * width, boundingBox.minY + 0.1D, posZ + ((double)rand.NextSingle() - 0.5D) * width, 4.0D * ((double)rand.NextSingle() - 0.5D), 0.5D, ((double)rand.NextSingle() - 0.5D) * 4.0D);
                }
            }

        }

        public override bool func_48100_a(Type class1)
        {
            return func_48112_E_() && class1.IsAssignableFrom(typeof(EntityPlayer)) ? false : base.func_48100_a(class1);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setBoolean("PlayerCreated", func_48112_E_());
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            func_48115_b(nBTTagCompound1.getBoolean("PlayerCreated"));
        }

        public override bool attackEntityAsMob(Entity entity1)
        {
            field_48120_c = 10;
            worldObj.setEntityState(this, 4);
            bool z2 = entity1.attackEntityFrom(DamageSource.causeMobDamage(this), 7 + rand.Next(15));
            if (z2)
            {
                entity1.motionY += 0.4F;
            }

            worldObj.playSoundAtEntity(this, "mob.irongolem.throw", 1.0F, 1.0F);
            return z2;
        }

        public override void handleHealthUpdate(sbyte b1)
        {
            if (b1 == 4)
            {
                field_48120_c = 10;
                worldObj.playSoundAtEntity(this, "mob.irongolem.throw", 1.0F, 1.0F);
            }
            else if (b1 == 11)
            {
                field_48118_d = 400;
            }
            else
            {
                base.handleHealthUpdate(b1);
            }

        }

        public virtual Village Village
        {
            get
            {
                return villageObj;
            }
        }

        public virtual int func_48114_ab()
        {
            return field_48120_c;
        }

        public virtual void func_48116_a(bool z1)
        {
            field_48118_d = z1 ? 400 : 0;
            worldObj.setEntityState(this, 11);
        }

        protected internal override string LivingSound
        {
            get
            {
                return "none";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.irongolem.hit";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.irongolem.death";
            }
        }

        protected internal override void playStepSound(int i1, int i2, int i3, int i4)
        {
            worldObj.playSoundAtEntity(this, "mob.irongolem.walk", 1.0F, 1.0F);
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(3);

            int i4;
            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Block.plantRed.blockID, 1);
            }

            i4 = 3 + rand.Next(3);

            for (int i5 = 0; i5 < i4; ++i5)
            {
                dropItem(Item.ingotIron.shiftedIndex, 1);
            }

        }

        public virtual int func_48117_D_()
        {
            return field_48118_d;
        }

        public virtual bool func_48112_E_()
        {
            return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
        }

        public virtual void func_48115_b(bool z1)
        {
            sbyte b2 = dataWatcher.getWatchableObjectByte(16);
            if (z1)
            {
                dataWatcher.updateObject(16, (sbyte)(b2 | 1));
            }
            else
            {
                dataWatcher.updateObject(16, (sbyte)(b2 & -2));
            }

        }
    }

}