using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAISit : EntityAIBase
	{
		private EntityTameable theEntity;
		private bool field_48408_b = false;

		public EntityAISit(EntityTameable entityTameable1)
		{
			this.theEntity = entityTameable1;
			this.MutexBits = 5;
		}

		public override bool shouldExecute()
		{
			if (!this.theEntity.Tamed)
			{
				return false;
			}
			else if (this.theEntity.InWater)
			{
				return false;
			}
			else if (!this.theEntity.onGround)
			{
				return false;
			}
			else
			{
				EntityLiving entityLiving1 = this.theEntity.getOwner();
				return entityLiving1 == null ? true : (this.theEntity.getDistanceSqToEntity(entityLiving1) < 144.0D && entityLiving1.AITarget != null ? false : this.field_48408_b);
			}
		}

		public override void startExecuting()
		{
			this.theEntity.Navigator.clearPathEntity();
			this.theEntity.func_48140_f(true);
		}

		public override void resetTask()
		{
			this.theEntity.func_48140_f(false);
		}

		public virtual void func_48407_a(bool z1)
		{
			this.field_48408_b = z1;
		}
	}

}