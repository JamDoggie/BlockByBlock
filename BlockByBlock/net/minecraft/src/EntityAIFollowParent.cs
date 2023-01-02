using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIFollowParent : EntityAIBase
	{
		internal EntityAnimal childAnimal;
		internal EntityAnimal parentAnimal;
		internal float field_48248_c;
		private int field_48246_d;

		public EntityAIFollowParent(EntityAnimal entityAnimal1, float f2)
		{
			this.childAnimal = entityAnimal1;
			this.field_48248_c = f2;
		}

		public override bool shouldExecute()
		{
			if (this.childAnimal.GrowingAge >= 0)
			{
				return false;
			}
			else
			{
				System.Collections.IList list1 = this.childAnimal.worldObj.getEntitiesWithinAABB(this.childAnimal.GetType(), this.childAnimal.boundingBox.expand(8.0D, 4.0D, 8.0D));
				EntityAnimal entityAnimal2 = null;
				double d3 = double.MaxValue;
				System.Collections.IEnumerator iterator5 = list1.GetEnumerator();

				while (iterator5.MoveNext())
				{
					Entity entity6 = (Entity)iterator5.Current;
					EntityAnimal entityAnimal7 = (EntityAnimal)entity6;
					if (entityAnimal7.GrowingAge >= 0)
					{
						double d8 = this.childAnimal.getDistanceSqToEntity(entityAnimal7);
						if (d8 <= d3)
						{
							d3 = d8;
							entityAnimal2 = entityAnimal7;
						}
					}
				}

				if (entityAnimal2 == null)
				{
					return false;
				}
				else if (d3 < 9.0D)
				{
					return false;
				}
				else
				{
					this.parentAnimal = entityAnimal2;
					return true;
				}
			}
		}

		public override bool continueExecuting()
		{
			if (!this.parentAnimal.EntityAlive)
			{
				return false;
			}
			else
			{
				double d1 = this.childAnimal.getDistanceSqToEntity(this.parentAnimal);
				return d1 >= 9.0D && d1 <= 256.0D;
			}
		}

		public override void startExecuting()
		{
			this.field_48246_d = 0;
		}

		public override void resetTask()
		{
			this.parentAnimal = null;
		}

		public override void updateTask()
		{
			if (--this.field_48246_d <= 0)
			{
				this.field_48246_d = 10;
				this.childAnimal.Navigator.func_48667_a(this.parentAnimal, this.field_48248_c);
			}
		}
	}

}