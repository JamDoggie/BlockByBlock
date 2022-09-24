namespace net.minecraft.src
{
	public class EnchantmentLootBonus : Enchantment
	{
		protected internal EnchantmentLootBonus(int i1, int i2, EnumEnchantmentType enumEnchantmentType3) : base(i1, i2, enumEnchantmentType3)
		{
			Name = "lootBonus";
			if (enumEnchantmentType3 == EnumEnchantmentType.digger)
			{
				Name = "lootBonusDigger";
			}

		}

		public override int getMinEnchantability(int i1)
		{
			return 20 + (i1 - 1) * 12;
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

		public override bool canApplyTogether(Enchantment enchantment1)
		{
			return base.canApplyTogether(enchantment1) && enchantment1.effectId != silkTouch.effectId;
		}
	}

}