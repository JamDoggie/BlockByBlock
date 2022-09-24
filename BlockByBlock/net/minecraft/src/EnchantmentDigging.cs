namespace net.minecraft.src
{
	public class EnchantmentDigging : Enchantment
	{
		protected internal EnchantmentDigging(int i1, int i2) : base(i1, i2, EnumEnchantmentType.digger)
		{
			Name = "digging";
		}

		public override int getMinEnchantability(int i1)
		{
			return 1 + 15 * (i1 - 1);
		}

		public override int getMaxEnchantability(int i1)
		{
			return base.getMinEnchantability(i1) + 50;
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