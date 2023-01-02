using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class TileEntityMobSpawner : TileEntity
	{
		public int delay = -1;
		private string mobID = "Pig";
		public double yaw;
		public double yaw2 = 0.0D;

		public TileEntityMobSpawner()
		{
			this.delay = 20;
		}

		public virtual string MobID
		{
			get
			{
				return this.mobID;
			}
			set
			{
				this.mobID = value;
			}
		}


		public virtual bool anyPlayerInRange()
		{
			return this.worldObj.getClosestPlayer((double)this.xCoord + 0.5D, (double)this.yCoord + 0.5D, (double)this.zCoord + 0.5D, 16.0D) != null;
		}

		public override void updateEntity()
		{
			this.yaw2 = this.yaw;
			if (this.anyPlayerInRange())
			{
				double d1 = (double)((float)this.xCoord + this.worldObj.rand.NextSingle());
				double d3 = (double)((float)this.yCoord + this.worldObj.rand.NextSingle());
				double d5 = (double)((float)this.zCoord + this.worldObj.rand.NextSingle());
				this.worldObj.spawnParticle("smoke", d1, d3, d5, 0.0D, 0.0D, 0.0D);
				this.worldObj.spawnParticle("flame", d1, d3, d5, 0.0D, 0.0D, 0.0D);

				for (this.yaw += (double)(1000.0F / ((float)this.delay + 200.0F)); this.yaw > 360.0D; this.yaw2 -= 360.0D)
				{
					this.yaw -= 360.0D;
				}

				if (!this.worldObj.isRemote)
				{
					if (this.delay == -1)
					{
						this.updateDelay();
					}

					if (this.delay > 0)
					{
						--this.delay;
						return;
					}

					sbyte b7 = 4;

					for (int i8 = 0; i8 < b7; ++i8)
					{
						EntityLiving entityLiving9 = (EntityLiving)((EntityLiving)EntityList.createEntityByName(this.mobID, this.worldObj));
						if (entityLiving9 == null)
						{
							return;
						}

						int i10 = this.worldObj.getEntitiesWithinAABB(entityLiving9.GetType(), AxisAlignedBB.getBoundingBoxFromPool((double)this.xCoord, (double)this.yCoord, (double)this.zCoord, (double)(this.xCoord + 1), (double)(this.yCoord + 1), (double)(this.zCoord + 1)).expand(8.0D, 4.0D, 8.0D)).Count;
						if (i10 >= 6)
						{
							this.updateDelay();
							return;
						}

						if (entityLiving9 != null)
						{
							double d11 = (double)this.xCoord + (this.worldObj.rand.NextDouble() - this.worldObj.rand.NextDouble()) * 4.0D;
							double d13 = (double)(this.yCoord + this.worldObj.rand.Next(3) - 1);
							double d15 = (double)this.zCoord + (this.worldObj.rand.NextDouble() - this.worldObj.rand.NextDouble()) * 4.0D;
							entityLiving9.setLocationAndAngles(d11, d13, d15, this.worldObj.rand.NextSingle() * 360.0F, 0.0F);
							if (entityLiving9.CanSpawnHere)
							{
								this.worldObj.spawnEntityInWorld(entityLiving9);
								this.worldObj.playAuxSFX(2004, this.xCoord, this.yCoord, this.zCoord, 0);
								entityLiving9.spawnExplosionParticle();
								this.updateDelay();
							}
						}
					}
				}

				base.updateEntity();
			}
		}

		private void updateDelay()
		{
			this.delay = 200 + this.worldObj.rand.Next(600);
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			this.mobID = nBTTagCompound1.getString("EntityId");
			this.delay = nBTTagCompound1.getShort("Delay");
		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			nBTTagCompound1.setString("EntityId", this.mobID);
			nBTTagCompound1.setShort("Delay", (short)this.delay);
		}
	}

}