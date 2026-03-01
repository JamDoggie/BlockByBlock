using System;

namespace net.minecraft.src
{

	public abstract class EntityAnimal : EntityAgeable
	{
		private int inLove;
		private int breeding = 0;

		public EntityAnimal(World world1) : base(world1)
		{
		}

		protected internal override void updateAITick()
		{
			if (this.GrowingAge != 0)
			{
				this.inLove = 0;
			}

			base.updateAITick();
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			if (this.GrowingAge != 0)
			{
				this.inLove = 0;
			}

			if (this.inLove > 0)
			{
				--this.inLove;
				string string1 = "heart";
				if (this.inLove % 10 == 0)
				{
					double d2 = this.rand.NextGaussian() * 0.02D;
					double d4 = this.rand.NextGaussian() * 0.02D;
					double d6 = this.rand.NextGaussian() * 0.02D;
					this.worldObj.spawnParticle(string1, this.posX + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.NextSingle() * this.height), this.posZ + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, d2, d4, d6);
				}
			}
			else
			{
				this.breeding = 0;
			}

		}

		protected internal override void attackEntity(Entity entity1, float f2)
		{
			if (entity1 is EntityPlayer)
			{
				if (f2 < 3.0F)
				{
					double d3 = entity1.posX - this.posX;
					double d5 = entity1.posZ - this.posZ;
					this.rotationYaw = (float)(Math.Atan2(d5, d3) * 180.0D / (double)(float)Math.PI) - 90.0F;
					this.hasAttacked = true;
				}

				EntityPlayer entityPlayer7 = (EntityPlayer)entity1;
				if (entityPlayer7.CurrentEquippedItem == null || !this.isWheat(entityPlayer7.CurrentEquippedItem))
				{
					this.entityToAttack = null;
				}
			}
			else if (entity1 is EntityAnimal)
			{
				EntityAnimal entityAnimal8 = (EntityAnimal)entity1;
				if (this.GrowingAge > 0 && entityAnimal8.GrowingAge < 0)
				{
					if ((double)f2 < 2.5D)
					{
						this.hasAttacked = true;
					}
				}
				else if (this.inLove > 0 && entityAnimal8.inLove > 0)
				{
					if (entityAnimal8.entityToAttack == null)
					{
						entityAnimal8.entityToAttack = this;
					}

					if (entityAnimal8.entityToAttack == this && (double)f2 < 3.5D)
					{
						++entityAnimal8.inLove;
						++this.inLove;
						++this.breeding;
						if (this.breeding % 4 == 0)
						{
							this.worldObj.spawnParticle("heart", this.posX + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.NextSingle() * this.height), this.posZ + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, 0.0D, 0.0D, 0.0D);
						}

						if (this.breeding == 60)
						{
							this.procreate((EntityAnimal)entity1);
						}
					}
					else
					{
						this.breeding = 0;
					}
				}
				else
				{
					this.breeding = 0;
					this.entityToAttack = null;
				}
			}

		}

		private void procreate(EntityAnimal entityAnimal1)
		{
			EntityAnimal entityAnimal2 = this.spawnBabyAnimal(entityAnimal1);
			if (entityAnimal2 != null)
			{
				this.GrowingAge = 6000;
				entityAnimal1.GrowingAge = 6000;
				this.inLove = 0;
				this.breeding = 0;
				this.entityToAttack = null;
				entityAnimal1.entityToAttack = null;
				entityAnimal1.breeding = 0;
				entityAnimal1.inLove = 0;
				entityAnimal2.GrowingAge = -24000;
				entityAnimal2.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, this.rotationPitch);

				for (int i3 = 0; i3 < 7; ++i3)
				{
					double d4 = this.rand.NextGaussian() * 0.02D;
					double d6 = this.rand.NextGaussian() * 0.02D;
					double d8 = this.rand.NextGaussian() * 0.02D;
					this.worldObj.spawnParticle("heart", this.posX + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.NextSingle() * this.height), this.posZ + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, d4, d6, d8);
				}

				this.worldObj.spawnEntityInWorld(entityAnimal2);
			}

		}

		public abstract EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1);

		protected internal override void attackBlockedEntity(Entity entity1, float f2)
		{
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			this.fleeingTick = 60;
			this.entityToAttack = null;
			this.inLove = 0;
			return base.attackEntityFrom(damageSource1, i2);
		}

		public override float getBlockPathWeight(int i1, int i2, int i3)
		{
			return this.worldObj.getBlockId(i1, i2 - 1, i3) == Block.grass.blockID ? 10.0F : this.worldObj.getLightBrightness(i1, i2, i3) - 0.5F;
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setInteger("InLove", this.inLove);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.inLove = nBTTagCompound1.getInteger("InLove");
		}

		protected internal override Entity findPlayerToAttack()
		{
			if (this.fleeingTick > 0)
			{
				return null;
			}
			else
			{
				float f1 = 8.0F;
				System.Collections.IList list2;
				int i3;
				EntityAnimal entityAnimal4;
				if (this.inLove > 0)
				{
					list2 = this.worldObj.getEntitiesWithinAABB(this.GetType(), this.boundingBox.expand((double)f1, (double)f1, (double)f1));

					for (i3 = 0; i3 < list2.Count; ++i3)
					{
						entityAnimal4 = (EntityAnimal)list2[i3];
						if (entityAnimal4 != this && entityAnimal4.inLove > 0)
						{
							return entityAnimal4;
						}
					}
				}
				else if (this.GrowingAge == 0)
				{
					list2 = this.worldObj.getEntitiesWithinAABB(typeof(EntityPlayer), this.boundingBox.expand((double)f1, (double)f1, (double)f1));

					for (i3 = 0; i3 < list2.Count; ++i3)
					{
						EntityPlayer entityPlayer5 = (EntityPlayer)list2[i3];
						if (entityPlayer5.CurrentEquippedItem != null && this.isWheat(entityPlayer5.CurrentEquippedItem))
						{
							return entityPlayer5;
						}
					}
				}
				else if (this.GrowingAge > 0)
				{
					list2 = this.worldObj.getEntitiesWithinAABB(this.GetType(), this.boundingBox.expand((double)f1, (double)f1, (double)f1));

					for (i3 = 0; i3 < list2.Count; ++i3)
					{
						entityAnimal4 = (EntityAnimal)list2[i3];
						if (entityAnimal4 != this && entityAnimal4.GrowingAge < 0)
						{
							return entityAnimal4;
						}
					}
				}

				return null;
			}
		}

		public override bool CanSpawnHere
		{
			get
			{
				int i1 = MathHelper.floor_double(this.posX);
				int i2 = MathHelper.floor_double(this.boundingBox.minY);
				int i3 = MathHelper.floor_double(this.posZ);
				return this.worldObj.getBlockId(i1, i2 - 1, i3) == Block.grass.blockID && this.worldObj.getFullBlockLightValue(i1, i2, i3) > 8 && base.CanSpawnHere;
			}
		}

		public override int TalkInterval
		{
			get
			{
				return 120;
			}
		}

		protected internal override bool canDespawn()
		{
			return false;
		}

		protected internal override int getExperiencePoints(EntityPlayer entityPlayer1)
		{
			return 1 + this.worldObj.rand.Next(3);
		}

		public virtual bool isWheat(ItemStack itemStack1)
		{
			return itemStack1.itemID == Item.wheat.shiftedIndex;
		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
			if (itemStack2 != null && this.isWheat(itemStack2) && this.GrowingAge == 0)
			{
				if (!entityPlayer1.capabilities.isCreativeMode)
				{
					--itemStack2.stackSize;
					if (itemStack2.stackSize <= 0)
					{
						entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, (ItemStack)null);
					}
				}

				this.inLove = 600;
				this.entityToAttack = null;

				for (int i3 = 0; i3 < 7; ++i3)
				{
					double d4 = this.rand.NextGaussian() * 0.02D;
					double d6 = this.rand.NextGaussian() * 0.02D;
					double d8 = this.rand.NextGaussian() * 0.02D;
					this.worldObj.spawnParticle("heart", this.posX + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.NextSingle() * this.height), this.posZ + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, d4, d6, d8);
				}

				return true;
			}
			else
			{
				return base.interact(entityPlayer1);
			}
		}

		public virtual bool InLove
		{
			get
			{
				return this.inLove > 0;
			}
		}

		public virtual void resetInLove()
		{
			this.inLove = 0;
		}

		public virtual bool canMateWith(EntityAnimal entityAnimal1)
		{
			return entityAnimal1 == this ? false : (entityAnimal1.GetType() != this.GetType() ? false : this.InLove && entityAnimal1.InLove);
		}
	}

}