using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIPanic : EntityAIBase
	{
		private EntityCreature theAnimal;
		private float field_48314_b;
		private double targetLocX;
		private double targetLocY;
		private double targetLocZ;

		public EntityAIPanic(EntityCreature entityCreature1, float f2)
		{
			this.theAnimal = entityCreature1;
			this.field_48314_b = f2;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (this.theAnimal.AITarget == null)
			{
				return false;
			}
			else
			{
				Vec3D? vec3D1 = RandomPositionGenerator.func_48622_a(theAnimal, 5, 4);
				if (vec3D1 == null)
				{
					return false;
				}
				else
				{
					this.targetLocX = vec3D1.xCoord;
					this.targetLocY = vec3D1.yCoord;
					this.targetLocZ = vec3D1.zCoord;
					return true;
				}
			}
		}

		public override void startExecuting()
		{
			this.theAnimal.Navigator.func_48666_a(this.targetLocX, this.targetLocY, this.targetLocZ, this.field_48314_b);
		}

		public override bool continueExecuting()
		{
			return !this.theAnimal.Navigator.noPath();
		}
	}

}