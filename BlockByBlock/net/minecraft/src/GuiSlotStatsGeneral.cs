namespace net.minecraft.src
{
	internal class GuiSlotStatsGeneral : GuiSlot
	{
		internal readonly GuiStats field_27276_a;

		public GuiSlotStatsGeneral(GuiStats guiStats1) : base(GuiStats.getMinecraft(guiStats1), guiStats1.width, guiStats1.height, 32, guiStats1.height - 64, 10)
		{
			this.field_27276_a = guiStats1;
			this.func_27258_a(false);
		}

		protected internal override int Size
		{
			get
			{
				return StatList.generalStats.Count;
			}
		}

		protected internal override void elementClicked(int i1, bool z2)
		{
		}

		protected internal override bool isSelected(int i1)
		{
			return false;
		}

		protected internal override int ContentHeight
		{
			get
			{
				return this.Size * 10;
			}
		}

		protected internal override void drawBackground()
		{
			this.field_27276_a.drawDefaultBackground();
		}

		protected internal override void drawSlot(int i1, int i2, int i3, int i4, Tessellator tessellator5)
		{
			StatBase statBase6 = (StatBase)StatList.generalStats[i1];
			this.field_27276_a.drawString(GuiStats.getFontRenderer1(this.field_27276_a), StatCollector.translateToLocal(statBase6.Name), i2 + 2, i3 + 1, i1 % 2 == 0 ? 0xFFFFFF : 9474192);
			string string7 = statBase6.func_27084_a(GuiStats.getStatsFileWriter(this.field_27276_a).writeStat(statBase6));
			this.field_27276_a.drawString(GuiStats.getFontRenderer2(this.field_27276_a), string7, i2 + 2 + 213 - GuiStats.getFontRenderer3(this.field_27276_a).getStringWidth(string7), i3 + 1, i1 % 2 == 0 ? 0xFFFFFF : 9474192);
		}
	}

}