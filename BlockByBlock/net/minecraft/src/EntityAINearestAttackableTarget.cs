using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAINearestAttackableTarget : EntityAITarget
	{
		internal EntityLiving targetEntity;
		internal Type targetClass;
		internal int field_48386_f;
		private EntityAINearestAttackableTargetSorter entDistanceSorter;

		public EntityAINearestAttackableTarget(EntityLiving entityLiving1, Type class2, float f3, int i4, bool z5) : this(entityLiving1, class2, f3, i4, z5, false)
		{
		}

		public EntityAINearestAttackableTarget(EntityLiving entityLiving1, Type class2, float f3, int i4, bool z5, bool z6) : base(entityLiving1, f3, z5, z6)
		{
			this.targetClass = class2;
			this.field_48379_d = f3;
			this.field_48386_f = i4;
			this.entDistanceSorter = new EntityAINearestAttackableTargetSorter(this, entityLiving1);
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (this.field_48386_f > 0 && this.taskOwner.RNG.Next(this.field_48386_f) != 0)
			{
				return false;
			}
			else
			{
				if (this.targetClass == typeof(EntityPlayer))
				{
					EntityPlayer entityPlayer1 = this.taskOwner.worldObj.getClosestVulnerablePlayerToEntity(this.taskOwner, (double)this.field_48379_d);
					if (this.func_48376_a(entityPlayer1, false))
					{
						this.targetEntity = entityPlayer1;
						return true;
					}
				}
				else
				{
					System.Collections.IList list5 = this.taskOwner.worldObj.getEntitiesWithinAABB(this.targetClass, this.taskOwner.boundingBox.expand((double)this.field_48379_d, 4.0D, (double)this.field_48379_d));
					list5 = list5.Cast<Entity>().OrderBy(ent => ent, this.entDistanceSorter).ToList();
					System.Collections.IEnumerator iterator2 = list5.GetEnumerator();

					while (iterator2.MoveNext())
					{
						Entity entity3 = (Entity)iterator2.Current;
						EntityLiving entityLiving4 = (EntityLiving)entity3;
						if (this.func_48376_a(entityLiving4, false))
						{
							this.targetEntity = entityLiving4;
							return true;
						}
					}
				}

				return false;
			}
		}

		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.targetEntity;
			base.startExecuting();
		}
	}

}