using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIRestrictSun : EntityAIBase
	{
		private EntityCreature theEntity;

		public EntityAIRestrictSun(EntityCreature entityCreature1)
		{
			this.theEntity = entityCreature1;
		}

		public override bool shouldExecute()
		{
			return this.theEntity.worldObj.Daytime;
		}

		public override void startExecuting()
		{
			this.theEntity.Navigator.func_48680_d(true);
		}

		public override void resetTask()
		{
			this.theEntity.Navigator.func_48680_d(false);
		}
	}

}