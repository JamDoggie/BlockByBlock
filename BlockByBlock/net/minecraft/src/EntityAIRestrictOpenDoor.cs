using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIRestrictOpenDoor : EntityAIBase
	{
		private EntityCreature entityObj;
		private VillageDoorInfo frontDoor;

		public EntityAIRestrictOpenDoor(EntityCreature entityCreature1)
		{
			this.entityObj = entityCreature1;
		}

		public override bool shouldExecute()
		{
			if (this.entityObj.worldObj.Daytime)
			{
				return false;
			}
			else
			{
				Village village1 = this.entityObj.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ), 16);
				if (village1 == null)
				{
					return false;
				}
				else
				{
					this.frontDoor = village1.findNearestDoor(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ));
					return this.frontDoor == null ? false : (double)this.frontDoor.getInsideDistanceSquare(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ)) < 2.25D;
				}
			}
		}

		public override bool continueExecuting()
		{
			return this.entityObj.worldObj.Daytime ? false :!this.frontDoor.isDetachedFromVillageFlag && this.frontDoor.isInside(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posZ));
		}

		public override void startExecuting()
		{
			this.entityObj.Navigator.BreakDoors = false;
			this.entityObj.Navigator.func_48663_c(false);
		}

		public override void resetTask()
		{
			this.entityObj.Navigator.BreakDoors = true;
			this.entityObj.Navigator.func_48663_c(true);
			this.frontDoor = null;
		}

		public override void updateTask()
		{
			this.frontDoor.incrementDoorOpeningRestrictionCounter();
		}
	}

}