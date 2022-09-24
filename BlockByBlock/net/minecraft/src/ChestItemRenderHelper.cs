namespace net.minecraft.src
{
	public class ChestItemRenderHelper
	{
		public static ChestItemRenderHelper instance = new ChestItemRenderHelper();
		private TileEntityChest chestEntity = new TileEntityChest();

		/// <summary>
		/// Most likely just renders the chest tile entity in the world. I assume this is run every frame.
		/// This might also be for rendering the chest tile entity in the inventory... this is currently my best guess.
		/// PORTING TODO: figure out specifically what this is and should be called.
		/// </summary>
		/// <param name="block1"></param>
		/// <param name="i2"></param>
		/// <param name="f3"></param>
		public virtual void func_35609_a(Block block1, int i2, float f3)
		{
			TileEntityRenderer.instance.renderTileEntityAt(chestEntity, 0.0D, 0.0D, 0.0D, 0.0F);
		}
	}

}