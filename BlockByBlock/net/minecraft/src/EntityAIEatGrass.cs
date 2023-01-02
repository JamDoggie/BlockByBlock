using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIEatGrass : EntityAIBase
	{
		private EntityLiving theEntity;
		private World theWorld;
		internal int eatGrassTick = 0;

		public EntityAIEatGrass(EntityLiving entityLiving1)
		{
			this.theEntity = entityLiving1;
			this.theWorld = entityLiving1.worldObj;
			this.MutexBits = 7;
		}

		public override bool shouldExecute()
		{
			if (this.theEntity.RNG.Next(this.theEntity.Child ? 50 : 1000) != 0)
			{
				return false;
			}
			else
			{
				int i1 = MathHelper.floor_double(this.theEntity.posX);
				int i2 = MathHelper.floor_double(this.theEntity.posY);
				int i3 = MathHelper.floor_double(this.theEntity.posZ);
				return this.theWorld.getBlockId(i1, i2, i3) == Block.tallGrass.blockID && this.theWorld.getBlockMetadata(i1, i2, i3) == 1 ? true : this.theWorld.getBlockId(i1, i2 - 1, i3) == Block.grass.blockID;
			}
		}

		public override void startExecuting()
		{
			this.eatGrassTick = 40;
			this.theWorld.setEntityState(this.theEntity, (sbyte)10);
			this.theEntity.Navigator.clearPathEntity();
		}

		public override void resetTask()
		{
			this.eatGrassTick = 0;
		}

		public override bool continueExecuting()
		{
			return this.eatGrassTick > 0;
		}

		public virtual int func_48396_h()
		{
			return this.eatGrassTick;
		}

		public override void updateTask()
		{
			this.eatGrassTick = Math.Max(0, this.eatGrassTick - 1);
			if (this.eatGrassTick == 4)
			{
				int i1 = MathHelper.floor_double(this.theEntity.posX);
				int i2 = MathHelper.floor_double(this.theEntity.posY);
				int i3 = MathHelper.floor_double(this.theEntity.posZ);
				if (this.theWorld.getBlockId(i1, i2, i3) == Block.tallGrass.blockID)
				{
					this.theWorld.playAuxSFX(2001, i1, i2, i3, Block.tallGrass.blockID + 4096);
					this.theWorld.setBlockWithNotify(i1, i2, i3, 0);
					this.theEntity.eatGrassBonus();
				}
				else if (this.theWorld.getBlockId(i1, i2 - 1, i3) == Block.grass.blockID)
				{
					this.theWorld.playAuxSFX(2001, i1, i2 - 1, i3, Block.grass.blockID);
					this.theWorld.setBlockWithNotify(i1, i2 - 1, i3, Block.dirt.blockID);
					this.theEntity.eatGrassBonus();
				}

			}
		}
	}

}