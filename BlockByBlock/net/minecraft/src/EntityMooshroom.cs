namespace net.minecraft.src
{
	public class EntityMooshroom : EntityCow
	{
		public EntityMooshroom(World world1) : base(world1)
		{
			this.texture = "/mob/redcow.png";
			this.setSize(0.9F, 1.3F);
		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
			if (itemStack2 != null && itemStack2.itemID == Item.bowlEmpty.shiftedIndex && this.GrowingAge >= 0)
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

			if (itemStack2 != null && itemStack2.itemID == Item.shears.shiftedIndex && this.GrowingAge >= 0)
			{
				this.setDead();
				this.worldObj.spawnParticle("largeexplode", this.posX, this.posY + (double)(this.height / 2.0F), this.posZ, 0.0D, 0.0D, 0.0D);
				if (!this.worldObj.isRemote)
				{
					EntityCow entityCow3 = new EntityCow(this.worldObj);
					entityCow3.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);
					entityCow3.EntityHealth = this.Health;
					entityCow3.renderYawOffset = this.renderYawOffset;
					this.worldObj.spawnEntityInWorld(entityCow3);

					for (int i4 = 0; i4 < 5; ++i4)
					{
						this.worldObj.spawnEntityInWorld(new EntityItem(this.worldObj, this.posX, this.posY + (double)this.height, this.posZ, new ItemStack(Block.mushroomRed)));
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
			return new EntityMooshroom(this.worldObj);
		}
	}

}