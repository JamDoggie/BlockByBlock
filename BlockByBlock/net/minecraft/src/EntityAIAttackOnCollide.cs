using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIAttackOnCollide : EntityAIBase
	{
		internal World worldObj;
		internal EntityLiving attacker;
		internal EntityLiving entityTarget;
		internal int field_46091_d;
		internal float field_48266_e;
		internal bool field_48264_f;
		internal PathEntity field_48265_g;
		internal Type classTarget;
		private int field_48269_i;

		public EntityAIAttackOnCollide(EntityLiving entityLiving1, Type class2, float f3, bool z4) : this(entityLiving1, f3, z4)
		{
			this.classTarget = class2;
		}

		public EntityAIAttackOnCollide(EntityLiving entityLiving1, float f2, bool z3)
		{
			this.field_46091_d = 0;
			this.attacker = entityLiving1;
			this.worldObj = entityLiving1.worldObj;
			this.field_48266_e = f2;
			this.field_48264_f = z3;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			EntityLiving entityLiving1 = this.attacker.AttackTarget;
			if (entityLiving1 == null)
			{
				return false;
			}
			else if (this.classTarget != null && !this.classTarget.IsAssignableFrom(entityLiving1.GetType()))
			{
				return false;
			}
			else
			{
				this.entityTarget = entityLiving1;
				this.field_48265_g = this.attacker.Navigator.func_48679_a(this.entityTarget);
				return this.field_48265_g != null;
			}
		}

		public override bool continueExecuting()
		{
			EntityLiving entityLiving1 = this.attacker.AttackTarget;
			return entityLiving1 == null ? false : (!this.entityTarget.EntityAlive ? false : (!this.field_48264_f ?!this.attacker.Navigator.noPath() : this.attacker.isWithinHomeDistance(MathHelper.floor_double(this.entityTarget.posX), MathHelper.floor_double(this.entityTarget.posY), MathHelper.floor_double(this.entityTarget.posZ))));
		}

		public override void startExecuting()
		{
			this.attacker.Navigator.setPath(this.field_48265_g, this.field_48266_e);
			this.field_48269_i = 0;
		}

		public override void resetTask()
		{
			this.entityTarget = null;
			this.attacker.Navigator.clearPathEntity();
		}

		public override void updateTask()
		{
			this.attacker.LookHelper.setLookPositionWithEntity(this.entityTarget, 30.0F, 30.0F);
			if ((this.field_48264_f || this.attacker.func_48090_aM().canSee(this.entityTarget)) && --this.field_48269_i <= 0)
			{
				this.field_48269_i = 4 + this.attacker.RNG.Next(7);
				this.attacker.Navigator.func_48667_a(this.entityTarget, this.field_48266_e);
			}

			this.field_46091_d = Math.Max(this.field_46091_d - 1, 0);
			double d1 = (double)(this.attacker.width * 2.0F * this.attacker.width * 2.0F);
			if (this.attacker.getDistanceSq(this.entityTarget.posX, this.entityTarget.boundingBox.minY, this.entityTarget.posZ) <= d1)
			{
				if (this.field_46091_d <= 0)
				{
					this.field_46091_d = 20;
					this.attacker.attackEntityAsMob(this.entityTarget);
				}
			}
		}
	}

}