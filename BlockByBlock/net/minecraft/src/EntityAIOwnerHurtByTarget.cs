using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIOwnerHurtByTarget : EntityAITarget
	{
		internal EntityTameable field_48394_a;
		internal EntityLiving field_48393_b;

		public EntityAIOwnerHurtByTarget(EntityTameable entityTameable1) : base(entityTameable1, 32.0F, false)
		{
			this.field_48394_a = entityTameable1;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (!this.field_48394_a.Tamed)
			{
				return false;
			}
			else
			{
				EntityLiving entityLiving1 = this.field_48394_a.getOwner();
				if (entityLiving1 == null)
				{
					return false;
				}
				else
				{
					this.field_48393_b = entityLiving1.AITarget;
					return this.func_48376_a(this.field_48393_b, false);
				}
			}
		}

		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.field_48393_b;
			base.startExecuting();
		}
	}

}