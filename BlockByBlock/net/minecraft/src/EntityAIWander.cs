using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIWander : EntityAIBase
	{
		private EntityCreature entity;
		private double field_46098_b;
		private double field_46099_c;
		private double field_46097_d;
		private float field_48317_e;

		public EntityAIWander(EntityCreature entityCreature1, float f2)
		{
			this.entity = entityCreature1;
			this.field_48317_e = f2;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (this.entity.Age >= 100)
			{
				return false;
			}
			else if (this.entity.RNG.Next(120) != 0)
			{
				return false;
			}
			else
			{
				Vec3D? vec3D1 = RandomPositionGenerator.func_48622_a(this.entity, 10, 7);
				if (vec3D1 == null)
				{
					return false;
				}
				else
				{
					this.field_46098_b = vec3D1.xCoord;
					this.field_46099_c = vec3D1.yCoord;
					this.field_46097_d = vec3D1.zCoord;
					return true;
				}
			}
		}

		public override bool continueExecuting()
		{
			return !this.entity.Navigator.noPath();
		}

		public override void startExecuting()
		{
			this.entity.Navigator.func_48666_a(this.field_46098_b, this.field_46099_c, this.field_46097_d, this.field_48317_e);
		}
	}

}