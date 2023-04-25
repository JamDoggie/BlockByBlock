using System;
using System.Collections;
using System.Diagnostics;
using BlockByBlock.helpers;
using BlockByBlock.java_extensions;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

    using Minecraft = net.minecraft.client.Minecraft;


    public class GuiIngame : Gui
	{
		private static RenderItem itemRenderer = new RenderItem();
		private System.Collections.IList chatMessageList = new ArrayList();
		private System.Collections.IList field_50016_f = new ArrayList();
		private RandomExtended rand = new RandomExtended();
		private Minecraft mc;
		private int updateCounter = 0;
		private string recordPlaying = "";
		private int recordPlayingUpFor = 0;
		private bool recordIsPlaying = false;
		private int field_50017_n = 0;
		private bool field_50018_o = false;
		public float damageGuiPartialTime;
		internal float prevVignetteBrightness = 1.0F;

		private Process process = Process.GetCurrentProcess();

		public GuiIngame(Minecraft minecraft1)
		{
			this.mc = minecraft1;
		}

		public virtual void renderGameOverlay(float f1, bool z2, int i3, int i4)
		{
			ScaledResolution scaledResolution5 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
			int i6 = scaledResolution5.ScaledWidth;
			int i7 = scaledResolution5.ScaledHeight;
			FontRenderer fontRenderer8 = this.mc.fontRenderer;
			this.mc.gameRenderer.setupOverlayRendering();
			GL.Enable(EnableCap.Blend);
			if (Minecraft.FancyGraphicsEnabled)
			{
				this.renderVignette(this.mc.thePlayer.getBrightness(f1), i6, i7);
			}
			else
			{
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			}

			ItemStack itemStack9 = this.mc.thePlayer.inventory.armorItemInSlot(3);
			if (this.mc.gameSettings.thirdPersonView == 0 && itemStack9 != null && itemStack9.itemID == Block.pumpkin.blockID)
			{
				this.renderPumpkinBlur(i6, i7);
			}

			if (!this.mc.thePlayer.isPotionActive(Potion.confusion))
			{
				float f10 = this.mc.thePlayer.prevTimeInPortal + (this.mc.thePlayer.timeInPortal - this.mc.thePlayer.prevTimeInPortal) * f1;
				if (f10 > 0.0F)
				{
					this.renderPortalOverlay(f10, i6, i7);
				}
			}

			bool z11;
			int i12;
			int i13;
			int i16;
			int i17;
			int i19;
			int i20;
			int i22;
			int i23;
			int i45;
			if (!this.mc.playerController.IsPanoramaCamera())
			{
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/gui/gui.png"));
				InventoryPlayer inventoryPlayer31 = this.mc.thePlayer.inventory;
				this.zLevel = -90.0F;
				this.drawTexturedModalRect(i6 / 2 - 91, i7 - 22, 0, 0, 182, 22);
				this.drawTexturedModalRect(i6 / 2 - 91 - 1 + inventoryPlayer31.currentItem * 20, i7 - 22 - 1, 0, 22, 24, 22);
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/gui/icons.png"));
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.OneMinusDstColor, BlendingFactor.OneMinusSrcColor);
				this.drawTexturedModalRect(i6 / 2 - 7, i7 / 2 - 7, 0, 0, 16, 16);
				GL.Disable(EnableCap.Blend);
				z11 = this.mc.thePlayer.heartsLife / 3 % 2 == 1;
				if (this.mc.thePlayer.heartsLife < 10)
				{
					z11 = false;
				}

				i12 = this.mc.thePlayer.Health;
				i13 = this.mc.thePlayer.prevHealth;
				rand.SetSeed((long)(this.updateCounter * 312871));
				bool z14 = false;
				FoodStats foodStats15 = this.mc.thePlayer.FoodStats;
				i16 = foodStats15.FoodLevel;
				i17 = foodStats15.PrevFoodLevel;
				this.renderBossHealth();
				int i18;
				if (this.mc.playerController.shouldDrawHUD())
				{
					i18 = i6 / 2 - 91;
					i19 = i6 / 2 + 91;
					i20 = this.mc.thePlayer.xpBarCap();
					if (i20 > 0)
					{
						short s21 = 182;
						i22 = (int)(this.mc.thePlayer.experience * (float)(s21 + 1));
						i23 = i7 - 32 + 3;
						this.drawTexturedModalRect(i18, i23, 0, 64, s21, 5);
						if (i22 > 0)
						{
							this.drawTexturedModalRect(i18, i23, 0, 69, i22, 5);
						}
					}

					i45 = i7 - 39;
					i22 = i45 - 10;
					i23 = this.mc.thePlayer.TotalArmorValue;
					int i24 = -1;
					if (this.mc.thePlayer.isPotionActive(Potion.regeneration))
					{
						i24 = this.updateCounter % 25;
					}

					int i25;
					int i26;
					int i28;
					int i29;
					for (i25 = 0; i25 < 10; ++i25)
					{
						if (i23 > 0)
						{
							i26 = i18 + i25 * 8;
							if (i25 * 2 + 1 < i23)
							{
								this.drawTexturedModalRect(i26, i22, 34, 9, 9, 9);
							}

							if (i25 * 2 + 1 == i23)
							{
								this.drawTexturedModalRect(i26, i22, 25, 9, 9, 9);
							}

							if (i25 * 2 + 1 > i23)
							{
								this.drawTexturedModalRect(i26, i22, 16, 9, 9, 9);
							}
						}

						i26 = 16;
						if (this.mc.thePlayer.isPotionActive(Potion.poison))
						{
							i26 += 36;
						}

						sbyte b27 = 0;
						if (z11)
						{
							b27 = 1;
						}

						i28 = i18 + i25 * 8;
						i29 = i45;
						if (i12 <= 4)
						{
							i29 = i45 + this.rand.Next(2);
						}

						if (i25 == i24)
						{
							i29 -= 2;
						}

						sbyte b30 = 0;
						if (this.mc.theWorld.WorldInfo.HardcoreModeEnabled)
						{
							b30 = 5;
						}

						this.drawTexturedModalRect(i28, i29, 16 + b27 * 9, 9 * b30, 9, 9);
						if (z11)
						{
							if (i25 * 2 + 1 < i13)
							{
								this.drawTexturedModalRect(i28, i29, i26 + 54, 9 * b30, 9, 9);
							}

							if (i25 * 2 + 1 == i13)
							{
								this.drawTexturedModalRect(i28, i29, i26 + 63, 9 * b30, 9, 9);
							}
						}

						if (i25 * 2 + 1 < i12)
						{
							this.drawTexturedModalRect(i28, i29, i26 + 36, 9 * b30, 9, 9);
						}

						if (i25 * 2 + 1 == i12)
						{
							this.drawTexturedModalRect(i28, i29, i26 + 45, 9 * b30, 9, 9);
						}
					}

					int i51;
					for (i25 = 0; i25 < 10; ++i25)
					{
						i26 = i45;
						i51 = 16;
						sbyte b52 = 0;
						if (this.mc.thePlayer.isPotionActive(Potion.hunger))
						{
							i51 += 36;
							b52 = 13;
						}

						if (this.mc.thePlayer.FoodStats.SaturationLevel <= 0.0F && this.updateCounter % (i16 * 3 + 1) == 0)
						{
							i26 = i45 + (this.rand.Next(3) - 1);
						}

						if (z14)
						{
							b52 = 1;
						}

						i29 = i19 - i25 * 8 - 9;
						this.drawTexturedModalRect(i29, i26, 16 + b52 * 9, 27, 9, 9);
						if (z14)
						{
							if (i25 * 2 + 1 < i17)
							{
								this.drawTexturedModalRect(i29, i26, i51 + 54, 27, 9, 9);
							}

							if (i25 * 2 + 1 == i17)
							{
								this.drawTexturedModalRect(i29, i26, i51 + 63, 27, 9, 9);
							}
						}

						if (i25 * 2 + 1 < i16)
						{
							this.drawTexturedModalRect(i29, i26, i51 + 36, 27, 9, 9);
						}

						if (i25 * 2 + 1 == i16)
						{
							this.drawTexturedModalRect(i29, i26, i51 + 45, 27, 9, 9);
						}
					}

					if (this.mc.thePlayer.isInsideOfMaterial(Material.water))
					{
						i25 = this.mc.thePlayer.Air;
						i26 = (int)Math.Ceiling((double)(i25 - 2) * 10.0D / 300.0D);
						i51 = (int)Math.Ceiling((double)i25 * 10.0D / 300.0D) - i26;

						for (i28 = 0; i28 < i26 + i51; ++i28)
						{
							if (i28 < i26)
							{
								this.drawTexturedModalRect(i19 - i28 * 8 - 9, i22, 16, 18, 9, 9);
							}
							else
							{
								this.drawTexturedModalRect(i19 - i28 * 8 - 9, i22, 25, 18, 9, 9);
							}
						}
					}
				}

				GL.Disable(EnableCap.Blend);
				GameLighting.EnableGUIStandardItemLighting();

				for (i18 = 0; i18 < 9; ++i18)
				{
					i19 = i6 / 2 - 90 + i18 * 20 + 2;
					i20 = i7 - 16 - 3;
					this.renderInventorySlot(i18, i19, i20, f1);
				}

				GameLighting.DisableMeshLighting();
			}

			float f33;
			if (this.mc.thePlayer.SleepTimer > 0)
			{
				GL.Disable(EnableCap.DepthTest);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                int i32 = this.mc.thePlayer.SleepTimer;
				f33 = (float)i32 / 100.0F;
				if (f33 > 1.0F)
				{
					f33 = 1.0F - (float)(i32 - 100) / 10.0F;
				}

				i12 = (int)(220.0F * f33) << 24 | 1052704;
				drawRect(0, 0, i6, i7, i12);
				GL.Enable(EnableCap.DepthTest);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
            }

			int i39;
			int i40;
			if (this.mc.playerController.func_35642_f() && this.mc.thePlayer.experienceLevel > 0)
			{
				z11 = false;
				i12 = z11 ? 0xFFFFFF : 8453920;
				string string34 = "" + this.mc.thePlayer.experienceLevel;
				i39 = (i6 - fontRenderer8.getStringWidth(string34)) / 2;
				i40 = i7 - 31 - 4;
				fontRenderer8.drawString(string34, i39 + 1, i40, 0);
				fontRenderer8.drawString(string34, i39 - 1, i40, 0);
				fontRenderer8.drawString(string34, i39, i40 + 1, 0);
				fontRenderer8.drawString(string34, i39, i40 - 1, 0);
				fontRenderer8.drawString(string34, i39, i40, i12);
			}

			if (this.mc.gameSettings.showDebugInfo)
			{
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				if (Minecraft.hasPaidCheckTime > 0L)
				{
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 32.0F, 0.0F);
				}

				fontRenderer8.drawStringWithShadow($"Minecraft 1.2.5 ({mc.debug}) Runtime: .Net {Environment.Version}", 2, 2, 0xFFFFFF);
				fontRenderer8.drawStringWithShadow(this.mc.debugInfoRenders(), 2, 12, 0xFFFFFF);
				fontRenderer8.drawStringWithShadow(this.mc.EntityDebug, 2, 22, 0xFFFFFF);
				fontRenderer8.drawStringWithShadow(this.mc.debugInfoEntities(), 2, 32, 0xFFFFFF);
				fontRenderer8.drawStringWithShadow(this.mc.WorldProviderName, 2, 42, 0xFFFFFF);
				long j35 = 69420;
				long j36 = process.PrivateMemorySize64;
				long j41 = 69420;
				long j42 = j36 - j41;
				string string44 = "Used memory: " + j42 * 100L / j35 + "% (" + j42 / 1024L / 1024L + "MB) of " + j35 / 1024L / 1024L + "MB";
				this.drawString(fontRenderer8, string44, i6 - fontRenderer8.getStringWidth(string44) - 2, 2, 14737632);
				string44 = "Allocated memory: " + j36 * 100L / j35 + "% (" + j36 / 1024L / 1024L + "MB)";
				this.drawString(fontRenderer8, string44, i6 - fontRenderer8.getStringWidth(string44) - 2, 12, 14737632);
				this.drawString(fontRenderer8, "x: " + this.mc.thePlayer.posX, 2, 64, 14737632);
				this.drawString(fontRenderer8, "y: " + this.mc.thePlayer.posY, 2, 72, 14737632);
				this.drawString(fontRenderer8, "z: " + this.mc.thePlayer.posZ, 2, 80, 14737632);
				this.drawString(fontRenderer8, "f: " + (MathHelper.floor_double((double)(this.mc.thePlayer.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3), 2, 88, 14737632);
				i45 = MathHelper.floor_double(this.mc.thePlayer.posX);
				i22 = MathHelper.floor_double(this.mc.thePlayer.posY);
				i23 = MathHelper.floor_double(this.mc.thePlayer.posZ);
				if (this.mc.theWorld != null && this.mc.theWorld.blockExists(i45, i22, i23))
				{
					Chunk chunk48 = this.mc.theWorld.getChunkFromBlockCoords(i45, i23);
					this.drawString(fontRenderer8, "lc: " + (chunk48.TopFilledSegment + 15) + " b: " + chunk48.func_48490_a(i45 & 15, i23 & 15, this.mc.theWorld.WorldChunkManager).biomeName + " bl: " + chunk48.getSavedLightValue(EnumSkyBlock.Block, i45 & 15, i22, i23 & 15) + " sl: " + chunk48.getSavedLightValue(EnumSkyBlock.Sky, i45 & 15, i22, i23 & 15) + " rl: " + chunk48.getBlockLightValue(i45 & 15, i22, i23 & 15, 0), 2, 96, 14737632);
				}

				if (!this.mc.theWorld.isRemote)
				{
					this.drawString(fontRenderer8, "Seed: " + this.mc.theWorld.Seed, 2, 112, 14737632);
				}

                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			}

			if (this.recordPlayingUpFor > 0)
			{
				f33 = (float)this.recordPlayingUpFor - f1;
				i12 = (int)(f33 * 256.0F / 20.0F);
				if (i12 > 255)
				{
					i12 = 255;
				}

				if (i12 > 0)
				{
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Translate((float)(i6 / 2), (float)(i7 - 48), 0.0F);
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
					i13 = 0xFFFFFF;
					if (this.recordIsPlaying)
					{
						i13 = ColorHelpers.ColorFromHSV(f33 / 50.0F, 0.7F, 0.6F).ToArgb() & 0xFFFFFF;
					}

					fontRenderer8.drawString(this.recordPlaying, -fontRenderer8.getStringWidth(this.recordPlaying) / 2, -4, i13 + (i12 << 24));
					GL.Disable(EnableCap.Blend);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}
			}
            
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, (float)(i7 - 48), 0.0F);
			this.func_50010_a(fontRenderer8); // TODO: whatever this is should be named.
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			if (this.mc.thePlayer is EntityClientPlayerMP && this.mc.gameSettings.keyBindPlayerList.pressed)
			{
				NetClientHandler netClientHandler37 = ((EntityClientPlayerMP)this.mc.thePlayer).sendQueue;
				System.Collections.IList list38 = netClientHandler37.playerNames;
				i13 = netClientHandler37.currentServerMaxPlayers;
				i39 = i13;

				for (i40 = 1; i39 > 20; i39 = (i13 + i40 - 1) / i40)
				{
					++i40;
				}

				i16 = 300 / i40;
				if (i16 > 150)
				{
					i16 = 150;
				}

				i17 = (i6 - i40 * i16) / 2;
				sbyte b43 = 10;
				drawRect(i17 - 1, b43 - 1, i17 + i16 * i40, b43 + 9 * i39, int.MinValue);

				for (i19 = 0; i19 < i13; ++i19)
				{
					i20 = i17 + i19 % i40 * i16;
					i45 = b43 + i19 / i40 * 9;
					drawRect(i20, i45, i20 + i16 - 1, i45 + 8, 553648127);
					Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                    if (i19 < list38.Count)
					{
						GuiPlayerInfo guiPlayerInfo46 = (GuiPlayerInfo)list38[i19];
						fontRenderer8.drawStringWithShadow(guiPlayerInfo46.name, i20, i45, 0xFFFFFF);
						this.mc.renderEngine.bindTexture(this.mc.renderEngine.getTexture("/gui/icons.png"));
						sbyte b47 = 0;
						bool z49 = false;
						sbyte b50;
						if (guiPlayerInfo46.responseTime < 0)
						{
							b50 = 5;
						}
						else if (guiPlayerInfo46.responseTime < 150)
						{
							b50 = 0;
						}
						else if (guiPlayerInfo46.responseTime < 300)
						{
							b50 = 1;
						}
						else if (guiPlayerInfo46.responseTime < 600)
						{
							b50 = 2;
						}
						else if (guiPlayerInfo46.responseTime < 1000)
						{
							b50 = 3;
						}
						else
						{
							b50 = 4;
						}

						this.zLevel += 100.0F;
						this.drawTexturedModalRect(i20 + i16 - 12, i45, 0 + b47 * 10, 176 + b50 * 8, 10, 8);
						this.zLevel -= 100.0F;
					}
				}
			}

			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
        }

		private void func_50010_a(FontRenderer fontRenderer1)
		{
			sbyte b2 = 10;
			bool z3 = false;
			int i4 = 0;
			int i5 = this.chatMessageList.Count;
			if (i5 > 0)
			{
				if (this.ChatOpen)
				{
					b2 = 20;
					z3 = true;
				}

				int i6;
				int i10;
				for (i6 = 0; i6 + this.field_50017_n < this.chatMessageList.Count && i6 < b2; ++i6)
				{
					if (((ChatLine)this.chatMessageList[i6]).updateCounter < 200 || z3)
					{
						ChatLine chatLine7 = (ChatLine)this.chatMessageList[i6 + this.field_50017_n];
						double d8 = (double)chatLine7.updateCounter / 200.0D;
						d8 = 1.0D - d8;
						d8 *= 10.0D;
						if (d8 < 0.0D)
						{
							d8 = 0.0D;
						}

						if (d8 > 1.0D)
						{
							d8 = 1.0D;
						}

						d8 *= d8;
						i10 = (int)(255.0D * d8);
						if (z3)
						{
							i10 = 255;
						}

						++i4;
						if (i10 > 2)
						{
							sbyte b11 = 3;
							int i12 = -i6 * 9;
							string string13 = chatLine7.message;
							drawRect(b11, i12 - 1, b11 + 320 + 4, i12 + 8, i10 / 2 << 24);
							GL.Enable(EnableCap.Blend);
							fontRenderer1.drawStringWithShadow(string13, b11, i12, 0xFFFFFF + (i10 << 24));
						}
					}
				}

				if (z3)
				{
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, (float)fontRenderer1.FONT_HEIGHT, 0.0F);
					i6 = i5 * fontRenderer1.FONT_HEIGHT + i5;
					int i14 = i4 * fontRenderer1.FONT_HEIGHT + i4;
					int i15 = this.field_50017_n * i14 / i5;
					int i9 = i14 * i14 / i6;
					if (i6 != i14)
					{
						i10 = i15 > 0 ? 170 : 96;
						int i16 = this.field_50018_o ? 13382451 : 3355562;
						drawRect(0, -i15, 2, -i15 - i9, i16 + (i10 << 24));
						drawRect(2, -i15, 1, -i15 - i9, 13421772 + (i10 << 24));
					}
				}

			}
		}

		private void renderBossHealth()
		{
			if (RenderDragon.entityDragon != null)
			{
				EntityDragon entityDragon1 = RenderDragon.entityDragon;
				RenderDragon.entityDragon = null;
				FontRenderer fontRenderer2 = this.mc.fontRenderer;
				ScaledResolution scaledResolution3 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
				int i4 = scaledResolution3.ScaledWidth;
				short s5 = 182;
				int i6 = i4 / 2 - s5 / 2;
				int i7 = (int)((float)entityDragon1.func_41010_ax() / (float)entityDragon1.MaxHealth * (float)(s5 + 1));
				sbyte b8 = 12;
				this.drawTexturedModalRect(i6, b8, 0, 74, s5, 5);
				this.drawTexturedModalRect(i6, b8, 0, 74, s5, 5);
				if (i7 > 0)
				{
					this.drawTexturedModalRect(i6, b8, 0, 79, i7, 5);
				}

				string string9 = "Boss health";
				fontRenderer2.drawStringWithShadow(string9, i4 / 2 - fontRenderer2.getStringWidth(string9) / 2, b8 - 10, 16711935);
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/gui/icons.png"));
			}
		}

		private void renderPumpkinBlur(int i1, int i2)
		{
			GL.Disable(EnableCap.DepthTest);
			GL.DepthMask(false);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
            GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("%blur%/misc/pumpkinblur.png"));
			Tessellator tessellator3 = Tessellator.instance;
			tessellator3.startDrawingQuads();
			tessellator3.AddVertexWithUV(0.0D, (double)i2, -90.0D, 0.0D, 1.0D);
			tessellator3.AddVertexWithUV((double)i1, (double)i2, -90.0D, 1.0D, 1.0D);
			tessellator3.AddVertexWithUV((double)i1, 0.0D, -90.0D, 1.0D, 0.0D);
			tessellator3.AddVertexWithUV(0.0D, 0.0D, -90.0D, 0.0D, 0.0D);
			tessellator3.DrawImmediate();
			GL.DepthMask(true);
			GL.Enable(EnableCap.DepthTest);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
		}

		private void renderVignette(float f1, int i2, int i3)
		{
			f1 = 1.0F - f1;
			if (f1 < 0.0F)
			{
				f1 = 0.0F;
			}

			if (f1 > 1.0F)
			{
				f1 = 1.0F;
			}

			this.prevVignetteBrightness = (float)((double)this.prevVignetteBrightness + (double)(f1 - this.prevVignetteBrightness) * 0.01D);
			GL.Disable(EnableCap.DepthTest);
			GL.DepthMask(false);
			GL.BlendFunc(BlendingFactor.Zero, BlendingFactor.OneMinusSrcColor);
			Minecraft.renderPipeline.SetColor(this.prevVignetteBrightness, this.prevVignetteBrightness, this.prevVignetteBrightness, 1.0F);
			GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("%blur%/misc/vignette.png"));
			Tessellator tessellator4 = Tessellator.instance;
			tessellator4.startDrawingQuads();
			tessellator4.AddVertexWithUV(0.0D, (double)i3, -90.0D, 0.0D, 1.0D);
			tessellator4.AddVertexWithUV((double)i2, (double)i3, -90.0D, 1.0D, 1.0D);
			tessellator4.AddVertexWithUV((double)i2, 0.0D, -90.0D, 1.0D, 0.0D);
			tessellator4.AddVertexWithUV(0.0D, 0.0D, -90.0D, 0.0D, 0.0D);
			tessellator4.DrawImmediate();
			GL.DepthMask(true);
			GL.Enable(EnableCap.DepthTest);
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
		}

		private void renderPortalOverlay(float f1, int i2, int i3)
		{
			if (f1 < 1.0F)
			{
				f1 *= f1;
				f1 *= f1;
				f1 = f1 * 0.8F + 0.2F;
			}

            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
            GL.Disable(EnableCap.DepthTest);
			GL.DepthMask(false);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, f1);
			GL.BindTexture(TextureTarget.Texture2D, mc.renderEngine.getTexture("/terrain.png"));
			float f4 = (float)(Block.portal.blockIndexInTexture % 16) / 16.0F;
			float f5 = (float)(Block.portal.blockIndexInTexture / 16) / 16.0F;
			float f6 = (float)(Block.portal.blockIndexInTexture % 16 + 1) / 16.0F;
			float f7 = (float)(Block.portal.blockIndexInTexture / 16 + 1) / 16.0F;
			Tessellator tessellator8 = Tessellator.instance;
			tessellator8.startDrawingQuads();
			tessellator8.AddVertexWithUV(0.0D, (double)i3, -90.0D, (double)f4, (double)f7);
			tessellator8.AddVertexWithUV((double)i2, (double)i3, -90.0D, (double)f6, (double)f7);
			tessellator8.AddVertexWithUV((double)i2, 0.0D, -90.0D, (double)f6, (double)f5);
			tessellator8.AddVertexWithUV(0.0D, 0.0D, -90.0D, (double)f4, (double)f5);
			tessellator8.DrawImmediate();
			GL.DepthMask(true);
			GL.Enable(EnableCap.DepthTest);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
		}

		private void renderInventorySlot(int i1, int i2, int i3, float f4)
		{
			ItemStack itemStack5 = this.mc.thePlayer.inventory.mainInventory[i1];
			if (itemStack5 != null)
			{
				float f6 = (float)itemStack5.animationsToGo - f4;
				if (f6 > 0.0F)
				{
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
					float f7 = 1.0F + f6 / 5.0F;
                    Minecraft.renderPipeline.ModelMatrix.Translate((float)(i2 + 8), (float)(i3 + 12), 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f7, (f7 + 1.0F) / 2.0F, 1.0F);
                    Minecraft.renderPipeline.ModelMatrix.Translate((float)(-(i2 + 8)), (float)(-(i3 + 12)), 0.0F);
				}

				itemRenderer.renderItemIntoGUI(this.mc.fontRenderer, this.mc.renderEngine, itemStack5, i2, i3);
				if (f6 > 0.0F)
				{
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}

				itemRenderer.renderItemOverlayIntoGUI(this.mc.fontRenderer, this.mc.renderEngine, itemStack5, i2, i3);
			}
		}

		public virtual void updateTick()
		{
			if (this.recordPlayingUpFor > 0)
			{
				--this.recordPlayingUpFor;
			}

			++this.updateCounter;

			for (int i1 = 0; i1 < this.chatMessageList.Count; ++i1)
			{
				++((ChatLine)this.chatMessageList[i1]).updateCounter;
			}

		}

		public virtual void clearChatMessages()
		{
			this.chatMessageList.Clear();
			this.field_50016_f.Clear();
		}

		public virtual void addChatMessage(string string1)
		{
			bool z2 = this.ChatOpen;
			bool z3 = true;
			System.Collections.IEnumerator iterator4 = this.mc.fontRenderer.func_50108_c(string1, 320).GetEnumerator();

			while (iterator4.MoveNext())
			{
				string string5 = (string)iterator4.Current;
				if (z2 && this.field_50017_n > 0)
				{
					this.field_50018_o = true;
					this.func_50011_a(1);
				}

				if (!z3)
				{
					string5 = " " + string5;
				}

				z3 = false;
				this.chatMessageList.Insert(0, new ChatLine(string5));
			}

			while (this.chatMessageList.Count > 100)
			{
				this.chatMessageList.RemoveAt(this.chatMessageList.Count - 1);
			}

		}

		public virtual System.Collections.IList func_50013_c()
		{
			return this.field_50016_f;
		}

		public virtual void func_50014_d()
		{
			this.field_50017_n = 0;
			this.field_50018_o = false;
		}

		public virtual void func_50011_a(int i1)
		{
			this.field_50017_n += i1;
			int i2 = this.chatMessageList.Count;
			if (this.field_50017_n > i2 - 20)
			{
				this.field_50017_n = i2 - 20;
			}

			if (this.field_50017_n <= 0)
			{
				this.field_50017_n = 0;
				this.field_50018_o = false;
			}

		}

		public virtual ChatClickData func_50012_a(int i1, int i2)
		{
			if (!this.ChatOpen)
			{
				return null;
			}
			else
			{
				ScaledResolution scaledResolution3 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
				i2 = i2 / scaledResolution3.scaleFactor - 40;
				i1 = i1 / scaledResolution3.scaleFactor - 3;
				if (i1 >= 0 && i2 >= 0)
				{
					int i4 = Math.Min(20, this.chatMessageList.Count);
					if (i1 <= 320 && i2 < this.mc.fontRenderer.FONT_HEIGHT * i4 + i4)
					{
						int i5 = i2 / (this.mc.fontRenderer.FONT_HEIGHT + 1) + this.field_50017_n;
						return new ChatClickData(this.mc.fontRenderer, (ChatLine)this.chatMessageList[i5], i1, i2 - (i5 - this.field_50017_n) * this.mc.fontRenderer.FONT_HEIGHT + i5);
					}
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
		}

		public virtual string RecordPlayingMessage
		{
			set
			{
				this.recordPlaying = "Now playing: " + value;
				this.recordPlayingUpFor = 60;
				this.recordIsPlaying = true;
			}
		}

		public virtual bool ChatOpen
		{
			get
			{
				return this.mc.currentScreen is GuiChat;
			}
		}

		public virtual void addChatMessageTranslate(string string1)
		{
			StringTranslate stringTranslate2 = StringTranslate.Instance;
			string string3 = stringTranslate2.translateKey(string1);
			this.addChatMessage(string3);
		}
	}

}