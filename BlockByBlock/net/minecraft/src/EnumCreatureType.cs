using System;
using System.Collections.Generic;
using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public sealed class EnumCreatureType
	{
		public static readonly EnumCreatureType monster = new EnumCreatureType("monster", InnerEnum.monster, typeof(IMob), 70, Material.air, false);
		public static readonly EnumCreatureType creature = new EnumCreatureType("creature", InnerEnum.creature, typeof(EntityAnimal), 15, Material.air, true);
		public static readonly EnumCreatureType waterCreature = new EnumCreatureType("waterCreature", InnerEnum.waterCreature, typeof(EntityWaterMob), 5, Material.water, true);

		private static readonly List<EnumCreatureType> valueList = new List<EnumCreatureType>();

		static EnumCreatureType()
		{
			valueList.Add(monster);
			valueList.Add(creature);
			valueList.Add(waterCreature);
		}

		public enum InnerEnum
		{
			monster,
			creature,
			waterCreature
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		private readonly Type creatureClass;
		private readonly int maxNumberOfCreature;
		private readonly Material creatureMaterial;
		private readonly bool isPeacefulCreature;

		private EnumCreatureType(string name, InnerEnum innerEnum, Type class3, int i4, Material material5, bool z6)
		{
			this.creatureClass = class3;
			this.maxNumberOfCreature = i4;
			this.creatureMaterial = material5;
			this.isPeacefulCreature = z6;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public Type CreatureClass
		{
			get
			{
				return this.creatureClass;
			}
		}

		public int MaxNumberOfCreature
		{
			get
			{
				return this.maxNumberOfCreature;
			}
		}

		public Material CreatureMaterial
		{
			get
			{
				return this.creatureMaterial;
			}
		}

		public bool PeacefulCreature
		{
			get
			{
				return this.isPeacefulCreature;
			}
		}

		public static EnumCreatureType[] values()
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

		public static EnumCreatureType valueOf(string name)
		{
			foreach (EnumCreatureType enumInstance in EnumCreatureType.valueList)
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