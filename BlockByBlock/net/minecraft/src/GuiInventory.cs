using System;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

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
#if DEBUGTOOLS
            controlList.Add(new GuiButton(50, guiLeft + 200, guiTop, "Restore Hunger"));
            controlList.Add(new GuiButton(51, guiLeft + 200, guiTop + 25, "Restore Health"));
            controlList.Add(new GuiButton(52, guiLeft + 200, guiTop + 50, "Creative Mode"));
            controlList.Add(new GuiButton(53, guiLeft + 200, guiTop + 75, "Survival Mode"));
#endif
		}

		protected internal override void drawGuiContainerForegroundLayer()
		{
			this.fontRenderer.drawString(StatCollector.translateToLocal("container.crafting"), 86, 16, 4210752);

#if DEBUGTOOLS
            // Hax
            fontRenderer.drawString("Hax Menu (only for singleplayer use)", 200, -15, 0xFFFFFF);
#endif
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
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.mc.renderEngine.bindTexture(i4);
			int i5 = this.guiLeft;
			int i6 = this.guiTop;
			this.drawTexturedModalRect(i5, i6, 0, 0, this.xSize, this.ySize);
			this.displayDebuffEffects();

			Minecraft.renderPipeline.SetState(RenderState.ColorMaterialState, true);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)(i5 + 51), (float)(i6 + 75), 50.0F);
			float f7 = 30.0F;
            Minecraft.renderPipeline.ModelMatrix.Scale(-f7, f7, f7);
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 0.0F, 1.0F);
			float f8 = this.mc.thePlayer.renderYawOffset;
			float f9 = this.mc.thePlayer.rotationYaw;
			float f10 = this.mc.thePlayer.rotationPitch;
			float f11 = (float)(i5 + 51) - this.xSize_lo;
			float f12 = (float)(i6 + 75 - 50) - this.ySize_lo;
            Minecraft.renderPipeline.ModelMatrix.Rotate(135.0F, 0.0F, 1.0F, 0.0F);
			GameLighting.EnableMeshLighting();
            Minecraft.renderPipeline.ModelMatrix.Rotate(-135.0F, 0.0F, 1.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(-((float)Math.Atan((double)(f12 / 40.0F))) * 20.0F, 1.0F, 0.0F, 0.0F);
			this.mc.thePlayer.renderYawOffset = (float)Math.Atan((double)(f11 / 40.0F)) * 20.0F;
			this.mc.thePlayer.rotationYaw = (float)Math.Atan((double)(f11 / 40.0F)) * 40.0F;
			this.mc.thePlayer.rotationPitch = -((float)Math.Atan((double)(f12 / 40.0F))) * 20.0F;
			this.mc.thePlayer.rotationYawHead = this.mc.thePlayer.rotationYaw;
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, this.mc.thePlayer.yOffset, 0.0F);
			RenderManager.instance.playerViewY = 180.0F;
			RenderManager.instance.renderEntityWithPosYaw(this.mc.thePlayer, 0.0D, 0.0D, 0.0D, 0.0F, 1.0F);
			this.mc.thePlayer.renderYawOffset = f8;
			this.mc.thePlayer.rotationYaw = f9;
			this.mc.thePlayer.rotationPitch = f10;
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			GameLighting.DisableMeshLighting();
            Minecraft.renderPipeline.SetState(RenderState.ColorMaterialState, false);
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

			if (guiButton1.id == 50)
			{
                mc.thePlayer.FoodStats.FoodLevel = 20;
				mc.thePlayer.FoodStats.FoodSaturationLevel = 5;
            }

            if (guiButton1.id == 51)
            {
				mc.thePlayer.Health = mc.thePlayer.MaxHealth;
            }

            if (guiButton1.id == 52)
            {
                mc.playerController = new PlayerControllerCreative(mc);
				mc.thePlayer.addChatMessage("Updated gamemode to creative.");
				mc.thePlayer.capabilities.allowFlying = true;
				mc.thePlayer.capabilities.isCreativeMode = true;
				mc.thePlayer.capabilities.disableDamage = true;
            }

            if (guiButton1.id == 53)
            {
                mc.playerController = new PlayerControllerSP(mc);
                mc.thePlayer.addChatMessage("Updated gamemode to survival.");
                mc.thePlayer.capabilities.allowFlying = false;
                mc.thePlayer.capabilities.isCreativeMode = false;
                mc.thePlayer.capabilities.disableDamage = false;
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
					Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
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