using System;
using System.Globalization;

namespace net.minecraft.src
{

	public class StatBase
	{
		public readonly int statId;
		private readonly string statName;
		public bool isIndependent;
		public string statGuid;
		private readonly IStatType type;
		private static CultureInfo numberFormat = new CultureInfo("en-US");
		public static IStatType simpleStatType = new StatTypeSimple();
		private static string decimalFormat = "########0.00";
		public static IStatType timeStatType = new StatTypeTime();
		public static IStatType distanceStatType = new StatTypeDistance();

		public StatBase(int i1, string string2, IStatType iStatType3)
		{
			this.isIndependent = false;
			this.statId = i1;
			this.statName = string2;
			this.type = iStatType3;
		}

		public StatBase(int i1, string string2) : this(i1, string2, simpleStatType)
		{
		}

		public virtual StatBase initIndependentStat()
		{
			this.isIndependent = true;
			return this;
		}

		public virtual StatBase registerStat()
		{
			if (StatList.oneShotStats.Contains(this.statId))
			{
				throw new Exception("Duplicate stat id: \"" + ((StatBase)StatList.oneShotStats[this.statId]).statName + "\" and \"" + this.statName + "\" at id " + this.statId);
			}
			else
			{
				StatList.allStats.Add(this);
				StatList.oneShotStats[this.statId] = this;
				this.statGuid = AchievementMap.getGuid(this.statId);
				return this;
			}
		}

		public virtual bool IsAchievement
		{
			get
			{
				return false;
			}
		}

		public virtual string func_27084_a(int i1)
		{
			return this.type.format(i1);
		}

		public virtual string Name
		{
			get
			{
				return this.statName;
			}
		}

		public override string ToString()
		{
			return StatCollector.translateToLocal(this.statName);
		}

		internal static CultureInfo NumberFormat
		{
			get
			{
				return numberFormat;
			}
		}

		internal static string DecimalFormat
		{
			get
			{
				return decimalFormat;
			}
		}
	}

}