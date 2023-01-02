using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIFollowGolem : EntityAIBase
	{
		private EntityVillager theVillager;
		private EntityIronGolem theGolem;
		private int field_48402_c;
		private bool field_48400_d = false;

		public EntityAIFollowGolem(EntityVillager entityVillager1)
		{
			this.theVillager = entityVillager1;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			if (this.theVillager.GrowingAge >= 0)
			{
				return false;
			}
			else if (!this.theVillager.worldObj.Daytime)
			{
				return false;
			}
			else
			{
				System.Collections.IList list1 = this.theVillager.worldObj.getEntitiesWithinAABB(typeof(EntityIronGolem), this.theVillager.boundingBox.expand(6.0D, 2.0D, 6.0D));
				if (list1.Count == 0)
				{
					return false;
				}
				else
				{
					System.Collections.IEnumerator iterator2 = list1.GetEnumerator();

					while (iterator2.MoveNext())
					{
						Entity entity3 = (Entity)iterator2.Current;
						EntityIronGolem entityIronGolem4 = (EntityIronGolem)entity3;
						if (entityIronGolem4.func_48117_D_() > 0)
						{
							this.theGolem = entityIronGolem4;
							break;
						}
					}

					return this.theGolem != null;
				}
			}
		}

		public override bool continueExecuting()
		{
			return this.theGolem.func_48117_D_() > 0;
		}

		public override void startExecuting()
		{
			this.field_48402_c = this.theVillager.RNG.Next(320);
			this.field_48400_d = false;
			this.theGolem.Navigator.clearPathEntity();
		}

		public override void resetTask()
		{
			this.theGolem = null;
			this.theVillager.Navigator.clearPathEntity();
		}

		public override void updateTask()
		{
			this.theVillager.LookHelper.setLookPositionWithEntity(this.theGolem, 30.0F, 30.0F);
			if (this.theGolem.func_48117_D_() == this.field_48402_c)
			{
				this.theVillager.Navigator.func_48667_a(this.theGolem, 0.15F);
				this.field_48400_d = true;
			}

			if (this.field_48400_d && this.theVillager.getDistanceSqToEntity(this.theGolem) < 4.0D)
			{
				this.theGolem.func_48116_a(false);
				this.theVillager.Navigator.clearPathEntity();
			}

		}
	}

}