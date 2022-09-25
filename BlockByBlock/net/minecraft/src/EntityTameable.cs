namespace net.minecraft.src
{
	public abstract class EntityTameable : EntityAnimal
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			aiSit = new EntityAISit(this);
		}

		protected internal EntityAISit aiSit;

		public EntityTameable(World world1) : base(world1)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, (sbyte)0);
			this.dataWatcher.addObject(17, "");
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			if (string.ReferenceEquals(this.OwnerName, null))
			{
				nBTTagCompound1.setString("Owner", "");
			}
			else
			{
				nBTTagCompound1.setString("Owner", this.OwnerName);
			}

			nBTTagCompound1.setBoolean("Sitting", this.Sitting);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			string string2 = nBTTagCompound1.getString("Owner");
			if (string2.Length > 0)
			{
				this.setOwner(string2);
				this.Tamed = true;
			}

			this.aiSit.func_48407_a(nBTTagCompound1.getBoolean("Sitting"));
		}

		protected internal virtual void func_48142_a(bool z1)
		{
			string string2 = "heart";
			if (!z1)
			{
				string2 = "smoke";
			}

			for (int i3 = 0; i3 < 7; ++i3)
			{
				double d4 = this.rand.NextGaussian() * 0.02D;
				double d6 = this.rand.NextGaussian() * 0.02D;
				double d8 = this.rand.NextGaussian() * 0.02D;
				this.worldObj.spawnParticle(string2, this.posX + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, this.posY + 0.5D + (double)(this.rand.NextSingle() * this.height), this.posZ + (double)(this.rand.NextSingle() * this.width * 2.0F) - (double)this.width, d4, d6, d8);
			}

		}

		public override void handleHealthUpdate(sbyte b1)
		{
			if (b1 == 7)
			{
				this.func_48142_a(true);
			}
			else if (b1 == 6)
			{
				this.func_48142_a(false);
			}
			else
			{
				base.handleHealthUpdate(b1);
			}

		}

		public virtual bool Tamed
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 4) != 0;
			}
			set
			{
				sbyte b2 = this.dataWatcher.getWatchableObjectByte(16);
				if (value)
				{
					this.dataWatcher.updateObject(16, (sbyte)(b2 | 4));
				}
				else
				{
					this.dataWatcher.updateObject(16, (sbyte)(b2 & -5));
				}
    
			}
		}


		public virtual bool Sitting
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
			}
		}

		public virtual void func_48140_f(bool z1)
		{
			sbyte b2 = this.dataWatcher.getWatchableObjectByte(16);
			if (z1)
			{
				this.dataWatcher.updateObject(16, (sbyte)(b2 | 1));
			}
			else
			{
				this.dataWatcher.updateObject(16, (sbyte)(b2 & -2));
			}

		}

		public virtual string OwnerName
		{
			get
			{
				return this.dataWatcher.getWatchableObjectString(17);
			}
		}

		public void setOwner(string string1)
		{
			dataWatcher.updateObject(17, string1);
		}

		public EntityLiving getOwner()
		{
			return worldObj.getPlayerEntityByName(OwnerName);
		}


		public virtual EntityAISit func_50008_ai()
		{
			return this.aiSit;
		}
	}

}