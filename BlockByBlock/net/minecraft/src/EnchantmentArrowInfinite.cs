namespace net.minecraft.src
{
	public class EnchantmentArrowInfinite : Enchantment
	{
		public EnchantmentArrowInfinite(int i1, int i2) : base(i1, i2, EnumEnchantmentType.bow)
		{
			Name = "arrowInfinite";
		}

		public override int getMinEnchantability(int i1)
		{
			return 20;
		}

		public override int getMaxEnchantability(int i1)
		{
			return 50;
		}

		public override int MaxLevel
		{
			get
			{
				return 1;
			}
		}
	}

}