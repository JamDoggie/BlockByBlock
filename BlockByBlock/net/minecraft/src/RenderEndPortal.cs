using System;
using BlockByBlock.java_extensions;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	public class RenderEndPortal : TileEntitySpecialRenderer
	{
		internal float[] floatBuf = new float[16];

		public virtual void func_40446_a(TileEntityEndPortal tileEntityEndPortal1, double d2, double d4, double d6, float f8)
		{
			float f9 = (float)tileEntityRenderer.playerX;
			float f10 = (float)tileEntityRenderer.playerY;
			float f11 = (float)tileEntityRenderer.playerZ;
			GL.Disable(EnableCap.Lighting);
			RandomExtended random12 = new RandomExtended(31100L);
			float f13 = 0.75F;

			for (int i14 = 0; i14 < 16; ++i14)
			{
				GL.PushMatrix();
				float f15 = (float)(16 - i14);
				float f16 = 0.0625F;
				float f17 = 1.0F / (f15 + 1.0F);
				if (i14 == 0)
				{
					bindTextureByName("/misc/tunnel.png");
					f17 = 0.1F;
					f15 = 65.0F;
					f16 = 0.125F;
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				}

				if (i14 == 1)
				{
					bindTextureByName("/misc/particlefield.png");
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
					f16 = 0.5F;
				}

				float f18 = (float)(-(d4 + (double)f13));
				float f19 = f18 + ActiveRenderInfo.objectY;
				float f20 = f18 + f15 + ActiveRenderInfo.objectY;
				float f21 = f19 / f20;
				f21 += (float)(d4 + (double)f13);
				GL.Translate(f9, f21, f11);
                GL.TexGen(TextureCoordName.S, TextureGenParameter.TextureGenMode, (int)All.ObjectLinear);
				GL.TexGen(TextureCoordName.T, TextureGenParameter.TextureGenMode, (int)All.ObjectLinear);
				GL.TexGen(TextureCoordName.R, TextureGenParameter.TextureGenMode, (int)All.ObjectLinear);
				GL.TexGen(TextureCoordName.Q, TextureGenParameter.TextureGenMode, (int)All.EyeLinear);
				GL.TexGen(TextureCoordName.S, TextureGenParameter.ObjectPlane, func_40447_a(1.0F, 0.0F, 0.0F, 0.0F));
				GL.TexGen(TextureCoordName.T, TextureGenParameter.ObjectPlane, func_40447_a(0.0F, 0.0F, 1.0F, 0.0F));
				GL.TexGen(TextureCoordName.R, TextureGenParameter.ObjectPlane, func_40447_a(0.0F, 0.0F, 0.0F, 1.0F));
				GL.TexGen(TextureCoordName.Q, TextureGenParameter.EyePlane, func_40447_a(0.0F, 1.0F, 0.0F, 0.0F));

				GL.Enable(EnableCap.TextureGenS);
				GL.Enable(EnableCap.TextureGenT);
				GL.Enable(EnableCap.TextureGenR);
				GL.Enable(EnableCap.TextureGenQ);
				GL.PopMatrix();
				GL.MatrixMode(MatrixMode.Texture);
				GL.PushMatrix();
				GL.LoadIdentity();
				GL.Translate(0.0F, (float)(DateTimeHelper.CurrentUnixTimeMillis() % 700000L) / 700000.0F, 0.0F);
				GL.Scale(f16, f16, f16);
				GL.Translate(0.5F, 0.5F, 0.0F);
				GL.Rotate((float)(i14 * i14 * 4321 + i14 * 9) * 2.0F, 0.0F, 0.0F, 1.0F);
				GL.Translate(-0.5F, -0.5F, 0.0F);
				GL.Translate(-f9, -f11, -f10);
				f19 = f18 + ActiveRenderInfo.objectY;
				GL.Translate(ActiveRenderInfo.objectX * f15 / f19, ActiveRenderInfo.objectZ * f15 / f19, -f10);
				Tessellator tessellator24 = Tessellator.instance;
				tessellator24.startDrawingQuads();
				f21 = random12.NextSingle() * 0.5F + 0.1F;
				float f22 = random12.NextSingle() * 0.5F + 0.4F;
				float f23 = random12.NextSingle() * 0.5F + 0.5F;
				if (i14 == 0)
				{
					f23 = 1.0F;
					f22 = 1.0F;
					f21 = 1.0F;
				}

				tessellator24.setColorRGBA_F(f21 * f17, f22 * f17, f23 * f17, 1.0F);
				tessellator24.addVertex(d2, d4 + (double)f13, d6);
				tessellator24.addVertex(d2, d4 + (double)f13, d6 + 1.0D);
				tessellator24.addVertex(d2 + 1.0D, d4 + (double)f13, d6 + 1.0D);
				tessellator24.addVertex(d2 + 1.0D, d4 + (double)f13, d6);
				tessellator24.draw();
				GL.PopMatrix();
				GL.MatrixMode(MatrixMode.Modelview);
			}

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.TextureGenS);
			GL.Disable(EnableCap.TextureGenT);
			GL.Disable(EnableCap.TextureGenR);
			GL.Disable(EnableCap.TextureGenQ);
			GL.Enable(EnableCap.Lighting);
		}

		private float[] func_40447_a(float f1, float f2, float f3, float f4)
		{
			floatBuf[0] = f1;
			floatBuf[1] = f2;
			floatBuf[2] = f3;
			floatBuf[3] = f4;

			return floatBuf;
		}

		public override void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			func_40446_a((TileEntityEndPortal)tileEntity1, d2, d4, d6, f8);
		}
	}

}