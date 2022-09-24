namespace net.minecraft.src
{
	public class EnchantmentUntouching : Enchantment
	{
		protected internal EnchantmentUntouching(int i1, int i2) : base(i1, i2, EnumEnchantmentType.digger)
		{
			Name = "untouching";
		}

		public override int getMinEnchantability(int i1)
		{
			return 25;
		}

		public override int getMaxEnchantability(int i1)
		{
			return base.getMinEnchantability(i1) + 50;
		}

		public override int MaxLevel
		{
			get
			{
				return 1;
			}
		}

		public override bool canApplyTogether(Enchantment enchantment1)
		{
			return base.canApplyTogether(enchantment1) && enchantment1.effectId != fortune.effectId;
		}
	}

}