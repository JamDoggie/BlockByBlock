using System;

namespace net.minecraft.src
{
	public class EntityOcelot : EntityTameable
	{
		private EntityAITempt aiTempt;

		public EntityOcelot(World world1) : base(world1)
		{
			this.texture = "/mob/ozelot.png";
			this.setSize(0.6F, 0.8F);
			this.Navigator.func_48664_a(true);
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, this.aiSit);
			this.tasks.addTask(3, this.aiTempt = new EntityAITempt(this, 0.18F, Item.fishRaw.shiftedIndex, true));
			this.tasks.addTask(4, new EntityAIAvoidEntity(this, typeof(EntityPlayer), 16.0F, 0.23F, 0.4F));
			this.tasks.addTask(5, new EntityAIFollowOwner(this, 0.3F, 10.0F, 5.0F));
			this.tasks.addTask(6, new EntityAIOcelotSit(this, 0.4F));
			this.tasks.addTask(7, new EntityAILeapAtTarget(this, 0.3F));
			this.tasks.addTask(8, new EntityAIOcelotAttack(this));
			this.tasks.addTask(9, new EntityAIMate(this, 0.23F));
			this.tasks.addTask(10, new EntityAIWander(this, 0.23F));
			this.tasks.addTask(11, new EntityAIWatchClosest(this, typeof(EntityPlayer), 10.0F));
			this.targetTasks.addTask(1, new EntityAITargetNonTamed(this, typeof(EntityChicken), 14.0F, 750, false));
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(18, (sbyte)0);
		}

		protected internal override void updateAITick()
		{
			if (!this.MoveHelper.func_48186_a())
			{
				this.Sneaking = false;
				this.Sprinting = false;
			}
			else
			{
				float f1 = this.MoveHelper.Speed;
				if (f1 == 0.18F)
				{
					this.Sneaking = true;
					this.Sprinting = false;
				}
				else if (f1 == 0.4F)
				{
					this.Sneaking = false;
					this.Sprinting = true;
				}
				else
				{
					this.Sneaking = false;
					this.Sprinting = false;
				}
			}

		}

		protected internal override bool canDespawn()
		{
			return !this.Tamed;
		}

		public override string Texture
		{
			get
			{
				switch (this.func_48148_ad())
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
			nBTTagCompound1.setInteger("CatType", this.func_48148_ad());
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.func_48147_c(nBTTagCompound1.getInteger("CatType"));
		}

		protected internal override string LivingSound
		{
			get
			{
				return this.Tamed ? (this.InLove ? "mob.cat.purr" : (this.rand.Next(4) == 0 ? "mob.cat.purreow" : "mob.cat.meow")) : "";
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
			this.aiSit.func_48407_a(false);
			return base.attackEntityFrom(damageSource1, i2);
		}

		protected internal override void dropFewItems(bool z1, int i2)
		{
		}

		public override bool interact(EntityPlayer entityPlayer1)
		{
			ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
			if (!this.Tamed)
			{
				if (this.aiTempt.func_48270_h() && itemStack2 != null && itemStack2.itemID == Item.fishRaw.shiftedIndex && entityPlayer1.getDistanceSqToEntity(this) < 9.0D)
				{
					--itemStack2.stackSize;
					if (itemStack2.stackSize <= 0)
					{
						entityPlayer1.inventory.setInventorySlotContents(entityPlayer1.inventory.currentItem, (ItemStack)null);
					}

					if (!this.worldObj.isRemote)
					{
						if (this.rand.Next(3) == 0)
						{
							this.Tamed = true;
							this.func_48147_c(1 + this.worldObj.rand.Next(3));
							this.setOwner(entityPlayer1.username);
							this.func_48142_a(true);
							this.aiSit.func_48407_a(true);
							this.worldObj.setEntityState(this, (sbyte)7);
						}
						else
						{
							this.func_48142_a(false);
							this.worldObj.setEntityState(this, (sbyte)6);
						}
					}
				}

				return true;
			}
			else
			{
				if (entityPlayer1.username.Equals(this.OwnerName, StringComparison.OrdinalIgnoreCase) && !this.worldObj.isRemote && !this.isWheat(itemStack2))
				{
					this.aiSit.func_48407_a(!this.Sitting);
				}

				return base.interact(entityPlayer1);
			}
		}

		public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
		{
			EntityOcelot entityOcelot2 = new EntityOcelot(this.worldObj);
			if (this.Tamed)
			{
				entityOcelot2.setOwner(this.OwnerName);
				entityOcelot2.Tamed = true;
				entityOcelot2.func_48147_c(this.func_48148_ad());
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
			else if (!this.Tamed)
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
				return !entityOcelot2.Tamed ? false : this.InLove && entityOcelot2.InLove;
			}
		}

		public virtual int func_48148_ad()
		{
			return this.dataWatcher.getWatchableObjectByte(18);
		}

		public virtual void func_48147_c(int i1)
		{
			this.dataWatcher.updateObject(18, (sbyte)i1);
		}

		public override bool CanSpawnHere
		{
			get
			{
				if (this.worldObj.rand.Next(3) == 0)
				{
					return false;
				}
				else
				{
					if (this.worldObj.checkIfAABBIsClear(this.boundingBox) && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Count == 0 && !this.worldObj.isAnyLiquid(this.boundingBox))
					{
						int i1 = MathHelper.floor_double(this.posX);
						int i2 = MathHelper.floor_double(this.boundingBox.minY);
						int i3 = MathHelper.floor_double(this.posZ);
						if (i2 < 63)
						{
							return false;
						}
    
						int i4 = this.worldObj.getBlockId(i1, i2 - 1, i3);
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