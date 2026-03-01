using System;

namespace net.minecraft.src
{
	public class EntityExpBottle : EntityThrowable
	{
		public EntityExpBottle(World world1) : base(world1)
		{
		}

		public EntityExpBottle(World world1, EntityLiving entityLiving2) : base(world1, entityLiving2)
		{
		}

		public EntityExpBottle(World world1, double d2, double d4, double d6) : base(world1, d2, d4, d6)
		{
		}

		protected internal override float func_40075_e()
		{
			return 0.07F;
		}

		protected internal override float func_40077_c()
		{
			return 0.7F;
		}

		protected internal override float func_40074_d()
		{
			return -20.0F;
		}

		protected internal override void onImpact(MovingObjectPosition movingObjectPosition1)
		{
			if (!this.worldObj.isRemote)
			{
				this.worldObj.playAuxSFX(2002, (int)(long)Math.Round(this.posX, MidpointRounding.AwayFromZero), (int)(long)Math.Round(this.posY, MidpointRounding.AwayFromZero), (int)(long)Math.Round(this.posZ, MidpointRounding.AwayFromZero), 0);
				int i2 = 3 + this.worldObj.rand.Next(5) + this.worldObj.rand.Next(5);

				while (i2 > 0)
				{
					int i3 = EntityXPOrb.getXPSplit(i2);
					i2 -= i3;
					this.worldObj.spawnEntityInWorld(new EntityXPOrb(this.worldObj, this.posX, this.posY, this.posZ, i3));
				}

				this.setDead();
			}

		}
	}

}