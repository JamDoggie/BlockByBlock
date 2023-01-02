using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

    public class MapItemRenderer
	{
		private int[] intArray = new int[16384];
		private int bufferedImage;
		private GameSettings gameSettings;
		private FontRenderer fontRenderer;

		public MapItemRenderer(FontRenderer fontRenderer1, GameSettings gameSettings2, TextureManager renderEngine3)
		{
			gameSettings = gameSettings2;
			fontRenderer = fontRenderer1;
			bufferedImage = renderEngine3.allocateAndSetupTexture(new Image<Bgra32>(128, 128));

			for (int i4 = 0; i4 < 16384; ++i4)
			{
				this.intArray[i4] = 0;
			}

		}

		public virtual void renderMap(EntityPlayer entityPlayer1, TextureManager renderEngine2, MapData mapData3)
		{
			for (int i4 = 0; i4 < 16384; ++i4)
			{
				sbyte b5 = mapData3.colors[i4];
				if (b5 / 4 == 0)
				{
					this.intArray[i4] = (i4 + i4 / 128 & 1) * 8 + 16 << 24;
				}
				else
				{
					int i6 = MapColor.mapColorArray[b5 / 4].colorValue;
					int i7 = b5 & 3;
					short s8 = 220;
					if (i7 == 2)
					{
						s8 = 255;
					}

					if (i7 == 0)
					{
						s8 = 180;
					}

					int i9 = (i6 >> 16 & 255) * s8 / 255;
					int i10 = (i6 >> 8 & 255) * s8 / 255;
					int i11 = (i6 & 255) * s8 / 255;

					this.intArray[i4] = unchecked((int)0xFF000000) | i9 << 16 | i10 << 8 | i11;
				}
			}

			renderEngine2.createTextureFromBytes(this.intArray, 128, 128, this.bufferedImage);
			sbyte b15 = 0;
			sbyte b16 = 0;
			Tessellator tessellator17 = Tessellator.instance;
			float f18 = 0.0F;
			GL.BindTexture(TextureTarget.Texture2D, this.bufferedImage);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
            tessellator17.startDrawingQuads();
			tessellator17.AddVertexWithUV((double)((float)(b15 + 0) + f18), (double)((float)(b16 + 128) - f18), -0.009999999776482582D, 0.0D, 1.0D);
			tessellator17.AddVertexWithUV((double)((float)(b15 + 128) - f18), (double)((float)(b16 + 128) - f18), -0.009999999776482582D, 1.0D, 1.0D);
			tessellator17.AddVertexWithUV((double)((float)(b15 + 128) - f18), (double)((float)(b16 + 0) + f18), -0.009999999776482582D, 1.0D, 0.0D);
			tessellator17.AddVertexWithUV((double)((float)(b15 + 0) + f18), (double)((float)(b16 + 0) + f18), -0.009999999776482582D, 0.0D, 0.0D);
			tessellator17.DrawImmediate();
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
            GL.Disable(EnableCap.Blend);
			renderEngine2.bindTexture(renderEngine2.getTexture("/misc/mapicons.png"));
			System.Collections.IEnumerator iterator19 = mapData3.playersVisibleOnMap.GetEnumerator();

			while (iterator19.MoveNext())
			{
				MapCoord mapCoord20 = (MapCoord)iterator19.Current;
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate((float)b15 + (float)mapCoord20.centerX / 2.0F + 64.0F, (float)b16 + (float)mapCoord20.centerZ / 2.0F + 64.0F, -0.02F);
                Minecraft.renderPipeline.ModelMatrix.Rotate((float)(mapCoord20.iconRotation * 360) / 16.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Scale(4.0F, 4.0F, 3.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(-0.125F, 0.125F, 0.0F);
				float f21 = (float)(mapCoord20.field_28217_a % 4 + 0) / 4.0F;
				float f22 = (float)(mapCoord20.field_28217_a / 4 + 0) / 4.0F;
				float f23 = (float)(mapCoord20.field_28217_a % 4 + 1) / 4.0F;
				float f24 = (float)(mapCoord20.field_28217_a / 4 + 1) / 4.0F;
				tessellator17.startDrawingQuads();
				tessellator17.AddVertexWithUV(-1.0D, 1.0D, 0.0D, (double)f21, (double)f22);
				tessellator17.AddVertexWithUV(1.0D, 1.0D, 0.0D, (double)f23, (double)f22);
				tessellator17.AddVertexWithUV(1.0D, -1.0D, 0.0D, (double)f23, (double)f24);
				tessellator17.AddVertexWithUV(-1.0D, -1.0D, 0.0D, (double)f21, (double)f24);
				tessellator17.DrawImmediate();
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			}

            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -0.04F);
            Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, 1.0F, 1.0F);
			fontRenderer.drawString(mapData3.mapName, b15, b16, unchecked((int)0xFF000000));
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
		}
	}

}