namespace net.minecraft.src
{
    using net.minecraft.client;
    using net.minecraft.client.entity.render.model;
    using OpenTK.Graphics.OpenGL;

    public class TileEntitySignRenderer : TileEntitySpecialRenderer
	{
		private ModelSign modelSign = new ModelSign();

		public virtual void renderTileEntitySignAt(TileEntitySign tileEntitySign1, double d2, double d4, double d6, float f8)
		{
			Block block9 = tileEntitySign1.BlockType;
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
			float f10 = 0.6666667F;
			float f12;
			if (block9 == Block.signPost)
			{
                Minecraft.renderPipeline.ModelMatrix.Translate((float)d2 + 0.5F, (float)d4 + 0.75F * f10, (float)d6 + 0.5F);
				float f11 = (float)(tileEntitySign1.BlockMetadata * 360) / 16.0F;
                Minecraft.renderPipeline.ModelMatrix.Rotate(-f11, 0.0F, 1.0F, 0.0F);
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

                Minecraft.renderPipeline.ModelMatrix.Translate((float)d2 + 0.5F, (float)d4 + 0.75F * f10, (float)d6 + 0.5F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-f12, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.3125F, -0.4375F);
				this.modelSign.signStick.showModel = false;
			}

			this.bindTextureByName("/item/sign.png");
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Scale(f10, -f10, -f10);
			this.modelSign.renderSign();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			FontRenderer fontRenderer17 = this.FontRenderer;
			f12 = 0.016666668F * f10;
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.5F * f10, 0.07F * f10);
            Minecraft.renderPipeline.ModelMatrix.Scale(f12, -f12, f12);
            Minecraft.renderPipeline.SetNormal(0.0F, 0.0F, -1.0F * f12);
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
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
		}

		public override void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			this.renderTileEntitySignAt((TileEntitySign)tileEntity1, d2, d4, d6, f8);
		}
	}

}