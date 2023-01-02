using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIMoveTowardsTarget : EntityAIBase
	{
		private EntityCreature theEntity;
		private EntityLiving? targetEntity;
		private double movePosX;
		private double movePosY;
		private double movePosZ;
		private float field_48330_f;
		private float field_48331_g;

		public EntityAIMoveTowardsTarget(EntityCreature entityCreature1, float f2, float f3)
		{
			this.theEntity = entityCreature1;
			this.field_48330_f = f2;
			this.field_48331_g = f3;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			this.targetEntity = this.theEntity.AttackTarget;
			if (this.targetEntity == null)
			{
				return false;
			}
			else if (this.targetEntity.getDistanceSqToEntity(this.theEntity) > (double)(this.field_48331_g * this.field_48331_g))
			{
				return false;
			}
			else
			{
				Vec3D vec3D1 = RandomPositionGenerator.func_48620_a(this.theEntity, 16, 7, Vec3D.createVector(this.targetEntity.posX, this.targetEntity.posY, this.targetEntity.posZ));
				if (vec3D1 == null)
				{
					return false;
				}
				else
				{
					this.movePosX = vec3D1.xCoord;
					this.movePosY = vec3D1.yCoord;
					this.movePosZ = vec3D1.zCoord;
					return true;
				}
			}
		}

		public override bool continueExecuting()
		{
			return !this.theEntity.Navigator.noPath() && this.targetEntity.EntityAlive && this.targetEntity.getDistanceSqToEntity(this.theEntity) < (double)(this.field_48331_g * this.field_48331_g);
		}

		public override void resetTask()
		{
			this.targetEntity = null;
		}

		public override void startExecuting()
		{
			this.theEntity.Navigator.func_48666_a(this.movePosX, this.movePosY, this.movePosZ, this.field_48330_f);
		}
	}

}