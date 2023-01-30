using net.minecraft.src;

namespace net.minecraft.client.entity.render
{
    using BlockByBlock.net.minecraft.render;
    using net.minecraft.client.entity;
    using net.minecraft.client.entity.render.model;
    using OpenTK.Graphics.OpenGL;
    using Minecraft = Minecraft;

    public class RenderPlayer : RenderLiving
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            modelBipedMain = (ModelBiped)mainModel;
        }

        private ModelBiped modelBipedMain;
        private ModelBiped modelArmorChestplate = new ModelBiped(1.0F);
        private ModelBiped modelArmor = new ModelBiped(0.5F);
        private static readonly string[] armorFilenamePrefix = new string[] { "cloth", "chain", "iron", "diamond", "gold" };

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
                    loadTexture("/armor/" + armorFilenamePrefix[itemArmor6.renderIndex] + "_" + (i2 == 2 ? 2 : 1) + ".png");
                    ModelBiped modelBiped7 = i2 == 2 ? modelArmor : modelArmorChestplate;
                    modelBiped7.bipedHead.showModel = i2 == 0;
                    modelBiped7.bipedHeadwear.showModel = i2 == 0;
                    modelBiped7.bipedBody.showModel = i2 == 1 || i2 == 2;
                    modelBiped7.bipedRightArm.showModel = i2 == 1;
                    modelBiped7.bipedLeftArm.showModel = i2 == 1;
                    modelBiped7.bipedRightLeg.showModel = i2 == 2 || i2 == 3;
                    modelBiped7.bipedLeftLeg.showModel = i2 == 2 || i2 == 3;
                    RenderPassModel = modelBiped7;
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
            modelArmorChestplate.heldItemRight = modelArmor.heldItemRight = modelBipedMain.heldItemRight = itemStack10 != null ? 1 : 0;
            if (itemStack10 != null && entityPlayer1.ItemInUseCount > 0)
            {
                EnumAction enumAction11 = itemStack10.ItemUseAction;
                if (enumAction11 == EnumAction.block)
                {
                    modelArmorChestplate.heldItemRight = modelArmor.heldItemRight = modelBipedMain.heldItemRight = 3;
                }
                else if (enumAction11 == EnumAction.bow)
                {
                    modelArmorChestplate.aimedBow = modelArmor.aimedBow = modelBipedMain.aimedBow = true;
                }
            }

            modelArmorChestplate.isSneak = modelArmor.isSneak = modelBipedMain.isSneak = entityPlayer1.Sneaking;
            double d13 = d4 - entityPlayer1.yOffset;
            if (entityPlayer1.Sneaking && !(entityPlayer1 is EntityPlayerSP))
            {
                d13 -= 0.125D;
            }

            base.doRenderLiving(entityPlayer1, d2, d13, d6, f8, f9);
            modelArmorChestplate.aimedBow = modelArmor.aimedBow = modelBipedMain.aimedBow = false;
            modelArmorChestplate.isSneak = modelArmor.isSneak = modelBipedMain.isSneak = false;
            modelArmorChestplate.heldItemRight = modelArmor.heldItemRight = modelBipedMain.heldItemRight = 0;
        }

        protected internal virtual void renderName(EntityPlayer entityPlayer1, double d2, double d4, double d6)
        {
            if (Minecraft.GuiEnabled && entityPlayer1 != renderManager.livingPlayer)
            {
                float f8 = 1.6F;
                float f9 = 0.016666668F * f8;
                float f10 = entityPlayer1.getDistanceToEntity(renderManager.livingPlayer);
                float f11 = entityPlayer1.Sneaking ? 32.0F : 64.0F;
                if (f10 < f11)
                {
                    string string12 = entityPlayer1.username;
                    if (!entityPlayer1.Sneaking)
                    {
                        if (entityPlayer1.PlayerSleeping)
                        {
                            renderLivingLabel(entityPlayer1, string12, d2, d4 - 1.5D, d6, 64);
                        }
                        else
                        {
                            renderLivingLabel(entityPlayer1, string12, d2, d4, d6, 64);
                        }
                    }
                    else
                    {
                        FontRenderer fontRenderer13 = FontRendererFromRenderManager;
                        Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                        Minecraft.renderPipeline.ModelMatrix.Translate((float)d2 + 0.0F, (float)d4 + 2.3F, (float)d6);
                        Minecraft.renderPipeline.SetNormal(0.0F, 1.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(-renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Scale(-f9, -f9, f9);
                        Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.25F / f9, 0.0F);
                        GL.DepthMask(false);
                        GL.Enable(EnableCap.Blend);
                        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                        Tessellator tessellator14 = Tessellator.instance;
                        Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                        tessellator14.startDrawingQuads();
                        int i15 = fontRenderer13.getStringWidth(string12) / 2;
                        tessellator14.setColorRGBA_F(0.0F, 0.0F, 0.0F, 0.25F);
                        tessellator14.AddVertex(-i15 - 1, -1.0D, 0.0D);
                        tessellator14.AddVertex(-i15 - 1, 8.0D, 0.0D);
                        tessellator14.AddVertex(i15 + 1, 8.0D, 0.0D);
                        tessellator14.AddVertex(i15 + 1, -1.0D, 0.0D);
                        tessellator14.DrawImmediate();
                        Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                        GL.DepthMask(true);
                        fontRenderer13.drawString(string12, -fontRenderer13.getStringWidth(string12) / 2, 0, 553648127);
                        Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                        GL.Disable(EnableCap.Blend);
                        Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                        Minecraft.renderPipeline.ModelMatrix.PopMatrix();
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
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                modelBipedMain.bipedHead.postRender(0.0625F);
                if (RenderBlocks.renderItemIn3d(Block.blocksList[itemStack3.itemID].RenderType))
                {
                    float f4 = 0.625F;
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.25F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(f4, -f4, f4);
                }

                renderManager.itemRenderer.renderItem(entityPlayer1, itemStack3, 0);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }

            float f6;
            if (entityPlayer1.username.Equals("deadmau5") && loadDownloadableImageTexture(entityPlayer1.skinUrl, null))
            {
                for (int i19 = 0; i19 < 2; ++i19)
                {
                    float f5 = entityPlayer1.prevRotationYaw + (entityPlayer1.rotationYaw - entityPlayer1.prevRotationYaw) * f2 - (entityPlayer1.prevRenderYawOffset + (entityPlayer1.renderYawOffset - entityPlayer1.prevRenderYawOffset) * f2);
                    f6 = entityPlayer1.prevRotationPitch + (entityPlayer1.rotationPitch - entityPlayer1.prevRotationPitch) * f2;
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Rotate(f5, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(f6, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.375F * (i19 * 2 - 1), 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.375F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-f6, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-f5, 0.0F, 1.0F, 0.0F);
                    float f7 = 1.3333334F;
                    Minecraft.renderPipeline.ModelMatrix.Scale(f7, f7, f7);
                    modelBipedMain.renderEars(0.0625F);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                }
            }

            float f10;
            if (loadDownloadableImageTexture(entityPlayer1.playerCloakUrl, null))
            {
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, 0.125F);
                double d20 = entityPlayer1.field_20066_r + (entityPlayer1.field_20063_u - entityPlayer1.field_20066_r) * (double)f2 - (entityPlayer1.prevPosX + (entityPlayer1.posX - entityPlayer1.prevPosX) * (double)f2);
                double d23 = entityPlayer1.field_20065_s + (entityPlayer1.field_20062_v - entityPlayer1.field_20065_s) * (double)f2 - (entityPlayer1.prevPosY + (entityPlayer1.posY - entityPlayer1.prevPosY) * (double)f2);
                double d8 = entityPlayer1.field_20064_t + (entityPlayer1.field_20061_w - entityPlayer1.field_20064_t) * (double)f2 - (entityPlayer1.prevPosZ + (entityPlayer1.posZ - entityPlayer1.prevPosZ) * (double)f2);
                f10 = entityPlayer1.prevRenderYawOffset + (entityPlayer1.renderYawOffset - entityPlayer1.prevRenderYawOffset) * f2;
                double d11 = (double)MathHelper.sin(f10 * (float)Math.PI / 180.0F);
                double d13 = (double)-MathHelper.cos(f10 * (float)Math.PI / 180.0F);
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

                Minecraft.renderPipeline.ModelMatrix.Rotate(6.0F + f16 / 2.0F + f15, 1.0F, 0.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(f17 / 2.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-f17 / 2.0F, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
                modelBipedMain.renderCloak(0.0625F);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }

            ItemStack itemStack21 = entityPlayer1.inventory.CurrentItem;
            if (itemStack21 != null)
            {
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                modelBipedMain.bipedRightArm.postRender(0.0625F);
                Minecraft.renderPipeline.ModelMatrix.Translate(-0.0625F, 0.4375F, 0.0625F);
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
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.1875F, -0.3125F);
                    f6 *= 0.75F;
                    Minecraft.renderPipeline.ModelMatrix.Rotate(20.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(f6, -f6, f6);
                }
                else if (itemStack21.itemID == Item.bow.shiftedIndex)
                {
                    f6 = 0.625F;
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.125F, 0.3125F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-20.0F, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(f6, -f6, f6);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                }
                else if (Item.itemsList[itemStack21.itemID].Full3D)
                {
                    f6 = 0.625F;
                    if (Item.itemsList[itemStack21.itemID].shouldRotateAroundWhenRendering())
                    {
                        Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 0.0F, 1.0F);
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -0.125F, 0.0F);
                    }

                    if (entityPlayer1.ItemInUseCount > 0 && enumAction22 == EnumAction.block)
                    {
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.05F, 0.0F, -0.1F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(-50.0F, 0.0F, 1.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(-10.0F, 1.0F, 0.0F, 0.0F);
                        Minecraft.renderPipeline.ModelMatrix.Rotate(-60.0F, 0.0F, 0.0F, 1.0F);
                    }

                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.1875F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(f6, -f6, f6);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                }
                else
                {
                    f6 = 0.375F;
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.25F, 0.1875F, -0.1875F);
                    Minecraft.renderPipeline.ModelMatrix.Scale(f6, f6, f6);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(60.0F, 0.0F, 0.0F, 1.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(20.0F, 0.0F, 0.0F, 1.0F);
                }

                if (itemStack21.Item.func_46058_c())
                {
                    for (int i25 = 0; i25 <= 1; ++i25)
                    {
                        int i24 = itemStack21.Item.getColorFromDamage(itemStack21.ItemDamage, i25);
                        float f26 = (i24 >> 16 & 255) / 255.0F;
                        float f9 = (i24 >> 8 & 255) / 255.0F;
                        f10 = (i24 & 255) / 255.0F;
                        Minecraft.renderPipeline.SetColor(f26, f9, f10, 1.0F);
                        renderManager.itemRenderer.renderItem(entityPlayer1, itemStack21, i25);
                    }
                }
                else
                {
                    renderManager.itemRenderer.renderItem(entityPlayer1, itemStack21, 0);
                }

                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }

        }

        protected internal virtual void renderPlayerScale(EntityPlayer entityPlayer1, float f2)
        {
            float f3 = 0.9375F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f3, f3, f3);
        }

        public virtual void drawFirstPersonHand()
        {
            modelBipedMain.onGround = 0.0F;
            modelBipedMain.setRotationAngles(0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0625F);
            modelBipedMain.bipedRightArm.render(0.0625F);
        }

        protected internal virtual void renderPlayerSleep(EntityPlayer entityPlayer1, double d2, double d4, double d6)
        {
            if (entityPlayer1.EntityAlive && entityPlayer1.PlayerSleeping)
            {
                base.renderLivingAt(entityPlayer1, d2 + entityPlayer1.field_22063_x, d4 + entityPlayer1.field_22062_y, d6 + entityPlayer1.field_22061_z);
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
                Minecraft.renderPipeline.ModelMatrix.Rotate(entityPlayer1.BedOrientationInDegrees, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(getDeathMaxRotation(entityPlayer1), 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(270.0F, 0.0F, 1.0F, 0.0F);
            }
            else
            {
                base.rotateCorpse(entityPlayer1, f2, f3, f4);
            }

        }

        protected internal override void passSpecialRender(EntityLiving entityLiving1, double d2, double d4, double d6)
        {
            renderName((EntityPlayer)entityLiving1, d2, d4, d6);
        }

        protected internal override void preRenderCallback(EntityLiving entityLiving1, float f2)
        {
            renderPlayerScale((EntityPlayer)entityLiving1, f2);
        }

        protected internal override int shouldRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return setArmorModel((EntityPlayer)entityLiving1, i2, f3);
        }

        protected internal override void renderEquippedItems(EntityLiving entityLiving1, float f2)
        {
            renderSpecials((EntityPlayer)entityLiving1, f2);
        }

        protected internal override void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            rotatePlayer((EntityPlayer)entityLiving1, f2, f3, f4);
        }

        protected internal override void renderLivingAt(EntityLiving entityLiving1, double d2, double d4, double d6)
        {
            renderPlayerSleep((EntityPlayer)entityLiving1, d2, d4, d6);
        }

        public override void doRenderLiving(EntityLiving entityLiving1, double d2, double d4, double d6, float f8, float f9)
        {
            renderPlayer((EntityPlayer)entityLiving1, d2, d4, d6, f8, f9);
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            renderPlayer((EntityPlayer)entity1, d2, d4, d6, f8, f9);
        }
    }

}