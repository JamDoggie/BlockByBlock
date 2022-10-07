namespace net.minecraft.src
{
    using OpenTK.Graphics.OpenGL;
    using Minecraft = net.minecraft.client.Minecraft;

	public class RenderPlayer : RenderLiving
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			modelBipedMain = (ModelBiped)this.mainModel;
		}

		private ModelBiped modelBipedMain;
		private ModelBiped modelArmorChestplate = new ModelBiped(1.0F);
		private ModelBiped modelArmor = new ModelBiped(0.5F);
		private static readonly string[] armorFilenamePrefix = new string[]{"cloth", "chain", "iron", "diamond", "gold"};

		public RenderPlayer() : base(new ModelBiped(0.0F), 0.5F)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
		}

		protected internal virtual int setArmorModel(EntityPlayer entityPlayer1, int i2, float f3)
		{
			ItemStack itemStack4 = entityPlayer1.inventory.armorItemInSlot(3 - i2);
			if (itemStack4 != null)
			{
				Item item5 = itemStack4.Item;
				if (item5 is ItemArmor)
				{
					ItemArmor itemArmor6 = (ItemArmor)item5;
					this.loadTexture("/armor/" + armorFilenamePrefix[itemArmor6.renderIndex] + "_" + (i2 == 2 ? 2 : 1) + ".png");
					ModelBiped modelBiped7 = i2 == 2 ? this.modelArmor : this.modelArmorChestplate;
					modelBiped7.bipedHead.showModel = i2 == 0;
					modelBiped7.bipedHeadwear.showModel = i2 == 0;
					modelBiped7.bipedBody.showModel = i2 == 1 || i2 == 2;
					modelBiped7.bipedRightArm.showModel = i2 == 1;
					modelBiped7.bipedLeftArm.showModel = i2 == 1;
					modelBiped7.bipedRightLeg.showModel = i2 == 2 || i2 == 3;
					modelBiped7.bipedLeftLeg.showModel = i2 == 2 || i2 == 3;
					this.RenderPassModel = modelBiped7;
					if (itemStack4.ItemEnchanted)
					{
						return 15;
					}

					return 1;
				}
			}

			return -1;
		}

		public virtual void renderPlayer(EntityPlayer entityPlayer1, double d2, double d4, double d6, float f8, float f9)
		{
			ItemStack itemStack10 = entityPlayer1.inventory.CurrentItem;
			this.modelArmorChestplate.heldItemRight = this.modelArmor.heldItemRight = this.modelBipedMain.heldItemRight = itemStack10 != null ? 1 : 0;
			if (itemStack10 != null && entityPlayer1.ItemInUseCount > 0)
			{
				EnumAction enumAction11 = itemStack10.ItemUseAction;
				if (enumAction11 == EnumAction.block)
				{
					this.modelArmorChestplate.heldItemRight = this.modelArmor.heldItemRight = this.modelBipedMain.heldItemRight = 3;
				}
				else if (enumAction11 == EnumAction.bow)
				{
					this.modelArmorChestplate.aimedBow = this.modelArmor.aimedBow = this.modelBipedMain.aimedBow = true;
				}
			}

			this.modelArmorChestplate.isSneak = this.modelArmor.isSneak = this.modelBipedMain.isSneak = entityPlayer1.Sneaking;
			double d13 = d4 - (double)entityPlayer1.yOffset;
			if (entityPlayer1.Sneaking && !(entityPlayer1 is EntityPlayerSP))
			{
				d13 -= 0.125D;
			}

			base.doRenderLiving(entityPlayer1, d2, d13, d6, f8, f9);
			this.modelArmorChestplate.aimedBow = this.modelArmor.aimedBow = this.modelBipedMain.aimedBow = false;
			this.modelArmorChestplate.isSneak = this.modelArmor.isSneak = this.modelBipedMain.isSneak = false;
			this.modelArmorChestplate.heldItemRight = this.modelArmor.heldItemRight = this.modelBipedMain.heldItemRight = 0;
		}

		protected internal virtual void renderName(EntityPlayer entityPlayer1, double d2, double d4, double d6)
		{
			if (Minecraft.GuiEnabled && entityPlayer1 != this.renderManager.livingPlayer)
			{
				float f8 = 1.6F;
				float f9 = 0.016666668F * f8;
				float f10 = entityPlayer1.getDistanceToEntity(this.renderManager.livingPlayer);
				float f11 = entityPlayer1.Sneaking ? 32.0F : 64.0F;
				if (f10 < f11)
				{
					string string12 = entityPlayer1.username;
					if (!entityPlayer1.Sneaking)
					{
						if (entityPlayer1.PlayerSleeping)
						{
							this.renderLivingLabel(entityPlayer1, string12, d2, d4 - 1.5D, d6, 64);
						}
						else
						{
							this.renderLivingLabel(entityPlayer1, string12, d2, d4, d6, 64);
						}
					}
					else
					{
						FontRenderer fontRenderer13 = this.FontRendererFromRenderManager;
						GL.PushMatrix();
						GL.Translate((float)d2 + 0.0F, (float)d4 + 2.3F, (float)d6);
						GL.Normal3(0.0F, 1.0F, 0.0F);
						GL.Rotate(-this.renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
						GL.Rotate(this.renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
						GL.Scale(-f9, -f9, f9);
						GL.Disable(EnableCap.Lighting);
						GL.Translate(0.0F, 0.25F / f9, 0.0F);
						GL.DepthMask(false);
						GL.Enable(EnableCap.Blend);
						GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
						Tessellator tessellator14 = Tessellator.instance;
						GL.Disable(EnableCap.Texture2D);
						tessellator14.startDrawingQuads();
						int i15 = fontRenderer13.getStringWidth(string12) / 2;
						tessellator14.setColorRGBA_F(0.0F, 0.0F, 0.0F, 0.25F);
						tessellator14.addVertex((double)(-i15 - 1), -1.0D, 0.0D);
						tessellator14.addVertex((double)(-i15 - 1), 8.0D, 0.0D);
						tessellator14.addVertex((double)(i15 + 1), 8.0D, 0.0D);
						tessellator14.addVertex((double)(i15 + 1), -1.0D, 0.0D);
						tessellator14.draw();
						GL.Enable(EnableCap.Texture2D);
						GL.DepthMask(true);
						fontRenderer13.drawString(string12, -fontRenderer13.getStringWidth(string12) / 2, 0, 553648127);
						GL.Enable(EnableCap.Lighting);
						GL.Disable(EnableCap.Blend);
						GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
						GL.PopMatrix();
					}
				}
			}

		}

		protected internal virtual void renderSpecials(EntityPlayer entityPlayer1, float f2)
		{
			base.renderEquippedItems(entityPlayer1, f2);
			ItemStack itemStack3 = entityPlayer1.inventory.armorItemInSlot(3);
			if (itemStack3 != null && itemStack3.Item.shiftedIndex < 256)
			{
				GL.PushMatrix();
				this.modelBipedMain.bipedHead.postRender(0.0625F);
				if (RenderBlocks.renderItemIn3d(Block.blocksList[itemStack3.itemID].RenderType))
				{
					float f4 = 0.625F;
					GL.Translate(0.0F, -0.25F, 0.0F);
					GL.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
					GL.Scale(f4, -f4, f4);
				}

				this.renderManager.itemRenderer.renderItem(entityPlayer1, itemStack3, 0);
				GL.PopMatrix();
			}

			float f6;
			if (entityPlayer1.username.Equals("deadmau5") && this.loadDownloadableImageTexture(entityPlayer1.skinUrl, (string)null))
			{
				for (int i19 = 0; i19 < 2; ++i19)
				{
					float f5 = entityPlayer1.prevRotationYaw + (entityPlayer1.rotationYaw - entityPlayer1.prevRotationYaw) * f2 - (entityPlayer1.prevRenderYawOffset + (entityPlayer1.renderYawOffset - entityPlayer1.prevRenderYawOffset) * f2);
					f6 = entityPlayer1.prevRotationPitch + (entityPlayer1.rotationPitch - entityPlayer1.prevRotationPitch) * f2;
					GL.PushMatrix();
					GL.Rotate(f5, 0.0F, 1.0F, 0.0F);
					GL.Rotate(f6, 1.0F, 0.0F, 0.0F);
					GL.Translate(0.375F * (float)(i19 * 2 - 1), 0.0F, 0.0F);
					GL.Translate(0.0F, -0.375F, 0.0F);
					GL.Rotate(-f6, 1.0F, 0.0F, 0.0F);
					GL.Rotate(-f5, 0.0F, 1.0F, 0.0F);
					float f7 = 1.3333334F;
					GL.Scale(f7, f7, f7);
					this.modelBipedMain.renderEars(0.0625F);
					GL.PopMatrix();
				}
			}

			float f10;
			if (this.loadDownloadableImageTexture(entityPlayer1.playerCloakUrl, (string)null))
			{
				GL.PushMatrix();
				GL.Translate(0.0F, 0.0F, 0.125F);
				double d20 = entityPlayer1.field_20066_r + (entityPlayer1.field_20063_u - entityPlayer1.field_20066_r) * (double)f2 - (entityPlayer1.prevPosX + (entityPlayer1.posX - entityPlayer1.prevPosX) * (double)f2);
				double d23 = entityPlayer1.field_20065_s + (entityPlayer1.field_20062_v - entityPlayer1.field_20065_s) * (double)f2 - (entityPlayer1.prevPosY + (entityPlayer1.posY - entityPlayer1.prevPosY) * (double)f2);
				double d8 = entityPlayer1.field_20064_t + (entityPlayer1.field_20061_w - entityPlayer1.field_20064_t) * (double)f2 - (entityPlayer1.prevPosZ + (entityPlayer1.posZ - entityPlayer1.prevPosZ) * (double)f2);
				f10 = entityPlayer1.prevRenderYawOffset + (entityPlayer1.renderYawOffset - entityPlayer1.prevRenderYawOffset) * f2;
				double d11 = (double)MathHelper.sin(f10 * (float)Math.PI / 180.0F);
				double d13 = (double)(-MathHelper.cos(f10 * (float)Math.PI / 180.0F));
				float f15 = (float)d23 * 10.0F;
				if (f15 < -6.0F)
				{
					f15 = -6.0F;
				}

				if (f15 > 32.0F)
				{
					f15 = 32.0F;
				}

				float f16 = (float)(d20 * d11 + d8 * d13) * 100.0F;
				float f17 = (float)(d20 * d13 - d8 * d11) * 100.0F;
				if (f16 < 0.0F)
				{
					f16 = 0.0F;
				}

				float f18 = entityPlayer1.prevCameraYaw + (entityPlayer1.cameraYaw - entityPlayer1.prevCameraYaw) * f2;
				f15 += MathHelper.sin((entityPlayer1.prevDistanceWalkedModified + (entityPlayer1.distanceWalkedModified - entityPlayer1.prevDistanceWalkedModified) * f2) * 6.0F) * 32.0F * f18;
				if (entityPlayer1.Sneaking)
				{
					f15 += 25.0F;
				}

				GL.Rotate(6.0F + f16 / 2.0F + f15, 1.0F, 0.0F, 0.0F);
				GL.Rotate(f17 / 2.0F, 0.0F, 0.0F, 1.0F);
				GL.Rotate(-f17 / 2.0F, 0.0F, 1.0F, 0.0F);
				GL.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
				this.modelBipedMain.renderCloak(0.0625F);
				GL.PopMatrix();
			}

			ItemStack itemStack21 = entityPlayer1.inventory.CurrentItem;
			if (itemStack21 != null)
			{
				GL.PushMatrix();
				this.modelBipedMain.bipedRightArm.postRender(0.0625F);
				GL.Translate(-0.0625F, 0.4375F, 0.0625F);
				if (entityPlayer1.fishEntity != null)
				{
					itemStack21 = new ItemStack(Item.stick);
				}

				EnumAction? enumAction22 = null;
				if (entityPlayer1.ItemInUseCount > 0)
				{
					enumAction22 = itemStack21.ItemUseAction;
				}

				if (itemStack21.itemID < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[itemStack21.itemID].RenderType))
				{
					f6 = 0.5F;
					GL.Translate(0.0F, 0.1875F, -0.3125F);
					f6 *= 0.75F;
					GL.Rotate(20.0F, 1.0F, 0.0F, 0.0F);
					GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
					GL.Scale(f6, -f6, f6);
				}
				else if (itemStack21.itemID == Item.bow.shiftedIndex)
				{
					f6 = 0.625F;
					GL.Translate(0.0F, 0.125F, 0.3125F);
					GL.Rotate(-20.0F, 0.0F, 1.0F, 0.0F);
					GL.Scale(f6, -f6, f6);
					GL.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
					GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
				}
				else if (Item.itemsList[itemStack21.itemID].Full3D)
				{
					f6 = 0.625F;
					if (Item.itemsList[itemStack21.itemID].shouldRotateAroundWhenRendering())
					{
						GL.Rotate(180.0F, 0.0F, 0.0F, 1.0F);
						GL.Translate(0.0F, -0.125F, 0.0F);
					}

					if (entityPlayer1.ItemInUseCount > 0 && enumAction22 == EnumAction.block)
					{
						GL.Translate(0.05F, 0.0F, -0.1F);
						GL.Rotate(-50.0F, 0.0F, 1.0F, 0.0F);
						GL.Rotate(-10.0F, 1.0F, 0.0F, 0.0F);
						GL.Rotate(-60.0F, 0.0F, 0.0F, 1.0F);
					}

					GL.Translate(0.0F, 0.1875F, 0.0F);
					GL.Scale(f6, -f6, f6);
					GL.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
					GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
				}
				else
				{
					f6 = 0.375F;
					GL.Translate(0.25F, 0.1875F, -0.1875F);
					GL.Scale(f6, f6, f6);
					GL.Rotate(60.0F, 0.0F, 0.0F, 1.0F);
					GL.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
					GL.Rotate(20.0F, 0.0F, 0.0F, 1.0F);
				}

				if (itemStack21.Item.func_46058_c())
				{
					for (int i25 = 0; i25 <= 1; ++i25)
					{
						int i24 = itemStack21.Item.getColorFromDamage(itemStack21.ItemDamage, i25);
						float f26 = (float)(i24 >> 16 & 255) / 255.0F;
						float f9 = (float)(i24 >> 8 & 255) / 255.0F;
						f10 = (float)(i24 & 255) / 255.0F;
						GL.Color4(f26, f9, f10, 1.0F);
						this.renderManager.itemRenderer.renderItem(entityPlayer1, itemStack21, i25);
					}
				}
				else
				{
					this.renderManager.itemRenderer.renderItem(entityPlayer1, itemStack21, 0);
				}
                
				GL.PopMatrix();
			}

		}

		protected internal virtual void renderPlayerScale(EntityPlayer entityPlayer1, float f2)
		{
			float f3 = 0.9375F;
			GL.Scale(f3, f3, f3);
		}

		public virtual void drawFirstPersonHand()
		{
			this.modelBipedMain.onGround = 0.0F;
			this.modelBipedMain.setRotationAngles(0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0625F);
			this.modelBipedMain.bipedRightArm.render(0.0625F);
		}

		protected internal virtual void renderPlayerSleep(EntityPlayer entityPlayer1, double d2, double d4, double d6)
		{
			if (entityPlayer1.EntityAlive && entityPlayer1.PlayerSleeping)
			{
				base.renderLivingAt(entityPlayer1, d2 + (double)entityPlayer1.field_22063_x, d4 + (double)entityPlayer1.field_22062_y, d6 + (double)entityPlayer1.field_22061_z);
			}
			else
			{
				base.renderLivingAt(entityPlayer1, d2, d4, d6);
			}

		}

		protected internal virtual void rotatePlayer(EntityPlayer entityPlayer1, float f2, float f3, float f4)
		{
			if (entityPlayer1.EntityAlive && entityPlayer1.PlayerSleeping)
			{
				GL.Rotate(entityPlayer1.BedOrientationInDegrees, 0.0F, 1.0F, 0.0F);
				GL.Rotate(this.getDeathMaxRotation(entityPlayer1), 0.0F, 0.0F, 1.0F);
				GL.Rotate(270.0F, 0.0F, 1.0F, 0.0F);
			}
			else
			{
				base.rotateCorpse(entityPlayer1, f2, f3, f4);
			}

		}

		protected internal override void passSpecialRender(EntityLiving entityLiving1, double d2, double d4, double d6)
		{
			this.renderName((EntityPlayer)entityLiving1, d2, d4, d6);
		}

		protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
		{
			this.renderPlayerScale((EntityPlayer)entityLiving1, f2);
		}

		protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
		{
			return this.setArmorModel((EntityPlayer)entityLiving1, i2, f3);
		}

		protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
		{
			this.renderSpecials((EntityPlayer)entityLiving1, f2);
		}

		protected internal override void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
		{
			this.rotatePlayer((EntityPlayer)entityLiving1, f2, f3, f4);
		}

		protected internal override void renderLivingAt(EntityLiving entityLiving1, double d2, double d4, double d6)
		{
			this.renderPlayerSleep((EntityPlayer)entityLiving1, d2, d4, d6);
		}

		public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
		{
			this.renderPlayer((EntityPlayer)entityLiving1, d2, d4, d6, f8, f9);
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.renderPlayer((EntityPlayer)entity1, d2, d4, d6, f8, f9);
		}
	}

}