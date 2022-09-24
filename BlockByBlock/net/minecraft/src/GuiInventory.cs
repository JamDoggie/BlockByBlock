using System;

namespace net.minecraft.src
{

	using GL11 = org.lwjgl.opengl.GL11;
	using GL12 = org.lwjgl.opengl.GL12;

	public class GuiInventory : GuiContainer
	{
		private float xSize_lo;
		private float ySize_lo;

		public GuiInventory(EntityPlayer entityPlayer1) : base(entityPlayer1.inventorySlots)
		{
			this.allowUserInput = true;
			entityPlayer1.addStat(AchievementList.openInventory, 1);
		}

		public override void updateScreen()
		{
			if (this.mc.playerController.InCreativeMode)
			{
				this.mc.displayGuiScreen(new GuiContainerCreative(this.mc.thePlayer));
			}

		}

		public override void initGui()
		{
			this.controlList.Clear();
			if (this.mc.playerController.InCreativeMode)
			{
				this.mc.displayGuiScreen(new GuiContainerCreative(this.mc.thePlayer));
			}
			else
			{
				base.initGui();
				if (this.mc.thePlayer.ActivePotionEffects.Count > 0)
				{
					this.guiLeft = 160 + (this.width - this.xSize - 200) / 2;
				}
			}

		}

		protected internal override void drawGuiContainerForegroundLayer()
		{
			this.fontRenderer.drawString(StatCollector.translateToLocal("container.crafting"), 86, 16, 4210752);
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			base.drawScreen(i1, i2, f3);
			this.xSize_lo = (float)i1;
			this.ySize_lo = (float)i2;
		}

		protected internal override void drawGuiContainerBackgroundLayer(float f1, int i2, int i3)
		{
			int i4 = this.mc.renderEngine.getTexture("/gui/inventory.png");
			GL11.glColor4f(1.0F, 1.0F, 1.0F, 1.0F);
			this.mc.renderEngine.bindTexture(i4);
			int i5 = this.guiLeft;
			int i6 = this.guiTop;
			this.drawTexturedModalRect(i5, i6, 0, 0, this.xSize, this.ySize);
			this.displayDebuffEffects();
			GL11.glEnable(GL12.GL_RESCALE_NORMAL);
			GL11.glEnable(GL11.GL_COLOR_MATERIAL);
			GL11.glPushMatrix();
			GL11.glTranslatef((float)(i5 + 51), (float)(i6 + 75), 50.0F);
			float f7 = 30.0F;
			GL11.glScalef(-f7, f7, f7);
			GL11.glRotatef(180.0F, 0.0F, 0.0F, 1.0F);
			float f8 = this.mc.thePlayer.renderYawOffset;
			float f9 = this.mc.thePlayer.rotationYaw;
			float f10 = this.mc.thePlayer.rotationPitch;
			float f11 = (float)(i5 + 51) - this.xSize_lo;
			float f12 = (float)(i6 + 75 - 50) - this.ySize_lo;
			GL11.glRotatef(135.0F, 0.0F, 1.0F, 0.0F);
			RenderHelper.enableStandardItemLighting();
			GL11.glRotatef(-135.0F, 0.0F, 1.0F, 0.0F);
			GL11.glRotatef(-((float)Math.Atan((double)(f12 / 40.0F))) * 20.0F, 1.0F, 0.0F, 0.0F);
			this.mc.thePlayer.renderYawOffset = (float)Math.Atan((double)(f11 / 40.0F)) * 20.0F;
			this.mc.thePlayer.rotationYaw = (float)Math.Atan((double)(f11 / 40.0F)) * 40.0F;
			this.mc.thePlayer.rotationPitch = -((float)Math.Atan((double)(f12 / 40.0F))) * 20.0F;
			this.mc.thePlayer.rotationYawHead = this.mc.thePlayer.rotationYaw;
			GL11.glTranslatef(0.0F, this.mc.thePlayer.yOffset, 0.0F);
			RenderManager.instance.playerViewY = 180.0F;
			RenderManager.instance.renderEntityWithPosYaw(this.mc.thePlayer, 0.0D, 0.0D, 0.0D, 0.0F, 1.0F);
			this.mc.thePlayer.renderYawOffset = f8;
			this.mc.thePlayer.rotationYaw = f9;
			this.mc.thePlayer.rotationPitch = f10;
			GL11.glPopMatrix();
			RenderHelper.disableStandardItemLighting();
			GL11.glDisable(GL12.GL_RESCALE_NORMAL);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.id == 0)
			{
				this.mc.displayGuiScreen(new GuiAchievements(this.mc.statFileWriter));
			}

			if (guiButton1.id == 1)
			{
				this.mc.displayGuiScreen(new GuiStats(this, this.mc.statFileWriter));
			}

		}

		private void displayDebuffEffects()
		{
			int i1 = this.guiLeft - 124;
			int i2 = this.guiTop;
			int i3 = this.mc.renderEngine.getTexture("/gui/inventory.png");
			System.Collections.ICollection collection4 = this.mc.thePlayer.ActivePotionEffects;
			if (collection4.Count > 0)
			{
				int i5 = 33;
				if (collection4.Count > 5)
				{
					i5 = 132 / (collection4.Count - 1);
				}

				for (System.Collections.IEnumerator iterator6 = this.mc.thePlayer.ActivePotionEffects.GetEnumerator(); iterator6.MoveNext(); i2 += i5)
				{
					PotionEffect potionEffect7 = (PotionEffect)iterator6.Current;
					Potion potion8 = Potion.potionTypes[potionEffect7.PotionID];
					GL11.glColor4f(1.0F, 1.0F, 1.0F, 1.0F);
					this.mc.renderEngine.bindTexture(i3);
					this.drawTexturedModalRect(i1, i2, 0, this.ySize, 140, 32);
					if (potion8.hasStatusIcon())
					{
						int i9 = potion8.StatusIconIndex;
						this.drawTexturedModalRect(i1 + 6, i2 + 7, 0 + i9 % 8 * 18, this.ySize + 32 + i9 / 8 * 18, 18, 18);
					}

					string string11 = StatCollector.translateToLocal(potion8.Name);
					if (potionEffect7.Amplifier == 1)
					{
						string11 = string11 + " II";
					}
					else if (potionEffect7.Amplifier == 2)
					{
						string11 = string11 + " III";
					}
					else if (potionEffect7.Amplifier == 3)
					{
						string11 = string11 + " IV";
					}

					this.fontRenderer.drawStringWithShadow(string11, i1 + 10 + 18, i2 + 6, 0xFFFFFF);
					string string10 = Potion.getDurationString(potionEffect7);
					this.fontRenderer.drawStringWithShadow(string10, i1 + 10 + 18, i2 + 6 + 10, 8355711);
				}

			}
		}
	}

}