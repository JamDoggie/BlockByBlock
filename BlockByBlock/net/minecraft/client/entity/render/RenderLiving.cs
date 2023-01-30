using System;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render.model;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.client.entity.render
{
    using Minecraft = Minecraft;

    public class RenderLiving : Renderer
    {
        protected internal ModelBase mainModel;
        protected internal ModelBase renderPassModel;

        public RenderLiving(ModelBase modelBase1, float f2)
        {
            mainModel = modelBase1;
            shadowSize = f2;
        }

        public virtual ModelBase RenderPassModel
        {
            set
            {
                renderPassModel = value;
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

        public virtual void doRenderLiving(EntityLiving ent, double x, double y, double z, float f8, float f9)
        {
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            GL.Disable(EnableCap.CullFace);
            mainModel.onGround = renderSwingProgress(ent, f9);
            if (renderPassModel != null)
            {
                renderPassModel.onGround = mainModel.onGround;
            }

            mainModel.isRiding = ent.Riding;
            if (renderPassModel != null)
            {
                renderPassModel.isRiding = mainModel.isRiding;
            }

            mainModel.isChild = ent.Child;
            if (renderPassModel != null)
            {
                renderPassModel.isChild = mainModel.isChild;
            }

            try
            {
                float f10 = func_48418_a(ent.prevRenderYawOffset, ent.renderYawOffset, f9);
                float f11 = func_48418_a(ent.prevRotationYawHead, ent.rotationYawHead, f9);
                float f12 = ent.prevRotationPitch + (ent.rotationPitch - ent.prevRotationPitch) * f9;
                renderLivingAt(ent, x, y, z);
                float f13 = handleRotationFloat(ent, f9);
                rotateCorpse(ent, f13, f10, f9);
                float f14 = 0.0625F;
                
                Minecraft.renderPipeline.ModelMatrix.Scale(-1.0F, -1.0F, 1.0F);
                preRenderCallback(ent, f9);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -24.0F * f14 - 0.0078125F, 0.0F);
                float f15 = ent.field_705_Q + (ent.field_704_R - ent.field_705_Q) * f9;
                float f16 = ent.field_703_S - ent.field_704_R * (1.0F - f9);
                if (ent.Child)
                {
                    f16 *= 3.0F;
                }

                if (f15 > 1.0F)
                {
                    f15 = 1.0F;
                }

                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                mainModel.setLivingAnimations(ent, f16, f15, f9);
                renderModel(ent, f16, f15, f13, f11 - f10, f12, f14);

                int i18;
                float f19;
                float f20;
                float f22;
                for (int i17 = 0; i17 < 4; ++i17)
                {
                    i18 = shouldRenderPass(ent, i17, f9);
                    if (i18 > 0)
                    {
                        renderPassModel.setLivingAnimations(ent, f16, f15, f9);
                        renderPassModel.render(ent, f16, f15, f13, f11 - f10, f12, f14);
                        if (i18 == 15)
                        {
                            f19 = ent.ticksExisted + f9;
                            loadTexture("%blur%/misc/glint.png");
                            GL.Enable(EnableCap.Blend);
                            f20 = 0.5F;
                            Minecraft.renderPipeline.SetColor(f20, f20, f20, 1.0F);
                            GL.DepthFunc(DepthFunction.Equal);
                            GL.DepthMask(false);

                            for (int i21 = 0; i21 < 2; ++i21)
                            {
                                Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                                f22 = 0.76F;
                                Minecraft.renderPipeline.SetColor(0.5F * f22, 0.25F * f22, 0.8F * f22, 1.0F);
                                GL.BlendFunc(BlendingFactor.SrcColor, BlendingFactor.One);
                                Minecraft.renderPipeline.TextureMatrix.LoadIdentity();
                                float f23 = f19 * (0.001F + i21 * 0.003F) * 20.0F;
                                float f24 = 0.33333334F;
                                Minecraft.renderPipeline.TextureMatrix.Scale(f24, f24, f24);
                                Minecraft.renderPipeline.TextureMatrix.Rotate(30.0F - i21 * 60.0F, 0.0F, 0.0F, 1.0F);
                                Minecraft.renderPipeline.TextureMatrix.Translate(0.0F, f23, 0.0F);
                                renderPassModel.render(ent, f16, f15, f13, f11 - f10, f12, f14);
                            }

                            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                            GL.DepthMask(true);
                            Minecraft.renderPipeline.TextureMatrix.LoadIdentity();
                            Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                            GL.Disable(EnableCap.Blend);
                            GL.DepthFunc(DepthFunction.Lequal);
                        }

                        GL.Disable(EnableCap.Blend);
                        Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                    }
                }

                renderEquippedItems(ent, f9);
                float brightness = ent.getBrightness(f9);
                i18 = getColorMultiplier(ent, brightness, f9);
                LightmapManager.ActiveTexture = LightmapManager.lightmapTexUnit;
                Minecraft.renderPipeline.SetState(RenderState.LightmapState, false);
                LightmapManager.ActiveTexture = LightmapManager.defaultTexUnit;
                if ((i18 >> 24 & 255) > 0 || ent.hurtTime > 0 || ent.deathTime > 0)
                {
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                    GL.DepthFunc(DepthFunction.Equal);
                    if (ent.hurtTime > 0 || ent.deathTime > 0)
                    {
                        Minecraft.renderPipeline.SetColor(brightness, 0.0F, 0.0F, 0.4F);
                        mainModel.render(ent, f16, f15, f13, f11 - f10, f12, f14);

                        for (int i27 = 0; i27 < 4; ++i27)
                        {
                            if (inheritRenderPass(ent, i27, f9) >= 0)
                            {
                                Minecraft.renderPipeline.SetColor(brightness, 0.0F, 0.0F, 0.4F);
                                renderPassModel.render(ent, f16, f15, f13, f11 - f10, f12, f14);
                            }
                        }
                    }

                    if ((i18 >> 24 & 255) > 0)
                    {
                        f19 = (i18 >> 16 & 255) / 255.0F;
                        f20 = (i18 >> 8 & 255) / 255.0F;
                        float f28 = (i18 & 255) / 255.0F;
                        f22 = (i18 >> 24 & 255) / 255.0F;
                        Minecraft.renderPipeline.SetColor(f19, f20, f28, f22);
                        mainModel.render(ent, f16, f15, f13, f11 - f10, f12, f14);

                        for (int i29 = 0; i29 < 4; ++i29)
                        {
                            if (inheritRenderPass(ent, i29, f9) >= 0)
                            {
                                Minecraft.renderPipeline.SetColor(f19, f20, f28, f22);
                                renderPassModel.render(ent, f16, f15, f13, f11 - f10, f12, f14);
                            }
                        }
                    }
                    
                    GL.DepthFunc(DepthFunction.Lequal);
                    GL.Disable(EnableCap.Blend);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                }
            }
            catch (Exception exception25)
            {
                Console.WriteLine(exception25.ToString());
                Console.Write(exception25.StackTrace);
            }

            
            LightmapManager.ActiveTexture = LightmapManager.lightmapTexUnit;
            Minecraft.renderPipeline.SetState(RenderState.LightmapState, true);
            LightmapManager.ActiveTexture = LightmapManager.defaultTexUnit;
            GL.Enable(EnableCap.CullFace);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();

            passSpecialRender(ent, x, y, z);
        }

        protected internal virtual void renderModel(EntityLiving entityLiving1, float f2, float f3, float f4, float f5, float f6, float f7)
        {
            loadDownloadableImageTexture(entityLiving1.skinUrl, entityLiving1.Texture);
            mainModel.render(entityLiving1, f2, f3, f4, f5, f6, f7);
        }

        protected internal virtual void renderLivingAt(EntityLiving entityLiving1, double d2, double d4, double d6)
        {
            Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4, (float)d6);
        }

        protected internal virtual void rotateCorpse(EntityLiving entityLiving1, float f2, float f3, float f4)
        {
            Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F - f3, 0.0F, 1.0F, 0.0F);
            if (entityLiving1.deathTime > 0)
            {
                float f5 = (entityLiving1.deathTime + f4 - 1.0F) / 20.0F * 1.6F;
                f5 = MathHelper.sqrt_float(f5);
                if (f5 > 1.0F)
                {
                    f5 = 1.0F;
                }

                Minecraft.renderPipeline.ModelMatrix.Rotate(f5 * getDeathMaxRotation(entityLiving1), 0.0F, 0.0F, 1.0F);
            }

        }

        protected internal virtual float renderSwingProgress(EntityLiving entityLiving1, float f2)
        {
            return entityLiving1.getSwingProgress(f2);
        }

        protected internal virtual float handleRotationFloat(EntityLiving entityLiving1, float f2)
        {
            return entityLiving1.ticksExisted + f2;
        }

        protected internal virtual void renderEquippedItems(EntityLiving entityLiving1, float f2)
        {
        }

        protected internal virtual int inheritRenderPass(EntityLiving entityLiving1, int i2, float f3)
        {
            return shouldRenderPass(entityLiving1, i2, f3);
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

        protected internal virtual void renderLivingLabel(EntityLiving entityLiving1, string text, double offsetX, double offsetY, double offsetZ, int maxDistance)
        {
            float f10 = entityLiving1.getDistanceToEntity(renderManager.livingPlayer);
            if (f10 <= maxDistance)
            {
                FontRenderer fontRenderer11 = FontRendererFromRenderManager;
                float f12 = 1.6F;
                float f13 = 0.016666668F * f12;
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate((float)offsetX + 0.0F, (float)offsetY + 2.3F, (float)offsetZ);
                Minecraft.renderPipeline.SetNormal(0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Scale(-f13, -f13, f13);
                Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                GL.DepthMask(false);
                GL.Disable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                Tessellator tessellator = Tessellator.instance;
                sbyte b15 = 0;
                if (text.Equals("deadmau5"))
                {
                    b15 = -10;
                }

                Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                tessellator.startDrawingQuads();
                int i16 = fontRenderer11.getStringWidth(text) / 2;
                tessellator.setColorRGBA_F(0.0F, 0.0F, 0.0F, 0.25F);
                tessellator.AddVertex(-i16 - 1, -1 + b15, 0.0D);
                tessellator.AddVertex(-i16 - 1, 8 + b15, 0.0D);
                tessellator.AddVertex(i16 + 1, 8 + b15, 0.0D);
                tessellator.AddVertex(i16 + 1, -1 + b15, 0.0D);
                tessellator.DrawImmediate();
                Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                fontRenderer11.drawString(text, -fontRenderer11.getStringWidth(text) / 2, b15, 553648127);
                GL.Enable(EnableCap.DepthTest);
                GL.DepthMask(true);
                fontRenderer11.drawString(text, -fontRenderer11.getStringWidth(text) / 2, b15, -1);
                Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
                GL.Disable(EnableCap.Blend);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
            }
        }

        public override void doRender(Entity entity1, double d2, double d4, double d6, float f8, float f9)
        {
            doRenderLiving((EntityLiving)entity1, d2, d4, d6, f8, f9);
        }
    }

}