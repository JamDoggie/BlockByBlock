using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIDefendVillage : EntityAITarget
	{
		internal EntityIronGolem irongolem;
		internal EntityLiving villageAgressorTarget;

		public EntityAIDefendVillage(EntityIronGolem entityIronGolem1) : base(entityIronGolem1, 16.0F, false, true)
		{
			this.irongolem = entityIronGolem1;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			Village village1 = this.irongolem.Village;
			if (village1 == null)
			{
				return false;
			}
			else
			{
				this.villageAgressorTarget = village1.findNearestVillageAggressor(this.irongolem);
				return this.func_48376_a(this.villageAgressorTarget, false);
			}
		}

		public override void startExecuting()
		{
			this.irongolem.AttackTarget = this.villageAgressorTarget;
			base.startExecuting();
		}
	}

}