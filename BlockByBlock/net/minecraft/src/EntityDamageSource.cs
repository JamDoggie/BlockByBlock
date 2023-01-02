using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityDamageSource : DamageSource
	{
		protected internal Entity damageSourceEntity;

		public EntityDamageSource(string string1, Entity entity2) : base(string1)
		{
			this.damageSourceEntity = entity2;
		}

		public override Entity Entity
		{
			get
			{
				return this.damageSourceEntity;
			}
		}
	}

}