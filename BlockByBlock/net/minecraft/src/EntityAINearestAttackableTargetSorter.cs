using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAINearestAttackableTargetSorter : IComparer<Entity>
	{
		private Entity theEntity;
		internal readonly EntityAINearestAttackableTarget parent;

		public EntityAINearestAttackableTargetSorter(EntityAINearestAttackableTarget entityAINearestAttackableTarget1, Entity entity2)
		{
			this.parent = entityAINearestAttackableTarget1;
			this.theEntity = entity2;
		}

		public virtual int func_48469_a(Entity entity1, Entity entity2)
		{
			double d3 = this.theEntity.getDistanceSqToEntity(entity1);
			double d5 = this.theEntity.getDistanceSqToEntity(entity2);
			return d3 < d5 ? -1 : (d3 > d5 ? 1 : 0);
		}

		public virtual int Compare(Entity? object1, Entity? object2)
		{
			return this.func_48469_a(object1, object2);
		}
	}

}