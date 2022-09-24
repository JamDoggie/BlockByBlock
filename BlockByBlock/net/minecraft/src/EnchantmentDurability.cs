namespace net.minecraft.src
{
	public class EnchantmentDurability : Enchantment
	{
		protected internal EnchantmentDurability(int i1, int i2) : base(i1, i2, EnumEnchantmentType.digger)
		{
			Name = "durability";
		}

		public override int getMinEnchantability(int i1)
		{
			return 5 + (i1 - 1) * 10;
		}

		public override int getMaxEnchantability(int i1)
		{
			return base.getMinEnchantability(i1) + 50;
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