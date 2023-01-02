using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAILookIdle : EntityAIBase
	{
		private EntityLiving idleEntity;
		private double lookX;
		private double lookZ;
		private int idleTime = 0;

		public EntityAILookIdle(EntityLiving entityLiving1)
		{
			this.idleEntity = entityLiving1;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			return this.idleEntity.RNG.NextSingle() < 0.02F;
		}

		public override bool continueExecuting()
		{
			return this.idleTime >= 0;
		}

		public override void startExecuting()
		{
			double d1 = Math.PI * 2D * this.idleEntity.RNG.NextDouble();
			this.lookX = Math.Cos(d1);
			this.lookZ = Math.Sin(d1);
			this.idleTime = 20 + this.idleEntity.RNG.Next(20);
		}

		public override void updateTask()
		{
			--this.idleTime;
			this.idleEntity.LookHelper.setLookPosition(this.idleEntity.posX + this.lookX, this.idleEntity.posY + (double)this.idleEntity.EyeHeight, this.idleEntity.posZ + this.lookZ, 10.0F, (float)this.idleEntity.VerticalFaceSpeed);
		}
	}

}