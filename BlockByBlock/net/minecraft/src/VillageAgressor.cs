using net.minecraft.client.entity;

namespace net.minecraft.src
{
    internal class VillageAgressor
	{
		public EntityLiving agressor;
		public int agressionTime;
		internal readonly Village villageObj;

		internal VillageAgressor(Village village1, EntityLiving entityLiving2, int i3)
		{
			this.villageObj = village1;
			this.agressor = entityLiving2;
			this.agressionTime = i3;
		}
	}

}