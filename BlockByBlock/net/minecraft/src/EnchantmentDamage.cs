using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EnchantmentDamage : Enchantment
	{
		private static readonly string[] protectionName = new string[]{"all", "undead", "arthropods"};
		private static readonly int[] baseEnchantability = new int[]{1, 5, 5};
		private static readonly int[] levelEnchantability = new int[]{16, 8, 8};
		private static readonly int[] thresholdEnchantability = new int[]{20, 20, 20};
		public readonly int damageType;

		public EnchantmentDamage(int i1, int i2, int i3) : base(i1, i2, EnumEnchantmentType.weapon)
		{
			damageType = i3;
		}

		public override int getMinEnchantability(int i1)
		{
			return baseEnchantability[this.damageType] + (i1 - 1) * levelEnchantability[this.damageType];
		}

		public override int getMaxEnchantability(int i1)
		{
			return this.getMinEnchantability(i1) + thresholdEnchantability[this.damageType];
		}

		public override int MaxLevel
		{
			get
			{
				return 5;
			}
		}

		public override int calcModifierLiving(int i1, EntityLiving entityLiving2)
		{
			return this.damageType == 0 ? i1 * 3 : (this.damageType == 1 && entityLiving2.CreatureAttribute == EnumCreatureAttribute.UNDEAD ? i1 * 4 : (this.damageType == 2 && entityLiving2.CreatureAttribute == EnumCreatureAttribute.ARTHROPOD ? i1 * 4 : 0));
		}

		public override string Name
		{
			get
			{
				return "enchantment.damage." + protectionName[this.damageType];
			}
		}

		public override bool canApplyTogether(Enchantment enchantment1)
		{
			return !(enchantment1 is EnchantmentDamage);
		}
	}

}