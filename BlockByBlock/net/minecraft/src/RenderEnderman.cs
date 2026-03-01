using System;
using BlockByBlock.java_extensions;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class RenderEnderman : RenderLiving
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			endermanModel = (ModelEnderman)this.mainModel;
		}

		private ModelEnderman endermanModel;
		private RandomExtended rnd = new RandomExtended();

		public RenderEnderman() : base(new ModelEnderman(), 0.5F)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.RenderPassModel = this.endermanModel;
		}

		public virtual void renderEnderman(EntityEnderman entityEnderman1, double d2, double d4, double d6, float f8, float f9)
		{
			this.endermanModel.isCarrying = entityEnderman1.Carried > 0;
			this.endermanModel.isAttacking = entityEnderman1.isAttacking;
			if (entityEnderman1.isAttacking)
			{
				double d10 = 0.02D;
				d2 += this.rnd.NextGaussian() * d10;
				d6 += this.rnd.NextGaussian() * d10;
			}

			base.doRenderLiving(entityEnderman1, d2, d4, d6, f8, f9);
		}

		protected internal virtual void renderCarrying(EntityEnderman entityEnderman1, float f2)
		{
			base.renderEquippedItems(entityEnderman1, f2);
			if (entityEnderman1.Carried > 0)
			{
				GL.Enable(EnableCap.RescaleNormal);
				GL.PushMatrix();
				float f3 = 0.5F;
				GL.Translate(0.0F, 0.6875F, -0.75F);
				f3 *= 1.0F;
				GL.Rotate(20.0F, 1.0F, 0.0F, 0.0F);
				GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
				GL.Scale(f3, -f3, f3);
				int i4 = entityEnderman1.getBrightnessForRender(f2);
				int i5 = i4 % 65536;
				int i6 = i4 / 65536;
				OpenGlHelper.setLightmapTextureCoords(OpenGlHelper.lightmapTexUnit, (float)i5 / 1.0F, (float)i6 / 1.0F);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				this.loadTexture("/terrain.png");
				this.renderBlocks.renderBlockAsItem(Block.blocksList[entityEnderman1.Carried], entityEnderman1.CarryingData, 1.0F);
				GL.PopMatrix();
				GL.Disable(EnableCap.RescaleNormal);
			}

		}

		protected internal virtual int renderEyes(EntityEnderman entityEnderman1, int i2, float f3)
		{
			if (i2 != 0)
			{
				return -1;
			}
			else
			{
				this.loadTexture("/mob/enderman_eyes.png");
				float f4 = 1.0F;
				GL.Enable(EnableCap.Blend);
				GL.Disable(EnableCap.AlphaTest);
				GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
				GL.Disable(EnableCap.Lighting);
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
			return this.renderEyes((EntityEnderman)entityLiving1, i2, f3);
		}

		protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
		{
			this.renderCarrying((EntityEnderman)entityLiving1, f2);
		}

		public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
		{
			this.renderEnderman((EntityEnderman)entityLiving1, d2, d4, d6, f8, f9);
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.renderEnderman((EntityEnderman)entity1, d2, d4, d6, f8, f9);
		}
	}

}