namespace net.minecraft.src
{
	public class EnchantmentFireAspect : Enchantment
	{
		protected internal EnchantmentFireAspect(int i1, int i2) : base(i1, i2, EnumEnchantmentType.weapon)
		{
			Name = "fire";
		}

		public override int getMinEnchantability(int i1)
		{
			return 10 + 20 * (i1 - 1);
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