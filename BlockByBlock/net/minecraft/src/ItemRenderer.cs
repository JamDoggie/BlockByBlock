using BlockByBlock.net.minecraft.render;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
    using Minecraft = net.minecraft.client.Minecraft;

    public class ItemRenderer
	{
		private Minecraft mc;
		private ItemStack itemToRender = null;
		private float equippedProgress = 0.0F;
		private float prevEquippedProgress = 0.0F;
		private RenderBlocks renderBlocksInstance = new RenderBlocks();
		private MapItemRenderer mapItemRenderer;
		private int equippedItemSlot = -1;

		public ItemRenderer(Minecraft minecraft1)
		{
			this.mc = minecraft1;
			this.mapItemRenderer = new MapItemRenderer(minecraft1.fontRenderer, minecraft1.gameSettings, minecraft1.renderEngine);
		}

		public virtual void renderItem(EntityLiving entityLiving1, ItemStack itemStack2, int i3)
		{
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
			if (itemStack2.itemID < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[itemStack2.itemID].RenderType))
			{
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/terrain.png"));
				this.renderBlocksInstance.renderBlockAsItem(Block.blocksList[itemStack2.itemID], itemStack2.ItemDamage, 1.0F);
			}
			else
			{
				if (itemStack2.itemID < 256)
				{
					GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/terrain.png"));
				}
				else
				{
					GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/gui/items.png"));
				}

				Tessellator tessellator4 = Tessellator.instance;
				int i5 = entityLiving1.getItemIcon(itemStack2, i3);
				float f6 = ((float)(i5 % 16 * 16) + 0.0F) / 256.0F;
				float f7 = ((float)(i5 % 16 * 16) + 15.99F) / 256.0F;
				float f8 = ((float)(i5 / 16 * 16) + 0.0F) / 256.0F;
				float f9 = ((float)(i5 / 16 * 16) + 15.99F) / 256.0F;
				float f10 = 0.0F;
				float f11 = 0.3F;
                Minecraft.renderPipeline.ModelMatrix.Translate(-f10, -f11, 0.0F);
				float f12 = 1.5F;
				Minecraft.renderPipeline.ModelMatrix.Scale(f12, f12, f12);
				Minecraft.renderPipeline.ModelMatrix.Rotate(50.0F, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(335.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(-0.9375F, -0.0625F, 0.0F);
				this.renderItemIn2D(tessellator4, f7, f8, f6, f9);
				if (itemStack2 != null && itemStack2.hasEffect() && i3 == 0)
				{
					GL.DepthFunc(DepthFunction.Equal);
					Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
					this.mc.renderEngine.bindTexture(this.mc.renderEngine.getTexture("%blur%/misc/glint.png"));
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
					float f13 = 0.76F;
                    Minecraft.renderPipeline.SetColor(0.5F * f13, 0.25F * f13, 0.8F * f13, 1.0F);
                    Minecraft.renderPipeline.TextureMatrix.PushMatrix();
					float f14 = 0.125F;
                    Minecraft.renderPipeline.TextureMatrix.Scale(f14, f14, f14);
					float f15 = (float)(DateTimeHelper.CurrentUnixTimeMillis() % 3000L) / 3000.0F * 8.0F;
                    Minecraft.renderPipeline.TextureMatrix.Translate(f15, 0.0F, 0.0F);
                    Minecraft.renderPipeline.TextureMatrix.Rotate(-50.0F, 0.0F, 0.0F, 1.0F);
					this.renderItemIn2D(tessellator4, 0.0F, 0.0F, 1.0F, 1.0F);
                    Minecraft.renderPipeline.TextureMatrix.PopMatrix();
                    Minecraft.renderPipeline.TextureMatrix.PushMatrix();
                    Minecraft.renderPipeline.TextureMatrix.Scale(f14, f14, f14);
					f15 = (float)(DateTimeHelper.CurrentUnixTimeMillis() % 4873L) / 4873.0F * 8.0F;
                    Minecraft.renderPipeline.TextureMatrix.Translate(-f15, 0.0F, 0.0F);
                    Minecraft.renderPipeline.TextureMatrix.Rotate(10.0F, 0.0F, 0.0F, 1.0F);
					this.renderItemIn2D(tessellator4, 0.0F, 0.0F, 1.0F, 1.0F);
                    Minecraft.renderPipeline.TextureMatrix.PopMatrix();
					GL.Disable(EnableCap.Blend);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                    GL.DepthFunc(DepthFunction.Lequal);
				}
			}

            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
		}

		private void renderItemIn2D(Tessellator tessellator1, float f2, float f3, float f4, float f5)
		{
			float f6 = 1.0F;
			float f7 = 0.0625F;
			tessellator1.startDrawingQuads();
			tessellator1.SetNormal(0.0F, 0.0F, 1.0F);
			tessellator1.AddVertexWithUV(0.0D, 0.0D, 0.0D, (double)f2, (double)f5);
			tessellator1.AddVertexWithUV((double)f6, 0.0D, 0.0D, (double)f4, (double)f5);
			tessellator1.AddVertexWithUV((double)f6, 1.0D, 0.0D, (double)f4, (double)f3);
			tessellator1.AddVertexWithUV(0.0D, 1.0D, 0.0D, (double)f2, (double)f3);
			tessellator1.DrawImmediate();
			tessellator1.startDrawingQuads();
			tessellator1.SetNormal(0.0F, 0.0F, -1.0F);
			tessellator1.AddVertexWithUV(0.0D, 1.0D, (double)(0.0F - f7), (double)f2, (double)f3);
			tessellator1.AddVertexWithUV((double)f6, 1.0D, (double)(0.0F - f7), (double)f4, (double)f3);
			tessellator1.AddVertexWithUV((double)f6, 0.0D, (double)(0.0F - f7), (double)f4, (double)f5);
			tessellator1.AddVertexWithUV(0.0D, 0.0D, (double)(0.0F - f7), (double)f2, (double)f5);
			tessellator1.DrawImmediate();
			tessellator1.startDrawingQuads();
			tessellator1.SetNormal(-1.0F, 0.0F, 0.0F);

			int i8;
			float f9;
			float f10;
			float f11;
			for (i8 = 0; i8 < 16; ++i8)
			{
				f9 = (float)i8 / 16.0F;
				f10 = f2 + (f4 - f2) * f9 - 0.001953125F;
				f11 = f6 * f9;
				tessellator1.AddVertexWithUV((double)f11, 0.0D, (double)(0.0F - f7), (double)f10, (double)f5);
				tessellator1.AddVertexWithUV((double)f11, 0.0D, 0.0D, (double)f10, (double)f5);
				tessellator1.AddVertexWithUV((double)f11, 1.0D, 0.0D, (double)f10, (double)f3);
				tessellator1.AddVertexWithUV((double)f11, 1.0D, (double)(0.0F - f7), (double)f10, (double)f3);
			}

			tessellator1.DrawImmediate();
			tessellator1.startDrawingQuads();
			tessellator1.SetNormal(1.0F, 0.0F, 0.0F);

			for (i8 = 0; i8 < 16; ++i8)
			{
				f9 = (float)i8 / 16.0F;
				f10 = f2 + (f4 - f2) * f9 - 0.001953125F;
				f11 = f6 * f9 + 0.0625F;
				tessellator1.AddVertexWithUV((double)f11, 1.0D, (double)(0.0F - f7), (double)f10, (double)f3);
				tessellator1.AddVertexWithUV((double)f11, 1.0D, 0.0D, (double)f10, (double)f3);
				tessellator1.AddVertexWithUV((double)f11, 0.0D, 0.0D, (double)f10, (double)f5);
				tessellator1.AddVertexWithUV((double)f11, 0.0D, (double)(0.0F - f7), (double)f10, (double)f5);
			}

			tessellator1.DrawImmediate();
			tessellator1.startDrawingQuads();
			tessellator1.SetNormal(0.0F, 1.0F, 0.0F);

			for (i8 = 0; i8 < 16; ++i8)
			{
				f9 = (float)i8 / 16.0F;
				f10 = f5 + (f3 - f5) * f9 - 0.001953125F;
				f11 = f6 * f9 + 0.0625F;
				tessellator1.AddVertexWithUV(0.0D, (double)f11, 0.0D, (double)f2, (double)f10);
				tessellator1.AddVertexWithUV((double)f6, (double)f11, 0.0D, (double)f4, (double)f10);
				tessellator1.AddVertexWithUV((double)f6, (double)f11, (double)(0.0F - f7), (double)f4, (double)f10);
				tessellator1.AddVertexWithUV(0.0D, (double)f11, (double)(0.0F - f7), (double)f2, (double)f10);
			}

			tessellator1.DrawImmediate();
			tessellator1.startDrawingQuads();
			tessellator1.SetNormal(0.0F, -1.0F, 0.0F);

			for (i8 = 0; i8 < 16; ++i8)
			{
				f9 = (float)i8 / 16.0F;
				f10 = f5 + (f3 - f5) * f9 - 0.001953125F;
				f11 = f6 * f9;
				tessellator1.AddVertexWithUV((double)f6, (double)f11, 0.0D, (double)f4, (double)f10);
				tessellator1.AddVertexWithUV(0.0D, (double)f11, 0.0D, (double)f2, (double)f10);
				tessellator1.AddVertexWithUV(0.0D, (double)f11, (double)(0.0F - f7), (double)f2, (double)f10);
				tessellator1.AddVertexWithUV((double)f6, (double)f11, (double)(0.0F - f7), (double)f4, (double)f10);
			}

			tessellator1.DrawImmediate();
		}

		public virtual void renderItemInFirstPerson(float f1)
		{
			float f2 = this.prevEquippedProgress + (this.equippedProgress - this.prevEquippedProgress) * f1;
			EntityPlayerSP entityPlayerSP3 = this.mc.thePlayer;
			float f4 = entityPlayerSP3.prevRotationPitch + (entityPlayerSP3.rotationPitch - entityPlayerSP3.prevRotationPitch) * f1;
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Rotate(f4, 1.0F, 0.0F, 0.0F);
            Minecraft.renderPipeline.ModelMatrix.Rotate(entityPlayerSP3.prevRotationYaw + (entityPlayerSP3.rotationYaw - entityPlayerSP3.prevRotationYaw) * f1, 0.0F, 1.0F, 0.0F);
			GameLighting.EnableMeshLighting();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			float f6;
			float f7;
			if (entityPlayerSP3 is EntityPlayerSP)
			{
				EntityPlayerSP entityPlayerSP5 = (EntityPlayerSP)entityPlayerSP3;
				f6 = entityPlayerSP5.prevRenderArmPitch + (entityPlayerSP5.renderArmPitch - entityPlayerSP5.prevRenderArmPitch) * f1;
				f7 = entityPlayerSP5.prevRenderArmYaw + (entityPlayerSP5.renderArmYaw - entityPlayerSP5.prevRenderArmYaw) * f1;
                Minecraft.renderPipeline.ModelMatrix.Rotate((entityPlayerSP3.rotationPitch - f6) * 0.1F, 1.0F, 0.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate((entityPlayerSP3.rotationYaw - f7) * 0.1F, 0.0F, 1.0F, 0.0F);
			}

			ItemStack itemStack14 = this.itemToRender;
			f6 = this.mc.theWorld.getLightBrightness(MathHelper.floor_double(entityPlayerSP3.posX), MathHelper.floor_double(entityPlayerSP3.posY), MathHelper.floor_double(entityPlayerSP3.posZ));
			f6 = 1.0F;
			int i15 = this.mc.theWorld.GetLightBrightnessForSkyBlocks(MathHelper.floor_double(entityPlayerSP3.posX), MathHelper.floor_double(entityPlayerSP3.posY), MathHelper.floor_double(entityPlayerSP3.posZ), 0);
			int i8 = i15 % 65536;
			int i9 = i15 / 65536;
			LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, (float)i8 / 1.0F, (float)i9 / 1.0F);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			float f10;
			float f16;
			float f18;
			if (itemStack14 != null)
			{
				i15 = Item.itemsList[itemStack14.itemID].getColorFromDamage(itemStack14.ItemDamage, 0);
				f16 = (float)(i15 >> 16 & 255) / 255.0F;
				f18 = (float)(i15 >> 8 & 255) / 255.0F;
				f10 = (float)(i15 & 255) / 255.0F;
                Minecraft.renderPipeline.SetColor(f6 * f16, f6 * f18, f6 * f10, 1.0F);
			}
			else
			{
                Minecraft.renderPipeline.SetColor(f6, f6, f6, 1.0F);
			}

			float f11;
			float f13;
			if (itemStack14 != null && itemStack14.itemID == Item.map.shiftedIndex)
			{
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				f7 = 0.8F;
				f16 = entityPlayerSP3.getSwingProgress(f1);
				f18 = MathHelper.sin(f16 * (float)Math.PI);
				f10 = MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI);
                Minecraft.renderPipeline.ModelMatrix.Translate(-f10 * 0.4F, MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI * 2.0F) * 0.2F, -f18 * 0.2F);
				f16 = 1.0F - f4 / 45.0F + 0.1F;
				if (f16 < 0.0F)
				{
					f16 = 0.0F;
				}
                
				if (f16 > 1.0F)
				{
					f16 = 1.0F;
				}

				f16 = -MathHelper.cos(f16 * (float)Math.PI) * 0.5F + 0.5F;
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F * f7 - (1.0F - f2) * 1.2F - f16 * 0.5F + 0.04F, -0.9F * f7);
                Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(f16 * -85.0F, 0.0F, 0.0F, 1.0F);
                
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTextureForDownloadableImage(this.mc.thePlayer.skinUrl, this.mc.thePlayer.Texture));

				for (i9 = 0; i9 < 2; ++i9)
				{
					int i24 = i9 * 2 - 1;
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Translate(-0.0F, -0.6F, 1.1F * (float)i24);
                    Minecraft.renderPipeline.ModelMatrix.Rotate((float)(-45 * i24), 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 0.0F, 1.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(59.0F, 0.0F, 0.0F, 1.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate((float)(-65 * i24), 0.0F, 1.0F, 0.0F);
					Renderer render22 = RenderManager.instance.getEntityRenderObject(this.mc.thePlayer);
					RenderPlayer renderPlayer26 = (RenderPlayer)render22;
					f13 = 1.0F;
                    Minecraft.renderPipeline.ModelMatrix.Scale(f13, f13, f13);
					renderPlayer26.drawFirstPersonHand();
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}

				f18 = entityPlayerSP3.getSwingProgress(f1);
				f10 = MathHelper.sin(f18 * f18 * (float)Math.PI);
				f11 = MathHelper.sin(MathHelper.sqrt_float(f18) * (float)Math.PI);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f10 * 20.0F, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f11 * 20.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-f11 * 80.0F, 1.0F, 0.0F, 0.0F);
				f18 = 0.38F;
				Minecraft.renderPipeline.ModelMatrix.Scale(f18, f18, f18);
				Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(-1.0F, -1.0F, 0.0F);
				f10 = 0.015625F;
                Minecraft.renderPipeline.ModelMatrix.Scale(f10, f10, f10);
				this.mc.renderEngine.bindTexture(this.mc.renderEngine.getTexture("/misc/mapbg.png"));
				Tessellator tessellator23 = Tessellator.instance;
				Minecraft.renderPipeline.SetNormal(0.0F, 0.0F, -1.0F);
                tessellator23.startDrawingQuads();
				sbyte b27 = 7;
				tessellator23.AddVertexWithUV((double)(0 - b27), (double)(128 + b27), 0.0D, 0.0D, 1.0D);
				tessellator23.AddVertexWithUV((double)(128 + b27), (double)(128 + b27), 0.0D, 1.0D, 1.0D);
				tessellator23.AddVertexWithUV((double)(128 + b27), (double)(0 - b27), 0.0D, 1.0D, 0.0D);
				tessellator23.AddVertexWithUV((double)(0 - b27), (double)(0 - b27), 0.0D, 0.0D, 0.0D);
				tessellator23.DrawImmediate();
				MapData mapData25 = Item.map.getMapData(itemStack14, this.mc.theWorld);
				this.mapItemRenderer.renderMap(this.mc.thePlayer, this.mc.renderEngine, mapData25);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			}
			else if (itemStack14 != null)
			{
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				f7 = 0.8F;
				float f12;
				if (entityPlayerSP3.ItemInUseCount > 0)
				{
					EnumAction enumAction17 = itemStack14.ItemUseAction;
					if (enumAction17 == EnumAction.eat || enumAction17 == EnumAction.drink)
					{
						f18 = (float)entityPlayerSP3.ItemInUseCount - f1 + 1.0F;
						f10 = 1.0F - f18 / (float)itemStack14.MaxItemUseDuration;
						f12 = 1.0F - f10;
						f12 = f12 * f12 * f12;
						f12 = f12 * f12 * f12;
						f12 = f12 * f12 * f12;
						f13 = 1.0F - f12;
						Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, MathHelper.abs(MathHelper.cos(f18 / 4.0F * (float)Math.PI) * 0.1F) * (float)((double)f10 > 0.2D ? 1 : 0), 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Translate(f13 * 0.6F, -f13 * 0.5F, 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(f13 * 90.0F, 0.0F, 1.0F, 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(f13 * 10.0F, 1.0F, 0.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(f13 * 30.0F, 0.0F, 0.0F, 1.0F);
					}
				}
				else
				{
					f16 = entityPlayerSP3.getSwingProgress(f1);
					f18 = MathHelper.sin(f16 * (float)Math.PI);
					f10 = MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI);
                    Minecraft.renderPipeline.ModelMatrix.Translate(-f10 * 0.4F, MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI * 2.0F) * 0.2F, -f18 * 0.2F);
				}

				Minecraft.renderPipeline.ModelMatrix.Translate(0.7F * f7, -0.65F * f7 - (1.0F - f2) * 0.6F, -0.9F * f7);
                Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                
				f16 = entityPlayerSP3.getSwingProgress(f1);
				f18 = MathHelper.sin(f16 * f16 * (float)Math.PI);
				f10 = MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f18 * 20.0F, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f10 * 20.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-f10 * 80.0F, 1.0F, 0.0F, 0.0F);
				f16 = 0.4F;
                Minecraft.renderPipeline.ModelMatrix.Scale(f16, f16, f16);
				if (entityPlayerSP3.ItemInUseCount > 0)
				{
					EnumAction enumAction20 = itemStack14.ItemUseAction;
					if (enumAction20 == EnumAction.block)
					{
						Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, 0.2F, 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(30.0F, 0.0F, 1.0F, 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(-80.0F, 1.0F, 0.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(60.0F, 0.0F, 1.0F, 0.0F);
					}
					else if (enumAction20 == EnumAction.bow)
					{
						Minecraft.renderPipeline.ModelMatrix.Rotate(-18.0F, 0.0F, 0.0F, 1.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(-12.0F, 0.0F, 1.0F, 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(-8.0F, 1.0F, 0.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Translate(-0.9F, 0.2F, 0.0F);
						f10 = (float)itemStack14.MaxItemUseDuration - ((float)entityPlayerSP3.ItemInUseCount - f1 + 1.0F);
						f11 = f10 / 20.0F;
						f11 = (f11 * f11 + f11 * 2.0F) / 3.0F;
						if (f11 > 1.0F)
						{
							f11 = 1.0F;
						}

						if (f11 > 0.1F)
						{
                            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, MathHelper.sin((f10 - 0.1F) * 1.3F) * 0.01F * (f11 - 0.1F), 0.0F);
						}

						Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, f11 * 0.1F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(-335.0F, 0.0F, 0.0F, 1.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(-50.0F, 0.0F, 1.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.5F, 0.0F);
						f12 = 1.0F + f11 * 0.2F;
						Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, 1.0F, f12);
						Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.5F, 0.0F);
						Minecraft.renderPipeline.ModelMatrix.Rotate(50.0F, 0.0F, 1.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(335.0F, 0.0F, 0.0F, 1.0F);
					}
				}

				if (itemStack14.Item.shouldRotateAroundWhenRendering())
				{
                    Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
				}

				if (itemStack14.Item.func_46058_c())
				{
					this.renderItem(entityPlayerSP3, itemStack14, 0);
					i9 = Item.itemsList[itemStack14.itemID].getColorFromDamage(itemStack14.ItemDamage, 1);
					f10 = (float)(i9 >> 16 & 255) / 255.0F;
					f11 = (float)(i9 >> 8 & 255) / 255.0F;
					f12 = (float)(i9 & 255) / 255.0F;
                    Minecraft.renderPipeline.SetColor(f6 * f10, f6 * f11, f6 * f12, 1.0F);
					this.renderItem(entityPlayerSP3, itemStack14, 1);
				}
				else
				{
					this.renderItem(entityPlayerSP3, itemStack14, 0);
				}

                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			}
			else
			{
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				f7 = 0.8F;
				f16 = entityPlayerSP3.getSwingProgress(f1);
				f18 = MathHelper.sin(f16 * (float)Math.PI);
				f10 = MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI);
				Minecraft.renderPipeline.ModelMatrix.Translate(-f10 * 0.3F, MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI * 2.0F) * 0.4F, -f18 * 0.4F);
				Minecraft.renderPipeline.ModelMatrix.Translate(0.8F * f7, -0.75F * f7 - (1.0F - f2) * 0.6F, -0.9F * f7);
				Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                
				f16 = entityPlayerSP3.getSwingProgress(f1);
				f18 = MathHelper.sin(f16 * f16 * (float)Math.PI);
				f10 = MathHelper.sin(MathHelper.sqrt_float(f16) * (float)Math.PI);
				Minecraft.renderPipeline.ModelMatrix.Rotate(f10 * 70.0F, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f18 * 20.0F, 0.0F, 0.0F, 1.0F);
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTextureForDownloadableImage(this.mc.thePlayer.skinUrl, this.mc.thePlayer.Texture));
				Minecraft.renderPipeline.ModelMatrix.Translate(-1.0F, 3.6F, 3.5F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(120.0F, 0.0F, 0.0F, 1.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(200.0F, 1.0F, 0.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-135.0F, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, 1.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(5.6F, 0.0F, 0.0F);
				Renderer render19 = RenderManager.instance.getEntityRenderObject(this.mc.thePlayer);
				RenderPlayer renderPlayer21 = (RenderPlayer)render19;
				f10 = 1.0F;
                Minecraft.renderPipeline.ModelMatrix.Scale(f10, f10, f10);
				renderPlayer21.drawFirstPersonHand();
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			}
            
			GameLighting.DisableMeshLighting();
		}

		public virtual void renderOverlays(float f1)
		{
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
            int i2;
			if (this.mc.thePlayer.Burning)
			{
				i2 = this.mc.renderEngine.getTexture("/terrain.png");
				GL.BindTexture(TextureTarget.Texture2D, i2);
				this.renderFireInFirstPerson(f1);
			}

			if (this.mc.thePlayer.EntityInsideOpaqueBlock)
			{
				i2 = MathHelper.floor_double(this.mc.thePlayer.posX);
				int i3 = MathHelper.floor_double(this.mc.thePlayer.posY);
				int i4 = MathHelper.floor_double(this.mc.thePlayer.posZ);
				int i5 = this.mc.renderEngine.getTexture("/terrain.png");
				GL.BindTexture(TextureTarget.Texture2D, i5);
				int i6 = this.mc.theWorld.getBlockId(i2, i3, i4);
				if (this.mc.theWorld.isBlockNormalCube(i2, i3, i4))
				{
					this.renderInsideOfBlock(f1, Block.blocksList[i6].getBlockTextureFromSide(2));
				}
				else
				{
					for (int i7 = 0; i7 < 8; ++i7)
					{
						float f8 = ((float)((i7 >> 0) % 2) - 0.5F) * this.mc.thePlayer.width * 0.9F;
						float f9 = ((float)((i7 >> 1) % 2) - 0.5F) * this.mc.thePlayer.height * 0.2F;
						float f10 = ((float)((i7 >> 2) % 2) - 0.5F) * this.mc.thePlayer.width * 0.9F;
						int i11 = MathHelper.floor_float((float)i2 + f8);
						int i12 = MathHelper.floor_float((float)i3 + f9);
						int i13 = MathHelper.floor_float((float)i4 + f10);
						if (this.mc.theWorld.isBlockNormalCube(i11, i12, i13))
						{
							i6 = this.mc.theWorld.getBlockId(i11, i12, i13);
						}
					}
				}

				if (Block.blocksList[i6] != null)
				{
					this.renderInsideOfBlock(f1, Block.blocksList[i6].getBlockTextureFromSide(2));
				}
			}

			if (this.mc.thePlayer.isInsideOfMaterial(Material.water))
			{
				i2 = this.mc.renderEngine.getTexture("/misc/water.png");
				GL.BindTexture(TextureTarget.Texture2D, i2);
				this.renderWarpedTextureOverlay(f1);
			}

            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
        }

		private void renderInsideOfBlock(float f1, int i2)
		{
			Tessellator tessellator3 = Tessellator.instance;
			this.mc.thePlayer.getBrightness(f1);
			float f4 = 0.1F;
            Minecraft.renderPipeline.SetColor(f4, f4, f4, 0.5F);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
			float f5 = -1.0F;
			float f6 = 1.0F;
			float f7 = -1.0F;
			float f8 = 1.0F;
			float f9 = -0.5F;
			float f10 = 0.0078125F;
			float f11 = (float)(i2 % 16) / 256.0F - f10;
			float f12 = ((float)(i2 % 16) + 15.99F) / 256.0F + f10;
			float f13 = (float)(i2 / 16) / 256.0F - f10;
			float f14 = ((float)(i2 / 16) + 15.99F) / 256.0F + f10;
			tessellator3.startDrawingQuads();
			tessellator3.AddVertexWithUV((double)f5, (double)f7, (double)f9, (double)f12, (double)f14);
			tessellator3.AddVertexWithUV((double)f6, (double)f7, (double)f9, (double)f11, (double)f14);
			tessellator3.AddVertexWithUV((double)f6, (double)f8, (double)f9, (double)f11, (double)f13);
			tessellator3.AddVertexWithUV((double)f5, (double)f8, (double)f9, (double)f12, (double)f13);
			tessellator3.DrawImmediate();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
		}

		private void renderWarpedTextureOverlay(float f1)
		{
			Tessellator tessellator2 = Tessellator.instance;
			float f3 = this.mc.thePlayer.getBrightness(f1);
            Minecraft.renderPipeline.SetColor(f3, f3, f3, 0.5F);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
			float f4 = 4.0F;
			float f5 = -1.0F;
			float f6 = 1.0F;
			float f7 = -1.0F;
			float f8 = 1.0F;
			float f9 = -0.5F;
			float f10 = -this.mc.thePlayer.rotationYaw / 64.0F;
			float f11 = this.mc.thePlayer.rotationPitch / 64.0F;
			tessellator2.startDrawingQuads();
			tessellator2.AddVertexWithUV((double)f5, (double)f7, (double)f9, (double)(f4 + f10), (double)(f4 + f11));
			tessellator2.AddVertexWithUV((double)f6, (double)f7, (double)f9, (double)(0.0F + f10), (double)(f4 + f11));
			tessellator2.AddVertexWithUV((double)f6, (double)f8, (double)f9, (double)(0.0F + f10), (double)(0.0F + f11));
			tessellator2.AddVertexWithUV((double)f5, (double)f8, (double)f9, (double)(f4 + f10), (double)(0.0F + f11));
			tessellator2.DrawImmediate();
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			GL.Disable(EnableCap.Blend);
		}

		private void renderFireInFirstPerson(float f1)
		{
			Tessellator tessellator2 = Tessellator.instance;
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 0.9F);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			float f3 = 1.0F;

			for (int i4 = 0; i4 < 2; ++i4)
			{
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				int i5 = Block.fire.blockIndexInTexture + i4 * 16;
				int i6 = (i5 & 15) << 4;
				int i7 = i5 & 240;
				float f8 = (float)i6 / 256.0F;
				float f9 = ((float)i6 + 15.99F) / 256.0F;
				float f10 = (float)i7 / 256.0F;
				float f11 = ((float)i7 + 15.99F) / 256.0F;
				float f12 = (0.0F - f3) / 2.0F;
				float f13 = f12 + f3;
				float f14 = 0.0F - f3 / 2.0F;
				float f15 = f14 + f3;
				float f16 = -0.5F;
                Minecraft.renderPipeline.ModelMatrix.Translate((float)(-(i4 * 2 - 1)) * 0.24F, -0.3F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate((float)(i4 * 2 - 1) * 10.0F, 0.0F, 1.0F, 0.0F);
				tessellator2.startDrawingQuads();
				tessellator2.AddVertexWithUV((double)f12, (double)f14, (double)f16, (double)f9, (double)f11);
				tessellator2.AddVertexWithUV((double)f13, (double)f14, (double)f16, (double)f8, (double)f11);
				tessellator2.AddVertexWithUV((double)f13, (double)f15, (double)f16, (double)f8, (double)f10);
				tessellator2.AddVertexWithUV((double)f12, (double)f15, (double)f16, (double)f9, (double)f10);
				tessellator2.DrawImmediate();
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			}

            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			GL.Disable(EnableCap.Blend);
		}

		public virtual void updateEquippedItem()
		{
			this.prevEquippedProgress = this.equippedProgress;
			EntityPlayerSP entityPlayerSP1 = this.mc.thePlayer;
			ItemStack itemStack2 = entityPlayerSP1.inventory.CurrentItem;
			bool z4 = this.equippedItemSlot == entityPlayerSP1.inventory.currentItem && itemStack2 == this.itemToRender;
			if (this.itemToRender == null && itemStack2 == null)
			{
				z4 = true;
			}

			if (itemStack2 != null && this.itemToRender != null && itemStack2 != this.itemToRender && itemStack2.itemID == this.itemToRender.itemID && itemStack2.ItemDamage == this.itemToRender.ItemDamage)
			{
				this.itemToRender = itemStack2;
				z4 = true;
			}

			float f5 = 0.4F;
			float f6 = z4 ? 1.0F : 0.0F;
			float f7 = f6 - this.equippedProgress;
			if (f7 < -f5)
			{
				f7 = -f5;
			}

			if (f7 > f5)
			{
				f7 = f5;
			}

			this.equippedProgress += f7;
			if (this.equippedProgress < 0.1F)
			{
				this.itemToRender = itemStack2;
				this.equippedItemSlot = entityPlayerSP1.inventory.currentItem;
			}

		}

		public virtual void func_9449_b()
		{
			this.equippedProgress = 0.0F;
		}

		public virtual void func_9450_c()
		{
			this.equippedProgress = 0.0F;
		}
	}

}