using System.Collections;

namespace net.minecraft.src
{

	using GL11 = org.lwjgl.opengl.GL11;

	public class ModelRenderer
	{
		public float textureWidth;
		public float textureHeight;
		private int textureOffsetX;
		private int textureOffsetY;
		public float rotationPointX;
		public float rotationPointY;
		public float rotationPointZ;
		public float rotateAngleX;
		public float rotateAngleY;
		public float rotateAngleZ;
		private bool compiled;
		private int displayList;
		public bool mirror;
		public bool showModel;
		public bool isHidden;
		public System.Collections.IList cubeList;
		public System.Collections.IList childModels;
		public readonly string boxName;
		private ModelBase baseModel;

		public ModelRenderer(ModelBase modelBase1, string string2)
		{
			this.textureWidth = 64.0F;
			this.textureHeight = 32.0F;
			this.compiled = false;
			this.displayList = 0;
			this.mirror = false;
			this.showModel = true;
			this.isHidden = false;
			this.cubeList = new ArrayList();
			this.baseModel = modelBase1;
			modelBase1.boxList.Add(this);
			this.boxName = string2;
			this.setTextureSize(modelBase1.textureWidth, modelBase1.textureHeight);
		}

		public ModelRenderer(ModelBase modelBase1) : this(modelBase1, (string)null)
		{
		}

		public ModelRenderer(ModelBase modelBase1, int i2, int i3) : this(modelBase1)
		{
			this.setTextureOffset(i2, i3);
		}

		public virtual void addChild(ModelRenderer modelRenderer1)
		{
			if (this.childModels == null)
			{
				this.childModels = new ArrayList();
			}

			this.childModels.Add(modelRenderer1);
		}

		public virtual ModelRenderer setTextureOffset(int i1, int i2)
		{
			this.textureOffsetX = i1;
			this.textureOffsetY = i2;
			return this;
		}

		public virtual ModelRenderer addBox(string string1, float f2, float f3, float f4, int i5, int i6, int i7)
		{
			string1 = this.boxName + "." + string1;
			TextureOffset textureOffset8 = this.baseModel.getTextureOffset(string1);
			this.setTextureOffset(textureOffset8.field_40734_a, textureOffset8.field_40733_b);
			this.cubeList.Add((new ModelBox(this, this.textureOffsetX, this.textureOffsetY, f2, f3, f4, i5, i6, i7, 0.0F)).func_40671_a(string1));
			return this;
		}

		public virtual ModelRenderer addBox(float f1, float f2, float f3, int i4, int i5, int i6)
		{
			this.cubeList.Add(new ModelBox(this, this.textureOffsetX, this.textureOffsetY, f1, f2, f3, i4, i5, i6, 0.0F));
			return this;
		}

		public virtual void addBox(float f1, float f2, float f3, int i4, int i5, int i6, float f7)
		{
			this.cubeList.Add(new ModelBox(this, this.textureOffsetX, this.textureOffsetY, f1, f2, f3, i4, i5, i6, f7));
		}

		public virtual void setRotationPoint(float f1, float f2, float f3)
		{
			this.rotationPointX = f1;
			this.rotationPointY = f2;
			this.rotationPointZ = f3;
		}

		public virtual void render(float f1)
		{
			if (!this.isHidden)
			{
				if (this.showModel)
				{
					if (!this.compiled)
					{
						this.compileDisplayList(f1);
					}

					int i2;
					if (this.rotateAngleX == 0.0F && this.rotateAngleY == 0.0F && this.rotateAngleZ == 0.0F)
					{
						if (this.rotationPointX == 0.0F && this.rotationPointY == 0.0F && this.rotationPointZ == 0.0F)
						{
							GL11.glCallList(this.displayList);
							if (this.childModels != null)
							{
								for (i2 = 0; i2 < this.childModels.Count; ++i2)
								{
									((ModelRenderer)this.childModels[i2]).render(f1);
								}
							}
						}
						else
						{
							GL11.glTranslatef(this.rotationPointX * f1, this.rotationPointY * f1, this.rotationPointZ * f1);
							GL11.glCallList(this.displayList);
							if (this.childModels != null)
							{
								for (i2 = 0; i2 < this.childModels.Count; ++i2)
								{
									((ModelRenderer)this.childModels[i2]).render(f1);
								}
							}

							GL11.glTranslatef(-this.rotationPointX * f1, -this.rotationPointY * f1, -this.rotationPointZ * f1);
						}
					}
					else
					{
						GL11.glPushMatrix();
						GL11.glTranslatef(this.rotationPointX * f1, this.rotationPointY * f1, this.rotationPointZ * f1);
						if (this.rotateAngleZ != 0.0F)
						{
							GL11.glRotatef(this.rotateAngleZ * 57.295776F, 0.0F, 0.0F, 1.0F);
						}

						if (this.rotateAngleY != 0.0F)
						{
							GL11.glRotatef(this.rotateAngleY * 57.295776F, 0.0F, 1.0F, 0.0F);
						}

						if (this.rotateAngleX != 0.0F)
						{
							GL11.glRotatef(this.rotateAngleX * 57.295776F, 1.0F, 0.0F, 0.0F);
						}

						GL11.glCallList(this.displayList);
						if (this.childModels != null)
						{
							for (i2 = 0; i2 < this.childModels.Count; ++i2)
							{
								((ModelRenderer)this.childModels[i2]).render(f1);
							}
						}

						GL11.glPopMatrix();
					}

				}
			}
		}

		public virtual void renderWithRotation(float f1)
		{
			if (!this.isHidden)
			{
				if (this.showModel)
				{
					if (!this.compiled)
					{
						this.compileDisplayList(f1);
					}

					GL11.glPushMatrix();
					GL11.glTranslatef(this.rotationPointX * f1, this.rotationPointY * f1, this.rotationPointZ * f1);
					if (this.rotateAngleY != 0.0F)
					{
						GL11.glRotatef(this.rotateAngleY * 57.295776F, 0.0F, 1.0F, 0.0F);
					}

					if (this.rotateAngleX != 0.0F)
					{
						GL11.glRotatef(this.rotateAngleX * 57.295776F, 1.0F, 0.0F, 0.0F);
					}

					if (this.rotateAngleZ != 0.0F)
					{
						GL11.glRotatef(this.rotateAngleZ * 57.295776F, 0.0F, 0.0F, 1.0F);
					}

					GL11.glCallList(this.displayList);
					GL11.glPopMatrix();
				}
			}
		}

		public virtual void postRender(float f1)
		{
			if (!this.isHidden)
			{
				if (this.showModel)
				{
					if (!this.compiled)
					{
						this.compileDisplayList(f1);
					}

					if (this.rotateAngleX == 0.0F && this.rotateAngleY == 0.0F && this.rotateAngleZ == 0.0F)
					{
						if (this.rotationPointX != 0.0F || this.rotationPointY != 0.0F || this.rotationPointZ != 0.0F)
						{
							GL11.glTranslatef(this.rotationPointX * f1, this.rotationPointY * f1, this.rotationPointZ * f1);
						}
					}
					else
					{
						GL11.glTranslatef(this.rotationPointX * f1, this.rotationPointY * f1, this.rotationPointZ * f1);
						if (this.rotateAngleZ != 0.0F)
						{
							GL11.glRotatef(this.rotateAngleZ * 57.295776F, 0.0F, 0.0F, 1.0F);
						}

						if (this.rotateAngleY != 0.0F)
						{
							GL11.glRotatef(this.rotateAngleY * 57.295776F, 0.0F, 1.0F, 0.0F);
						}

						if (this.rotateAngleX != 0.0F)
						{
							GL11.glRotatef(this.rotateAngleX * 57.295776F, 1.0F, 0.0F, 0.0F);
						}
					}

				}
			}
		}

		private void compileDisplayList(float f1)
		{
			this.displayList = GLAllocation.generateDisplayLists(1);
			GL11.glNewList(this.displayList, GL11.GL_COMPILE);
			Tessellator tessellator2 = Tessellator.instance;

			for (int i3 = 0; i3 < this.cubeList.Count; ++i3)
			{
				((ModelBox)this.cubeList[i3]).render(tessellator2, f1);
			}

			GL11.glEndList();
			this.compiled = true;
		}

		public virtual ModelRenderer setTextureSize(int i1, int i2)
		{
			this.textureWidth = (float)i1;
			this.textureHeight = (float)i2;
			return this;
		}
	}

}