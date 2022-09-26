using System.Collections.Generic;

namespace net.minecraft.src
{
	// PORTING TODO: Check this class. I think it's fine but not sure.
	public sealed class EnumArmorMaterial
	{
        public static readonly EnumArmorMaterial CLOTH = new EnumArmorMaterial("CLOTH", InnerEnum.CLOTH, 5, new int[] { 1, 3, 2, 1 }, 15);
        public static readonly EnumArmorMaterial CHAIN = new EnumArmorMaterial("CHAIN", InnerEnum.CHAIN, 15, new int[] { 2, 5, 4, 1 }, 12);
        public static readonly EnumArmorMaterial IRON = new EnumArmorMaterial("IRON", InnerEnum.IRON, 15, new int[] { 2, 6, 5, 2 }, 9);
        public static readonly EnumArmorMaterial GOLD = new EnumArmorMaterial("GOLD", InnerEnum.GOLD, 7, new int[] { 2, 5, 3, 1 }, 25);
        public static readonly EnumArmorMaterial DIAMOND = new EnumArmorMaterial("DIAMOND", InnerEnum.DIAMOND, 33, new int[] { 3, 8, 6, 3 }, 10);

        private static readonly List<EnumArmorMaterial> valueList = new List<EnumArmorMaterial>();

		static EnumArmorMaterial()
		{
			valueList.Add(new EnumArmorMaterial("CLOTH", InnerEnum.CLOTH, 5, new int[] { 1, 3, 2, 1 }, 15));
			valueList.Add(new EnumArmorMaterial("CHAIN", InnerEnum.CHAIN, 15, new int[] { 2, 5, 4, 1 }, 12));
			valueList.Add(new EnumArmorMaterial("IRON", InnerEnum.IRON, 15, new int[] { 2, 6, 5, 2 }, 9));
			valueList.Add(new EnumArmorMaterial("GOLD", InnerEnum.GOLD, 7, new int[] { 2, 5, 3, 1 }, 25));
			valueList.Add(new EnumArmorMaterial("DIAMOND", InnerEnum.DIAMOND, 33, new int[] { 3, 8, 6, 3 }, 10));
		}

		public enum InnerEnum
		{
			CLOTH,
			CHAIN,
			IRON,
			GOLD,
			DIAMOND
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private EnumArmorMaterial(string name, InnerEnum innerEnum)
		{
			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		private int maxDamageFactor;
		private int[] damageReductionAmountArray;
		private int enchantability;

		private EnumArmorMaterial(string name, InnerEnum innerEnum, int i3, int[] i4, int i5)
		{
			this.maxDamageFactor = i3;
			this.damageReductionAmountArray = i4;
			this.enchantability = i5;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public int getDurability(int i1)
		{
			return ItemArmor.MaxDamageArray[i1] * this.maxDamageFactor;
		}

		public int getDamageReductionAmount(int i1)
		{
			return this.damageReductionAmountArray[i1];
		}

		public int Enchantability
		{
			get
			{
				return this.enchantability;
			}
		}

		public static EnumArmorMaterial[] values()
		{
			return valueList.ToArray();
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static EnumArmorMaterial valueOf(string name)
		{
			foreach (EnumArmorMaterial enumInstance in EnumArmorMaterial.valueList)
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}

}