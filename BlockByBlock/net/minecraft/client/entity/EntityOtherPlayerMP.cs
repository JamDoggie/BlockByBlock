using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityOtherPlayerMP : EntityPlayer
    {
        private bool isItemInUse = false;
        private int otherPlayerMPPosRotationIncrements;
        private double otherPlayerMPX;
        private double otherPlayerMPY;
        private double otherPlayerMPZ;
        private double otherPlayerMPYaw;
        private double otherPlayerMPPitch;

        public EntityOtherPlayerMP(World world1, string string2) : base(world1)
        {
            username = string2;
            yOffset = 0.0F;
            stepHeight = 0.0F;
            if (!ReferenceEquals(string2, null) && string2.Length > 0)
            {
                skinUrl = "http://s3.amazonaws.com/MinecraftSkins/" + string2 + ".png";
            }

            noClip = true;
            field_22062_y = 0.25F;
            renderDistanceWeight = 10.0D;
        }

        protected internal override void resetHeight()
        {
            yOffset = 0.0F;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            return true;
        }

        public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
        {
            otherPlayerMPX = d1;
            otherPlayerMPY = d3;
            otherPlayerMPZ = d5;
            otherPlayerMPYaw = f7;
            otherPlayerMPPitch = f8;
            otherPlayerMPPosRotationIncrements = i9;
        }

        public override void onUpdate()
        {
            field_22062_y = 0.0F;
            base.onUpdate();
            field_705_Q = field_704_R;
            double d1 = posX - prevPosX;
            double d3 = posZ - prevPosZ;
            float f5 = MathHelper.sqrt_double(d1 * d1 + d3 * d3) * 4.0F;
            if (f5 > 1.0F)
            {
                f5 = 1.0F;
            }

            field_704_R += (f5 - field_704_R) * 0.4F;
            field_703_S += field_704_R;
            if (!isItemInUse && Eating && inventory.mainInventory[inventory.currentItem] != null)
            {
                ItemStack itemStack6 = inventory.mainInventory[inventory.currentItem];
                setItemInUse(inventory.mainInventory[inventory.currentItem], Item.itemsList[itemStack6.itemID].getMaxItemUseDuration(itemStack6));
                isItemInUse = true;
            }
            else if (isItemInUse && !Eating)
            {
                clearItemInUse();
                isItemInUse = false;
            }

        }

        public override float ShadowSize
        {
            get
            {
                return 0.0F;
            }
        }

        public override void onLivingUpdate()
        {
            base.updateEntityActionState();
            if (otherPlayerMPPosRotationIncrements > 0)
            {
                double d1 = posX + (otherPlayerMPX - posX) / otherPlayerMPPosRotationIncrements;
                double d3 = posY + (otherPlayerMPY - posY) / otherPlayerMPPosRotationIncrements;
                double d5 = posZ + (otherPlayerMPZ - posZ) / otherPlayerMPPosRotationIncrements;

                double d7;
                for (d7 = otherPlayerMPYaw - rotationYaw; d7 < -180.0D; d7 += 360.0D)
                {
                }

                while (d7 >= 180.0D)
                {
                    d7 -= 360.0D;
                }

                rotationYaw = (float)(rotationYaw + d7 / otherPlayerMPPosRotationIncrements);
                rotationPitch = (float)(rotationPitch + (otherPlayerMPPitch - rotationPitch) / otherPlayerMPPosRotationIncrements);
                --otherPlayerMPPosRotationIncrements;
                SetPosition(d1, d3, d5);
                setRotation(rotationYaw, rotationPitch);
            }

            prevCameraYaw = cameraYaw;
            float f9 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
            float f2 = (float)Math.Atan(-motionY * (double)0.2F) * 15.0F;
            if (f9 > 0.1F)
            {
                f9 = 0.1F;
            }

            if (!onGround || Health <= 0)
            {
                f9 = 0.0F;
            }

            if (onGround || Health <= 0)
            {
                f2 = 0.0F;
            }

            cameraYaw += (f9 - cameraYaw) * 0.4F;
            cameraPitch += (f2 - cameraPitch) * 0.8F;
        }

        public override void outfitWithItem(int i1, int i2, int i3)
        {
            ItemStack itemStack4 = null;
            if (i2 >= 0)
            {
                itemStack4 = new ItemStack(i2, 1, i3);
            }

            if (i1 == 0)
            {
                inventory.mainInventory[inventory.currentItem] = itemStack4;
            }
            else
            {
                inventory.armorInventory[i1 - 1] = itemStack4;
            }

        }

        public override void func_6420_o()
        {
        }

        public override float EyeHeight
        {
            get
            {
                return 1.82F;
            }
        }
    }

}