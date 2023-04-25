using BlockByBlock;
using BlockByBlock.java_extensions;
using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.IO;

namespace net.minecraft.src
{
    public class GuiWinGame : GuiScreen
	{
		private int updateCounter = 0;
		private System.Collections.ArrayList lines;
		private int field_41042_d = 0;
		private float field_41043_e = 0.5F;

		public override void updateScreen()
		{
			++this.updateCounter;
			float f1 = (float)(this.field_41042_d + this.height + this.height + 24) / this.field_41043_e;
			if ((float)this.updateCounter > f1)
			{
				this.respawnPlayer();
			}

		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (i2 == 1)
			{
				this.respawnPlayer();
			}

		}

		private void respawnPlayer()
		{
			if (this.mc.theWorld.isRemote)
			{
				EntityClientPlayerMP entityClientPlayerMP1 = (EntityClientPlayerMP)this.mc.thePlayer;
				entityClientPlayerMP1.sendQueue.addToSendQueue(new Packet9Respawn(entityClientPlayerMP1.dimension, (sbyte)this.mc.theWorld.difficultySetting, this.mc.theWorld.WorldInfo.TerrainType, this.mc.theWorld.Height, 0));
			}
			else
			{
				this.mc.displayGuiScreen((GuiScreen)null);
				this.mc.respawn(this.mc.theWorld.isRemote, 0, true);
			}

		}

		public override bool doesGuiPauseGame()
		{
			return true;
		}

		public override void initGui()
		{
			if (this.lines == null)
			{
				this.lines = new ArrayList();

				try
				{
					string string1 = "";
					string string2 = "\u00a7f\u00a7k\u00a7a\u00a7b";
					short s3 = 274;
					StreamReader bufferedReader4 = new StreamReader(GameEnv.GetResourceAsStream("/title/win.txt"));
					RandomExtended random5 = new RandomExtended(8124371L);

					int i6;
					while (!string.ReferenceEquals((string1 = bufferedReader4.ReadLine()), null))
					{
						string string7;
						string string8;
						for (string1 = string1.Replace("PLAYERNAME", mc.session.username); string1.IndexOf(string2, StringComparison.Ordinal) >= 0; string1 = string7 + "\u00a7f\u00a7k" + "XXXXXXXX".Substring(0, random5.Next(4) + 3) + string8)
						{
							i6 = string1.IndexOf(string2, StringComparison.Ordinal);
							string7 = string1.Substring(0, i6);
							string8 = string1.Substring(i6 + string2.Length);
						}

						this.lines.AddRange(this.mc.fontRenderer.func_50108_c(string1, s3));
						this.lines.Add("");
					}

					for (i6 = 0; i6 < 8; ++i6)
					{
						this.lines.Add("");
					}

					bufferedReader4 = new StreamReader(GameEnv.GetResourceAsStream("/title/credits.txt"));

					while (!string.ReferenceEquals((string1 = bufferedReader4.ReadLine()), null))
					{
						string1 = string1.Replace("PLAYERNAME", this.mc.session.username);
						string1 = string1.Replace("\t", "    ");
						this.lines.AddRange(this.mc.fontRenderer.func_50108_c(string1, s3));
						this.lines.Add("");
					}

					this.field_41042_d = this.lines.Count * 12;
				}
				catch (Exception exception9)
				{
					Console.WriteLine(exception9.ToString());
					Console.Write(exception9.StackTrace);
				}

			}
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
		}

		private void func_41040_b(int i1, int i2, float f3)
		{
			Tessellator tessellator4 = Tessellator.instance;
			GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("%blur%/gui/background.png"));
			tessellator4.startDrawingQuads();
			tessellator4.setColorRGBA_F(1.0F, 1.0F, 1.0F, 1.0F);
			int i5 = this.width;
			float f6 = 0.0F - ((float)this.updateCounter + f3) * 0.5F * this.field_41043_e;
			float f7 = (float)this.height - ((float)this.updateCounter + f3) * 0.5F * this.field_41043_e;
			float f8 = 0.015625F;
			float f9 = ((float)this.updateCounter + f3 - 0.0F) * 0.02F;
			float f10 = (float)(this.field_41042_d + this.height + this.height + 24) / this.field_41043_e;
			float f11 = (f10 - 20.0F - ((float)this.updateCounter + f3)) * 0.005F;
			if (f11 < f9)
			{
				f9 = f11;
			}

			if (f9 > 1.0F)
			{
				f9 = 1.0F;
			}

			f9 *= f9;
			f9 = f9 * 96.0F / 255.0F;
			tessellator4.setColorOpaque_F(f9, f9, f9);
			tessellator4.AddVertexWithUV(0.0D, (double)this.height, (double)this.zLevel, 0.0D, (double)(f6 * f8));
			tessellator4.AddVertexWithUV((double)i5, (double)this.height, (double)this.zLevel, (double)((float)i5 * f8), (double)(f6 * f8));
			tessellator4.AddVertexWithUV((double)i5, 0.0D, (double)this.zLevel, (double)((float)i5 * f8), (double)(f7 * f8));
			tessellator4.AddVertexWithUV(0.0D, 0.0D, (double)this.zLevel, 0.0D, (double)(f7 * f8));
			tessellator4.DrawImmediate();
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			func_41040_b(i1, i2, f3);
			Tessellator tessellator4 = Tessellator.instance;
			short s5 = 274;
			int i6 = this.width / 2 - s5 / 2;
			int i7 = this.height + 50;
			float f8 = -((float)this.updateCounter + f3) * this.field_41043_e;
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, f8, 0.0F);
			GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/gui/refinedgui/blockbyblock-logo.png"));
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.drawTexturedModalRect(i6, i7, 0, 0, 155, 44);
			this.drawTexturedModalRect(i6 + 155, i7, 0, 45, 155, 44);
			tessellator4.ColorOpaque_I = 0xFFFFFF;
			int i9 = i7 + 200;

			int i10;
			for (i10 = 0; i10 < this.lines.Count; ++i10)
			{
				if (i10 == this.lines.Count - 1)
				{
					float f11 = (float)i9 + f8 - (float)(this.height / 2 - 6);
					if (f11 < 0.0F)
					{
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -f11, 0.0F);
					}
				}

				if ((float)i9 + f8 + 12.0F + 8.0F > 0.0F && (float)i9 + f8 < (float)this.height)
				{
					string string12 = (string)this.lines[i10];
					if (string12.StartsWith("[C]", StringComparison.Ordinal))
					{
						this.fontRenderer.drawStringWithShadow(string12.Substring(3), i6 + (s5 - this.fontRenderer.getStringWidth(string12.Substring(3))) / 2, i9, 0xFFFFFF);
					}
					else
					{
						fontRenderer.fontRandom.SetSeed((long)i10 * 4238972211L + (long)(this.updateCounter / 4));
						fontRenderer.fontRandom = new RandomExtended((long)i10 * 4238972211L + (long)(this.updateCounter / 4));
						this.fontRenderer.drawText(string12, i6 + 1, i9 + 1, 0xFFFFFF, true);
                        fontRenderer.fontRandom.SetSeed((long)i10 * 4238972211L + (long)(this.updateCounter / 4));
                        this.fontRenderer.drawText(string12, i6, i9, 0xFFFFFF, false);
					}
				}

				i9 += 12;
			}

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("%blur%/misc/vignette.png"));
			GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.Zero, BlendingFactor.OneMinusSrcColor);
            tessellator4.startDrawingQuads();
			tessellator4.setColorRGBA_F(1.0F, 1.0F, 1.0F, 1.0F);
			i10 = width;
			int i13 = height;
			tessellator4.AddVertexWithUV(0.0D, (double)i13, (double)this.zLevel, 0.0D, 1.0D);
			tessellator4.AddVertexWithUV((double)i10, (double)i13, (double)this.zLevel, 1.0D, 1.0D);
			tessellator4.AddVertexWithUV((double)i10, 0.0D, (double)this.zLevel, 1.0D, 0.0D);
			tessellator4.AddVertexWithUV(0.0D, 0.0D, (double)this.zLevel, 0.0D, 0.0D);
			tessellator4.DrawImmediate();
			GL.Disable(EnableCap.Blend);
			base.drawScreen(i1, i2, f3);
		}
	}

}