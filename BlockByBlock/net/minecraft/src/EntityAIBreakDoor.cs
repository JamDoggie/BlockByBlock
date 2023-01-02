using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIBreakDoor : EntityAIDoorInteract
	{
		private int field_48329_i;

		public EntityAIBreakDoor(EntityLiving entityLiving1) : base(entityLiving1)
		{
		}

		public override bool shouldExecute()
		{
			return !base.shouldExecute() ? false :!this.targetDoor.func_48213_h(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ);
		}

		public override void startExecuting()
		{
			base.startExecuting();
			this.field_48329_i = 240;
		}

		public override bool continueExecuting()
		{
			double d1 = this.theEntity.getDistanceSq((double)this.entityPosX, (double)this.entityPosY, (double)this.entityPosZ);
			return this.field_48329_i >= 0 && !this.targetDoor.func_48213_h(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ) && d1 < 4.0D;
		}

		public override void updateTask()
		{
			base.updateTask();
			if (this.theEntity.RNG.Next(20) == 0)
			{
				this.theEntity.worldObj.playAuxSFX(1010, this.entityPosX, this.entityPosY, this.entityPosZ, 0);
			}

			if (--this.field_48329_i == 0 && this.theEntity.worldObj.difficultySetting == 3)
			{
				this.theEntity.worldObj.setBlockWithNotify(this.entityPosX, this.entityPosY, this.entityPosZ, 0);
				this.theEntity.worldObj.playAuxSFX(1012, this.entityPosX, this.entityPosY, this.entityPosZ, 0);
				this.theEntity.worldObj.playAuxSFX(2001, this.entityPosX, this.entityPosY, this.entityPosZ, this.targetDoor.blockID);
			}

		}
	}

}