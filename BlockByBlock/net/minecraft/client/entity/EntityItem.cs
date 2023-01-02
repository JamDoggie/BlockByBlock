using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityItem : Entity
    {
        public ItemStack item;
        public int age = 0;
        public int delayBeforeCanPickup;
        private int health = 5;
        public float field_804_d = (float)(portinghelpers.MathHelper.NextDouble * Math.PI * 2.0D);

        public EntityItem(World world1, double d2, double d4, double d6, ItemStack itemStack8) : base(world1)
        {
            SetSize(0.25F, 0.25F);
            yOffset = height / 2.0F;
            SetPosition(d2, d4, d6);
            item = itemStack8;
            rotationYaw = (float)(portinghelpers.MathHelper.NextDouble * 360.0D);
            motionX = (float)(portinghelpers.MathHelper.NextDouble * (double)0.2F - (double)0.1F);
            motionY = 0.2F;
            motionZ = (float)(portinghelpers.MathHelper.NextDouble * (double)0.2F - (double)0.1F);
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        public EntityItem(World world1) : base(world1)
        {
            SetSize(0.25F, 0.25F);
            yOffset = height / 2.0F;
        }

        protected internal override void entityInit()
        {
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (delayBeforeCanPickup > 0)
            {
                --delayBeforeCanPickup;
            }

            prevPosX = posX;
            prevPosY = posY;
            prevPosZ = posZ;
            motionY -= 0.04F;
            if (worldObj.getBlockMaterial(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) == Material.lava)
            {
                motionY = 0.2F;
                motionX = (rand.NextSingle() - rand.NextSingle()) * 0.2F;
                motionZ = (rand.NextSingle() - rand.NextSingle()) * 0.2F;
                worldObj.playSoundAtEntity(this, "random.fizz", 0.4F, 2.0F + rand.NextSingle() * 0.4F);
            }

            pushOutOfBlocks(posX, (boundingBox.minY + boundingBox.maxY) / 2.0D, posZ);
            moveEntity(motionX, motionY, motionZ);
            float f1 = 0.98F;
            if (onGround)
            {
                f1 = 0.58800006F;
                int i2 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(boundingBox.minY) - 1, MathHelper.floor_double(posZ));
                if (i2 > 0)
                {
                    f1 = Block.blocksList[i2].slipperiness * 0.98F;
                }
            }

            motionX *= f1;
            motionY *= 0.98F;
            motionZ *= f1;
            if (onGround)
            {
                motionY *= -0.5D;
            }

            ++age;
            if (age >= 6000)
            {
                setDead();
            }

        }

        public override bool handleWaterMovement()
        {
            return worldObj.handleMaterialAcceleration(boundingBox, Material.water, this);
        }

        protected internal override void dealFireDamage(int i1)
        {
            attackEntityFrom(DamageSource.inFire, i1);
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            setBeenAttacked();
            health -= i2;
            if (health <= 0)
            {
                setDead();
            }

            return false;
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("Health", (sbyte)health);
            nBTTagCompound1.setShort("Age", (short)age);
            nBTTagCompound1.setCompoundTag("Item", item.writeToNBT(new NBTTagCompound()));
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            health = nBTTagCompound1.getShort("Health") & 255;
            age = nBTTagCompound1.getShort("Age");
            NBTTagCompound nBTTagCompound2 = nBTTagCompound1.getCompoundTag("Item");
            item = ItemStack.loadItemStackFromNBT(nBTTagCompound2);
            if (item == null)
            {
                setDead();
            }

        }

        public override void onCollideWithPlayer(EntityPlayer entityPlayer1)
        {
            if (!worldObj.isRemote)
            {
                int i2 = item.stackSize;
                if (delayBeforeCanPickup == 0 && entityPlayer1.inventory.addItemStackToInventory(item))
                {
                    if (item.itemID == Block.wood.blockID)
                    {
                        entityPlayer1.triggerAchievement(AchievementList.mineWood);
                    }

                    if (item.itemID == Item.leather.shiftedIndex)
                    {
                        entityPlayer1.triggerAchievement(AchievementList.killCow);
                    }

                    if (item.itemID == Item.diamond.shiftedIndex)
                    {
                        entityPlayer1.triggerAchievement(AchievementList.diamonds);
                    }

                    if (item.itemID == Item.blazeRod.shiftedIndex)
                    {
                        entityPlayer1.triggerAchievement(AchievementList.blazeRod);
                    }

                    worldObj.playSoundAtEntity(this, "random.pop", 0.2F, ((rand.NextSingle() - rand.NextSingle()) * 0.7F + 1.0F) * 2.0F);
                    entityPlayer1.onItemPickup(this, i2);
                    if (item.stackSize <= 0)
                    {
                        setDead();
                    }
                }

            }
        }

        public override bool canAttackWithItem()
        {
            return false;
        }
    }

}