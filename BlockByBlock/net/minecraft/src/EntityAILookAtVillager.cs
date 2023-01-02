using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAILookAtVillager : EntityAIBase
	{
		private EntityIronGolem theGolem;
		private EntityVillager theVillager;
		private int field_48405_c;

		public EntityAILookAtVillager(EntityIronGolem entityIronGolem1)
		{
			this.theGolem = entityIronGolem1;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			if (!this.theGolem.worldObj.Daytime)
			{
				return false;
			}
			else if (this.theGolem.RNG.Next(8000) != 0)
			{
				return false;
			}
			else
			{
				this.theVillager = (EntityVillager)this.theGolem.worldObj.findNearestEntityWithinAABB(typeof(EntityVillager), this.theGolem.boundingBox.expand(6.0D, 2.0D, 6.0D), this.theGolem);
				return this.theVillager != null;
			}
		}

		public override bool continueExecuting()
		{
			return this.field_48405_c > 0;
		}

		public override void startExecuting()
		{
			this.field_48405_c = 400;
			this.theGolem.func_48116_a(true);
		}

		public override void resetTask()
		{
			this.theGolem.func_48116_a(false);
			this.theVillager = null;
		}

		public override void updateTask()
		{
			this.theGolem.LookHelper.setLookPositionWithEntity(this.theVillager, 30.0F, 30.0F);
			--this.field_48405_c;
		}
	}

}