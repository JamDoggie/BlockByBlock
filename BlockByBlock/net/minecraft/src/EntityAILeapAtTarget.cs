using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAILeapAtTarget : EntityAIBase
	{
		internal EntityLiving leaper;
		internal EntityLiving leapTarget;
		internal float leapMotionY;

		public EntityAILeapAtTarget(EntityLiving entityLiving1, float f2)
		{
			this.leaper = entityLiving1;
			this.leapMotionY = f2;
			this.MutexBits = 5;
		}

		public override bool shouldExecute()
		{
			this.leapTarget = this.leaper.AttackTarget;
			if (this.leapTarget == null)
			{
				return false;
			}
			else
			{
				double d1 = this.leaper.getDistanceSqToEntity(this.leapTarget);
				return d1 >= 4.0D && d1 <= 16.0D ? (!this.leaper.onGround ? false : this.leaper.RNG.Next(5) == 0) : false;
			}
		}

		public override bool continueExecuting()
		{
			return !this.leaper.onGround;
		}

		public override void startExecuting()
		{
			double d1 = this.leapTarget.posX - this.leaper.posX;
			double d3 = this.leapTarget.posZ - this.leaper.posZ;
			float f5 = MathHelper.sqrt_double(d1 * d1 + d3 * d3);
			this.leaper.motionX += d1 / (double)f5 * 0.5D * (double)0.8F + this.leaper.motionX * (double)0.2F;
			this.leaper.motionZ += d3 / (double)f5 * 0.5D * (double)0.8F + this.leaper.motionZ * (double)0.2F;
			this.leaper.motionY = (double)this.leapMotionY;
		}
	}

}