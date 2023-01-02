using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace net.minecraft.src
{

    public class GuiContainerCreative : GuiContainer
	{
		private static InventoryBasic inventory = new InventoryBasic("tmp", 72);
		private float currentScroll = 0.0F;
		private bool isScrolling = false;
		private bool wasClicking;

		public GuiContainerCreative(EntityPlayer entityPlayer1) : base(new ContainerCreative(entityPlayer1))
		{
			entityPlayer1.craftingInventory = this.inventorySlots;
			this.allowUserInput = true;
			entityPlayer1.addStat(AchievementList.openInventory, 1);
			this.ySize = 208;
		}

		

		public override void updateScreen()
		{
			if (!this.mc.playerController.InCreativeMode)
			{
				this.mc.displayGuiScreen(new GuiInventory(this.mc.thePlayer));
			}

		}

		protected internal override void handleMouseClick(Slot slot1, int i2, int i3, bool z4)
		{
			InventoryPlayer inventoryPlayer5;
			ItemStack itemStack6;
			if (slot1 != null)
			{
				if (slot1.inventory == inventory)
				{
					inventoryPlayer5 = this.mc.thePlayer.inventory;
					itemStack6 = inventoryPlayer5.ItemStack;
					ItemStack itemStack7 = slot1.Stack;
					if (itemStack6 != null && itemStack7 != null && itemStack6.itemID == itemStack7.itemID)
					{
						if (i3 == 0)
						{
							if (z4)
							{
								itemStack6.stackSize = itemStack6.MaxStackSize;
							}
							else if (itemStack6.stackSize < itemStack6.MaxStackSize)
							{
								++itemStack6.stackSize;
							}
						}
						else if (itemStack6.stackSize <= 1)
						{
							inventoryPlayer5.ItemStack = (ItemStack)null;
						}
						else
						{
							--itemStack6.stackSize;
						}
					}
					else if (itemStack6 != null)
					{
						inventoryPlayer5.ItemStack = (ItemStack)null;
					}
					else if (itemStack7 == null)
					{
						inventoryPlayer5.ItemStack = (ItemStack)null;
					}
					else if (itemStack6 == null || itemStack6.itemID != itemStack7.itemID)
					{
						inventoryPlayer5.ItemStack = ItemStack.copyItemStack(itemStack7);
						itemStack6 = inventoryPlayer5.ItemStack;
						if (z4)
						{
							itemStack6.stackSize = itemStack6.MaxStackSize;
						}
					}
				}
				else
				{
					this.inventorySlots.slotClick(slot1.slotNumber, i3, z4, this.mc.thePlayer);
					ItemStack itemStack8 = this.inventorySlots.getSlot(slot1.slotNumber).Stack;
					this.mc.playerController.sendSlotPacket(itemStack8, slot1.slotNumber - this.inventorySlots.inventorySlots.Count + 9 + 36);
				}
			}
			else
			{
				inventoryPlayer5 = this.mc.thePlayer.inventory;
				if (inventoryPlayer5.ItemStack != null)
				{
					if (i3 == 0)
					{
						this.mc.thePlayer.dropPlayerItem(inventoryPlayer5.ItemStack);
						this.mc.playerController.func_35639_a(inventoryPlayer5.ItemStack);
						inventoryPlayer5.ItemStack = (ItemStack)null;
					}

					if (i3 == 1)
					{
						itemStack6 = inventoryPlayer5.ItemStack.splitStack(1);
						this.mc.thePlayer.dropPlayerItem(itemStack6);
						this.mc.playerController.func_35639_a(itemStack6);
						if (inventoryPlayer5.ItemStack.stackSize == 0)
						{
							inventoryPlayer5.ItemStack = (ItemStack)null;
						}
					}
				}
			}

		}

		public override void initGui()
		{
			if (!this.mc.playerController.InCreativeMode)
			{
				this.mc.displayGuiScreen(new GuiInventory(this.mc.thePlayer));
			}
			else
			{
				base.initGui();
				this.controlList.Clear();
			}

		}

		protected internal override void drawGuiContainerForegroundLayer()
		{
			this.fontRenderer.drawString(StatCollector.translateToLocal("container.creative"), 8, 6, 4210752);
		}

		public override void handleMouseInput()
		{
			base.handleMouseInput();
			float i1 = mc.MouseScrollDelta;
			if (i1 != 0)
			{
				int i2 = ((ContainerCreative)inventorySlots).itemList.Count / 8 - 8 + 1;
				if (i1 > 0F)
				{
					i1 = 1;
				}

				if (i1 < 0F)
				{
					i1 = -1;
				}

				currentScroll = currentScroll - i1 / (float)i2;
				if (currentScroll < 0.0F)
				{
					currentScroll = 0.0F;
				}

				if (currentScroll > 1.0F)
				{
					currentScroll = 1.0F;
				}

				((ContainerCreative)inventorySlots).scrollTo(currentScroll);
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			bool z4 = mc.mcApplet.MouseState.IsButtonDown(MouseButton.Left);
			int i5 = this.guiLeft;
			int i6 = this.guiTop;
			int i7 = i5 + 155;
			int i8 = i6 + 17;
			int i9 = i7 + 14;
			int i10 = i8 + 160 + 2;
			if (!this.wasClicking && z4 && i1 >= i7 && i2 >= i8 && i1 < i9 && i2 < i10)
			{
				this.isScrolling = true;
			}

			if (!z4)
			{
				this.isScrolling = false;
			}

			this.wasClicking = z4;
			if (this.isScrolling)
			{
				this.currentScroll = (float)(i2 - (i8 + 8)) / ((float)(i10 - i8) - 16.0F);
				if (this.currentScroll < 0.0F)
				{
					this.currentScroll = 0.0F;
				}

				if (this.currentScroll > 1.0F)
				{
					this.currentScroll = 1.0F;
				}

				((ContainerCreative)this.inventorySlots).scrollTo(this.currentScroll);
			}

			base.drawScreen(i1, i2, f3);
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
        }

		protected internal override void drawGuiContainerBackgroundLayer(float f1, int i2, int i3)
		{
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			int i4 = this.mc.renderEngine.getTexture("/gui/allitems.png");
			this.mc.renderEngine.bindTexture(i4);
			int i5 = this.guiLeft;
			int i6 = this.guiTop;
			this.drawTexturedModalRect(i5, i6, 0, 0, this.xSize, this.ySize);
			int i7 = i5 + 155;
			int i8 = i6 + 17;
			int i9 = i8 + 160 + 2;
			this.drawTexturedModalRect(i5 + 154, i6 + 17 + (int)((float)(i9 - i8 - 17) * this.currentScroll), 0, 208, 16, 16);
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

		internal static InventoryBasic Inventory
		{
			get
			{
				return inventory;
			}
		}
	}

}