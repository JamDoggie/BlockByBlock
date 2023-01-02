using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class GuiChest : GuiContainer
	{
		private IInventory upperChestInventory;
		private IInventory lowerChestInventory;
		private int inventoryRows = 0;

		public GuiChest(IInventory iInventory1, IInventory iInventory2) : base(new ContainerChest(iInventory1, iInventory2))
		{
			this.upperChestInventory = iInventory1;
			this.lowerChestInventory = iInventory2;
			this.allowUserInput = false;
			short s3 = 222;
			int i4 = s3 - 108;
			this.inventoryRows = iInventory2.SizeInventory / 9;
			this.ySize = i4 + this.inventoryRows * 18;
		}

		protected internal override void drawGuiContainerForegroundLayer()
		{
			this.fontRenderer.drawString(StatCollector.translateToLocal(this.lowerChestInventory.InvName), 8, 6, 4210752);
			this.fontRenderer.drawString(StatCollector.translateToLocal(this.upperChestInventory.InvName), 8, this.ySize - 96 + 2, 4210752);
		}

		protected internal override void drawGuiContainerBackgroundLayer(float f1, int i2, int i3)
		{
			int i4 = this.mc.renderEngine.getTexture("/gui/container.png");
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.mc.renderEngine.bindTexture(i4);
			int i5 = (this.width - this.xSize) / 2;
			int i6 = (this.height - this.ySize) / 2;
			this.drawTexturedModalRect(i5, i6, 0, 0, this.xSize, this.inventoryRows * 18 + 17);
			this.drawTexturedModalRect(i5, i6 + this.inventoryRows * 18 + 17, 0, 126, this.xSize, 96);
		}
	}

}