using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityWolf : EntityTameable
    {
        private bool looksWithInterest = false;
        private float field_25048_b;
        private float field_25054_c;
        private bool isShaking;
        private bool field_25052_g;
        private float timeWolfIsShaking;
        private float prevTimeWolfIsShaking;

        public EntityWolf(World world1) : base(world1)
        {
            texture = "/mob/wolf.png";
            SetSize(0.6F, 0.8F);
            moveSpeed = 0.3F;
            Navigator.func_48664_a(true);
            tasks.addTask(1, new EntityAISwimming(this));
            tasks.addTask(2, aiSit);
            tasks.addTask(3, new EntityAILeapAtTarget(this, 0.4F));
            tasks.addTask(4, new EntityAIAttackOnCollide(this, moveSpeed, true));
            tasks.addTask(5, new EntityAIFollowOwner(this, moveSpeed, 10.0F, 2.0F));
            tasks.addTask(6, new EntityAIMate(this, moveSpeed));
            tasks.addTask(7, new EntityAIWander(this, moveSpeed));
            tasks.addTask(8, new EntityAIBeg(this, 8.0F));
            tasks.addTask(9, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
            tasks.addTask(9, new EntityAILookIdle(this));
            targetTasks.addTask(1, new EntityAIOwnerHurtByTarget(this));
            targetTasks.addTask(2, new EntityAIOwnerHurtTarget(this));
            targetTasks.addTask(3, new EntityAIHurtByTarget(this, true));
            targetTasks.addTask(4, new EntityAITargetNonTamed(this, typeof(EntitySheep), 16.0F, 200, false));
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
        }

        public override EntityLiving AttackTarget
        {
            set
            {
                base.AttackTarget = value;
                if (value is EntityPlayer)
                {
                    Angry = true;
                }

            }
        }

        protected internal override void updateAITick()
        {
            dataWatcher.updateObject(18, Health);
        }

        public override int MaxHealth
        {
            get
            {
                return Tamed ? 20 : 8;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(18, new int?(Health));
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        public override string Texture
        {
            get
            {
                return Tamed ? "/mob/wolf_tame.png" : Angry ? "/mob/wolf_angry.png" : base.Texture;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setBoolean("Angry", Angry);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            Angry = nBTTagCompound1.getBoolean("Angry");
        }

        protected internal override bool canDespawn()
        {
            return Angry;
        }

        protected internal override string LivingSound
        {
            get
            {
                return Angry ? "mob.wolf.growl" : rand.Next(3) == 0 ? Tamed && dataWatcher.getWatchableObjectInt(18) < 10 ? "mob.wolf.whine" : "mob.wolf.panting" : "mob.wolf.bark";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.wolf.hurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.wolf.death";
            }
        }

        protected internal override float SoundVolume
        {
            get
            {
                return 0.4F;
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return -1;
            }
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
            if (!worldObj.isRemote && isShaking && !field_25052_g && !hasPath() && onGround)
            {
                field_25052_g = true;
                timeWolfIsShaking = 0.0F;
                prevTimeWolfIsShaking = 0.0F;
                worldObj.setEntityState(this, 8);
            }

        }

        public override void onUpdate()
        {
            base.onUpdate();
            field_25054_c = field_25048_b;
            if (looksWithInterest)
            {
                field_25048_b += (1.0F - field_25048_b) * 0.4F;
            }
            else
            {
                field_25048_b += (0.0F - field_25048_b) * 0.4F;
            }

            if (looksWithInterest)
            {
                numTicksToChaseTarget = 10;
            }

            if (Wet)
            {
                isShaking = true;
                field_25052_g = false;
                timeWolfIsShaking = 0.0F;
                prevTimeWolfIsShaking = 0.0F;
            }
            else if ((isShaking || field_25052_g) && field_25052_g)
            {
                if (timeWolfIsShaking == 0.0F)
                {
                    worldObj.playSoundAtEntity(this, "mob.wolf.shake", SoundVolume, (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F);
                }

                prevTimeWolfIsShaking = timeWolfIsShaking;
                timeWolfIsShaking += 0.05F;
                if (prevTimeWolfIsShaking >= 2.0F)
                {
                    isShaking = false;
                    field_25052_g = false;
                    prevTimeWolfIsShaking = 0.0F;
                    timeWolfIsShaking = 0.0F;
                }

                if (timeWolfIsShaking > 0.4F)
                {
                    float f1 = (float)boundingBox.minY;
                    int i2 = (int)(MathHelper.sin((timeWolfIsShaking - 0.4F) * (float)Math.PI) * 7.0F);

                    for (int i3 = 0; i3 < i2; ++i3)
                    {
                        float f4 = (rand.NextSingle() * 2.0F - 1.0F) * width * 0.5F;
                        float f5 = (rand.NextSingle() * 2.0F - 1.0F) * width * 0.5F;
                        worldObj.spawnParticle("splash", posX + (double)f4, (double)(f1 + 0.8F), posZ + (double)f5, motionX, motionY, motionZ);
                    }
                }
            }

        }

        public virtual bool WolfShaking
        {
            get
            {
                return isShaking;
            }
        }

        public virtual float getShadingWhileShaking(float f1)
        {
            return 0.75F + (prevTimeWolfIsShaking + (timeWolfIsShaking - prevTimeWolfIsShaking) * f1) / 2.0F * 0.25F;
        }

        public virtual float getShakeAngle(float f1, float f2)
        {
            float f3 = (prevTimeWolfIsShaking + (timeWolfIsShaking - prevTimeWolfIsShaking) * f1 + f2) / 1.8F;
            if (f3 < 0.0F)
            {
                f3 = 0.0F;
            }
            else if (f3 > 1.0F)
            {
                f3 = 1.0F;
            }

            return MathHelper.sin(f3 * (float)Math.PI) * MathHelper.sin(f3 * (float)Math.PI * 11.0F) * 0.15F * (float)Math.PI;
        }

        public virtual float getInterestedAngle(float f1)
        {
            return (field_25054_c + (field_25048_b - field_25054_c) * f1) * 0.15F * (float)Math.PI;
        }

        public override float EyeHeight
        {
            get
            {
                return height * 0.8F;
            }
        }

        public override int VerticalFaceSpeed
        {
            get
            {
                return Sitting ? 20 : base.VerticalFaceSpeed;
            }
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            Entity entity3 = damageSource1.Entity;
            aiSit.func_48407_a(false);
            if (entity3 != null && !(entity3 is EntityPlayer) && !(entity3 is EntityArrow))
            {
                i2 = (i2 + 1) / 2;
            }

            return base.attackEntityFrom(damageSource1, i2);
        }

        public override bool attackEntityAsMob(Entity entity1)
        {
            int i2 = Tamed ? 4 : 2;
            return entity1.attackEntityFrom(DamageSource.causeMobDamage(this), i2);
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
            if (!Tamed)
            {
                if (itemStack2 != null && itemStack2.itemID == Item.bone.shiftedIndex && !Angry)
                {
                    if (!entityPlayer1.capabilities.isCreativeMode)
                    {
                        --itemStack2.stackSize;
                    }

                    if (itemStack2.stackSize <= 0)
                    {
                        entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, null);
                    }

                    if (!worldObj.isRemote)
                    {
                        if (rand.Next(3) == 0)
                        {
                            Tamed = true;
                            PathToEntity = null;
                            AttackTarget = null;
                            aiSit.func_48407_a(true);
                            EntityHealth = 20;
                            setOwner(entityPlayer1.username);
                            func_48142_a(true);
                            worldObj.setEntityState(this, 7);
                        }
                        else
                        {
                            func_48142_a(false);
                            worldObj.setEntityState(this, 6);
                        }
                    }

                    return true;
                }
            }
            else
            {
                if (itemStack2 != null && Item.itemsList[itemStack2.itemID] is ItemFood)
                {
                    ItemFood itemFood3 = (ItemFood)Item.itemsList[itemStack2.itemID];
                    if (itemFood3.WolfsFavoriteMeat && dataWatcher.getWatchableObjectInt(18) < 20)
                    {
                        if (!entityPlayer1.capabilities.isCreativeMode)
                        {
                            --itemStack2.stackSize;
                        }

                        heal(itemFood3.HealAmount);
                        if (itemStack2.stackSize <= 0)
                        {
                            entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, null);
                        }

                        return true;
                    }
                }

                if (entityPlayer1.username.Equals(OwnerName, StringComparison.OrdinalIgnoreCase) && !worldObj.isRemote && !isWheat(itemStack2))
                {
                    aiSit.func_48407_a(!Sitting);
                    isJumping = false;
                    PathToEntity = null;
                }
            }

            return base.interact(entityPlayer1);
        }

        public override void handleHealthUpdate(sbyte b1)
        {
            if (b1 == 8)
            {
                field_25052_g = true;
                timeWolfIsShaking = 0.0F;
                prevTimeWolfIsShaking = 0.0F;
            }
            else
            {
                base.handleHealthUpdate(b1);
            }

        }

        public virtual float TailRotation
        {
            get
            {
                return Angry ? 1.5393804F : Tamed ? (0.55F - (20 - dataWatcher.getWatchableObjectInt(18)) * 0.02F) * (float)Math.PI : 0.62831855F;
            }
        }

        public override bool isWheat(ItemStack itemStack1)
        {
            return itemStack1 == null ? false : !(Item.itemsList[itemStack1.itemID] is ItemFood) ? false : ((ItemFood)Item.itemsList[itemStack1.itemID]).WolfsFavoriteMeat;
        }

        public override int MaxSpawnedInChunk
        {
            get
            {
                return 8;
            }
        }

        public virtual bool Angry
        {
            get
            {
                return (dataWatcher.getWatchableObjectByte(16) & 2) != 0;
            }
            set
            {
                sbyte b2 = dataWatcher.getWatchableObjectByte(16);
                if (value)
                {
                    dataWatcher.updateObject(16, (sbyte)(b2 | 2));
                }
                else
                {
                    dataWatcher.updateObject(16, (sbyte)(b2 & -3));
                }

            }
        }


        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            EntityWolf entityWolf2 = new EntityWolf(worldObj);
            entityWolf2.setOwner(OwnerName);
            entityWolf2.Tamed = true;
            return entityWolf2;
        }

        public virtual void func_48150_h(bool z1)
        {
            looksWithInterest = z1;
        }

        public override bool canMateWith(EntityAnimal entityAnimal1)
        {
            if (entityAnimal1 == this)
            {
                return false;
            }
            else if (!Tamed)
            {
                return false;
            }
            else if (!(entityAnimal1 is EntityWolf))
            {
                return false;
            }
            else
            {
                EntityWolf entityWolf2 = (EntityWolf)entityAnimal1;
                return !entityWolf2.Tamed ? false : entityWolf2.Sitting ? false : InLove && entityWolf2.InLove;
            }
        }
    }

}