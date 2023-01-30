using System;
using BlockByBlock.java_extensions;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{

    public class RenderItem : Renderer
    {
        private new RenderBlocks renderBlocks = new RenderBlocks();
        private RandomExtended random = new RandomExtended();
        public bool field_27004_a = true;
        public float zLevel = 0.0F;

        public RenderItem()
        {
            shadowSize = 0.15F;
            shadowOpaque = 0.75F;
        }

        public virtual void doRenderItem(EntityItem entityItem1, double d2, double d4, double d6, float f8, float f9)
        {
            random.SetSeed(187L);
            ItemStack itemStack10 = entityItem1.item;
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            float f11 = MathHelper.sin((entityItem1.age + f9) / 10.0F + entityItem1.field_804_d) * 0.1F + 0.1F;
            float f12 = ((entityItem1.age + f9) / 20.0F + entityItem1.field_804_d) * 57.295776F;
            sbyte b13 = 1;
            if (entityItem1.item.stackSize > 1)
            {
                b13 = 2;
            }

            if (entityItem1.item.stackSize > 5)
            {
                b13 = 3;
            }

            if (entityItem1.item.stackSize > 20)
            {
                b13 = 4;
            }

            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4 + f11, (float)d6);
            int i15;
            float f18;
            float f19;
            float f23;
            if (itemStack10.itemID < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[itemStack10.itemID].RenderType))
            {
                Minecraft.renderPipeline.ModelMatrix.Rotate(f12, 0.0F, 1.0F, 0.0F);
                loadTexture("/terrain.png");
                float f21 = 0.25F;
                i15 = Block.blocksList[itemStack10.itemID].RenderType;
                if (i15 == 1 || i15 == 19 || i15 == 12 || i15 == 2)
                {
                    f21 = 0.5F;
                }

                Minecraft.renderPipeline.ModelMatrix.Scale(f21, f21, f21);

                for (int i22 = 0; i22 < b13; ++i22)
                {
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    if (i22 > 0)
                    {
                        f23 = (random.NextSingle() * 2.0F - 1.0F) * 0.2F / f21;
                        f18 = (random.NextSingle() * 2.0F - 1.0F) * 0.2F / f21;
                        f19 = (random.NextSingle() * 2.0F - 1.0F) * 0.2F / f21;
                        Minecraft.renderPipeline.ModelMatrix.Translate(f23, f18, f19);
                    }

                    f23 = 1.0F;
                    renderBlocks.renderBlockAsItem(Block.blocksList[itemStack10.itemID], itemStack10.ItemDamage, f23);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                }
            }
            else
            {
                int i14;
                float f16;
                if (itemStack10.Item.func_46058_c())
                {
                    Minecraft.renderPipeline.ModelMatrix.Scale(0.5F, 0.5F, 0.5F);
                    loadTexture("/gui/items.png");

                    for (i14 = 0; i14 <= 1; ++i14)
                    {
                        i15 = itemStack10.Item.func_46057_a(itemStack10.ItemDamage, i14);
                        f16 = 1.0F;
                        if (field_27004_a)
                        {
                            int i17 = Item.itemsList[itemStack10.itemID].getColorFromDamage(itemStack10.ItemDamage, i14);
                            f18 = (i17 >> 16 & 255) / 255.0F;
                            f19 = (i17 >> 8 & 255) / 255.0F;
                            float f20 = (i17 & 255) / 255.0F;
                            Minecraft.renderPipeline.SetColor(f18 * f16, f19 * f16, f20 * f16, 1.0F);
                        }

                        func_40267_a(i15, b13);
                    }
                }
                else
                {
                    Minecraft.renderPipeline.ModelMatrix.Scale(0.5F, 0.5F, 0.5F);
                    i14 = itemStack10.IconIndex;
                    if (itemStack10.itemID < 256)
                    {
                        loadTexture("/terrain.png");
                    }
                    else
                    {
                        loadTexture("/gui/items.png");
                    }

                    if (field_27004_a)
                    {
                        i15 = Item.itemsList[itemStack10.itemID].getColorFromDamage(itemStack10.ItemDamage, 0);
                        f16 = (i15 >> 16 & 255) / 255.0F;
                        f23 = (i15 >> 8 & 255) / 255.0F;
                        f18 = (i15 & 255) / 255.0F;
                        f19 = 1.0F;
                        Minecraft.renderPipeline.SetColor(f16 * f19, f23 * f19, f18 * f19, 1.0F);
                    }

                    func_40267_a(i14, b13);
                }
            }
            
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
        }

        private void func_40267_a(int i1, int i2)
        {
            Tessellator tessellator3 = Tessellator.instance;
            float f4 = (i1 % 16 * 16 + 0) / 256.0F;
            float f5 = (i1 % 16 * 16 + 16) / 256.0F;
            float f6 = (i1 / 16 * 16 + 0) / 256.0F;
            float f7 = (i1 / 16 * 16 + 16) / 256.0F;
            float f8 = 1.0F;
            float f9 = 0.5F;
            float f10 = 0.25F;

            for (int i11 = 0; i11 < i2; ++i11)
            {
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                if (i11 > 0)
                {
                    float f12 = (random.NextSingle() * 2.0F - 1.0F) * 0.3F;
                    float f13 = (random.NextSingle() * 2.0F - 1.0F) * 0.3F;
                    float f14 = (random.NextSingle() * 2.0F - 1.0F) * 0.3F;
                    Minecraft.renderPipeline.ModelMatrix.Translate(f12, f13, f14);
                }

                Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
                tessellator3.startDrawingQuads();
                tessellator3.SetNormal(0.0F, 1.0F, 0.0F);
                tessellator3.AddVertexWithUV((double)(0.0F - f9), (double)(0.0F - f10), 0.0D, (double)f4, (double)f7);
                tessellator3.AddVertexWithUV((double)(f8 - f9), (double)(0.0F - f10), 0.0D, (double)f5, (double)f7);
                tessellator3.AddVertexWithUV((double)(f8 - f9), (double)(1.0F - f10), 0.0D, (double)f5, (double)f6);
                tessellator3.AddVertexWithUV((double)(0.0F - f9), (double)(1.0F - f10), 0.0D, (double)f4, (double)f6);
                tessellator3.DrawImmediate();
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }

        }

        public virtual void drawItemIntoGui(FontRenderer fontRenderer1, TextureManager renderEngine2, int i3, int i4, int i5, int i6, int i7)
        {
            int i10;
            float f11;
            float f12;
            float f13;
            if (i3 < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[i3].RenderType))
            {
                renderEngine2.bindTexture(renderEngine2.getTexture("/terrain.png"));

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);

                Block block15 = Block.blocksList[i3];
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate(i6 - 2, i7 + 3, -3.0F + zLevel);
                Minecraft.renderPipeline.ModelMatrix.Scale(10.0F, 10.0F, 10.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(1.0F, 0.5F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, 1.0F, -1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(210.0F, 1.0F, 0.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                i10 = Item.itemsList[i3].getColorFromDamage(i4, 0);
                f11 = (i10 >> 16 & 255) / 255.0F;
                f12 = (i10 >> 8 & 255) / 255.0F;
                f13 = (i10 & 255) / 255.0F;
                if (field_27004_a)
                {
                    Minecraft.renderPipeline.SetColor(f11, f12, f13, 1.0F);
                }

                Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
                renderBlocks.useInventoryTint = field_27004_a;
                renderBlocks.renderBlockAsItem(block15, i4, 1.0F);
                renderBlocks.useInventoryTint = true;
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 4);
            }
            else
            {
                int i8;
                if (Item.itemsList[i3].func_46058_c())
                {
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    renderEngine2.bindTexture(renderEngine2.getTexture("/gui/items.png"));

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);

                    for (i8 = 0; i8 <= 1; ++i8)
                    {
                        int i9 = Item.itemsList[i3].func_46057_a(i4, i8);
                        i10 = Item.itemsList[i3].getColorFromDamage(i4, i8);
                        f11 = (i10 >> 16 & 255) / 255.0F;
                        f12 = (i10 >> 8 & 255) / 255.0F;
                        f13 = (i10 & 255) / 255.0F;
                        if (field_27004_a)
                        {
                            Minecraft.renderPipeline.SetColor(f11, f12, f13, 1.0F);
                        }

                        renderTexturedQuad(i6, i7, i9 % 16 * 16, i9 / 16 * 16, 16, 16);
                    }

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 4);

                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                }
                else if (i5 >= 0)
                {
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    if (i3 < 256)
                    {
                        renderEngine2.bindTexture(renderEngine2.getTexture("/terrain.png"));
                    }
                    else
                    {
                        renderEngine2.bindTexture(renderEngine2.getTexture("/gui/items.png"));
                    }

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);

                    i8 = Item.itemsList[i3].getColorFromDamage(i4, 0);
                    float f14 = (i8 >> 16 & 255) / 255.0F;
                    float f16 = (i8 >> 8 & 255) / 255.0F;
                    f11 = (i8 & 255) / 255.0F;
                    if (field_27004_a)
                    {
                        Minecraft.renderPipeline.SetColor(f14, f16, f11, 1.0F);
                    }

                    renderTexturedQuad(i6, i7, i5 % 16 * 16, i5 / 16 * 16, 16, 16);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 4);
                }
            }

            GL.Enable(EnableCap.CullFace);
        }

        public virtual void renderItemIntoGUI(FontRenderer fontRenderer1, TextureManager renderEngine2, ItemStack itemStack3, int i4, int i5)
        {
            if (itemStack3 != null)
            {
                drawItemIntoGui(fontRenderer1, renderEngine2, itemStack3.itemID, itemStack3.ItemDamage, itemStack3.IconIndex, i4, i5);
                if (itemStack3 != null && itemStack3.hasEffect())
                {
                    GL.DepthFunc(DepthFunction.Greater);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    GL.DepthMask(false);
                    renderEngine2.bindTexture(renderEngine2.getTexture("%blur%/misc/glint.png"));
                    zLevel -= 50.0F;
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.DstColor, BlendingFactor.DstColor);
                    Minecraft.renderPipeline.SetColor(0.5F, 0.25F, 0.8F, 1.0F);
                    func_40266_a(i4 * 431278612 + i5 * 32178161, i4 - 2, i5 - 2, 20, 20);
                    GL.Disable(EnableCap.Blend);
                    GL.DepthMask(true);
                    zLevel += 50.0F;
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                    GL.DepthFunc(DepthFunction.Lequal);
                }

            }
        }

        private void func_40266_a(int i1, int i2, int i3, int i4, int i5)
        {
            for (int i6 = 0; i6 < 2; ++i6)
            {
                if (i6 == 0)
                {
                    GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
                }

                if (i6 == 1)
                {
                    GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
                }

                float f7 = 0.00390625F;
                float f8 = 0.00390625F;
                float f9 = DateTimeHelper.CurrentUnixTimeMillis() % (3000 + i6 * 1873) / (3000.0F + i6 * 1873) * 256.0F;
                float f10 = 0.0F;
                Tessellator tessellator11 = Tessellator.instance;
                float f12 = 4.0F;
                if (i6 == 1)
                {
                    f12 = -1.0F;
                }

                tessellator11.startDrawingQuads();
                tessellator11.AddVertexWithUV(i2 + 0, i3 + i5, zLevel, (double)((f9 + i5 * f12) * f7), (double)((f10 + i5) * f8));
                tessellator11.AddVertexWithUV(i2 + i4, i3 + i5, zLevel, (double)((f9 + i4 + i5 * f12) * f7), (double)((f10 + i5) * f8));
                tessellator11.AddVertexWithUV(i2 + i4, i3 + 0, zLevel, (double)((f9 + i4) * f7), (double)((f10 + 0.0F) * f8));
                tessellator11.AddVertexWithUV(i2 + 0, i3 + 0, zLevel, (double)((f9 + 0.0F) * f7), (double)((f10 + 0.0F) * f8));
                tessellator11.DrawImmediate();
            }

        }

        public virtual void renderItemOverlayIntoGUI(FontRenderer fontRenderer1, TextureManager renderEngine2, ItemStack itemStack3, int i4, int i5)
        {
            if (itemStack3 != null)
            {
                if (itemStack3.stackSize > 1)
                {
                    string string6 = "" + itemStack3.stackSize;
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    GL.Disable(EnableCap.DepthTest);
                    fontRenderer1.drawStringWithShadow(string6, i4 + 19 - 2 - fontRenderer1.getStringWidth(string6), i5 + 6 + 3, 0xFFFFFF);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                    GL.Enable(EnableCap.DepthTest);
                }

                if (itemStack3.ItemDamaged)
                {
                    int i11 = (int)(long)Math.Round(13.0D - itemStack3.ItemDamageForDisplay * 13.0D / itemStack3.MaxDamage, MidpointRounding.AwayFromZero);
                    int i7 = (int)(long)Math.Round(255.0D - itemStack3.ItemDamageForDisplay * 255.0D / itemStack3.MaxDamage, MidpointRounding.AwayFromZero);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    GL.Disable(EnableCap.DepthTest);
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                    Tessellator tessellator8 = Tessellator.instance;
                    int i9 = 255 - i7 << 16 | i7 << 8;
                    int i10 = (255 - i7) / 4 << 16 | 16128;
                    renderQuad(tessellator8, i4 + 2, i5 + 13, 13, 2, 0);
                    renderQuad(tessellator8, i4 + 2, i5 + 13, 12, 1, i10);
                    renderQuad(tessellator8, i4 + 2, i5 + 13, i11, 1, i9);
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                    GL.Enable(EnableCap.DepthTest);
                    Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                }

            }
        }

        private void renderQuad(Tessellator tessellator1, int i2, int i3, int i4, int i5, int i6)
        {
            tessellator1.startDrawingQuads();
            tessellator1.ColorOpaque_I = i6;
            tessellator1.AddVertex(i2 + 0, i3 + 0, 0.0D);
            tessellator1.AddVertex(i2 + 0, i3 + i5, 0.0D);
            tessellator1.AddVertex(i2 + i4, i3 + i5, 0.0D);
            tessellator1.AddVertex(i2 + i4, i3 + 0, 0.0D);
            tessellator1.DrawImmediate();
        }

        public virtual void renderTexturedQuad(int i1, int i2, int i3, int i4, int i5, int i6)
        {
            float f7 = 0.00390625F;
            float f8 = 0.00390625F;
            Tessellator tessellator9 = Tessellator.instance;
            tessellator9.startDrawingQuads();
            tessellator9.AddVertexWithUV(i1 + 0, i2 + i6, zLevel, (double)((i3 + 0) * f7), (double)((i4 + i6) * f8));
            tessellator9.AddVertexWithUV(i1 + i5, i2 + i6, zLevel, (double)((i3 + i5) * f7), (double)((i4 + i6) * f8));
            tessellator9.AddVertexWithUV(i1 + i5, i2 + 0, zLevel, (double)((i3 + i5) * f7), (double)((i4 + 0) * f8));
            tessellator9.AddVertexWithUV(i1 + 0, i2 + 0, zLevel, (double)((i3 + 0) * f7), (double)((i4 + 0) * f8));
            tessellator9.DrawImmediate();
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            doRenderItem((EntityItem)entity1, d2, d4, d6, f8, f9);
        }
    }

}