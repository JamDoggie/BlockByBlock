namespace net.minecraft.src
{
    using net.minecraft.client.entity.render;
    using OpenTK.Graphics.OpenGL;
    using Minecraft = net.minecraft.client.Minecraft;
    public class GuiStats : GuiScreen
	{
		private static RenderItem renderItem = new RenderItem();
		protected internal GuiScreen parentGui;
		protected internal string statsTitle = "Select world";
		private GuiSlotStatsGeneral slotGeneral;
		private GuiSlotStatsItem slotItem;
		private GuiSlotStatsBlock slotBlock;
		private StatFileWriter statFileWriter;
		private GuiSlot selectedSlot = null;

		public GuiStats(GuiScreen guiScreen1, StatFileWriter statFileWriter2)
		{
			this.parentGui = guiScreen1;
			this.statFileWriter = statFileWriter2;
		}

		public override void initGui()
		{
			this.statsTitle = StatCollector.translateToLocal("gui.stats");
			this.slotGeneral = new GuiSlotStatsGeneral(this);
			this.slotGeneral.registerScrollButtons(this.controlList, 1, 1);
			this.slotItem = new GuiSlotStatsItem(this);
			this.slotItem.registerScrollButtons(this.controlList, 1, 1);
			this.slotBlock = new GuiSlotStatsBlock(this);
			this.slotBlock.registerScrollButtons(this.controlList, 1, 1);
			this.selectedSlot = this.slotGeneral;
			this.addHeaderButtons();
		}

		public virtual void addHeaderButtons()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Add(new GuiButton(0, this.width / 2 + 4, this.height - 28, 150, 20, stringTranslate1.translateKey("gui.done")));
			this.controlList.Add(new GuiButton(1, this.width / 2 - 154, this.height - 52, 100, 20, stringTranslate1.translateKey("stat.generalButton")));
			GuiButton guiButton2;
			this.controlList.Add(guiButton2 = new GuiButton(2, this.width / 2 - 46, this.height - 52, 100, 20, stringTranslate1.translateKey("stat.blocksButton")));
			GuiButton guiButton3;
			this.controlList.Add(guiButton3 = new GuiButton(3, this.width / 2 + 62, this.height - 52, 100, 20, stringTranslate1.translateKey("stat.itemsButton")));
			if (this.slotBlock.Size == 0)
			{
				guiButton2.enabled = false;
			}

			if (this.slotItem.Size == 0)
			{
				guiButton3.enabled = false;
			}

		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 0)
				{
					this.mc.displayGuiScreen(this.parentGui);
				}
				else if (guiButton1.id == 1)
				{
					this.selectedSlot = this.slotGeneral;
				}
				else if (guiButton1.id == 3)
				{
					this.selectedSlot = this.slotItem;
				}
				else if (guiButton1.id == 2)
				{
					this.selectedSlot = this.slotBlock;
				}
				else
				{
					this.selectedSlot.actionPerformed(guiButton1);
				}

			}
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.selectedSlot.drawScreen(i1, i2, f3);
			this.drawCenteredString(this.fontRenderer, this.statsTitle, this.width / 2, 20, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}

		private void drawItemSprite(int i1, int i2, int i3)
		{
			this.drawButtonBackground(i1 + 1, i2 + 1);
            
			GameLighting.EnableGUIStandardItemLighting();
			renderItem.drawItemIntoGui(this.fontRenderer, this.mc.renderEngine, i3, 0, Item.itemsList[i3].getIconFromDamage(0), i1 + 2, i2 + 2);
			GameLighting.DisableMeshLighting();
		}

		private void drawButtonBackground(int i1, int i2)
		{
			this.drawSprite(i1, i2, 0, 0);
		}

		private void drawSprite(int i1, int i2, int i3, int i4)
		{
			int i5 = this.mc.renderEngine.getTexture("/gui/slot.png");
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.mc.renderEngine.bindTexture(i5);
			Tessellator tessellator10 = Tessellator.instance;
			tessellator10.startDrawingQuads();
			tessellator10.AddVertexWithUV((double)(i1 + 0), (double)(i2 + 18), (double)this.zLevel, (double)((float)(i3 + 0) * 0.0078125F), (double)((float)(i4 + 18) * 0.0078125F));
			tessellator10.AddVertexWithUV((double)(i1 + 18), (double)(i2 + 18), (double)this.zLevel, (double)((float)(i3 + 18) * 0.0078125F), (double)((float)(i4 + 18) * 0.0078125F));
			tessellator10.AddVertexWithUV((double)(i1 + 18), (double)(i2 + 0), (double)this.zLevel, (double)((float)(i3 + 18) * 0.0078125F), (double)((float)(i4 + 0) * 0.0078125F));
			tessellator10.AddVertexWithUV((double)(i1 + 0), (double)(i2 + 0), (double)this.zLevel, (double)((float)(i3 + 0) * 0.0078125F), (double)((float)(i4 + 0) * 0.0078125F));
			tessellator10.DrawImmediate();
		}

		internal static Minecraft getMinecraft(GuiStats guiStats0)
		{
			return guiStats0.mc;
		}

		internal static FontRenderer getFontRenderer1(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static StatFileWriter getStatsFileWriter(GuiStats guiStats0)
		{
			return guiStats0.statFileWriter;
		}

		internal static FontRenderer getFontRenderer2(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static FontRenderer getFontRenderer3(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static Minecraft getMinecraft1(GuiStats guiStats0)
		{
			return guiStats0.mc;
		}

		internal static void drawSprite(GuiStats guiStats0, int i1, int i2, int i3, int i4)
		{
			guiStats0.drawSprite(i1, i2, i3, i4);
		}

		internal static Minecraft getMinecraft2(GuiStats guiStats0)
		{
			return guiStats0.mc;
		}

		internal static FontRenderer getFontRenderer4(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static FontRenderer getFontRenderer5(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static FontRenderer getFontRenderer6(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static FontRenderer getFontRenderer7(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static FontRenderer getFontRenderer8(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static void drawGradientRect(GuiStats guiStats0, int i1, int i2, int i3, int i4, int i5, int i6)
		{
			guiStats0.drawGradientRect(i1, i2, i3, i4, i5, i6);
		}

		internal static FontRenderer getFontRenderer9(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static FontRenderer getFontRenderer10(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static void drawGradientRect1(GuiStats guiStats0, int i1, int i2, int i3, int i4, int i5, int i6)
		{
			guiStats0.drawGradientRect(i1, i2, i3, i4, i5, i6);
		}

		internal static FontRenderer getFontRenderer11(GuiStats guiStats0)
		{
			return guiStats0.fontRenderer;
		}

		internal static void drawItemSprite(GuiStats guiStats0, int i1, int i2, int i3)
		{
			guiStats0.drawItemSprite(i1, i2, i3);
		}
	}

}