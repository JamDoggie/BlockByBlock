using BlockByBlock.java_extensions;
using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using System;

namespace net.minecraft.src
{

	public class RenderDragon : RenderLiving
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			modelDragon = (ModelDragon)this.mainModel;
		}

		public static EntityDragon entityDragon;
		private static int field_40284_d = 0;
		protected internal ModelDragon modelDragon;

		public RenderDragon() : base(new ModelDragon(0.0F), 0.5F)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.RenderPassModel = this.mainModel;
		}

		protected internal virtual void rotateDragonBody(EntityDragon entityDragon1, float f2, float f3, float f4)
		{
			float f5 = (float)entityDragon1.func_40160_a(7, f4)[0];
			float f6 = (float)(entityDragon1.func_40160_a(5, f4)[1] - entityDragon1.func_40160_a(10, f4)[1]);
            Minecraft.newRenderer.ModelMatrix.Rotate(-f5, 0.0F, 1.0F, 0.0F);
            Minecraft.newRenderer.ModelMatrix.Rotate(f6 * 10.0F, 1.0F, 0.0F, 0.0F);
            Minecraft.newRenderer.ModelMatrix.Translate(0.0F, 0.0F, 1.0F);
			if (entityDragon1.deathTime > 0)
			{
				float f7 = ((float)entityDragon1.deathTime + f4 - 1.0F) / 20.0F * 1.6F;
				f7 = MathHelper.sqrt_float(f7);
				if (f7 > 1.0F)
				{
					f7 = 1.0F;
				}

                Minecraft.newRenderer.ModelMatrix.Rotate(f7 * this.getDeathMaxRotation(entityDragon1), 0.0F, 0.0F, 1.0F);
			}

		}

		protected internal virtual void func_40280_a(EntityDragon entityDragon1, float f2, float f3, float f4, float f5, float f6, float f7)
		{
			if (entityDragon1.field_40178_aA > 0)
			{
				float f8 = (float)entityDragon1.field_40178_aA / 200.0F;
				GL.DepthFunc(DepthFunction.Lequal);
				GL.Enable(EnableCap.AlphaTest);
				GL.AlphaFunc(AlphaFunction.Greater, f8);
				this.loadDownloadableImageTexture(entityDragon1.skinUrl, "/mob/enderdragon/shuffle.png");
				this.mainModel.render(entityDragon1, f2, f3, f4, f5, f6, f7);
				GL.AlphaFunc(AlphaFunction.Greater, 0.1F);
				GL.DepthFunc(DepthFunction.Equal);
			}

			this.loadDownloadableImageTexture(entityDragon1.skinUrl, entityDragon1.Texture);
			this.mainModel.render(entityDragon1, f2, f3, f4, f5, f6, f7);
			if (entityDragon1.hurtTime > 0)
			{
				GL.DepthFunc(DepthFunction.Equal);
				GL.Disable(EnableCap.Texture2D);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				GL.Color4(1.0F, 0.0F, 0.0F, 0.5F);
				this.mainModel.render(entityDragon1, f2, f3, f4, f5, f6, f7);
				GL.Enable(EnableCap.Texture2D);
				GL.Disable(EnableCap.Blend);
				GL.DepthFunc(DepthFunction.Lequal);
			}

		}

		public virtual void renderDragon(EntityDragon entityDragon1, double d2, double d4, double d6, float f8, float f9)
		{
			entityDragon = entityDragon1;
			if (field_40284_d != 4)
			{
				this.mainModel = new ModelDragon(0.0F);
				field_40284_d = 4;
			}

			base.doRenderLiving(entityDragon1, d2, d4, d6, f8, f9);
			if (entityDragon1.healingEnderCrystal != null)
			{
				float f10 = (float)entityDragon1.healingEnderCrystal.innerRotation + f9;
				float f11 = MathHelper.sin(f10 * 0.2F) / 2.0F + 0.5F;
				f11 = (f11 * f11 + f11) * 0.2F;
				float f12 = (float)(entityDragon1.healingEnderCrystal.posX - entityDragon1.posX - (entityDragon1.prevPosX - entityDragon1.posX) * (double)(1.0F - f9));
				float f13 = (float)((double)f11 + entityDragon1.healingEnderCrystal.posY - 1.0D - entityDragon1.posY - (entityDragon1.prevPosY - entityDragon1.posY) * (double)(1.0F - f9));
				float f14 = (float)(entityDragon1.healingEnderCrystal.posZ - entityDragon1.posZ - (entityDragon1.prevPosZ - entityDragon1.posZ) * (double)(1.0F - f9));
				float f15 = MathHelper.sqrt_float(f12 * f12 + f14 * f14);
				float f16 = MathHelper.sqrt_float(f12 * f12 + f13 * f13 + f14 * f14);
                Minecraft.newRenderer.ModelMatrix.PushMatrix();
                Minecraft.newRenderer.ModelMatrix.Translate((float)d2, (float)d4 + 2.0F, (float)d6);
                Minecraft.newRenderer.ModelMatrix.Rotate((float)(-Math.Atan2((double)f14, (double)f12)) * 180.0F / (float)Math.PI - 90.0F, 0.0F, 1.0F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Rotate((float)(-Math.Atan2((double)f15, (double)f13)) * 180.0F / (float)Math.PI - 90.0F, 1.0F, 0.0F, 0.0F);
				Tessellator tessellator17 = Tessellator.instance;
				RenderHelper.disableStandardItemLighting();
				GL.Disable(EnableCap.CullFace);
				this.loadTexture("/mob/enderdragon/beam.png");
				GL.ShadeModel(ShadingModel.Smooth);
				float f18 = 0.0F - ((float)entityDragon1.ticksExisted + f9) * 0.01F;
				float f19 = MathHelper.sqrt_float(f12 * f12 + f13 * f13 + f14 * f14) / 32.0F - ((float)entityDragon1.ticksExisted + f9) * 0.01F;
				tessellator17.startDrawing(5);
				sbyte b20 = 8;

				for (int i21 = 0; i21 <= b20; ++i21)
				{
					float f22 = MathHelper.sin((float)(i21 % b20) * (float)Math.PI * 2.0F / (float)b20) * 0.75F;
					float f23 = MathHelper.cos((float)(i21 % b20) * (float)Math.PI * 2.0F / (float)b20) * 0.75F;
					float f24 = (float)(i21 % b20) * 1.0F / (float)b20;
					tessellator17.ColorOpaque_I = 0;
					tessellator17.addVertexWithUV((double)(f22 * 0.2F), (double)(f23 * 0.2F), 0.0D, (double)f24, (double)f19);
					tessellator17.ColorOpaque_I = 0xFFFFFF;
					tessellator17.addVertexWithUV((double)f22, (double)f23, (double)f16, (double)f24, (double)f18);
				}

				tessellator17.draw();
				GL.Enable(EnableCap.CullFace);
				GL.ShadeModel(ShadingModel.Flat); ;
				RenderHelper.enableStandardItemLighting();
                Minecraft.newRenderer.ModelMatrix.PopMatrix();
			}

		}

		protected internal virtual void renderDragonDying(EntityDragon entityDragon1, float f2)
		{
			base.renderEquippedItems(entityDragon1, f2);
			Tessellator tessellator3 = Tessellator.instance;
			if (entityDragon1.field_40178_aA > 0)
			{
				RenderHelper.disableStandardItemLighting();
				float f4 = ((float)entityDragon1.field_40178_aA + f2) / 200.0F;
				float f5 = 0.0F;
				if (f4 > 0.8F)
				{
					f5 = (f4 - 0.8F) / 0.2F;
				}

				RandomExtended random6 = new RandomExtended(432L);
				GL.Disable(EnableCap.Texture2D);
				GL.ShadeModel(ShadingModel.Smooth);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
				GL.Disable(EnableCap.AlphaTest);
				GL.Enable(EnableCap.CullFace);
				GL.DepthMask(false);
                Minecraft.newRenderer.ModelMatrix.PushMatrix();
                Minecraft.newRenderer.ModelMatrix.Translate(0.0F, -1.0F, -2.0F);

				for (int i7 = 0; (float)i7 < (f4 + f4 * f4) / 2.0F * 60.0F; ++i7)
				{
					Minecraft.newRenderer.ModelMatrix.Rotate(random6.NextSingle() * 360.0F, 1.0F, 0.0F, 0.0F);
					Minecraft.newRenderer.ModelMatrix.Rotate(random6.NextSingle() * 360.0F, 0.0F, 1.0F, 0.0F);
					Minecraft.newRenderer.ModelMatrix.Rotate(random6.NextSingle() * 360.0F, 0.0F, 0.0F, 1.0F);
					Minecraft.newRenderer.ModelMatrix.Rotate(random6.NextSingle() * 360.0F, 1.0F, 0.0F, 0.0F);
					Minecraft.newRenderer.ModelMatrix.Rotate(random6.NextSingle() * 360.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.newRenderer.ModelMatrix.Rotate(random6.NextSingle() * 360.0F + f4 * 90.0F, 0.0F, 0.0F, 1.0F);
					tessellator3.startDrawing(6);
					float f8 = random6.NextSingle() * 20.0F + 5.0F + f5 * 10.0F;
					float f9 = random6.NextSingle() * 2.0F + 1.0F + f5 * 2.0F;
					tessellator3.setColorRGBA_I(0xFFFFFF, (int)(255.0F * (1.0F - f5)));
					tessellator3.addVertex(0.0D, 0.0D, 0.0D);
					tessellator3.setColorRGBA_I(16711935, 0);
					tessellator3.addVertex(-0.866D * (double)f9, (double)f8, (double)(-0.5F * f9));
					tessellator3.addVertex(0.866D * (double)f9, (double)f8, (double)(-0.5F * f9));
					tessellator3.addVertex(0.0D, (double)f8, (double)(1.0F * f9));
					tessellator3.addVertex(-0.866D * (double)f9, (double)f8, (double)(-0.5F * f9));
					tessellator3.draw();
				}

                Minecraft.newRenderer.ModelMatrix.PopMatrix();
				GL.DepthMask(true);
				GL.Disable(EnableCap.CullFace);
				GL.Disable(EnableCap.Blend);
				GL.ShadeModel(ShadingModel.Flat);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				GL.Enable(EnableCap.Texture2D);
				GL.Enable(EnableCap.AlphaTest);
				RenderHelper.enableStandardItemLighting();
			}

		}

		protected internal virtual int func_40283_a(EntityDragon entityDragon1, int i2, float f3)
		{
			if (i2 == 1)
			{
				GL.DepthFunc(DepthFunction.Lequal);
			}

			if (i2 != 0)
			{
				return -1;
			}
			else
			{
				this.loadTexture("/mob/enderdragon/ender_eyes.png");
				float f4 = 1.0F;
				GL.Enable(EnableCap.Blend);
                GL.Disable(EnableCap.AlphaTest);
				GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
				GL.Disable(EnableCap.Lighting);
				GL.DepthFunc(DepthFunction.Equal);
				int i5 = 61680;
				int i6 = i5 % 65536;
				int i7 = i5 / 65536;
				OpenGlHelper.setLightmapTextureCoords(OpenGlHelper.lightmapTexUnit, (float)i6 / 1.0F, (float)i7 / 1.0F);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				GL.Enable(EnableCap.Lighting);
				GL.Color4(1.0F, 1.0F, 1.0F, f4);
				return 1;
			}
		}

		protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
		{
			return this.func_40283_a((EntityDragon)entityLiving1, i2, f3);
		}

		protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
		{
			this.renderDragonDying((EntityDragon)entityLiving1, f2);
		}

		protected internal override void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
		{
			this.rotateDragonBody((EntityDragon)entityLiving1, f2, f3, f4);
		}

		protected internal override void renderModel(EntityLiving entityLiving1, float f2, float f3, float f4, float f5, float f6, float f7)
		{
			this.func_40280_a((EntityDragon)entityLiving1, f2, f3, f4, f5, f6, f7);
		}

		public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
		{
			this.renderDragon((EntityDragon)entityLiving1, d2, d4, d6, f8, f9);
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.renderDragon((EntityDragon)entity1, d2, d4, d6, f8, f9);
		}
	}

}