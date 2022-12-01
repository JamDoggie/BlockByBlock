using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using System;

namespace net.minecraft.src
{
	public class RenderIronGolem : RenderLiving
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			field_48422_c = (ModelIronGolem)this.mainModel;
		}

		private ModelIronGolem field_48422_c;

		public RenderIronGolem() : base(new ModelIronGolem(), 0.5F)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
		}

		public virtual void func_48421_a(EntityIronGolem entityIronGolem1, double d2, double d4, double d6, float f8, float f9)
		{
			base.doRenderLiving(entityIronGolem1, d2, d4, d6, f8, f9);
		}

		protected internal virtual void func_48420_a(EntityIronGolem entityIronGolem1, float f2, float f3, float f4)
		{
			base.rotateCorpse(entityIronGolem1, f2, f3, f4);
			if ((double)entityIronGolem1.field_704_R >= 0.01D)
			{
				float f5 = 13.0F;
				float f6 = entityIronGolem1.field_703_S - entityIronGolem1.field_704_R * (1.0F - f4) + 6.0F;
				float f7 = (Math.Abs(f6 % f5 - f5 * 0.5F) - f5 * 0.25F) / (f5 * 0.25F);
                Minecraft.newRenderer.ModelMatrix.Rotate(6.5F * f7, 0.0F, 0.0F, 1.0F);
			}
		}

		protected internal virtual void func_48419_a(EntityIronGolem entityIronGolem1, float f2)
		{
			base.renderEquippedItems(entityIronGolem1, f2);
			if (entityIronGolem1.func_48117_D_() != 0)
			{
				GL.Enable(EnableCap.RescaleNormal);
				Minecraft.newRenderer.ModelMatrix.PushMatrix();
				Minecraft.newRenderer.ModelMatrix.Rotate(5.0F + 180.0F * this.field_48422_c.field_48233_c.rotateAngleX / (float)Math.PI, 1.0F, 0.0F, 0.0F);
				Minecraft.newRenderer.ModelMatrix.Translate(-0.6875F, 1.25F, -0.9375F);
                Minecraft.newRenderer.ModelMatrix.Rotate(90.0F, 1.0F, 0.0F, 0.0F);
				float f3 = 0.8F;
                Minecraft.newRenderer.ModelMatrix.Scale(f3, -f3, f3);
				int i4 = entityIronGolem1.getBrightnessForRender(f2);
				int i5 = i4 % 65536;
				int i6 = i4 / 65536;
				OpenGlHelper.setLightmapTextureCoords(OpenGlHelper.lightmapTexUnit, (float)i5 / 1.0F, (float)i6 / 1.0F);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
				this.loadTexture("/terrain.png");
				this.renderBlocks.renderBlockAsItem(Block.plantRed, 0, 1.0F);
                Minecraft.newRenderer.ModelMatrix.PopMatrix();
				GL.Disable(EnableCap.RescaleNormal);
			}
		}

		protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
		{
			this.func_48419_a((EntityIronGolem)entityLiving1, f2);
		}

		protected internal override void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
		{
			this.func_48420_a((EntityIronGolem)entityLiving1, f2, f3, f4);
		}

		public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
		{
			this.func_48421_a((EntityIronGolem)entityLiving1, d2, d4, d6, f8, f9);
		}

		public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			this.func_48421_a((EntityIronGolem)entity1, d2, d4, d6, f8, f9);
		}
	}

}