namespace net.minecraft.src
{
	using BlockByBlock.net.minecraft.render;
	using OpenTK.Graphics.OpenGL;
    using Minecraft = net.minecraft.client.Minecraft;

    public class TileEntityRendererPiston : TileEntitySpecialRenderer
	{
		private RenderBlocks blockRenderer;

		public virtual void renderPiston(TileEntityPiston tileEntityPiston1, double d2, double d4, double d6, float f8)
		{
			Block block9 = Block.blocksList[tileEntityPiston1.StoredBlockID];
			if (block9 != null && tileEntityPiston1.getProgress(f8) < 1.0F)
			{
				Tessellator tessellator10 = Tessellator.instance;
				this.bindTextureByName("/terrain.png");
				GameLighting.DisableMeshLighting();
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				GL.Enable(EnableCap.Blend);
				GL.Disable(EnableCap.CullFace);
				if (Minecraft.AmbientOcclusionEnabled)
				{
                    Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, true);
                }
				else
				{
                    Minecraft.renderPipeline.SetState(RenderState.SmoothShadingState, false);
                }

				tessellator10.startDrawingQuads();
				tessellator10.setTranslation((double)((float)d2 - (float)tileEntityPiston1.xCoord + tileEntityPiston1.getOffsetX(f8)), (double)((float)d4 - (float)tileEntityPiston1.yCoord + tileEntityPiston1.getOffsetY(f8)), (double)((float)d6 - (float)tileEntityPiston1.zCoord + tileEntityPiston1.getOffsetZ(f8)));
				tessellator10.setColorOpaque(1, 1, 1);
				if (block9 == Block.pistonExtension && tileEntityPiston1.getProgress(f8) < 0.5F)
				{
					this.blockRenderer.renderPistonExtensionAllFaces(block9, tileEntityPiston1.xCoord, tileEntityPiston1.yCoord, tileEntityPiston1.zCoord, false);
				}
				else if (tileEntityPiston1.shouldRenderHead() && !tileEntityPiston1.Extending)
				{
					Block.pistonExtension.HeadTexture = ((BlockPistonBase)block9).PistonExtensionTexture;
					this.blockRenderer.renderPistonExtensionAllFaces(Block.pistonExtension, tileEntityPiston1.xCoord, tileEntityPiston1.yCoord, tileEntityPiston1.zCoord, tileEntityPiston1.getProgress(f8) < 0.5F);
					Block.pistonExtension.clearHeadTexture();
					tessellator10.setTranslation((double)((float)d2 - (float)tileEntityPiston1.xCoord), (double)((float)d4 - (float)tileEntityPiston1.yCoord), (double)((float)d6 - (float)tileEntityPiston1.zCoord));
					this.blockRenderer.renderPistonBaseAllFaces(block9, tileEntityPiston1.xCoord, tileEntityPiston1.yCoord, tileEntityPiston1.zCoord);
				}
				else
				{
					this.blockRenderer.renderBlockAllFaces(block9, tileEntityPiston1.xCoord, tileEntityPiston1.yCoord, tileEntityPiston1.zCoord);
				}

				tessellator10.setTranslation(0.0D, 0.0D, 0.0D);
				tessellator10.DrawImmediate();
				GameLighting.EnableMeshLighting();
			}

		}

		public override void cacheSpecialRenderInfo(World world1)
		{
			this.blockRenderer = new RenderBlocks(world1);
		}

		public override void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			this.renderPiston((TileEntityPiston)tileEntity1, d2, d4, d6, f8);
		}
	}

}