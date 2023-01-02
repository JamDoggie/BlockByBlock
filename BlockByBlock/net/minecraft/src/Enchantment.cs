using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public abstract class Enchantment
	{
		public static readonly Enchantment[] enchantmentsList = new Enchantment[256];
		public static readonly Enchantment protection = new EnchantmentProtection(0, 10, 0);
		public static readonly Enchantment fireProtection = new EnchantmentProtection(1, 5, 1);
		public static readonly Enchantment featherFalling = new EnchantmentProtection(2, 5, 2);
		public static readonly Enchantment blastProtection = new EnchantmentProtection(3, 2, 3);
		public static readonly Enchantment projectileProtection = new EnchantmentProtection(4, 5, 4);
		public static readonly Enchantment respiration = new EnchantmentOxygen(5, 2);
		public static readonly Enchantment aquaAffinity = new EnchantmentWaterWorker(6, 2);
		public static readonly Enchantment sharpness = new EnchantmentDamage(16, 10, 0);
		public static readonly Enchantment smite = new EnchantmentDamage(17, 5, 1);
		public static readonly Enchantment baneOfArthropods = new EnchantmentDamage(18, 5, 2);
		public static readonly Enchantment knockback = new EnchantmentKnockback(19, 5);
		public static readonly Enchantment fireAspect = new EnchantmentFireAspect(20, 2);
		public static readonly Enchantment looting = new EnchantmentLootBonus(21, 2, EnumEnchantmentType.weapon);
		public static readonly Enchantment efficiency = new EnchantmentDigging(32, 10);
		public static readonly Enchantment silkTouch = new EnchantmentUntouching(33, 1);
		public static readonly Enchantment unbreaking = new EnchantmentDurability(34, 5);
		public static readonly Enchantment fortune = new EnchantmentLootBonus(35, 2, EnumEnchantmentType.digger);
		public static readonly Enchantment power = new EnchantmentArrowDamage(48, 10);
		public static readonly Enchantment punch = new EnchantmentArrowKnockback(49, 2);
		public static readonly Enchantment flame = new EnchantmentArrowFire(50, 2);
		public static readonly Enchantment infinity = new EnchantmentArrowInfinite(51, 1);
		public readonly int effectId;
		private readonly int weight;
		public EnumEnchantmentType type;
		protected internal string name;

		protected internal Enchantment(int i1, int i2, EnumEnchantmentType enumEnchantmentType3)
		{
			this.effectId = i1;
			this.weight = i2;
			this.type = enumEnchantmentType3;
			if (enchantmentsList[i1] != null)
			{
				throw new System.ArgumentException("Duplicate enchantment id!");
			}
			else
			{
				enchantmentsList[i1] = this;
			}
		}

		public virtual int Weight
		{
			get
			{
				return this.weight;
			}
		}

		public virtual int MinLevel
		{
			get
			{
				return 1;
			}
		}

		public virtual int MaxLevel
		{
			get
			{
				return 1;
			}
		}

		public virtual int getMinEnchantability(int i1)
		{
			return 1 + i1 * 10;
		}

		public virtual int getMaxEnchantability(int i1)
		{
			return this.getMinEnchantability(i1) + 5;
		}

		public virtual int calcModifierDamage(int i1, DamageSource damageSource2)
		{
			return 0;
		}

		public virtual int calcModifierLiving(int i1, EntityLiving entityLiving2)
		{
			return 0;
		}

		public virtual bool canApplyTogether(Enchantment enchantment1)
		{
			return this != enchantment1;
		}

		public virtual Enchantment setName(string string1)
		{
			this.name = string1;
			return this;
		}

		public virtual string Name
		{
			get
			{
				return "enchantment." + name;
			}

			protected set
            {
				name = value;
            }
		}

		public virtual string getTranslatedName(int i1)
		{
			string string2 = StatCollector.translateToLocal(this.Name);
			return string2 + " " + StatCollector.translateToLocal("enchantment.level." + i1);
		}
	}

}