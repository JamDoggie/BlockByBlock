using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityOcelot : EntityTameable
    {
        private EntityAITempt aiTempt;

        public EntityOcelot(World world1) : base(world1)
        {
            texture = "/mob/ozelot.png";
            SetSize(0.6F, 0.8F);
            Navigator.func_48664_a(true);
            tasks.addTask(1, new EntityAISwimming(this));
            tasks.addTask(2, aiSit);
            tasks.addTask(3, aiTempt = new EntityAITempt(this, 0.18F, Item.fishRaw.shiftedIndex, true));
            tasks.addTask(4, new EntityAIAvoidEntity(this, typeof(EntityPlayer), 16.0F, 0.23F, 0.4F));
            tasks.addTask(5, new EntityAIFollowOwner(this, 0.3F, 10.0F, 5.0F));
            tasks.addTask(6, new EntityAIOcelotSit(this, 0.4F));
            tasks.addTask(7, new EntityAILeapAtTarget(this, 0.3F));
            tasks.addTask(8, new EntityAIOcelotAttack(this));
            tasks.addTask(9, new EntityAIMate(this, 0.23F));
            tasks.addTask(10, new EntityAIWander(this, 0.23F));
            tasks.addTask(11, new EntityAIWatchClosest(this, typeof(EntityPlayer), 10.0F));
            targetTasks.addTask(1, new EntityAITargetNonTamed(this, typeof(EntityChicken), 14.0F, 750, false));
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(18, (sbyte)0);
        }

        protected internal override void updateAITick()
        {
            if (!MoveHelper.func_48186_a())
            {
                Sneaking = false;
                Sprinting = false;
            }
            else
            {
                float f1 = MoveHelper.Speed;
                if (f1 == 0.18F)
                {
                    Sneaking = true;
                    Sprinting = false;
                }
                else if (f1 == 0.4F)
                {
                    Sneaking = false;
                    Sprinting = true;
                }
                else
                {
                    Sneaking = false;
                    Sprinting = false;
                }
            }

        }

        protected internal override bool canDespawn()
        {
            return !Tamed;
        }

        public override string Texture
        {
            get
            {
                switch (func_48148_ad())
                {
                    case 0:
                        return "/mob/ozelot.png";
                    case 1:
                        return "/mob/cat_black.png";
                    case 2:
                        return "/mob/cat_red.png";
                    case 3:
                        return "/mob/cat_siamese.png";
                    default:
                        return base.Texture;
                }
            }
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
        }

        public override int MaxHealth
        {
            get
            {
                return 10;
            }
        }

        protected internal override void fall(float f1)
        {
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setInteger("CatType", func_48148_ad());
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            func_48147_c(nBTTagCompound1.getInteger("CatType"));
        }

        protected internal override string LivingSound
        {
            get
            {
                return Tamed ? InLove ? "mob.cat.purr" : rand.Next(4) == 0 ? "mob.cat.purreow" : "mob.cat.meow" : "";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.cat.hitt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.cat.hitt";
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
                return Item.leather.shiftedIndex;
            }
        }

        public override bool attackEntityAsMob(Entity entity1)
        {
            return entity1.attackEntityFrom(DamageSource.causeMobDamage(this), 3);
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            aiSit.func_48407_a(false);
            return base.attackEntityFrom(damageSource1, i2);
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
            if (!Tamed)
            {
                if (aiTempt.func_48270_h() && itemStack2 != null && itemStack2.itemID == Item.fishRaw.shiftedIndex && entityPlayer1.getDistanceSqToEntity(this) < 9.0D)
                {
                    --itemStack2.stackSize;
                    if (itemStack2.stackSize <= 0)
                    {
                        entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, null);
                    }

                    if (!worldObj.isRemote)
                    {
                        if (rand.Next(3) == 0)
                        {
                            Tamed = true;
                            func_48147_c(1 + worldObj.rand.Next(3));
                            setOwner(entityPlayer1.username);
                            func_48142_a(true);
                            aiSit.func_48407_a(true);
                            worldObj.setEntityState(this, 7);
                        }
                        else
                        {
                            func_48142_a(false);
                            worldObj.setEntityState(this, 6);
                        }
                    }
                }

                return true;
            }
            else
            {
                if (entityPlayer1.username.Equals(OwnerName, StringComparison.OrdinalIgnoreCase) && !worldObj.isRemote && !isWheat(itemStack2))
                {
                    aiSit.func_48407_a(!Sitting);
                }

                return base.interact(entityPlayer1);
            }
        }

        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            EntityOcelot entityOcelot2 = new EntityOcelot(worldObj);
            if (Tamed)
            {
                entityOcelot2.setOwner(OwnerName);
                entityOcelot2.Tamed = true;
                entityOcelot2.func_48147_c(func_48148_ad());
            }

            return entityOcelot2;
        }

        public override bool isWheat(ItemStack itemStack1)
        {
            return itemStack1 != null && itemStack1.itemID == Item.fishRaw.shiftedIndex;
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
            else if (!(entityAnimal1 is EntityOcelot))
            {
                return false;
            }
            else
            {
                EntityOcelot entityOcelot2 = (EntityOcelot)entityAnimal1;
                return !entityOcelot2.Tamed ? false : InLove && entityOcelot2.InLove;
            }
        }

        public virtual int func_48148_ad()
        {
            return dataWatcher.getWatchableObjectByte(18);
        }

        public virtual void func_48147_c(int i1)
        {
            dataWatcher.updateObject(18, (sbyte)i1);
        }

        public override bool CanSpawnHere
        {
            get
            {
                if (worldObj.rand.Next(3) == 0)
                {
                    return false;
                }
                else
                {
                    if (worldObj.checkIfAABBIsClear(boundingBox) && worldObj.getCollidingBoundingBoxes(this, boundingBox).Count == 0 && !worldObj.isAnyLiquid(boundingBox))
                    {
                        int i1 = MathHelper.floor_double(posX);
                        int i2 = MathHelper.floor_double(boundingBox.minY);
                        int i3 = MathHelper.floor_double(posZ);
                        if (i2 < 63)
                        {
                            return false;
                        }

                        int i4 = worldObj.getBlockId(i1, i2 - 1, i3);
                        if (i4 == Block.grass.blockID || i4 == Block.leaves.blockID)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }
    }

}