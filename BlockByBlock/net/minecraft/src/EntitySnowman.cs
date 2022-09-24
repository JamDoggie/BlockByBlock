namespace net.minecraft.src
{
	public class EntitySnowman : EntityGolem
	{
		public EntitySnowman(World world1) : base(world1)
		{
			this.texture = "/mob/snowman.png";
			this.setSize(0.4F, 1.8F);
			this.Navigator.func_48664_a(true);
			this.tasks.addTask(1, new EntityAIArrowAttack(this, 0.25F, 2, 20));
			this.tasks.addTask(2, new EntityAIWander(this, 0.2F));
			this.tasks.addTask(3, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
			this.tasks.addTask(4, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAINearestAttackableTarget(this, typeof(EntityMob), 16.0F, 0, true));
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
				return 4;
			}
		}

		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			if (this.Wet)
			{
				this.attackEntityFrom(DamageSource.drown, 1);
			}

			int i1 = MathHelper.floor_double(this.posX);
			int i2 = MathHelper.floor_double(this.posZ);
			if (this.worldObj.getBiomeGenForCoords(i1, i2).FloatTemperature > 1.0F)
			{
				this.attackEntityFrom(DamageSource.onFire, 1);
			}

			for (i1 = 0; i1 < 4; ++i1)
			{
				i2 = MathHelper.floor_double(this.posX + (double)((float)(i1 % 2 * 2 - 1) * 0.25F));
				int i3 = MathHelper.floor_double(this.posY);
				int i4 = MathHelper.floor_double(this.posZ + (double)((float)(i1 / 2 % 2 * 2 - 1) * 0.25F));
				if (this.worldObj.getBlockId(i2, i3, i4) == 0 && this.worldObj.getBiomeGenForCoords(i2, i4).FloatTemperature < 0.8F && Block.snow.canPlaceBlockAt(this.worldObj, i2, i3, i4))
				{
					this.worldObj.setBlockWithNotify(i2, i3, i4, Block.snow.blockID);
				}
			}

		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeEntityToNBT(nBTTagCompound1);
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readEntityFromNBT(nBTTagCompound1);
		}

		protected internal override int DropItemId
		{
			get
			{
				return Item.snowball.shiftedIndex;
			}
		}

		protected internal override void dropFewItems(bool z1, int i2)
		{
			int i3 = this.rand.Next(16);

			for (int i4 = 0; i4 < i3; ++i4)
			{
				this.dropItem(Item.snowball.shiftedIndex, 1);
			}

		}
	}

}