using System;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity.render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace net.minecraft.src
{
    public abstract class GuiContainer : GuiScreen
	{
		protected internal static RenderItem itemRenderer = new RenderItem();
		protected internal int xSize = 176;
		protected internal int ySize = 166;
		public Container inventorySlots;
		protected internal int guiLeft;
		protected internal int guiTop;

		public GuiContainer(Container container1)
		{
			this.inventorySlots = container1;
		}

		public override void initGui()
		{
			base.initGui();
			this.mc.thePlayer.craftingInventory = this.inventorySlots;
			this.guiLeft = (this.width - this.xSize) / 2;
			this.guiTop = (this.height - this.ySize) / 2;
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawDefaultBackground();
			int i4 = this.guiLeft;
			int i5 = this.guiTop;
			this.drawGuiContainerBackgroundLayer(f3, i1, i2);
			GameLighting.EnableGUIStandardItemLighting();
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)i4, (float)i5, 0.0F);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            
			Slot slot6 = null;
			short s7 = 240;
			short s8 = 240;
			LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, (float)s7 / 1.0F, (float)s8 / 1.0F);
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);

			int i10;
			for (int i20 = 0; i20 < this.inventorySlots.inventorySlots.Count; ++i20)
			{
				Slot slot22 = (Slot)this.inventorySlots.inventorySlots[i20];
				this.drawSlotInventory(slot22);
				if (this.isMouseOverSlot(slot22, i1, i2))
				{
					slot6 = slot22;
					Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
					GL.Disable(EnableCap.DepthTest);
					int i9 = slot22.xDisplayPosition;
					i10 = slot22.yDisplayPosition;
					this.drawGradientRect(i9, i10, i9 + 16, i10 + 16, -2130706433, -2130706433);
					Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
					GL.Enable(EnableCap.DepthTest);
				}
			}

			this.drawGuiContainerForegroundLayer();
			InventoryPlayer inventoryPlayer21 = this.mc.thePlayer.inventory;
			if (inventoryPlayer21.ItemStack != null)
			{
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, 32.0F);
				this.zLevel = 200.0F;
				itemRenderer.zLevel = 200.0F;
				itemRenderer.renderItemIntoGUI(this.fontRenderer, this.mc.renderEngine, inventoryPlayer21.ItemStack, i1 - i4 - 8, i2 - i5 - 8);
				itemRenderer.renderItemOverlayIntoGUI(this.fontRenderer, this.mc.renderEngine, inventoryPlayer21.ItemStack, i1 - i4 - 8, i2 - i5 - 8);
				this.zLevel = 0.0F;
				itemRenderer.zLevel = 0.0F;
			}

            
			GameLighting.DisableMeshLighting();
			Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
			GL.Disable(EnableCap.DepthTest);
			if (inventoryPlayer21.ItemStack == null && slot6 != null && slot6.HasStack)
			{
				ItemStack itemStack23 = slot6.Stack;
				System.Collections.IList list24 = itemStack23.ItemNameandInformation;
				if (list24.Count > 0)
				{
					i10 = 0;

					int i11;
					int i12;
					for (i11 = 0; i11 < list24.Count; ++i11)
					{
						i12 = this.fontRenderer.getStringWidth((string)list24[i11]);
						if (i12 > i10)
						{
							i10 = i12;
						}
					}

					i11 = i1 - i4 + 12;
					i12 = i2 - i5 - 12;
					int i14 = 8;
					if (list24.Count > 1)
					{
						i14 += 2 + (list24.Count - 1) * 10;
					}

					this.zLevel = 300.0F;
					itemRenderer.zLevel = 300.0F;
					int i15 = -267386864;
					this.drawGradientRect(i11 - 3, i12 - 4, i11 + i10 + 3, i12 - 3, i15, i15);
					this.drawGradientRect(i11 - 3, i12 + i14 + 3, i11 + i10 + 3, i12 + i14 + 4, i15, i15);
					this.drawGradientRect(i11 - 3, i12 - 3, i11 + i10 + 3, i12 + i14 + 3, i15, i15);
					this.drawGradientRect(i11 - 4, i12 - 3, i11 - 3, i12 + i14 + 3, i15, i15);
					this.drawGradientRect(i11 + i10 + 3, i12 - 3, i11 + i10 + 4, i12 + i14 + 3, i15, i15);
					int i16 = 1347420415;
					int i17 = (i16 & 16711422) >> 1 | i16 & (int)unchecked((int)0xFF000000);
					this.drawGradientRect(i11 - 3, i12 - 3 + 1, i11 - 3 + 1, i12 + i14 + 3 - 1, i16, i17);
					this.drawGradientRect(i11 + i10 + 2, i12 - 3 + 1, i11 + i10 + 3, i12 + i14 + 3 - 1, i16, i17);
					this.drawGradientRect(i11 - 3, i12 - 3, i11 + i10 + 3, i12 - 3 + 1, i16, i16);
					this.drawGradientRect(i11 - 3, i12 + i14 + 2, i11 + i10 + 3, i12 + i14 + 3, i17, i17);

					for (int i18 = 0; i18 < list24.Count; ++i18)
					{
						string string19 = (string)list24[i18];
						if (i18 == 0)
						{
							string19 = "\u00a7" + Convert.ToString(itemStack23.Rarity.nameColor, 16) + string19;
						}
						else
						{
							string19 = "\u00a77" + string19;
						}

						this.fontRenderer.drawStringWithShadow(string19, i11, i12, -1);
						if (i18 == 0)
						{
							i12 += 2;
						}

						i12 += 10;
					}

					this.zLevel = 0.0F;
					itemRenderer.zLevel = 0.0F;
				}
			}

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			base.drawScreen(i1, i2, f3);
            Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
            GL.Enable(EnableCap.DepthTest);
		}

		protected internal virtual void drawGuiContainerForegroundLayer()
		{
		}

		protected internal abstract void drawGuiContainerBackgroundLayer(float f1, int i2, int i3);

		private void drawSlotInventory(Slot slot1)
		{
			int i2 = slot1.xDisplayPosition;
			int i3 = slot1.yDisplayPosition;
			ItemStack itemStack4 = slot1.Stack;
			bool z5 = false;
			this.zLevel = 100.0F;
			itemRenderer.zLevel = 100.0F;
			if (itemStack4 == null)
			{
				int i8 = slot1.BackgroundIconIndex;
				if (i8 >= 0)
				{
					Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
					this.mc.renderEngine.bindTexture(this.mc.renderEngine.getTexture("/gui/items.png"));
					this.drawTexturedModalRect(i2, i3, i8 % 16 * 16, i8 / 16 * 16, 16, 16);
					Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
					z5 = true;
				}
			}

			if (!z5)
			{
				itemRenderer.renderItemIntoGUI(this.fontRenderer, this.mc.renderEngine, itemStack4, i2, i3);
				itemRenderer.renderItemOverlayIntoGUI(this.fontRenderer, this.mc.renderEngine, itemStack4, i2, i3);
			}

			itemRenderer.zLevel = 0.0F;
			this.zLevel = 0.0F;
		}

		private Slot getSlotAtPosition(int i1, int i2)
		{
			for (int i3 = 0; i3 < this.inventorySlots.inventorySlots.Count; ++i3)
			{
				Slot slot4 = (Slot)this.inventorySlots.inventorySlots[i3];
				if (this.isMouseOverSlot(slot4, i1, i2))
				{
					return slot4;
				}
			}

			return null;
		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
			if (i3 == 0 || i3 == 1)
			{
				Slot slot4 = this.getSlotAtPosition(i1, i2);
				int i5 = this.guiLeft;
				int i6 = this.guiTop;
				bool z7 = i1 < i5 || i2 < i6 || i1 >= i5 + this.xSize || i2 >= i6 + this.ySize;
				int i8 = -1;
				if (slot4 != null)
				{
					i8 = slot4.slotNumber;
				}

				if (z7)
				{
					i8 = -999;
				}

				if (i8 != -1)
				{
					bool z9 = i8 != -999 && (mc.mcApplet.KeyboardState.IsKeyDown(Keys.LeftShift) || mc.mcApplet.KeyboardState.IsKeyDown(Keys.RightShift));
					this.handleMouseClick(slot4, i8, i3, z9);
				}
			}

		}

		private bool isMouseOverSlot(Slot slot1, int i2, int i3)
		{
			int i4 = this.guiLeft;
			int i5 = this.guiTop;
			i2 -= i4;
			i3 -= i5;
			return i2 >= slot1.xDisplayPosition - 1 && i2 < slot1.xDisplayPosition + 16 + 1 && i3 >= slot1.yDisplayPosition - 1 && i3 < slot1.yDisplayPosition + 16 + 1;
		}

		protected internal virtual void handleMouseClick(Slot slot1, int i2, int i3, bool z4)
		{
			if (slot1 != null)
			{
				i2 = slot1.slotNumber;
			}

			this.mc.playerController.windowClick(this.inventorySlots.windowId, i2, i3, z4, this.mc.thePlayer);
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (i2 == 1 || i2 == this.mc.gameSettings.keyBindInventory.keyCode)
			{
				this.mc.thePlayer.closeScreen();
			}

		}

		public override void onGuiClosed()
		{
			if (this.mc.thePlayer != null)
			{
				this.inventorySlots.onCraftGuiClosed(this.mc.thePlayer);
				this.mc.playerController.func_20086_a(this.inventorySlots.windowId, this.mc.thePlayer);
			}
		}

		public override bool doesGuiPauseGame()
		{
			return false;
		}

		public override void updateScreen()
		{
			base.updateScreen();
			if (!this.mc.thePlayer.EntityAlive || this.mc.thePlayer.isDead)
			{
				this.mc.thePlayer.closeScreen();
			}

		}
	}

}