using System.Collections.Generic;

namespace net.minecraft.src
{
	public sealed class EnumToolMaterial
	{
		public static readonly EnumToolMaterial WOOD = new EnumToolMaterial("WOOD", InnerEnum.WOOD, 0, 59, 2.0F, 0, 15);
		public static readonly EnumToolMaterial STONE = new EnumToolMaterial("STONE", InnerEnum.STONE, 1, 131, 4.0F, 1, 5);
		public static readonly EnumToolMaterial IRON = new EnumToolMaterial("IRON", InnerEnum.IRON, 2, 250, 6.0F, 2, 14);
		public static readonly EnumToolMaterial EMERALD = new EnumToolMaterial("EMERALD", InnerEnum.EMERALD, 3, 1561, 8.0F, 3, 10);
		public static readonly EnumToolMaterial GOLD = new EnumToolMaterial("GOLD", InnerEnum.GOLD, 0, 32, 12.0F, 0, 22);

		private static readonly List<EnumToolMaterial> valueList = new List<EnumToolMaterial>();

		static EnumToolMaterial()
		{
			valueList.Add(WOOD);
			valueList.Add(STONE);
			valueList.Add(IRON);
			valueList.Add(EMERALD);
			valueList.Add(GOLD);
		}

		public enum InnerEnum
		{
			WOOD,
			STONE,
			IRON,
			EMERALD,
			GOLD
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private readonly int harvestLevel;
		private readonly int maxUses;
		private readonly float efficiencyOnProperMaterial;
		private readonly int damageVsEntity;
		private readonly int enchantability;

		private EnumToolMaterial(string name, InnerEnum innerEnum, int i3, int i4, float f5, int i6, int i7)
		{
			this.harvestLevel = i3;
			this.maxUses = i4;
			this.efficiencyOnProperMaterial = f5;
			this.damageVsEntity = i6;
			this.enchantability = i7;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public int MaxUses
		{
			get
			{
				return this.maxUses;
			}
		}

		public float EfficiencyOnProperMaterial
		{
			get
			{
				return this.efficiencyOnProperMaterial;
			}
		}

		public int DamageVsEntity
		{
			get
			{
				return this.damageVsEntity;
			}
		}

		public int HarvestLevel
		{
			get
			{
				return this.harvestLevel;
			}
		}

		public int Enchantability
		{
			get
			{
				return this.enchantability;
			}
		}

		public static EnumToolMaterial[] values()
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

		public static EnumToolMaterial valueOf(string name)
		{
			foreach (EnumToolMaterial enumInstance in EnumToolMaterial.valueList)
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