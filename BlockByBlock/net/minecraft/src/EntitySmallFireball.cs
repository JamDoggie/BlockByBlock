namespace net.minecraft.src
{
	public class EntitySmallFireball : EntityFireball
	{
		public EntitySmallFireball(World world1) : base(world1)
		{
			this.setSize(0.3125F, 0.3125F);
		}

		public EntitySmallFireball(World world1, EntityLiving entityLiving2, double d3, double d5, double d7) : base(world1, entityLiving2, d3, d5, d7)
		{
			this.setSize(0.3125F, 0.3125F);
		}

		public EntitySmallFireball(World world1, double d2, double d4, double d6, double d8, double d10, double d12) : base(world1, d2, d4, d6, d8, d10, d12)
		{
			this.setSize(0.3125F, 0.3125F);
		}

		protected internal override void func_40071_a(MovingObjectPosition movingObjectPosition1)
		{
			if (!this.worldObj.isRemote)
			{
				if (movingObjectPosition1.entityHit != null)
				{
					if (!movingObjectPosition1.entityHit.ImmuneToFire && movingObjectPosition1.entityHit.attackEntityFrom(DamageSource.causeFireballDamage(this, this.shootingEntity), 5))
					{
						movingObjectPosition1.entityHit.Fire = 5;
					}
				}
				else
				{
					int i2 = movingObjectPosition1.blockX;
					int i3 = movingObjectPosition1.blockY;
					int i4 = movingObjectPosition1.blockZ;
					switch (movingObjectPosition1.sideHit)
					{
					case 0:
						--i3;
						break;
					case 1:
						++i3;
						break;
					case 2:
						--i4;
						break;
					case 3:
						++i4;
						break;
					case 4:
						--i2;
						break;
					case 5:
						++i2;
					break;
					}

					if (this.worldObj.isAirBlock(i2, i3, i4))
					{
						this.worldObj.setBlockWithNotify(i2, i3, i4, Block.fire.blockID);
					}
				}

				this.setDead();
			}

		}

		public override bool canBeCollidedWith()
		{
			return false;
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			return false;
		}
	}

}