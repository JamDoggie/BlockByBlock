using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityDamageSourceIndirect : EntityDamageSource
	{
		private Entity indirectEntity;

		public EntityDamageSourceIndirect(string string1, Entity entity2, Entity entity3) : base(string1, entity2)
		{
			this.indirectEntity = entity3;
		}

		public override Entity SourceOfDamage
		{
			get
			{
				return this.damageSourceEntity;
			}
		}

		public override Entity Entity
		{
			get
			{
				return this.indirectEntity;
			}
		}
	}

}