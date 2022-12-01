using System;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class RenderLiving : Render
	{
		protected internal ModelBase mainModel;
		protected internal ModelBase renderPassModel;

		public RenderLiving(ModelBase modelBase1, float f2)
		{
			this.mainModel = modelBase1;
			this.shadowSize = f2;
		}

		public virtual ModelBase RenderPassModel
		{
			set
			{
				this.renderPassModel = value;
			}
		}

		private float func_48418_a(float f1, float f2, float f3)
		{
			float f4;
			for (f4 = f2 - f1; f4 < -180.0F; f4 += 360.0F)
			{
			}

			while (f4 >= 180.0F)
			{
				f4 -= 360.0F;
			}

			return f1 + f3 * f4;
		}

		public virtual void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
		{
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            GL.Disable(EnableCap.CullFace);
			this.mainModel.onGround = this.renderSwingProgress(entityLiving1, f9);
			if (this.renderPassModel != null)
			{
				this.renderPassModel.onGround = this.mainModel.onGround;
			}

			this.mainModel.isRiding = entityLiving1.Riding;
			if (this.renderPassModel != null)
			{
				this.renderPassModel.isRiding = this.mainModel.isRiding;
			}

			this.mainModel.isChild = entityLiving1.Child;
			if (this.renderPassModel != null)
			{
				this.renderPassModel.isChild = this.mainModel.isChild;
			}

			try
			{
				float f10 = this.func_48418_a(entityLiving1.prevRenderYawOffset, entityLiving1.renderYawOffset, f9);
				float f11 = this.func_48418_a(entityLiving1.prevRotationYawHead, entityLiving1.rotationYawHead, f9);
				float f12 = entityLiving1.prevRotationPitch + (entityLiving1.rotationPitch - entityLiving1.prevRotationPitch) * f9;
				this.renderLivingAt(entityLiving1, d2, d4, d6);
				float f13 = this.handleRotationFloat(entityLiving1, f9);
				this.rotateCorpse(entityLiving1, f13, f10, f9);
				float f14 = 0.0625F;
				GL.Enable(EnableCap.RescaleNormal);
                Minecraft.newRenderer.ModelMatrix.Scale(-1.0F, -1.0F, 1.0F);
				this.preRenderCallback(entityLiving1, f9);
                Minecraft.newRenderer.ModelMatrix.Translate(0.0F, -24.0F * f14 - 0.0078125F, 0.0F);
				float f15 = entityLiving1.field_705_Q + (entityLiving1.field_704_R - entityLiving1.field_705_Q) * f9;
				float f16 = entityLiving1.field_703_S - entityLiving1.field_704_R * (1.0F - f9);
				if (entityLiving1.Child)
				{
					f16 *= 3.0F;
				}

				if (f15 > 1.0F)
				{
					f15 = 1.0F;
				}

				GL.Enable(EnableCap.AlphaTest);
				this.mainModel.setLivingAnimations(entityLiving1, f16, f15, f9);
				this.renderModel(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);

				int i18;
				float f19;
				float f20;
				float f22;
				for (int i17 = 0; i17 < 4; ++i17)
				{
					i18 = this.shouldRenderPass(entityLiving1, i17, f9);
					if (i18 > 0)
					{
						this.renderPassModel.setLivingAnimations(entityLiving1, f16, f15, f9);
						this.renderPassModel.render(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);
						if (i18 == 15)
						{
							f19 = (float)entityLiving1.ticksExisted + f9;
							this.loadTexture("%blur%/misc/glint.png");
							GL.Enable(EnableCap.Blend);
							f20 = 0.5F;
							GL.Color4(f20, f20, f20, 1.0F);
							GL.DepthFunc(DepthFunction.Equal);
							GL.DepthMask(false);

							for (int i21 = 0; i21 < 2; ++i21)
							{
								GL.Disable(EnableCap.Lighting);
								f22 = 0.76F;
								GL.Color4(0.5F * f22, 0.25F * f22, 0.8F * f22, 1.0F);
								GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
								GL.MatrixMode(MatrixMode.Texture);
                                Minecraft.newRenderer.TextureMatrix.LoadIdentity();
								float f23 = f19 * (0.001F + (float)i21 * 0.003F) * 20.0F;
								float f24 = 0.33333334F;
                                Minecraft.newRenderer.TextureMatrix.Scale(f24, f24, f24);
                                Minecraft.newRenderer.TextureMatrix.Rotate(30.0F - (float)i21 * 60.0F, 0.0F, 0.0F, 1.0F);
                                Minecraft.newRenderer.TextureMatrix.Translate(0.0F, f23, 0.0F);
								GL.MatrixMode(MatrixMode.Modelview);
								this.renderPassModel.render(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);
							}

							GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
							GL.MatrixMode(MatrixMode.Texture);
							GL.DepthMask(true);
                            Minecraft.newRenderer.TextureMatrix.LoadIdentity();
							GL.MatrixMode(MatrixMode.Modelview);
							GL.Enable(EnableCap.Lighting);
							GL.Disable(EnableCap.Blend);
							GL.DepthFunc(DepthFunction.Lequal);
						}

						GL.Disable(EnableCap.Blend);
						GL.Enable(EnableCap.AlphaTest);
					}
				}

				this.renderEquippedItems(entityLiving1, f9);
				float f26 = entityLiving1.getBrightness(f9);
				i18 = this.getColorMultiplier(entityLiving1, f26, f9);
				OpenGlHelper.ActiveTexture = OpenGlHelper.lightmapTexUnit;
				GL.Disable(EnableCap.Texture2D);
				OpenGlHelper.ActiveTexture = OpenGlHelper.defaultTexUnit;
				if ((i18 >> 24 & 255) > 0 || entityLiving1.hurtTime > 0 || entityLiving1.deathTime > 0)
				{
					GL.Disable(EnableCap.Texture2D);
					GL.Disable(EnableCap.AlphaTest);
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
					GL.DepthFunc(DepthFunction.Equal);
					if (entityLiving1.hurtTime > 0 || entityLiving1.deathTime > 0)
					{
						GL.Color4(f26, 0.0F, 0.0F, 0.4F);
						this.mainModel.render(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);

						for (int i27 = 0; i27 < 4; ++i27)
						{
							if (this.inheritRenderPass(entityLiving1, i27, f9) >= 0)
							{
								GL.Color4(f26, 0.0F, 0.0F, 0.4F);
								this.renderPassModel.render(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);
							}
						}
					}

					if ((i18 >> 24 & 255) > 0)
					{
						f19 = (float)(i18 >> 16 & 255) / 255.0F;
						f20 = (float)(i18 >> 8 & 255) / 255.0F;
						float f28 = (float)(i18 & 255) / 255.0F;
						f22 = (float)(i18 >> 24 & 255) / 255.0F;
						GL.Color4(f19, f20, f28, f22);
						this.mainModel.render(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);

						for (int i29 = 0; i29 < 4; ++i29)
						{
							if (this.inheritRenderPass(entityLiving1, i29, f9) >= 0)
							{
								GL.Color4(f19, f20, f28, f22);
								this.renderPassModel.render(entityLiving1, f16, f15, f13, f11 - f10, f12, f14);
							}
						}
					}

					GL.DepthFunc(DepthFunction.Lequal);
					GL.Disable(EnableCap.Blend);
					GL.Enable(EnableCap.AlphaTest);
					GL.Enable(EnableCap.Texture2D);
				}
                
				GL.Disable(EnableCap.RescaleNormal);
			}
			catch (Exception exception25)
			{
				Console.WriteLine(exception25.ToString());
				Console.Write(exception25.StackTrace);
			}

			OpenGlHelper.ActiveTexture = OpenGlHelper.lightmapTexUnit;
			GL.Enable(EnableCap.Texture2D);
			OpenGlHelper.ActiveTexture = OpenGlHelper.defaultTexUnit;
			GL.Enable(EnableCap.CullFace);
            Minecraft.newRenderer.ModelMatrix.PopMatrix();
			this.passSpecialRender(entityLiving1, d2, d4, d6);
		}

		protected internal virtual void renderModel(EntityLiving entityLiving1, float f2, float f3, float f4, float f5, float f6, float f7)
		{
			this.loadDownloadableImageTexture(entityLiving1.skinUrl, entityLiving1.Texture);
			this.mainModel.render(entityLiving1, f2, f3, f4, f5, f6, f7);
		}

		protected internal virtual void renderLivingAt(EntityLiving entityLiving1, double d2, double d4, double d6)
		{
            Minecraft.newRenderer.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
		}

		protected internal virtual void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
		{
            Minecraft.newRenderer.ModelMatrix.Rotate(180.0F - f3, 0.0F, 1.0F, 0.0F);
			if (entityLiving1.deathTime > 0)
			{
				float f5 = ((float)entityLiving1.deathTime + f4 - 1.0F) / 20.0F * 1.6F;
				f5 = MathHelper.sqrt_float(f5);
				if (f5 > 1.0F)
				{
					f5 = 1.0F;
				}

                Minecraft.newRenderer.ModelMatrix.Rotate(f5 * this.getDeathMaxRotation(entityLiving1), 0.0F, 0.0F, 1.0F);
			}

		}

		protected internal virtual float renderSwingProgress(EntityLiving entityLiving1, float f2)
		{
			return entityLiving1.getSwingProgress(f2);
		}

		protected internal virtual float handleRotationFloat(EntityLiving entityLiving1, float f2)
		{
			return (float)entityLiving1.ticksExisted + f2;
		}

		protected internal virtual void renderEquippedItems(EntityLiving entityLiving1, float f2)
		{
		}

		protected internal virtual int inheritRenderPass(EntityLiving entityLiving1, int i2, float f3)
		{
			return this.shouldRenderPass(entityLiving1, i2, f3);
		}

		protected internal virtual int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
		{
			return -1;
		}

		protected internal virtual float getDeathMaxRotation(EntityLiving entityLiving1)
		{
			return 90.0F;
		}

		protected internal virtual int getColorMultiplier(EntityLiving entityLiving1, float f2, float f3)
		{
			return 0;
		}

		protected internal virtual void preRenderCallback(EntityLiving entityLiving1, float f2)
		{
		}

		protected internal virtual void passSpecialRender(EntityLiving entityLiving1, double d2, double d4, double d6)
		{
			if (Minecraft.DebugInfoEnabled)
			{
				;
			}

		}

		protected internal virtual void renderLivingLabel(EntityLiving entityLiving1, string string2, double d3, double d5, double d7, int i9)
		{
			float f10 = entityLiving1.getDistanceToEntity(this.renderManager.livingPlayer);
			if (f10 <= (float)i9)
			{
				FontRenderer fontRenderer11 = this.FontRendererFromRenderManager;
				float f12 = 1.6F;
				float f13 = 0.016666668F * f12;
				Minecraft.newRenderer.ModelMatrix.PushMatrix();
                Minecraft.newRenderer.ModelMatrix.Translate((float)d3 + 0.0F, (float)d5 + 2.3F, (float)d7);
				GL.Normal3(0.0F, 1.0F, 0.0F);
				Minecraft.newRenderer.ModelMatrix.Rotate(-this.renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
				Minecraft.newRenderer.ModelMatrix.Rotate(this.renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Scale(-f13, -f13, f13);
				GL.Disable(EnableCap.Lighting);
				GL.DepthMask(false);
				GL.Disable(EnableCap.DepthTest);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				Tessellator tessellator14 = Tessellator.instance;
				sbyte b15 = 0;
				if (string2.Equals("deadmau5"))
				{
					b15 = -10;
				}

				GL.Disable(EnableCap.Texture2D);
				tessellator14.startDrawingQuads();
				int i16 = fontRenderer11.getStringWidth(string2) / 2;
				tessellator14.setColorRGBA_F(0.0F, 0.0F, 0.0F, 0.25F);
				tessellator14.addVertex((double)(-i16 - 1), (double)(-1 + b15), 0.0D);
				tessellator14.addVertex((double)(-i16 - 1), (double)(8 + b15), 0.0D);
				tessellator14.addVertex((double)(i16 + 1), (double)(8 + b15), 0.0D);
				tessellator14.addVertex((double)(i16 + 1), (double)(-1 + b15), 0.0D);
				tessellator14.draw();
				GL.Enable(EnableCap.Texture2D);
				fontRenderer11.drawString(string2, -fontRenderer11.getStringWidth(string2) / 2, b15, 553648127);
				GL.Enable(EnableCap.DepthTest);
				GL.DepthMask(true);
				fontRenderer11.drawString(string2, -fontRenderer11.getStringWidth(string2) / 2, b15, -1);
				GL.Enable(EnableCap.Lighting);
				GL.Disable(EnableCap.Blend);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
                Minecraft.newRenderer.ModelMatrix.PopMatrix();
			}
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.doRenderLiving((EntityLiving)entity1, d2, d4, d6, f8, f9);
		}
	}

}