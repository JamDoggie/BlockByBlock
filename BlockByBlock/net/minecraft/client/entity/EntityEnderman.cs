using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityEnderman : EntityMob
    {
        private static bool[] canCarryBlocks = new bool[256];
        public bool isAttacking = false;
        private int teleportDelay = 0;
        private int field_35185_e = 0;

        public EntityEnderman(World world1) : base(world1)
        {
            texture = "/mob/enderman.png";
            moveSpeed = 0.2F;
            attackStrength = 7;
            SetSize(0.6F, 2.9F);
            stepHeight = 1.0F;
        }

        public override int MaxHealth
        {
            get
            {
                return 40;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new sbyte?(0));
            dataWatcher.addObject(17, new sbyte?(0));
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setShort("carried", (short)Carried);
            nBTTagCompound1.setShort("carriedData", (short)CarryingData);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            Carried = nBTTagCompound1.getShort("carried");
            CarryingData = nBTTagCompound1.getShort("carriedData");
        }

        protected internal override Entity findPlayerToAttack()
        {
            EntityPlayer entityPlayer1 = worldObj.getClosestVulnerablePlayerToEntity(this, 64.0D);
            if (entityPlayer1 != null)
            {
                if (shouldAttackPlayer(entityPlayer1))
                {
                    if (field_35185_e++ == 5)
                    {
                        field_35185_e = 0;
                        return entityPlayer1;
                    }
                }
                else
                {
                    field_35185_e = 0;
                }
            }

            return null;
        }

        public override int getBrightnessForRender(float f1)
        {
            return base.getBrightnessForRender(f1);
        }

        public override float getBrightness(float f1)
        {
            return base.getBrightness(f1);
        }

        private bool shouldAttackPlayer(EntityPlayer entityPlayer1)
        {
            ItemStack itemStack2 = entityPlayer1.inventory.armorInventory[3];
            if (itemStack2 != null && itemStack2.itemID == Block.pumpkin.blockID)
            {
                return false;
            }
            else
            {
                Vec3D vec3D3 = entityPlayer1.getLook(1.0F).normalize();
                Vec3D vec3D4 = Vec3D.createVector(posX - entityPlayer1.posX, boundingBox.minY + (double)(height / 2.0F) - (entityPlayer1.posY + (double)entityPlayer1.EyeHeight), posZ - entityPlayer1.posZ);
                double d5 = vec3D4.lengthVector();
                vec3D4 = vec3D4.normalize();
                double d7 = vec3D3.dotProduct(vec3D4);
                return d7 > 1.0D - 0.025D / d5 ? entityPlayer1.canEntityBeSeen(this) : false;
            }
        }

        public override void onLivingUpdate()
        {
            if (Wet)
            {
                attackEntityFrom(DamageSource.drown, 1);
            }

            isAttacking = entityToAttack != null;
            moveSpeed = entityToAttack != null ? 6.5F : 0.3F;
            int i1;
            if (!worldObj.isRemote)
            {
                int i2;
                int i3;
                int i4;
                if (Carried == 0)
                {
                    if (rand.Next(20) == 0)
                    {
                        i1 = MathHelper.floor_double(posX - 2.0D + rand.NextDouble() * 4.0D);
                        i2 = MathHelper.floor_double(posY + rand.NextDouble() * 3.0D);
                        i3 = MathHelper.floor_double(posZ - 2.0D + rand.NextDouble() * 4.0D);
                        i4 = worldObj.getBlockId(i1, i2, i3);
                        if (canCarryBlocks[i4])
                        {
                            Carried = worldObj.getBlockId(i1, i2, i3);
                            CarryingData = worldObj.getBlockMetadata(i1, i2, i3);
                            worldObj.setBlockWithNotify(i1, i2, i3, 0);
                        }
                    }
                }
                else if (rand.Next(2000) == 0)
                {
                    i1 = MathHelper.floor_double(posX - 1.0D + rand.NextDouble() * 2.0D);
                    i2 = MathHelper.floor_double(posY + rand.NextDouble() * 2.0D);
                    i3 = MathHelper.floor_double(posZ - 1.0D + rand.NextDouble() * 2.0D);
                    i4 = worldObj.getBlockId(i1, i2, i3);
                    int i5 = worldObj.getBlockId(i1, i2 - 1, i3);
                    if (i4 == 0 && i5 > 0 && Block.blocksList[i5].renderAsNormalBlock())
                    {
                        worldObj.setBlockAndMetadataWithNotify(i1, i2, i3, Carried, CarryingData);
                        Carried = 0;
                    }
                }
            }

            for (i1 = 0; i1 < 2; ++i1)
            {
                worldObj.spawnParticle("portal", posX + (rand.NextDouble() - 0.5D) * width, posY + rand.NextDouble() * height - 0.25D, posZ + (rand.NextDouble() - 0.5D) * width, (rand.NextDouble() - 0.5D) * 2.0D, -rand.NextDouble(), (rand.NextDouble() - 0.5D) * 2.0D);
            }

            if (worldObj.Daytime && !worldObj.isRemote)
            {
                float f6 = getBrightness(1.0F);
                if (f6 > 0.5F && worldObj.canBlockSeeTheSky(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) && rand.NextSingle() * 30.0F < (f6 - 0.4F) * 2.0F)
                {
                    entityToAttack = null;
                    teleportRandomly();
                }
            }

            if (Wet)
            {
                entityToAttack = null;
                teleportRandomly();
            }

            isJumping = false;
            if (entityToAttack != null)
            {
                faceEntity(entityToAttack, 100.0F, 100.0F);
            }

            if (!worldObj.isRemote && EntityAlive)
            {
                if (entityToAttack != null)
                {
                    if (entityToAttack is EntityPlayer && shouldAttackPlayer((EntityPlayer)entityToAttack))
                    {
                        moveStrafing = moveForward = 0.0F;
                        moveSpeed = 0.0F;
                        if (entityToAttack.getDistanceSqToEntity(this) < 16.0D)
                        {
                            teleportRandomly();
                        }

                        teleportDelay = 0;
                    }
                    else if (entityToAttack.getDistanceSqToEntity(this) > 256.0D && teleportDelay++ >= 30 && teleportToEntity(entityToAttack))
                    {
                        teleportDelay = 0;
                    }
                }
                else
                {
                    teleportDelay = 0;
                }
            }

            base.onLivingUpdate();
        }

        protected internal virtual bool teleportRandomly()
        {
            double d1 = posX + (rand.NextDouble() - 0.5D) * 64.0D;
            double d3 = posY + (rand.Next(64) - 32);
            double d5 = posZ + (rand.NextDouble() - 0.5D) * 64.0D;
            return teleportTo(d1, d3, d5);
        }

        protected internal virtual bool teleportToEntity(Entity entity1)
        {
            Vec3D vec3D2 = Vec3D.createVector(posX - entity1.posX, boundingBox.minY + (double)(height / 2.0F) - entity1.posY + (double)entity1.EyeHeight, posZ - entity1.posZ);
            vec3D2 = vec3D2.normalize();
            double d3 = 16.0D;
            double d5 = posX + (rand.NextDouble() - 0.5D) * 8.0D - vec3D2.xCoord * d3;
            double d7 = posY + (rand.Next(16) - 8) - vec3D2.yCoord * d3;
            double d9 = posZ + (rand.NextDouble() - 0.5D) * 8.0D - vec3D2.zCoord * d3;
            return teleportTo(d5, d7, d9);
        }

        protected internal virtual bool teleportTo(double d1, double d3, double d5)
        {
            double d7 = posX;
            double d9 = posY;
            double d11 = posZ;
            posX = d1;
            posY = d3;
            posZ = d5;
            bool z13 = false;
            int i14 = MathHelper.floor_double(posX);
            int i15 = MathHelper.floor_double(posY);
            int i16 = MathHelper.floor_double(posZ);
            int i18;
            if (worldObj.blockExists(i14, i15, i16))
            {
                bool z17 = false;

                while (true)
                {
                    while (!z17 && i15 > 0)
                    {
                        i18 = worldObj.getBlockId(i14, i15 - 1, i16);
                        if (i18 != 0 && Block.blocksList[i18].blockMaterial.blocksMovement())
                        {
                            z17 = true;
                        }
                        else
                        {
                            --posY;
                            --i15;
                        }
                    }

                    if (z17)
                    {
                        SetPosition(posX, posY, posZ);
                        if (worldObj.getCollidingBoundingBoxes(this, boundingBox).Count == 0 && !worldObj.isAnyLiquid(boundingBox))
                        {
                            z13 = true;
                        }
                    }
                    break;
                }
            }

            if (!z13)
            {
                SetPosition(d7, d9, d11);
                return false;
            }
            else
            {
                short s30 = 128;

                for (i18 = 0; i18 < s30; ++i18)
                {
                    double d19 = i18 / (s30 - 1.0D);
                    float f21 = (rand.NextSingle() - 0.5F) * 0.2F;
                    float f22 = (rand.NextSingle() - 0.5F) * 0.2F;
                    float f23 = (rand.NextSingle() - 0.5F) * 0.2F;
                    double d24 = d7 + (posX - d7) * d19 + (rand.NextDouble() - 0.5D) * width * 2.0D;
                    double d26 = d9 + (posY - d9) * d19 + rand.NextDouble() * height;
                    double d28 = d11 + (posZ - d11) * d19 + (rand.NextDouble() - 0.5D) * width * 2.0D;
                    worldObj.spawnParticle("portal", d24, d26, d28, (double)f21, (double)f22, (double)f23);
                }

                worldObj.playSoundEffect(d7, d9, d11, "mob.endermen.portal", 1.0F, 1.0F);
                worldObj.playSoundAtEntity(this, "mob.endermen.portal", 1.0F, 1.0F);
                return true;
            }
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.endermen.idle";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.endermen.hit";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.endermen.death";
            }
        }

        protected internal override int DropItemId
        {
            get
            {
                return Item.enderPearl.shiftedIndex;
            }
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            int i3 = DropItemId;
            if (i3 > 0)
            {
                int i4 = rand.Next(2 + i2);

                for (int i5 = 0; i5 < i4; ++i5)
                {
                    dropItem(i3, 1);
                }
            }

        }

        public virtual int Carried
        {
            set
            {
                dataWatcher.updateObject(16, unchecked((sbyte)(value & 255)));
            }
            get
            {
                return dataWatcher.getWatchableObjectByte(16);
            }
        }


        public virtual int CarryingData
        {
            set
            {
                dataWatcher.updateObject(17, unchecked((sbyte)(value & 255)));
            }
            get
            {
                return dataWatcher.getWatchableObjectByte(17);
            }
        }


        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (damageSource1 is EntityDamageSourceIndirect)
            {
                for (int i3 = 0; i3 < 64; ++i3)
                {
                    if (teleportRandomly())
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return base.attackEntityFrom(damageSource1, i2);
            }
        }

        static EntityEnderman()
        {
            canCarryBlocks[Block.grass.blockID] = true;
            canCarryBlocks[Block.dirt.blockID] = true;
            canCarryBlocks[Block.sand.blockID] = true;
            canCarryBlocks[Block.gravel.blockID] = true;
            canCarryBlocks[Block.plantYellow.blockID] = true;
            canCarryBlocks[Block.plantRed.blockID] = true;
            canCarryBlocks[Block.mushroomBrown.blockID] = true;
            canCarryBlocks[Block.mushroomRed.blockID] = true;
            canCarryBlocks[Block.tnt.blockID] = true;
            canCarryBlocks[Block.cactus.blockID] = true;
            canCarryBlocks[Block.blockClay.blockID] = true;
            canCarryBlocks[Block.pumpkin.blockID] = true;
            canCarryBlocks[Block.melon.blockID] = true;
            canCarryBlocks[Block.mycelium.blockID] = true;
        }
    }

}