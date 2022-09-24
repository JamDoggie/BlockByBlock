namespace net.minecraft.src
{
	public class GuiLanguage : GuiScreen
	{
		protected internal GuiScreen parentGui;
		private int updateTimer = -1;
		private GuiSlotLanguage languageList;
		private readonly GameSettings field_44006_d;
		private GuiSmallButton doneButton;

		public GuiLanguage(GuiScreen guiScreen1, GameSettings gameSettings2)
		{
			this.parentGui = guiScreen1;
			this.field_44006_d = gameSettings2;
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Add(this.doneButton = new GuiSmallButton(6, this.width / 2 - 75, this.height - 38, stringTranslate1.translateKey("gui.done")));
			this.languageList = new GuiSlotLanguage(this);
			this.languageList.registerScrollButtons(this.controlList, 7, 8);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				switch (guiButton1.id)
				{
				case 5:
					break;
				case 6:
					this.field_44006_d.saveOptions();
					this.mc.displayGuiScreen(this.parentGui);
					break;
				default:
					this.languageList.actionPerformed(guiButton1);
				break;
				}

			}
		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
		}

		protected internal override void mouseMovedOrUp(int i1, int i2, int i3)
		{
			base.mouseMovedOrUp(i1, i2, i3);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.languageList.drawScreen(i1, i2, f3);
			if (this.updateTimer <= 0)
			{
				this.mc.texturePackList.updateAvaliableTexturePacks();
				this.updateTimer += 20;
			}

			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("options.language"), this.width / 2, 16, 0xFFFFFF);
			this.drawCenteredString(this.fontRenderer, "(" + stringTranslate4.translateKey("options.languageWarning") + ")", this.width / 2, this.height - 56, 8421504);
			base.drawScreen(i1, i2, f3);
		}

		public override void updateScreen()
		{
			base.updateScreen();
			--this.updateTimer;
		}

		internal static GameSettings func_44005_a(GuiLanguage guiLanguage0)
		{
			return guiLanguage0.field_44006_d;
		}

		internal static GuiSmallButton func_46028_b(GuiLanguage guiLanguage0)
		{
			return guiLanguage0.doneButton;
		}
	}

}