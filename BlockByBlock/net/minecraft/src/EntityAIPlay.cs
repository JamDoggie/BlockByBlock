using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIPlay : EntityAIBase
	{
		private EntityVillager villagerObj;
		private EntityLiving targetVillager;
		private float field_48358_c;
		private int field_48356_d;

		public EntityAIPlay(EntityVillager entityVillager1, float f2)
		{
			this.villagerObj = entityVillager1;
			this.field_48358_c = f2;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (this.villagerObj.GrowingAge >= 0)
			{
				return false;
			}
			else if (this.villagerObj.RNG.Next(400) != 0)
			{
				return false;
			}
			else
			{
				System.Collections.IList list1 = this.villagerObj.worldObj.getEntitiesWithinAABB(typeof(EntityVillager), this.villagerObj.boundingBox.expand(6.0D, 3.0D, 6.0D));
				double d2 = double.MaxValue;
				System.Collections.IEnumerator iterator4 = list1.GetEnumerator();

				while (iterator4.MoveNext())
				{
					Entity entity5 = (Entity)iterator4.Current;
					if (entity5 != this.villagerObj)
					{
						EntityVillager entityVillager6 = (EntityVillager)entity5;
						if (!entityVillager6.IsPlayingFlag && entityVillager6.GrowingAge < 0)
						{
							double d7 = entityVillager6.getDistanceSqToEntity(this.villagerObj);
							if (d7 <= d2)
							{
								d2 = d7;
								this.targetVillager = entityVillager6;
							}
						}
					}
				}

				if (this.targetVillager == null)
				{
					Vec3D? vec3D9 = RandomPositionGenerator.func_48622_a(this.villagerObj, 16, 3);
					if (vec3D9 == null)
					{
						return false;
					}
				}

				return true;
			}
		}

		public override bool continueExecuting()
		{
			return this.field_48356_d > 0;
		}

		public override void startExecuting()
		{
			if (this.targetVillager != null)
			{
				this.villagerObj.IsPlayingFlag = true;
			}

			this.field_48356_d = 1000;
		}

		public override void resetTask()
		{
			this.villagerObj.IsPlayingFlag = false;
			this.targetVillager = null;
		}

		public override void updateTask()
		{
			--this.field_48356_d;
			if (this.targetVillager != null)
			{
				if (this.villagerObj.getDistanceSqToEntity(this.targetVillager) > 4.0D)
				{
					this.villagerObj.Navigator.func_48667_a(this.targetVillager, this.field_48358_c);
				}
			}
			else if (this.villagerObj.Navigator.noPath())
			{
				Vec3D? vec3D1 = RandomPositionGenerator.func_48622_a(this.villagerObj, 16, 3);
				if (vec3D1 == null)
				{
					return;
				}

				this.villagerObj.Navigator.func_48666_a(vec3D1.xCoord, vec3D1.yCoord, vec3D1.zCoord, this.field_48358_c);
			}

		}
	}

}