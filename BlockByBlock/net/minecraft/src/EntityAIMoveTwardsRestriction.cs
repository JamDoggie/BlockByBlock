using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIMoveTwardsRestriction : EntityAIBase
	{
		private EntityCreature theEntity;
		private double movePosX;
		private double movePosY;
		private double movePosZ;
		private float field_48352_e;

		public EntityAIMoveTwardsRestriction(EntityCreature entityCreature1, float f2)
		{
			this.theEntity = entityCreature1;
			this.field_48352_e = f2;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (theEntity.WithinHomeDistanceCurrentPosition)
			{
				return false;
			}
			else
			{
				ChunkCoordinates chunkCoordinates1 = this.theEntity.HomePosition;
				Vec3D? vec3D2 = RandomPositionGenerator.func_48620_a(this.theEntity, 16, 7, Vec3D.createVector((double)chunkCoordinates1.posX, (double)chunkCoordinates1.posY, (double)chunkCoordinates1.posZ));
				if (vec3D2 == null)
				{
					return false;
				}
				else
				{
					movePosX = vec3D2.xCoord;
					movePosY = vec3D2.yCoord;
					movePosZ = vec3D2.zCoord;
					return true;
				}
			}
		}

		public override bool continueExecuting()
		{
			return !this.theEntity.Navigator.noPath();
		}

		public override void startExecuting()
		{
			this.theEntity.Navigator.func_48666_a(this.movePosX, this.movePosY, this.movePosZ, this.field_48352_e);
		}
	}

}