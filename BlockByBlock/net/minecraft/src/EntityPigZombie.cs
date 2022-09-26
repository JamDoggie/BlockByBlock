namespace net.minecraft.src
{

	public class EntityPigZombie : EntityZombie
	{
		private int angerLevel = 0;
		private int randomSoundDelay = 0;
		private static readonly ItemStack defaultHeldItem = new ItemStack(Item.swordGold, 1);

		public EntityPigZombie(World world1) : base(world1)
		{
			this.texture = "/mob/pigzombie.png";
			this.moveSpeed = 0.5F;
			this.attackStrength = 5;
			this.isImmuneToFire = true;
		}

		protected internal override bool AIEnabled
		{
			get
			{
				return false;
			}
		}

		public override void onUpdate()
		{
			this.moveSpeed = this.entityToAttack != null ? 0.95F : 0.5F;
			if (this.randomSoundDelay > 0 && --this.randomSoundDelay == 0)
			{
				this.worldObj.playSoundAtEntity(this, "mob.zombiepig.zpigangry", this.SoundVolume * 2.0F, ((this.rand.NextSingle() - this.rand.NextSingle()) * 0.2F + 1.0F) * 1.8F);
			}

			base.onUpdate();
		}

		public override bool CanSpawnHere
		{
			get
			{
				return this.worldObj.difficultySetting > 0 && this.worldObj.checkIfAABBIsClear(this.boundingBox) && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Count == 0 && !this.worldObj.isAnyLiquid(this.boundingBox);
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setShort("Anger", (short)this.angerLevel);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.angerLevel = nBTTagCompound1.getShort("Anger");
		}

		protected internal override Entity findPlayerToAttack()
		{
			return this.angerLevel == 0 ? null : base.findPlayerToAttack();
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
				System.Collections.IList list4 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand(32.0D, 32.0D, 32.0D));

				for (int i5 = 0; i5 < list4.Count; ++i5)
				{
					Entity entity6 = (Entity)list4[i5];
					if (entity6 is EntityPigZombie)
					{
						EntityPigZombie entityPigZombie7 = (EntityPigZombie)entity6;
						entityPigZombie7.becomeAngryAt(entity3);
					}
				}

				this.becomeAngryAt(entity3);
			}

			return base.attackEntityFrom(damageSource1, i2);
		}

		private void becomeAngryAt(Entity entity1)
		{
			this.entityToAttack = entity1;
			this.angerLevel = 400 + this.rand.Next(400);
			this.randomSoundDelay = this.rand.Next(40);
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
			int i3 = this.rand.Next(2 + i2);

			int i4;
			for (i4 = 0; i4 < i3; ++i4)
			{
				this.dropItem(Item.rottenFlesh.shiftedIndex, 1);
			}

			i3 = this.rand.Next(2 + i2);

			for (i4 = 0; i4 < i3; ++i4)
			{
				this.dropItem(Item.goldNugget.shiftedIndex, 1);
			}

		}

		protected internal override void dropRareDrop(int i1)
		{
			if (i1 > 0)
			{
				ItemStack itemStack2 = new ItemStack(Item.swordGold);
				EnchantmentHelper.func_48441_a(this.rand, itemStack2, 5);
				this.entityDropItem(itemStack2, 0.0F);
			}
			else
			{
				int i3 = this.rand.Next(3);
				if (i3 == 0)
				{
					this.dropItem(Item.ingotGold.shiftedIndex, 1);
				}
				else if (i3 == 1)
				{
					this.dropItem(Item.swordGold.shiftedIndex, 1);
				}
				else if (i3 == 2)
				{
					this.dropItem(Item.helmetGold.shiftedIndex, 1);
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