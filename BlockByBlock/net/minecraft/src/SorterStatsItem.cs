namespace net.minecraft.src
{

	internal class SorterStatsItem : IComparer<StatCrafting>
	{
		internal readonly GuiStats statsGUI;
		internal readonly GuiSlotStatsItem slotStatsItemGUI;

		internal SorterStatsItem(GuiSlotStatsItem guiSlotStatsItem1, GuiStats guiStats2)
		{
			this.slotStatsItemGUI = guiSlotStatsItem1;
			this.statsGUI = guiStats2;
		}

		public virtual int func_27371_a(StatCrafting statCrafting1, StatCrafting statCrafting2)
		{
			int i3 = statCrafting1.ItemID;
			int i4 = statCrafting2.ItemID;
			StatBase statBase5 = null;
			StatBase statBase6 = null;
			if (this.slotStatsItemGUI.field_27271_e == 0)
			{
				statBase5 = StatList.objectBreakStats[i3];
				statBase6 = StatList.objectBreakStats[i4];
			}
			else if (this.slotStatsItemGUI.field_27271_e == 1)
			{
				statBase5 = StatList.objectCraftStats[i3];
				statBase6 = StatList.objectCraftStats[i4];
			}
			else if (this.slotStatsItemGUI.field_27271_e == 2)
			{
				statBase5 = StatList.objectUseStats[i3];
				statBase6 = StatList.objectUseStats[i4];
			}

			if (statBase5 != null || statBase6 != null)
			{
				if (statBase5 == null)
				{
					return 1;
				}

				if (statBase6 == null)
				{
					return -1;
				}

				int i7 = GuiStats.getStatsFileWriter(this.slotStatsItemGUI.field_27275_a).writeStat(statBase5);
				int i8 = GuiStats.getStatsFileWriter(this.slotStatsItemGUI.field_27275_a).writeStat(statBase6);
				if (i7 != i8)
				{
					return (i7 - i8) * this.slotStatsItemGUI.field_27270_f;
				}
			}

			return i3 - i4;
		}

		public virtual int Compare(StatCrafting object1, StatCrafting object2)
		{
			return this.func_27371_a(object1, object2);
		}
	}

}