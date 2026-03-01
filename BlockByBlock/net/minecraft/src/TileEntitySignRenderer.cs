namespace net.minecraft.src
{
	using OpenTK.Graphics.OpenGL;
	
	public class TileEntitySignRenderer : TileEntitySpecialRenderer
	{
		private ModelSign modelSign = new ModelSign();

		public virtual void renderTileEntitySignAt(TileEntitySign tileEntitySign1, double d2, double d4, double d6, float f8)
		{
			Block block9 = tileEntitySign1.BlockType;
			GL.PushMatrix();
			float f10 = 0.6666667F;
			float f12;
			if (block9 == Block.signPost)
			{
				GL.Translate((float)d2 + 0.5F, (float)d4 + 0.75F * f10, (float)d6 + 0.5F);
				float f11 = (float)(tileEntitySign1.BlockMetadata * 360) / 16.0F;
				GL.Rotate(-f11, 0.0F, 1.0F, 0.0F);
				this.modelSign.signStick.showModel = true;
			}
			else
			{
				int i16 = tileEntitySign1.BlockMetadata;
				f12 = 0.0F;
				if (i16 == 2)
				{
					f12 = 180.0F;
				}

				if (i16 == 4)
				{
					f12 = 90.0F;
				}

				if (i16 == 5)
				{
					f12 = -90.0F;
				}

				GL.Translate((float)d2 + 0.5F, (float)d4 + 0.75F * f10, (float)d6 + 0.5F);
				GL.Rotate(-f12, 0.0F, 1.0F, 0.0F);
				GL.Translate(0.0F, -0.3125F, -0.4375F);
				this.modelSign.signStick.showModel = false;
			}

			this.bindTextureByName("/item/sign.png");
			GL.PushMatrix();
			GL.Scale(f10, -f10, -f10);
			this.modelSign.renderSign();
			GL.PopMatrix();
			FontRenderer fontRenderer17 = this.FontRenderer;
			f12 = 0.016666668F * f10;
			GL.Translate(0.0F, 0.5F * f10, 0.07F * f10);
			GL.Scale(f12, -f12, f12);
			GL.Normal3(0.0F, 0.0F, -1.0F * f12);
			GL.DepthMask(false);
			sbyte b13 = 0;

			for (int i14 = 0; i14 < tileEntitySign1.signText.Length; ++i14)
			{
				string string15 = tileEntitySign1.signText[i14];
				if (i14 == tileEntitySign1.lineBeingEdited)
				{
					string15 = "> " + string15 + " <";
					fontRenderer17.drawString(string15, -fontRenderer17.getStringWidth(string15) / 2, i14 * 10 - tileEntitySign1.signText.Length * 5, b13);
				}
				else
				{
					fontRenderer17.drawString(string15, -fontRenderer17.getStringWidth(string15) / 2, i14 * 10 - tileEntitySign1.signText.Length * 5, b13);
				}
			}

			GL.DepthMask(true);
			GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
			GL.PopMatrix();
		}

		public override void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			this.renderTileEntitySignAt((TileEntitySign)tileEntity1, d2, d4, d6, f8);
		}
	}

}