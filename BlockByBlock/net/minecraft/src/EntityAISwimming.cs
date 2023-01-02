using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAISwimming : EntityAIBase
	{
		private EntityLiving theEntity;

		public EntityAISwimming(EntityLiving entityLiving1)
		{
			this.theEntity = entityLiving1;
			this.MutexBits = 4;
			entityLiving1.Navigator.func_48669_e(true);
		}

		public override bool shouldExecute()
		{
			return this.theEntity.InWater || this.theEntity.handleLavaMovement();
		}

		public override void updateTask()
		{
			if (this.theEntity.RNG.NextSingle() < 0.8F)
			{
				this.theEntity.JumpHelper.setJumping();
			}

		}
	}

}