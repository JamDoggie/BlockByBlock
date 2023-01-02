using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class GuiCrafting : GuiContainer
	{
		public GuiCrafting(InventoryPlayer inventoryPlayer1, World world2, int i3, int i4, int i5) : base(new ContainerWorkbench(inventoryPlayer1, world2, i3, i4, i5))
		{
		}

		public override void onGuiClosed()
		{
			base.onGuiClosed();
		}

		protected internal override void drawGuiContainerForegroundLayer()
		{
			this.fontRenderer.drawString(StatCollector.translateToLocal("container.crafting"), 28, 6, 4210752);
			this.fontRenderer.drawString(StatCollector.translateToLocal("container.inventory"), 8, this.ySize - 96 + 2, 4210752);
		}

		protected internal override void drawGuiContainerBackgroundLayer(float f1, int i2, int i3)
		{
			int i4 = this.mc.renderEngine.getTexture("/gui/crafting.png");
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.mc.renderEngine.bindTexture(i4);
			int i5 = (this.width - this.xSize) / 2;
			int i6 = (this.height - this.ySize) / 2;
			this.drawTexturedModalRect(i5, i6, 0, 0, this.xSize, this.ySize);
		}
	}

}