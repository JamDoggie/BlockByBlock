using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIArrowAttack : EntityAIBase
	{
		internal World worldObj;
		internal EntityLiving entityHost;
		internal EntityLiving attackTarget;
		internal int rangedAttackTime = 0;
		internal float field_48370_e;
		internal int field_48367_f = 0;
		internal int rangedAttackID;
		internal int maxRangedAttackTime;

		public EntityAIArrowAttack(EntityLiving entityLiving1, float f2, int i3, int i4)
		{
			this.entityHost = entityLiving1;
			this.worldObj = entityLiving1.worldObj;
			this.field_48370_e = f2;
			this.rangedAttackID = i3;
			this.maxRangedAttackTime = i4;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			EntityLiving entityLiving1 = this.entityHost.AttackTarget;
			if (entityLiving1 == null)
			{
				return false;
			}
			else
			{
				this.attackTarget = entityLiving1;
				return true;
			}
		}

		public override bool continueExecuting()
		{
			return this.shouldExecute() || !this.entityHost.Navigator.noPath();
		}

		public override void resetTask()
		{
			this.attackTarget = null;
		}

		public override void updateTask()
		{
			double d1 = 100.0D;
			double d3 = this.entityHost.getDistanceSq(this.attackTarget.posX, this.attackTarget.boundingBox.minY, this.attackTarget.posZ);
			bool z5 = this.entityHost.func_48090_aM().canSee(this.attackTarget);
			if (z5)
			{
				++this.field_48367_f;
			}
			else
			{
				this.field_48367_f = 0;
			}

			if (d3 <= d1 && this.field_48367_f >= 20)
			{
				this.entityHost.Navigator.clearPathEntity();
			}
			else
			{
				this.entityHost.Navigator.func_48667_a(this.attackTarget, this.field_48370_e);
			}

			this.entityHost.LookHelper.setLookPositionWithEntity(this.attackTarget, 30.0F, 30.0F);
			this.rangedAttackTime = Math.Max(this.rangedAttackTime - 1, 0);
			if (this.rangedAttackTime <= 0)
			{
				if (d3 <= d1 && z5)
				{
					this.doRangedAttack();
					this.rangedAttackTime = this.maxRangedAttackTime;
				}
			}
		}

		private void doRangedAttack()
		{
			if (this.rangedAttackID == 1)
			{
				EntityArrow entityArrow1 = new EntityArrow(this.worldObj, this.entityHost, this.attackTarget, 1.6F, 12.0F);
				this.worldObj.playSoundAtEntity(this.entityHost, "random.bow", 1.0F, 1.0F / (this.entityHost.RNG.NextSingle() * 0.4F + 0.8F));
				this.worldObj.spawnEntityInWorld(entityArrow1);
			}
			else if (this.rangedAttackID == 2)
			{
				EntitySnowball entitySnowball9 = new EntitySnowball(this.worldObj, this.entityHost);
				double d2 = this.attackTarget.posX - this.entityHost.posX;
				double d4 = this.attackTarget.posY + (double)this.attackTarget.EyeHeight - 1.100000023841858D - entitySnowball9.posY;
				double d6 = this.attackTarget.posZ - this.entityHost.posZ;
				float f8 = MathHelper.sqrt_double(d2 * d2 + d6 * d6) * 0.2F;
				entitySnowball9.setThrowableHeading(d2, d4 + (double)f8, d6, 1.6F, 12.0F);
				this.worldObj.playSoundAtEntity(this.entityHost, "random.bow", 1.0F, 1.0F / (this.entityHost.RNG.NextSingle() * 0.4F + 0.8F));
				this.worldObj.spawnEntityInWorld(entitySnowball9);
			}

		}
	}

}