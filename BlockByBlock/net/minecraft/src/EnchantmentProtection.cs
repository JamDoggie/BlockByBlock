namespace net.minecraft.src
{
	public class EnchantmentProtection : Enchantment
	{
		private static readonly string[] protectionName = new string[]{"all", "fire", "fall", "explosion", "projectile"};
		private static readonly int[] baseEnchantability = new int[]{1, 10, 5, 5, 3};
		private static readonly int[] levelEnchantability = new int[]{16, 8, 6, 8, 6};
		private static readonly int[] thresholdEnchantability = new int[]{20, 12, 10, 12, 15};
		public readonly int protectionType;

		public EnchantmentProtection(int i1, int i2, int i3) : base(i1, i2, EnumEnchantmentType.armor)
		{
			protectionType = i3;
			if (i3 == 2)
			{
				type = EnumEnchantmentType.armor_feet;
			}

		}

		public override int getMinEnchantability(int i1)
		{
			return baseEnchantability[this.protectionType] + (i1 - 1) * levelEnchantability[protectionType];
		}

		public override int getMaxEnchantability(int i1)
		{
			return getMinEnchantability(i1) + thresholdEnchantability[protectionType];
		}

		public override int MaxLevel
		{
			get
			{
				return 4;
			}
		}

		public override int calcModifierDamage(int i1, DamageSource damageSource2)
		{
			if (damageSource2.canHarmInCreative())
			{
				return 0;
			}
			else
			{
				int i3 = (6 + i1 * i1) / 2;
				return this.protectionType == 0 ? i3 : (this.protectionType == 1 && damageSource2.getFireDamage() ? i3 : (this.protectionType == 2 && damageSource2 == DamageSource.fall ? i3 * 2 : (this.protectionType == 3 && damageSource2 == DamageSource.explosion ? i3 : (this.protectionType == 4 && damageSource2.Projectile ? i3 : 0))));
			}
		}

		public override string Name
		{
			get
			{
				return "enchantment.protect." + protectionName[this.protectionType];
			}
		}

		public override bool canApplyTogether(Enchantment enchantment1)
		{
			if (enchantment1 is EnchantmentProtection)
			{
				EnchantmentProtection enchantmentProtection2 = (EnchantmentProtection)enchantment1;
				return enchantmentProtection2.protectionType == this.protectionType ? false : this.protectionType == 2 || enchantmentProtection2.protectionType == 2;
			}
			else
			{
				return base.canApplyTogether(enchantment1);
			}
		}
	}

}