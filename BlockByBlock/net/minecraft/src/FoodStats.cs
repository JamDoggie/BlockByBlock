using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class FoodStats
	{
		private int foodLevel = 20;
		private float foodSaturationLevel = 5.0F;
		private float foodExhaustionLevel;
		private int foodTimer = 0;
		private int prevFoodLevel = 20;

		public virtual void addStats(int i1, float f2)
		{
			this.foodLevel = Math.Min(i1 + this.foodLevel, 20);
			this.foodSaturationLevel = Math.Min(this.foodSaturationLevel + (float)i1 * f2 * 2.0F, (float)this.foodLevel);
		}

		public virtual void addStats(ItemFood itemFood1)
		{
			this.addStats(itemFood1.HealAmount, itemFood1.SaturationModifier);
		}

		public virtual void onUpdate(EntityPlayer entityPlayer1)
		{
			int i2 = entityPlayer1.worldObj.difficultySetting;
			this.prevFoodLevel = this.foodLevel;
			if (this.foodExhaustionLevel > 4.0F)
			{
				this.foodExhaustionLevel -= 4.0F;
				if (this.foodSaturationLevel > 0.0F)
				{
					this.foodSaturationLevel = Math.Max(this.foodSaturationLevel - 1.0F, 0.0F);
				}
				else if (i2 > 0)
				{
					this.foodLevel = Math.Max(this.foodLevel - 1, 0);
				}
			}

			if (this.foodLevel >= 18 && entityPlayer1.shouldHeal())
			{
				++this.foodTimer;
				if (this.foodTimer >= 80)
				{
					entityPlayer1.heal(1);
					this.foodTimer = 0;
				}
			}
			else if (this.foodLevel <= 0)
			{
				++this.foodTimer;
				if (this.foodTimer >= 80)
				{
					if (entityPlayer1.Health > 10 || i2 >= 3 || entityPlayer1.Health > 1 && i2 >= 2)
					{
						entityPlayer1.attackEntityFrom(DamageSource.starve, 1);
					}

					this.foodTimer = 0;
				}
			}
			else
			{
				this.foodTimer = 0;
			}

		}

		public virtual void readNBT(NBTTagCompound nBTTagCompound1)
		{
			if (nBTTagCompound1.hasKey("foodLevel"))
			{
				this.foodLevel = nBTTagCompound1.getInteger("foodLevel");
				this.foodTimer = nBTTagCompound1.getInteger("foodTickTimer");
				this.foodSaturationLevel = nBTTagCompound1.getFloat("foodSaturationLevel");
				this.foodExhaustionLevel = nBTTagCompound1.getFloat("foodExhaustionLevel");
			}

		}

		public virtual void writeNBT(NBTTagCompound nBTTagCompound1)
		{
			nBTTagCompound1.setInteger("foodLevel", this.foodLevel);
			nBTTagCompound1.setInteger("foodTickTimer", this.foodTimer);
			nBTTagCompound1.setFloat("foodSaturationLevel", this.foodSaturationLevel);
			nBTTagCompound1.setFloat("foodExhaustionLevel", this.foodExhaustionLevel);
		}

		public virtual int FoodLevel
		{
			get
			{
				return this.foodLevel;
			}
			set
			{
				this.foodLevel = value;
			}
		}

		public virtual int PrevFoodLevel
		{
			get
			{
				return this.prevFoodLevel;
			}
		}

		public virtual bool needFood()
		{
			return this.foodLevel < 20;
		}

		public virtual void addExhaustion(float f1)
		{
			this.foodExhaustionLevel = Math.Min(this.foodExhaustionLevel + f1, 40.0F);
		}

		public virtual float SaturationLevel
		{
			get
			{
				return this.foodSaturationLevel;
			}
		}


		public virtual float FoodSaturationLevel
		{
			set
			{
				this.foodSaturationLevel = value;
			}
		}
	}

}