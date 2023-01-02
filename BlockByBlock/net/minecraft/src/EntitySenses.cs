using System.Collections;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntitySenses
	{
		internal EntityLiving entityObj;
		internal ArrayList canSeeCachePositive = new ArrayList();
		internal ArrayList canSeeCacheNegative = new ArrayList();

		public EntitySenses(EntityLiving entityLiving1)
		{
			this.entityObj = entityLiving1;
		}

		public virtual void clearSensingCache()
		{
			this.canSeeCachePositive.Clear();
			this.canSeeCacheNegative.Clear();
		}

		public virtual bool canSee(Entity entity1)
		{
			if (this.canSeeCachePositive.Contains(entity1))
			{
				return true;
			}
			else if (this.canSeeCacheNegative.Contains(entity1))
			{
				return false;
			}
			else
			{
				Profiler.startSection("canSee");
				bool z2 = this.entityObj.canEntityBeSeen(entity1);
				Profiler.endSection();
				if (z2)
				{
					this.canSeeCachePositive.Add(entity1);
				}
				else
				{
					this.canSeeCacheNegative.Add(entity1);
				}

				return z2;
			}
		}
	}

}