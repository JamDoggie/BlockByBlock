using System;
using System.Collections;
using TextCopy;
using OpenTK.Graphics.OpenGL;
using net.minecraft.client;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class GuiScreen : Gui
	{
		protected internal Minecraft mc;
		public int width;
		public int height;
		protected internal System.Collections.IList controlList = new ArrayList();
		public bool allowUserInput = false;
		protected internal FontRenderer fontRenderer;
		public GuiParticle guiParticles;
		private GuiButton selectedButton = null;

		public virtual void drawScreen(int i1, int i2, float f3)
		{
			for (int i4 = 0; i4 < this.controlList.Count; ++i4)
			{
				GuiButton guiButton5 = (GuiButton)this.controlList[i4];
				guiButton5.drawButton(this.mc, i1, i2);
			}

		}

		protected internal virtual void keyTyped(char c1, int i2)
		{
			if (i2 == 1)
			{
				this.mc.displayGuiScreen((GuiScreen)null);
				this.mc.setIngameFocus();
			}

		}

		public static string? ClipboardString
		{
			get
			{
				return ClipboardService.GetText();
			}
		}

		public static void setClipboardString(string string0)
		{
			ClipboardService.SetText(string0);
		}

		protected internal virtual void mouseClicked(int i1, int i2, int i3)
		{
			if (i3 == 0)
			{
				for (int i4 = 0; i4 < this.controlList.Count; ++i4)
				{
					GuiButton guiButton5 = (GuiButton)this.controlList[i4];
					if (guiButton5.mousePressed(this.mc, i1, i2))
					{
						this.selectedButton = guiButton5;
						this.mc.sndManager.playSoundFX("random.click", 1.0F, 1.0F);
						this.actionPerformed(guiButton5);
					}
				}
			}

		}

		protected internal virtual void mouseMovedOrUp(int i1, int i2, int i3)
		{
			if (this.selectedButton != null && i3 == 0)
			{
				this.selectedButton.mouseReleased(i1, i2);
				this.selectedButton = null;
			}

		}

		protected internal virtual void actionPerformed(GuiButton guiButton1)
		{
		}

		public virtual void setWorldAndResolution(Minecraft minecraft1, int i2, int i3)
		{
			this.guiParticles = new GuiParticle(minecraft1);
			this.mc = minecraft1;
			this.fontRenderer = minecraft1.fontRenderer;
			this.width = i2;
			this.height = i3;
			this.controlList.Clear();
			this.initGui();
		}

		public virtual void initGui()
		{
		}

		public virtual void handleInput()
		{
			while (mc.mcApplet.NextMouseEvent())
			{
				this.handleMouseInput();
			}

			while (mc.mcApplet.NextKeyEvent())
			{
				this.handleKeyboardInput();
			}

		}

		public virtual void handleMouseInput()
		{
			int i1;
			int i2;

			if (mc.mcApplet.CurrentMouseEvent() == null)
				return;

			MouseEvent e = mc.mcApplet.CurrentMouseEvent()!.Value;

			if (e.IsPressed)
			{
				i1 = (int)mc.MouseX * this.width / this.mc.displayWidth;
				i2 = this.height - (int)mc.MouseY * this.height / this.mc.displayHeight - 1;
				this.mouseClicked(i1, i2, (int)e.button!.Value);
			}
			else
			{
				i1 = e.posX * this.width / this.mc.displayWidth;
				i2 = this.height - e.posY * this.height / this.mc.displayHeight - 1;
                this.mouseMovedOrUp(i1, i2, e.button == null ? -1 : (int)e.button!.Value);
            }

		}

		public virtual void handleKeyboardInput()
		{
			if (mc.mcApplet.CurrentKeyEvent() == null)
				return;

			KeyEvent e = mc.mcApplet.CurrentKeyEvent()!.Value;

			if (e.isPressed)
			{
				if (e.e.Key == Keys.F11)
				{
					this.mc.toggleFullscreen();
					return;
				}
				keyTyped((char)e.e.Key, (int)e.e.Key);
			}

		}

		public virtual void updateScreen()
		{
		}

		public virtual void onGuiClosed()
		{
		}

		public virtual void drawDefaultBackground()
		{
			this.drawWorldBackground(0);
		}

		public virtual void drawWorldBackground(int i1)
		{
			if (this.mc.theWorld != null)
			{
				this.drawGradientRect(0, 0, this.width, this.height, -1072689136, -804253680);
			}
			else
			{
				this.drawBackground(i1);
			}

		}

		public virtual void drawBackground(int i1)
		{
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Fog);
			Tessellator tessellator2 = Tessellator.instance;
			GL.BindTexture(TextureTarget.Texture2D, mc.renderEngine.getTexture("/gui/background.png"));
			GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
			float f3 = 32.0F;
			tessellator2.startDrawingQuads();
			tessellator2.ColorOpaque_I = 4210752;
			tessellator2.addVertexWithUV(0.0D, (double)this.height, 0.0D, 0.0D, (double)((float)this.height / f3 + (float)i1));
			tessellator2.addVertexWithUV((double)this.width, (double)this.height, 0.0D, (double)((float)this.width / f3), (double)((float)this.height / f3 + (float)i1));
			tessellator2.addVertexWithUV((double)this.width, 0.0D, 0.0D, (double)((float)this.width / f3), (double)i1);
			tessellator2.addVertexWithUV(0.0D, 0.0D, 0.0D, 0.0D, (double)i1);
			tessellator2.draw();
		}

		public virtual bool doesGuiPauseGame()
		{
			return true;
		}

		public virtual void confirmClicked(bool z1, int i2)
		{
		}

		public static bool isControlDown()
		{
			return MinecraftApplet.mcWindow.KeyboardState.IsKeyDown(Keys.LeftShift) || MinecraftApplet.mcWindow.KeyboardState.IsKeyDown(Keys.RightShift);
		}

		public static bool isShiftDown()
		{
			return MinecraftApplet.mcWindow.KeyboardState.IsKeyDown(Keys.LeftControl) || MinecraftApplet.mcWindow.KeyboardState.IsKeyDown(Keys.RightControl);
		}
	}

}