using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIHurtByTarget : EntityAITarget
	{
		internal bool field_48395_a;

		public EntityAIHurtByTarget(EntityLiving entityLiving1, bool z2) : base(entityLiving1, 16.0F, false)
		{
			this.field_48395_a = z2;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			return this.func_48376_a(this.taskOwner.AITarget, true);
		}

		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.taskOwner.AITarget;
			if (this.field_48395_a)
			{
				System.Collections.IList list1 = this.taskOwner.worldObj.getEntitiesWithinAABB(this.taskOwner.GetType(), AxisAlignedBB.getBoundingBoxFromPool(this.taskOwner.posX, this.taskOwner.posY, this.taskOwner.posZ, this.taskOwner.posX + 1.0D, this.taskOwner.posY + 1.0D, this.taskOwner.posZ + 1.0D).expand((double)this.field_48379_d, 4.0D, (double)this.field_48379_d));
				System.Collections.IEnumerator iterator2 = list1.GetEnumerator();

				while (iterator2.MoveNext())
				{
					Entity entity3 = (Entity)iterator2.Current;
					EntityLiving entityLiving4 = (EntityLiving)entity3;
					if (this.taskOwner != entityLiving4 && entityLiving4.AttackTarget == null)
					{
						entityLiving4.AttackTarget = this.taskOwner.AITarget;
					}
				}
			}

			base.startExecuting();
		}
	}

}