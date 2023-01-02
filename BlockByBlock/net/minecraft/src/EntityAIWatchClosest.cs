using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIWatchClosest : EntityAIBase
	{
		private EntityLiving field_46105_a;
		private Entity closestEntity;
		private float field_46101_d;
		private int field_46102_e;
		private float field_48294_e;
		private Type field_48293_f;

		public EntityAIWatchClosest(EntityLiving entityLiving1, Type class2, float f3)
		{
			this.field_46105_a = entityLiving1;
			this.field_48293_f = class2;
			this.field_46101_d = f3;
			this.field_48294_e = 0.02F;
			this.MutexBits = 2;
		}

		public EntityAIWatchClosest(EntityLiving entityLiving1, Type class2, float f3, float f4)
		{
			this.field_46105_a = entityLiving1;
			this.field_48293_f = class2;
			this.field_46101_d = f3;
			this.field_48294_e = f4;
			this.MutexBits = 2;
		}

		public override bool shouldExecute()
		{
			if (this.field_46105_a.RNG.NextSingle() >= this.field_48294_e)
			{
				return false;
			}
			else
			{
				if (this.field_48293_f == typeof(EntityPlayer))
				{
					this.closestEntity = this.field_46105_a.worldObj.getClosestPlayerToEntity(this.field_46105_a, (double)this.field_46101_d);
				}
				else
				{
					this.closestEntity = this.field_46105_a.worldObj.findNearestEntityWithinAABB(this.field_48293_f, this.field_46105_a.boundingBox.expand((double)this.field_46101_d, 3.0D, (double)this.field_46101_d), this.field_46105_a);
				}

				return this.closestEntity != null;
			}
		}

		public override bool continueExecuting()
		{
			return !this.closestEntity.EntityAlive ? false : (this.field_46105_a.getDistanceSqToEntity(this.closestEntity) > (double)(this.field_46101_d * this.field_46101_d) ? false : this.field_46102_e > 0);
		}

		public override void startExecuting()
		{
			this.field_46102_e = 40 + this.field_46105_a.RNG.Next(40);
		}

		public override void resetTask()
		{
			this.closestEntity = null;
		}

		public override void updateTask()
		{
			this.field_46105_a.LookHelper.setLookPosition(this.closestEntity.posX, this.closestEntity.posY + (double)this.closestEntity.EyeHeight, this.closestEntity.posZ, 10.0F, (float)this.field_46105_a.VerticalFaceSpeed);
			--this.field_46102_e;
		}
	}

}