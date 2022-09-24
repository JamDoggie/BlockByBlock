using System;

namespace net.minecraft.src
{

	public class EntityAIMate : EntityAIBase
	{
		private EntityAnimal theAnimal;
		internal World theWorld;
		private EntityAnimal targetMate;
		internal int field_48261_b = 0;
		internal float field_48262_c;

		public EntityAIMate(EntityAnimal entityAnimal1, float f2)
		{
			this.theAnimal = entityAnimal1;
			this.theWorld = entityAnimal1.worldObj;
			this.field_48262_c = f2;
			this.MutexBits = 3;
		}

		public override bool shouldExecute()
		{
			if (!this.theAnimal.InLove)
			{
				return false;
			}
			else
			{
				this.targetMate = this.func_48258_h();
				return this.targetMate != null;
			}
		}

		public override bool continueExecuting()
		{
			return this.targetMate.EntityAlive && this.targetMate.InLove && this.field_48261_b < 60;
		}

		public override void resetTask()
		{
			this.targetMate = null;
			this.field_48261_b = 0;
		}

		public override void updateTask()
		{
			this.theAnimal.LookHelper.setLookPositionWithEntity(this.targetMate, 10.0F, (float)this.theAnimal.VerticalFaceSpeed);
			this.theAnimal.Navigator.func_48667_a(this.targetMate, this.field_48262_c);
			++this.field_48261_b;
			if (this.field_48261_b == 60)
			{
				this.func_48257_i();
			}

		}

		private EntityAnimal func_48258_h()
		{
			float f1 = 8.0F;
			System.Collections.IList list2 = this.theWorld.getEntitiesWithinAABB(this.theAnimal.GetType(), this.theAnimal.boundingBox.expand((double)f1, (double)f1, (double)f1));
			System.Collections.IEnumerator iterator3 = list2.GetEnumerator();

			EntityAnimal entityAnimal5;
			do
			{
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
				if (!iterator3.hasNext())
				{
					return null;
				}

//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
				Entity entity4 = (Entity)iterator3.next();
				entityAnimal5 = (EntityAnimal)entity4;
			} while (!this.theAnimal.func_48135_b(entityAnimal5));

			return entityAnimal5;
		}

		private void func_48257_i()
		{
			EntityAnimal entityAnimal1 = this.theAnimal.spawnBabyAnimal(this.targetMate);
			if (entityAnimal1 != null)
			{
				this.theAnimal.GrowingAge = 6000;
				this.targetMate.GrowingAge = 6000;
				this.theAnimal.resetInLove();
				this.targetMate.resetInLove();
				entityAnimal1.GrowingAge = -24000;
				entityAnimal1.setLocationAndAngles(this.theAnimal.posX, this.theAnimal.posY, this.theAnimal.posZ, 0.0F, 0.0F);
				this.theWorld.spawnEntityInWorld(entityAnimal1);
				Random random2 = this.theAnimal.RNG;

				for (int i3 = 0; i3 < 7; ++i3)
				{
					double d4 = random2.nextGaussian() * 0.02D;
					double d6 = random2.nextGaussian() * 0.02D;
					double d8 = random2.nextGaussian() * 0.02D;
					this.theWorld.spawnParticle("heart", this.theAnimal.posX + (double)(random2.nextFloat() * this.theAnimal.width * 2.0F) - (double)this.theAnimal.width, this.theAnimal.posY + 0.5D + (double)(random2.nextFloat() * this.theAnimal.height), this.theAnimal.posZ + (double)(random2.nextFloat() * this.theAnimal.width * 2.0F) - (double)this.theAnimal.width, d4, d6, d8);
				}

			}
		}
	}

}