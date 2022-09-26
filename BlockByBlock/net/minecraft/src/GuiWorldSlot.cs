using System;

namespace net.minecraft.src
{

	internal class GuiWorldSlot : GuiSlot
	{
		internal readonly GuiSelectWorld parentWorldGui;

		public GuiWorldSlot(GuiSelectWorld guiSelectWorld1) : base(guiSelectWorld1.mc, guiSelectWorld1.width, guiSelectWorld1.height, 32, guiSelectWorld1.height - 64, 36)
		{
			this.parentWorldGui = guiSelectWorld1;
		}

		protected internal override int Size
		{
			get
			{
				return GuiSelectWorld.getSize(this.parentWorldGui).Count;
			}
		}

		protected internal override void elementClicked(int i1, bool z2)
		{
			GuiSelectWorld.onElementSelected(this.parentWorldGui, i1);
			bool z3 = GuiSelectWorld.getSelectedWorld(this.parentWorldGui) >= 0 && GuiSelectWorld.getSelectedWorld(this.parentWorldGui) < this.Size;
			GuiSelectWorld.getSelectButton(this.parentWorldGui).enabled = z3;
			GuiSelectWorld.getRenameButton(this.parentWorldGui).enabled = z3;
			GuiSelectWorld.getDeleteButton(this.parentWorldGui).enabled = z3;
			if (z2 && z3)
			{
				this.parentWorldGui.selectWorld(i1);
			}

		}

		protected internal override bool isSelected(int i1)
		{
			return i1 == GuiSelectWorld.getSelectedWorld(this.parentWorldGui);
		}

		protected internal override int ContentHeight
		{
			get
			{
				return GuiSelectWorld.getSize(this.parentWorldGui).Count * 36;
			}
		}

		protected internal override void drawBackground()
		{
			this.parentWorldGui.drawDefaultBackground();
		}

		protected internal override void drawSlot(int i1, int i2, int i3, int i4, Tessellator tessellator5)
		{
			SaveFormatComparator saveFormatComparator6 = (SaveFormatComparator)GuiSelectWorld.getSize(this.parentWorldGui)[i1];
			string string7 = saveFormatComparator6.DisplayName;
			if (string.ReferenceEquals(string7, null) || MathHelper.stringNullOrLengthZero(string7))
			{
				string7 = GuiSelectWorld.getLocalizedWorldName(this.parentWorldGui) + " " + (i1 + 1);
			}

			string string8 = saveFormatComparator6.FileName;
			string8 = string8 + " (" + new DateTime(saveFormatComparator6.LastTimePlayed).ToString(GuiSelectWorld.getDateFormat(parentWorldGui));
			string8 = string8 + ")";
			string string9 = "";
			if (saveFormatComparator6.getRequiresConversion())
			{
				string9 = GuiSelectWorld.getLocalizedMustConvert(this.parentWorldGui) + " " + string9;
			}
			else
			{
				string9 = GuiSelectWorld.getLocalizedGameMode(this.parentWorldGui)[saveFormatComparator6.GameType];
				if (saveFormatComparator6.HardcoreModeEnabled)
				{
					string9 = "\u00a74" + StatCollector.translateToLocal("gameMode.hardcore") + "\u00a78";
				}
			}

			this.parentWorldGui.drawString(this.parentWorldGui.fontRenderer, string7, i2 + 2, i3 + 1, 0xFFFFFF);
			this.parentWorldGui.drawString(this.parentWorldGui.fontRenderer, string8, i2 + 2, i3 + 12, 8421504);
			this.parentWorldGui.drawString(this.parentWorldGui.fontRenderer, string9, i2 + 2, i3 + 12 + 10, 8421504);
		}
	}

}