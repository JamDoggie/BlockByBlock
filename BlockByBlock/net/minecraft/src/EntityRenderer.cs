using System;
using System.Threading;
using BlockByBlock.helpers;
using BlockByBlock.java_extensions;
using BlockByBlock.net.minecraft.client.entity.particle;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.render;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

    using Minecraft = net.minecraft.client.Minecraft;

    public class GameRenderer
	{
		private Minecraft mc;
		private float farPlaneDistance = 0.0F;
		public ItemRenderer itemRenderer;
		private int rendererUpdateCount;
		private Entity pointedEntity = null;
		private MouseFilter mouseFilterXAxis = new MouseFilter();
		private MouseFilter mouseFilterYAxis = new MouseFilter();
		private MouseFilter mouseFilterDummy1 = new MouseFilter();
		private MouseFilter mouseFilterDummy2 = new MouseFilter();
		private MouseFilter mouseFilterDummy3 = new MouseFilter();
		private MouseFilter mouseFilterDummy4 = new MouseFilter();
		private float thirdPersonDistance = 4.0F;
		private float thirdPersonDistanceTemp = 4.0F;
		private float debugCamYaw = 0.0F;
		private float prevDebugCamYaw = 0.0F;
		private float debugCamPitch = 0.0F;
		private float prevDebugCamPitch = 0.0F;
		private float smoothCamYaw;
		private float smoothCamPitch;
		private float smoothCamFilterX;
		private float smoothCamFilterY;
		private float smoothCamPartialTicks;
		private float debugCamFOV = 0.0F;
		private float prevDebugCamFOV = 0.0F;
		private float camRoll = 0.0F;
		private float prevCamRoll = 0.0F;
		public int lightmapTexture;
		private int[] lightmapColors;
		private float fovModifierHand;
		private float fovModifierHandPrev;
		private float fovMultiplierTemp;
		private bool cloudFog = false;
		private double cameraZoom = 1.0D;
		private double cameraYaw = 0.0D;
		private double cameraPitch = 0.0D;
		private long prevFrameTime = DateTimeHelper.CurrentUnixTimeMillis();
		private long renderEndNanoTime = 0L;
		private bool lightmapUpdateNeeded = false;
		internal float torchFlickerX = 0.0F;
		internal float torchFlickerDX = 0.0F;
		internal float torchFlickerY = 0.0F;
		internal float torchFlickerDY = 0.0F;
		private RandomExtended random = new RandomExtended();
		private int rainSoundCounter = 0;
		internal float[] rainXCoords;
		internal float[] rainYCoords;
		internal volatile int field_1394_b = 0;
		internal volatile int field_1393_c = 0;
		internal float[] fogColorBuffer = new float[4];
		internal float fogColorRed;
		internal float fogColorGreen;
		internal float fogColorBlue;
		private float fogColor2;
		private float fogColor1;
		public int debugViewDirection;

		public GameRenderer(Minecraft minecraft1)
		{
			this.mc = minecraft1;
			this.itemRenderer = new ItemRenderer(minecraft1);
			this.lightmapTexture = minecraft1.renderEngine.allocateAndSetupTexture(new Image<Bgra32>(16, 16));
			this.lightmapColors = new int[256];
		}

		public virtual void updateRenderer()
		{
			this.updateFovModifierHand();
			this.updateTorchFlicker();
			this.fogColor2 = this.fogColor1;
			this.thirdPersonDistanceTemp = this.thirdPersonDistance;
			this.prevDebugCamYaw = this.debugCamYaw;
			this.prevDebugCamPitch = this.debugCamPitch;
			this.prevDebugCamFOV = this.debugCamFOV;
			this.prevCamRoll = this.camRoll;
			float f1;
			float f2;
			if (this.mc.gameSettings.smoothCamera)
			{
				f1 = this.mc.gameSettings.mouseSensitivity * 0.6F + 0.2F;
				f2 = f1 * f1 * f1 * 8.0F;
				this.smoothCamFilterX = this.mouseFilterXAxis.func_22386_a(this.smoothCamYaw, 0.05F * f2);
				this.smoothCamFilterY = this.mouseFilterYAxis.func_22386_a(this.smoothCamPitch, 0.05F * f2);
				this.smoothCamPartialTicks = 0.0F;
				this.smoothCamYaw = 0.0F;
				this.smoothCamPitch = 0.0F;
			}

			if (this.mc.renderViewEntity == null)
			{
				this.mc.renderViewEntity = this.mc.thePlayer;
			}

			f1 = this.mc.theWorld.getLightBrightness(MathHelper.floor_double(this.mc.renderViewEntity.posX), MathHelper.floor_double(this.mc.renderViewEntity.posY), MathHelper.floor_double(this.mc.renderViewEntity.posZ));
			f2 = (float)(3 - this.mc.gameSettings.renderDistance) / 3.0F;
			float f3 = f1 * (1.0F - f2) + f2;
			this.fogColor1 += (f3 - this.fogColor1) * 0.1F;
			++this.rendererUpdateCount;
			this.itemRenderer.updateEquippedItem();
			this.addRainParticles();
		}

		public virtual void getMouseOver(float f1)
		{
			if (this.mc.renderViewEntity != null)
			{
				if (this.mc.theWorld != null)
				{
					double d2 = (double)this.mc.playerController.BlockReachDistance;
					this.mc.objectMouseOver = this.mc.renderViewEntity.rayTrace(d2, f1);
					double d4 = d2;
					Vec3D vec3D6 = this.mc.renderViewEntity.getPosition(f1);
					if (this.mc.playerController.extendedReach())
					{
						d2 = 6.0D;
						d4 = 6.0D;
					}
					else
					{
						if (d2 > 3.0D)
						{
							d4 = 3.0D;
						}

						d2 = d4;
					}

					if (this.mc.objectMouseOver != null)
					{
						d4 = this.mc.objectMouseOver.hitVec.distanceTo(vec3D6);
					}

					Vec3D vec3D7 = this.mc.renderViewEntity.getLook(f1);
					Vec3D vec3D8 = vec3D6.addVector(vec3D7.xCoord * d2, vec3D7.yCoord * d2, vec3D7.zCoord * d2);
					this.pointedEntity = null;
					float f9 = 1.0F;
					System.Collections.IList list10 = this.mc.theWorld.getEntitiesWithinAABBExcludingEntity(this.mc.renderViewEntity, this.mc.renderViewEntity.boundingBox.addCoord(vec3D7.xCoord * d2, vec3D7.yCoord * d2, vec3D7.zCoord * d2).expand((double)f9, (double)f9, (double)f9));
					double d11 = d4;

					for (int i13 = 0; i13 < list10.Count; ++i13)
					{
						Entity entity14 = (Entity)list10[i13];
						if (entity14.canBeCollidedWith())
						{
							float f15 = entity14.CollisionBorderSize;
							AxisAlignedBB axisAlignedBB16 = entity14.boundingBox.expand((double)f15, (double)f15, (double)f15);
							MovingObjectPosition movingObjectPosition17 = axisAlignedBB16.calculateIntercept(vec3D6, vec3D8);
							if (axisAlignedBB16.isVecInside(vec3D6))
							{
								if (0.0D < d11 || d11 == 0.0D)
								{
									this.pointedEntity = entity14;
									d11 = 0.0D;
								}
							}
							else if (movingObjectPosition17 != null)
							{
								double d18 = vec3D6.distanceTo(movingObjectPosition17.hitVec);
								if (d18 < d11 || d11 == 0.0D)
								{
									this.pointedEntity = entity14;
									d11 = d18;
								}
							}
						}
					}

					if (this.pointedEntity != null && (d11 < d4 || this.mc.objectMouseOver == null))
					{
						this.mc.objectMouseOver = new MovingObjectPosition(this.pointedEntity);
					}

				}
			}
		}

		private void updateFovModifierHand()
		{
			EntityPlayerSP entityPlayerSP1 = (EntityPlayerSP)this.mc.renderViewEntity;
			this.fovMultiplierTemp = entityPlayerSP1.FOVMultiplier;
			this.fovModifierHandPrev = this.fovModifierHand;
			this.fovModifierHand += (this.fovMultiplierTemp - this.fovModifierHand) * 0.5F;
		}

		private float getFOVModifier(float f1, bool z2)
		{
			if (this.debugViewDirection > 0)
			{
				return 90.0F;
			}
			else
			{
				EntityPlayer entityPlayer3 = (EntityPlayer)this.mc.renderViewEntity;
				float f4 = 70.0F;
				if (z2)
				{
					f4 += this.mc.gameSettings.fovSetting * 40.0F;
					f4 *= this.fovModifierHandPrev + (this.fovModifierHand - this.fovModifierHandPrev) * f1;
				}

				if (entityPlayer3.Health <= 0)
				{
					float f5 = (float)entityPlayer3.deathTime + f1;
					f4 /= (1.0F - 500.0F / (f5 + 500.0F)) * 2.0F + 1.0F;
				}

				int i6 = ActiveRenderInfo.getBlockIdAtEntityViewpoint(this.mc.theWorld, entityPlayer3, f1);
				if (i6 != 0 && Block.blocksList[i6].blockMaterial == Material.water)
				{
					f4 = f4 * 60.0F / 70.0F;
				}
                
				if (mc.zoom)
				{
					f4 = 10.0f;
				}

				return f4 + this.prevDebugCamFOV + (this.debugCamFOV - this.prevDebugCamFOV) * f1;
			}
		}

		private void hurtCameraEffect(float f1)
		{
			EntityLiving entityLiving2 = this.mc.renderViewEntity;
			float f3 = (float)entityLiving2.hurtTime - f1;
			float f4;
			if (entityLiving2.Health <= 0)
			{
				f4 = (float)entityLiving2.deathTime + f1;
				Minecraft.renderPipeline.ModelMatrix.Rotate(40.0F - 8000.0F / (f4 + 200.0F), 0.0F, 0.0F, 1.0F);
			}

			if (f3 >= 0.0F)
			{
				f3 /= (float)entityLiving2.maxHurtTime;
				f3 = MathHelper.sin(f3 * f3 * f3 * f3 * (float)Math.PI);
				f4 = entityLiving2.attackedAtYaw;
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f4, 0.0F, 1.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(-f3 * 14.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(f4, 0.0F, 1.0F, 0.0F);
			}
		}

		private void setupViewBobbing(float f1)
		{
			if (this.mc.renderViewEntity is EntityPlayer)
			{
				EntityPlayer entityPlayer2 = (EntityPlayer)this.mc.renderViewEntity;
				float f3 = entityPlayer2.distanceWalkedModified - entityPlayer2.prevDistanceWalkedModified;
				float f4 = -(entityPlayer2.distanceWalkedModified + f3 * f1);
				float f5 = entityPlayer2.prevCameraYaw + (entityPlayer2.cameraYaw - entityPlayer2.prevCameraYaw) * f1;
				float f6 = entityPlayer2.prevCameraPitch + (entityPlayer2.cameraPitch - entityPlayer2.prevCameraPitch) * f1;
				Minecraft.renderPipeline.ModelMatrix.Translate(MathHelper.sin(f4 * (float)Math.PI) * f5 * 0.5F, -Math.Abs(MathHelper.cos(f4 * (float)Math.PI) * f5), 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(MathHelper.sin(f4 * (float)Math.PI) * f5 * 3.0F, 0.0F, 0.0F, 1.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(Math.Abs(MathHelper.cos(f4 * (float)Math.PI - 0.2F) * f5) * 5.0F, 1.0F, 0.0F, 0.0F);
				Minecraft.renderPipeline.ModelMatrix.Rotate(f6, 1.0F, 0.0F, 0.0F);
			}
		}

		private void orientCamera(float f1)
		{
			EntityLiving entityLiving2 = this.mc.renderViewEntity;
			float f3 = entityLiving2.yOffset - 1.62F;
			double d4 = entityLiving2.prevPosX + (entityLiving2.posX - entityLiving2.prevPosX) * (double)f1;
			double d6 = entityLiving2.prevPosY + (entityLiving2.posY - entityLiving2.prevPosY) * (double)f1 - (double)f3;
			double d8 = entityLiving2.prevPosZ + (entityLiving2.posZ - entityLiving2.prevPosZ) * (double)f1;
            Minecraft.renderPipeline.ModelMatrix.Rotate(this.prevCamRoll + (this.camRoll - this.prevCamRoll) * f1, 0.0F, 0.0F, 1.0F);
			if (entityLiving2.PlayerSleeping)
			{
				f3 = (float)((double)f3 + 1.0D);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.3F, 0.0F);
				if (!this.mc.gameSettings.debugCamEnable)
				{
					int i10 = this.mc.theWorld.getBlockId(MathHelper.floor_double(entityLiving2.posX), MathHelper.floor_double(entityLiving2.posY), MathHelper.floor_double(entityLiving2.posZ));
					if (i10 == Block.bed.blockID)
					{
						int i11 = this.mc.theWorld.getBlockMetadata(MathHelper.floor_double(entityLiving2.posX), MathHelper.floor_double(entityLiving2.posY), MathHelper.floor_double(entityLiving2.posZ));
						int i12 = i11 & 3;
                        Minecraft.renderPipeline.ModelMatrix.Rotate((float)(i12 * 90), 0.0F, 1.0F, 0.0F);
					}

                    Minecraft.renderPipeline.ModelMatrix.Rotate(entityLiving2.prevRotationYaw + (entityLiving2.rotationYaw - entityLiving2.prevRotationYaw) * f1 + 180.0F, 0.0F, -1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(entityLiving2.prevRotationPitch + (entityLiving2.rotationPitch - entityLiving2.prevRotationPitch) * f1, -1.0F, 0.0F, 0.0F);
				}
			}
			else if (this.mc.gameSettings.thirdPersonView > 0)
			{
				double d27 = (double)(this.thirdPersonDistanceTemp + (this.thirdPersonDistance - this.thirdPersonDistanceTemp) * f1);
				float f13;
				float f28;
				if (this.mc.gameSettings.debugCamEnable)
				{
					f28 = this.prevDebugCamYaw + (this.debugCamYaw - this.prevDebugCamYaw) * f1;
					f13 = this.prevDebugCamPitch + (this.debugCamPitch - this.prevDebugCamPitch) * f1;
					Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, (float)(-d27));
					Minecraft.renderPipeline.ModelMatrix.Rotate(f13, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(f28, 0.0F, 1.0F, 0.0F);
				}
				else
				{
					f28 = entityLiving2.rotationYaw;
					f13 = entityLiving2.rotationPitch;
					if (this.mc.gameSettings.thirdPersonView == 2)
					{
						f13 += 180.0F;
					}

					double d14 = (double)(-MathHelper.sin(f28 / 180.0F * (float)Math.PI) * MathHelper.cos(f13 / 180.0F * (float)Math.PI)) * d27;
					double d16 = (double)(MathHelper.cos(f28 / 180.0F * (float)Math.PI) * MathHelper.cos(f13 / 180.0F * (float)Math.PI)) * d27;
					double d18 = (double)(-MathHelper.sin(f13 / 180.0F * (float)Math.PI)) * d27;

					for (int i20 = 0; i20 < 8; ++i20)
					{
						float f21 = (float)((i20 & 1) * 2 - 1);
						float f22 = (float)((i20 >> 1 & 1) * 2 - 1);
						float f23 = (float)((i20 >> 2 & 1) * 2 - 1);
						f21 *= 0.1F;
						f22 *= 0.1F;
						f23 *= 0.1F;
						MovingObjectPosition movingObjectPosition24 = this.mc.theWorld.rayTraceBlocks(Vec3D.createVector(d4 + (double)f21, d6 + (double)f22, d8 + (double)f23), Vec3D.createVector(d4 - d14 + (double)f21 + (double)f23, d6 - d18 + (double)f22, d8 - d16 + (double)f23));
						if (movingObjectPosition24 != null)
						{
							double d25 = movingObjectPosition24.hitVec.distanceTo(Vec3D.createVector(d4, d6, d8));
							if (d25 < d27)
							{
								d27 = d25;
							}
						}
					}

					if (this.mc.gameSettings.thirdPersonView == 2)
					{
                        Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
					}

					Minecraft.renderPipeline.ModelMatrix.Rotate(entityLiving2.rotationPitch - f13, 1.0F, 0.0F, 0.0F);
					Minecraft.renderPipeline.ModelMatrix.Rotate(entityLiving2.rotationYaw - f28, 0.0F, 1.0F, 0.0F);
					Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, (float)(-d27));
					Minecraft.renderPipeline.ModelMatrix.Rotate(f28 - entityLiving2.rotationYaw, 0.0F, 1.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(f13 - entityLiving2.rotationPitch, 1.0F, 0.0F, 0.0F);
				}
			}
			else
			{
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -0.1F); // TODO: this is an oddly small number. Make sure this isn't just
																			   // the result of floating point precision from the decompiler.
			}

			if (!this.mc.gameSettings.debugCamEnable)
			{
				Minecraft.renderPipeline.ModelMatrix.Rotate(entityLiving2.prevRotationPitch + (entityLiving2.rotationPitch - entityLiving2.prevRotationPitch) * f1, 1.0F, 0.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(entityLiving2.prevRotationYaw + (entityLiving2.rotationYaw - entityLiving2.prevRotationYaw) * f1 + 180.0F, 0.0F, 1.0F, 0.0F);
			}

            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, f3, 0.0F);
			d4 = entityLiving2.prevPosX + (entityLiving2.posX - entityLiving2.prevPosX) * (double)f1;
			d6 = entityLiving2.prevPosY + (entityLiving2.posY - entityLiving2.prevPosY) * (double)f1 - (double)f3;
			d8 = entityLiving2.prevPosZ + (entityLiving2.posZ - entityLiving2.prevPosZ) * (double)f1;
			this.cloudFog = this.mc.renderGlobal.func_27307_a(d4, d6, d8, f1);
		}

		private void setupCameraTransform(float f1, int i2)
		{
			this.farPlaneDistance = (float)(256 >> this.mc.gameSettings.renderDistance);

			Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();

			float f3 = 0.07F;

			if (this.cameraZoom != 1.0D)
			{
                Minecraft.renderPipeline.ProjectionMatrix.Translate((float)this.cameraYaw, (float)(-this.cameraPitch), 0.0F);
                Minecraft.renderPipeline.ProjectionMatrix.Scale((float)this.cameraZoom, (float)this.cameraZoom, 1.0F);
            }
            
			Matrix4 cameraMatrix = Glu.Perspective(getFOVModifier(f1, true), (float)mc.displayWidth / (float)mc.displayHeight, 0.05F, farPlaneDistance * 2.0F);
			Minecraft.renderPipeline.ProjectionMatrix.MultMatrix(cameraMatrix); 

            float f4;
			if (mc.playerController.IsPanoramaCamera())
			{
				f4 = 0.6666667F;
				Minecraft.renderPipeline.ProjectionMatrix.Scale(1.0F, f4, 1.0F);
			}
            
			Minecraft.renderPipeline.ModelMatrix.LoadIdentity();

			this.hurtCameraEffect(f1);
			if (this.mc.gameSettings.viewBobbing)
			{
				this.setupViewBobbing(f1);
			}

			f4 = this.mc.thePlayer.prevTimeInPortal + (this.mc.thePlayer.timeInPortal - this.mc.thePlayer.prevTimeInPortal) * f1;
			if (f4 > 0.0F)
			{
				sbyte b5 = 20;
				if (this.mc.thePlayer.isPotionActive(Potion.confusion))
				{
					b5 = 7;
				}

				float f6 = 5.0F / (f4 * f4 + 5.0F) - f4 * 0.04F;
				f6 *= f6;

				Minecraft.renderPipeline.ModelMatrix.Rotate(((float)this.rendererUpdateCount + f1) * (float)b5, 0.0F, 1.0F, 1.0F);
				Minecraft.renderPipeline.ModelMatrix.Scale(1.0F / f6, 1.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-((float)this.rendererUpdateCount + f1) * (float)b5, 0.0F, 1.0F, 1.0F);
			}

			this.orientCamera(f1);
			if (this.debugViewDirection > 0)
			{
				int i7 = this.debugViewDirection - 1;
				if (i7 == 1)
				{
                    Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 1.0F, 0.0F);
				}

				if (i7 == 2)
				{
                    Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
				}

				if (i7 == 3)
				{
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
				}

				if (i7 == 4)
				{
                    Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 1.0F, 0.0F, 0.0F);
				}

				if (i7 == 5)
				{
                    Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
				}
			}

		}

		private void renderHand(float f1, int i2)
		{
			if (this.debugViewDirection <= 0)
			{
				Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();

				float f3 = 0.07F;

				if (this.cameraZoom != 1.0D)
				{
                    Minecraft.renderPipeline.ProjectionMatrix.Translate((float)this.cameraYaw, (float)(-this.cameraPitch), 0.0F);
                    Minecraft.renderPipeline.ProjectionMatrix.Scale((float)this.cameraZoom, (float)this.cameraZoom, 1.0F);
				}
                
                Matrix4 cameraMatrix = Glu.Perspective(getFOVModifier(f1, false), (float)mc.displayWidth / (float)mc.displayHeight, 0.05F, farPlaneDistance * 2.0F);
				Minecraft.renderPipeline.ProjectionMatrix.MultMatrix(cameraMatrix);

                if (this.mc.playerController.IsPanoramaCamera())
				{
					float f4 = 0.6666667F;
                    Minecraft.renderPipeline.ProjectionMatrix.Scale(1.0F, f4, 1.0F);
				}
                
                Minecraft.renderPipeline.ModelMatrix.LoadIdentity();

				Minecraft.renderPipeline.ModelMatrix.PushMatrix();

                this.hurtCameraEffect(f1);
				if (this.mc.gameSettings.viewBobbing)
				{
					this.setupViewBobbing(f1);
				}

				if (this.mc.gameSettings.thirdPersonView == 0 && !this.mc.renderViewEntity.PlayerSleeping && !this.mc.gameSettings.hideGUI && !this.mc.playerController.IsPanoramaCamera())
				{
					this.enableLightmap((double)f1);
					this.itemRenderer.renderItemInFirstPerson(f1);
					this.disableLightmap((double)f1);
				}
                
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                if (this.mc.gameSettings.thirdPersonView == 0 && !this.mc.renderViewEntity.PlayerSleeping)
				{
					this.itemRenderer.renderOverlays(f1);
					this.hurtCameraEffect(f1);
				}

				if (this.mc.gameSettings.viewBobbing)
				{
					this.setupViewBobbing(f1);
				}

			}
		}

		public virtual void disableLightmap(double d1)
		{
            Minecraft.renderPipeline.SetState(RenderState.LightmapState, false);
            LightmapManager.ActiveTexture = LightmapManager.defaultTexUnit;
		}
        
		public virtual void enableLightmap(double d1)
		{
			LightmapManager.ActiveTexture = LightmapManager.lightmapTexUnit;
			Minecraft.renderPipeline.TextureMatrix.LoadIdentity();
			float f3 = 0.00390625F;
            Minecraft.renderPipeline.TextureMatrix.Scale(f3, f3, f3);
            Minecraft.renderPipeline.TextureMatrix.Translate(8.0F, 8.0F, 8.0F);
			this.mc.renderEngine.bindTexture(this.lightmapTexture);
            Minecraft.renderPipeline.SetState(RenderState.LightmapState, true);
            /*Tessellator tessellator = Tessellator.instance;

			// DEBUG: render the lightmap texture on the screen
			Minecraft.newRenderer.ProjectionMatrix.PushMatrix();
            Minecraft.newRenderer.ProjectionMatrix.LoadIdentity();

            Minecraft.newRenderer.ProjectionMatrix.Ortho(0.0D, mc.displayWidth, mc.displayHeight, 0.0D, 1000.0D, 3000.0D);

            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            Minecraft.newRenderer.ModelMatrix.LoadIdentity();
            Minecraft.newRenderer.ModelMatrix.Translate(0.0F, 0.0F, -2000.0F);

			GL.Disable(EnableCap.CullFace);
			Minecraft.newRenderer.SetState(RenderState.TextureState, false);
            Minecraft.newRenderer.SetState(RenderState.LightmapState, true);
            Minecraft.newRenderer.SetState(RenderState.LightingState, false);
            tessellator.startDrawingQuads();
			tessellator.Brightness = 983055;
            tessellator.AddVertexWithUV(0.0D, (double)mc.displayHeight, 0.0D, 0.0D, 1.0D);
            tessellator.AddVertexWithUV((double)mc.displayWidth, (double)mc.displayHeight, 0.0D, 1.0D, 1.0D);
            tessellator.AddVertexWithUV((double)mc.displayWidth, 0.0D, 0.0D, 1.0D, 0.0D);
            tessellator.AddVertexWithUV(0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
            tessellator.DrawImmediate();
            Minecraft.newRenderer.SetState(RenderState.TextureState, true);
            Minecraft.newRenderer.SetState(RenderState.LightingState, true);
            Minecraft.newRenderer.SetState(RenderState.LightmapState, false);
            GL.Enable(EnableCap.CullFace);

            Minecraft.newRenderer.ModelMatrix.PopMatrix();
            Minecraft.newRenderer.ProjectionMatrix.PopMatrix();*/


            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureManager.TextureFilterLinear);
			GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureManager.TextureFilterLinear);
			GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureManager.TextureFilterLinear);
			GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureManager.TextureFilterLinear);
			GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, TextureManager.TextureWrapClamp);
			GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, TextureManager.TextureWrapClamp);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
            LightmapManager.ActiveTexture = LightmapManager.defaultTexUnit;
		}

		private void updateTorchFlicker()
		{
			this.torchFlickerDX = (float)((double)this.torchFlickerDX + (portinghelpers.MathHelper.NextDouble - portinghelpers.MathHelper.NextDouble) * portinghelpers.MathHelper.NextDouble * portinghelpers.MathHelper.NextDouble);
			this.torchFlickerDY = (float)((double)this.torchFlickerDY + (portinghelpers.MathHelper.NextDouble - portinghelpers.MathHelper.NextDouble) * portinghelpers.MathHelper.NextDouble * portinghelpers.MathHelper.NextDouble);
			this.torchFlickerDX = (float)((double)this.torchFlickerDX * 0.9D);
			this.torchFlickerDY = (float)((double)this.torchFlickerDY * 0.9D);
			this.torchFlickerX += (this.torchFlickerDX - this.torchFlickerX) * 1.0F;
			this.torchFlickerY += (this.torchFlickerDY - this.torchFlickerY) * 1.0F;
			this.lightmapUpdateNeeded = true;
		}

		private void updateLightmap()
		{
			World world1 = this.mc.theWorld;
			if (world1 != null)
			{
				Profiler.startSection("sky_tex_world");
				for (int i2 = 0; i2 < 256; ++i2)
				{
					float f3 = world1.getSkyBrightness(1.0F) * 0.95F + 0.05F;
					float f4 = world1.worldProvider.lightBrightnessTable[i2 / 16] * f3;
					float f5 = world1.worldProvider.lightBrightnessTable[i2 % 16] * (this.torchFlickerX * 0.1F + 1.5F);
					if (world1.lightningFlash > 0)
					{
						f4 = world1.worldProvider.lightBrightnessTable[i2 / 16];
					}

					float f6 = f4 * (world1.getSkyBrightness(1.0F) * 0.65F + 0.35F);
					float f7 = f4 * (world1.getSkyBrightness(1.0F) * 0.65F + 0.35F);
					float f10 = f5 * ((f5 * 0.6F + 0.4F) * 0.6F + 0.4F);
					float f11 = f5 * (f5 * f5 * 0.6F + 0.4F);
					float f12 = f6 + f5;
					float f13 = f7 + f10;
					float f14 = f4 + f11;
					f12 = f12 * 0.96F + 0.03F;
					f13 = f13 * 0.96F + 0.03F;
					f14 = f14 * 0.96F + 0.03F;
					if (world1.worldProvider.worldType == 1)
					{
						f12 = 0.22F + f5 * 0.75F;
						f13 = 0.28F + f10 * 0.75F;
						f14 = 0.25F + f11 * 0.75F;
					}

					float f15 = this.mc.gameSettings.gammaSetting;
					if (f12 > 1.0F)
					{
						f12 = 1.0F;
					}

					if (f13 > 1.0F)
					{
						f13 = 1.0F;
					}

					if (f14 > 1.0F)
					{
						f14 = 1.0F;
					}

					float f16 = 1.0F - f12;
					float f17 = 1.0F - f13;
					float f18 = 1.0F - f14;
					f16 = 1.0F - f16 * f16 * f16 * f16;
					f17 = 1.0F - f17 * f17 * f17 * f17;
					f18 = 1.0F - f18 * f18 * f18 * f18;
					f12 = f12 * (1.0F - f15) + f16 * f15;
					f13 = f13 * (1.0F - f15) + f17 * f15;
					f14 = f14 * (1.0F - f15) + f18 * f15;
					f12 = f12 * 0.96F + 0.03F;
					f13 = f13 * 0.96F + 0.03F;
					f14 = f14 * 0.96F + 0.03F;
					if (f12 > 1.0F)
					{
						f12 = 1.0F;
					}

					if (f13 > 1.0F)
					{
						f13 = 1.0F;
					}

					if (f14 > 1.0F)
					{
						f14 = 1.0F;
					}

					if (f12 < 0.0F)
					{
						f12 = 0.0F;
					}

					if (f13 < 0.0F)
					{
						f13 = 0.0F;
					}

					if (f14 < 0.0F)
					{
						f14 = 0.0F;
					}

					short s19 = 255;
					int i20 = (int)(f12 * 255.0F);
					int i21 = (int)(f13 * 255.0F);
					int i22 = (int)(f14 * 255.0F);
					this.lightmapColors[i2] = s19 << 24 | i20 << 16 | i21 << 8 | i22;
				}

				Profiler.endStartSection("sky_tex_create");
				this.mc.renderEngine.createTextureFromBytes(this.lightmapColors, 16, 16, this.lightmapTexture);

				if (mc.mcApplet.KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftBracket))
				{
					byte[] imgBytes = new byte[lightmapColors.Length * 4];
                    System.Buffer.BlockCopy(lightmapColors, 0, imgBytes, 0, lightmapColors.Length * 4);
					Image<Bgra32> outImg = Image.LoadPixelData<Bgra32>(imgBytes, 16, 16);
					outImg.SaveAsPng("lightmaptest.png");
				}
				Profiler.endSection();
			}
		}

		public virtual void updateCameraAndRender(float f1)
		{
            Profiler.startSection("lightTex");
			if (this.lightmapUpdateNeeded)
			{
				this.updateLightmap();
			}

			Profiler.endSection();
			if (!mc.mcApplet.IsFocused)
			{
				if (DateTimeHelper.CurrentUnixTimeMillis() - this.prevFrameTime > 500L)
				{
					this.mc.displayInGameMenu();
				}
			}
			else
			{
				this.prevFrameTime = DateTimeHelper.CurrentUnixTimeMillis();
			}

			Profiler.startSection("mouse");
			if (this.mc.inGameHasFocus)
			{
				this.mc.mouseHelper.mouseXYChange();
				float f2 = this.mc.gameSettings.mouseSensitivity * 0.6F + 0.2F;
				float f3 = f2 * f2 * f2 * 8.0F;
				float f4 = (float)this.mc.mouseHelper.deltaX * f3;
				float f5 = (float)this.mc.mouseHelper.deltaY * f3;
				sbyte b6 = 1;
				if (this.mc.gameSettings.invertMouse)
				{
					b6 = -1;
				}

				if (this.mc.gameSettings.smoothCamera)
				{
					this.smoothCamYaw += f4;
					this.smoothCamPitch += f5;
					float f7 = f1 - this.smoothCamPartialTicks;
					this.smoothCamPartialTicks = f1;
					f4 = this.smoothCamFilterX * f7;
					f5 = this.smoothCamFilterY * f7;
					this.mc.thePlayer.setAngles(f4, f5 * (float)b6);
				}
				else
				{
					this.mc.thePlayer.setAngles(f4, f5 * (float)b6);
				}
            }
            
            Profiler.endSection();
			if (!this.mc.skipRenderWorld)
			{
				Profiler.startSection("scale_resolution");
				ScaledResolution scaledResolution13 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
				int i14 = scaledResolution13.ScaledWidth;
				int i15 = scaledResolution13.ScaledHeight;
				int i16 = (int)mc.MouseX * i14 / this.mc.displayWidth;
				int i17 = i15 - (int)mc.MouseY * i15 / this.mc.displayHeight - 1;
				short s18 = 200;
				if (this.mc.gameSettings.limitFramerate == 1)
				{
					s18 = 120;
				}

				if (this.mc.gameSettings.limitFramerate == 2)
				{
					s18 = 40;
				}

				Profiler.endSection();

				long sleepMs;
				if (this.mc.theWorld != null)
				{
					Profiler.startSection("level");
					if (this.mc.gameSettings.limitFramerate == 0)
					{
                        this.renderWorld(f1, 0L);
					}
					else
					{
						this.renderWorld(f1, this.renderEndNanoTime + (long)(1000000000 / s18));
					}

					Profiler.endStartSection("sleep");
					if (this.mc.gameSettings.limitFramerate == 2)
					{
						sleepMs = (this.renderEndNanoTime + (long)(1000000000 / s18) - JTime.NanoTime()) / 1000000L;
						if (sleepMs > 0L && sleepMs < 500L)
						{
							Thread.Sleep((int)sleepMs);
						}
					}

					this.renderEndNanoTime = JTime.NanoTime();
					Profiler.endStartSection("gui");
					if (!this.mc.gameSettings.hideGUI || this.mc.currentScreen != null)
					{
						this.mc.ingameGUI.renderGameOverlay(f1, this.mc.currentScreen != null, i16, i17);
					}

					Profiler.endSection();
				}
				else
				{
                    GL.Viewport(0, 0, this.mc.displayWidth, this.mc.displayHeight);

					Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();
					Minecraft.renderPipeline.ModelMatrix.LoadIdentity();

                    this.setupOverlayRendering();
					sleepMs = (renderEndNanoTime + (long)(1000000000 / s18) - JTime.NanoTime()) / 1000000L;
					if (sleepMs < 0L)
					{
						sleepMs += 10L;
					}

					if (sleepMs > 0L && sleepMs < 500L)
					{
						Thread.Sleep((int)sleepMs);
					}

					this.renderEndNanoTime = JTime.NanoTime();
				}

				if (this.mc.currentScreen != null)
				{
					Profiler.startSection("screen_render");
					GL.Clear(ClearBufferMask.DepthBufferBit);
					this.mc.currentScreen.drawScreen(i16, i17, f1);
					if (this.mc.currentScreen != null && this.mc.currentScreen.guiParticles != null)
					{
						this.mc.currentScreen.guiParticles.draw(f1);
					}
					Profiler.endSection();
				}

			}
		}

		public virtual void renderWorld(float f1, long j2)
		{
			Profiler.startSection("lightTex");
			if (this.lightmapUpdateNeeded)
			{
				this.updateLightmap();
			}

			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
			if (this.mc.renderViewEntity == null)
			{
				this.mc.renderViewEntity = this.mc.thePlayer;
			}

			Profiler.endStartSection("pick");
			this.getMouseOver(f1);
			EntityLiving entityLiving4 = this.mc.renderViewEntity;
			RenderGlobal renderGlobal5 = this.mc.renderGlobal;
			EffectRenderer effectRenderer6 = this.mc.effectRenderer;
			double d7 = entityLiving4.lastTickPosX + (entityLiving4.posX - entityLiving4.lastTickPosX) * (double)f1;
			double d9 = entityLiving4.lastTickPosY + (entityLiving4.posY - entityLiving4.lastTickPosY) * (double)f1;
			double d11 = entityLiving4.lastTickPosZ + (entityLiving4.posZ - entityLiving4.lastTickPosZ) * (double)f1;
			Profiler.endStartSection("center");
			IChunkProvider iChunkProvider13 = this.mc.theWorld.ChunkProvider;
			int i16;
			if (iChunkProvider13 is ChunkProviderLoadOrGenerate)
			{
				ChunkProviderLoadOrGenerate chunkProviderLoadOrGenerate14 = (ChunkProviderLoadOrGenerate)iChunkProvider13;
				int i15 = MathHelper.floor_float((float)((int)d7)) >> 4;
				i16 = MathHelper.floor_float((float)((int)d11)) >> 4;
				chunkProviderLoadOrGenerate14.setCurrentChunkOver(i15, i16);
			}

			for (int i18 = 0; i18 < 2; ++i18)
			{
				Profiler.endStartSection("clear");
				GL.Viewport(0, 0, this.mc.displayWidth, this.mc.displayHeight);
				this.updateFogColor(f1);
				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
				GL.Enable(EnableCap.CullFace);
				Profiler.endStartSection("camera");
				setupCameraTransform(f1, i18);
				ActiveRenderInfo.updateRenderInfo(this.mc.thePlayer, this.mc.gameSettings.thirdPersonView == 2);
				Profiler.endStartSection("frustrum");
				ClippingHelperImpl.getInstance();
				if (this.mc.gameSettings.renderDistance < 2)
				{
					this.SetupFog(-1, f1);
					Profiler.endStartSection("sky");
					renderGlobal5.renderSky(f1);
				}
                
				Minecraft.renderPipeline.SetState(RenderState.FogState, true);
				SetupFog(1, f1);
				if (mc.gameSettings.ambientOcclusion)
				{
                    Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, true);
                }

				Profiler.endStartSection("culling");
				Frustrum frustrum19 = new();
				frustrum19.setPosition(d7, d9, d11);
				this.mc.renderGlobal.clipRenderersByFrustum(frustrum19, f1);
				if (i18 == 0)
				{
					Profiler.endStartSection("updatechunks");

					while (!this.mc.renderGlobal.updateRenderers(entityLiving4, false) && j2 != 0L)
					{
						long j20 = j2 - JTime.NanoTime();
						if (j20 < 0L || j20 > 1000000000L)
						{
							break;
						}
					}
				}

				this.SetupFog(0, f1);
				Minecraft.renderPipeline.SetState(RenderState.FogState, true);
				GL.BindTexture(TextureTarget.Texture2D, mc.renderEngine.getTexture("/terrain.png"));
				GameLighting.DisableMeshLighting();
				Profiler.endStartSection("terrain");
				renderGlobal5.sortAndRender(entityLiving4, 0, (double)f1);
				Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, false);
                EntityPlayer entityPlayer21;
				if (this.debugViewDirection == 0)
				{
					GameLighting.EnableMeshLighting();
					Profiler.endStartSection("entities");
					renderGlobal5.renderEntities(entityLiving4.getPosition(f1), frustrum19, f1);
					this.enableLightmap((double)f1);
					Profiler.endStartSection("litParticles");
					effectRenderer6.func_1187_b(entityLiving4, f1);
					GameLighting.DisableMeshLighting();
					this.SetupFog(0, f1);
					Profiler.endStartSection("particles");
					effectRenderer6.renderParticles(entityLiving4, f1);
					this.disableLightmap((double)f1);
					if (this.mc.objectMouseOver != null && entityLiving4.isInsideOfMaterial(Material.water) && entityLiving4 is EntityPlayer && !this.mc.gameSettings.hideGUI)
					{
						entityPlayer21 = (EntityPlayer)entityLiving4;
                        Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                        Profiler.endStartSection("outline");
						renderGlobal5.drawBlockBreaking(entityPlayer21, this.mc.objectMouseOver, 0, entityPlayer21.inventory.CurrentItem, f1);
						renderGlobal5.drawSelectionBox(entityPlayer21, this.mc.objectMouseOver, 0, entityPlayer21.inventory.CurrentItem, f1);
                        Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                    }
				}

				GL.Disable(EnableCap.Blend);
				GL.Enable(EnableCap.CullFace);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                GL.DepthMask(true);
				this.SetupFog(0, f1);
				GL.Enable(EnableCap.Blend);
				GL.Disable(EnableCap.CullFace);
				GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/terrain.png"));
				if (this.mc.gameSettings.fancyGraphics)
				{
					Profiler.endStartSection("water");
					if (this.mc.gameSettings.ambientOcclusion)
					{
                        Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, true);
                    }
                    
					i16 = renderGlobal5.sortAndRender(entityLiving4, 1, (double)f1);

					if (i16 > 0)
					{
						renderGlobal5.RenderAllWorldRenderers(1, (double)f1);
					}

                    Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, false);
                }
				else
				{
					Profiler.endStartSection("water");
					renderGlobal5.sortAndRender(entityLiving4, 1, (double)f1);
				}

				GL.DepthMask(true);
				GL.Enable(EnableCap.CullFace);
				GL.Disable(EnableCap.Blend);
				if (this.cameraZoom == 1.0D && entityLiving4 is EntityPlayer && !this.mc.gameSettings.hideGUI && this.mc.objectMouseOver != null && !entityLiving4.isInsideOfMaterial(Material.water))
				{
					entityPlayer21 = (EntityPlayer)entityLiving4;
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                    Profiler.endStartSection("outline");
					renderGlobal5.drawBlockBreaking(entityPlayer21, this.mc.objectMouseOver, 0, entityPlayer21.inventory.CurrentItem, f1);
					renderGlobal5.drawSelectionBox(entityPlayer21, this.mc.objectMouseOver, 0, entityPlayer21.inventory.CurrentItem, f1);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                }

				Profiler.endStartSection("weather");
				this.renderRainSnow(f1);
                Minecraft.renderPipeline.SetState(RenderState.FogState, false);
                if (this.pointedEntity != null)
				{
					;
				}

				if (this.mc.gameSettings.shouldRenderClouds())
				{
					Profiler.endStartSection("clouds");
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    this.SetupFog(0, f1);
                    Minecraft.renderPipeline.SetState(RenderState.FogState, true);
                    renderGlobal5.renderClouds(f1);
                    Minecraft.renderPipeline.SetState(RenderState.FogState, false);
                    this.SetupFog(1, f1);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}

				Profiler.endStartSection("hand");
				if (this.cameraZoom == 1.0D)
				{
					GL.Clear(ClearBufferMask.DepthBufferBit);
					this.renderHand(f1, i18);
				}
			}

            GL.ColorMask(true, true, true, false);
			Profiler.endSection();
		}

		private void addRainParticles()
		{
			float f1 = this.mc.theWorld.getRainStrength(1.0F);
			if (!this.mc.gameSettings.fancyGraphics)
			{
				f1 /= 2.0F;
			}

			if (f1 != 0.0F)
			{
				random.SetSeed((long)this.rendererUpdateCount * 312987231L);
				EntityLiving entityLiving2 = this.mc.renderViewEntity;
				World world3 = this.mc.theWorld;
				int i4 = MathHelper.floor_double(entityLiving2.posX);
				int i5 = MathHelper.floor_double(entityLiving2.posY);
				int i6 = MathHelper.floor_double(entityLiving2.posZ);
				sbyte b7 = 10;
				double d8 = 0.0D;
				double d10 = 0.0D;
				double d12 = 0.0D;
				int i14 = 0;
				int i15 = (int)(100.0F * f1 * f1);
				if (this.mc.gameSettings.particleSetting == 1)
				{
					i15 >>= 1;
				}
				else if (this.mc.gameSettings.particleSetting == 2)
				{
					i15 = 0;
				}

				for (int i16 = 0; i16 < i15; ++i16)
				{
					int i17 = i4 + this.random.Next(b7) - this.random.Next(b7);
					int i18 = i6 + this.random.Next(b7) - this.random.Next(b7);
					int i19 = world3.getPrecipitationHeight(i17, i18);
					int i20 = world3.getBlockId(i17, i19 - 1, i18);
					BiomeGenBase biomeGenBase21 = world3.getBiomeGenForCoords(i17, i18);
					if (i19 <= i5 + b7 && i19 >= i5 - b7 && biomeGenBase21.canSpawnLightningBolt() && biomeGenBase21.FloatTemperature > 0.2F)
					{
						float f22 = this.random.NextSingle();
						float f23 = this.random.NextSingle();
						if (i20 > 0)
						{
							if (Block.blocksList[i20].blockMaterial == Material.lava)
							{
								this.mc.effectRenderer.addEffect(new EntitySmokeFX(world3, (double)((float)i17 + f22), (double)((float)i19 + 0.1F) - Block.blocksList[i20].minY, (double)((float)i18 + f23), 0.0D, 0.0D, 0.0D));
							}
							else
							{
								++i14;
								if (this.random.Next(i14) == 0)
								{
									d8 = (double)((float)i17 + f22);
									d10 = (double)((float)i19 + 0.1F) - Block.blocksList[i20].minY;
									d12 = (double)((float)i18 + f23);
								}

								this.mc.effectRenderer.addEffect(new EntityRainFX(world3, (double)((float)i17 + f22), (double)((float)i19 + 0.1F) - Block.blocksList[i20].minY, (double)((float)i18 + f23)));
							}
						}
					}
				}

				if (i14 > 0 && this.random.Next(3) < this.rainSoundCounter++)
				{
					this.rainSoundCounter = 0;
					if (d10 > entityLiving2.posY + 1.0D && world3.getPrecipitationHeight(MathHelper.floor_double(entityLiving2.posX), MathHelper.floor_double(entityLiving2.posZ)) > MathHelper.floor_double(entityLiving2.posY))
					{
						this.mc.theWorld.playSoundEffect(d8, d10, d12, "ambient.weather.rain", 0.1F, 0.5F);
					}
					else
					{
						this.mc.theWorld.playSoundEffect(d8, d10, d12, "ambient.weather.rain", 0.2F, 1.0F);
					}
				}

			}
		}

		protected internal virtual void renderRainSnow(float f1)
		{
			float f2 = this.mc.theWorld.getRainStrength(f1);
			if (f2 > 0.0F)
			{
				this.enableLightmap((double)f1);
				if (this.rainXCoords == null)
				{
					this.rainXCoords = new float[1024];
					this.rainYCoords = new float[1024];

					for (int i3 = 0; i3 < 32; ++i3)
					{
						for (int i4 = 0; i4 < 32; ++i4)
						{
							float f5 = (float)(i4 - 16);
							float f6 = (float)(i3 - 16);
							float f7 = MathHelper.sqrt_float(f5 * f5 + f6 * f6);
							this.rainXCoords[i3 << 5 | i4] = -f6 / f7;
							this.rainYCoords[i3 << 5 | i4] = f5 / f7;
						}
					}
				}

				EntityLiving entityLiving41 = this.mc.renderViewEntity;
				World world42 = this.mc.theWorld;
				int i43 = MathHelper.floor_double(entityLiving41.posX);
				int i44 = MathHelper.floor_double(entityLiving41.posY);
				int i45 = MathHelper.floor_double(entityLiving41.posZ);
				Tessellator tessellator8 = Tessellator.instance;
				GL.Disable(EnableCap.CullFace);
                Minecraft.renderPipeline.SetNormal(0.0F, 1.0F, 0.0F);
                GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                Minecraft.renderPipeline.AlphaTestThreshold(0.01f);
                GL.BindTexture(TextureTarget.Texture2D, mc.renderEngine.getTexture("/environment/snow.png"));
				double d9 = entityLiving41.lastTickPosX + (entityLiving41.posX - entityLiving41.lastTickPosX) * (double)f1;
				double d11 = entityLiving41.lastTickPosY + (entityLiving41.posY - entityLiving41.lastTickPosY) * (double)f1;
				double d13 = entityLiving41.lastTickPosZ + (entityLiving41.posZ - entityLiving41.lastTickPosZ) * (double)f1;
				int i15 = MathHelper.floor_double(d11);
				sbyte b16 = 5;
				if (this.mc.gameSettings.fancyGraphics)
				{
					b16 = 10;
				}

				bool z17 = false;
				sbyte b18 = -1;
				float f19 = (float)this.rendererUpdateCount + f1;
				if (this.mc.gameSettings.fancyGraphics)
				{
					b16 = 10;
				}

                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				z17 = false;

				for (int i20 = i45 - b16; i20 <= i45 + b16; ++i20)
				{
					for (int i21 = i43 - b16; i21 <= i43 + b16; ++i21)
					{
						int i22 = (i20 - i45 + 16) * 32 + i21 - i43 + 16;
						float f23 = this.rainXCoords[i22] * 0.5F;
						float f24 = this.rainYCoords[i22] * 0.5F;
						BiomeGenBase biomeGenBase25 = world42.getBiomeGenForCoords(i21, i20);
						if (biomeGenBase25.canSpawnLightningBolt() || biomeGenBase25.EnableSnow)
						{
							int i26 = world42.getPrecipitationHeight(i21, i20);
							int i27 = i44 - b16;
							int i28 = i44 + b16;
							if (i27 < i26)
							{
								i27 = i26;
							}

							if (i28 < i26)
							{
								i28 = i26;
							}

							float f29 = 1.0F;
							int i30 = i26;
							if (i26 < i15)
							{
								i30 = i15;
							}

							if (i27 != i28)
							{
								random.SetSeed((long)(i21 * i21 * 3121 + i21 * 45238971 ^ i20 * i20 * 418711 + i20 * 13761));
								float f31 = biomeGenBase25.FloatTemperature;
								float f32;
								double d35;
								if (world42.WorldChunkManager.getTemperatureAtHeight(f31, i26) >= 0.15F)
								{
									if (b18 != 0)
									{
										if (b18 >= 0)
										{
											tessellator8.DrawImmediate();
										}

										b18 = 0;
										GL.BindTexture(TextureTarget.Texture2D, mc.renderEngine.getTexture("/environment/rain.png"));
										tessellator8.startDrawingQuads();
									}

									f32 = ((float)(this.rendererUpdateCount + i21 * i21 * 3121 + i21 * 45238971 + i20 * i20 * 418711 + i20 * 13761 & 31) + f1) / 32.0F * (3.0F + this.random.NextSingle());
									double d33 = (double)((float)i21 + 0.5F) - entityLiving41.posX;
									d35 = (double)((float)i20 + 0.5F) - entityLiving41.posZ;
									float f37 = MathHelper.sqrt_double(d33 * d33 + d35 * d35) / (float)b16;
									float f38 = 1.0F;
									tessellator8.Brightness = world42.GetLightBrightnessForSkyBlocks(i21, i30, i20, 0);
									tessellator8.setColorRGBA_F(f38, f38, f38, ((1.0F - f37 * f37) * 0.5F + 0.5F) * f2);
									tessellator8.setTranslation(-d9 * 1.0D, -d11 * 1.0D, -d13 * 1.0D);
									tessellator8.AddVertexWithUV((double)((float)i21 - f23) + 0.5D, (double)i27, (double)((float)i20 - f24) + 0.5D, (double)(0.0F * f29), (double)((float)i27 * f29 / 4.0F + f32 * f29));
									tessellator8.AddVertexWithUV((double)((float)i21 + f23) + 0.5D, (double)i27, (double)((float)i20 + f24) + 0.5D, (double)(1.0F * f29), (double)((float)i27 * f29 / 4.0F + f32 * f29));
									tessellator8.AddVertexWithUV((double)((float)i21 + f23) + 0.5D, (double)i28, (double)((float)i20 + f24) + 0.5D, (double)(1.0F * f29), (double)((float)i28 * f29 / 4.0F + f32 * f29));
									tessellator8.AddVertexWithUV((double)((float)i21 - f23) + 0.5D, (double)i28, (double)((float)i20 - f24) + 0.5D, (double)(0.0F * f29), (double)((float)i28 * f29 / 4.0F + f32 * f29));
									tessellator8.setTranslation(0.0D, 0.0D, 0.0D);
								}
								else
								{
									if (b18 != 1)
									{
										if (b18 >= 0)
										{
											tessellator8.DrawImmediate();
										}

										b18 = 1;
										GL.BindTexture(TextureTarget.Texture2D, this.mc.renderEngine.getTexture("/environment/snow.png"));
										tessellator8.startDrawingQuads();
									}

									f32 = ((float)(this.rendererUpdateCount & 511) + f1) / 512.0F;
									float f46 = this.random.NextSingle() + f19 * 0.01F * (float)this.random.NextGaussian();
									float f34 = this.random.NextSingle() + f19 * (float)this.random.NextGaussian() * 0.001F;
									d35 = (double)((float)i21 + 0.5F) - entityLiving41.posX;
									double d47 = (double)((float)i20 + 0.5F) - entityLiving41.posZ;
									float f39 = MathHelper.sqrt_double(d35 * d35 + d47 * d47) / (float)b16;
									float f40 = 1.0F;
									tessellator8.Brightness = (world42.GetLightBrightnessForSkyBlocks(i21, i30, i20, 0) * 3 + 15728880) / 4;
									tessellator8.setColorRGBA_F(f40, f40, f40, ((1.0F - f39 * f39) * 0.3F + 0.5F) * f2);
									tessellator8.setTranslation(-d9 * 1.0D, -d11 * 1.0D, -d13 * 1.0D);
									tessellator8.AddVertexWithUV((double)((float)i21 - f23) + 0.5D, (double)i27, (double)((float)i20 - f24) + 0.5D, (double)(0.0F * f29 + f46), (double)((float)i27 * f29 / 4.0F + f32 * f29 + f34));
									tessellator8.AddVertexWithUV((double)((float)i21 + f23) + 0.5D, (double)i27, (double)((float)i20 + f24) + 0.5D, (double)(1.0F * f29 + f46), (double)((float)i27 * f29 / 4.0F + f32 * f29 + f34));
									tessellator8.AddVertexWithUV((double)((float)i21 + f23) + 0.5D, (double)i28, (double)((float)i20 + f24) + 0.5D, (double)(1.0F * f29 + f46), (double)((float)i28 * f29 / 4.0F + f32 * f29 + f34));
									tessellator8.AddVertexWithUV((double)((float)i21 - f23) + 0.5D, (double)i28, (double)((float)i20 - f24) + 0.5D, (double)(0.0F * f29 + f46), (double)((float)i28 * f29 / 4.0F + f32 * f29 + f34));
									tessellator8.setTranslation(0.0D, 0.0D, 0.0D);
								}
							}
						}
					}
				}

				if (b18 >= 0)
				{
					tessellator8.DrawImmediate();
				}

				GL.Enable(EnableCap.CullFace);
				GL.Disable(EnableCap.Blend);
				Minecraft.renderPipeline.AlphaTestThreshold(0.1f);
				disableLightmap((double)f1);
			}
		}

		public virtual void setupOverlayRendering()
		{
			ScaledResolution scaledResolution1 = new ScaledResolution(this.mc.gameSettings, this.mc.displayWidth, this.mc.displayHeight);
			GL.Clear(ClearBufferMask.DepthBufferBit);
			Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();
            Minecraft.renderPipeline.ProjectionMatrix.Ortho(0.0D, scaledResolution1.scaledWidthD, scaledResolution1.scaledHeightD, 0.0D, 1000.0D, 3000.0D);
			Minecraft.renderPipeline.ModelMatrix.LoadIdentity();

            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -2000.0F);
		}

		private void updateFogColor(float f1)
		{
			World world2 = this.mc.theWorld;
			EntityLiving entityLiving3 = this.mc.renderViewEntity;
			float f4 = 1.0F / (float)(4 - this.mc.gameSettings.renderDistance);
			f4 = 1.0F - (float)Math.Pow((double)f4, 0.25D);
			Vec3D vec3D5 = world2.getSkyColor(this.mc.renderViewEntity, f1);
			float f6 = (float)vec3D5.xCoord;
			float f7 = (float)vec3D5.yCoord;
			float f8 = (float)vec3D5.zCoord;
			Vec3D vec3D9 = world2.getFogColor(f1);
			this.fogColorRed = (float)vec3D9.xCoord;
			this.fogColorGreen = (float)vec3D9.yCoord;
			this.fogColorBlue = (float)vec3D9.zCoord;
			float f11;
			if (this.mc.gameSettings.renderDistance < 2)
			{
				Vec3D vec3D10 = MathHelper.sin(world2.getCelestialAngleRadians(f1)) > 0.0F ? Vec3D.createVector(-1.0D, 0.0D, 0.0D) : Vec3D.createVector(1.0D, 0.0D, 0.0D);
				f11 = (float)entityLiving3.getLook(f1).dotProduct(vec3D10);
				if (f11 < 0.0F)
				{
					f11 = 0.0F;
				}

				if (f11 > 0.0F)
				{
					float[] f12 = world2.worldProvider.calcSunriseSunsetColors(world2.getCelestialAngle(f1), f1);
					if (f12 != null)
					{
						f11 *= f12[3];
						this.fogColorRed = this.fogColorRed * (1.0F - f11) + f12[0] * f11;
						this.fogColorGreen = this.fogColorGreen * (1.0F - f11) + f12[1] * f11;
						this.fogColorBlue = this.fogColorBlue * (1.0F - f11) + f12[2] * f11;
					}
				}
			}

			this.fogColorRed += (f6 - this.fogColorRed) * f4;
			this.fogColorGreen += (f7 - this.fogColorGreen) * f4;
			this.fogColorBlue += (f8 - this.fogColorBlue) * f4;
			float f19 = world2.getRainStrength(f1);
			float f20;
			if (f19 > 0.0F)
			{
				f11 = 1.0F - f19 * 0.5F;
				f20 = 1.0F - f19 * 0.4F;
				this.fogColorRed *= f11;
				this.fogColorGreen *= f11;
				this.fogColorBlue *= f20;
			}

			f11 = world2.getWeightedThunderStrength(f1);
			if (f11 > 0.0F)
			{
				f20 = 1.0F - f11 * 0.5F;
				this.fogColorRed *= f20;
				this.fogColorGreen *= f20;
				this.fogColorBlue *= f20;
			}

			int i21 = ActiveRenderInfo.getBlockIdAtEntityViewpoint(this.mc.theWorld, entityLiving3, f1);
			if (this.cloudFog)
			{
				Vec3D vec3D13 = world2.drawClouds(f1);
				this.fogColorRed = (float)vec3D13.xCoord;
				this.fogColorGreen = (float)vec3D13.yCoord;
				this.fogColorBlue = (float)vec3D13.zCoord;
			}
			else if (i21 != 0 && Block.blocksList[i21].blockMaterial == Material.water)
			{
				this.fogColorRed = 0.02F;
				this.fogColorGreen = 0.02F;
				this.fogColorBlue = 0.2F;
			}
			else if (i21 != 0 && Block.blocksList[i21].blockMaterial == Material.lava)
			{
				this.fogColorRed = 0.6F;
				this.fogColorGreen = 0.1F;
				this.fogColorBlue = 0.0F;
			}

			float f22 = this.fogColor2 + (this.fogColor1 - this.fogColor2) * f1;
			this.fogColorRed *= f22;
			this.fogColorGreen *= f22;
			this.fogColorBlue *= f22;
			double d14 = (entityLiving3.lastTickPosY + (entityLiving3.posY - entityLiving3.lastTickPosY) * (double)f1) * world2.worldProvider.VoidFogYFactor;
			if (entityLiving3.isPotionActive(Potion.blindness))
			{
				int i16 = entityLiving3.getActivePotionEffect(Potion.blindness).Duration;
				if (i16 < 20)
				{
					d14 *= (double)(1.0F - (float)i16 / 20.0F);
				}
				else
				{
					d14 = 0.0D;
				}
			}

			if (d14 < 1.0D)
			{
				if (d14 < 0.0D)
				{
					d14 = 0.0D;
				}

				d14 *= d14;
				this.fogColorRed = (float)((double)this.fogColorRed * d14);
				this.fogColorGreen = (float)((double)this.fogColorGreen * d14);
				this.fogColorBlue = (float)((double)this.fogColorBlue * d14);
			}
            
			GL.ClearColor(this.fogColorRed, this.fogColorGreen, this.fogColorBlue, 0.0F);
		}

		private void SetupFog(int i1, float f2)
		{
			EntityLiving entityLiving3 = this.mc.renderViewEntity;
			bool z4 = false;
			if (entityLiving3 is EntityPlayer)
			{
				z4 = ((EntityPlayer)entityLiving3).capabilities.isCreativeMode;
			}

			if (i1 == 999)
			{
				Minecraft.renderPipeline.FogRenderer.FogColor = new Vector4(0.0F, 0.0F, 0.0F, 1.0F);
                Minecraft.renderPipeline.FogRenderer.FogMode = FogType.Linear;
                Minecraft.renderPipeline.FogRenderer.FogStart = 0.0F;
                Minecraft.renderPipeline.FogRenderer.FogEnd = 8.0F;
			}
			else
			{
                Minecraft.renderPipeline.FogRenderer.FogColor = new Vector4(fogColorRed, fogColorGreen, fogColorBlue, 1.0F);

                Minecraft.renderPipeline.SetNormal(0.0F, -1.0F, 0.0F);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				int i5 = ActiveRenderInfo.getBlockIdAtEntityViewpoint(mc.theWorld, entityLiving3, f2);
				float f6;
				if (entityLiving3.isPotionActive(Potion.blindness))
				{
					f6 = 5.0F;
					int i7 = entityLiving3.getActivePotionEffect(Potion.blindness).Duration;
					if (i7 < 20)
					{
						f6 = 5.0F + (this.farPlaneDistance - 5.0F) * (1.0F - (float)i7 / 20.0F);
					}

                    Minecraft.renderPipeline.FogRenderer.FogMode = FogType.Linear;
                    
                    if (i1 < 0)
					{
                        Minecraft.renderPipeline.FogRenderer.FogStart = 0.0F;
                        Minecraft.renderPipeline.FogRenderer.FogEnd = f6 * 0.8F;
					}
					else
					{
                        Minecraft.renderPipeline.FogRenderer.FogStart = f6 * 0.25F;
                        Minecraft.renderPipeline.FogRenderer.FogEnd = f6;
                    }
				}
				else
				{
					float f8;
					float f9;
					float f10;
					float f11;
					float f12;
					if (this.cloudFog)
					{
                        Minecraft.renderPipeline.FogRenderer.FogMode = FogType.Exp;
                        Minecraft.renderPipeline.FogRenderer.FogDensity = 0.1F;

                        f6 = 1.0F;
						f12 = 1.0F;
						f8 = 1.0F;
					}
					else if (i5 > 0 && Block.blocksList[i5].blockMaterial == Material.water)
					{
                        Minecraft.renderPipeline.FogRenderer.FogMode = FogType.Exp;
                        if (!entityLiving3.isPotionActive(Potion.waterBreathing))
						{
                            Minecraft.renderPipeline.FogRenderer.FogDensity = 0.1F;
                        }
						else
						{
                            Minecraft.renderPipeline.FogRenderer.FogDensity = 0.05F;
                        }

						f6 = 0.4F;
						f12 = 0.4F;
						f8 = 0.9F;
					}
					else if (i5 > 0 && Block.blocksList[i5].blockMaterial == Material.lava)
					{
                        Minecraft.renderPipeline.FogRenderer.FogMode = FogType.Exp;
                        Minecraft.renderPipeline.FogRenderer.FogDensity = 2.0F;
                        f6 = 0.4F;
						f12 = 0.3F;
						f8 = 0.3F;
					}
					else
					{
						f6 = this.farPlaneDistance;
						if (this.mc.theWorld.worldProvider.WorldHasNoSky && !z4)
						{
							double d13 = (double)((entityLiving3.getBrightnessForRender(f2) & 15728640) >> 20) / 16.0D + (entityLiving3.lastTickPosY + (entityLiving3.posY - entityLiving3.lastTickPosY) * (double)f2 + 4.0D) / 32.0D;
							if (d13 < 1.0D)
							{
								if (d13 < 0.0D)
								{
									d13 = 0.0D;
								}

								d13 *= d13;
								f9 = 100.0F * (float)d13;
								if (f9 < 5.0F)
								{
									f9 = 5.0F;
								}

								if (f6 > f9)
								{
									f6 = f9;
								}
							}
						}

						Minecraft.renderPipeline.FogRenderer.FogMode = FogType.Linear;
						if (i1 < 0)
						{
                            Minecraft.renderPipeline.FogRenderer.FogStart = 0.0F;
                            Minecraft.renderPipeline.FogRenderer.FogEnd = f6 * 0.8F;
                        }
						else
						{
                            Minecraft.renderPipeline.FogRenderer.FogStart = f6 * 0.25F;
                            Minecraft.renderPipeline.FogRenderer.FogEnd = f6;
                        }
                        
						if (this.mc.theWorld.worldProvider.func_48218_b((int)entityLiving3.posX, (int)entityLiving3.posZ))
						{
                            Minecraft.renderPipeline.FogRenderer.FogStart = f6 * 0.05F;
                            Minecraft.renderPipeline.FogRenderer.FogEnd = Math.Min(f6, 192.0F) * 0.5F;
						}
					}
				}

				//GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.Ambient); TODO: ColorMaterial
			}
		}

		private float[] setFogColorBuffer(float f1, float f2, float f3, float f4)
		{
			fogColorBuffer[0] = f1;
			fogColorBuffer[1] = f2;
			fogColorBuffer[2] = f3;
			fogColorBuffer[3] = f4;
			return fogColorBuffer;
		}
	}

}