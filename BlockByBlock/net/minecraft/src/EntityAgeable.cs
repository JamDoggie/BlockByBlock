namespace net.minecraft.src
{
	public abstract class EntityAgeable : EntityCreature
	{
		public EntityAgeable(World world1) : base(world1)
		{
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(12, new int?(0));
		}

		public virtual int GrowingAge
		{
			get
			{
				return this.dataWatcher.getWatchableObjectInt(12);
			}
			set
			{
				this.dataWatcher.updateObject(12, value);
			}
		}


		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
			nBTTagCompound1.setInteger("Age", this.GrowingAge);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
			this.GrowingAge = nBTTagCompound1.getInteger("Age");
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			int i1 = this.GrowingAge;
			if (i1 < 0)
			{
				++i1;
				this.GrowingAge = i1;
			}
			else if (i1 > 0)
			{
				--i1;
				this.GrowingAge = i1;
			}

		}

		public override bool Child
		{
			get
			{
				return this.GrowingAge < 0;
			}
		}
	}

}