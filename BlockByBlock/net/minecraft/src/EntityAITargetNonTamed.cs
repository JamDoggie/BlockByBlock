using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAITargetNonTamed : EntityAINearestAttackableTarget
	{
		private EntityTameable field_48390_g;

		public EntityAITargetNonTamed(EntityTameable entityTameable1, Type class2, float f3, int i4, bool z5) : base(entityTameable1, class2, f3, i4, z5)
		{
			this.field_48390_g = entityTameable1;
		}

		public override bool shouldExecute()
		{
			return this.field_48390_g.Tamed ? false : base.shouldExecute();
		}
	}

}