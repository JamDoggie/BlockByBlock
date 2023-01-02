using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemFishingRod : Item
	{
		public ItemFishingRod(int i1) : base(i1)
		{
			setMaxDamage(64);
			setMaxStackSize(1);
		}

		public override bool Full3D
		{
			get
			{
				return true;
			}
		}

		public override bool shouldRotateAroundWhenRendering()
		{
			return true;
		}

		public override ItemStack onItemRightClick(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			if (entityPlayer3.fishEntity != null)
			{
				int i4 = entityPlayer3.fishEntity.catchFish();
				itemStack1.damageItem(i4, entityPlayer3);
				entityPlayer3.swingItem();
			}
			else
			{
				world2.playSoundAtEntity(entityPlayer3, "random.bow", 0.5F, 0.4F / (itemRand.NextSingle() * 0.4F + 0.8F));
				if (!world2.isRemote)
				{
					world2.spawnEntityInWorld(new EntityFishHook(world2, entityPlayer3));
				}

				entityPlayer3.swingItem();
			}

			return itemStack1;
		}
	}

}