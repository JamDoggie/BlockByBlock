using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemSaddle : Item
	{
		public ItemSaddle(int i1) : base(i1)
		{
			this.maxStackSize = 1;
		}

		public override void useItemOnEntity(ItemStack itemStack1, EntityLiving entityLiving2)
		{
			if (entityLiving2 is EntityPig)
			{
				EntityPig entityPig3 = (EntityPig)entityLiving2;
				if (!entityPig3.Saddled && !entityPig3.Child)
				{
					entityPig3.Saddled = true;
					--itemStack1.stackSize;
				}
			}

		}

		public override bool hitEntity(ItemStack itemStack1, EntityLiving entityLiving2, EntityLiving entityLiving3)
		{
			this.useItemOnEntity(itemStack1, entityLiving2);
			return true;
		}
	}

}