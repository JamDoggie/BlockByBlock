using System;
using System.Collections;
using BlockByBlock.java_extensions;
using BlockByBlock.net.minecraft.client.entity.particle;
using BlockByBlock.net.minecraft.client.entity.render;
using BlockByBlock.net.minecraft.render;
using javax.swing;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render;
using net.minecraft.client.world.render;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

    using Minecraft = net.minecraft.client.Minecraft;

    public class RenderGlobal : IWorldAccess
	{
		public System.Collections.IList tileEntities = new ArrayList();
		private World worldObj;
		private TextureManager renderEngine;
		private List<WorldRenderer> worldRenderersToUpdate = new();
		private WorldRenderer[] sortedWorldRenderers { get; set; }
		private WorldRenderer[] worldRenderers;
		private int renderChunksWide;
		private int renderChunksTall;
		private int renderChunksDeep;
		private int glRenderListBase;
		private Minecraft mc;
		private RenderBlocks globalRenderBlocks;
		private int[] glOcclusionQueryBase;
		private bool occlusionEnabled { get; set; } = false;
		private int cloudOffsetX = 0;
		private int starGLCallList;
		private int glSkyList;
		private int glSkyList2;
		private int minBlockX;
		private int minBlockY;
		private int minBlockZ;
		private int maxBlockX;
		private int maxBlockY;
		private int maxBlockZ;
		private int renderDistance = -1;
		private int renderEntitiesStartupCounter = 2;
		private int countEntitiesTotal;
		private int countEntitiesRendered;
		private int countEntitiesHidden;
		internal int[] dummyBuf50k = new int[50000];
		internal int[] occlusionResult = new int[64];
		private int renderersLoaded;
		private int renderersBeingClipped;
		private int renderersBeingOccluded;
		private int renderersBeingRendered;
		private int renderersSkippingRenderPass;
		private int dummyRenderInt;
		private int worldRenderersCheckIndex;
		private System.Collections.IList activeWorldRenderers = new ArrayList();
		private ChunkMeshAllocator meshAllocator;

		private VertexBuffer starVBO;
        private VertexBuffer sky1VBO;
        private VertexBuffer sky2VBO;
		
		internal double prevSortX = -9999.0D;
		internal double prevSortY = -9999.0D;
		internal double prevSortZ = -9999.0D;
		public float damagePartialTime;
		internal int frustumCheckOffset = 0;

		public RenderGlobal(Minecraft minecraft1, TextureManager renderEngine2)
		{
			mc = minecraft1;
			renderEngine = renderEngine2;
			sbyte b3 = 34;
			sbyte b4 = 32;
			occlusionEnabled = OpenGlCapsChecker.checkARBOcclusion();
			if (this.occlusionEnabled)
			{
				glOcclusionQueryBase = new int[b3 * b3 * b4];
				GL.GenQueries(glOcclusionQueryBase.Length, glOcclusionQueryBase); // PORTING TODO: Wasn't 100% sure about the first argument here. Investigate if this doesn't work.
			}
            
			Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            
            starVBO = GenerateStarModel();
            
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			Tessellator tessellator5 = Tessellator.instance;
            
            sbyte b7 = 64;
			int i8 = 256 / b7 + 2;
			float f6 = 16.0F;

			int i9;
			int i10;
            tessellator5.StartBuildingVBO();
            for (i9 = -b7 * i8; i9 <= b7 * i8; i9 += b7)
			{
				for (i10 = -b7 * i8; i10 <= b7 * i8; i10 += b7)
				{
					
					tessellator5.AddVertex((double)(i9 + 0), (double)f6, (double)(i10 + 0));
					tessellator5.AddVertex((double)(i9 + b7), (double)f6, (double)(i10 + 0));
					tessellator5.AddVertex((double)(i9 + b7), (double)f6, (double)(i10 + b7));
					tessellator5.AddVertex((double)(i9 + 0), (double)f6, (double)(i10 + b7));
					
				}
			}
            sky1VBO = tessellator5.BuildCurrentVBO();
            
            f6 = -16.0F;
			tessellator5.StartBuildingVBO();

			for (i9 = -b7 * i8; i9 <= b7 * i8; i9 += b7)
			{
				for (i10 = -b7 * i8; i10 <= b7 * i8; i10 += b7)
				{
					tessellator5.AddVertex((double)(i9 + b7), (double)f6, (double)(i10 + 0));
					tessellator5.AddVertex((double)(i9 + 0), (double)f6, (double)(i10 + 0));
					tessellator5.AddVertex((double)(i9 + 0), (double)f6, (double)(i10 + b7));
					tessellator5.AddVertex((double)(i9 + b7), (double)f6, (double)(i10 + b7));
				}
			}

			sky2VBO = tessellator5.BuildCurrentVBO();
            
            meshAllocator = new();
        }

		private VertexBuffer GenerateStarModel()
		{
			RandomExtended random1 = new RandomExtended(10842L);
			Tessellator tessellator2 = Tessellator.instance;
			tessellator2.StartBuildingVBO();

			for (int i3 = 0; i3 < 1500; ++i3)
			{
				double d4 = (double)(random1.NextSingle() * 2.0F - 1.0F);
				double d6 = (double)(random1.NextSingle() * 2.0F - 1.0F);
				double d8 = (double)(random1.NextSingle() * 2.0F - 1.0F);
				double d10 = (double)(0.25F + random1.NextSingle() * 0.25F);
				double d12 = d4 * d4 + d6 * d6 + d8 * d8;
				if (d12 < 1.0D && d12 > 0.01D)
				{
					d12 = 1.0D / Math.Sqrt(d12);
					d4 *= d12;
					d6 *= d12;
					d8 *= d12;
					double d14 = d4 * 100.0D;
					double d16 = d6 * 100.0D;
					double d18 = d8 * 100.0D;
					double d20 = Math.Atan2(d4, d8);
					double d22 = Math.Sin(d20);
					double d24 = Math.Cos(d20);
					double d26 = Math.Atan2(Math.Sqrt(d4 * d4 + d8 * d8), d6);
					double d28 = Math.Sin(d26);
					double d30 = Math.Cos(d26);
					double d32 = random1.NextDouble() * Math.PI * 2.0D;
					double d34 = Math.Sin(d32);
					double d36 = Math.Cos(d32);

					for (int i38 = 0; i38 < 4; ++i38)
					{
						double d39 = 0.0D;
						double d41 = (double)((i38 & 2) - 1) * d10;
						double d43 = (double)((i38 + 1 & 2) - 1) * d10;
						double d47 = d41 * d36 - d43 * d34;
						double d49 = d43 * d36 + d41 * d34;
						double d53 = d47 * d28 + d39 * d30;
						double d55 = d39 * d28 - d47 * d30;
						double d57 = d55 * d22 - d49 * d24;
						double d61 = d49 * d22 + d55 * d24;
						tessellator2.AddVertex(d14 + d57, d16 + d53, d18 + d61);
					}
				}
			}

			return tessellator2.BuildCurrentVBO();
		}

		public virtual void changeWorld(World world1)
		{
			if (this.worldObj != null)
			{
				this.worldObj.removeWorldAccess(this);
			}

			this.prevSortX = -9999.0D;
			this.prevSortY = -9999.0D;
			this.prevSortZ = -9999.0D;
			RenderManager.instance.set(world1);
			this.worldObj = world1;
			this.globalRenderBlocks = new RenderBlocks(world1);
			if (world1 != null)
			{
				world1.addWorldAccess(this);
				this.loadRenderers();
			}

		}

		public virtual void loadRenderers()
		{
			if (worldObj != null)
			{
				Block.leaves.GraphicsLevel = mc.gameSettings.fancyGraphics;
				renderDistance = mc.gameSettings.renderDistance;
				int i1;
				if (worldRenderers != null)
				{
					for (i1 = 0; i1 < worldRenderers.Length; ++i1)
					{
						worldRenderers[i1].stopRendering();
					}
				}

				i1 = 64 << 3 - renderDistance;
				if (i1 > 400)
				{
					i1 = 400;
				}

				renderChunksWide = i1 / 16 + 1;
				renderChunksTall = 16;
				renderChunksDeep = i1 / 16 + 1;
				worldRenderers = new WorldRenderer[renderChunksWide * renderChunksTall * renderChunksDeep];
				sortedWorldRenderers = new WorldRenderer[renderChunksWide * renderChunksTall * renderChunksDeep];
				int i2 = 0;
				int i3 = 0;
				minBlockX = 0;
				minBlockY = 0;
				minBlockZ = 0;
				maxBlockX = renderChunksWide;
				maxBlockY = renderChunksTall;
				maxBlockZ = renderChunksDeep;

				int i4;
				for (i4 = 0; i4 < worldRenderersToUpdate.Count; ++i4)
				{
					((WorldRenderer)worldRenderersToUpdate[i4]).needsUpdate = false;
				}

				worldRenderersToUpdate.Clear();
				tileEntities.Clear();

				for (i4 = 0; i4 < renderChunksWide; ++i4)
				{
					for (int i5 = 0; i5 < renderChunksTall; ++i5)
					{
						for (int i6 = 0; i6 < renderChunksDeep; ++i6)
						{
							worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4] = new WorldRenderer(worldObj, tileEntities, i4 * 16, i5 * 16, i6 * 16, meshAllocator);
							if (occlusionEnabled)
							{
								worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4].glOcclusionQuery = glOcclusionQueryBase[i3];
							}

							worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4].isWaitingOnOcclusionQuery = false;
							worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4].isVisible = true;
							worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4].isInFrustum = true;
							worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4].chunkIndex = i3++;
							worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4].markDirty();
							sortedWorldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4] = worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4];
							worldRenderersToUpdate.Add(worldRenderers[(i6 * renderChunksTall + i5) * renderChunksWide + i4]);
							i2 += 3;
						}
					}
				}

				if (worldObj != null)
				{
					EntityLiving entityLiving7 = mc.renderViewEntity;
					if (entityLiving7 != null)
					{
						markRenderersForNewPosition(MathHelper.floor_double(entityLiving7.posX), MathHelper.floor_double(entityLiving7.posY), MathHelper.floor_double(entityLiving7.posZ));
						Array.Sort(sortedWorldRenderers, new EntitySorter(entityLiving7));
					}
				}

				renderEntitiesStartupCounter = 2;
			}
		}

		public virtual void renderEntities(Vec3D vec3D1, ICamera iCamera2, float f3)
		{
			if (renderEntitiesStartupCounter > 0)
			{
				--renderEntitiesStartupCounter;
			}
			else
			{
				Profiler.startSection("prepare");
				TileEntityRenderer.instance.cacheActiveRenderInfo(this.worldObj, this.renderEngine, this.mc.fontRenderer, this.mc.renderViewEntity, f3);
				RenderManager.instance.cacheActiveRenderInfo(this.worldObj, this.renderEngine, this.mc.fontRenderer, this.mc.renderViewEntity, this.mc.gameSettings, f3);
				TileEntityRenderer.instance.func_40742_a();
				this.countEntitiesTotal = 0;
				this.countEntitiesRendered = 0;
				this.countEntitiesHidden = 0;
				EntityLiving entityLiving4 = this.mc.renderViewEntity;
				RenderManager.renderPosX = entityLiving4.lastTickPosX + (entityLiving4.posX - entityLiving4.lastTickPosX) * (double)f3;
				RenderManager.renderPosY = entityLiving4.lastTickPosY + (entityLiving4.posY - entityLiving4.lastTickPosY) * (double)f3;
				RenderManager.renderPosZ = entityLiving4.lastTickPosZ + (entityLiving4.posZ - entityLiving4.lastTickPosZ) * (double)f3;
				TileEntityRenderer.staticPlayerX = entityLiving4.lastTickPosX + (entityLiving4.posX - entityLiving4.lastTickPosX) * (double)f3;
				TileEntityRenderer.staticPlayerY = entityLiving4.lastTickPosY + (entityLiving4.posY - entityLiving4.lastTickPosY) * (double)f3;
				TileEntityRenderer.staticPlayerZ = entityLiving4.lastTickPosZ + (entityLiving4.posZ - entityLiving4.lastTickPosZ) * (double)f3;
				this.mc.gameRenderer.enableLightmap((double)f3);
				Profiler.endStartSection("global");
				System.Collections.IList list5 = this.worldObj.LoadedEntityList;
				this.countEntitiesTotal = list5.Count;

				int i6;
				Entity entity7;
				for (i6 = 0; i6 < this.worldObj.weatherEffects.Count; ++i6)
				{
					entity7 = (Entity)this.worldObj.weatherEffects[i6];
					++this.countEntitiesRendered;
					if (entity7.isInRangeToRenderVec3D(vec3D1))
					{
						RenderManager.instance.renderEntity(entity7, f3);
					}
				}

				Profiler.endStartSection("entities");

				for (i6 = 0; i6 < list5.Count; ++i6)
				{
					entity7 = (Entity)list5[i6];
					if (entity7.isInRangeToRenderVec3D(vec3D1) && (entity7.ignoreFrustumCheck || iCamera2.isBoundingBoxInFrustum(entity7.boundingBox)) && (entity7 != this.mc.renderViewEntity || this.mc.gameSettings.thirdPersonView != 0 || this.mc.renderViewEntity.PlayerSleeping) && this.worldObj.blockExists(MathHelper.floor_double(entity7.posX), 0, MathHelper.floor_double(entity7.posZ)))
					{
						++this.countEntitiesRendered;
						RenderManager.instance.renderEntity(entity7, f3);
					}
				}

				Profiler.endStartSection("tileentities");
				GameLighting.EnableMeshLighting();

				for (i6 = 0; i6 < this.tileEntities.Count; ++i6)
				{
					TileEntityRenderer.instance.renderTileEntity((TileEntity)this.tileEntities[i6], f3);
				}

				this.mc.gameRenderer.disableLightmap((double)f3);
				Profiler.endSection();
			}
		}

		public virtual string DebugInfoRenders
		{
			get
			{
				return "C: " + this.renderersBeingRendered + "/" + this.renderersLoaded + ". F: " + this.renderersBeingClipped + ", O: " + this.renderersBeingOccluded + ", E: " + this.renderersSkippingRenderPass;
			}
		}

		public virtual string DebugInfoEntities
		{
			get
			{
				return "E: " + this.countEntitiesRendered + "/" + this.countEntitiesTotal + ". B: " + this.countEntitiesHidden + ", I: " + (this.countEntitiesTotal - this.countEntitiesHidden - this.countEntitiesRendered);
			}
		}

		private void markRenderersForNewPosition(int rendererX, int rendererY, int rendererZ)
		{
            rendererX -= 8;
			rendererY -= 8;
			rendererZ -= 8;
			this.minBlockX = int.MaxValue;
			this.minBlockY = int.MaxValue;
			this.minBlockZ = int.MaxValue;
			this.maxBlockX = int.MinValue;
			this.maxBlockY = int.MinValue;
			this.maxBlockZ = int.MinValue;
			int renderDistanceInBlocks = this.renderChunksWide * 16;
			int renderDistanceBlocksHalved = renderDistanceInBlocks / 2;

			for (int chunkIter = 0; chunkIter < this.renderChunksWide; chunkIter++)
			{
				int x = chunkIter * 16;
				int i8 = x + renderDistanceBlocksHalved - rendererX;
				if (i8 < 0)
				{
					i8 -= renderDistanceInBlocks - 1;
				}

				i8 /= renderDistanceInBlocks;
				x -= i8 * renderDistanceInBlocks;
				if (x < this.minBlockX)
				{
					this.minBlockX = x;
				}

				if (x > this.maxBlockX)
				{
					this.maxBlockX = x;
				}

				for (int i9 = 0; i9 < this.renderChunksDeep; ++i9)
				{
					int z = i9 * 16;
					int i11 = z + renderDistanceBlocksHalved - rendererZ;
					if (i11 < 0)
					{
						i11 -= renderDistanceInBlocks - 1;
					}

					i11 /= renderDistanceInBlocks;
					z -= i11 * renderDistanceInBlocks;
					if (z < this.minBlockZ)
					{
						this.minBlockZ = z;
					}

					if (z > this.maxBlockZ)
					{
						this.maxBlockZ = z;
					}

					for (int i12 = 0; i12 < this.renderChunksTall; ++i12)
					{
						int y = i12 * 16;
						if (y < this.minBlockY)
						{
							this.minBlockY = y;
						}

						if (y > this.maxBlockY)
						{
							this.maxBlockY = y;
						}

						WorldRenderer worldRenderer14 = this.worldRenderers[(i9 * this.renderChunksTall + i12) * this.renderChunksWide + chunkIter];
						bool z15 = worldRenderer14.needsUpdate;
						worldRenderer14.setPosition(x, y, z);
						if (!z15 && worldRenderer14.needsUpdate)
						{
							this.worldRenderersToUpdate.Add(worldRenderer14);
						}
					}
				}
			}
		}

		public virtual int sortAndRender(EntityLiving entityLiving1, int pass, double d3)
		{
			Profiler.startSection("sortchunks");

			for (int i5 = 0; i5 < 10; ++i5)
			{
				worldRenderersCheckIndex = (worldRenderersCheckIndex + 1) % worldRenderers.Length;
				WorldRenderer worldRenderer6 = worldRenderers[worldRenderersCheckIndex];
				if (worldRenderer6.needsUpdate && !worldRenderersToUpdate.Contains(worldRenderer6))
				{
					worldRenderersToUpdate.Add(worldRenderer6);
				}
			}

			if (mc.gameSettings.renderDistance != renderDistance)
			{
				loadRenderers();
			}

			if (pass == 0)
			{
				renderersLoaded = 0;
				dummyRenderInt = 0;
				renderersBeingClipped = 0;
				renderersBeingOccluded = 0;
				renderersBeingRendered = 0;
				renderersSkippingRenderPass = 0;
			}

			double d33 = entityLiving1.lastTickPosX + (entityLiving1.posX - entityLiving1.lastTickPosX) * d3;
			double d7 = entityLiving1.lastTickPosY + (entityLiving1.posY - entityLiving1.lastTickPosY) * d3;
			double d9 = entityLiving1.lastTickPosZ + (entityLiving1.posZ - entityLiving1.lastTickPosZ) * d3;
			double d11 = entityLiving1.posX - this.prevSortX;
			double d13 = entityLiving1.posY - this.prevSortY;
			double d15 = entityLiving1.posZ - this.prevSortZ;
			if (d11 * d11 + d13 * d13 + d15 * d15 > 16.0D)
			{
				this.prevSortX = entityLiving1.posX;
				this.prevSortY = entityLiving1.posY;
				this.prevSortZ = entityLiving1.posZ;
				this.markRenderersForNewPosition(MathHelper.floor_double(entityLiving1.posX), MathHelper.floor_double(entityLiving1.posY), MathHelper.floor_double(entityLiving1.posZ));
				Array.Sort(this.sortedWorldRenderers, new EntitySorter(entityLiving1));
			}

			GameLighting.DisableMeshLighting();
			sbyte b17 = 0;
			int i34;
			if (this.occlusionEnabled && this.mc.gameSettings.advancedOpengl && pass == 0)
			{
				sbyte b18 = 0;
				int i19 = 16;
				this.checkOcclusionQueryResult(b18, i19);

				for (int i20 = b18; i20 < i19; ++i20)
				{
					this.sortedWorldRenderers[i20].isVisible = true;
				}

				Profiler.endStartSection("render");
				i34 = b17 + this.renderSortedRenderers(b18, i19, pass, d3);

				do
				{
					Profiler.endStartSection("occ");
					int i35 = i19;
					i19 *= 2;
					if (i19 > this.sortedWorldRenderers.Length)
					{
						i19 = this.sortedWorldRenderers.Length;
					}

                    Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                    Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                    Minecraft.renderPipeline.SetState(RenderState.FogState, false);
                    GL.ColorMask(false, false, false, false);
					GL.DepthMask(false);
					Profiler.startSection("check");
					this.checkOcclusionQueryResult(i35, i19);
					Profiler.endSection();
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
					float f36 = 0.0F;
					float f21 = 0.0F;
					float f22 = 0.0F;

					for (int i23 = i35; i23 < i19; ++i23)
					{
						if (this.sortedWorldRenderers[i23].skipAllRenderPasses())
						{
							this.sortedWorldRenderers[i23].isInFrustum = false;
						}
						else
						{
							if (!this.sortedWorldRenderers[i23].isInFrustum)
							{
								this.sortedWorldRenderers[i23].isVisible = true;
							}

							if (this.sortedWorldRenderers[i23].isInFrustum && !this.sortedWorldRenderers[i23].isWaitingOnOcclusionQuery)
							{
								float f24 = MathHelper.sqrt_float(this.sortedWorldRenderers[i23].distanceToEntitySquared(entityLiving1));
								int i25 = (int)(1.0F + f24 / 128.0F);
								if (this.cloudOffsetX % i25 == i23 % i25)
								{
									WorldRenderer worldRenderer26 = this.sortedWorldRenderers[i23];
									float f27 = (float)((double)worldRenderer26.posXMinus - d33);
									float f28 = (float)((double)worldRenderer26.posYMinus - d7);
									float f29 = (float)((double)worldRenderer26.posZMinus - d9);
									float f30 = f27 - f36;
									float f31 = f28 - f21;
									float f32 = f29 - f22;
									if (f30 != 0.0F || f31 != 0.0F || f32 != 0.0F)
									{
                                        Minecraft.renderPipeline.ModelMatrix.Translate(f30, f31, f32);
										f36 += f30;
										f21 += f31;
										f22 += f32;
									}
                                    
									Profiler.startSection("bb");
                                    GL.BeginQuery(QueryTarget.SamplesPassed, sortedWorldRenderers[i23].glOcclusionQuery);
                                    sortedWorldRenderers[i23].TessellateOcclusionQueryAABB();
									GL.EndQuery(QueryTarget.SamplesPassed);
									Profiler.endSection();
									sortedWorldRenderers[i23].isWaitingOnOcclusionQuery = true;
								}
							}
						}
					}

                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
					GL.ColorMask(true, true, true, true);

					GL.DepthMask(true);
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                    Minecraft.renderPipeline.SetState(RenderState.FogState, true);
                    Profiler.endStartSection("render");
					i34 += this.renderSortedRenderers(i35, i19, pass, d3);
				} while (i19 < this.sortedWorldRenderers.Length);
			}
			else
			{
				Profiler.endStartSection("render");
				i34 = b17 + this.renderSortedRenderers(0, this.sortedWorldRenderers.Length, pass, d3);
			}

			Profiler.endSection();
			return i34;
		}

		private void checkOcclusionQueryResult(int i1, int i2)
		{
			for (int i3 = i1; i3 < i2; ++i3)
			{
				if (this.sortedWorldRenderers[i3].isWaitingOnOcclusionQuery)
				{
                    //Console.WriteLine($"World Renderer {i3} is waiting on occlusion query ({i1},{i2})");
                    GL.GetQueryObject(this.sortedWorldRenderers[i3].glOcclusionQuery, GetQueryObjectParam.QueryResultAvailable, this.occlusionResult);
					if (this.occlusionResult[0] != 0)
					{
						this.sortedWorldRenderers[i3].isWaitingOnOcclusionQuery = false;
						GL.GetQueryObject(this.sortedWorldRenderers[i3].glOcclusionQuery, GetQueryObjectParam.QueryResult, this.occlusionResult);
						this.sortedWorldRenderers[i3].isVisible = this.occlusionResult[0] != 0;
					}
				}
			}
		}

		private float worldRenderX;
		private float worldRenderY;
		private float worldRenderZ;

		private int renderSortedRenderers(int i1, int i2, int pass, double d4)
		{
			Profiler.startSection("addWorldRenderers");
            this.activeWorldRenderers.Clear();
			int i6 = 0;

			for (int i7 = i1; i7 < i2; ++i7)
			{
				if (pass == 0)
				{
					++this.renderersLoaded;
					if (this.sortedWorldRenderers[i7].skipRenderPass[pass])
					{
						++this.renderersSkippingRenderPass;
					}
					else if (!this.sortedWorldRenderers[i7].isInFrustum)
					{
						++this.renderersBeingClipped;
					}
					else if (this.occlusionEnabled && !this.sortedWorldRenderers[i7].isVisible)
					{
						++this.renderersBeingOccluded;
					}
					else
					{
						++this.renderersBeingRendered;
					}
				}

				if (!this.sortedWorldRenderers[i7].skipRenderPass[pass] && this.sortedWorldRenderers[i7].isInFrustum && (!this.occlusionEnabled || this.sortedWorldRenderers[i7].isVisible))
				{
                    this.activeWorldRenderers.Add(this.sortedWorldRenderers[i7]);
                }
			}

            EntityLiving entityLiving19 = this.mc.renderViewEntity;
			double x = entityLiving19.lastTickPosX + (entityLiving19.posX - entityLiving19.lastTickPosX) * d4;
			double y = entityLiving19.lastTickPosY + (entityLiving19.posY - entityLiving19.lastTickPosY) * d4;
			double z = entityLiving19.lastTickPosZ + (entityLiving19.posZ - entityLiving19.lastTickPosZ) * d4;

            int i14 = 0;

			worldRenderX = (float)x;
			worldRenderY = (float)y;
			worldRenderZ = (float)z;
			
			Profiler.endStartSection("positionWorldRenderers");
			
			for (int i = 0; i < this.activeWorldRenderers.Count; ++i)
			{
				WorldRenderer? renderer = (WorldRenderer?)this.activeWorldRenderers[i];

                renderer.SetRenderPos(x, y, z, renderer.posXMinus, renderer.posYMinus, renderer.posZMinus);
            }
			
			Profiler.endStartSection("tessellateWorldRenderers");
            RenderAllWorldRenderers(pass, d4);
			Profiler.endSection();
            return i6;
		}
        
		public virtual void RenderAllWorldRenderers(int pass, double d2)
		{
			meshAllocator.FrameUpdate();

            MatrixStack modelMatrix = Minecraft.renderPipeline.ModelMatrix;

            mc.gameRenderer.enableLightmap(d2);

            // Draw world VBO
			modelMatrix.PushMatrix();
			GL.Enable(EnableCap.CullFace);
            Tessellator.instance.DrawMeshAllocator(meshAllocator.WorldBuffer, meshAllocator, sortedWorldRenderers, pass);
            modelMatrix.PopMatrix();

            mc.gameRenderer.disableLightmap(d2);
        }

		public virtual void updateClouds()
		{
			++cloudOffsetX;
		}

		public virtual void renderSky(float f1)
		{
			if (this.mc.theWorld.worldProvider.worldType == 1)
			{
                Minecraft.renderPipeline.SetState(RenderState.FogState, false);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				GameLighting.DisableMeshLighting();
				GL.DepthMask(false);
				this.renderEngine.bindTexture(this.renderEngine.getTexture("/misc/tunnel.png"));
				Tessellator tessellator19 = Tessellator.instance;

				for (int i20 = 0; i20 < 6; ++i20)
				{
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
					if (i20 == 1)
					{
                        Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 1.0F, 0.0F, 0.0F);
					}

					if (i20 == 2)
					{
                        Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
					}

					if (i20 == 3)
					{
                        Minecraft.renderPipeline.ModelMatrix.Rotate(180.0F, 1.0F, 0.0F, 0.0F);
					}

					if (i20 == 4)
					{
                        Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 0.0F, 1.0F);
					}

					if (i20 == 5)
					{
                        Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 0.0F, 1.0F);
					}

					tessellator19.startDrawingQuads();
					tessellator19.ColorOpaque_I = 1579032;
					tessellator19.AddVertexWithUV(-100.0D, -100.0D, -100.0D, 0.0D, 0.0D);
					tessellator19.AddVertexWithUV(-100.0D, -100.0D, 100.0D, 0.0D, 16.0D);
					tessellator19.AddVertexWithUV(100.0D, -100.0D, 100.0D, 16.0D, 16.0D);
					tessellator19.AddVertexWithUV(100.0D, -100.0D, -100.0D, 16.0D, 0.0D);
					tessellator19.DrawImmediate();
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}

				GL.DepthMask(true);
                Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
            }
			else if (this.mc.theWorld.worldProvider.func_48217_e())
			{
                Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                Vec3D vec3D2 = this.worldObj.getSkyColor(this.mc.renderViewEntity, f1);
				float f3 = (float)vec3D2.xCoord;
				float f4 = (float)vec3D2.yCoord;
				float f5 = (float)vec3D2.zCoord;
				float f7;
				float f8;

				Minecraft.renderPipeline.SetColor(f3, f4, f5);
				Tessellator tessellator21 = Tessellator.instance;
				GL.DepthMask(false);
                Minecraft.renderPipeline.SetState(RenderState.FogState, true);
                Minecraft.renderPipeline.SetColor(f3, f4, f5);
                
				tessellator21.Draw(sky1VBO);

                Minecraft.renderPipeline.SetState(RenderState.FogState, false);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				GameLighting.DisableMeshLighting();
				float[] f22 = this.worldObj.worldProvider.calcSunriseSunsetColors(this.worldObj.getCelestialAngle(f1), f1);
				float f9;
				float f10;
				float f11;
				float f12;
				float f15;
				int i25;
				if (f22 != null)
				{
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                    Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, true);
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 1.0F, 0.0F, 0.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(MathHelper.sin(this.worldObj.getCelestialAngleRadians(f1)) < 0.0F ? 180.0F : 0.0F, 0.0F, 0.0F, 1.0F);
                    Minecraft.renderPipeline.ModelMatrix.Rotate(90.0F, 0.0F, 0.0F, 1.0F);
					f8 = f22[0];
					f9 = f22[1];
					f10 = f22[2];
					float f13;

					tessellator21.startDrawing(6);
					tessellator21.setColorRGBA_F(f8, f9, f10, f22[3]);
					tessellator21.AddVertex(0.0D, 100.0D, 0.0D);
					sbyte b24 = 16;
					tessellator21.setColorRGBA_F(f22[0], f22[1], f22[2], 0.0F);

					for (i25 = 0; i25 <= b24; ++i25)
					{
						f13 = (float)i25 * (float)Math.PI * 2.0F / (float)b24;
						float f14 = MathHelper.sin(f13);
						f15 = MathHelper.cos(f13);
						tessellator21.AddVertex((double)(f14 * 120.0F), (double)(f15 * 120.0F), (double)(-f15 * 40.0F * f22[3]));
					}

					tessellator21.DrawImmediate();
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                    Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, false);
                }

                Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				f7 = 1.0F - this.worldObj.getRainStrength(f1);
				f8 = 0.0F;
				f9 = 0.0F;
				f10 = 0.0F;
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, f7);
                Minecraft.renderPipeline.ModelMatrix.Translate(f8, f9, f10);
                Minecraft.renderPipeline.ModelMatrix.Rotate(-90.0F, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Rotate(this.worldObj.getCelestialAngle(f1) * 360.0F, 1.0F, 0.0F, 0.0F);
				f11 = 30.0F;
                GL.BindTexture(TextureTarget.Texture2D, this.renderEngine.getTexture("/terrain/sun.png"));
				tessellator21.startDrawingQuads();
				tessellator21.AddVertexWithUV((double)(-f11), 100.0D, (double)(-f11), 0.0D, 0.0D);
				tessellator21.AddVertexWithUV((double)f11, 100.0D, (double)(-f11), 1.0D, 0.0D);
				tessellator21.AddVertexWithUV((double)f11, 100.0D, (double)f11, 1.0D, 1.0D);
				tessellator21.AddVertexWithUV((double)(-f11), 100.0D, (double)f11, 0.0D, 1.0D);
				tessellator21.DrawImmediate();
				f11 = 20.0F;
				GL.BindTexture(TextureTarget.Texture2D, this.renderEngine.getTexture("/terrain/moon_phases.png"));
				i25 = this.worldObj.getMoonPhase(f1);
				int i26 = i25 % 4;
				int i27 = i25 / 4 % 2;
				f15 = (float)(i26 + 0) / 4.0F;
				float f16 = (float)(i27 + 0) / 2.0F;
				float f17 = (float)(i26 + 1) / 4.0F;
				float f18 = (float)(i27 + 1) / 2.0F;
				tessellator21.startDrawingQuads();
				tessellator21.AddVertexWithUV((double)(-f11), -100.0D, (double)f11, (double)f17, (double)f18);
				tessellator21.AddVertexWithUV((double)f11, -100.0D, (double)f11, (double)f15, (double)f18);
				tessellator21.AddVertexWithUV((double)f11, -100.0D, (double)(-f11), (double)f15, (double)f16);
				tessellator21.AddVertexWithUV((double)(-f11), -100.0D, (double)(-f11), (double)f17, (double)f16);
				tessellator21.DrawImmediate();
                Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                f12 = this.worldObj.getStarBrightness(f1) * f7;
				if (f12 > 0.0F)
				{
                    Minecraft.renderPipeline.SetColor(f12, f12, f12, f12);
					tessellator21.Draw(starVBO);
				}

                Minecraft.renderPipeline.SetColor(1.0F);
                GL.Disable(EnableCap.Blend);
                Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                Minecraft.renderPipeline.SetState(RenderState.FogState, true);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                Minecraft.renderPipeline.SetColor(0.0F, 0.0F, 0.0F);
				double d23 = this.mc.thePlayer.getPosition(f1).yCoord - this.worldObj.SeaLevel;
				if (d23 < 0.0D)
				{
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 12.0F, 0.0F);
                    tessellator21.Draw(sky2VBO);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
					f9 = 1.0F;
					f10 = -((float)(d23 + 65.0D));
					f11 = -f9;
					tessellator21.startDrawingQuads();
					tessellator21.setColorRGBA_I(0, 255);
					tessellator21.AddVertex((double)(-f9), (double)f10, (double)f9);
					tessellator21.AddVertex((double)f9, (double)f10, (double)f9);
					tessellator21.AddVertex((double)f9, (double)f11, (double)f9);
					tessellator21.AddVertex((double)(-f9), (double)f11, (double)f9);
					tessellator21.AddVertex((double)(-f9), (double)f11, (double)(-f9));
					tessellator21.AddVertex((double)f9, (double)f11, (double)(-f9));
					tessellator21.AddVertex((double)f9, (double)f10, (double)(-f9));
					tessellator21.AddVertex((double)(-f9), (double)f10, (double)(-f9));
					tessellator21.AddVertex((double)f9, (double)f11, (double)(-f9));
					tessellator21.AddVertex((double)f9, (double)f11, (double)f9);
					tessellator21.AddVertex((double)f9, (double)f10, (double)f9);
					tessellator21.AddVertex((double)f9, (double)f10, (double)(-f9));
					tessellator21.AddVertex((double)(-f9), (double)f10, (double)(-f9));
					tessellator21.AddVertex((double)(-f9), (double)f10, (double)f9);
					tessellator21.AddVertex((double)(-f9), (double)f11, (double)f9);
					tessellator21.AddVertex((double)(-f9), (double)f11, (double)(-f9));
					tessellator21.AddVertex((double)(-f9), (double)f11, (double)(-f9));
					tessellator21.AddVertex((double)(-f9), (double)f11, (double)f9);
					tessellator21.AddVertex((double)f9, (double)f11, (double)f9);
					tessellator21.AddVertex((double)f9, (double)f11, (double)(-f9));
					tessellator21.DrawImmediate();
				}

				if (this.worldObj.worldProvider.SkyColored)
				{
                    Minecraft.renderPipeline.SetColor(f3 * 0.2F + 0.04F, f4 * 0.2F + 0.04F, f5 * 0.6F + 0.1F);
				}
				else
				{
                    Minecraft.renderPipeline.SetColor(f3, f4, f5);
				}

                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, -((float)(d23 - 16.0D)), 0.0F);
                tessellator21.Draw(sky2VBO);
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
                Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                GL.DepthMask(true);
			}
		}

		public virtual void renderClouds(float f1)
		{
			if (this.mc.theWorld.worldProvider.func_48217_e())
			{
				if (this.mc.gameSettings.fancyGraphics)
				{
					this.renderCloudsFancy(f1);
				}
				else
				{
					GL.Disable(EnableCap.CullFace);
					float f2 = (float)(this.mc.renderViewEntity.lastTickPosY + (this.mc.renderViewEntity.posY - this.mc.renderViewEntity.lastTickPosY) * (double)f1);
					sbyte b3 = 32;
					int i4 = 256 / b3;
					Tessellator tessellator5 = Tessellator.instance;
					GL.BindTexture(TextureTarget.Texture2D, this.renderEngine.getTexture("/environment/clouds.png"));
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
					Vec3D vec3D6 = this.worldObj.drawClouds(f1);
					float f7 = (float)vec3D6.xCoord;
					float f8 = (float)vec3D6.yCoord;
					float f9 = (float)vec3D6.zCoord;
					float f10;

					f10 = 4.8828125E-4F;
					double d24 = (double)((float)this.cloudOffsetX + f1);
					double d13 = this.mc.renderViewEntity.prevPosX + (this.mc.renderViewEntity.posX - this.mc.renderViewEntity.prevPosX) * (double)f1 + d24 * (double)0.03F;
					double d15 = this.mc.renderViewEntity.prevPosZ + (this.mc.renderViewEntity.posZ - this.mc.renderViewEntity.prevPosZ) * (double)f1;
					int i17 = MathHelper.floor_double(d13 / 2048.0D);
					int i18 = MathHelper.floor_double(d15 / 2048.0D);
					d13 -= (double)(i17 * 2048);
					d15 -= (double)(i18 * 2048);
					float f19 = this.worldObj.worldProvider.CloudHeight - f2 + 0.33F;
					float f20 = (float)(d13 * (double)f10);
					float f21 = (float)(d15 * (double)f10);
					tessellator5.startDrawingQuads();
					tessellator5.setColorRGBA_F(f7, f8, f9, 0.8F);

					for (int i22 = -b3 * i4; i22 < b3 * i4; i22 += b3)
					{
						for (int i23 = -b3 * i4; i23 < b3 * i4; i23 += b3)
						{
							tessellator5.AddVertexWithUV((double)(i22 + 0), (double)f19, (double)(i23 + b3), (double)((float)(i22 + 0) * f10 + f20), (double)((float)(i23 + b3) * f10 + f21));
							tessellator5.AddVertexWithUV((double)(i22 + b3), (double)f19, (double)(i23 + b3), (double)((float)(i22 + b3) * f10 + f20), (double)((float)(i23 + b3) * f10 + f21));
							tessellator5.AddVertexWithUV((double)(i22 + b3), (double)f19, (double)(i23 + 0), (double)((float)(i22 + b3) * f10 + f20), (double)((float)(i23 + 0) * f10 + f21));
							tessellator5.AddVertexWithUV((double)(i22 + 0), (double)f19, (double)(i23 + 0), (double)((float)(i22 + 0) * f10 + f20), (double)((float)(i23 + 0) * f10 + f21));
						}
					}

					tessellator5.DrawImmediate();
                    Minecraft.renderPipeline.SetColor(1.0F);
                    GL.Disable(EnableCap.Blend);
					GL.Enable(EnableCap.CullFace);
				}
			}
		}

		public virtual bool func_27307_a(double d1, double d3, double d5, float f7)
		{
			return false;
		}

		public virtual void renderCloudsFancy(float f1)
		{
			GL.Disable(EnableCap.CullFace);
			float f2 = (float)(this.mc.renderViewEntity.lastTickPosY + (this.mc.renderViewEntity.posY - this.mc.renderViewEntity.lastTickPosY) * (double)f1);
			Tessellator tessellator3 = Tessellator.instance;
			float f4 = 12.0F;
			float f5 = 4.0F;
			double d6 = (double)((float)this.cloudOffsetX + f1);
			double d8 = (this.mc.renderViewEntity.prevPosX + (this.mc.renderViewEntity.posX - this.mc.renderViewEntity.prevPosX) * (double)f1 + d6 * (double)0.03F) / (double)f4;
			double d10 = (this.mc.renderViewEntity.prevPosZ + (this.mc.renderViewEntity.posZ - this.mc.renderViewEntity.prevPosZ) * (double)f1) / (double)f4 + (double)0.33F;
			float f12 = this.worldObj.worldProvider.CloudHeight - f2 + 0.33F;
			int i13 = MathHelper.floor_double(d8 / 2048.0D);
			int i14 = MathHelper.floor_double(d10 / 2048.0D);
			d8 -= (double)(i13 * 2048);
			d10 -= (double)(i14 * 2048);
			GL.BindTexture(TextureTarget.Texture2D, this.renderEngine.getTexture("/environment/clouds.png"));
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			Vec3D vec3D15 = this.worldObj.drawClouds(f1);
			float f16 = (float)vec3D15.xCoord;
			float f17 = (float)vec3D15.yCoord;
			float f18 = (float)vec3D15.zCoord;
			float f19;
			float f20;
			float f21;

			f19 = (float)(d8 * 0.0D);
			f20 = (float)(d10 * 0.0D);
			f21 = 0.00390625F;
			f19 = (float)MathHelper.floor_double(d8) * f21;
			f20 = (float)MathHelper.floor_double(d10) * f21;
			float f22 = (float)(d8 - (double)MathHelper.floor_double(d8));
			float f23 = (float)(d10 - (double)MathHelper.floor_double(d10));
			sbyte b24 = 8;
			sbyte b25 = 4;
			float f26 = 9.765625E-4F;
            Minecraft.renderPipeline.ModelMatrix.Scale(f4, 1.0F, f4);

			for (int i27 = 0; i27 < 2; ++i27)
			{
				if (i27 == 0)
				{
					GL.ColorMask(false, false, false, false);
				}
				else
				{
					GL.ColorMask(true, true, true, true);
				}

				for (int i28 = -b25 + 1; i28 <= b25; ++i28)
				{
					for (int i29 = -b25 + 1; i29 <= b25; ++i29)
					{
						tessellator3.startDrawingQuads();
						float f30 = (float)(i28 * b24);
						float f31 = (float)(i29 * b24);
						float f32 = f30 - f22;
						float f33 = f31 - f23;
						if (f12 > -f5 - 1.0F)
						{
							tessellator3.setColorRGBA_F(f16 * 0.7F, f17 * 0.7F, f18 * 0.7F, 0.8F);
							tessellator3.SetNormal(0.0F, -1.0F, 0.0F);
							tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + 0.0F), (double)(f33 + (float)b24), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
							tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + 0.0F), (double)(f33 + (float)b24), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
							tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + 0.0F), (double)(f33 + 0.0F), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
							tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + 0.0F), (double)(f33 + 0.0F), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
						}

						if (f12 <= f5 + 1.0F)
						{
							tessellator3.setColorRGBA_F(f16, f17, f18, 0.8F);
							tessellator3.SetNormal(0.0F, 1.0F, 0.0F);
							tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + f5 - f26), (double)(f33 + (float)b24), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
							tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + f5 - f26), (double)(f33 + (float)b24), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
							tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + f5 - f26), (double)(f33 + 0.0F), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
							tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + f5 - f26), (double)(f33 + 0.0F), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
						}

						tessellator3.setColorRGBA_F(f16 * 0.9F, f17 * 0.9F, f18 * 0.9F, 0.8F);
						int i34;
						if (i28 > -1)
						{
							tessellator3.SetNormal(-1.0F, 0.0F, 0.0F);

							for (i34 = 0; i34 < b24; ++i34)
							{
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 0.0F), (double)(f12 + 0.0F), (double)(f33 + (float)b24), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 0.0F), (double)(f12 + f5), (double)(f33 + (float)b24), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 0.0F), (double)(f12 + f5), (double)(f33 + 0.0F), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 0.0F), (double)(f12 + 0.0F), (double)(f33 + 0.0F), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
							}
						}

						if (i28 <= 1)
						{
							tessellator3.SetNormal(1.0F, 0.0F, 0.0F);

							for (i34 = 0; i34 < b24; ++i34)
							{
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 1.0F - f26), (double)(f12 + 0.0F), (double)(f33 + (float)b24), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 1.0F - f26), (double)(f12 + f5), (double)(f33 + (float)b24), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + (float)b24) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 1.0F - f26), (double)(f12 + f5), (double)(f33 + 0.0F), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)i34 + 1.0F - f26), (double)(f12 + 0.0F), (double)(f33 + 0.0F), (double)((f30 + (float)i34 + 0.5F) * f21 + f19), (double)((f31 + 0.0F) * f21 + f20));
							}
						}

						tessellator3.setColorRGBA_F(f16 * 0.8F, f17 * 0.8F, f18 * 0.8F, 0.8F);
						if (i29 > -1)
						{
							tessellator3.SetNormal(0.0F, 0.0F, -1.0F);

							for (i34 = 0; i34 < b24; ++i34)
							{
								tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + f5), (double)(f33 + (float)i34 + 0.0F), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + f5), (double)(f33 + (float)i34 + 0.0F), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + 0.0F), (double)(f33 + (float)i34 + 0.0F), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + 0.0F), (double)(f33 + (float)i34 + 0.0F), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
							}
						}

						if (i29 <= 1)
						{
							tessellator3.SetNormal(0.0F, 0.0F, 1.0F);

							for (i34 = 0; i34 < b24; ++i34)
							{
								tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + f5), (double)(f33 + (float)i34 + 1.0F - f26), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + f5), (double)(f33 + (float)i34 + 1.0F - f26), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + (float)b24), (double)(f12 + 0.0F), (double)(f33 + (float)i34 + 1.0F - f26), (double)((f30 + (float)b24) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
								tessellator3.AddVertexWithUV((double)(f32 + 0.0F), (double)(f12 + 0.0F), (double)(f33 + (float)i34 + 1.0F - f26), (double)((f30 + 0.0F) * f21 + f19), (double)((f31 + (float)i34 + 0.5F) * f21 + f20));
							}
						}

						tessellator3.DrawImmediate();
					}
				}
			}

            Minecraft.renderPipeline.SetColor(1.0F);
            GL.Disable(EnableCap.Blend);
			GL.Enable(EnableCap.CullFace);
		}

		public virtual bool updateRenderers(EntityLiving entityLiving1, bool z2)
		{
			bool z3 = false;
			if (z3)
			{
				this.worldRenderersToUpdate.Sort(new RenderSorter(entityLiving1));
				int i17 = this.worldRenderersToUpdate.Count - 1;
				int i18 = this.worldRenderersToUpdate.Count;

				for (int i19 = 0; i19 < i18; ++i19)
				{
					WorldRenderer worldRenderer20 = (WorldRenderer)this.worldRenderersToUpdate[i17 - i19];
					if (!z2)
					{
						if (worldRenderer20.distanceToEntitySquared(entityLiving1) > 256.0F)
						{
							if (worldRenderer20.isInFrustum)
							{
								if (i19 >= 30)
								{
									return false;
								}
							}
							else if (i19 >= 1)
							{
								return false;
							}
						}
					}
					else if (!worldRenderer20.isInFrustum)
					{
						continue;
					}

					worldRenderer20.updateRenderer();
					this.worldRenderersToUpdate.Remove(worldRenderer20);
					worldRenderer20.needsUpdate = false;
				}

				return this.worldRenderersToUpdate.Count == 0;
			}
			else
			{
				sbyte b4 = 2;
				RenderSorter renderSorter5 = new RenderSorter(entityLiving1);
				WorldRenderer[] worldRenderer6 = new WorldRenderer[b4];
				List<WorldRenderer> arrayList7 = null;
				int i8 = this.worldRenderersToUpdate.Count;
				int i9 = 0;

				int i10;
				WorldRenderer worldRenderer11;
				int i12;
				int i13;
				for (i10 = 0; i10 < i8; ++i10)
				{
					worldRenderer11 = (WorldRenderer)this.worldRenderersToUpdate[i10];
					if (!z2)
					{
						if (worldRenderer11.distanceToEntitySquared(entityLiving1) > 256.0F)
						{
							for (i12 = 0; i12 < b4 && (worldRenderer6[i12] == null || renderSorter5.doCompare(worldRenderer6[i12], worldRenderer11) <= 0); ++i12)
							{
							}

							--i12;
							if (i12 <= 0)
							{
								continue;
							}

							i13 = i12;

							while (true)
							{
								--i13;
								if (i13 == 0)
								{
									worldRenderer6[i12] = worldRenderer11;
									goto label169Continue;
								}

								worldRenderer6[i13 - 1] = worldRenderer6[i13];
							}
						}
					}
					else if (!worldRenderer11.isInFrustum)
					{
						continue;
					}

					if (arrayList7 == null)
					{
						arrayList7 = new();
					}

					++i9;
					arrayList7.Add(worldRenderer11);
					worldRenderersToUpdate[i10] = null;
					label169Continue:;
				}
				label169Break:

				if (arrayList7 != null)
				{
					if (arrayList7.Count > 1)
					{
						arrayList7.Sort(renderSorter5);
					}

					for (i10 = arrayList7.Count - 1; i10 >= 0; --i10)
					{
						worldRenderer11 = (WorldRenderer)arrayList7[i10];
						worldRenderer11.updateRenderer();
						worldRenderer11.needsUpdate = false;
					}
				}

				i10 = 0;

				int i21;
				for (i21 = b4 - 1; i21 >= 0; --i21)
				{
					WorldRenderer worldRenderer22 = worldRenderer6[i21];
					if (worldRenderer22 != null)
					{
						if (!worldRenderer22.isInFrustum && i21 != b4 - 1)
						{
							worldRenderer6[i21] = null;
							worldRenderer6[0] = null;
							break;
						}

						worldRenderer6[i21].updateRenderer();
						worldRenderer6[i21].needsUpdate = false;
						++i10;
					}
				}

				i21 = 0;
				i12 = 0;

				for (i13 = this.worldRenderersToUpdate.Count; i21 != i13; ++i21)
				{
					WorldRenderer worldRenderer14 = (WorldRenderer)this.worldRenderersToUpdate[i21];
					if (worldRenderer14 != null)
					{
						bool z15 = false;

						for (int i16 = 0; i16 < b4 && !z15; ++i16)
						{
							if (worldRenderer14 == worldRenderer6[i16])
							{
								z15 = true;
							}
						}

						if (!z15)
						{
							if (i12 != i21)
							{
								this.worldRenderersToUpdate[i12] = worldRenderer14;
							}

							++i12;
						}
					}
				}

				while (true)
				{
					--i21;
					if (i21 < i12)
					{
						return i8 == i9 + i10;
					}

					this.worldRenderersToUpdate.RemoveAt(i21);
				}
			}
		}
		
		public virtual void drawBlockBreaking(EntityPlayer entityPlayer1, MovingObjectPosition movingObjectPosition2, int i3, ItemStack itemStack4, float f5)
		{
			Tessellator tessellator6 = Tessellator.instance;
			GL.Enable(EnableCap.Blend);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, (MathHelper.sin((float)DateTimeHelper.CurrentUnixTimeMillis() / 100.0F) * 0.2F + 0.4F) * 0.5F);
            int i8;
			if (i3 == 0)
			{
				if (this.damagePartialTime > 0.0F)
				{
					GL.BlendFunc(BlendingFactor.DstColor, BlendingFactor.SrcColor);
					int i7 = this.renderEngine.getTexture("/terrain.png");
					GL.BindTexture(TextureTarget.Texture2D, i7);
                    Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 0.5F);
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
					i8 = this.worldObj.getBlockId(movingObjectPosition2.blockX, movingObjectPosition2.blockY, movingObjectPosition2.blockZ);
					Block block9 = i8 > 0 ? Block.blocksList[i8] : null;
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                    GL.PolygonOffset(-3.0F, -3.0F);
					GL.Enable(EnableCap.PolygonOffsetFill);
					double d10 = entityPlayer1.lastTickPosX + (entityPlayer1.posX - entityPlayer1.lastTickPosX) * (double)f5;
					double d12 = entityPlayer1.lastTickPosY + (entityPlayer1.posY - entityPlayer1.lastTickPosY) * (double)f5;
					double d14 = entityPlayer1.lastTickPosZ + (entityPlayer1.posZ - entityPlayer1.lastTickPosZ) * (double)f5;
					if (block9 == null)
					{
						block9 = Block.stone;
					}

                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
                    tessellator6.startDrawingQuads();
					tessellator6.setTranslation(-d10, -d12, -d14);
					tessellator6.disableColor();
					this.globalRenderBlocks.renderBlockUsingTexture(block9, movingObjectPosition2.blockX, movingObjectPosition2.blockY, movingObjectPosition2.blockZ, 240 + (int)(this.damagePartialTime * 10.0F));
					tessellator6.DrawImmediate();
					tessellator6.setTranslation(0.0D, 0.0D, 0.0D);
                    Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
                    GL.PolygonOffset(0.0F, 0.0F);
					GL.Disable(EnableCap.PolygonOffsetFill);
					Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, true);
					GL.DepthMask(true);
                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}
			}
			else if (itemStack4 != null)
			{
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				float f16 = MathHelper.sin((float)DateTimeHelper.CurrentUnixTimeMillis() / 100.0F) * 0.2F + 0.8F;
                Minecraft.renderPipeline.SetColor(f16, f16, f16, MathHelper.sin((float)DateTimeHelper.CurrentUnixTimeMillis() / 200.0F) * 0.2F + 0.5F);
				i8 = this.renderEngine.getTexture("/terrain.png");
				GL.BindTexture(TextureTarget.Texture2D, i8);
				int i17 = movingObjectPosition2.blockX;
				int i18 = movingObjectPosition2.blockY;
				int i11 = movingObjectPosition2.blockZ;
				if (movingObjectPosition2.sideHit == 0)
				{
					--i18;
				}

				if (movingObjectPosition2.sideHit == 1)
				{
					++i18;
				}

				if (movingObjectPosition2.sideHit == 2)
				{
					--i11;
				}

				if (movingObjectPosition2.sideHit == 3)
				{
					++i11;
				}

				if (movingObjectPosition2.sideHit == 4)
				{
					--i17;
				}

				if (movingObjectPosition2.sideHit == 5)
				{
					++i17;
				}
			}

			GL.Disable(EnableCap.Blend);
            Minecraft.renderPipeline.SetState(RenderState.AlphaTestState, false);
        }

		public virtual void drawSelectionBox(EntityPlayer entityPlayer1, MovingObjectPosition movingObjectPosition2, int i3, ItemStack itemStack4, float f5)
		{
			if (i3 == 0 && movingObjectPosition2.typeOfHit == EnumMovingObjectType.TILE)
			{
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                Minecraft.renderPipeline.SetColor(0.0F, 0.0F, 0.0F, 0.4F);
				GL.LineWidth(2.0F);
                Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
                GL.DepthMask(false);
				float f6 = 0.002F;
				int i7 = this.worldObj.getBlockId(movingObjectPosition2.blockX, movingObjectPosition2.blockY, movingObjectPosition2.blockZ);
				if (i7 > 0)
				{
					Block.blocksList[i7].setBlockBoundsBasedOnState(this.worldObj, movingObjectPosition2.blockX, movingObjectPosition2.blockY, movingObjectPosition2.blockZ);
					double d8 = entityPlayer1.lastTickPosX + (entityPlayer1.posX - entityPlayer1.lastTickPosX) * (double)f5;
					double d10 = entityPlayer1.lastTickPosY + (entityPlayer1.posY - entityPlayer1.lastTickPosY) * (double)f5;
					double d12 = entityPlayer1.lastTickPosZ + (entityPlayer1.posZ - entityPlayer1.lastTickPosZ) * (double)f5;
					this.drawOutlinedBoundingBox(Block.blocksList[i7].getSelectedBoundingBoxFromPool(this.worldObj, movingObjectPosition2.blockX, movingObjectPosition2.blockY, movingObjectPosition2.blockZ).expand((double)f6, (double)f6, (double)f6).getOffsetBoundingBox(-d8, -d10, -d12));
				}

				GL.DepthMask(true);
                Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                GL.Disable(EnableCap.Blend);
			}

		}

		private void drawOutlinedBoundingBox(AxisAlignedBB axisAlignedBB1)
		{
			Tessellator tessellator2 = Tessellator.instance;
			tessellator2.startDrawing(3);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.minY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.minY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.minY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.minY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.minY, axisAlignedBB1.minZ);
			tessellator2.DrawImmediate();
			tessellator2.startDrawing(3);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.maxY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.maxY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.maxY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.maxY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.maxY, axisAlignedBB1.minZ);
			tessellator2.DrawImmediate();
			tessellator2.startDrawing(1);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.minY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.maxY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.minY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.maxY, axisAlignedBB1.minZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.minY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.maxX, axisAlignedBB1.maxY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.minY, axisAlignedBB1.maxZ);
			tessellator2.AddVertex(axisAlignedBB1.minX, axisAlignedBB1.maxY, axisAlignedBB1.maxZ);
			tessellator2.DrawImmediate();
		}

		public virtual void markBlocksForUpdate(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			int i7 = MathHelper.bucketInt(i1, 16);
			int i8 = MathHelper.bucketInt(i2, 16);
			int i9 = MathHelper.bucketInt(i3, 16);
			int i10 = MathHelper.bucketInt(i4, 16);
			int i11 = MathHelper.bucketInt(i5, 16);
			int i12 = MathHelper.bucketInt(i6, 16);

			for (int i13 = i7; i13 <= i10; ++i13)
			{
				int i14 = i13 % this.renderChunksWide;
				if (i14 < 0)
				{
					i14 += this.renderChunksWide;
				}

				for (int i15 = i8; i15 <= i11; ++i15)
				{
					int i16 = i15 % this.renderChunksTall;
					if (i16 < 0)
					{
						i16 += this.renderChunksTall;
					}

					for (int i17 = i9; i17 <= i12; ++i17)
					{
						int i18 = i17 % this.renderChunksDeep;
						if (i18 < 0)
						{
							i18 += this.renderChunksDeep;
						}

						int i19 = (i18 * this.renderChunksTall + i16) * this.renderChunksWide + i14;
						WorldRenderer worldRenderer20 = this.worldRenderers[i19];
						if (!worldRenderer20.needsUpdate)
						{
							this.worldRenderersToUpdate.Add(worldRenderer20);
							worldRenderer20.markDirty();
						}
					}
				}
			}

		}

		public virtual void markBlockNeedsUpdate(int i1, int i2, int i3)
		{
			this.markBlocksForUpdate(i1 - 1, i2 - 1, i3 - 1, i1 + 1, i2 + 1, i3 + 1);
		}

		public virtual void markBlockNeedsUpdate2(int i1, int i2, int i3)
		{
			this.markBlocksForUpdate(i1 - 1, i2 - 1, i3 - 1, i1 + 1, i2 + 1, i3 + 1);
		}

		public virtual void markBlockRangeNeedsUpdate(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			this.markBlocksForUpdate(i1 - 1, i2 - 1, i3 - 1, i4 + 1, i5 + 1, i6 + 1);
		}

		public virtual void clipRenderersByFrustum(ICamera iCamera1, float f2)
		{
			for (int i3 = 0; i3 < this.worldRenderers.Length; ++i3)
			{
				if (!this.worldRenderers[i3].skipAllRenderPasses() && (!this.worldRenderers[i3].isInFrustum || (i3 + this.frustumCheckOffset & 15) == 0))
				{
					this.worldRenderers[i3].updateInFrustum(iCamera1);
				}
			}

			++this.frustumCheckOffset;
		}

		public virtual void playRecord(string string1, int i2, int i3, int i4)
		{
			if (!string.ReferenceEquals(string1, null))
			{
				this.mc.ingameGUI.RecordPlayingMessage = "C418 - " + string1;
			}

			this.mc.sndManager.playStreaming(string1, (float)i2, (float)i3, (float)i4, 1.0F, 1.0F);
		}

		public virtual void playSound(string string1, double d2, double d4, double d6, float f8, float f9)
		{
			float f10 = 16.0F;
			if (f8 > 1.0F)
			{
				f10 *= f8;
			}

			if (this.mc.renderViewEntity.getDistanceSq(d2, d4, d6) < (double)(f10 * f10))
			{
				this.mc.sndManager.playSound(string1, (float)d2, (float)d4, (float)d6, f8, f9);
			}

		}

		public virtual void spawnParticle(string string1, double d2, double d4, double d6, double d8, double d10, double d12)
		{
			this.func_40193_b(string1, d2, d4, d6, d8, d10, d12);
		}

		public virtual ParticleEffect func_40193_b(string string1, double d2, double d4, double d6, double d8, double d10, double d12)
		{
			if (this.mc != null && this.mc.renderViewEntity != null && this.mc.effectRenderer != null)
			{
				int i14 = this.mc.gameSettings.particleSetting;
				if (i14 == 1 && this.worldObj.rand.Next(3) == 0)
				{
					i14 = 2;
				}

				double d15 = this.mc.renderViewEntity.posX - d2;
				double d17 = this.mc.renderViewEntity.posY - d4;
				double d19 = this.mc.renderViewEntity.posZ - d6;
				object object21 = null;
				if (string1.Equals("hugeexplosion"))
				{
					this.mc.effectRenderer.addEffect((ParticleEffect)(object21 = new EntityHugeExplodeFX(this.worldObj, d2, d4, d6, d8, d10, d12)));
				}
				else if (string1.Equals("largeexplode"))
				{
					this.mc.effectRenderer.addEffect((ParticleEffect)(object21 = new EntityLargeExplodeFX(this.renderEngine, this.worldObj, d2, d4, d6, d8, d10, d12)));
				}

				if (object21 != null)
				{
					return (ParticleEffect)object21;
				}
				else
				{
					double d22 = 16.0D;
					if (d15 * d15 + d17 * d17 + d19 * d19 > d22 * d22)
					{
						return null;
					}
					else if (i14 > 1)
					{
						return null;
					}
					else
					{
						if (string1.Equals("bubble"))
						{
							object21 = new EntityBubbleFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("suspended"))
						{
							object21 = new EntitySuspendFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("depthsuspend"))
						{
							object21 = new EntityAuraFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("townaura"))
						{
							object21 = new EntityAuraFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("crit"))
						{
							object21 = new EntityCritFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("magicCrit"))
						{
							object21 = new EntityCritFX(this.worldObj, d2, d4, d6, d8, d10, d12);
							((ParticleEffect)object21).SetColor(((ParticleEffect)object21).GetParticleColorR() * 0.3F, ((ParticleEffect)object21).GetParticleColorG() * 0.8F, ((ParticleEffect)object21).GetParticleColorB());
							((ParticleEffect)object21).ParticleTextureIndex = ((ParticleEffect)object21).ParticleTextureIndex + 1;
						}
						else if (string1.Equals("smoke"))
						{
							object21 = new EntitySmokeFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("mobSpell"))
						{
							object21 = new EntitySpellParticleFX(this.worldObj, d2, d4, d6, 0.0D, 0.0D, 0.0D);
							((ParticleEffect)object21).SetColor((float)d8, (float)d10, (float)d12);
						}
						else if (string1.Equals("spell"))
						{
							object21 = new EntitySpellParticleFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("instantSpell"))
						{
							object21 = new EntitySpellParticleFX(this.worldObj, d2, d4, d6, d8, d10, d12);
							((EntitySpellParticleFX)object21).func_40110_b(144);
						}
						else if (string1.Equals("note"))
						{
							object21 = new EntityNoteFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("portal"))
						{
							object21 = new EntityPortalFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("enchantmenttable"))
						{
							object21 = new EntityEnchantmentTableParticleFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("explode"))
						{
							object21 = new EntityExplodeFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("flame"))
						{
							object21 = new EntityFlameFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("lava"))
						{
							object21 = new EntityLavaFX(this.worldObj, d2, d4, d6);
						}
						else if (string1.Equals("footstep"))
						{
							object21 = new EntityFootStepFX(this.renderEngine, this.worldObj, d2, d4, d6);
						}
						else if (string1.Equals("splash"))
						{
							object21 = new EntitySplashFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("largesmoke"))
						{
							object21 = new EntitySmokeFX(this.worldObj, d2, d4, d6, d8, d10, d12, 2.5F);
						}
						else if (string1.Equals("cloud"))
						{
							object21 = new EntityCloudFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("reddust"))
						{
							object21 = new EntityReddustFX(this.worldObj, d2, d4, d6, (float)d8, (float)d10, (float)d12);
						}
						else if (string1.Equals("snowballpoof"))
						{
							object21 = new EntityBreakingFX(this.worldObj, d2, d4, d6, Item.snowball);
						}
						else if (string1.Equals("dripWater"))
						{
							object21 = new EntityDropParticleFX(this.worldObj, d2, d4, d6, Material.water);
						}
						else if (string1.Equals("dripLava"))
						{
							object21 = new EntityDropParticleFX(this.worldObj, d2, d4, d6, Material.lava);
						}
						else if (string1.Equals("snowshovel"))
						{
							object21 = new EntitySnowShovelFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else if (string1.Equals("slime"))
						{
							object21 = new EntityBreakingFX(this.worldObj, d2, d4, d6, Item.slimeBall);
						}
						else if (string1.Equals("heart"))
						{
							object21 = new EntityHeartFX(this.worldObj, d2, d4, d6, d8, d10, d12);
						}
						else
						{
							int i24;
							if (string1.StartsWith("iconcrack_", StringComparison.Ordinal))
							{
								i24 = int.Parse(string1.Substring(string1.IndexOf("_", StringComparison.Ordinal) + 1));
								object21 = new EntityBreakingFX(this.worldObj, d2, d4, d6, d8, d10, d12, Item.itemsList[i24]);
							}
							else if (string1.StartsWith("tilecrack_", StringComparison.Ordinal))
							{
								i24 = int.Parse(string1.Substring(string1.IndexOf("_", StringComparison.Ordinal) + 1));
								object21 = new EntityDiggingFX(this.worldObj, d2, d4, d6, d8, d10, d12, Block.blocksList[i24], 0, 0);
							}
						}

						if (object21 != null)
						{
							this.mc.effectRenderer.addEffect((ParticleEffect)object21);
						}

						return (ParticleEffect)object21;
					}
				}
			}
			else
			{
				return null;
			}
		}

		public virtual void obtainEntitySkin(Entity entity1)
		{
			entity1.updateCloak();
			if (!string.ReferenceEquals(entity1.skinUrl, null))
			{
				this.renderEngine.obtainImageData(entity1.skinUrl, new ImageBufferDownload());
			}

			if (!string.ReferenceEquals(entity1.cloakUrl, null))
			{
				this.renderEngine.obtainImageData(entity1.cloakUrl, new ImageBufferDownload());
			}

		}

		public virtual void releaseEntitySkin(Entity entity1)
		{
			if (!string.ReferenceEquals(entity1.skinUrl, null))
			{
				this.renderEngine.releaseImageData(entity1.skinUrl);
			}

			if (!string.ReferenceEquals(entity1.cloakUrl, null))
			{
				this.renderEngine.releaseImageData(entity1.cloakUrl);
			}

		}

		public virtual void doNothingWithTileEntity(int i1, int i2, int i3, TileEntity tileEntity4)
		{
		}

		public virtual void playAuxSFX(EntityPlayer entityPlayer1, int i2, int i3, int i4, int i5, int i6)
		{
			RandomExtended random7 = this.worldObj.rand;
			int i8;
			double d10;
			double d12;
			string string14;
			int i15;
			double d21;
			double d23;
			double d25;
			double d27;
			double d29;
			double d33;
			switch (i2)
			{
			case 1000:
				this.worldObj.playSoundEffect((double)i3, (double)i4, (double)i5, "random.click", 1.0F, 1.0F);
				break;
			case 1001:
				this.worldObj.playSoundEffect((double)i3, (double)i4, (double)i5, "random.click", 1.0F, 1.2F);
				break;
			case 1002:
				this.worldObj.playSoundEffect((double)i3, (double)i4, (double)i5, "random.bow", 1.0F, 1.2F);
				break;
			case 1003:
				if (portinghelpers.MathHelper.NextDouble < 0.5D)
				{
					this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "random.door_open", 1.0F, this.worldObj.rand.NextSingle() * 0.1F + 0.9F);
				}
				else
				{
					this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "random.door_close", 1.0F, this.worldObj.rand.NextSingle() * 0.1F + 0.9F);
				}
				break;
			case 1004:
				this.worldObj.playSoundEffect((double)((float)i3 + 0.5F), (double)((float)i4 + 0.5F), (double)((float)i5 + 0.5F), "random.fizz", 0.5F, 2.6F + (random7.NextSingle() - random7.NextSingle()) * 0.8F);
				break;
			case 1005:
				if (Item.itemsList[i6] is ItemRecord)
				{
					this.worldObj.playRecord(((ItemRecord)Item.itemsList[i6]).recordName, i3, i4, i5);
				}
				else
				{
					this.worldObj.playRecord((string)null, i3, i4, i5);
				}
				break;
			case 1007:
				this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "mob.ghast.charge", 10.0F, (random7.NextSingle() - random7.NextSingle()) * 0.2F + 1.0F);
				break;
			case 1008:
				this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "mob.ghast.fireball", 10.0F, (random7.NextSingle() - random7.NextSingle()) * 0.2F + 1.0F);
				break;
			case 1010:
				this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "mob.zombie.wood", 2.0F, (random7.NextSingle() - random7.NextSingle()) * 0.2F + 1.0F);
				break;
			case 1011:
				this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "mob.zombie.metal", 2.0F, (random7.NextSingle() - random7.NextSingle()) * 0.2F + 1.0F);
				break;
			case 1012:
				this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "mob.zombie.woodbreak", 2.0F, (random7.NextSingle() - random7.NextSingle()) * 0.2F + 1.0F);
				break;
			case 2000:
				i8 = i6 % 3 - 1;
				int i35 = i6 / 3 % 3 - 1;
				d10 = (double)i3 + (double)i8 * 0.6D + 0.5D;
				d12 = (double)i4 + 0.5D;
				double d36 = (double)i5 + (double)i35 * 0.6D + 0.5D;

				for (int i38 = 0; i38 < 10; ++i38)
				{
					double d39 = random7.NextDouble() * 0.2D + 0.01D;
					double d40 = d10 + (double)i8 * 0.01D + (random7.NextDouble() - 0.5D) * (double)i35 * 0.5D;
					d21 = d12 + (random7.NextDouble() - 0.5D) * 0.5D;
					d23 = d36 + (double)i35 * 0.01D + (random7.NextDouble() - 0.5D) * (double)i8 * 0.5D;
					d25 = (double)i8 * d39 + random7.NextGaussian() * 0.01D;
					d27 = -0.03D + random7.NextGaussian() * 0.01D;
					d29 = (double)i35 * d39 + random7.NextGaussian() * 0.01D;
					this.spawnParticle("smoke", d40, d21, d23, d25, d27, d29);
				}

				return;
			case 2001:
				i8 = i6 & 4095;
				if (i8 > 0)
				{
					Block block34 = Block.blocksList[i8];
					this.mc.sndManager.playSound(block34.stepSound.BreakSound, (float)i3 + 0.5F, (float)i4 + 0.5F, (float)i5 + 0.5F, (block34.stepSound.Volume + 1.0F) / 2.0F, block34.stepSound.Pitch * 0.8F);
				}

				this.mc.effectRenderer.addBlockDestroyEffects(i3, i4, i5, i6 & 4095, i6 >> 12 & 255);
				break;
			case 2002:
				d33 = (double)i3;
				d10 = (double)i4;
				d12 = (double)i5;
				string14 = "iconcrack_" + Item.potion.shiftedIndex;

				for (i15 = 0; i15 < 8; ++i15)
				{
					this.spawnParticle(string14, d33, d10, d12, random7.NextGaussian() * 0.15D, random7.NextDouble() * 0.2D, random7.NextGaussian() * 0.15D);
				}

				i15 = Item.potion.getColorFromDamage(i6, 0);
				float f16 = (float)(i15 >> 16 & 255) / 255.0F;
				float f17 = (float)(i15 >> 8 & 255) / 255.0F;
				float f18 = (float)(i15 >> 0 & 255) / 255.0F;
				string string19 = "spell";
				if (Item.potion.isEffectInstant(i6))
				{
					string19 = "instantSpell";
				}

				for (int i20 = 0; i20 < 100; ++i20)
				{
					d21 = random7.NextDouble() * 4.0D;
					d23 = random7.NextDouble() * Math.PI * 2.0D;
					d25 = Math.Cos(d23) * d21;
					d27 = 0.01D + random7.NextDouble() * 0.5D;
					d29 = Math.Sin(d23) * d21;
					ParticleEffect entityFX31 = this.func_40193_b(string19, d33 + d25 * 0.1D, d10 + 0.3D, d12 + d29 * 0.1D, d25, d27, d29);
					if (entityFX31 != null)
					{
						float f32 = 0.75F + random7.NextSingle() * 0.25F;
						entityFX31.SetColor(f16 * f32, f17 * f32, f18 * f32);
						entityFX31.MultiplyVelocity((float)d21);
					}
				}

				this.worldObj.playSoundEffect((double)i3 + 0.5D, (double)i4 + 0.5D, (double)i5 + 0.5D, "random.glass", 1.0F, this.worldObj.rand.NextSingle() * 0.1F + 0.9F);
				break;
			case 2003:
				d33 = (double)i3 + 0.5D;
				d10 = (double)i4;
				d12 = (double)i5 + 0.5D;
				string14 = "iconcrack_" + Item.eyeOfEnder.shiftedIndex;

				for (i15 = 0; i15 < 8; ++i15)
				{
					this.spawnParticle(string14, d33, d10, d12, random7.NextGaussian() * 0.15D, random7.NextDouble() * 0.2D, random7.NextGaussian() * 0.15D);
				}

				for (double d37 = 0.0D; d37 < Math.PI * 2D; d37 += 0.15707963267948966D)
				{
					this.spawnParticle("portal", d33 + Math.Cos(d37) * 5.0D, d10 - 0.4D, d12 + Math.Sin(d37) * 5.0D, Math.Cos(d37) * -5.0D, 0.0D, Math.Sin(d37) * -5.0D);
					this.spawnParticle("portal", d33 + Math.Cos(d37) * 5.0D, d10 - 0.4D, d12 + Math.Sin(d37) * 5.0D, Math.Cos(d37) * -7.0D, 0.0D, Math.Sin(d37) * -7.0D);
				}

				return;
			case 2004:
				for (i8 = 0; i8 < 20; ++i8)
				{
					double d9 = (double)i3 + 0.5D + ((double)this.worldObj.rand.NextSingle() - 0.5D) * 2.0D;
					double d11 = (double)i4 + 0.5D + ((double)this.worldObj.rand.NextSingle() - 0.5D) * 2.0D;
					double d13 = (double)i5 + 0.5D + ((double)this.worldObj.rand.NextSingle() - 0.5D) * 2.0D;
					this.worldObj.spawnParticle("smoke", d9, d11, d13, 0.0D, 0.0D, 0.0D);
					this.worldObj.spawnParticle("flame", d9, d11, d13, 0.0D, 0.0D, 0.0D);
				}
			break;
			}

		}
	}

}