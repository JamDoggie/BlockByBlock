using System.Collections;

namespace net.minecraft.src
{

	internal class GuiSlotStatsItem : GuiSlotStats
	{
		internal readonly GuiStats field_27275_a;

		public GuiSlotStatsItem(GuiStats guiStats1) : base(guiStats1)
		{
			this.field_27275_a = guiStats1;
			this.field_27273_c = new();
			System.Collections.IEnumerator iterator2 = StatList.itemStats.GetEnumerator();

			while (iterator2.MoveNext())
			{
				StatCrafting statCrafting3 = (StatCrafting)iterator2.Current;
				bool z4 = false;
				int i5 = statCrafting3.ItemID;
				if (GuiStats.getStatsFileWriter(guiStats1).writeStat(statCrafting3) > 0)
				{
					z4 = true;
				}
				else if (StatList.objectBreakStats[i5] != null && GuiStats.getStatsFileWriter(guiStats1).writeStat(StatList.objectBreakStats[i5]) > 0)
				{
					z4 = true;
				}
				else if (StatList.objectCraftStats[i5] != null && GuiStats.getStatsFileWriter(guiStats1).writeStat(StatList.objectCraftStats[i5]) > 0)
				{
					z4 = true;
				}

				if (z4)
				{
					this.field_27273_c.Add(statCrafting3);
				}
			}

			this.field_27272_d = new SorterStatsItem(this, guiStats1);
		}

		protected internal override void func_27260_a(int i1, int i2, Tessellator tessellator3)
		{
			base.func_27260_a(i1, i2, tessellator3);
			if (this.field_27268_b == 0)
			{
				GuiStats.drawSprite(this.field_27275_a, i1 + 115 - 18 + 1, i2 + 1 + 1, 72, 18);
			}
			else
			{
				GuiStats.drawSprite(this.field_27275_a, i1 + 115 - 18, i2 + 1, 72, 18);
			}

			if (this.field_27268_b == 1)
			{
				GuiStats.drawSprite(this.field_27275_a, i1 + 165 - 18 + 1, i2 + 1 + 1, 18, 18);
			}
			else
			{
				GuiStats.drawSprite(this.field_27275_a, i1 + 165 - 18, i2 + 1, 18, 18);
			}

			if (this.field_27268_b == 2)
			{
				GuiStats.drawSprite(this.field_27275_a, i1 + 215 - 18 + 1, i2 + 1 + 1, 36, 18);
			}
			else
			{
				GuiStats.drawSprite(this.field_27275_a, i1 + 215 - 18, i2 + 1, 36, 18);
			}

		}

		protected internal override void drawSlot(int i1, int i2, int i3, int i4, Tessellator tessellator5)
		{
			StatCrafting statCrafting6 = this.func_27264_b(i1);
			int i7 = statCrafting6.ItemID;
			GuiStats.drawItemSprite(this.field_27275_a, i2 + 40, i3, i7);
			this.func_27265_a((StatCrafting)StatList.objectBreakStats[i7], i2 + 115, i3, i1 % 2 == 0);
			this.func_27265_a((StatCrafting)StatList.objectCraftStats[i7], i2 + 165, i3, i1 % 2 == 0);
			this.func_27265_a(statCrafting6, i2 + 215, i3, i1 % 2 == 0);
		}

		protected internal override string func_27263_a(int i1)
		{
			return i1 == 1 ? "stat.crafted" : (i1 == 2 ? "stat.used" : "stat.depleted");
		}
	}

}