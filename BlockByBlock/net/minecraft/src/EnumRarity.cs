using System.Collections.Generic;

namespace net.minecraft.src
{
	public sealed class EnumRarity
	{
		public static readonly EnumRarity common = new EnumRarity("common", InnerEnum.common, 15, "Common");
		public static readonly EnumRarity uncommon = new EnumRarity("uncommon", InnerEnum.uncommon, 14, "Uncommon");
		public static readonly EnumRarity rare = new EnumRarity("rare", InnerEnum.rare, 11, "Rare");
		public static readonly EnumRarity epic = new EnumRarity("epic", InnerEnum.epic, 13, "Epic");

		private static readonly List<EnumRarity> valueList = new List<EnumRarity>();

		static EnumRarity()
		{
			valueList.Add(common);
			valueList.Add(uncommon);
			valueList.Add(rare);
			valueList.Add(epic);
		}

		public enum InnerEnum
		{
			common,
			uncommon,
			rare,
			epic
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		public readonly int nameColor;
		public readonly string field_40532_f;

		private EnumRarity(string name, InnerEnum innerEnum, int i3, string string4)
		{
			this.nameColor = i3;
			this.field_40532_f = string4;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public static EnumRarity[] values()
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

		public static EnumRarity valueOf(string name)
		{
			foreach (EnumRarity enumInstance in EnumRarity.valueList)
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