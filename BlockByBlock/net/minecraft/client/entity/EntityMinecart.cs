using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityMinecart : Entity, IInventory
    {
        private ItemStack[] cargoItems;
        private int fuel;
        private bool field_856_i;
        public int minecartType;
        public double pushX;
        public double pushZ;
        private static readonly int[][][] field_855_j = new int[][][]
        {
            new int[][]
            {
                new int[] {0, 0, -1},
                new int[] {0, 0, 1}
            },
            new int[][]
            {
                new int[] {-1, 0, 0},
                new int[] {1, 0, 0}
            },
            new int[][]
            {
                new int[] {-1, -1, 0},
                new int[] {1, 0, 0}
            },
            new int[][]
            {
                new int[] {-1, 0, 0},
                new int[] {1, -1, 0}
            },
            new int[][]
            {
                new int[] {0, 0, -1},
                new int[] {0, -1, 1}
            },
            new int[][]
            {
                new int[] {0, -1, -1},
                new int[] {0, 0, 1}
            },
            new int[][]
            {
                new int[] {0, 0, 1},
                new int[] {1, 0, 0}
            },
            new int[][]
            {
                new int[] {0, 0, 1},
                new int[] {-1, 0, 0}
            },
            new int[][]
            {
                new int[] {0, 0, -1},
                new int[] {-1, 0, 0}
            },
            new int[][]
            {
                new int[] {0, 0, -1},
                new int[] {1, 0, 0}
            }
        };
        private int turnProgress;
        private double minecartX;
        private double minecartY;
        private double minecartZ;
        private double minecartYaw;
        private double minecartPitch;
        private double velocityX;
        private double velocityY;
        private double velocityZ;

        public EntityMinecart(World world1) : base(world1)
        {
            cargoItems = new ItemStack[36];
            fuel = 0;
            field_856_i = false;
            preventEntitySpawning = true;
            SetSize(0.98F, 0.7F);
            yOffset = height / 2.0F;
        }

        protected internal override bool canTriggerWalking()
        {
            return false;
        }

        protected internal override void entityInit()
        {
            dataWatcher.addObject(16, new sbyte?(0));
            dataWatcher.addObject(17, new int?(0));
            dataWatcher.addObject(18, new int?(1));
            dataWatcher.addObject(19, new int?(0));
        }

        public override AxisAlignedBB getCollisionBox(Entity entity1)
        {
            return entity1.boundingBox;
        }

        public override AxisAlignedBB BoundingBox
        {
            get
            {
                return null;
            }
        }

        public override bool canBePushed()
        {
            return true;
        }

        public EntityMinecart(World world1, double d2, double d4, double d6, int i8) : this(world1)
        {
            SetPosition(d2, d4 + yOffset, d6);
            motionX = 0.0D;
            motionY = 0.0D;
            motionZ = 0.0D;
            prevPosX = d2;
            prevPosY = d4;
            prevPosZ = d6;
            minecartType = i8;
        }

        public override double MountedYOffset
        {
            get
            {
                return height * 0.0D - (double)0.3F;
            }
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (!worldObj.isRemote && !isDead)
            {
                func_41029_h(-func_41030_m());
                func_41028_c(10);
                setBeenAttacked();
                func_41024_b(func_41025_i() + i2 * 10);
                if (func_41025_i() > 40)
                {
                    if (riddenByEntity != null)
                    {
                        riddenByEntity.mountEntity(this);
                    }

                    setDead();
                    dropItemWithOffset(Item.minecartEmpty.shiftedIndex, 1, 0.0F);
                    if (minecartType == 1)
                    {
                        EntityMinecart entityMinecart3 = this;

                        for (int i4 = 0; i4 < entityMinecart3.SizeInventory; ++i4)
                        {
                            ItemStack itemStack5 = entityMinecart3.getStackInSlot(i4);
                            if (itemStack5 != null)
                            {
                                float f6 = rand.NextSingle() * 0.8F + 0.1F;
                                float f7 = rand.NextSingle() * 0.8F + 0.1F;
                                float f8 = rand.NextSingle() * 0.8F + 0.1F;

                                while (itemStack5.stackSize > 0)
                                {
                                    int i9 = rand.Next(21) + 10;
                                    if (i9 > itemStack5.stackSize)
                                    {
                                        i9 = itemStack5.stackSize;
                                    }

                                    itemStack5.stackSize -= i9;
                                    EntityItem entityItem10 = new EntityItem(worldObj, posX + (double)f6, posY + (double)f7, posZ + (double)f8, new ItemStack(itemStack5.itemID, i9, itemStack5.ItemDamage));
                                    float f11 = 0.05F;
                                    entityItem10.motionX = (float)rand.NextGaussian() * f11;
                                    entityItem10.motionY = (float)rand.NextGaussian() * f11 + 0.2F;
                                    entityItem10.motionZ = (float)rand.NextGaussian() * f11;
                                    worldObj.spawnEntityInWorld(entityItem10);
                                }
                            }
                        }

                        dropItemWithOffset(Block.chest.blockID, 1, 0.0F);
                    }
                    else if (minecartType == 2)
                    {
                        dropItemWithOffset(Block.stoneOvenIdle.blockID, 1, 0.0F);
                    }
                }

                return true;
            }
            else
            {
                return true;
            }
        }

        public override void performHurtAnimation()
        {
            func_41029_h(-func_41030_m());
            func_41028_c(10);
            func_41024_b(func_41025_i() + func_41025_i() * 10);
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override void setDead()
        {
            for (int i1 = 0; i1 < SizeInventory; ++i1)
            {
                ItemStack itemStack2 = getStackInSlot(i1);
                if (itemStack2 != null)
                {
                    float f3 = rand.NextSingle() * 0.8F + 0.1F;
                    float f4 = rand.NextSingle() * 0.8F + 0.1F;
                    float f5 = rand.NextSingle() * 0.8F + 0.1F;

                    while (itemStack2.stackSize > 0)
                    {
                        int i6 = rand.Next(21) + 10;
                        if (i6 > itemStack2.stackSize)
                        {
                            i6 = itemStack2.stackSize;
                        }

                        itemStack2.stackSize -= i6;
                        EntityItem entityItem7 = new EntityItem(worldObj, posX + (double)f3, posY + (double)f4, posZ + (double)f5, new ItemStack(itemStack2.itemID, i6, itemStack2.ItemDamage));
                        if (itemStack2.hasTagCompound())
                        {
                            entityItem7.item.TagCompound = (NBTTagCompound)itemStack2.TagCompound.copy();
                        }

                        float f8 = 0.05F;
                        entityItem7.motionX = (float)rand.NextGaussian() * f8;
                        entityItem7.motionY = (float)rand.NextGaussian() * f8 + 0.2F;
                        entityItem7.motionZ = (float)rand.NextGaussian() * f8;
                        worldObj.spawnEntityInWorld(entityItem7);
                    }
                }
            }

            base.setDead();
        }

        public override void onUpdate()
        {
            if (func_41023_l() > 0)
            {
                func_41028_c(func_41023_l() - 1);
            }

            if (func_41025_i() > 0)
            {
                func_41024_b(func_41025_i() - 1);
            }

            if (posY < -64.0D)
            {
                kill();
            }

            if (MinecartPowered && rand.Next(4) == 0)
            {
                worldObj.spawnParticle("largesmoke", posX, posY + 0.8D, posZ, 0.0D, 0.0D, 0.0D);
            }

            if (worldObj.isRemote)
            {
                if (turnProgress > 0)
                {
                    double d45 = posX + (minecartX - posX) / turnProgress;
                    double d46 = posY + (minecartY - posY) / turnProgress;
                    double d5 = posZ + (minecartZ - posZ) / turnProgress;

                    double d7;
                    for (d7 = minecartYaw - rotationYaw; d7 < -180.0D; d7 += 360.0D)
                    {
                    }

                    while (d7 >= 180.0D)
                    {
                        d7 -= 360.0D;
                    }

                    rotationYaw = (float)(rotationYaw + d7 / turnProgress);
                    rotationPitch = (float)(rotationPitch + (minecartPitch - rotationPitch) / turnProgress);
                    --turnProgress;
                    SetPosition(d45, d46, d5);
                    setRotation(rotationYaw, rotationPitch);
                }
                else
                {
                    SetPosition(posX, posY, posZ);
                    setRotation(rotationYaw, rotationPitch);
                }

            }
            else
            {
                prevPosX = posX;
                prevPosY = posY;
                prevPosZ = posZ;
                motionY -= 0.04F;
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(posY);
                int i3 = MathHelper.floor_double(posZ);
                if (BlockRail.isRailBlockAt(worldObj, i1, i2 - 1, i3))
                {
                    --i2;
                }

                double d4 = 0.4D;
                double d6 = 2.0D / 256D;
                int i8 = worldObj.getBlockId(i1, i2, i3);
                if (BlockRail.isRailBlock(i8))
                {
                    Vec3D vec3D9 = func_514_g(posX, posY, posZ);
                    int i10 = worldObj.getBlockMetadata(i1, i2, i3);
                    posY = i2;
                    bool z11 = false;
                    bool z12 = false;
                    if (i8 == Block.railPowered.blockID)
                    {
                        z11 = (i10 & 8) != 0;
                        z12 = !z11;
                    }

                    if (((BlockRail)Block.blocksList[i8]).Powered)
                    {
                        i10 &= 7;
                    }

                    if (i10 >= 2 && i10 <= 5)
                    {
                        posY = i2 + 1;
                    }

                    if (i10 == 2)
                    {
                        motionX -= d6;
                    }

                    if (i10 == 3)
                    {
                        motionX += d6;
                    }

                    if (i10 == 4)
                    {
                        motionZ += d6;
                    }

                    if (i10 == 5)
                    {
                        motionZ -= d6;
                    }

                    int[][] i13 = field_855_j[i10];
                    double d14 = i13[1][0] - i13[0][0];
                    double d16 = i13[1][2] - i13[0][2];
                    double d18 = Math.Sqrt(d14 * d14 + d16 * d16);
                    double d20 = motionX * d14 + motionZ * d16;
                    if (d20 < 0.0D)
                    {
                        d14 = -d14;
                        d16 = -d16;
                    }

                    double d22 = Math.Sqrt(motionX * motionX + motionZ * motionZ);
                    motionX = d22 * d14 / d18;
                    motionZ = d22 * d16 / d18;
                    double d24;
                    if (z12)
                    {
                        d24 = Math.Sqrt(motionX * motionX + motionZ * motionZ);
                        if (d24 < 0.03D)
                        {
                            motionX *= 0.0D;
                            motionY *= 0.0D;
                            motionZ *= 0.0D;
                        }
                        else
                        {
                            motionX *= 0.5D;
                            motionY *= 0.0D;
                            motionZ *= 0.5D;
                        }
                    }

                    d24 = 0.0D;
                    double d26 = i1 + 0.5D + i13[0][0] * 0.5D;
                    double d28 = i3 + 0.5D + i13[0][2] * 0.5D;
                    double d30 = i1 + 0.5D + i13[1][0] * 0.5D;
                    double d32 = i3 + 0.5D + i13[1][2] * 0.5D;
                    d14 = d30 - d26;
                    d16 = d32 - d28;
                    double d34;
                    double d36;
                    double d38;
                    if (d14 == 0.0D)
                    {
                        posX = i1 + 0.5D;
                        d24 = posZ - i3;
                    }
                    else if (d16 == 0.0D)
                    {
                        posZ = i3 + 0.5D;
                        d24 = posX - i1;
                    }
                    else
                    {
                        d34 = posX - d26;
                        d36 = posZ - d28;
                        d38 = (d34 * d14 + d36 * d16) * 2.0D;
                        d24 = d38;
                    }

                    posX = d26 + d14 * d24;
                    posZ = d28 + d16 * d24;
                    SetPosition(posX, posY + yOffset, posZ);
                    d34 = motionX;
                    d36 = motionZ;
                    if (riddenByEntity != null)
                    {
                        d34 *= 0.75D;
                        d36 *= 0.75D;
                    }

                    if (d34 < -d4)
                    {
                        d34 = -d4;
                    }

                    if (d34 > d4)
                    {
                        d34 = d4;
                    }

                    if (d36 < -d4)
                    {
                        d36 = -d4;
                    }

                    if (d36 > d4)
                    {
                        d36 = d4;
                    }

                    moveEntity(d34, 0.0D, d36);
                    if (i13[0][1] != 0 && MathHelper.floor_double(posX) - i1 == i13[0][0] && MathHelper.floor_double(posZ) - i3 == i13[0][2])
                    {
                        SetPosition(posX, posY + i13[0][1], posZ);
                    }
                    else if (i13[1][1] != 0 && MathHelper.floor_double(posX) - i1 == i13[1][0] && MathHelper.floor_double(posZ) - i3 == i13[1][2])
                    {
                        SetPosition(posX, posY + i13[1][1], posZ);
                    }

                    if (riddenByEntity != null)
                    {
                        motionX *= 0.997F;
                        motionY *= 0.0D;
                        motionZ *= 0.997F;
                    }
                    else
                    {
                        if (minecartType == 2)
                        {
                            d38 = (double)MathHelper.sqrt_double(pushX * pushX + pushZ * pushZ);
                            if (d38 > 0.01D)
                            {
                                pushX /= d38;
                                pushZ /= d38;
                                double d40 = 0.04D;
                                motionX *= 0.8F;
                                motionY *= 0.0D;
                                motionZ *= 0.8F;
                                motionX += pushX * d40;
                                motionZ += pushZ * d40;
                            }
                            else
                            {
                                motionX *= 0.9F;
                                motionY *= 0.0D;
                                motionZ *= 0.9F;
                            }
                        }

                        motionX *= 0.96F;
                        motionY *= 0.0D;
                        motionZ *= 0.96F;
                    }

                    Vec3D vec3D51 = func_514_g(posX, posY, posZ);
                    if (vec3D51 != null && vec3D9 != null)
                    {
                        double d39 = (vec3D9.yCoord - vec3D51.yCoord) * 0.05D;
                        d22 = Math.Sqrt(motionX * motionX + motionZ * motionZ);
                        if (d22 > 0.0D)
                        {
                            motionX = motionX / d22 * (d22 + d39);
                            motionZ = motionZ / d22 * (d22 + d39);
                        }

                        SetPosition(posX, vec3D51.yCoord, posZ);
                    }

                    int i52 = MathHelper.floor_double(posX);
                    int i53 = MathHelper.floor_double(posZ);
                    if (i52 != i1 || i53 != i3)
                    {
                        d22 = Math.Sqrt(motionX * motionX + motionZ * motionZ);
                        motionX = d22 * (i52 - i1);
                        motionZ = d22 * (i53 - i3);
                    }

                    double d41;
                    if (minecartType == 2)
                    {
                        d41 = (double)MathHelper.sqrt_double(pushX * pushX + pushZ * pushZ);
                        if (d41 > 0.01D && motionX * motionX + motionZ * motionZ > 0.001D)
                        {
                            pushX /= d41;
                            pushZ /= d41;
                            if (pushX * motionX + pushZ * motionZ < 0.0D)
                            {
                                pushX = 0.0D;
                                pushZ = 0.0D;
                            }
                            else
                            {
                                pushX = motionX;
                                pushZ = motionZ;
                            }
                        }
                    }

                    if (z11)
                    {
                        d41 = Math.Sqrt(motionX * motionX + motionZ * motionZ);
                        if (d41 > 0.01D)
                        {
                            double d43 = 0.06D;
                            motionX += motionX / d41 * d43;
                            motionZ += motionZ / d41 * d43;
                        }
                        else if (i10 == 1)
                        {
                            if (worldObj.isBlockNormalCube(i1 - 1, i2, i3))
                            {
                                motionX = 0.02D;
                            }
                            else if (worldObj.isBlockNormalCube(i1 + 1, i2, i3))
                            {
                                motionX = -0.02D;
                            }
                        }
                        else if (i10 == 0)
                        {
                            if (worldObj.isBlockNormalCube(i1, i2, i3 - 1))
                            {
                                motionZ = 0.02D;
                            }
                            else if (worldObj.isBlockNormalCube(i1, i2, i3 + 1))
                            {
                                motionZ = -0.02D;
                            }
                        }
                    }
                }
                else
                {
                    if (motionX < -d4)
                    {
                        motionX = -d4;
                    }

                    if (motionX > d4)
                    {
                        motionX = d4;
                    }

                    if (motionZ < -d4)
                    {
                        motionZ = -d4;
                    }

                    if (motionZ > d4)
                    {
                        motionZ = d4;
                    }

                    if (onGround)
                    {
                        motionX *= 0.5D;
                        motionY *= 0.5D;
                        motionZ *= 0.5D;
                    }

                    moveEntity(motionX, motionY, motionZ);
                    if (!onGround)
                    {
                        motionX *= 0.95F;
                        motionY *= 0.95F;
                        motionZ *= 0.95F;
                    }
                }

                rotationPitch = 0.0F;
                double d47 = prevPosX - posX;
                double d48 = prevPosZ - posZ;
                if (d47 * d47 + d48 * d48 > 0.001D)
                {
                    rotationYaw = (float)(Math.Atan2(d48, d47) * 180.0D / Math.PI);
                    if (field_856_i)
                    {
                        rotationYaw += 180.0F;
                    }
                }

                double d49;
                for (d49 = (double)(rotationYaw - prevRotationYaw); d49 >= 180.0D; d49 -= 360.0D)
                {
                }

                while (d49 < -180.0D)
                {
                    d49 += 360.0D;
                }

                if (d49 < -170.0D || d49 >= 170.0D)
                {
                    rotationYaw += 180.0F;
                    field_856_i = !field_856_i;
                }

                setRotation(rotationYaw, rotationPitch);
                System.Collections.IList list15 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand((double)0.2F, 0.0D, (double)0.2F));
                if (list15 != null && list15.Count > 0)
                {
                    for (int i50 = 0; i50 < list15.Count; ++i50)
                    {
                        Entity entity17 = (Entity)list15[i50];
                        if (entity17 != riddenByEntity && entity17.canBePushed() && entity17 is EntityMinecart)
                        {
                            entity17.applyEntityCollision(this);
                        }
                    }
                }

                if (riddenByEntity != null && riddenByEntity.isDead)
                {
                    if (riddenByEntity.ridingEntity == this)
                    {
                        riddenByEntity.ridingEntity = null;
                    }

                    riddenByEntity = null;
                }

                if (fuel > 0)
                {
                    --fuel;
                }

                if (fuel <= 0)
                {
                    pushX = pushZ = 0.0D;
                }

                MinecartPowered = fuel > 0;
            }
        }

        public virtual Vec3D func_515_a(double d1, double d3, double d5, double d7)
        {
            int i9 = MathHelper.floor_double(d1);
            int i10 = MathHelper.floor_double(d3);
            int i11 = MathHelper.floor_double(d5);
            if (BlockRail.isRailBlockAt(worldObj, i9, i10 - 1, i11))
            {
                --i10;
            }

            int i12 = worldObj.getBlockId(i9, i10, i11);
            if (!BlockRail.isRailBlock(i12))
            {
                return null;
            }
            else
            {
                int i13 = worldObj.getBlockMetadata(i9, i10, i11);
                if (((BlockRail)Block.blocksList[i12]).Powered)
                {
                    i13 &= 7;
                }

                d3 = i10;
                if (i13 >= 2 && i13 <= 5)
                {
                    d3 = i10 + 1;
                }

                int[][] i14 = field_855_j[i13];
                double d15 = i14[1][0] - i14[0][0];
                double d17 = i14[1][2] - i14[0][2];
                double d19 = Math.Sqrt(d15 * d15 + d17 * d17);
                d15 /= d19;
                d17 /= d19;
                d1 += d15 * d7;
                d5 += d17 * d7;
                if (i14[0][1] != 0 && MathHelper.floor_double(d1) - i9 == i14[0][0] && MathHelper.floor_double(d5) - i11 == i14[0][2])
                {
                    d3 += i14[0][1];
                }
                else if (i14[1][1] != 0 && MathHelper.floor_double(d1) - i9 == i14[1][0] && MathHelper.floor_double(d5) - i11 == i14[1][2])
                {
                    d3 += i14[1][1];
                }

                return func_514_g(d1, d3, d5);
            }
        }

        public virtual Vec3D func_514_g(double d1, double d3, double d5)
        {
            int i7 = MathHelper.floor_double(d1);
            int i8 = MathHelper.floor_double(d3);
            int i9 = MathHelper.floor_double(d5);
            if (BlockRail.isRailBlockAt(worldObj, i7, i8 - 1, i9))
            {
                --i8;
            }

            int i10 = worldObj.getBlockId(i7, i8, i9);
            if (BlockRail.isRailBlock(i10))
            {
                int i11 = worldObj.getBlockMetadata(i7, i8, i9);
                d3 = i8;
                if (((BlockRail)Block.blocksList[i10]).Powered)
                {
                    i11 &= 7;
                }

                if (i11 >= 2 && i11 <= 5)
                {
                    d3 = i8 + 1;
                }

                int[][] i12 = field_855_j[i11];
                double d13 = 0.0D;
                double d15 = i7 + 0.5D + i12[0][0] * 0.5D;
                double d17 = i8 + 0.5D + i12[0][1] * 0.5D;
                double d19 = i9 + 0.5D + i12[0][2] * 0.5D;
                double d21 = i7 + 0.5D + i12[1][0] * 0.5D;
                double d23 = i8 + 0.5D + i12[1][1] * 0.5D;
                double d25 = i9 + 0.5D + i12[1][2] * 0.5D;
                double d27 = d21 - d15;
                double d29 = (d23 - d17) * 2.0D;
                double d31 = d25 - d19;
                if (d27 == 0.0D)
                {
                    d1 = i7 + 0.5D;
                    d13 = d5 - i9;
                }
                else if (d31 == 0.0D)
                {
                    d5 = i9 + 0.5D;
                    d13 = d1 - i7;
                }
                else
                {
                    double d33 = d1 - d15;
                    double d35 = d5 - d19;
                    double d37 = (d33 * d27 + d35 * d31) * 2.0D;
                    d13 = d37;
                }

                d1 = d15 + d27 * d13;
                d3 = d17 + d29 * d13;
                d5 = d19 + d31 * d13;
                if (d29 < 0.0D)
                {
                    ++d3;
                }

                if (d29 > 0.0D)
                {
                    d3 += 0.5D;
                }

                return Vec3D.createVector(d1, d3, d5);
            }
            else
            {
                return null;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setInteger("Type", minecartType);
            if (minecartType == 2)
            {
                nBTTagCompound1.setDouble("PushX", pushX);
                nBTTagCompound1.setDouble("PushZ", pushZ);
                nBTTagCompound1.setShort("Fuel", (short)fuel);
            }
            else if (minecartType == 1)
            {
                NBTTagList nBTTagList2 = new NBTTagList();

                for (int i3 = 0; i3 < cargoItems.Length; ++i3)
                {
                    if (cargoItems[i3] != null)
                    {
                        NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
                        nBTTagCompound4.setByte("Slot", (sbyte)i3);
                        cargoItems[i3].writeToNBT(nBTTagCompound4);
                        nBTTagList2.appendTag(nBTTagCompound4);
                    }
                }

                nBTTagCompound1.setTag("Items", nBTTagList2);
            }

        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            minecartType = nBTTagCompound1.getInteger("Type");
            if (minecartType == 2)
            {
                pushX = nBTTagCompound1.getDouble("PushX");
                pushZ = nBTTagCompound1.getDouble("PushZ");
                fuel = nBTTagCompound1.getShort("Fuel");
            }
            else if (minecartType == 1)
            {
                NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Items");
                cargoItems = new ItemStack[SizeInventory];

                for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
                {
                    NBTTagCompound nBTTagCompound4 = (NBTTagCompound)nBTTagList2.tagAt(i3);
                    int i5 = nBTTagCompound4.getByte("Slot") & 255;
                    if (i5 >= 0 && i5 < cargoItems.Length)
                    {
                        cargoItems[i5] = ItemStack.loadItemStackFromNBT(nBTTagCompound4);
                    }
                }
            }

        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public override void applyEntityCollision(Entity entity1)
        {
            if (!worldObj.isRemote)
            {
                if (entity1 != riddenByEntity)
                {
                    if (entity1 is EntityLiving && !(entity1 is EntityPlayer) && !(entity1 is EntityIronGolem) && minecartType == 0 && motionX * motionX + motionZ * motionZ > 0.01D && riddenByEntity == null && entity1.ridingEntity == null)
                    {
                        entity1.mountEntity(this);
                    }

                    double d2 = entity1.posX - posX;
                    double d4 = entity1.posZ - posZ;
                    double d6 = d2 * d2 + d4 * d4;
                    if (d6 >= 9.999999747378752E-5D)
                    {
                        d6 = (double)MathHelper.sqrt_double(d6);
                        d2 /= d6;
                        d4 /= d6;
                        double d8 = 1.0D / d6;
                        if (d8 > 1.0D)
                        {
                            d8 = 1.0D;
                        }

                        d2 *= d8;
                        d4 *= d8;
                        d2 *= (double)0.1F;
                        d4 *= (double)0.1F;
                        d2 *= (double)(1.0F - entityCollisionReduction);
                        d4 *= (double)(1.0F - entityCollisionReduction);
                        d2 *= 0.5D;
                        d4 *= 0.5D;
                        if (entity1 is EntityMinecart)
                        {
                            double d10 = entity1.posX - posX;
                            double d12 = entity1.posZ - posZ;
                            Vec3D vec3D14 = Vec3D.createVector(d10, 0.0D, d12).normalize();
                            Vec3D vec3D15 = Vec3D.createVector((double)MathHelper.cos(rotationYaw * (float)Math.PI / 180.0F), 0.0D, (double)MathHelper.sin(rotationYaw * (float)Math.PI / 180.0F)).normalize();
                            double d16 = Math.Abs(vec3D14.dotProduct(vec3D15));
                            if (d16 < (double)0.8F)
                            {
                                return;
                            }

                            double d18 = entity1.motionX + motionX;
                            double d20 = entity1.motionZ + motionZ;
                            if (((EntityMinecart)entity1).minecartType == 2 && minecartType != 2)
                            {
                                motionX *= 0.2F;
                                motionZ *= 0.2F;
                                addVelocity(entity1.motionX - d2, 0.0D, entity1.motionZ - d4);
                                entity1.motionX *= 0.95F;
                                entity1.motionZ *= 0.95F;
                            }
                            else if (((EntityMinecart)entity1).minecartType != 2 && minecartType == 2)
                            {
                                entity1.motionX *= 0.2F;
                                entity1.motionZ *= 0.2F;
                                entity1.addVelocity(motionX + d2, 0.0D, motionZ + d4);
                                motionX *= 0.95F;
                                motionZ *= 0.95F;
                            }
                            else
                            {
                                d18 /= 2.0D;
                                d20 /= 2.0D;
                                motionX *= 0.2F;
                                motionZ *= 0.2F;
                                addVelocity(d18 - d2, 0.0D, d20 - d4);
                                entity1.motionX *= 0.2F;
                                entity1.motionZ *= 0.2F;
                                entity1.addVelocity(d18 + d2, 0.0D, d20 + d4);
                            }
                        }
                        else
                        {
                            addVelocity(-d2, 0.0D, -d4);
                            entity1.addVelocity(d2 / 4.0D, 0.0D, d4 / 4.0D);
                        }
                    }

                }
            }
        }

        public virtual int SizeInventory
        {
            get
            {
                return 27;
            }
        }

        public virtual ItemStack getStackInSlot(int i1)
        {
            return cargoItems[i1];
        }

        public virtual ItemStack decrStackSize(int i1, int i2)
        {
            if (cargoItems[i1] != null)
            {
                ItemStack itemStack3;
                if (cargoItems[i1].stackSize <= i2)
                {
                    itemStack3 = cargoItems[i1];
                    cargoItems[i1] = null;
                    return itemStack3;
                }
                else
                {
                    itemStack3 = cargoItems[i1].splitStack(i2);
                    if (cargoItems[i1].stackSize == 0)
                    {
                        cargoItems[i1] = null;
                    }

                    return itemStack3;
                }
            }
            else
            {
                return null;
            }
        }

        public virtual ItemStack getStackInSlotOnClosing(int i1)
        {
            if (cargoItems[i1] != null)
            {
                ItemStack itemStack2 = cargoItems[i1];
                cargoItems[i1] = null;
                return itemStack2;
            }
            else
            {
                return null;
            }
        }

        public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
        {
            cargoItems[i1] = itemStack2;
            if (itemStack2 != null && itemStack2.stackSize > InventoryStackLimit)
            {
                itemStack2.stackSize = InventoryStackLimit;
            }

        }

        public virtual string InvName
        {
            get
            {
                return "container.minecart";
            }
        }

        public virtual int InventoryStackLimit
        {
            get
            {
                return 64;
            }
        }

        public virtual void onInventoryChanged()
        {
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            if (minecartType == 0)
            {
                if (riddenByEntity != null && riddenByEntity is EntityPlayer && riddenByEntity != entityPlayer1)
                {
                    return true;
                }

                if (!worldObj.isRemote)
                {
                    entityPlayer1.mountEntity(this);
                }
            }
            else if (minecartType == 1)
            {
                if (!worldObj.isRemote)
                {
                    entityPlayer1.displayGUIChest(this);
                }
            }
            else if (minecartType == 2)
            {
                ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
                if (itemStack2 != null && itemStack2.itemID == Item.coal.shiftedIndex)
                {
                    if (--itemStack2.stackSize == 0)
                    {
                        entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, null);
                    }

                    fuel += 3600;
                }

                pushX = posX - entityPlayer1.posX;
                pushZ = posZ - entityPlayer1.posZ;
            }

            return true;
        }

        public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
        {
            minecartX = d1;
            minecartY = d3;
            minecartZ = d5;
            minecartYaw = f7;
            minecartPitch = f8;
            turnProgress = i9 + 2;
            motionX = velocityX;
            motionY = velocityY;
            motionZ = velocityZ;
        }

        public override void setVelocity(double d1, double d3, double d5)
        {
            velocityX = motionX = d1;
            velocityY = motionY = d3;
            velocityZ = motionZ = d5;
        }

        public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
        {
            return isDead ? false : entityPlayer1.getDistanceSqToEntity(this) <= 64.0D;
        }

        protected internal virtual bool MinecartPowered
        {
            get
            {
                return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
            }
            set
            {
                if (value)
                {
                    dataWatcher.updateObject(16, (sbyte)(dataWatcher.getWatchableObjectByte(16) | 1));
                }
                else
                {
                    dataWatcher.updateObject(16, (sbyte)(dataWatcher.getWatchableObjectByte(16) & -2));
                }

            }
        }


        public virtual void openChest()
        {
        }

        public virtual void closeChest()
        {
        }

        public virtual void func_41024_b(int i1)
        {
            dataWatcher.updateObject(19, i1);
        }

        public virtual int func_41025_i()
        {
            return dataWatcher.getWatchableObjectInt(19);
        }

        public virtual void func_41028_c(int i1)
        {
            dataWatcher.updateObject(17, i1);
        }

        public virtual int func_41023_l()
        {
            return dataWatcher.getWatchableObjectInt(17);
        }

        public virtual void func_41029_h(int i1)
        {
            dataWatcher.updateObject(18, i1);
        }

        public virtual int func_41030_m()
        {
            return dataWatcher.getWatchableObjectInt(18);
        }
    }

}