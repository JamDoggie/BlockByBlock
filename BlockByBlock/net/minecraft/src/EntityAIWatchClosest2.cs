using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIWatchClosest2 : EntityAIWatchClosest
	{
		public EntityAIWatchClosest2(EntityLiving entityLiving1, Type class2, float f3) : base(entityLiving1, class2, f3)
		{
			this.MutexBits = 3;
		}

		public EntityAIWatchClosest2(EntityLiving entityLiving1, Type class2, float f3, float f4) : base(entityLiving1, class2, f3, f4)
		{
			this.MutexBits = 3;
		}
	}

}