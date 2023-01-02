using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIOwnerHurtTarget : EntityAITarget
	{
		internal EntityTameable field_48392_a;
		internal EntityLiving field_48391_b;

		public EntityAIOwnerHurtTarget(EntityTameable entityTameable1) : base(entityTameable1, 32.0F, false)
		{
			this.field_48392_a = entityTameable1;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (!this.field_48392_a.Tamed)
			{
				return false;
			}
			else
			{
				EntityLiving entityLiving1 = this.field_48392_a.getOwner();
				if (entityLiving1 == null)
				{
					return false;
				}
				else
				{
					this.field_48391_b = entityLiving1.LastAttackingEntity;
					return this.func_48376_a(this.field_48391_b, false);
				}
			}
		}

		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.field_48391_b;
			base.startExecuting();
		}
	}

}