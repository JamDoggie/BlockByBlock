using System.Collections;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public class EntityPainting : Entity
    {
        private int tickCounter1;
        public int direction;
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public EnumArt art;

        public EntityPainting(World world1) : base(world1)
        {
            tickCounter1 = 0;
            direction = 0;
            yOffset = 0.0F;
            SetSize(0.5F, 0.5F);
        }

        public EntityPainting(World world1, int i2, int i3, int i4, int i5) : this(world1)
        {
            xPosition = i2;
            yPosition = i3;
            zPosition = i4;
            ArrayList arrayList6 = new ArrayList();
            EnumArt[] enumArt7 = EnumArt.values();
            int i8 = enumArt7.Length;

            for (int i9 = 0; i9 < i8; ++i9)
            {
                EnumArt enumArt10 = enumArt7[i9];
                art = enumArt10;
                func_412_b(i5);
                if (onValidSurface())
                {
                    arrayList6.Add(enumArt10);
                }
            }

            if (arrayList6.Count > 0)
            {
                art = (EnumArt)arrayList6[rand.Next(arrayList6.Count)];
            }

            func_412_b(i5);
        }

        public EntityPainting(World world1, int i2, int i3, int i4, int i5, string string6) : this(world1)
        {
            xPosition = i2;
            yPosition = i3;
            zPosition = i4;
            EnumArt[] enumArt7 = EnumArt.values();
            int i8 = enumArt7.Length;

            for (int i9 = 0; i9 < i8; ++i9)
            {
                EnumArt enumArt10 = enumArt7[i9];
                if (enumArt10.title.Equals(string6))
                {
                    art = enumArt10;
                    break;
                }
            }

            func_412_b(i5);
        }

        protected internal override void entityInit()
        {
        }

        public virtual void func_412_b(int i1)
        {
            direction = i1;
            prevRotationYaw = rotationYaw = i1 * 90;
            float f2 = art.sizeX;
            float f3 = art.sizeY;
            float f4 = art.sizeX;
            if (i1 != 0 && i1 != 2)
            {
                f2 = 0.5F;
            }
            else
            {
                f4 = 0.5F;
            }

            f2 /= 32.0F;
            f3 /= 32.0F;
            f4 /= 32.0F;
            float f5 = xPosition + 0.5F;
            float f6 = yPosition + 0.5F;
            float f7 = zPosition + 0.5F;
            float f8 = 0.5625F;
            if (i1 == 0)
            {
                f7 -= f8;
            }

            if (i1 == 1)
            {
                f5 -= f8;
            }

            if (i1 == 2)
            {
                f7 += f8;
            }

            if (i1 == 3)
            {
                f5 += f8;
            }

            if (i1 == 0)
            {
                f5 -= func_411_c(art.sizeX);
            }

            if (i1 == 1)
            {
                f7 += func_411_c(art.sizeX);
            }

            if (i1 == 2)
            {
                f5 += func_411_c(art.sizeX);
            }

            if (i1 == 3)
            {
                f7 -= func_411_c(art.sizeX);
            }

            f6 += func_411_c(art.sizeY);
            SetPosition((double)f5, (double)f6, (double)f7);
            float f9 = -0.00625F;
            boundingBox.setBounds((double)(f5 - f2 - f9), (double)(f6 - f3 - f9), (double)(f7 - f4 - f9), (double)(f5 + f2 + f9), (double)(f6 + f3 + f9), (double)(f7 + f4 + f9));
        }

        private float func_411_c(int i1)
        {
            return i1 == 32 ? 0.5F : i1 == 64 ? 0.5F : 0.0F;
        }

        public override void onUpdate()
        {
            if (tickCounter1++ == 100 && !worldObj.isRemote)
            {
                tickCounter1 = 0;
                if (!isDead && !onValidSurface())
                {
                    setDead();
                    worldObj.spawnEntityInWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.painting)));
                }
            }

        }

        public virtual bool onValidSurface()
        {
            if (worldObj.getCollidingBoundingBoxes(this, boundingBox).Count > 0)
            {
                return false;
            }
            else
            {
                int i1 = art.sizeX / 16;
                int i2 = art.sizeY / 16;
                int i3 = xPosition;
                int i4 = yPosition;
                int i5 = zPosition;
                if (direction == 0)
                {
                    i3 = MathHelper.floor_double(posX - (double)(art.sizeX / 32.0F));
                }

                if (direction == 1)
                {
                    i5 = MathHelper.floor_double(posZ - (double)(art.sizeX / 32.0F));
                }

                if (direction == 2)
                {
                    i3 = MathHelper.floor_double(posX - (double)(art.sizeX / 32.0F));
                }

                if (direction == 3)
                {
                    i5 = MathHelper.floor_double(posZ - (double)(art.sizeX / 32.0F));
                }

                i4 = MathHelper.floor_double(posY - (double)(art.sizeY / 32.0F));

                int i7;
                for (int i6 = 0; i6 < i1; ++i6)
                {
                    for (i7 = 0; i7 < i2; ++i7)
                    {
                        Material material8;
                        if (direction != 0 && direction != 2)
                        {
                            material8 = worldObj.getBlockMaterial(xPosition, i4 + i7, i5 + i6);
                        }
                        else
                        {
                            material8 = worldObj.getBlockMaterial(i3 + i6, i4 + i7, zPosition);
                        }

                        if (!material8.Solid)
                        {
                            return false;
                        }
                    }
                }

                IList list9 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox);

                for (i7 = 0; i7 < list9.Count; ++i7)
                {
                    if (list9[i7] is EntityPainting)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public override bool canBeCollidedWith()
        {
            return true;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (!isDead && !worldObj.isRemote)
            {
                setDead();
                setBeenAttacked();
                worldObj.spawnEntityInWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.painting)));
            }

            return true;
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setByte("Dir", (sbyte)direction);
            nBTTagCompound1.setString("Motive", art.title);
            nBTTagCompound1.setInteger("TileX", xPosition);
            nBTTagCompound1.setInteger("TileY", yPosition);
            nBTTagCompound1.setInteger("TileZ", zPosition);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            direction = nBTTagCompound1.getByte("Dir");
            xPosition = nBTTagCompound1.getInteger("TileX");
            yPosition = nBTTagCompound1.getInteger("TileY");
            zPosition = nBTTagCompound1.getInteger("TileZ");
            string string2 = nBTTagCompound1.getString("Motive");
            EnumArt[] enumArt3 = EnumArt.values();
            int i4 = enumArt3.Length;

            for (int i5 = 0; i5 < i4; ++i5)
            {
                EnumArt enumArt6 = enumArt3[i5];
                if (enumArt6.title.Equals(string2))
                {
                    art = enumArt6;
                }
            }

            if (art == null)
            {
                art = EnumArt.Kebab;
            }

            func_412_b(direction);
        }

        public override void moveEntity(double d1, double d3, double d5)
        {
            if (!worldObj.isRemote && !isDead && d1 * d1 + d3 * d3 + d5 * d5 > 0.0D)
            {
                setDead();
                worldObj.spawnEntityInWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.painting)));
            }

        }

        public override void addVelocity(double d1, double d3, double d5)
        {
            if (!worldObj.isRemote && !isDead && d1 * d1 + d3 * d3 + d5 * d5 > 0.0D)
            {
                setDead();
                worldObj.spawnEntityInWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.painting)));
            }

        }
    }

}