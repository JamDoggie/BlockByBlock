namespace net.minecraft.src
{
	public class EnchantmentArrowKnockback : Enchantment
	{
		public EnchantmentArrowKnockback(int i1, int i2) : base(i1, i2, EnumEnchantmentType.bow)
		{
			Name = "arrowKnockback";
		}

		public override int getMinEnchantability(int i1)
		{
			return 12 + (i1 - 1) * 20;
		}

		public override int getMaxEnchantability(int i1)
		{
			return this.getMinEnchantability(i1) + 25;
		}

		public override int MaxLevel
		{
			get
			{
				return 2;
			}
		}
	}

}