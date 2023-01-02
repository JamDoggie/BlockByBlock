using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAICreeperSwell : EntityAIBase
	{
		internal EntityCreeper swellingCreeper;
		internal EntityLiving creeperAttackTarget;

		public EntityAICreeperSwell(EntityCreeper entityCreeper1)
		{
			this.swellingCreeper = entityCreeper1;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			EntityLiving entityLiving1 = this.swellingCreeper.AttackTarget;
			return this.swellingCreeper.CreeperState > 0 || entityLiving1 != null && this.swellingCreeper.getDistanceSqToEntity(entityLiving1) < 9.0D;
		}

		public override void startExecuting()
		{
			this.swellingCreeper.Navigator.clearPathEntity();
			this.creeperAttackTarget = this.swellingCreeper.AttackTarget;
		}

		public override void resetTask()
		{
			this.creeperAttackTarget = null;
		}

		public override void updateTask()
		{
			if (this.creeperAttackTarget == null)
			{
				this.swellingCreeper.CreeperState = -1;
			}
			else if (this.swellingCreeper.getDistanceSqToEntity(this.creeperAttackTarget) > 49.0D)
			{
				this.swellingCreeper.CreeperState = -1;
			}
			else if (!this.swellingCreeper.func_48090_aM().canSee(this.creeperAttackTarget))
			{
				this.swellingCreeper.CreeperState = -1;
			}
			else
			{
				this.swellingCreeper.CreeperState = 1;
			}
		}
	}

}