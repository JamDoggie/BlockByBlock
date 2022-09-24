namespace net.minecraft.src
{
	public class EnchantmentOxygen : Enchantment
	{
		public EnchantmentOxygen(int i1, int i2) : base(i1, i2, EnumEnchantmentType.armor_head)
		{
			Name = "oxygen";
		}

		public override int getMinEnchantability(int i1)
		{
			return 10 * i1;
		}

		public override int getMaxEnchantability(int i1)
		{
			return getMinEnchantability(i1) + 30;
		}

		public override int MaxLevel
		{
			get
			{
				return 3;
			}
		}
	}

}