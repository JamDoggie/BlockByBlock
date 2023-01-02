using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public class EntityMooshroom : EntityCow
    {
        public EntityMooshroom(World world1) : base(world1)
        {
            texture = "/mob/redcow.png";
            SetSize(0.9F, 1.3F);
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
            if (itemStack2 != null && itemStack2.itemID == Item.bowlEmpty.shiftedIndex && GrowingAge >= 0)
            {
                if (itemStack2.stackSize == 1)
                {
                    entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, new ItemStack(Item.bowlSoup));
                    return true;
                }

                if (entityPlayer1.inventory.addItemStackToInventory(new ItemStack(Item.bowlSoup)) && !entityPlayer1.capabilities.isCreativeMode)
                {
                    entityPlayer1.inventory.decrStackSize(entityPlayer1.inventory.currentItem, 1);
                    return true;
                }
            }

            if (itemStack2 != null && itemStack2.itemID == Item.shears.shiftedIndex && GrowingAge >= 0)
            {
                setDead();
                worldObj.spawnParticle("largeexplode", posX, posY + (double)(height / 2.0F), posZ, 0.0D, 0.0D, 0.0D);
                if (!worldObj.isRemote)
                {
                    EntityCow entityCow3 = new EntityCow(worldObj);
                    entityCow3.setLocationAndAngles(posX, posY, posZ, rotationYaw, rotationPitch);
                    entityCow3.EntityHealth = Health;
                    entityCow3.renderYawOffset = renderYawOffset;
                    worldObj.spawnEntityInWorld(entityCow3);

                    for (int i4 = 0; i4 < 5; ++i4)
                    {
                        worldObj.spawnEntityInWorld(new EntityItem(worldObj, posX, posY + height, posZ, new ItemStack(Block.mushroomRed)));
                    }
                }

                return true;
            }
            else
            {
                return base.interact(entityPlayer1);
            }
        }

        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            return new EntityMooshroom(worldObj);
        }
    }

}