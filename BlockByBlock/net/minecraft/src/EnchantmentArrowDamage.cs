namespace net.minecraft.src
{
	public class EnchantmentArrowDamage : Enchantment
	{
		public EnchantmentArrowDamage(int i1, int i2) : base(i1, i2, EnumEnchantmentType.bow)
		{
			Name = "arrowDamage";
		}

		public override int getMinEnchantability(int i1)
		{
			return 1 + (i1 - 1) * 10;
		}

		public override int getMaxEnchantability(int i1)
		{
			return this.getMinEnchantability(i1) + 15;
		}

		public override int MaxLevel
		{
			get
			{
				return 5;
			}
		}
	}

}