namespace net.minecraft.src
{
	public class EntitySlime : EntityLiving, IMob
	{
		public float field_40139_a;
		public float field_768_a;
		public float field_767_b;
		private int slimeJumpDelay = 0;

		public EntitySlime(World world1) : base(world1)
		{
			this.texture = "/mob/slime.png";
			int i2 = 1 << this.rand.Next(3);
			this.yOffset = 0.0F;
			this.slimeJumpDelay = this.rand.Next(20) + 10;
			this.SlimeSize = i2;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)1));
		}

		public virtual int SlimeSize
		{
			set
			{
				this.dataWatcher.updateObject(16, new sbyte?((sbyte)value));
				this.setSize(0.6F * (float)value, 0.6F * (float)value);
				this.setPosition(this.posX, this.posY, this.posZ);
				this.EntityHealth = this.MaxHealth;
				this.experienceValue = value;
			}
			get
			{
				return this.dataWatcher.getWatchableObjectByte(16);
			}
		}

		public override int MaxHealth
		{
			get
			{
				int i1 = this.SlimeSize;
				return i1 * i1;
			}
		}


		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setInteger("Size", this.SlimeSize - 1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.SlimeSize = nBTTagCompound1.getInteger("Size") + 1;
		}

		protected internal virtual string SlimeParticle
		{
			get
			{
				return "slime";
			}
		}

		protected internal virtual string func_40138_aj()
		{
			return "mob.slime";
		}

		public override void onUpdate()
		{
			if (!this.worldObj.isRemote && this.worldObj.difficultySetting == 0 && this.SlimeSize > 0)
			{
				this.isDead = true;
			}

			this.field_768_a += (this.field_40139_a - this.field_768_a) * 0.5F;
			this.field_767_b = this.field_768_a;
			bool z1 = this.onGround;
			base.onUpdate();
			if (this.onGround && !z1)
			{
				int i2 = this.SlimeSize;

				for (int i3 = 0; i3 < i2 * 8; ++i3)
				{
					float f4 = this.rand.NextSingle() * (float)Math.PI * 2.0F;
					float f5 = this.rand.NextSingle() * 0.5F + 0.5F;
					float f6 = MathHelper.sin(f4) * (float)i2 * 0.5F * f5;
					float f7 = MathHelper.cos(f4) * (float)i2 * 0.5F * f5;
					this.worldObj.spawnParticle(this.SlimeParticle, this.posX + (double)f6, this.boundingBox.minY, this.posZ + (double)f7, 0.0D, 0.0D, 0.0D);
				}

				if (this.func_40134_ak())
				{
					this.worldObj.playSoundAtEntity(this, this.func_40138_aj(), this.SoundVolume, ((this.rand.NextSingle() - this.rand.NextSingle()) * 0.2F + 1.0F) / 0.8F);
				}

				this.field_40139_a = -0.5F;
			}

			this.func_40136_ag();
		}

		public override void updateEntityActionState()
		{
			this.despawnEntity();
			EntityPlayer entityPlayer1 = this.worldObj.getClosestVulnerablePlayerToEntity(this, 16.0D);
			if (entityPlayer1 != null)
			{
				this.faceEntity(entityPlayer1, 10.0F, 20.0F);
			}

			if (this.onGround && this.slimeJumpDelay-- <= 0)
			{
				this.slimeJumpDelay = this.func_40131_af();
				if (entityPlayer1 != null)
				{
					this.slimeJumpDelay /= 3;
				}

				this.isJumping = true;
				if (this.func_40133_ao())
				{
					this.worldObj.playSoundAtEntity(this, this.func_40138_aj(), this.SoundVolume, ((this.rand.NextSingle() - this.rand.NextSingle()) * 0.2F + 1.0F) * 0.8F);
				}

				this.field_40139_a = 1.0F;
				this.moveStrafing = 1.0F - this.rand.NextSingle() * 2.0F;
				this.moveForward = (float)(1 * this.SlimeSize);
			}
			else
			{
				this.isJumping = false;
				if (this.onGround)
				{
					this.moveStrafing = this.moveForward = 0.0F;
				}
			}

		}

		protected internal virtual void func_40136_ag()
		{
			this.field_40139_a *= 0.6F;
		}

		protected internal virtual int func_40131_af()
		{
			return this.rand.Next(20) + 10;
		}

		protected internal virtual EntitySlime createInstance()
		{
			return new EntitySlime(this.worldObj);
		}

		public override void setDead()
		{
			int i1 = this.SlimeSize;
			if (!this.worldObj.isRemote && i1 > 1 && this.Health <= 0)
			{
				int i2 = 2 + this.rand.Next(3);

				for (int i3 = 0; i3 < i2; ++i3)
				{
					float f4 = ((float)(i3 % 2) - 0.5F) * (float)i1 / 4.0F;
					float f5 = ((float)(i3 / 2) - 0.5F) * (float)i1 / 4.0F;
					EntitySlime entitySlime6 = this.createInstance();
					entitySlime6.SlimeSize = i1 / 2;
					entitySlime6.setLocationAndAngles(this.posX + (double)f4, this.posY + 0.5D, this.posZ + (double)f5, this.rand.NextSingle() * 360.0F, 0.0F);
					this.worldObj.spawnEntityInWorld(entitySlime6);
				}
			}

			base.setDead();
		}

		public override void onCollideWithPlayer(EntityPlayer entityPlayer1)
		{
			if (this.func_40137_ah())
			{
				int i2 = this.SlimeSize;
				if (this.canEntityBeSeen(entityPlayer1) && (double)this.getDistanceToEntity(entityPlayer1) < 0.6D * (double)i2 && entityPlayer1.attackEntityFrom(DamageSource.causeMobDamage(this), this.func_40130_ai()))
				{
					this.worldObj.playSoundAtEntity(this, "mob.slimeattack", 1.0F, (this.rand.NextSingle() - this.rand.NextSingle()) * 0.2F + 1.0F);
				}
			}

		}

		protected internal virtual bool func_40137_ah()
		{
			return this.SlimeSize > 1;
		}

		protected internal virtual int func_40130_ai()
		{
			return this.SlimeSize;
		}

		protected internal override string HurtSound
		{
			get
			{
				return "mob.slime";
			}
		}

		protected internal override string DeathSound
		{
			get
			{
				return "mob.slime";
			}
		}

		protected internal override int DropItemId
		{
			get
			{
				return this.SlimeSize == 1 ? Item.slimeBall.shiftedIndex : 0;
			}
		}

		public override bool CanSpawnHere
		{
			get
			{
				Chunk chunk1 = this.worldObj.getChunkFromBlockCoords(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posZ));
				return (this.SlimeSize == 1 || this.worldObj.difficultySetting > 0) && this.rand.Next(10) == 0 && chunk1.getRandomWithSeed(987234911L).Next(10) == 0 && this.posY < 40.0D ? base.CanSpawnHere : false;
			}
		}

		protected internal override float SoundVolume
		{
			get
			{
				return 0.4F * (float)this.SlimeSize;
			}
		}

		public override int VerticalFaceSpeed
		{
			get
			{
				return 0;
			}
		}

		protected internal virtual bool func_40133_ao()
		{
			return this.SlimeSize > 1;
		}

		protected internal virtual bool func_40134_ak()
		{
			return this.SlimeSize > 2;
		}
	}

}