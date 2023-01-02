using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;

namespace BlockByBlock.net.minecraft.client.entity.render.model;

public class ModelBox
{
    private PositionTextureVertex[] vertexPositions;
    private TexturedQuad[] quadList;
    private VertexBuffer? currentVBO = null;

    public readonly float posX1;
    public readonly float posY1;
    public readonly float posZ1;
    public readonly float posX2;
    public readonly float posY2;
    public readonly float posZ2;
    public string name;

    public ModelBox(ModelRenderer modelRenderer1, int i2, int i3, float f4, float f5, float f6, int i7, int i8, int i9, float f10)
    {
        posX1 = f4;
        posY1 = f5;
        posZ1 = f6;
        posX2 = f4 + i7;
        posY2 = f5 + i8;
        posZ2 = f6 + i9;
        vertexPositions = new PositionTextureVertex[8];
        quadList = new TexturedQuad[6];
        float f11 = f4 + i7;
        float f12 = f5 + i8;
        float f13 = f6 + i9;
        f4 -= f10;
        f5 -= f10;
        f6 -= f10;
        f11 += f10;
        f12 += f10;
        f13 += f10;
        if (modelRenderer1.mirror)
        {
            float f14 = f11;
            f11 = f4;
            f4 = f14;
        }

        PositionTextureVertex positionTextureVertex23 = new(f4, f5, f6, 0.0F, 0.0F);
        PositionTextureVertex positionTextureVertex15 = new(f11, f5, f6, 0.0F, 8.0F);
        PositionTextureVertex positionTextureVertex16 = new(f11, f12, f6, 8.0F, 8.0F);
        PositionTextureVertex positionTextureVertex17 = new(f4, f12, f6, 8.0F, 0.0F);
        PositionTextureVertex positionTextureVertex18 = new(f4, f5, f13, 0.0F, 0.0F);
        PositionTextureVertex positionTextureVertex19 = new(f11, f5, f13, 0.0F, 8.0F);
        PositionTextureVertex positionTextureVertex20 = new(f11, f12, f13, 8.0F, 8.0F);
        PositionTextureVertex positionTextureVertex21 = new(f4, f12, f13, 8.0F, 0.0F);
        vertexPositions[0] = positionTextureVertex23;
        vertexPositions[1] = positionTextureVertex15;
        vertexPositions[2] = positionTextureVertex16;
        vertexPositions[3] = positionTextureVertex17;
        vertexPositions[4] = positionTextureVertex18;
        vertexPositions[5] = positionTextureVertex19;
        vertexPositions[6] = positionTextureVertex20;
        vertexPositions[7] = positionTextureVertex21;
        quadList[0] = new TexturedQuad(new PositionTextureVertex[] { positionTextureVertex19, positionTextureVertex15, positionTextureVertex16, positionTextureVertex20 }, i2 + i9 + i7, i3 + i9, i2 + i9 + i7 + i9, i3 + i9 + i8, modelRenderer1.textureWidth, modelRenderer1.textureHeight);
        quadList[1] = new TexturedQuad(new PositionTextureVertex[] { positionTextureVertex23, positionTextureVertex18, positionTextureVertex21, positionTextureVertex17 }, i2, i3 + i9, i2 + i9, i3 + i9 + i8, modelRenderer1.textureWidth, modelRenderer1.textureHeight);
        quadList[2] = new TexturedQuad(new PositionTextureVertex[] { positionTextureVertex19, positionTextureVertex18, positionTextureVertex23, positionTextureVertex15 }, i2 + i9, i3, i2 + i9 + i7, i3 + i9, modelRenderer1.textureWidth, modelRenderer1.textureHeight);
        quadList[3] = new TexturedQuad(new PositionTextureVertex[] { positionTextureVertex16, positionTextureVertex17, positionTextureVertex21, positionTextureVertex20 }, i2 + i9 + i7, i3 + i9, i2 + i9 + i7 + i7, i3, modelRenderer1.textureWidth, modelRenderer1.textureHeight);
        quadList[4] = new TexturedQuad(new PositionTextureVertex[] { positionTextureVertex15, positionTextureVertex23, positionTextureVertex17, positionTextureVertex16 }, i2 + i9, i3 + i9, i2 + i9 + i7, i3 + i9 + i8, modelRenderer1.textureWidth, modelRenderer1.textureHeight);
        quadList[5] = new TexturedQuad(new PositionTextureVertex[] { positionTextureVertex18, positionTextureVertex19, positionTextureVertex20, positionTextureVertex21 }, i2 + i9 + i7 + i9, i3 + i9, i2 + i9 + i7 + i9 + i7, i3 + i9 + i8, modelRenderer1.textureWidth, modelRenderer1.textureHeight);
        if (modelRenderer1.mirror)
        {
            for (int i22 = 0; i22 < quadList.Length; ++i22)
            {
                quadList[i22].FlipFace();
            }
        }

        // Build the vertex buffer for this model box.
        currentVBO = BuildVBO();
    }

    public virtual void render(Tessellator tessellator, float f2)
    {
        if (currentVBO != null)
        {
            MatrixStack modelView = Minecraft.renderPipeline.ModelMatrix;

            modelView.PushMatrix();
            modelView.Scale(f2);
            
            tessellator.Draw(currentVBO.Value);

            modelView.PopMatrix();
        }
    }

    protected virtual VertexBuffer BuildVBO()
    {
        Tessellator tessellator = Tessellator.instance;

        tessellator.StartBuildingVBO();

        for (int i = 0; i < quadList.Length; ++i)
        {
            quadList[i].AddToCurrentBuildingVBO(tessellator);
        }

        return tessellator.BuildCurrentVBO();
    }

    public virtual ModelBox SetName(string name)
    {
        this.name = name;
        return this;
    }
}