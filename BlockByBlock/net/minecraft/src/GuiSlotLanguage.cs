using System.Collections;
using System.Collections.Generic;

namespace net.minecraft.src
{

	internal class GuiSlotLanguage : GuiSlot
	{
		private ArrayList field_44013_b;
		private Dictionary<string,string> languageList;
		internal readonly GuiLanguage field_44015_a;

		public GuiSlotLanguage(GuiLanguage guiLanguage1) : base(guiLanguage1.mc, guiLanguage1.width, guiLanguage1.height, 32, guiLanguage1.height - 65 + 4, 18)
		{
			this.field_44015_a = guiLanguage1;
			this.languageList = StringTranslate.Instance.LanguageList;
			this.field_44013_b = new ArrayList();
			System.Collections.IEnumerator iterator2 = this.languageList.Keys.GetEnumerator();

			while (iterator2.MoveNext())
			{
				string string3 = (string)iterator2.Current;
				this.field_44013_b.Add(string3);
			}

		}

		protected internal override int Size
		{
			get
			{
				return this.field_44013_b.Count;
			}
		}

		protected internal override void elementClicked(int i1, bool z2)
		{
			StringTranslate.Instance.Language = (string)this.field_44013_b[i1];
			this.field_44015_a.mc.fontRenderer.UnicodeFlag = StringTranslate.Instance.Unicode;
			GuiLanguage.func_44005_a(this.field_44015_a).language = (string)this.field_44013_b[i1];
			this.field_44015_a.fontRenderer.BidiFlag = StringTranslate.isBidrectional(GuiLanguage.func_44005_a(this.field_44015_a).language);
			GuiLanguage.func_46028_b(this.field_44015_a).displayString = StringTranslate.Instance.translateKey("gui.done");
		}

		protected internal override bool isSelected(int i1)
		{
			return ((string)this.field_44013_b[i1]).Equals(StringTranslate.Instance.CurrentLanguage);
		}

		protected internal override int ContentHeight
		{
			get
			{
				return this.Size * 18;
			}
		}

		protected internal override void drawBackground()
		{
			this.field_44015_a.drawDefaultBackground();
		}

		protected internal override void drawSlot(int i1, int i2, int i3, int i4, Tessellator tessellator5)
		{
			this.field_44015_a.fontRenderer.BidiFlag = true;
			this.field_44015_a.drawCenteredString(this.field_44015_a.fontRenderer, this.languageList[this.field_44013_b[i1] as string], this.field_44015_a.width / 2, i3 + 1, 0xFFFFFF);
			this.field_44015_a.fontRenderer.BidiFlag = StringTranslate.isBidrectional(GuiLanguage.func_44005_a(this.field_44015_a).language);
		}
	}

}