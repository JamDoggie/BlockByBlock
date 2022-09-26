using System.Collections.Generic;

namespace net.minecraft.src
{
	public sealed class EnumArt
	{
		public static readonly EnumArt Kebab = new EnumArt("Kebab", InnerEnum.Kebab, "Kebab", 16, 16, 0, 0);
		public static readonly EnumArt Aztec = new EnumArt("Aztec", InnerEnum.Aztec, "Aztec", 16, 16, 16, 0);
		public static readonly EnumArt Alban = new EnumArt("Alban", InnerEnum.Alban, "Alban", 16, 16, 32, 0);
		public static readonly EnumArt Aztec2 = new EnumArt("Aztec2", InnerEnum.Aztec2, "Aztec2", 16, 16, 48, 0);
		public static readonly EnumArt Bomb = new EnumArt("Bomb", InnerEnum.Bomb, "Bomb", 16, 16, 64, 0);
		public static readonly EnumArt Plant = new EnumArt("Plant", InnerEnum.Plant, "Plant", 16, 16, 80, 0);
		public static readonly EnumArt Wasteland = new EnumArt("Wasteland", InnerEnum.Wasteland, "Wasteland", 16, 16, 96, 0);
		public static readonly EnumArt Pool = new EnumArt("Pool", InnerEnum.Pool, "Pool", 32, 16, 0, 32);
		public static readonly EnumArt Courbet = new EnumArt("Courbet", InnerEnum.Courbet, "Courbet", 32, 16, 32, 32);
		public static readonly EnumArt Sea = new EnumArt("Sea", InnerEnum.Sea, "Sea", 32, 16, 64, 32);
		public static readonly EnumArt Sunset = new EnumArt("Sunset", InnerEnum.Sunset, "Sunset", 32, 16, 96, 32);
		public static readonly EnumArt Creebet = new EnumArt("Creebet", InnerEnum.Creebet, "Creebet", 32, 16, 128, 32);
		public static readonly EnumArt Wanderer = new EnumArt("Wanderer", InnerEnum.Wanderer, "Wanderer", 16, 32, 0, 64);
		public static readonly EnumArt Graham = new EnumArt("Graham", InnerEnum.Graham, "Graham", 16, 32, 16, 64);
		public static readonly EnumArt Match = new EnumArt("Match", InnerEnum.Match, "Match", 32, 32, 0, 128);
		public static readonly EnumArt Bust = new EnumArt("Bust", InnerEnum.Bust, "Bust", 32, 32, 32, 128);
		public static readonly EnumArt Stage = new EnumArt("Stage", InnerEnum.Stage, "Stage", 32, 32, 64, 128);
		public static readonly EnumArt Void = new EnumArt("Void", InnerEnum.Void, "Void", 32, 32, 96, 128);
		public static readonly EnumArt SkullAndRoses = new EnumArt("SkullAndRoses", InnerEnum.SkullAndRoses, "SkullAndRoses", 32, 32, 128, 128);
		public static readonly EnumArt Fighters = new EnumArt("Fighters", InnerEnum.Fighters, "Fighters", 64, 32, 0, 96);
		public static readonly EnumArt Pointer = new EnumArt("Pointer", InnerEnum.Pointer, "Pointer", 64, 64, 0, 192);
		public static readonly EnumArt Pigscene = new EnumArt("Pigscene", InnerEnum.Pigscene, "Pigscene", 64, 64, 64, 192);
		public static readonly EnumArt BurningSkull = new EnumArt("BurningSkull", InnerEnum.BurningSkull, "BurningSkull", 64, 64, 128, 192);
		public static readonly EnumArt Skeleton = new EnumArt("Skeleton", InnerEnum.Skeleton, "Skeleton", 64, 48, 192, 64);
		public static readonly EnumArt DonkeyKong = new EnumArt("DonkeyKong", InnerEnum.DonkeyKong, "DonkeyKong", 64, 48, 192, 112);

		private static readonly List<EnumArt> valueList = new List<EnumArt>();

		static EnumArt()
		{
			valueList.Add(Kebab);
			valueList.Add(Aztec);
			valueList.Add(Alban);
			valueList.Add(Aztec2);
			valueList.Add(Bomb);
			valueList.Add(Plant);
			valueList.Add(Wasteland);
			valueList.Add(Pool);
			valueList.Add(Courbet);
			valueList.Add(Sea);
			valueList.Add(Sunset);
			valueList.Add(Creebet);
			valueList.Add(Wanderer);
			valueList.Add(Graham);
			valueList.Add(Match);
			valueList.Add(Bust);
			valueList.Add(Stage);
			valueList.Add(Void);
			valueList.Add(SkullAndRoses);
			valueList.Add(Fighters);
			valueList.Add(Pointer);
			valueList.Add(Pigscene);
			valueList.Add(BurningSkull);
			valueList.Add(Skeleton);
			valueList.Add(DonkeyKong);
		}

		public enum InnerEnum
		{
			Kebab,
			Aztec,
			Alban,
			Aztec2,
			Bomb,
			Plant,
			Wasteland,
			Pool,
			Courbet,
			Sea,
			Sunset,
			Creebet,
			Wanderer,
			Graham,
			Match,
			Bust,
			Stage,
			Void,
			SkullAndRoses,
			Fighters,
			Pointer,
			Pigscene,
			BurningSkull,
			Skeleton,
			DonkeyKong
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		public static readonly int maxArtTitleLength = "SkullAndRoses".Length;
		public readonly string title;
		public readonly int sizeX;
		public readonly int sizeY;
		public readonly int offsetX;
		public readonly int offsetY;

		private EnumArt(string name, InnerEnum innerEnum, string string3, int i4, int i5, int i6, int i7)
		{
			this.title = string3;
			this.sizeX = i4;
			this.sizeY = i5;
			this.offsetX = i6;
			this.offsetY = i7;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public static EnumArt[] values()
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

		public static EnumArt valueOf(string name)
		{
			foreach (EnumArt enumInstance in EnumArt.valueList)
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