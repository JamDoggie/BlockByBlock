using System.Collections.Generic;

namespace net.minecraft.src
{
	public sealed class EnumEnchantmentType
	{
		public static readonly EnumEnchantmentType all = new EnumEnchantmentType("all", InnerEnum.all);
		public static readonly EnumEnchantmentType armor = new EnumEnchantmentType("armor", InnerEnum.armor);
		public static readonly EnumEnchantmentType armor_feet = new EnumEnchantmentType("armor_feet", InnerEnum.armor_feet);
		public static readonly EnumEnchantmentType armor_legs = new EnumEnchantmentType("armor_legs", InnerEnum.armor_legs);
		public static readonly EnumEnchantmentType armor_torso = new EnumEnchantmentType("armor_torso", InnerEnum.armor_torso);
		public static readonly EnumEnchantmentType armor_head = new EnumEnchantmentType("armor_head", InnerEnum.armor_head);
		public static readonly EnumEnchantmentType weapon = new EnumEnchantmentType("weapon", InnerEnum.weapon);
		public static readonly EnumEnchantmentType digger = new EnumEnchantmentType("digger", InnerEnum.digger);
		public static readonly EnumEnchantmentType bow = new EnumEnchantmentType("bow", InnerEnum.bow);

		private static readonly List<EnumEnchantmentType> valueList = new List<EnumEnchantmentType>();

		static EnumEnchantmentType()
		{
			valueList.Add(all);
			valueList.Add(armor);
			valueList.Add(armor_feet);
			valueList.Add(armor_legs);
			valueList.Add(armor_torso);
			valueList.Add(armor_head);
			valueList.Add(weapon);
			valueList.Add(digger);
			valueList.Add(bow);
		}

		public enum InnerEnum
		{
			all,
			armor,
			armor_feet,
			armor_legs,
			armor_torso,
			armor_head,
			weapon,
			digger,
			bow
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private EnumEnchantmentType(string name, InnerEnum innerEnum)
		{
			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public bool canEnchantItem(Item item1)
		{
			if (this == all)
			{
				return true;
			}
			else if (item1 is ItemArmor)
			{
				if (this == armor)
				{
					return true;
				}
				else
				{
					ItemArmor itemArmor2 = (ItemArmor)item1;
					return itemArmor2.armorType == 0 ? this == armor_head : (itemArmor2.armorType == 2 ? this == armor_legs : (itemArmor2.armorType == 1 ? this == armor_torso : (itemArmor2.armorType == 3 ? this == armor_feet : false)));
				}
			}
			else
			{
				return item1 is ItemSword ? this == weapon : (item1 is ItemTool ? this == digger : (item1 is ItemBow ? this == bow : false));
			}
		}

		public static EnumEnchantmentType[] values()
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

		public static EnumEnchantmentType valueOf(string name)
		{
			foreach (EnumEnchantmentType enumInstance in EnumEnchantmentType.valueList)
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