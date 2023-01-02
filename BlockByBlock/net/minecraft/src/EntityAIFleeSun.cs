using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;

namespace net.minecraft.src
{

    public class EntityAIFleeSun : EntityAIBase
	{
		private EntityCreature theCreature;
		private double shelterX;
		private double shelterY;
		private double shelterZ;
		private float field_48299_e;
		private World theWorld;

		public EntityAIFleeSun(EntityCreature entityCreature1, float f2)
		{
			this.theCreature = entityCreature1;
			this.field_48299_e = f2;
			this.theWorld = entityCreature1.worldObj;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (!this.theWorld.Daytime)
			{
				return false;
			}
			else if (!this.theCreature.Burning)
			{
				return false;
			}
			else if (!this.theWorld.canBlockSeeTheSky(MathHelper.floor_double(this.theCreature.posX), (int)this.theCreature.boundingBox.minY, MathHelper.floor_double(this.theCreature.posZ)))
			{
				return false;
			}
			else
			{
				Vec3D vec3D1 = this.findPossibleShelter();
				if (vec3D1 == null)
				{
					return false;
				}
				else
				{
					this.shelterX = vec3D1.xCoord;
					this.shelterY = vec3D1.yCoord;
					this.shelterZ = vec3D1.zCoord;
					return true;
				}
			}
		}

		public override bool continueExecuting()
		{
			return !this.theCreature.Navigator.noPath();
		}

		public override void startExecuting()
		{
			this.theCreature.Navigator.func_48666_a(this.shelterX, this.shelterY, this.shelterZ, this.field_48299_e);
		}

		private Vec3D findPossibleShelter()
		{
            RandomExtended random1 = this.theCreature.RNG;

			for (int i2 = 0; i2 < 10; ++i2)
			{
				int i3 = MathHelper.floor_double(this.theCreature.posX + (double)random1.Next(20) - 10.0D);
				int i4 = MathHelper.floor_double(this.theCreature.boundingBox.minY + (double)random1.Next(6) - 3.0D);
				int i5 = MathHelper.floor_double(this.theCreature.posZ + (double)random1.Next(20) - 10.0D);
				if (!this.theWorld.canBlockSeeTheSky(i3, i4, i5) && this.theCreature.getBlockPathWeight(i3, i4, i5) < 0.0F)
				{
					return Vec3D.createVector((double)i3, (double)i4, (double)i5);
				}
			}

			return null;
		}
	}

}