using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityPigZombie : EntityZombie
    {
        private int angerLevel = 0;
        private int randomSoundDelay = 0;
        private static readonly ItemStack defaultHeldItem = new ItemStack(Item.swordGold, 1);

        public EntityPigZombie(World world1) : base(world1)
        {
            texture = "/mob/pigzombie.png";
            moveSpeed = 0.5F;
            attackStrength = 5;
            isImmuneToFire = true;
        }

        public override bool AIEnabled
        {
            get
            {
                return false;
            }
        }

        public override void onUpdate()
        {
            moveSpeed = entityToAttack != null ? 0.95F : 0.5F;
            if (randomSoundDelay > 0 && --randomSoundDelay == 0)
            {
                worldObj.playSoundAtEntity(this, "mob.zombiepig.zpigangry", SoundVolume * 2.0F, ((rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F) * 1.8F);
            }

            base.onUpdate();
        }

        public override bool CanSpawnHere
        {
            get
            {
                return worldObj.difficultySetting > 0 && worldObj.checkIfAABBIsClear(boundingBox) && worldObj.getCollidingBoundingBoxes(this, boundingBox).Count == 0 && !worldObj.isAnyLiquid(boundingBox);
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setShort("Anger", (short)angerLevel);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            angerLevel = nBTTagCompound1.getShort("Anger");
        }

        protected internal override Entity findPlayerToAttack()
        {
            return angerLevel == 0 ? null : base.findPlayerToAttack();
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            Entity entity3 = damageSource1.Entity;
            if (entity3 is EntityPlayer)
            {
                System.Collections.IList list4 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand(32.0D, 32.0D, 32.0D));

                for (int i5 = 0; i5 < list4.Count; ++i5)
                {
                    Entity entity6 = (Entity)list4[i5];
                    if (entity6 is EntityPigZombie)
                    {
                        EntityPigZombie entityPigZombie7 = (EntityPigZombie)entity6;
                        entityPigZombie7.becomeAngryAt(entity3);
                    }
                }

                becomeAngryAt(entity3);
            }

            return base.attackEntityFrom(damageSource1, i2);
        }

        private void becomeAngryAt(Entity entity1)
        {
            entityToAttack = entity1;
            angerLevel = 400 + rand.Next(400);
            randomSoundDelay = rand.Next(40);
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.zombiepig.zpig";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.zombiepig.zpighurt";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.zombiepig.zpigdeath";
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = rand.Next(2 + i2);

            int i4;
            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.rottenFlesh.shiftedIndex, 1);
            }

            i3 = rand.Next(2 + i2);

            for (i4 = 0; i4 < i3; ++i4)
            {
                dropItem(Item.goldNugget.shiftedIndex, 1);
            }

        }

        protected internal override void dropRareDrop(int i1)
        {
            if (i1 > 0)
            {
                ItemStack itemStack2 = new ItemStack(Item.swordGold);
                EnchantmentHelper.func_48441_a(rand, itemStack2, 5);
                entityDropItem(itemStack2, 0.0F);
            }
            else
            {
                int i3 = rand.Next(3);
                if (i3 == 0)
                {
                    dropItem(Item.ingotGold.shiftedIndex, 1);
                }
                else if (i3 == 1)
                {
                    dropItem(Item.swordGold.shiftedIndex, 1);
                }
                else if (i3 == 2)
                {
                    dropItem(Item.helmetGold.shiftedIndex, 1);
                }
            }

        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.rottenFlesh.shiftedIndex;
            }
        }

        public override ItemStack HeldItem
        {
            get
            {
                return defaultHeldItem;
            }
        }
    }

}