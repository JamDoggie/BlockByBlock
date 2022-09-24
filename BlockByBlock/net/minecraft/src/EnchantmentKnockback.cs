namespace net.minecraft.src
{
	public class EnchantmentKnockback : Enchantment
	{
		protected internal EnchantmentKnockback(int i1, int i2) : base(i1, i2, EnumEnchantmentType.weapon)
		{
			Name = "knockback";
		}

		public override int getMinEnchantability(int i1)
		{
			return 5 + 20 * (i1 - 1);
		}

		public override int getMaxEnchantability(int i1)
		{
			return base.getMinEnchantability(i1) + 50;
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