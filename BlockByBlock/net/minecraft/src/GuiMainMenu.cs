using BlockByBlock;
using BlockByBlock.helpers;
using BlockByBlock.java_extensions;
using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections;
using System.IO;

namespace net.minecraft.src
{

	public class GuiMainMenu : GuiScreen
	{
		private static readonly RandomExtended rand = new RandomExtended();
		private float updateCounter = 0.0F;
		private string splashText = "missingno";
		private GuiButton multiplayerButton;
		private int panoramaTimer = 0;
		private int viewportTexture;

		public GuiMainMenu()
		{
			try
			{
				ArrayList arrayList1 = new ArrayList();
				StreamReader bufferedReader2 = new StreamReader(GameEnv.GetResourceAsStream("/title/splashes.txt"));
				string string3 = "";

				while (!string.ReferenceEquals((string3 = bufferedReader2.ReadLine()), null))
				{
					string3 = string3.Trim();
					if (string3.Length > 0)
					{
						arrayList1.Add(string3);
					}
				}

				do
				{
					this.splashText = (string)arrayList1[rand.Next(arrayList1.Count)];
				} while (this.splashText.GetHashCode() == 125780783);
			}
			catch (Exception)
			{
			}

			this.updateCounter = rand.NextSingle();
		}

		public override void updateScreen()
		{
			// PORTING TODO: TEMPORARY, DELETE LATER WHEN A PROPER AUTHENTICATION SYSTEM IS IN PLACE.
			usernameField.updateCursorCounter();
            ++this.panoramaTimer;
		}

		public override bool doesGuiPauseGame()
		{
			return false;
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			usernameField.keyTyped(c1, i2);
			mc.session.username = usernameField.Text;
		}

		public override void initGui()
		{
			this.viewportTexture = this.mc.renderEngine.allocateAndSetupTexture(new Image<Bgra32>(256, 256));
			DateTime calendar1 = DateTime.Now;
			if (calendar1.Month + 1 == 11 && calendar1.Day == 9)
			{
				this.splashText = "Happy birthday, ez!";
			}
			else if (calendar1.Month + 1 == 6 && calendar1.Day == 1)
			{
				this.splashText = "Happy birthday, Notch!";
			}
			else if (calendar1.Month + 1 == 12 && calendar1.Day == 24)
			{
				this.splashText = "Merry X-mas!";
			}
			else if (calendar1.Month + 1 == 1 && calendar1.Day == 1)
			{
				this.splashText = "Happy new year!";
			}

			StringTranslate stringTranslate2 = StringTranslate.Instance;
			int i4 = this.height / 4 + 48;
			this.controlList.Add(new GuiButton(1, this.width / 2 - 100, i4, stringTranslate2.translateKey("menu.singleplayer")));
			this.controlList.Add(this.multiplayerButton = new GuiButton(2, this.width / 2 - 100, i4 + 24, stringTranslate2.translateKey("menu.multiplayer")));
			this.controlList.Add(new GuiButton(3, this.width / 2 - 100, i4 + 48, stringTranslate2.translateKey("menu.mods")));
			if (this.mc.hideQuitButton)
			{
				this.controlList.Add(new GuiButton(0, this.width / 2 - 100, i4 + 72, stringTranslate2.translateKey("menu.options")));
			}
			else
			{
				this.controlList.Add(new GuiButton(0, this.width / 2 - 100, i4 + 72 + 12, 98, 20, stringTranslate2.translateKey("menu.options")));
				this.controlList.Add(new GuiButton(4, this.width / 2 + 2, i4 + 72 + 12, 98, 20, stringTranslate2.translateKey("menu.quit")));
			}

			this.controlList.Add(new GuiButtonLanguage(5, this.width / 2 - 124, i4 + 72 + 12));
            
            if (this.mc.session == null)
			{
				this.multiplayerButton.enabled = false;
			}

			temporaryUsernameTextInit();
        }

		// This will be replaced with proper authentication with a microsoft account.
		private GuiTextField usernameField;

		protected void temporaryUsernameTextInit()
		{
            usernameField = new GuiTextField(fontRenderer, width / 2 - 100, height / 4 + 48 + 72 + 12 + 30, 200, 20) { Text = "Yammy" };
			usernameField.setFocused(true);
        }

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.id == 0)
			{
				this.mc.displayGuiScreen(new GuiOptions(this, this.mc.gameSettings));
			}

			if (guiButton1.id == 5)
			{
				this.mc.displayGuiScreen(new GuiLanguage(this, this.mc.gameSettings));
			}

			if (guiButton1.id == 1)
			{
				this.mc.displayGuiScreen(new GuiSelectWorld(this));
			}

			if (guiButton1.id == 2)
			{
				this.mc.displayGuiScreen(new GuiMultiplayer(this));
			}

			if (guiButton1.id == 3)
			{
				this.mc.displayGuiScreen(new GuiTexturePacks(this));
			}

			if (guiButton1.id == 4)
			{
				this.mc.shutdown();
			}

		}

		private void drawPanorama(int i1, int i2, float f3)
		{
			Tessellator tessellator4 = Tessellator.instance;
			GL.MatrixMode(MatrixMode.Projection);
            Minecraft.newRenderer.CameraMatrix.PushMatrix();
            Minecraft.newRenderer.CameraMatrix.LoadIdentity();
            
			Matrix4 projMatrix = Glu.Perspective(120.0F, 1.0F, 0.05F, 10.0F);
			Minecraft.newRenderer.CameraMatrix.MultMatrix(projMatrix);

            GL.MatrixMode(MatrixMode.Modelview);
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            Minecraft.newRenderer.ModelMatrix.LoadIdentity();
			GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            Minecraft.newRenderer.ModelMatrix.Rotate(180.0F, 1.0F, 0.0F, 0.0F);
			GL.Enable(EnableCap.Blend);
			GL.Disable(EnableCap.AlphaTest);
			GL.Disable(EnableCap.CullFace);
			GL.DepthMask(false);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			sbyte b5 = 8;

			for (int i6 = 0; i6 < b5 * b5; ++i6)
			{
                Minecraft.newRenderer.ModelMatrix.PushMatrix();
				float f7 = ((float)(i6 % b5) / (float)b5 - 0.5F) / 64.0F;
				float f8 = ((float)(i6 / b5) / (float)b5 - 0.5F) / 64.0F;
				float f9 = 0.0F;
                Minecraft.newRenderer.ModelMatrix.Translate(f7, f8, f9);
                Minecraft.newRenderer.ModelMatrix.Rotate(MathHelper.sin(((float)this.panoramaTimer + f3) / 400.0F) * 25.0F + 20.0F, 1.0F, 0.0F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Rotate(-((float)this.panoramaTimer + f3) * 0.1F, 0.0F, 1.0F, 0.0F);

				for (int i10 = 0; i10 < 6; ++i10)
				{
                    Minecraft.newRenderer.ModelMatrix.PushMatrix();
					if (i10 == 1)
					{
                        Minecraft.newRenderer.ModelMatrix.Rotate(90.0F, 0.0F, 1.0F, 0.0F);
					}

					if (i10 == 2)
					{
                        Minecraft.newRenderer.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
					}

					if (i10 == 3)
					{
                        Minecraft.newRenderer.ModelMatrix.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
					}

					if (i10 == 4)
					{
                        Minecraft.newRenderer.ModelMatrix.Rotate(90.0F, 1.0F, 0.0F, 0.0F);
					}

					if (i10 == 5)
					{
                        Minecraft.newRenderer.ModelMatrix.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
					}

					GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/title/bg/panorama" + i10 + ".png"));
					tessellator4.startDrawingQuads();
					tessellator4.setColorRGBA_I(0xFFFFFF, 255 / (i6 + 1));
					float f11 = 0.0F;
					tessellator4.addVertexWithUV(-1.0D, -1.0D, 1.0D, (double)(0.0F + f11), (double)(0.0F + f11));
					tessellator4.addVertexWithUV(1.0D, -1.0D, 1.0D, (double)(1.0F - f11), (double)(0.0F + f11));
					tessellator4.addVertexWithUV(1.0D, 1.0D, 1.0D, (double)(1.0F - f11), (double)(1.0F - f11));
					tessellator4.addVertexWithUV(-1.0D, 1.0D, 1.0D, (double)(0.0F + f11), (double)(1.0F - f11));
					tessellator4.draw();
                    Minecraft.newRenderer.ModelMatrix.PopMatrix();
				}

                Minecraft.newRenderer.ModelMatrix.PopMatrix();
				GL.ColorMask(true, true, true, false);
			}

			tessellator4.setTranslation(0.0D, 0.0D, 0.0D);
			GL.ColorMask(true, true, true, true);
			GL.MatrixMode(MatrixMode.Projection);
            Minecraft.newRenderer.CameraMatrix.PopMatrix();
			GL.MatrixMode(MatrixMode.Modelview);
            Minecraft.newRenderer.ModelMatrix.PopMatrix();
			GL.DepthMask(true);
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.AlphaTest);
			GL.Enable(EnableCap.DepthTest);
		}

		private void rotateAndBlurSkybox(float f1)
		{
			GL.BindTexture(TextureTarget.Texture2D, this.viewportTexture);
			GL.CopyTexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, 0, 0, 256, 256);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			GL.ColorMask(true, true, true, false);
			Tessellator tessellator2 = Tessellator.instance;
			tessellator2.startDrawingQuads();
			sbyte b3 = 3;

			for (int i4 = 0; i4 < b3; ++i4)
			{
				tessellator2.setColorRGBA_F(1.0F, 1.0F, 1.0F, 1.0F / (float)(i4 + 1));
				int i5 = this.width;
				int i6 = this.height;
				float f7 = (float)(i4 - b3 / 2) / 256.0F;
				tessellator2.addVertexWithUV((double)i5, (double)i6, (double)this.zLevel, (double)(0.0F + f7), 0.0D);
				tessellator2.addVertexWithUV((double)i5, 0.0D, (double)this.zLevel, (double)(1.0F + f7), 0.0D);
				tessellator2.addVertexWithUV(0.0D, 0.0D, (double)this.zLevel, (double)(1.0F + f7), 1.0D);
				tessellator2.addVertexWithUV(0.0D, (double)i6, (double)this.zLevel, (double)(0.0F + f7), 1.0D);
			}

			tessellator2.draw();
			GL.ColorMask(true, true, true, true);
		}

		private void renderSkybox(int i1, int i2, float f3)
		{
			GL.Viewport(0, 0, 256, 256);
			this.drawPanorama(i1, i2, f3);
			GL.Disable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Texture2D);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			this.rotateAndBlurSkybox(f3);
			GL.Viewport(0, 0, this.mc.displayWidth, this.mc.displayHeight);
			Tessellator tessellator4 = Tessellator.instance;
			tessellator4.startDrawingQuads();
			float f5 = this.width > this.height ? 120.0F / (float)this.width : 120.0F / (float)this.height;
			float f6 = (float)this.height * f5 / 256.0F;
			float f7 = (float)this.width * f5 / 256.0F;
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureManager.TextureFilterLinear);
			GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureManager.TextureFilterLinear);
			tessellator4.setColorRGBA_F(1.0F, 1.0F, 1.0F, 1.0F);
			int i8 = this.width;
			int i9 = this.height;
			tessellator4.addVertexWithUV(0.0D, (double)i9, (double)this.zLevel, (double)(0.5F - f6), (double)(0.5F + f7));
			tessellator4.addVertexWithUV((double)i8, (double)i9, (double)this.zLevel, (double)(0.5F - f6), (double)(0.5F - f7));
			tessellator4.addVertexWithUV((double)i8, 0.0D, (double)this.zLevel, (double)(0.5F + f6), (double)(0.5F - f7));
			tessellator4.addVertexWithUV(0.0D, 0.0D, (double)this.zLevel, (double)(0.5F + f6), (double)(0.5F + f7));
			tessellator4.draw();
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.renderSkybox(i1, i2, f3);
			Tessellator tessellator4 = Tessellator.instance;
			short s5 = 274;
			int i6 = this.width / 2 - s5 / 2;
			sbyte b7 = 30;
			this.drawGradientRect(0, 0, this.width, this.height, -2130706433, 0xFFFFFF);
			this.drawGradientRect(0, 0, this.width, this.height, 0, int.MinValue);
			GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/title/mclogo.png"));
			GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
			if ((double)this.updateCounter < 1.0E-4D)
			{
				this.drawTexturedModalRect(i6 + 0, b7 + 0, 0, 0, 99, 44);
				this.drawTexturedModalRect(i6 + 99, b7 + 0, 129, 0, 27, 44);
				this.drawTexturedModalRect(i6 + 99 + 26, b7 + 0, 126, 0, 3, 44);
				this.drawTexturedModalRect(i6 + 99 + 26 + 3, b7 + 0, 99, 0, 26, 44);
				this.drawTexturedModalRect(i6 + 155, b7 + 0, 0, 45, 155, 44);
			}
			else
			{
				this.drawTexturedModalRect(i6 + 0, b7 + 0, 0, 0, 155, 44);
				this.drawTexturedModalRect(i6 + 155, b7 + 0, 0, 45, 155, 44);
			}

			tessellator4.ColorOpaque_I = 0xFFFFFF;
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            Minecraft.newRenderer.ModelMatrix.Translate((float)(this.width / 2 + 90), 70.0F, 0.0F);
            Minecraft.newRenderer.ModelMatrix.Rotate(-20.0F, 0.0F, 0.0F, 1.0F);
			float f8 = 1.8F - MathHelper.abs(MathHelper.sin((float)(DateTimeHelper.CurrentUnixTimeMillis() % 1000L) / 1000.0F * (float)Math.PI * 2.0F) * 0.1F);
			f8 = f8 * 100.0F / (float)(this.fontRenderer.getStringWidth(this.splashText) + 32);
            Minecraft.newRenderer.ModelMatrix.Scale(f8, f8, f8);
			this.drawCenteredString(this.fontRenderer, this.splashText, 0, -8, 16776960);
            Minecraft.newRenderer.ModelMatrix.PopMatrix();
			this.drawString(this.fontRenderer, "Minecraft 1.2.5", 2, this.height - 10, 0xFFFFFF);
			string string9 = "Copyright Mojang AB. Do not distribute!";
			this.drawString(this.fontRenderer, string9, this.width - this.fontRenderer.getStringWidth(string9) - 2, this.height - 10, 0xFFFFFF);

			// PORTING TODO: TEMPORARY, DELETE LATER WHEN A PROPER AUTHENTICATION SYSTEM IS IN PLACE.
			usernameField.drawTextBox();

			base.drawScreen(i1, i2, f3);
		}
	}

}