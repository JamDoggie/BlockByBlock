using BlockByBlock.helpers;
using System;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;
    

	public class GuiTexturePacks : GuiScreen
	{
		protected internal GuiScreen guiScreen;
		private int refreshTimer = -1;
		private string fileLocation = "";
		private GuiTexturePackSlot guiTexturePackSlot;

		public GuiTexturePacks(GuiScreen guiScreen1)
		{
			this.guiScreen = guiScreen1;
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			controlList.Add(new GuiSmallButton(5, this.width / 2 - 154, this.height - 48, stringTranslate1.translateKey("texturePack.openFolder")));
			controlList.Add(new GuiSmallButton(6, this.width / 2 + 4, this.height - 48, stringTranslate1.translateKey("gui.done")));
			mc.texturePackList.updateAvaliableTexturePacks();
			fileLocation = Minecraft.MinecraftDir.FullName + "/texturepacks";
			guiTexturePackSlot = new GuiTexturePackSlot(this);
			guiTexturePackSlot.registerScrollButtons(this.controlList, 7, 8);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 5)
				{
					bool z2 = false;

					try
					{
						SystemHelpers.OpenUrl(fileLocation);
					}
					catch (Exception throwable5)
					{
						Console.WriteLine(throwable5.ToString());
						Console.Write(throwable5.StackTrace);
						z2 = true;
					}

					if (z2)
					{
						Console.WriteLine("Opening via Sys class!");
						SystemHelpers.OpenUrl("file://" + this.fileLocation);
					}
				}
				else if (guiButton1.id == 6)
				{
					this.mc.renderEngine.refreshTextures();
					this.mc.displayGuiScreen(this.guiScreen);
				}
				else
				{
					this.guiTexturePackSlot.actionPerformed(guiButton1);
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
			this.guiTexturePackSlot.drawScreen(i1, i2, f3);
			if (this.refreshTimer <= 0)
			{
				this.mc.texturePackList.updateAvaliableTexturePacks();
				this.refreshTimer += 20;
			}

			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("texturePack.title"), this.width / 2, 16, 0xFFFFFF);
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("texturePack.folderInfo"), this.width / 2 - 77, this.height - 26, 8421504);
			base.drawScreen(i1, i2, f3);
		}

		public override void updateScreen()
		{
			base.updateScreen();
			--this.refreshTimer;
		}

		internal static Minecraft func_22124_a(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22126_b(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22119_c(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22122_d(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22117_e(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_35307_f(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_35308_g(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22118_f(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22116_g(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22121_h(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???
																				   /// WTF???
		internal static Minecraft func_22123_i(GuiTexturePacks guiTexturePacks0)   /// WTF???
		{																		   /// WTF???
			return guiTexturePacks0.mc;											   /// WTF???
		}																		   /// WTF???

		internal static FontRenderer func_22127_j(GuiTexturePacks guiTexturePacks0)
		{
			return guiTexturePacks0.fontRenderer;
		}

		internal static FontRenderer func_22120_k(GuiTexturePacks guiTexturePacks0)
		{
			return guiTexturePacks0.fontRenderer;
		}

		internal static FontRenderer func_22125_l(GuiTexturePacks guiTexturePacks0)
		{
			return guiTexturePacks0.fontRenderer;
		}
	}

}