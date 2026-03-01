using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class ModelEnderCrystal : ModelBase
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			field_41058_h = new ModelRenderer(this, "glass");
		}

		private ModelRenderer field_41057_g;
		private ModelRenderer field_41058_h;
		private ModelRenderer field_41059_i;

		public ModelEnderCrystal(float f1)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.field_41058_h.setTextureOffset(0, 0).addBox(-4.0F, -4.0F, -4.0F, 8, 8, 8);
			this.field_41057_g = new ModelRenderer(this, "cube");
			this.field_41057_g.setTextureOffset(32, 0).addBox(-4.0F, -4.0F, -4.0F, 8, 8, 8);
			this.field_41059_i = new ModelRenderer(this, "base");
			this.field_41059_i.setTextureOffset(0, 16).addBox(-6.0F, 0.0F, -6.0F, 12, 4, 12);
		}

		public override void render(Entity entity1, float f2, float f3, float f4, float f5, float f6, float f7)
		{
			GL.PushMatrix();
			GL.Scale(2.0F, 2.0F, 2.0F);
			GL.Translate(0.0F, -0.5F, 0.0F);
			this.field_41059_i.render(f7);
			GL.Rotate(f3, 0.0F, 1.0F, 0.0F);
			GL.Translate(0.0F, 0.8F + f4, 0.0F);
			GL.Rotate(60.0F, 0.7071F, 0.0F, 0.7071F);
			this.field_41058_h.render(f7);
			float f8 = 0.875F;
			GL.Scale(f8, f8, f8);
			GL.Rotate(60.0F, 0.7071F, 0.0F, 0.7071F);
			GL.Rotate(f3, 0.0F, 1.0F, 0.0F);
			this.field_41058_h.render(f7);
			GL.Scale(f8, f8, f8);
			GL.Rotate(60.0F, 0.7071F, 0.0F, 0.7071F);
			GL.Rotate(f3, 0.0F, 1.0F, 0.0F);
			this.field_41057_g.render(f7);
			GL.PopMatrix();
		}

		public override void setRotationAngles(float f1, float f2, float f3, float f4, float f5, float f6)
		{
			base.setRotationAngles(f1, f2, f3, f4, f5, f6);
		}
	}

}