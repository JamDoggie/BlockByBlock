using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntitySlime : EntityLiving, IMob
    {
        public float field_40139_a;
        public float field_768_a;
        public float field_767_b;
        private int slimeJumpDelay = 0;

        public EntitySlime(World world1) : base(world1)
        {
            texture = "/mob/slime.png";
            int i2 = 1 << rand.Next(3);
            yOffset = 0.0F;
            slimeJumpDelay = rand.Next(20) + 10;
            SlimeSize = i2;
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new sbyte?(1));
        }

        public virtual int SlimeSize
        {
            set
            {
                dataWatcher.updateObject(16, new sbyte?((sbyte)value));
                SetSize(0.6F * value, 0.6F * value);
                SetPosition(posX, posY, posZ);
                EntityHealth = MaxHealth;
                experienceValue = value;
            }
            get
            {
                return dataWatcher.getWatchableObjectByte(16);
            }
        }

        public override int MaxHealth
        {
            get
            {
                int i1 = SlimeSize;
                return i1 * i1;
            }
        }


        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setInteger("Size", SlimeSize - 1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            SlimeSize = nBTTagCompound1.getInteger("Size") + 1;
        }

        protected internal virtual string SlimeParticle
        {
            get
            {
                return "slime";
            }
        }

        protected internal virtual string func_40138_aj()
        {
            return "mob.slime";
        }

        public override void onUpdate()
        {
            if (!worldObj.isRemote && worldObj.difficultySetting == 0 && SlimeSize > 0)
            {
                isDead = true;
            }

            field_768_a += (field_40139_a - field_768_a) * 0.5F;
            field_767_b = field_768_a;
            bool z1 = onGround;
            base.onUpdate();
            if (onGround && !z1)
            {
                int i2 = SlimeSize;

                for (int i3 = 0; i3 < i2 * 8; ++i3)
                {
                    float f4 = rand.NextSingle() * (float)Math.PI * 2.0F;
                    float f5 = rand.NextSingle() * 0.5F + 0.5F;
                    float f6 = MathHelper.sin(f4) * i2 * 0.5F * f5;
                    float f7 = MathHelper.cos(f4) * i2 * 0.5F * f5;
                    worldObj.spawnParticle(SlimeParticle, posX + (double)f6, boundingBox.minY, posZ + (double)f7, 0.0D, 0.0D, 0.0D);
                }

                if (func_40134_ak())
                {
                    worldObj.playSoundAtEntity(this, func_40138_aj(), SoundVolume, ((rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F) / 0.8F);
                }

                field_40139_a = -0.5F;
            }

            func_40136_ag();
        }

        public override void updateEntityActionState()
        {
            despawnEntity();
            EntityPlayer entityPlayer1 = worldObj.getClosestVulnerablePlayerToEntity(this, 16.0D);
            if (entityPlayer1 != null)
            {
                faceEntity(entityPlayer1, 10.0F, 20.0F);
            }

            if (onGround && slimeJumpDelay-- <= 0)
            {
                slimeJumpDelay = func_40131_af();
                if (entityPlayer1 != null)
                {
                    slimeJumpDelay /= 3;
                }

                isJumping = true;
                if (func_40133_ao())
                {
                    worldObj.playSoundAtEntity(this, func_40138_aj(), SoundVolume, ((rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F) * 0.8F);
                }

                field_40139_a = 1.0F;
                moveStrafing = 1.0F - rand.NextSingle() * 2.0F;
                moveForward = 1 * SlimeSize;
            }
            else
            {
                isJumping = false;
                if (onGround)
                {
                    moveStrafing = moveForward = 0.0F;
                }
            }

        }

        protected internal virtual void func_40136_ag()
        {
            field_40139_a *= 0.6F;
        }

        protected internal virtual int func_40131_af()
        {
            return rand.Next(20) + 10;
        }

        protected internal virtual EntitySlime createInstance()
        {
            return new EntitySlime(worldObj);
        }

        public override void setDead()
        {
            int i1 = SlimeSize;
            if (!worldObj.isRemote && i1 > 1 && Health <= 0)
            {
                int i2 = 2 + rand.Next(3);

                for (int i3 = 0; i3 < i2; ++i3)
                {
                    float f4 = (i3 % 2 - 0.5F) * i1 / 4.0F;
                    float f5 = (i3 / 2 - 0.5F) * i1 / 4.0F;
                    EntitySlime entitySlime6 = createInstance();
                    entitySlime6.SlimeSize = i1 / 2;
                    entitySlime6.setLocationAndAngles(posX + (double)f4, posY + 0.5D, posZ + (double)f5, rand.NextSingle() * 360.0F, 0.0F);
                    worldObj.spawnEntityInWorld(entitySlime6);
                }
            }

            base.setDead();
        }

        public override void onCollideWithPlayer(EntityPlayer entityPlayer1)
        {
            if (func_40137_ah())
            {
                int i2 = SlimeSize;
                if (canEntityBeSeen(entityPlayer1) && (double)getDistanceToEntity(entityPlayer1) < 0.6D * i2 && entityPlayer1.attackEntityFrom(DamageSource.causeMobDamage(this), func_40130_ai()))
                {
                    worldObj.playSoundAtEntity(this, "mob.slimeattack", 1.0F, (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F);
                }
            }

        }

        protected internal virtual bool func_40137_ah()
        {
            return SlimeSize > 1;
        }

        protected internal virtual int func_40130_ai()
        {
            return SlimeSize;
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.slime";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.slime";
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return SlimeSize == 1 ? Item.slimeBall.shiftedIndex : 0;
            }
        }

        public override bool CanSpawnHere
        {
            get
            {
                Chunk chunk1 = worldObj.getChunkFromBlockCoords(MathHelper.floor_double(posX), MathHelper.floor_double(posZ));
                return (SlimeSize == 1 || worldObj.difficultySetting > 0) && rand.Next(10) == 0 && chunk1.getRandomWithSeed(987234911L).Next(10) == 0 && posY < 40.0D ? base.CanSpawnHere : false;
            }
        }

        protected internal override float SoundVolume
        {
            get
            {
                return 0.4F * SlimeSize;
            }
        }

        public override int VerticalFaceSpeed
        {
            get
            {
                return 0;
            }
        }

        protected internal virtual bool func_40133_ao()
        {
            return SlimeSize > 1;
        }

        protected internal virtual bool func_40134_ak()
        {
            return SlimeSize > 2;
        }
    }

}