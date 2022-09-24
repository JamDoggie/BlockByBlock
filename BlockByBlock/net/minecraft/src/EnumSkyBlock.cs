using System.Collections.Generic;

namespace net.minecraft.src
{
	public sealed class EnumSkyBlock
	{
		public static readonly EnumSkyBlock Sky = new EnumSkyBlock("Sky", InnerEnum.Sky, 15);
		public static readonly EnumSkyBlock Block = new EnumSkyBlock("Block", InnerEnum.Block, 0);

		private static readonly List<EnumSkyBlock> valueList = new List<EnumSkyBlock>();

		static EnumSkyBlock()
		{
			valueList.Add(Sky);
			valueList.Add(Block);
		}

		public enum InnerEnum
		{
			Sky,
			Block
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		public readonly int defaultLightValue;

		private EnumSkyBlock(string name, InnerEnum innerEnum, int i3)
		{
			this.defaultLightValue = i3;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public static EnumSkyBlock[] values()
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

		public static EnumSkyBlock valueOf(string name)
		{
			foreach (EnumSkyBlock enumInstance in EnumSkyBlock.valueList)
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