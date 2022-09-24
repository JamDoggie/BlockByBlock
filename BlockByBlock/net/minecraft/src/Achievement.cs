namespace net.minecraft.src
{
	public class Achievement : StatBase
	{
		public readonly int displayColumn;
		public readonly int displayRow;
		public readonly Achievement parentAchievement;
		private readonly string achievementDescription;
		private IStatStringFormat statStringFormatter;
		public readonly ItemStack theItemStack;
		private bool isSpecial;

		public Achievement(int i1, string string2, int i3, int i4, Item item5, Achievement? achievement6) : this(i1, string2, i3, i4, new ItemStack(item5), achievement6)
		{
		}

		public Achievement(int i1, string string2, int i3, int i4, Block block5, Achievement? achievement6) : this(i1, string2, i3, i4, new ItemStack(block5), achievement6)
		{
		}

		public Achievement(int i1, string string2, int i3, int i4, ItemStack itemStack5, Achievement? achievement6) : base(5242880 + i1, "achievement." + string2)
		{
			this.theItemStack = itemStack5;
			this.achievementDescription = "achievement." + string2 + ".desc";
			this.displayColumn = i3;
			this.displayRow = i4;
			if (i3 < AchievementList.minDisplayColumn)
			{
				AchievementList.minDisplayColumn = i3;
			}

			if (i4 < AchievementList.minDisplayRow)
			{
				AchievementList.minDisplayRow = i4;
			}

			if (i3 > AchievementList.maxDisplayColumn)
			{
				AchievementList.maxDisplayColumn = i3;
			}

			if (i4 > AchievementList.maxDisplayRow)
			{
				AchievementList.maxDisplayRow = i4;
			}

			this.parentAchievement = achievement6;
		}

		public virtual Achievement setIndependent()
		{
			this.isIndependent = true;
			return this;
		}

		public virtual Achievement setSpecial()
		{
			this.isSpecial = true;
			return this;
		}

		public virtual Achievement registerAchievement()
		{
			base.registerStat();
			AchievementList.achievementList.Add(this);
			return this;
		}

		public override bool IsAchievement
		{
			get
			{
				return true;
			}
		}

		public virtual string Description
		{
			get
			{
				return this.statStringFormatter != null ? this.statStringFormatter.formatString(StatCollector.translateToLocal(this.achievementDescription)) : StatCollector.translateToLocal(this.achievementDescription);
			}
		}

		public virtual Achievement setStatStringFormatter(IStatStringFormat iStatStringFormat1)
		{
			this.statStringFormatter = iStatStringFormat1;
			return this;
		}

		public virtual bool Special
		{
			get
			{
				return this.isSpecial;
			}
		}

		public override StatBase registerStat()
		{
			return this.registerAchievement();
		}

		public override StatBase initIndependentStat()
		{
			return this.setIndependent();
		}
	}

}