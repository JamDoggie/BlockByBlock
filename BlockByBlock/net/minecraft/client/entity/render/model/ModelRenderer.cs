using System.Collections;
using BlockByBlock.net.minecraft.client.entity.render.model;
using net.minecraft.client;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render.model
{

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
        private int displayList;
        public bool mirror;
        public bool showModel;
        public bool isHidden;
        public IList cubeList;
        public IList childModels;
        public readonly string boxName;
        private ModelBase baseModel;


        public ModelRenderer(ModelBase modelBase1, string string2)
        {
            textureWidth = 64.0F;
            textureHeight = 32.0F;
            displayList = 0;
            mirror = false;
            showModel = true;
            isHidden = false;
            cubeList = new ArrayList();
            baseModel = modelBase1;
            modelBase1.boxList.Add(this);
            boxName = string2;
            setTextureSize(modelBase1.textureWidth, modelBase1.textureHeight);
        }

        public ModelRenderer(ModelBase modelBase1) : this(modelBase1, null)
        {
        }

        public ModelRenderer(ModelBase modelBase1, int i2, int i3) : this(modelBase1)
        {
            setTextureOffset(i2, i3);
        }

        public virtual void addChild(ModelRenderer modelRenderer1)
        {
            if (childModels == null)
            {
                childModels = new ArrayList();
            }

            childModels.Add(modelRenderer1);
        }

        public virtual ModelRenderer setTextureOffset(int i1, int i2)
        {
            textureOffsetX = i1;
            textureOffsetY = i2;
            return this;
        }

        public virtual ModelRenderer addBox(string string1, float f2, float f3, float f4, int i5, int i6, int i7)
        {
            string1 = boxName + "." + string1;
            TextureOffset textureOffset8 = baseModel.getTextureOffset(string1);
            setTextureOffset(textureOffset8.field_40734_a, textureOffset8.field_40733_b);
            cubeList.Add(new ModelBox(this, textureOffsetX, textureOffsetY, f2, f3, f4, i5, i6, i7, 0.0F).SetName(string1));
            return this;
        }

        public virtual ModelRenderer addBox(float f1, float f2, float f3, int i4, int i5, int i6)
        {
            cubeList.Add(new ModelBox(this, textureOffsetX, textureOffsetY, f1, f2, f3, i4, i5, i6, 0.0F));
            return this;
        }

        public virtual void addBox(float f1, float f2, float f3, int i4, int i5, int i6, float f7)
        {
            cubeList.Add(new ModelBox(this, textureOffsetX, textureOffsetY, f1, f2, f3, i4, i5, i6, f7));
        }

        public virtual void setRotationPoint(float f1, float f2, float f3)
        {
            rotationPointX = f1;
            rotationPointY = f2;
            rotationPointZ = f3;
        }

        public virtual void render(float f1)
        {
            if (!isHidden)
            {
                if (showModel)
                {
                    int i2;
                    if (rotateAngleX == 0.0F && rotateAngleY == 0.0F && rotateAngleZ == 0.0F)
                    {
                        if (rotationPointX == 0.0F && rotationPointY == 0.0F && rotationPointZ == 0.0F)
                        {
                            TessellateShapes(f1);
                            if (childModels != null)
                            {
                                for (i2 = 0; i2 < childModels.Count; ++i2)
                                {
                                    ((ModelRenderer)childModels[i2]).render(f1);
                                }
                            }
                        }
                        else
                        {
                            Minecraft.renderPipeline.ModelMatrix.Translate(rotationPointX * f1, rotationPointY * f1, rotationPointZ * f1);
                            TessellateShapes(f1);
                            if (childModels != null)
                            {
                                for (i2 = 0; i2 < childModels.Count; ++i2)
                                {
                                    ((ModelRenderer)childModels[i2]).render(f1);
                                }
                            }

                            Minecraft.renderPipeline.ModelMatrix.Translate(-rotationPointX * f1, -rotationPointY * f1, -rotationPointZ * f1);
                        }
                    }
                    else
                    {
                        Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                        Minecraft.renderPipeline.ModelMatrix.Translate(rotationPointX * f1, rotationPointY * f1, rotationPointZ * f1);
                        if (rotateAngleZ != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleZ * 57.295776F, 0.0F, 0.0F, 1.0F);
                        }

                        if (rotateAngleY != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleY * 57.295776F, 0.0F, 1.0F, 0.0F);
                        }

                        if (rotateAngleX != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleX * 57.295776F, 1.0F, 0.0F, 0.0F);
                        }

                        TessellateShapes(f1);
                        if (childModels != null)
                        {
                            for (i2 = 0; i2 < childModels.Count; ++i2)
                            {
                                ((ModelRenderer)childModels[i2]).render(f1);
                            }
                        }

                        Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                    }

                }
            }
        }

        public virtual void renderWithRotation(float f1)
        {
            if (!isHidden)
            {
                if (showModel)
                {
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Translate(rotationPointX * f1, rotationPointY * f1, rotationPointZ * f1);
                    if (rotateAngleY != 0.0F)
                    {
                        Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleY * 57.295776F, 0.0F, 1.0F, 0.0F);
                    }

                    if (rotateAngleX != 0.0F)
                    {
                        Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleX * 57.295776F, 1.0F, 0.0F, 0.0F);
                    }

                    if (rotateAngleZ != 0.0F)
                    {
                        Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleZ * 57.295776F, 0.0F, 0.0F, 1.0F);
                    }

                    TessellateShapes(f1);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                }
            }
        }

        public virtual void postRender(float f1)
        {
            if (!isHidden)
            {
                if (showModel)
                {
                    if (rotateAngleX == 0.0F && rotateAngleY == 0.0F && rotateAngleZ == 0.0F)
                    {
                        if (rotationPointX != 0.0F || rotationPointY != 0.0F || rotationPointZ != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Translate(rotationPointX * f1, rotationPointY * f1, rotationPointZ * f1);
                        }
                    }
                    else
                    {
                        Minecraft.renderPipeline.ModelMatrix.Translate(rotationPointX * f1, rotationPointY * f1, rotationPointZ * f1);
                        if (rotateAngleZ != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleZ * 57.295776F, 0.0F, 0.0F, 1.0F);
                        }

                        if (rotateAngleY != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleY * 57.295776F, 0.0F, 1.0F, 0.0F);
                        }

                        if (rotateAngleX != 0.0F)
                        {
                            Minecraft.renderPipeline.ModelMatrix.Rotate(rotateAngleX * 57.295776F, 1.0F, 0.0F, 0.0F);
                        }
                    }

                }
            }
        }

        private void TessellateShapes(float f1)
        {
            Tessellator tessellator2 = Tessellator.instance;

            for (int i3 = 0; i3 < cubeList.Count; ++i3)
            {
                ((ModelBox)cubeList[i3]).render(tessellator2, f1);
            }
        }

        public virtual ModelRenderer setTextureSize(int i1, int i2)
        {
            textureWidth = i1;
            textureHeight = i2;
            return this;
        }
    }

}