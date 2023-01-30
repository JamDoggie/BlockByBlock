using BlockByBlock.net.minecraft.render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using System;
using System.Drawing;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class LoadingScreenRenderer : IProgressUpdate
	{
		private string field_1004_a = "";
		private Minecraft mc;
		private string currentlyDisplayedText = "";
		private long field_1006_d = DateTimeHelper.CurrentUnixTimeMillis();
		private bool field_1005_e = false;

		public LoadingScreenRenderer(Minecraft minecraft1)
		{
			this.mc = minecraft1;
		}

		public virtual void printText(string string1)
		{
			this.field_1005_e = false;
			this.func_597_c(string1);
		}

		public virtual void displaySavingString(string string1)
		{
			this.field_1005_e = true;
			this.func_597_c(this.currentlyDisplayedText);
		}

		public virtual void func_597_c(string string1)
		{
			if (!this.mc.running)
			{
				if (!this.field_1005_e)
				{
					throw new MinecraftError();
				}
			}
			else
			{
				this.currentlyDisplayedText = string1;
				ScaledResolution scaledResolution2 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
				GL.Clear(ClearBufferMask.DepthBufferBit);
                Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();
                Minecraft.renderPipeline.ProjectionMatrix.Ortho(0.0D, scaledResolution2.scaledWidthD, scaledResolution2.scaledHeightD, 0.0D, 100.0D, 300.0D);
                Minecraft.renderPipeline.ModelMatrix.LoadIdentity();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -200.0F);
			}
		}

		public virtual void displayLoadingString(string string1)
		{
			if (!this.mc.running)
			{
				if (!this.field_1005_e)
				{
					throw new MinecraftError();
				}
			}
			else
			{
				this.field_1006_d = 0L;
				this.field_1004_a = string1;
				this.LoadingProgress = -1;
				this.field_1006_d = 0L;
			}
		}

		public virtual int LoadingProgress
		{
			set
			{
				if (!this.mc.running)
				{
					if (!this.field_1005_e)
					{
						throw new MinecraftError();
					}
				}
				else
				{
                    long j2 = DateTimeHelper.CurrentUnixTimeMillis();
					if (j2 - this.field_1006_d >= 100L)
					{
						this.field_1006_d = j2;
						ScaledResolution scaledResolution4 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
						int i5 = scaledResolution4.ScaledWidth;
						int i6 = scaledResolution4.ScaledHeight;
						GL.Clear(ClearBufferMask.DepthBufferBit);
                        Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();
                        Minecraft.renderPipeline.ProjectionMatrix.Ortho(0.0D, scaledResolution4.scaledWidthD, scaledResolution4.scaledHeightD, 0.0D, 100.0D, 300.0D);
                        Minecraft.renderPipeline.ModelMatrix.LoadIdentity();
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -200.0F);
						GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
						Tessellator tessellator7 = Tessellator.instance;
						int i8 = this.mc.renderEngine.getTexture("/gui/background.png");
						GL.BindTexture(TextureTarget.Texture2D, i8);
						float f9 = 32.0F;
						tessellator7.startDrawingQuads();
						tessellator7.ColorOpaque_I = 4210752;
						tessellator7.AddVertexWithUV(0.0D, (double)i6, 0.0D, 0.0D, (double)((float)i6 / f9));
						tessellator7.AddVertexWithUV((double)i5, (double)i6, 0.0D, (double)((float)i5 / f9), (double)((float)i6 / f9));
						tessellator7.AddVertexWithUV((double)i5, 0.0D, 0.0D, (double)((float)i5 / f9), 0.0D);
						tessellator7.AddVertexWithUV(0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
						tessellator7.DrawImmediate();
						if (value >= 0)
						{
							sbyte b10 = 100;
							sbyte b11 = 2;
							int i12 = i5 / 2 - b10 / 2;
							int i13 = i6 / 2 + 16;
                            Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                            tessellator7.startDrawingQuads();
							tessellator7.ColorOpaque_I = 8421504;
							tessellator7.AddVertex((double)i12, (double)i13, 0.0D);
							tessellator7.AddVertex((double)i12, (double)(i13 + b11), 0.0D);
							tessellator7.AddVertex((double)(i12 + b10), (double)(i13 + b11), 0.0D);
							tessellator7.AddVertex((double)(i12 + b10), (double)i13, 0.0D);
							tessellator7.ColorOpaque_I = 8454016;
							tessellator7.AddVertex((double)i12, (double)i13, 0.0D);
							tessellator7.AddVertex((double)i12, (double)(i13 + b11), 0.0D);
							tessellator7.AddVertex((double)(i12 + value), (double)(i13 + b11), 0.0D);
							tessellator7.AddVertex((double)(i12 + value), (double)i13, 0.0D);
							tessellator7.DrawImmediate();
                            Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                        }
						

						this.mc.fontRenderer.drawStringWithShadow(this.currentlyDisplayedText, (i5 - this.mc.fontRenderer.getStringWidth(this.currentlyDisplayedText)) / 2, i6 / 2 - 4 - 16, 0xFFFFFF);
						this.mc.fontRenderer.drawStringWithShadow(this.field_1004_a, (i5 - this.mc.fontRenderer.getStringWidth(this.field_1004_a)) / 2, i6 / 2 - 4 + 8, 0xFFFFFF);
                        mc.mcApplet.Context.SwapBuffers();

                        try
						{
							Thread.Yield();
						}
						catch (Exception)
						{
						}
    
					}
				}
			}
		}
	}

}