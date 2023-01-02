using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIOcelotAttack : EntityAIBase
	{
		internal World theWorld;
		internal EntityLiving theEntity;
		internal EntityLiving field_48362_c;
		internal int field_48360_d = 0;

		public EntityAIOcelotAttack(EntityLiving entityLiving1)
		{
			this.theEntity = entityLiving1;
			this.theWorld = entityLiving1.worldObj;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			EntityLiving entityLiving1 = this.theEntity.AttackTarget;
			if (entityLiving1 == null)
			{
				return false;
			}
			else
			{
				this.field_48362_c = entityLiving1;
				return true;
			}
		}

		public override bool continueExecuting()
		{
			return !this.field_48362_c.EntityAlive ? false : (this.theEntity.getDistanceSqToEntity(this.field_48362_c) > 225.0D ? false :!this.theEntity.Navigator.noPath() || this.shouldExecute());
		}

		public override void resetTask()
		{
			this.field_48362_c = null;
			this.theEntity.Navigator.clearPathEntity();
		}

		public override void updateTask()
		{
			this.theEntity.LookHelper.setLookPositionWithEntity(this.field_48362_c, 30.0F, 30.0F);
			double d1 = (double)(this.theEntity.width * 2.0F * this.theEntity.width * 2.0F);
			double d3 = this.theEntity.getDistanceSq(this.field_48362_c.posX, this.field_48362_c.boundingBox.minY, this.field_48362_c.posZ);
			float f5 = 0.23F;
			if (d3 > d1 && d3 < 16.0D)
			{
				f5 = 0.4F;
			}
			else if (d3 < 225.0D)
			{
				f5 = 0.18F;
			}

			this.theEntity.Navigator.func_48667_a(this.field_48362_c, f5);
			this.field_48360_d = Math.Max(this.field_48360_d - 1, 0);
			if (d3 <= d1)
			{
				if (this.field_48360_d <= 0)
				{
					this.field_48360_d = 20;
					this.theEntity.attackEntityAsMob(this.field_48362_c);
				}
			}
		}
	}

}