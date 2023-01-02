using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class GuiEditSign : GuiScreen
	{
		private static readonly string allowedCharacters = ChatAllowedCharacters.allowedCharacters;
		protected internal string screenTitle = "Edit sign message:";
		private TileEntitySign entitySign;
		private int updateCounter;
		private int editLine = 0;

		public GuiEditSign(TileEntitySign tileEntitySign1)
		{
			this.entitySign = tileEntitySign1;
		}

		public override void initGui()
		{
			this.controlList.Clear();
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			this.controlList.Add(new GuiButton(0, this.width / 2 - 100, this.height / 4 + 120, "Done"));
			this.entitySign.func_50006_a(false);
		}

		public override void onGuiClosed()
		{
			mc.mcApplet.EnableKeyRepeatingEvents(false);
			if (this.mc.theWorld.isRemote)
			{
				this.mc.SendQueue.addToSendQueue(new Packet130UpdateSign(this.entitySign.xCoord, this.entitySign.yCoord, this.entitySign.zCoord, this.entitySign.signText));
			}

			this.entitySign.func_50006_a(true);
		}

		public override void updateScreen()
		{
			++this.updateCounter;
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 0)
				{
					this.entitySign.onInventoryChanged();
					this.mc.displayGuiScreen((GuiScreen)null);
				}

			}
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (i2 == 200)
			{
				this.editLine = this.editLine - 1 & 3;
			}

			if (i2 == 208 || i2 == 28)
			{
				this.editLine = this.editLine + 1 & 3;
			}

			if (i2 == 14 && this.entitySign.signText[this.editLine].Length > 0)
			{
				this.entitySign.signText[this.editLine] = this.entitySign.signText[this.editLine].Substring(0, this.entitySign.signText[this.editLine].Length - 1);
			}

			if (allowedCharacters.IndexOf(c1) >= 0 && this.entitySign.signText[this.editLine].Length < 15)
			{
				this.entitySign.signText[this.editLine] = this.entitySign.signText[this.editLine] + c1;
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, this.screenTitle, this.width / 2, 40, 0xFFFFFF);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate((float)(this.width / 2), 0.0F, 50.0F);
			float f4 = 93.75F;
            Minecraft.renderPipeline.ModelMatrix.Scale(-f4, -f4, -f4);
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
			Block block5 = this.entitySign.BlockType;
			if (block5 == Block.signPost)
			{
				float f6 = (float)(this.entitySign.BlockMetadata * 360) / 16.0F;
                Minecraft.renderPipeline.ModelMatrix.Rotate(f6, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -1.0625F, 0.0F);
			}
			else
			{
				int i8 = this.entitySign.BlockMetadata;
				float f7 = 0.0F;
				if (i8 == 2)
				{
					f7 = 180.0F;
				}

				if (i8 == 4)
				{
					f7 = 90.0F;
				}

				if (i8 == 5)
				{
					f7 = -90.0F;
				}

                Minecraft.renderPipeline.ModelMatrix.Rotate(f7, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -1.0625F, 0.0F);
			}

			if (this.updateCounter / 6 % 2 == 0)
			{
				this.entitySign.lineBeingEdited = this.editLine;
			}

			TileEntityRenderer.instance.renderTileEntityAt(this.entitySign, -0.5D, -0.75D, -0.5D, 0.0F);
			this.entitySign.lineBeingEdited = -1;
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			base.drawScreen(i1, i2, f3);
		}
	}

}