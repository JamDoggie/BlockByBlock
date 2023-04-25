using BlockByBlock.helpers;
using BlockByBlock.net.minecraft.client.entity.render;
using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render;
using net.minecraft.client.world.render;
using OpenTK.Graphics.OpenGL;
using System.Collections;
using System.Collections.Generic;

namespace net.minecraft.src
{

    public class WorldRenderer
    {
        public World worldObj;
        private static Tessellator tessellator = Tessellator.instance;
        public static int chunksUpdated = 0;
        public int posX { get; set; }
        public int posY { get; set; }
        public int posZ { get; set; }
        public int posXMinus { get; set; }
        public int posYMinus { get; set; }
        public int posZMinus { get; set; }
        public int posXClip { get; set; }
        public int posYClip { get; set; }
        public int posZClip { get; set; }

        public int X2 = 0;
        public int Y2 = 0;
        public int Z2 = 0;
        public double X1 = 0;
        public double Y1 = 0;
        public double Z1 = 0;

        public bool isInFrustum { get; set; } = false;
        public bool[] skipRenderPass = new bool[2];
        public int posXPlus;
        public int posYPlus;
        public int posZPlus;
        public bool needsUpdate;
        public AxisAlignedBB rendererBoundingBox;
        public int chunkIndex;
        public bool isVisible = true;
        public bool isWaitingOnOcclusionQuery;
        public int glOcclusionQuery { get; set; }
        public bool isChunkLit;
        private bool isInitialized = false;
        public IList tileEntityRenderers = new ArrayList();
        private IList tileEntities;
        private int bytesDrawn;
        private ChunkMeshAllocator meshAllocator;
        
        private VertexBuffer? AABBVBO = null;
        
        public int?[] meshDataAllocations = { null, null };

        public WorldRenderer(World world1, IList list2, int x, int y, int z, ChunkMeshAllocator meshAllocator)
        {
            worldObj = world1;
            tileEntities = list2;
            posX = -999;
            setPosition(x, y, z);
            needsUpdate = false;
            this.meshAllocator = meshAllocator;
        }

        ~WorldRenderer()
        {
            for (int i = 0; i < meshDataAllocations.Length; i++)
            {
                if (meshDataAllocations[i] != null)
                {
                    meshAllocator.FreeData(meshDataAllocations[i].Value);
                }
            }
        }

        public virtual void setPosition(int x, int y, int z)
        {
            if (x != posX || y != posY || z != posZ)
            {
                setDontDraw();
                posX = x;
                posY = y;
                posZ = z;
                posXPlus = x + 8;
                posYPlus = y + 8;
                posZPlus = z + 8;
                posXClip = x & 1023;
                posYClip = y;
                posZClip = z & 1023;
                posXMinus = x - posXClip;
                posYMinus = y - posYClip;
                posZMinus = z - posZClip;
                float f4 = 6.0F;
                rendererBoundingBox = AxisAlignedBB.getBoundingBox((double)((float)x - f4), (double)((float)y - f4), (double)((float)z - f4), (double)((float)(x + 16) + f4), (double)((float)(y + 16) + f4), (double)((float)(z + 16) + f4));

                AABBVBO = Renderer.BuildAABBVBO(AxisAlignedBB.getBoundingBoxFromPool((double)((float)posXClip - f4), (double)((float)posYClip - f4), (double)((float)posZClip - f4), (double)((float)(posXClip + 16) + f4), (double)((float)(posYClip + 16) + f4), (double)((float)(posZClip + 16) + f4)));

                markDirty();
            }
        }

        public virtual void SetRenderPos(double x, double y, double z, int x2, int y2, int z2)
        {
            X1 = x;
            Y1 = y;
            Z1 = z;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
        }

        internal void setupGLTranslation()
        {
            //stack.Translate((float)this.posXClip, (float)this.posYClip, (float)this.posZClip);
            tessellator.setTranslation(posXClip, posYClip, posZClip);
        }

        public virtual void updateRenderer()
        {
            Profiler.startSection("updateRenderer");
            if (needsUpdate)
            {
                needsUpdate = false;
                int i1 = posX;
                int i2 = posY;
                int i3 = posZ;
                int i4 = posX + 16;
                int i5 = posY + 16;
                int i6 = posZ + 16;

                for (int i7 = 0; i7 < 2; ++i7)
                {
                    skipRenderPass[i7] = true;
                }

                Chunk.isLit = false;
                HashSet<object> hashSet21 = new HashSet<object>();
                hashSet21.AddAll(tileEntityRenderers);
                tileEntityRenderers.Clear();
                sbyte b8 = 1;
                ChunkCache chunkCache9 = new ChunkCache(worldObj, i1 - b8, i2 - b8, i3 - b8, i4 + b8, i5 + b8, i6 + b8);
                if (!chunkCache9.getChunksEmpty_IDK())
                {
                    ++chunksUpdated;
                    RenderBlocks renderBlocks10 = new(chunkCache9);

                    for (int currentPass = 0; currentPass < 2; ++currentPass)
                    {
                        bool z12 = false;
                        bool rendererContainsBlocks = false;
                        bool blockFound = false;

                        for (int i15 = i2; i15 < i5; ++i15)
                        {
                            for (int i16 = i3; i16 < i6; ++i16)
                            {
                                for (int i17 = i1; i17 < i4; ++i17)
                                {
                                    int i18 = chunkCache9.getBlockId(i17, i15, i16);
                                    if (i18 > 0)
                                    {
                                        if (!blockFound)
                                        {
                                            blockFound = true;

                                            tessellator.StartBuildingVBO(7);

                                            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                                            
                                            float f19 = 1F;
                                            setupGLTranslation();
                                            
                                            tessellator.addTranslation((double)(-posX), (double)(-posY), (double)(-posZ));
                                        }

                                        if (currentPass == 0 && Block.blocksList[i18].hasTileEntity())
                                        {
                                            TileEntity tileEntity23 = chunkCache9.getBlockTileEntity(i17, i15, i16);
                                            if (TileEntityRenderer.instance.hasSpecialRenderer(tileEntity23))
                                            {
                                                tileEntityRenderers.Add(tileEntity23);
                                            }
                                        }

                                        Block block24 = Block.blocksList[i18];
                                        int renderPass = block24.RenderBlockPass;
                                        if (renderPass != currentPass)
                                        {
                                            z12 = true;
                                        }
                                        else if (renderPass == currentPass)
                                        {
                                            rendererContainsBlocks |= renderBlocks10.renderBlockByRenderType(block24, i17, i15, i16);
                                        }
                                    }
                                }
                            }
                        }

                        if (blockFound)
                        {
                            Minecraft.renderPipeline.ModelMatrix.PopMatrix();

                            byte[] meshData = tessellator.BuildCurrentVertexBuffer();
                            Profiler.startSection("allocatemeshdata");

                            if (meshDataAllocations[currentPass] != null)
                                meshAllocator.FreeData(meshDataAllocations[currentPass]!.Value);

                            if (meshData.Length > 0)
                            {
                                meshDataAllocations[currentPass] = meshAllocator.AllocateData(meshData, this, currentPass);
                            }
                            
                            Profiler.endSection();
                            
                            tessellator.setTranslation(0.0D, 0.0D, 0.0D);
                        }
                        else
                        {
                            rendererContainsBlocks = false;
                        }

                        if (rendererContainsBlocks)
                        {
                            skipRenderPass[currentPass] = false;
                        }

                        if (!z12)
                        {
                            break;
                        }
                    }
                }

                HashSet<object> hashSet22 = new HashSet<object>();
                hashSet22.AddAll(this.tileEntityRenderers);
                hashSet22.RemoveAll(hashSet21);
                this.tileEntities.AddRange(hashSet22);
                hashSet21.RemoveAll(this.tileEntityRenderers);
                this.tileEntities.RemoveAll(hashSet21);
                this.isChunkLit = Chunk.isLit;
                this.isInitialized = true;
            }
            Profiler.endSection();
        }

        public virtual float distanceToEntitySquared(Entity entity1)
        {
            float f2 = (float)(entity1.posX - (double)this.posXPlus);
            float f3 = (float)(entity1.posY - (double)this.posYPlus);
            float f4 = (float)(entity1.posZ - (double)this.posZPlus);
            return f2 * f2 + f3 * f3 + f4 * f4;
        }

        public virtual void setDontDraw()
        {
            for (int i = 0; i < 2; ++i)
            {
                skipRenderPass[i] = true;
                if (meshDataAllocations[i] != null)
                {
                    meshAllocator.FreeData(meshDataAllocations[i]!.Value);
                    meshDataAllocations[i] = null;
                }
            }

            isInFrustum = false;
            isInitialized = false;
        }

        public virtual void stopRendering()
        {
            setDontDraw();
            worldObj = null;
        }

        public virtual void updateInFrustum(ICamera iCamera1)
        {
            this.isInFrustum = iCamera1.isBoundingBoxInFrustum(this.rendererBoundingBox);
        }

        public virtual void TessellateOcclusionQueryAABB()
        {
            if (AABBVBO != null)
                Tessellator.instance.TessellateOcclusionQuery(AABBVBO.Value.GLHandle);
        }

        public virtual bool skipAllRenderPasses()
        {
            return !this.isInitialized ? false : this.skipRenderPass[0] && this.skipRenderPass[1];
        }

        public virtual void markDirty()
        {
            needsUpdate = true;
        }
    }

}