using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIVillagerMate : EntityAIBase
	{
		private EntityVillager villagerObj;
		private EntityVillager mate;
		private World worldObj;
		private int matingTimeout = 0;
		internal Village villageObj;

		public EntityAIVillagerMate(EntityVillager entityVillager1)
		{
			this.villagerObj = entityVillager1;
			this.worldObj = entityVillager1.worldObj;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			if (this.villagerObj.GrowingAge != 0)
			{
				return false;
			}
			else if (this.villagerObj.RNG.Next(500) != 0)
			{
				return false;
			}
			else
			{
				this.villageObj = this.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.villagerObj.posX), MathHelper.floor_double(this.villagerObj.posY), MathHelper.floor_double(this.villagerObj.posZ), 0);
				if (this.villageObj == null)
				{
					return false;
				}
				else if (!this.checkSufficientDoorsPresentForNewVillager())
				{
					return false;
				}
				else
				{
					Entity entity1 = this.worldObj.findNearestEntityWithinAABB(typeof(EntityVillager), this.villagerObj.boundingBox.expand(8.0D, 3.0D, 8.0D), this.villagerObj);
					if (entity1 == null)
					{
						return false;
					}
					else
					{
						this.mate = (EntityVillager)entity1;
						return this.mate.GrowingAge == 0;
					}
				}
			}
		}

		public override void startExecuting()
		{
			this.matingTimeout = 300;
			this.villagerObj.IsMatingFlag = true;
		}

		public override void resetTask()
		{
			this.villageObj = null;
			this.mate = null;
			this.villagerObj.IsMatingFlag = false;
		}

		public override bool continueExecuting()
		{
			return this.matingTimeout >= 0 && this.checkSufficientDoorsPresentForNewVillager() && this.villagerObj.GrowingAge == 0;
		}

		public override void updateTask()
		{
			--this.matingTimeout;
			this.villagerObj.LookHelper.setLookPositionWithEntity(this.mate, 10.0F, 30.0F);
			if (this.villagerObj.getDistanceSqToEntity(this.mate) > 2.25D)
			{
				this.villagerObj.Navigator.func_48667_a(this.mate, 0.25F);
			}
			else if (this.matingTimeout == 0 && this.mate.IsMatingFlag)
			{
				this.giveBirth();
			}

			if (this.villagerObj.RNG.Next(35) == 0)
			{
				this.spawnHeartParticles(this.villagerObj);
			}

		}

		private bool checkSufficientDoorsPresentForNewVillager()
		{
			int i1 = (int)((double)((float)this.villageObj.NumVillageDoors) * 0.35D);
			return this.villageObj.NumVillagers < i1;
		}

		private void giveBirth()
		{
			EntityVillager entityVillager1 = new EntityVillager(this.worldObj);
			this.mate.GrowingAge = 6000;
			this.villagerObj.GrowingAge = 6000;
			entityVillager1.GrowingAge = -24000;
			entityVillager1.Profession = this.villagerObj.RNG.Next(5);
			entityVillager1.setLocationAndAngles(this.villagerObj.posX, this.villagerObj.posY, this.villagerObj.posZ, 0.0F, 0.0F);
			this.worldObj.spawnEntityInWorld(entityVillager1);
			this.spawnHeartParticles(entityVillager1);
		}

		private void spawnHeartParticles(EntityLiving entityLiving1)
		{
			RandomExtended random2 = entityLiving1.RNG;

			for (int i3 = 0; i3 < 5; ++i3)
			{
				double d4 = random2.NextGaussian() * 0.02D;
				double d6 = random2.NextGaussian() * 0.02D;
				double d8 = random2.NextGaussian() * 0.02D;
				this.worldObj.spawnParticle("heart", entityLiving1.posX + (double)(random2.NextSingle() * entityLiving1.width * 2.0F) - (double)entityLiving1.width, entityLiving1.posY + 1.0D + (double)(random2.NextSingle() * entityLiving1.height), entityLiving1.posZ + (double)(random2.NextSingle() * entityLiving1.width * 2.0F) - (double)entityLiving1.width, d4, d6, d8);
			}

		}
	}

}