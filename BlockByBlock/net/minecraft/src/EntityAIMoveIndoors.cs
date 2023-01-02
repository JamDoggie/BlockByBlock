using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIMoveIndoors : EntityAIBase
	{
		private EntityCreature entityObj;
		private VillageDoorInfo? doorInfo;
		private int insidePosX = -1;
		private int insidePosZ = -1;

		public EntityAIMoveIndoors(EntityCreature entityCreature1)
		{
			this.entityObj = entityCreature1;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if ((!this.entityObj.worldObj.Daytime || this.entityObj.worldObj.Raining) && !this.entityObj.worldObj.worldProvider.hasNoSky)
			{
				if (this.entityObj.RNG.Next(50) != 0)
				{
					return false;
				}
				else if (this.insidePosX != -1 && this.entityObj.getDistanceSq((double)this.insidePosX, this.entityObj.posY, (double)this.insidePosZ) < 4.0D)
				{
					return false;
				}
				else
				{
					Village village1 = this.entityObj.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ), 14);
					if (village1 == null)
					{
						return false;
					}
					else
					{
						this.doorInfo = village1.findNearestDoorUnrestricted(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ));
						return this.doorInfo != null;
					}
				}
			}
			else
			{
				return false;
			}
		}

		public override bool continueExecuting()
		{
			return !this.entityObj.Navigator.noPath();
		}

		public override void startExecuting()
		{
			this.insidePosX = -1;
			if (this.entityObj.getDistanceSq((double)this.doorInfo.InsidePosX, (double)this.doorInfo.posY, (double)this.doorInfo.InsidePosZ) > 256.0D)
			{
				Vec3D? vec3D1 = RandomPositionGenerator.func_48620_a(this.entityObj, 14, 3, Vec3D.createVector((double)this.doorInfo.InsidePosX + 0.5D, (double)this.doorInfo.InsidePosY, (double)this.doorInfo.InsidePosZ + 0.5D));
				if (vec3D1 != null)
				{
					this.entityObj.Navigator.func_48666_a(vec3D1.xCoord, vec3D1.yCoord, vec3D1.zCoord, 0.3F);
				}
			}
			else
			{
				this.entityObj.Navigator.func_48666_a((double)this.doorInfo.InsidePosX + 0.5D, (double)this.doorInfo.InsidePosY, (double)this.doorInfo.InsidePosZ + 0.5D, 0.3F);
			}

		}

		public override void resetTask()
		{
			this.insidePosX = this.doorInfo.InsidePosX;
			this.insidePosZ = this.doorInfo.InsidePosZ;
			this.doorInfo = null;
		}
	}

}