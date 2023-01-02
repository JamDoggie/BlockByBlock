using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIOpenDoor : EntityAIDoorInteract
	{
		internal bool field_48328_i;
		internal int field_48327_j;

		public EntityAIOpenDoor(EntityLiving entityLiving1, bool z2) : base(entityLiving1)
		{
			this.theEntity = entityLiving1;
			this.field_48328_i = z2;
		}

		public override bool continueExecuting()
		{
			return this.field_48328_i && this.field_48327_j > 0 && base.continueExecuting();
		}

		public override void startExecuting()
		{
			this.field_48327_j = 20;
			this.targetDoor.onPoweredBlockChange(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ, true);
		}

		public override void resetTask()
		{
			if (this.field_48328_i)
			{
				this.targetDoor.onPoweredBlockChange(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ, false);
			}

		}

		public override void updateTask()
		{
			--this.field_48327_j;
			base.updateTask();
		}
	}

}