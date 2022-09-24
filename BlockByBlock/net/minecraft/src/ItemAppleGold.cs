namespace net.minecraft.src
{
	public class ItemAppleGold : ItemFood
	{
		public ItemAppleGold(int i1, int i2, float f3, bool z4) : base(i1, i2, f3, z4)
		{
		}

		public override bool hasEffect(ItemStack itemStack1)
		{
			return true;
		}

		public override EnumRarity getRarity(ItemStack itemStack1)
		{
			return EnumRarity.epic;
		}
	}

}