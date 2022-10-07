using System;
using BlockByBlock.java_extensions;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class RenderItem : Render
	{
		private new RenderBlocks renderBlocks = new RenderBlocks();
		private RandomExtended random = new RandomExtended();
		public bool field_27004_a = true;
		public float zLevel = 0.0F;

		public RenderItem()
		{
			this.shadowSize = 0.15F;
			this.shadowOpaque = 0.75F;
		}

		public virtual void doRenderItem(EntityItem entityItem1, double d2, double d4, double d6, float f8, float f9)
		{
			//this.random.setSeed(187L); // RandomExtended.setSeed
			random = new RandomExtended(187L);
			ItemStack itemStack10 = entityItem1.item;
			GL.PushMatrix();
			float f11 = MathHelper.sin(((float)entityItem1.age + f9) / 10.0F + entityItem1.field_804_d) * 0.1F + 0.1F;
			float f12 = (((float)entityItem1.age + f9) / 20.0F + entityItem1.field_804_d) * 57.295776F;
			sbyte b13 = 1;
			if (entityItem1.item.stackSize > 1)
			{
				b13 = 2;
			}

			if (entityItem1.item.stackSize > 5)
			{
				b13 = 3;
			}

			if (entityItem1.item.stackSize > 20)
			{
				b13 = 4;
			}

			GL.Translate((float)d2, (float)d4 + f11, (float)d6);
			GL.Enable(EnableCap.RescaleNormal);
			int i15;
			float f18;
			float f19;
			float f23;
			if (itemStack10.itemID < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[itemStack10.itemID].RenderType))
			{
				GL.Rotate(f12, 0.0F, 1.0F, 0.0F);
				this.loadTexture("/terrain.png");
				float f21 = 0.25F;
				i15 = Block.blocksList[itemStack10.itemID].RenderType;
				if (i15 == 1 || i15 == 19 || i15 == 12 || i15 == 2)
				{
					f21 = 0.5F;
				}
                
				GL.Scale(f21, f21, f21);

				for (int i22 = 0; i22 < b13; ++i22)
				{
					GL.PushMatrix();
					if (i22 > 0)
					{
						f23 = (this.random.NextSingle() * 2.0F - 1.0F) * 0.2F / f21;
						f18 = (this.random.NextSingle() * 2.0F - 1.0F) * 0.2F / f21;
						f19 = (this.random.NextSingle() * 2.0F - 1.0F) * 0.2F / f21;
						GL.Translate(f23, f18, f19);
					}

					f23 = 1.0F;
					this.renderBlocks.renderBlockAsItem(Block.blocksList[itemStack10.itemID], itemStack10.ItemDamage, f23);
					GL.PopMatrix();
				}
			}
			else
			{
				int i14;
				float f16;
				if (itemStack10.Item.func_46058_c())
				{
					GL.Scale(0.5F, 0.5F, 0.5F);
					this.loadTexture("/gui/items.png");

					for (i14 = 0; i14 <= 1; ++i14)
					{
						i15 = itemStack10.Item.func_46057_a(itemStack10.ItemDamage, i14);
						f16 = 1.0F;
						if (this.field_27004_a)
						{
							int i17 = Item.itemsList[itemStack10.itemID].getColorFromDamage(itemStack10.ItemDamage, i14);
							f18 = (float)(i17 >> 16 & 255) / 255.0F;
							f19 = (float)(i17 >> 8 & 255) / 255.0F;
							float f20 = (float)(i17 & 255) / 255.0F;
							GL.Color4(f18 * f16, f19 * f16, f20 * f16, 1.0F);
						}

						this.func_40267_a(i15, b13);
					}
				}
				else
				{
					GL.Scale(0.5F, 0.5F, 0.5F);
					i14 = itemStack10.IconIndex;
					if (itemStack10.itemID < 256)
					{
						this.loadTexture("/terrain.png");
					}
					else
					{
						this.loadTexture("/gui/items.png");
					}

					if (this.field_27004_a)
					{
						i15 = Item.itemsList[itemStack10.itemID].getColorFromDamage(itemStack10.ItemDamage, 0);
						f16 = (float)(i15 >> 16 & 255) / 255.0F;
						f23 = (float)(i15 >> 8 & 255) / 255.0F;
						f18 = (float)(i15 & 255) / 255.0F;
						f19 = 1.0F;
						GL.Color4(f16 * f19, f23 * f19, f18 * f19, 1.0F);
					}

					this.func_40267_a(i14, b13);
				}
			}

			GL.Disable(EnableCap.RescaleNormal);
			GL.PopMatrix();
		}

		private void func_40267_a(int i1, int i2)
		{
			Tessellator tessellator3 = Tessellator.instance;
			float f4 = (float)(i1 % 16 * 16 + 0) / 256.0F;
			float f5 = (float)(i1 % 16 * 16 + 16) / 256.0F;
			float f6 = (float)(i1 / 16 * 16 + 0) / 256.0F;
			float f7 = (float)(i1 / 16 * 16 + 16) / 256.0F;
			float f8 = 1.0F;
			float f9 = 0.5F;
			float f10 = 0.25F;

			for (int i11 = 0; i11 < i2; ++i11)
			{
				GL.PushMatrix();
				if (i11 > 0)
				{
					float f12 = (this.random.NextSingle() * 2.0F - 1.0F) * 0.3F;
					float f13 = (this.random.NextSingle() * 2.0F - 1.0F) * 0.3F;
					float f14 = (this.random.NextSingle() * 2.0F - 1.0F) * 0.3F;
					GL.Translate(f12, f13, f14);
				}

				GL.Rotate(180.0F - this.renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
				tessellator3.startDrawingQuads();
				tessellator3.setNormal(0.0F, 1.0F, 0.0F);
				tessellator3.addVertexWithUV((double)(0.0F - f9), (double)(0.0F - f10), 0.0D, (double)f4, (double)f7);
				tessellator3.addVertexWithUV((double)(f8 - f9), (double)(0.0F - f10), 0.0D, (double)f5, (double)f7);
				tessellator3.addVertexWithUV((double)(f8 - f9), (double)(1.0F - f10), 0.0D, (double)f5, (double)f6);
				tessellator3.addVertexWithUV((double)(0.0F - f9), (double)(1.0F - f10), 0.0D, (double)f4, (double)f6);
				tessellator3.draw();
				GL.PopMatrix();
			}

		}

		public virtual void drawItemIntoGui(FontRenderer fontRenderer1, RenderEngine renderEngine2, int i3, int i4, int i5, int i6, int i7)
		{
			int i10;
			float f11;
			float f12;
			float f13;
			if (i3 < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[i3].RenderType))
			{
				renderEngine2.bindTexture(renderEngine2.getTexture("/terrain.png"));
				Block block15 = Block.blocksList[i3];
				GL.PushMatrix();
				GL.Translate((float)(i6 - 2), (float)(i7 + 3), -3.0F + this.zLevel);
				GL.Scale(10.0F, 10.0F, 10.0F);
				GL.Translate(1.0F, 0.5F, 1.0F);
				GL.Scale(1.0F, 1.0F, -1.0F);
				GL.Rotate(210.0F, 1.0F, 0.0F, 0.0F);
				GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
				i10 = Item.itemsList[i3].getColorFromDamage(i4, 0);
				f11 = (float)(i10 >> 16 & 255) / 255.0F;
				f12 = (float)(i10 >> 8 & 255) / 255.0F;
				f13 = (float)(i10 & 255) / 255.0F;
				if (this.field_27004_a)
				{
					GL.Color4(f11, f12, f13, 1.0F);
				}

				GL.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
				this.renderBlocks.useInventoryTint = this.field_27004_a;
				this.renderBlocks.renderBlockAsItem(block15, i4, 1.0F);
				this.renderBlocks.useInventoryTint = true;
				GL.PopMatrix();
			}
			else
			{
				int i8;
				if (Item.itemsList[i3].func_46058_c())
				{
					GL.Disable(EnableCap.Lighting);
					renderEngine2.bindTexture(renderEngine2.getTexture("/gui/items.png"));

					for (i8 = 0; i8 <= 1; ++i8)
					{
						int i9 = Item.itemsList[i3].func_46057_a(i4, i8);
						i10 = Item.itemsList[i3].getColorFromDamage(i4, i8);
						f11 = (float)(i10 >> 16 & 255) / 255.0F;
						f12 = (float)(i10 >> 8 & 255) / 255.0F;
						f13 = (float)(i10 & 255) / 255.0F;
						if (this.field_27004_a)
						{
							GL.Color4(f11, f12, f13, 1.0F);
						}

						this.renderTexturedQuad(i6, i7, i9 % 16 * 16, i9 / 16 * 16, 16, 16);
					}

					GL.Enable(EnableCap.Lighting);
				}
				else if (i5 >= 0)
				{
					GL.Disable(EnableCap.Lighting);
					if (i3 < 256)
					{
						renderEngine2.bindTexture(renderEngine2.getTexture("/terrain.png"));
					}
					else
					{
						renderEngine2.bindTexture(renderEngine2.getTexture("/gui/items.png"));
					}

					i8 = Item.itemsList[i3].getColorFromDamage(i4, 0);
					float f14 = (float)(i8 >> 16 & 255) / 255.0F;
					float f16 = (float)(i8 >> 8 & 255) / 255.0F;
					f11 = (float)(i8 & 255) / 255.0F;
					if (this.field_27004_a)
					{
						GL.Color4(f14, f16, f11, 1.0F);
					}

					this.renderTexturedQuad(i6, i7, i5 % 16 * 16, i5 / 16 * 16, 16, 16);
					GL.Enable(EnableCap.Lighting);
				}
			}

			GL.Enable(EnableCap.CullFace);
		}

		public virtual void renderItemIntoGUI(FontRenderer fontRenderer1, RenderEngine renderEngine2, ItemStack itemStack3, int i4, int i5)
		{
			if (itemStack3 != null)
			{
				this.drawItemIntoGui(fontRenderer1, renderEngine2, itemStack3.itemID, itemStack3.ItemDamage, itemStack3.IconIndex, i4, i5);
				if (itemStack3 != null && itemStack3.hasEffect())
				{
					GL.DepthFunc(DepthFunction.Greater);
					GL.Disable(EnableCap.Lighting);
					GL.DepthMask(false);
					renderEngine2.bindTexture(renderEngine2.getTexture("%blur%/misc/glint.png"));
					this.zLevel -= 50.0F;
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.DstColor, BlendingFactor.DstColor);
					GL.Color4(0.5F, 0.25F, 0.8F, 1.0F);
					this.func_40266_a(i4 * 431278612 + i5 * 32178161, i4 - 2, i5 - 2, 20, 20);
					GL.Disable(EnableCap.Blend);
					GL.DepthMask(true);
					this.zLevel += 50.0F;
					GL.Enable(EnableCap.Lighting);
					GL.DepthFunc(DepthFunction.Lequal);
				}

			}
		}

		private void func_40266_a(int i1, int i2, int i3, int i4, int i5)
		{
			for (int i6 = 0; i6 < 2; ++i6)
			{
				if (i6 == 0)
				{
					GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
				}

				if (i6 == 1)
				{
					GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
				}

				float f7 = 0.00390625F;
				float f8 = 0.00390625F;
				float f9 = (float)(DateTimeHelper.CurrentUnixTimeMillis() % (long)(3000 + i6 * 1873)) / (3000.0F + (float)(i6 * 1873)) * 256.0F;
				float f10 = 0.0F;
				Tessellator tessellator11 = Tessellator.instance;
				float f12 = 4.0F;
				if (i6 == 1)
				{
					f12 = -1.0F;
				}

				tessellator11.startDrawingQuads();
				tessellator11.addVertexWithUV((double)(i2 + 0), (double)(i3 + i5), (double)this.zLevel, (double)((f9 + (float)i5 * f12) * f7), (double)((f10 + (float)i5) * f8));
				tessellator11.addVertexWithUV((double)(i2 + i4), (double)(i3 + i5), (double)this.zLevel, (double)((f9 + (float)i4 + (float)i5 * f12) * f7), (double)((f10 + (float)i5) * f8));
				tessellator11.addVertexWithUV((double)(i2 + i4), (double)(i3 + 0), (double)this.zLevel, (double)((f9 + (float)i4) * f7), (double)((f10 + 0.0F) * f8));
				tessellator11.addVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)this.zLevel, (double)((f9 + 0.0F) * f7), (double)((f10 + 0.0F) * f8));
				tessellator11.draw();
			}

		}

		public virtual void renderItemOverlayIntoGUI(FontRenderer fontRenderer1, RenderEngine renderEngine2, ItemStack itemStack3, int i4, int i5)
		{
			if (itemStack3 != null)
			{
				if (itemStack3.stackSize > 1)
				{
					string string6 = "" + itemStack3.stackSize;
					GL.Disable(EnableCap.Lighting);
					GL.Disable(EnableCap.DepthTest);
					fontRenderer1.drawStringWithShadow(string6, i4 + 19 - 2 - fontRenderer1.getStringWidth(string6), i5 + 6 + 3, 0xFFFFFF);
					GL.Enable(EnableCap.Lighting);
					GL.Enable(EnableCap.DepthTest);
				}

				if (itemStack3.ItemDamaged)
				{
					int i11 = (int)(long)Math.Round(13.0D - (double)itemStack3.ItemDamageForDisplay * 13.0D / (double)itemStack3.MaxDamage, MidpointRounding.AwayFromZero);
					int i7 = (int)(long)Math.Round(255.0D - (double)itemStack3.ItemDamageForDisplay * 255.0D / (double)itemStack3.MaxDamage, MidpointRounding.AwayFromZero);
					GL.Disable(EnableCap.Lighting);
					GL.Disable(EnableCap.DepthTest);
					GL.Disable(EnableCap.Texture2D);
					Tessellator tessellator8 = Tessellator.instance;
					int i9 = 255 - i7 << 16 | i7 << 8;
					int i10 = (255 - i7) / 4 << 16 | 16128;
					this.renderQuad(tessellator8, i4 + 2, i5 + 13, 13, 2, 0);
					this.renderQuad(tessellator8, i4 + 2, i5 + 13, 12, 1, i10);
					this.renderQuad(tessellator8, i4 + 2, i5 + 13, i11, 1, i9);
					GL.Enable(EnableCap.Texture2D);
					GL.Enable(EnableCap.Lighting);
					GL.Enable(EnableCap.DepthTest);
					GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				}

			}
		}

		private void renderQuad(Tessellator tessellator1, int i2, int i3, int i4, int i5, int i6)
		{
			tessellator1.startDrawingQuads();
			tessellator1.ColorOpaque_I = i6;
			tessellator1.addVertex((double)(i2 + 0), (double)(i3 + 0), 0.0D);
			tessellator1.addVertex((double)(i2 + 0), (double)(i3 + i5), 0.0D);
			tessellator1.addVertex((double)(i2 + i4), (double)(i3 + i5), 0.0D);
			tessellator1.addVertex((double)(i2 + i4), (double)(i3 + 0), 0.0D);
			tessellator1.draw();
		}

		public virtual void renderTexturedQuad(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			float f7 = 0.00390625F;
			float f8 = 0.00390625F;
			Tessellator tessellator9 = Tessellator.instance;
			tessellator9.startDrawingQuads();
			tessellator9.addVertexWithUV((double)(i1 + 0), (double)(i2 + i6), (double)this.zLevel, (double)((float)(i3 + 0) * f7), (double)((float)(i4 + i6) * f8));
			tessellator9.addVertexWithUV((double)(i1 + i5), (double)(i2 + i6), (double)this.zLevel, (double)((float)(i3 + i5) * f7), (double)((float)(i4 + i6) * f8));
			tessellator9.addVertexWithUV((double)(i1 + i5), (double)(i2 + 0), (double)this.zLevel, (double)((float)(i3 + i5) * f7), (double)((float)(i4 + 0) * f8));
			tessellator9.addVertexWithUV((double)(i1 + 0), (double)(i2 + 0), (double)this.zLevel, (double)((float)(i3 + 0) * f7), (double)((float)(i4 + 0) * f8));
			tessellator9.draw();
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.doRenderItem((EntityItem)entity1, d2, d4, d6, f8, f9);
		}
	}

}