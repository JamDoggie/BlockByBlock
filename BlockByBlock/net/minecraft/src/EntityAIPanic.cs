namespace net.minecraft.src
{
	public class EntityAIPanic : EntityAIBase
	{
		private EntityCreature field_48316_a;
		private float field_48314_b;
		private double field_48315_c;
		private double field_48312_d;
		private double field_48313_e;

		public EntityAIPanic(EntityCreature entityCreature1, float f2)
		{
			this.field_48316_a = entityCreature1;
			this.field_48314_b = f2;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (this.field_48316_a.AITarget == null)
			{
				return false;
			}
			else
			{
				Vec3D vec3D1 = RandomPositionGenerator.func_48622_a(this.field_48316_a, 5, 4);
				if (vec3D1 == null)
				{
					return false;
				}
				else
				{
					this.field_48315_c = vec3D1.xCoord;
					this.field_48312_d = vec3D1.yCoord;
					this.field_48313_e = vec3D1.zCoord;
					return true;
				}
			}
		}

		public override void startExecuting()
		{
			this.field_48316_a.Navigator.func_48666_a(this.field_48315_c, this.field_48312_d, this.field_48313_e, this.field_48314_b);
		}

		public override bool continueExecuting()
		{
			return !this.field_48316_a.Navigator.noPath();
		}
	}

}