using OpenTK.Graphics.OpenGL;
using System;
using System.Numerics;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class RenderBlocks
	{
		public IBlockAccess blockAccess;
		private int overrideBlockTexture = -1;
		private bool flipTexture = false;
		private bool renderAllFaces = false;
		public static bool fancyGrass = true;
		public bool useInventoryTint = true;
		private int uvRotateEast = 0;
		private int uvRotateWest = 0;
		private int uvRotateSouth = 0;
		private int uvRotateNorth = 0;
		private int uvRotateTop = 0;
		private int uvRotateBottom = 0;
		private bool enableAO;
		private float lightValueOwn;
		private float aoLightValueXNeg;
		private float aoLightValueYNeg;
		private float aoLightValueZNeg;
		private float aoLightValueXPos;
		private float aoLightValueYPos;
		private float aoLightValueZPos;

		private float aoLightValueScratchTopLeft;
        private float aoLightValueScratchTopRight;
        private float aoLightValueScratchBottomRight;
        private float aoLightValueScratchBottomLeft;


        private float aoLightValueScratchXYZNNN;
		private float aoLightValueScratchXYNN;
		private float aoLightValueScratchXYZNNP;
		private float aoLightValueScratchYZNN;
		private float aoLightValueScratchYZNP;
		private float aoLightValueScratchXYZPNN;
		private float aoLightValueScratchXYPN;
		private float aoLightValueScratchXYZPNP;
		private float aoLightValueScratchXYZNPN;
		private float aoLightValueScratchXYNP;
		private float aoLightValueScratchXYZNPP;
		private float aoLightValueScratchYZPN;
		private float aoLightValueScratchXYZPPN;
		private float aoLightValueScratchXYPP;
		private float aoLightValueScratchYZPP;
		private float aoLightValueScratchXYZPPP;
		private float aoLightValueScratchXZNN;
		private float aoLightValueScratchXZPN;
		private float aoLightValueScratchXZNP;
		private float aoLightValueScratchXZPP;

		private int aoBrightnessTopLeft;
		private int aoBrightnessTopRight;
		private int aoBrightnessBottomLeft;
		private int aoBrightnessBottomRight;

		private int aoBrightnessXYZNNN;
		private int aoBrightnessXYNN;
		private int aoBrightnessXYZNNP;
		private int aoBrightnessYZNN;
		private int aoBrightnessYZNP;
		private int aoBrightnessXYZPNN;
		private int aoBrightnessXYPN;
		private int aoBrightnessXYZPNP;
		private int aoBrightnessXYZNPN;
		private int aoBrightnessXYNP;
		private int aoBrightnessXYZNPP;
		private int aoBrightnessYZPN;
		private int aoBrightnessXYZPPN;
		private int aoBrightnessXYPP;
		private int aoBrightnessYZPP;
		private int aoBrightnessXYZPPP;
		private int aoBrightnessXZNN;
		private int aoBrightnessXZPN;
		private int aoBrightnessXZNP;
		private int aoBrightnessXZPP;

		private int aoType = 1;
		private int brightnessTopLeft;
		private int brightnessBottomLeft;
		private int brightnessBottomRight;
		private int brightnessTopRight;
		private float colorRedTopLeft;
		private float colorRedBottomLeft;
		private float colorRedBottomRight;
		private float colorRedTopRight;
		private float colorGreenTopLeft;
		private float colorGreenBottomLeft;
		private float colorGreenBottomRight;
		private float colorGreenTopRight;
		private float colorBlueTopLeft;
		private float colorBlueBottomLeft;
		private float colorBlueBottomRight;
		private float colorBlueTopRight;
		private bool aoGrassXYZCPN;
		private bool aoGrassXYZPPC;
		private bool aoGrassXYZNPC;
		private bool aoGrassXYZCPP;
		private bool aoGrassXYZNCN;
		private bool aoGrassXYZPCP;
		private bool aoGrassXYZNCP;
		private bool aoGrassXYZPCN;
		private bool aoGrassXYZCNN;
		private bool aoGrassXYZPNC;
		private bool aoGrassXYZNNC;
		private bool aoGrassXYZCNP;

		public RenderBlocks(IBlockAccess iBlockAccess1)
		{
			this.blockAccess = iBlockAccess1;
		}

		public RenderBlocks()
		{
		}

		public virtual void clearOverrideBlockTexture()
		{
			this.overrideBlockTexture = -1;
		}

		public virtual void renderBlockUsingTexture(Block block1, int i2, int i3, int i4, int i5)
		{
			this.overrideBlockTexture = i5;
			this.renderBlockByRenderType(block1, i2, i3, i4);
			this.overrideBlockTexture = -1;
		}

		public virtual void renderBlockAllFaces(Block block1, int i2, int i3, int i4)
		{
			this.renderAllFaces = true;
			this.renderBlockByRenderType(block1, i2, i3, i4);
			this.renderAllFaces = false;
		}

		public virtual bool renderBlockByRenderType(Block block1, int i2, int i3, int i4)
		{
			int i5 = block1.RenderType;
			block1.setBlockBoundsBasedOnState(this.blockAccess, i2, i3, i4);
			return i5 == 0 ? this.renderStandardBlock(block1, i2, i3, i4) : (i5 == 4 ? this.renderBlockFluids(block1, i2, i3, i4) : (i5 == 13 ? this.renderBlockCactus(block1, i2, i3, i4) : (i5 == 1 ? this.renderCrossedSquares(block1, i2, i3, i4) : (i5 == 19 ? this.renderBlockStem(block1, i2, i3, i4) : (i5 == 23 ? this.renderBlockLilyPad(block1, i2, i3, i4) : (i5 == 6 ? this.renderBlockCrops(block1, i2, i3, i4) : (i5 == 2 ? this.renderBlockTorch(block1, i2, i3, i4) : (i5 == 3 ? this.renderBlockFire(block1, i2, i3, i4) : (i5 == 5 ? this.renderBlockRedstoneWire(block1, i2, i3, i4) : (i5 == 8 ? this.renderBlockLadder(block1, i2, i3, i4) : (i5 == 7 ? this.renderBlockDoor(block1, i2, i3, i4) : (i5 == 9 ? this.renderBlockMinecartTrack((BlockRail)block1, i2, i3, i4) : (i5 == 10 ? this.renderBlockStairs(block1, i2, i3, i4) : (i5 == 27 ? this.renderBlockDragonEgg((BlockDragonEgg)block1, i2, i3, i4) : (i5 == 11 ? this.renderBlockFence((BlockFence)block1, i2, i3, i4) : (i5 == 12 ? this.renderBlockLever(block1, i2, i3, i4) : (i5 == 14 ? this.renderBlockBed(block1, i2, i3, i4) : (i5 == 15 ? this.renderBlockRepeater(block1, i2, i3, i4) : (i5 == 16 ? this.renderPistonBase(block1, i2, i3, i4, false) : (i5 == 17 ? this.renderPistonExtension(block1, i2, i3, i4, true) : (i5 == 18 ? this.renderBlockPane((BlockPane)block1, i2, i3, i4) : (i5 == 20 ? this.renderBlockVine(block1, i2, i3, i4) : (i5 == 21 ? this.renderBlockFenceGate((BlockFenceGate)block1, i2, i3, i4) : (i5 == 24 ? this.renderBlockCauldron((BlockCauldron)block1, i2, i3, i4) : (i5 == 25 ? this.renderBlockBrewingStand((BlockBrewingStand)block1, i2, i3, i4) : (i5 == 26 ? this.renderBlockEndPortalFrame(block1, i2, i3, i4) : false))))))))))))))))))))))))));
		}

		private bool renderBlockEndPortalFrame(Block block1, int i2, int i3, int i4)
		{
			int i5 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i6 = i5 & 3;
			if (i6 == 0)
			{
				this.uvRotateTop = 3;
			}
			else if (i6 == 3)
			{
				this.uvRotateTop = 1;
			}
			else if (i6 == 1)
			{
				this.uvRotateTop = 2;
			}

			if (!BlockEndPortalFrame.isEnderEyeInserted(i5))
			{
				block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.8125F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				block1.setBlockBoundsForItemRender();
				this.uvRotateTop = 0;
				return true;
			}
			else
			{
				block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.8125F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.overrideBlockTexture = 174;
				block1.setBlockBounds(0.25F, 0.8125F, 0.25F, 0.75F, 1.0F, 0.75F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.clearOverrideBlockTexture();
				block1.setBlockBoundsForItemRender();
				this.uvRotateTop = 0;
				return true;
			}
		}

		private bool renderBlockBed(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i7 = BlockBed.getDirection(i6);
			bool z8 = BlockBed.isBlockFootOfBed(i6);
			float f9 = 0.5F;
			float f10 = 1.0F;
			float f11 = 0.8F;
			float f12 = 0.6F;
			int i25 = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			tessellator5.Brightness = i25;
			tessellator5.setColorOpaque_F(f9, f9, f9);
			int i27 = block1.getBlockTexture(this.blockAccess, i2, i3, i4, 0);
			int i28 = (i27 & 15) << 4;
			int i29 = i27 & 240;
			double d30 = (double)((float)i28 / 256.0F);
			double d32 = ((double)(i28 + 16) - 0.01D) / 256.0D;
			double d34 = (double)((float)i29 / 256.0F);
			double d36 = ((double)(i29 + 16) - 0.01D) / 256.0D;
			double d38 = (double)i2 + block1.minX;
			double d40 = (double)i2 + block1.maxX;
			double d42 = (double)i3 + block1.minY + 0.1875D;
			double d44 = (double)i4 + block1.minZ;
			double d46 = (double)i4 + block1.maxZ;
			tessellator5.AddVertexWithUV(d38, d42, d46, d30, d36);
			tessellator5.AddVertexWithUV(d38, d42, d44, d30, d34);
			tessellator5.AddVertexWithUV(d40, d42, d44, d32, d34);
			tessellator5.AddVertexWithUV(d40, d42, d46, d32, d36);
			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3 + 1, i4);
			tessellator5.setColorOpaque_F(f10, f10, f10);
			i27 = block1.getBlockTexture(this.blockAccess, i2, i3, i4, 1);
			i28 = (i27 & 15) << 4;
			i29 = i27 & 240;
			d30 = (double)((float)i28 / 256.0F);
			d32 = ((double)(i28 + 16) - 0.01D) / 256.0D;
			d34 = (double)((float)i29 / 256.0F);
			d36 = ((double)(i29 + 16) - 0.01D) / 256.0D;
			d38 = d30;
			d40 = d32;
			d42 = d34;
			d44 = d34;
			d46 = d30;
			double d48 = d32;
			double d50 = d36;
			double d52 = d36;
			if (i7 == 0)
			{
				d40 = d30;
				d42 = d36;
				d46 = d32;
				d52 = d34;
			}
			else if (i7 == 2)
			{
				d38 = d32;
				d44 = d36;
				d48 = d30;
				d50 = d34;
			}
			else if (i7 == 3)
			{
				d38 = d32;
				d44 = d36;
				d48 = d30;
				d50 = d34;
				d40 = d30;
				d42 = d36;
				d46 = d32;
				d52 = d34;
			}

			double d54 = (double)i2 + block1.minX;
			double d56 = (double)i2 + block1.maxX;
			double d58 = (double)i3 + block1.maxY;
			double d60 = (double)i4 + block1.minZ;
			double d62 = (double)i4 + block1.maxZ;
			tessellator5.AddVertexWithUV(d56, d58, d62, d46, d50);
			tessellator5.AddVertexWithUV(d56, d58, d60, d38, d42);
			tessellator5.AddVertexWithUV(d54, d58, d60, d40, d44);
			tessellator5.AddVertexWithUV(d54, d58, d62, d48, d52);
			i27 = Direction.headInvisibleFace[i7];
			if (z8)
			{
				i27 = Direction.headInvisibleFace[Direction.footInvisibleFaceRemap[i7]];
			}

			sbyte b64 = 4;
			switch (i7)
			{
			case 0:
				b64 = 5;
				break;
			case 1:
				b64 = 3;
				goto case 2;
			case 2:
			default:
				break;
			case 3:
				b64 = 2;
			break;
			}

			if (i27 != 2 && (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2, i3, i4 - 1, 2)))
			{
				tessellator5.Brightness = block1.minZ > 0.0D ? i25 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4 - 1);
				tessellator5.setColorOpaque_F(f11, f11, f11);
				this.flipTexture = b64 == 2;
				this.renderEastFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 2));
			}

			if (i27 != 3 && (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2, i3, i4 + 1, 3)))
			{
				tessellator5.Brightness = block1.maxZ < 1.0D ? i25 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4 + 1);
				tessellator5.setColorOpaque_F(f11, f11, f11);
				this.flipTexture = b64 == 3;
				this.renderWestFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 3));
			}

			if (i27 != 4 && (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2 - 1, i3, i4, 4)))
			{
				tessellator5.Brightness = block1.minZ > 0.0D ? i25 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2 - 1, i3, i4);
				tessellator5.setColorOpaque_F(f12, f12, f12);
				this.flipTexture = b64 == 4;
				this.renderNorthFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 4));
			}

			if (i27 != 5 && (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2 + 1, i3, i4, 5)))
			{
				tessellator5.Brightness = block1.maxZ < 1.0D ? i25 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2 + 1, i3, i4);
				tessellator5.setColorOpaque_F(f12, f12, f12);
				this.flipTexture = b64 == 5;
				this.renderSouthFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 5));
			}

			this.flipTexture = false;
			return true;
		}

		private bool renderBlockBrewingStand(BlockBrewingStand blockBrewingStand1, int i2, int i3, int i4)
		{
			blockBrewingStand1.setBlockBounds(0.4375F, 0.0F, 0.4375F, 0.5625F, 0.875F, 0.5625F);
			this.renderStandardBlock(blockBrewingStand1, i2, i3, i4);
			this.overrideBlockTexture = 156;
			blockBrewingStand1.setBlockBounds(0.5625F, 0.0F, 0.3125F, 0.9375F, 0.125F, 0.6875F);
			this.renderStandardBlock(blockBrewingStand1, i2, i3, i4);
			blockBrewingStand1.setBlockBounds(0.125F, 0.0F, 0.0625F, 0.5F, 0.125F, 0.4375F);
			this.renderStandardBlock(blockBrewingStand1, i2, i3, i4);
			blockBrewingStand1.setBlockBounds(0.125F, 0.0F, 0.5625F, 0.5F, 0.125F, 0.9375F);
			this.renderStandardBlock(blockBrewingStand1, i2, i3, i4);
			this.clearOverrideBlockTexture();
			Tessellator tessellator5 = Tessellator.instance;
			tessellator5.Brightness = blockBrewingStand1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f6 = 1.0F;
			int i7 = blockBrewingStand1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f8 = (float)(i7 >> 16 & 255) / 255.0F;
			float f9 = (float)(i7 >> 8 & 255) / 255.0F;
			float f10 = (float)(i7 & 255) / 255.0F;
            
			tessellator5.setColorOpaque_F(f6 * f8, f6 * f9, f6 * f10);
			int i34 = blockBrewingStand1.getBlockTextureFromSideAndMetadata(0, 0);
			if (this.overrideBlockTexture >= 0)
			{
				i34 = this.overrideBlockTexture;
			}

			int i35 = (i34 & 15) << 4;
			int i36 = i34 & 240;
			double d14 = (double)((float)i36 / 256.0F);
			double d16 = (double)(((float)i36 + 15.99F) / 256.0F);
			int i18 = this.blockAccess.getBlockMetadata(i2, i3, i4);

			for (int i19 = 0; i19 < 3; ++i19)
			{
				double d20 = (double)i19 * Math.PI * 2.0D / 3.0D + Math.PI / 2D;
				double d22 = (double)(((float)i35 + 8.0F) / 256.0F);
				double d24 = (double)(((float)i35 + 15.99F) / 256.0F);
				if ((i18 & 1 << i19) != 0)
				{
					d22 = (double)(((float)i35 + 7.99F) / 256.0F);
					d24 = (double)(((float)i35 + 0.0F) / 256.0F);
				}

				double d26 = (double)i2 + 0.5D;
				double d28 = (double)i2 + 0.5D + Math.Sin(d20) * 8.0D / 16.0D;
				double d30 = (double)i4 + 0.5D;
				double d32 = (double)i4 + 0.5D + Math.Cos(d20) * 8.0D / 16.0D;
				tessellator5.AddVertexWithUV(d26, (double)(i3 + 1), d30, d22, d14);
				tessellator5.AddVertexWithUV(d26, (double)(i3 + 0), d30, d22, d16);
				tessellator5.AddVertexWithUV(d28, (double)(i3 + 0), d32, d24, d16);
				tessellator5.AddVertexWithUV(d28, (double)(i3 + 1), d32, d24, d14);
				tessellator5.AddVertexWithUV(d28, (double)(i3 + 1), d32, d24, d14);
				tessellator5.AddVertexWithUV(d28, (double)(i3 + 0), d32, d24, d16);
				tessellator5.AddVertexWithUV(d26, (double)(i3 + 0), d30, d22, d16);
				tessellator5.AddVertexWithUV(d26, (double)(i3 + 1), d30, d22, d14);
			}

			blockBrewingStand1.setBlockBoundsForItemRender();
			return true;
		}

		private bool renderBlockCauldron(BlockCauldron blockCauldron1, int i2, int i3, int i4)
		{
			this.renderStandardBlock(blockCauldron1, i2, i3, i4);
			Tessellator tessellator5 = Tessellator.instance;
			tessellator5.Brightness = blockCauldron1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f6 = 1.0F;
			int i7 = blockCauldron1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f8 = (float)(i7 >> 16 & 255) / 255.0F;
			float f9 = (float)(i7 >> 8 & 255) / 255.0F;
			float f10 = (float)(i7 & 255) / 255.0F;
			float f12;

			tessellator5.setColorOpaque_F(f6 * f8, f6 * f9, f6 * f10);
			short s16 = 154;
			f12 = 0.125F;
			this.renderSouthFace(blockCauldron1, (double)((float)i2 - 1.0F + f12), (double)i3, (double)i4, s16);
			this.renderNorthFace(blockCauldron1, (double)((float)i2 + 1.0F - f12), (double)i3, (double)i4, s16);
			this.renderWestFace(blockCauldron1, (double)i2, (double)i3, (double)((float)i4 - 1.0F + f12), s16);
			this.renderEastFace(blockCauldron1, (double)i2, (double)i3, (double)((float)i4 + 1.0F - f12), s16);
			short s17 = 139;
			this.renderTopFace(blockCauldron1, (double)i2, (double)((float)i3 - 1.0F + 0.25F), (double)i4, s17);
			this.renderBottomFace(blockCauldron1, (double)i2, (double)((float)i3 + 1.0F - 0.75F), (double)i4, s17);
			int i14 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			if (i14 > 0)
			{
				short s15 = 205;
				if (i14 > 3)
				{
					i14 = 3;
				}

				this.renderTopFace(blockCauldron1, (double)i2, (double)((float)i3 - 1.0F + (6.0F + (float)i14 * 3.0F) / 16.0F), (double)i4, s15);
			}

			return true;
		}

		public virtual bool renderBlockTorch(Block block1, int i2, int i3, int i4)
		{
			int i5 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			Tessellator tessellator6 = Tessellator.instance;
			tessellator6.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			tessellator6.setColorOpaque_F(1.0F, 1.0F, 1.0F);
			double d7 = (double)0.4F;
			double d9 = 0.5D - d7;
			double d11 = (double)0.2F;
			if (i5 == 1)
			{
				this.renderTorchAtAngle(block1, (double)i2 - d9, (double)i3 + d11, (double)i4, -d7, 0.0D);
			}
			else if (i5 == 2)
			{
				this.renderTorchAtAngle(block1, (double)i2 + d9, (double)i3 + d11, (double)i4, d7, 0.0D);
			}
			else if (i5 == 3)
			{
				this.renderTorchAtAngle(block1, (double)i2, (double)i3 + d11, (double)i4 - d9, 0.0D, -d7);
			}
			else if (i5 == 4)
			{
				this.renderTorchAtAngle(block1, (double)i2, (double)i3 + d11, (double)i4 + d9, 0.0D, d7);
			}
			else
			{
				this.renderTorchAtAngle(block1, (double)i2, (double)i3, (double)i4, 0.0D, 0.0D);
			}

			return true;
		}

		private bool renderBlockRepeater(Block block1, int i2, int i3, int i4)
		{
			int i5 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i6 = i5 & 3;
			int i7 = (i5 & 12) >> 2;
			this.renderStandardBlock(block1, i2, i3, i4);
			Tessellator tessellator8 = Tessellator.instance;
			tessellator8.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			tessellator8.setColorOpaque_F(1.0F, 1.0F, 1.0F);
			double d9 = -0.1875D;
			double d11 = 0.0D;
			double d13 = 0.0D;
			double d15 = 0.0D;
			double d17 = 0.0D;
			switch (i6)
			{
			case 0:
				d17 = -0.3125D;
				d13 = BlockRedstoneRepeater.repeaterTorchOffset[i7];
				break;
			case 1:
				d15 = 0.3125D;
				d11 = -BlockRedstoneRepeater.repeaterTorchOffset[i7];
				break;
			case 2:
				d17 = 0.3125D;
				d13 = -BlockRedstoneRepeater.repeaterTorchOffset[i7];
				break;
			case 3:
				d15 = -0.3125D;
				d11 = BlockRedstoneRepeater.repeaterTorchOffset[i7];
			break;
			}

			this.renderTorchAtAngle(block1, (double)i2 + d11, (double)i3 + d9, (double)i4 + d13, 0.0D, 0.0D);
			this.renderTorchAtAngle(block1, (double)i2 + d15, (double)i3 + d9, (double)i4 + d17, 0.0D, 0.0D);
			int i19 = block1.getBlockTextureFromSide(1);
			int i20 = (i19 & 15) << 4;
			int i21 = i19 & 240;
			double d22 = (double)((float)i20 / 256.0F);
			double d24 = (double)(((float)i20 + 15.99F) / 256.0F);
			double d26 = (double)((float)i21 / 256.0F);
			double d28 = (double)(((float)i21 + 15.99F) / 256.0F);
			double d30 = 0.125D;
			double d32 = (double)(i2 + 1);
			double d34 = (double)(i2 + 1);
			double d36 = (double)(i2 + 0);
			double d38 = (double)(i2 + 0);
			double d40 = (double)(i4 + 0);
			double d42 = (double)(i4 + 1);
			double d44 = (double)(i4 + 1);
			double d46 = (double)(i4 + 0);
			double d48 = (double)i3 + d30;
			if (i6 == 2)
			{
				d32 = d34 = (double)(i2 + 0);
				d36 = d38 = (double)(i2 + 1);
				d40 = d46 = (double)(i4 + 1);
				d42 = d44 = (double)(i4 + 0);
			}
			else if (i6 == 3)
			{
				d32 = d38 = (double)(i2 + 0);
				d34 = d36 = (double)(i2 + 1);
				d40 = d42 = (double)(i4 + 0);
				d44 = d46 = (double)(i4 + 1);
			}
			else if (i6 == 1)
			{
				d32 = d38 = (double)(i2 + 1);
				d34 = d36 = (double)(i2 + 0);
				d40 = d42 = (double)(i4 + 1);
				d44 = d46 = (double)(i4 + 0);
			}

			tessellator8.AddVertexWithUV(d38, d48, d46, d22, d26);
			tessellator8.AddVertexWithUV(d36, d48, d44, d22, d28);
			tessellator8.AddVertexWithUV(d34, d48, d42, d24, d28);
			tessellator8.AddVertexWithUV(d32, d48, d40, d24, d26);
			return true;
		}

		public virtual void renderPistonBaseAllFaces(Block block1, int i2, int i3, int i4)
		{
			this.renderAllFaces = true;
			this.renderPistonBase(block1, i2, i3, i4, true);
			this.renderAllFaces = false;
		}

		private bool renderPistonBase(Block block1, int i2, int i3, int i4, bool z5)
		{
			int i6 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			bool z7 = z5 || (i6 & 8) != 0;
			int i8 = BlockPistonBase.getOrientation(i6);
			if (z7)
			{
				switch (i8)
				{
				case 0:
					this.uvRotateEast = 3;
					this.uvRotateWest = 3;
					this.uvRotateSouth = 3;
					this.uvRotateNorth = 3;
					block1.setBlockBounds(0.0F, 0.25F, 0.0F, 1.0F, 1.0F, 1.0F);
					break;
				case 1:
					block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.75F, 1.0F);
					break;
				case 2:
					this.uvRotateSouth = 1;
					this.uvRotateNorth = 2;
					block1.setBlockBounds(0.0F, 0.0F, 0.25F, 1.0F, 1.0F, 1.0F);
					break;
				case 3:
					this.uvRotateSouth = 2;
					this.uvRotateNorth = 1;
					this.uvRotateTop = 3;
					this.uvRotateBottom = 3;
					block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.75F);
					break;
				case 4:
					this.uvRotateEast = 1;
					this.uvRotateWest = 2;
					this.uvRotateTop = 2;
					this.uvRotateBottom = 1;
					block1.setBlockBounds(0.25F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
					break;
				case 5:
					this.uvRotateEast = 2;
					this.uvRotateWest = 1;
					this.uvRotateTop = 1;
					this.uvRotateBottom = 2;
					block1.setBlockBounds(0.0F, 0.0F, 0.0F, 0.75F, 1.0F, 1.0F);
				break;
				}

				this.renderStandardBlock(block1, i2, i3, i4);
				this.uvRotateEast = 0;
				this.uvRotateWest = 0;
				this.uvRotateSouth = 0;
				this.uvRotateNorth = 0;
				this.uvRotateTop = 0;
				this.uvRotateBottom = 0;
				block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}
			else
			{
				switch (i8)
				{
				case 0:
					this.uvRotateEast = 3;
					this.uvRotateWest = 3;
					this.uvRotateSouth = 3;
					this.uvRotateNorth = 3;
					goto case 1;
				case 1:
				default:
					break;
				case 2:
					this.uvRotateSouth = 1;
					this.uvRotateNorth = 2;
					break;
				case 3:
					this.uvRotateSouth = 2;
					this.uvRotateNorth = 1;
					this.uvRotateTop = 3;
					this.uvRotateBottom = 3;
					break;
				case 4:
					this.uvRotateEast = 1;
					this.uvRotateWest = 2;
					this.uvRotateTop = 2;
					this.uvRotateBottom = 1;
					break;
				case 5:
					this.uvRotateEast = 2;
					this.uvRotateWest = 1;
					this.uvRotateTop = 1;
					this.uvRotateBottom = 2;
				break;
				}

				this.renderStandardBlock(block1, i2, i3, i4);
				this.uvRotateEast = 0;
				this.uvRotateWest = 0;
				this.uvRotateSouth = 0;
				this.uvRotateNorth = 0;
				this.uvRotateTop = 0;
				this.uvRotateBottom = 0;
			}

			return true;
		}

		private void renderPistonRodUD(double d1, double d3, double d5, double d7, double d9, double d11, float f13, double d14)
		{
			int i16 = 108;
			if (this.overrideBlockTexture >= 0)
			{
				i16 = this.overrideBlockTexture;
			}

			int i17 = (i16 & 15) << 4;
			int i18 = i16 & 240;
			Tessellator tessellator19 = Tessellator.instance;
			double d20 = (double)((float)(i17 + 0) / 256.0F);
			double d22 = (double)((float)(i18 + 0) / 256.0F);
			double d24 = ((double)i17 + d14 - 0.01D) / 256.0D;
			double d26 = ((double)((float)i18 + 4.0F) - 0.01D) / 256.0D;
			tessellator19.setColorOpaque_F(f13, f13, f13);
			tessellator19.AddVertexWithUV(d1, d7, d9, d24, d22);
			tessellator19.AddVertexWithUV(d1, d5, d9, d20, d22);
			tessellator19.AddVertexWithUV(d3, d5, d11, d20, d26);
			tessellator19.AddVertexWithUV(d3, d7, d11, d24, d26);
		}

		private void renderPistonRodSN(double d1, double d3, double d5, double d7, double d9, double d11, float f13, double d14)
		{
			int i16 = 108;
			if (this.overrideBlockTexture >= 0)
			{
				i16 = this.overrideBlockTexture;
			}

			int i17 = (i16 & 15) << 4;
			int i18 = i16 & 240;
			Tessellator tessellator19 = Tessellator.instance;
			double d20 = (double)((float)(i17 + 0) / 256.0F);
			double d22 = (double)((float)(i18 + 0) / 256.0F);
			double d24 = ((double)i17 + d14 - 0.01D) / 256.0D;
			double d26 = ((double)((float)i18 + 4.0F) - 0.01D) / 256.0D;
			tessellator19.setColorOpaque_F(f13, f13, f13);
			tessellator19.AddVertexWithUV(d1, d5, d11, d24, d22);
			tessellator19.AddVertexWithUV(d1, d5, d9, d20, d22);
			tessellator19.AddVertexWithUV(d3, d7, d9, d20, d26);
			tessellator19.AddVertexWithUV(d3, d7, d11, d24, d26);
		}

		private void renderPistonRodEW(double d1, double d3, double d5, double d7, double d9, double d11, float f13, double d14)
		{
			int i16 = 108;
			if (this.overrideBlockTexture >= 0)
			{
				i16 = this.overrideBlockTexture;
			}

			int i17 = (i16 & 15) << 4;
			int i18 = i16 & 240;
			Tessellator tessellator19 = Tessellator.instance;
			double d20 = (double)((float)(i17 + 0) / 256.0F);
			double d22 = (double)((float)(i18 + 0) / 256.0F);
			double d24 = ((double)i17 + d14 - 0.01D) / 256.0D;
			double d26 = ((double)((float)i18 + 4.0F) - 0.01D) / 256.0D;
			tessellator19.setColorOpaque_F(f13, f13, f13);
			tessellator19.AddVertexWithUV(d3, d5, d9, d24, d22);
			tessellator19.AddVertexWithUV(d1, d5, d9, d20, d22);
			tessellator19.AddVertexWithUV(d1, d7, d11, d20, d26);
			tessellator19.AddVertexWithUV(d3, d7, d11, d24, d26);
		}

		public virtual void renderPistonExtensionAllFaces(Block block1, int i2, int i3, int i4, bool z5)
		{
			this.renderAllFaces = true;
			this.renderPistonExtension(block1, i2, i3, i4, z5);
			this.renderAllFaces = false;
		}

		private bool renderPistonExtension(Block block1, int i2, int i3, int i4, bool z5)
		{
			int i6 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i7 = BlockPistonExtension.getDirectionMeta(i6);
			float f11 = block1.getBlockBrightness(this.blockAccess, i2, i3, i4);
			float f12 = z5 ? 1.0F : 0.5F;
			double d13 = z5 ? 16.0D : 8.0D;
			switch (i7)
			{
			case 0:
				this.uvRotateEast = 3;
				this.uvRotateWest = 3;
				this.uvRotateSouth = 3;
				this.uvRotateNorth = 3;
				block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.25F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.renderPistonRodUD((double)((float)i2 + 0.375F), (double)((float)i2 + 0.625F), (double)((float)i3 + 0.25F), (double)((float)i3 + 0.25F + f12), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.625F), f11 * 0.8F, d13);
				this.renderPistonRodUD((double)((float)i2 + 0.625F), (double)((float)i2 + 0.375F), (double)((float)i3 + 0.25F), (double)((float)i3 + 0.25F + f12), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.375F), f11 * 0.8F, d13);
				this.renderPistonRodUD((double)((float)i2 + 0.375F), (double)((float)i2 + 0.375F), (double)((float)i3 + 0.25F), (double)((float)i3 + 0.25F + f12), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.625F), f11 * 0.6F, d13);
				this.renderPistonRodUD((double)((float)i2 + 0.625F), (double)((float)i2 + 0.625F), (double)((float)i3 + 0.25F), (double)((float)i3 + 0.25F + f12), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.375F), f11 * 0.6F, d13);
				break;
			case 1:
				block1.setBlockBounds(0.0F, 0.75F, 0.0F, 1.0F, 1.0F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.renderPistonRodUD((double)((float)i2 + 0.375F), (double)((float)i2 + 0.625F), (double)((float)i3 - 0.25F + 1.0F - f12), (double)((float)i3 - 0.25F + 1.0F), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.625F), f11 * 0.8F, d13);
				this.renderPistonRodUD((double)((float)i2 + 0.625F), (double)((float)i2 + 0.375F), (double)((float)i3 - 0.25F + 1.0F - f12), (double)((float)i3 - 0.25F + 1.0F), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.375F), f11 * 0.8F, d13);
				this.renderPistonRodUD((double)((float)i2 + 0.375F), (double)((float)i2 + 0.375F), (double)((float)i3 - 0.25F + 1.0F - f12), (double)((float)i3 - 0.25F + 1.0F), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.625F), f11 * 0.6F, d13);
				this.renderPistonRodUD((double)((float)i2 + 0.625F), (double)((float)i2 + 0.625F), (double)((float)i3 - 0.25F + 1.0F - f12), (double)((float)i3 - 0.25F + 1.0F), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.375F), f11 * 0.6F, d13);
				break;
			case 2:
				this.uvRotateSouth = 1;
				this.uvRotateNorth = 2;
				block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.25F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.renderPistonRodSN((double)((float)i2 + 0.375F), (double)((float)i2 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i4 + 0.25F), (double)((float)i4 + 0.25F + f12), f11 * 0.6F, d13);
				this.renderPistonRodSN((double)((float)i2 + 0.625F), (double)((float)i2 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i4 + 0.25F), (double)((float)i4 + 0.25F + f12), f11 * 0.6F, d13);
				this.renderPistonRodSN((double)((float)i2 + 0.375F), (double)((float)i2 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.375F), (double)((float)i4 + 0.25F), (double)((float)i4 + 0.25F + f12), f11 * 0.5F, d13);
				this.renderPistonRodSN((double)((float)i2 + 0.625F), (double)((float)i2 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.625F), (double)((float)i4 + 0.25F), (double)((float)i4 + 0.25F + f12), f11, d13);
				break;
			case 3:
				this.uvRotateSouth = 2;
				this.uvRotateNorth = 1;
				this.uvRotateTop = 3;
				this.uvRotateBottom = 3;
				block1.setBlockBounds(0.0F, 0.0F, 0.75F, 1.0F, 1.0F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.renderPistonRodSN((double)((float)i2 + 0.375F), (double)((float)i2 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i4 - 0.25F + 1.0F - f12), (double)((float)i4 - 0.25F + 1.0F), f11 * 0.6F, d13);
				this.renderPistonRodSN((double)((float)i2 + 0.625F), (double)((float)i2 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i4 - 0.25F + 1.0F - f12), (double)((float)i4 - 0.25F + 1.0F), f11 * 0.6F, d13);
				this.renderPistonRodSN((double)((float)i2 + 0.375F), (double)((float)i2 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.375F), (double)((float)i4 - 0.25F + 1.0F - f12), (double)((float)i4 - 0.25F + 1.0F), f11 * 0.5F, d13);
				this.renderPistonRodSN((double)((float)i2 + 0.625F), (double)((float)i2 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.625F), (double)((float)i4 - 0.25F + 1.0F - f12), (double)((float)i4 - 0.25F + 1.0F), f11, d13);
				break;
			case 4:
				this.uvRotateEast = 1;
				this.uvRotateWest = 2;
				this.uvRotateTop = 2;
				this.uvRotateBottom = 1;
				block1.setBlockBounds(0.0F, 0.0F, 0.0F, 0.25F, 1.0F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.renderPistonRodEW((double)((float)i2 + 0.25F), (double)((float)i2 + 0.25F + f12), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.375F), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.375F), f11 * 0.5F, d13);
				this.renderPistonRodEW((double)((float)i2 + 0.25F), (double)((float)i2 + 0.25F + f12), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.625F), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.625F), f11, d13);
				this.renderPistonRodEW((double)((float)i2 + 0.25F), (double)((float)i2 + 0.25F + f12), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.375F), f11 * 0.6F, d13);
				this.renderPistonRodEW((double)((float)i2 + 0.25F), (double)((float)i2 + 0.25F + f12), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.625F), f11 * 0.6F, d13);
				break;
			case 5:
				this.uvRotateEast = 2;
				this.uvRotateWest = 1;
				this.uvRotateTop = 1;
				this.uvRotateBottom = 2;
				block1.setBlockBounds(0.75F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
				this.renderPistonRodEW((double)((float)i2 - 0.25F + 1.0F - f12), (double)((float)i2 - 0.25F + 1.0F), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.375F), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.375F), f11 * 0.5F, d13);
				this.renderPistonRodEW((double)((float)i2 - 0.25F + 1.0F - f12), (double)((float)i2 - 0.25F + 1.0F), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.625F), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.625F), f11, d13);
				this.renderPistonRodEW((double)((float)i2 - 0.25F + 1.0F - f12), (double)((float)i2 - 0.25F + 1.0F), (double)((float)i3 + 0.375F), (double)((float)i3 + 0.625F), (double)((float)i4 + 0.375F), (double)((float)i4 + 0.375F), f11 * 0.6F, d13);
				this.renderPistonRodEW((double)((float)i2 - 0.25F + 1.0F - f12), (double)((float)i2 - 0.25F + 1.0F), (double)((float)i3 + 0.625F), (double)((float)i3 + 0.375F), (double)((float)i4 + 0.625F), (double)((float)i4 + 0.625F), f11 * 0.6F, d13);
			break;
			}

			this.uvRotateEast = 0;
			this.uvRotateWest = 0;
			this.uvRotateSouth = 0;
			this.uvRotateNorth = 0;
			this.uvRotateTop = 0;
			this.uvRotateBottom = 0;
			block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			return true;
		}

		public virtual bool renderBlockLever(Block block1, int i2, int i3, int i4)
		{
			int i5 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i6 = i5 & 7;
			bool z7 = (i5 & 8) > 0;
			Tessellator tessellator8 = Tessellator.instance;
			bool z9 = this.overrideBlockTexture >= 0;
			if (!z9)
			{
				this.overrideBlockTexture = Block.cobblestone.blockIndexInTexture;
			}

			float f10 = 0.25F;
			float f11 = 0.1875F;
			float f12 = 0.1875F;
			if (i6 == 5)
			{
				block1.setBlockBounds(0.5F - f11, 0.0F, 0.5F - f10, 0.5F + f11, f12, 0.5F + f10);
			}
			else if (i6 == 6)
			{
				block1.setBlockBounds(0.5F - f10, 0.0F, 0.5F - f11, 0.5F + f10, f12, 0.5F + f11);
			}
			else if (i6 == 4)
			{
				block1.setBlockBounds(0.5F - f11, 0.5F - f10, 1.0F - f12, 0.5F + f11, 0.5F + f10, 1.0F);
			}
			else if (i6 == 3)
			{
				block1.setBlockBounds(0.5F - f11, 0.5F - f10, 0.0F, 0.5F + f11, 0.5F + f10, f12);
			}
			else if (i6 == 2)
			{
				block1.setBlockBounds(1.0F - f12, 0.5F - f10, 0.5F - f11, 1.0F, 0.5F + f10, 0.5F + f11);
			}
			else if (i6 == 1)
			{
				block1.setBlockBounds(0.0F, 0.5F - f10, 0.5F - f11, f12, 0.5F + f10, 0.5F + f11);
			}

			this.renderStandardBlock(block1, i2, i3, i4);
			if (!z9)
			{
				this.overrideBlockTexture = -1;
			}

			tessellator8.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f13 = 1.0F;
			if (Block.lightValue[block1.blockID] > 0)
			{
				f13 = 1.0F;
			}

			tessellator8.setColorOpaque_F(f13, f13, f13);
			int i14 = block1.getBlockTextureFromSide(0);
			if (this.overrideBlockTexture >= 0)
			{
				i14 = this.overrideBlockTexture;
			}

			int i15 = (i14 & 15) << 4;
			int i16 = i14 & 240;
			float f17 = (float)i15 / 256.0F;
			float f18 = ((float)i15 + 15.99F) / 256.0F;
			float f19 = (float)i16 / 256.0F;
			float f20 = ((float)i16 + 15.99F) / 256.0F;
			Vec3D[] vec3D21 = new Vec3D[8];
			float f22 = 0.0625F;
			float f23 = 0.0625F;
			float f24 = 0.625F;
			vec3D21[0] = Vec3D.createVector((double)(-f22), 0.0D, (double)(-f23));
			vec3D21[1] = Vec3D.createVector((double)f22, 0.0D, (double)(-f23));
			vec3D21[2] = Vec3D.createVector((double)f22, 0.0D, (double)f23);
			vec3D21[3] = Vec3D.createVector((double)(-f22), 0.0D, (double)f23);
			vec3D21[4] = Vec3D.createVector((double)(-f22), (double)f24, (double)(-f23));
			vec3D21[5] = Vec3D.createVector((double)f22, (double)f24, (double)(-f23));
			vec3D21[6] = Vec3D.createVector((double)f22, (double)f24, (double)f23);
			vec3D21[7] = Vec3D.createVector((double)(-f22), (double)f24, (double)f23);

			for (int i25 = 0; i25 < 8; ++i25)
			{
				if (z7)
				{
					vec3D21[i25].zCoord -= 0.0625D;
					vec3D21[i25].rotateAroundX((float)Math.PI / 4.5F);
				}
				else
				{
					vec3D21[i25].zCoord += 0.0625D;
					vec3D21[i25].rotateAroundX(-0.69813174F);
				}

				if (i6 == 6)
				{
					vec3D21[i25].rotateAroundY((float)Math.PI / 2F);
				}

				if (i6 < 5)
				{
					vec3D21[i25].yCoord -= 0.375D;
					vec3D21[i25].rotateAroundX((float)Math.PI / 2F);
					if (i6 == 4)
					{
						vec3D21[i25].rotateAroundY(0.0F);
					}

					if (i6 == 3)
					{
						vec3D21[i25].rotateAroundY((float)Math.PI);
					}

					if (i6 == 2)
					{
						vec3D21[i25].rotateAroundY((float)Math.PI / 2F);
					}

					if (i6 == 1)
					{
						vec3D21[i25].rotateAroundY(-1.5707964F);
					}

					vec3D21[i25].xCoord += (double)i2 + 0.5D;
					vec3D21[i25].yCoord += (double)((float)i3 + 0.5F);
					vec3D21[i25].zCoord += (double)i4 + 0.5D;
				}
				else
				{
					vec3D21[i25].xCoord += (double)i2 + 0.5D;
					vec3D21[i25].yCoord += (double)((float)i3 + 0.125F);
					vec3D21[i25].zCoord += (double)i4 + 0.5D;
				}
			}

			Vec3D vec3D30 = null;
			Vec3D vec3D26 = null;
			Vec3D vec3D27 = null;
			Vec3D vec3D28 = null;

			for (int i29 = 0; i29 < 6; ++i29)
			{
				if (i29 == 0)
				{
					f17 = (float)(i15 + 7) / 256.0F;
					f18 = ((float)(i15 + 9) - 0.01F) / 256.0F;
					f19 = (float)(i16 + 6) / 256.0F;
					f20 = ((float)(i16 + 8) - 0.01F) / 256.0F;
				}
				else if (i29 == 2)
				{
					f17 = (float)(i15 + 7) / 256.0F;
					f18 = ((float)(i15 + 9) - 0.01F) / 256.0F;
					f19 = (float)(i16 + 6) / 256.0F;
					f20 = ((float)(i16 + 16) - 0.01F) / 256.0F;
				}

				if (i29 == 0)
				{
					vec3D30 = vec3D21[0];
					vec3D26 = vec3D21[1];
					vec3D27 = vec3D21[2];
					vec3D28 = vec3D21[3];
				}
				else if (i29 == 1)
				{
					vec3D30 = vec3D21[7];
					vec3D26 = vec3D21[6];
					vec3D27 = vec3D21[5];
					vec3D28 = vec3D21[4];
				}
				else if (i29 == 2)
				{
					vec3D30 = vec3D21[1];
					vec3D26 = vec3D21[0];
					vec3D27 = vec3D21[4];
					vec3D28 = vec3D21[5];
				}
				else if (i29 == 3)
				{
					vec3D30 = vec3D21[2];
					vec3D26 = vec3D21[1];
					vec3D27 = vec3D21[5];
					vec3D28 = vec3D21[6];
				}
				else if (i29 == 4)
				{
					vec3D30 = vec3D21[3];
					vec3D26 = vec3D21[2];
					vec3D27 = vec3D21[6];
					vec3D28 = vec3D21[7];
				}
				else if (i29 == 5)
				{
					vec3D30 = vec3D21[0];
					vec3D26 = vec3D21[3];
					vec3D27 = vec3D21[7];
					vec3D28 = vec3D21[4];
				}

				tessellator8.AddVertexWithUV(vec3D30.xCoord, vec3D30.yCoord, vec3D30.zCoord, (double)f17, (double)f20);
				tessellator8.AddVertexWithUV(vec3D26.xCoord, vec3D26.yCoord, vec3D26.zCoord, (double)f18, (double)f20);
				tessellator8.AddVertexWithUV(vec3D27.xCoord, vec3D27.yCoord, vec3D27.zCoord, (double)f18, (double)f19);
				tessellator8.AddVertexWithUV(vec3D28.xCoord, vec3D28.yCoord, vec3D28.zCoord, (double)f17, (double)f19);
			}

			return true;
		}

		public virtual bool renderBlockFire(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = block1.getBlockTextureFromSide(0);
			if (this.overrideBlockTexture >= 0)
			{
				i6 = this.overrideBlockTexture;
			}

			tessellator5.setColorOpaque_F(1.0F, 1.0F, 1.0F);
			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			int i7 = (i6 & 15) << 4;
			int i8 = i6 & 240;
			double d9 = (double)((float)i7 / 256.0F);
			double d11 = (double)(((float)i7 + 15.99F) / 256.0F);
			double d13 = (double)((float)i8 / 256.0F);
			double d15 = (double)(((float)i8 + 15.99F) / 256.0F);
			float f17 = 1.4F;
			double d20;
			double d22;
			double d24;
			double d26;
			double d28;
			double d30;
			double d32;
			if (!this.blockAccess.isBlockNormalCube(i2, i3 - 1, i4) && !Block.fire.canBlockCatchFire(this.blockAccess, i2, i3 - 1, i4))
			{
				float f36 = 0.2F;
				float f19 = 0.0625F;
				if ((i2 + i3 + i4 & 1) == 1)
				{
					d9 = (double)((float)i7 / 256.0F);
					d11 = (double)(((float)i7 + 15.99F) / 256.0F);
					d13 = (double)((float)(i8 + 16) / 256.0F);
					d15 = (double)(((float)i8 + 15.99F + 16.0F) / 256.0F);
				}

				if ((i2 / 2 + i3 / 2 + i4 / 2 & 1) == 1)
				{
					d20 = d11;
					d11 = d9;
					d9 = d20;
				}

				if (Block.fire.canBlockCatchFire(this.blockAccess, i2 - 1, i3, i4))
				{
					tessellator5.AddVertexWithUV((double)((float)i2 + f36), (double)((float)i3 + f17 + f19), (double)(i4 + 1), d11, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 1), d11, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d9, d15);
					tessellator5.AddVertexWithUV((double)((float)i2 + f36), (double)((float)i3 + f17 + f19), (double)(i4 + 0), d9, d13);
					tessellator5.AddVertexWithUV((double)((float)i2 + f36), (double)((float)i3 + f17 + f19), (double)(i4 + 0), d9, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d9, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 1), d11, d15);
					tessellator5.AddVertexWithUV((double)((float)i2 + f36), (double)((float)i3 + f17 + f19), (double)(i4 + 1), d11, d13);
				}

				if (Block.fire.canBlockCatchFire(this.blockAccess, i2 + 1, i3, i4))
				{
					tessellator5.AddVertexWithUV((double)((float)(i2 + 1) - f36), (double)((float)i3 + f17 + f19), (double)(i4 + 0), d9, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 1 - 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d9, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 1 - 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 1), d11, d15);
					tessellator5.AddVertexWithUV((double)((float)(i2 + 1) - f36), (double)((float)i3 + f17 + f19), (double)(i4 + 1), d11, d13);
					tessellator5.AddVertexWithUV((double)((float)(i2 + 1) - f36), (double)((float)i3 + f17 + f19), (double)(i4 + 1), d11, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 1 - 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 1), d11, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 1 - 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d9, d15);
					tessellator5.AddVertexWithUV((double)((float)(i2 + 1) - f36), (double)((float)i3 + f17 + f19), (double)(i4 + 0), d9, d13);
				}

				if (Block.fire.canBlockCatchFire(this.blockAccess, i2, i3, i4 - 1))
				{
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17 + f19), (double)((float)i4 + f36), d11, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d11, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d9, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17 + f19), (double)((float)i4 + f36), d9, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17 + f19), (double)((float)i4 + f36), d9, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d9, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 0), d11, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17 + f19), (double)((float)i4 + f36), d11, d13);
				}

				if (Block.fire.canBlockCatchFire(this.blockAccess, i2, i3, i4 + 1))
				{
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17 + f19), (double)((float)(i4 + 1) - f36), d9, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 0) + f19), (double)(i4 + 1 - 0), d9, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 1 - 0), d11, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17 + f19), (double)((float)(i4 + 1) - f36), d11, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17 + f19), (double)((float)(i4 + 1) - f36), d11, d13);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 0) + f19), (double)(i4 + 1 - 0), d11, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 0) + f19), (double)(i4 + 1 - 0), d9, d15);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17 + f19), (double)((float)(i4 + 1) - f36), d9, d13);
				}

				if (Block.fire.canBlockCatchFire(this.blockAccess, i2, i3 + 1, i4))
				{
					d20 = (double)i2 + 0.5D + 0.5D;
					d22 = (double)i2 + 0.5D - 0.5D;
					d24 = (double)i4 + 0.5D + 0.5D;
					d26 = (double)i4 + 0.5D - 0.5D;
					d28 = (double)i2 + 0.5D - 0.5D;
					d30 = (double)i2 + 0.5D + 0.5D;
					d32 = (double)i4 + 0.5D - 0.5D;
					double d34 = (double)i4 + 0.5D + 0.5D;
					d9 = (double)((float)i7 / 256.0F);
					d11 = (double)(((float)i7 + 15.99F) / 256.0F);
					d13 = (double)((float)i8 / 256.0F);
					d15 = (double)(((float)i8 + 15.99F) / 256.0F);
					++i3;
					f17 = -0.2F;
					if ((i2 + i3 + i4 & 1) == 0)
					{
						tessellator5.AddVertexWithUV(d28, (double)((float)i3 + f17), (double)(i4 + 0), d11, d13);
						tessellator5.AddVertexWithUV(d20, (double)(i3 + 0), (double)(i4 + 0), d11, d15);
						tessellator5.AddVertexWithUV(d20, (double)(i3 + 0), (double)(i4 + 1), d9, d15);
						tessellator5.AddVertexWithUV(d28, (double)((float)i3 + f17), (double)(i4 + 1), d9, d13);
						d9 = (double)((float)i7 / 256.0F);
						d11 = (double)(((float)i7 + 15.99F) / 256.0F);
						d13 = (double)((float)(i8 + 16) / 256.0F);
						d15 = (double)(((float)i8 + 15.99F + 16.0F) / 256.0F);
						tessellator5.AddVertexWithUV(d30, (double)((float)i3 + f17), (double)(i4 + 1), d11, d13);
						tessellator5.AddVertexWithUV(d22, (double)(i3 + 0), (double)(i4 + 1), d11, d15);
						tessellator5.AddVertexWithUV(d22, (double)(i3 + 0), (double)(i4 + 0), d9, d15);
						tessellator5.AddVertexWithUV(d30, (double)((float)i3 + f17), (double)(i4 + 0), d9, d13);
					}
					else
					{
						tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17), d34, d11, d13);
						tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), d26, d11, d15);
						tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), d26, d9, d15);
						tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17), d34, d9, d13);
						d9 = (double)((float)i7 / 256.0F);
						d11 = (double)(((float)i7 + 15.99F) / 256.0F);
						d13 = (double)((float)(i8 + 16) / 256.0F);
						d15 = (double)(((float)i8 + 15.99F + 16.0F) / 256.0F);
						tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17), d32, d11, d13);
						tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), d24, d11, d15);
						tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), d24, d9, d15);
						tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17), d32, d9, d13);
					}
				}
			}
			else
			{
				double d18 = (double)i2 + 0.5D + 0.2D;
				d20 = (double)i2 + 0.5D - 0.2D;
				d22 = (double)i4 + 0.5D + 0.2D;
				d24 = (double)i4 + 0.5D - 0.2D;
				d26 = (double)i2 + 0.5D - 0.3D;
				d28 = (double)i2 + 0.5D + 0.3D;
				d30 = (double)i4 + 0.5D - 0.3D;
				d32 = (double)i4 + 0.5D + 0.3D;
				tessellator5.AddVertexWithUV(d26, (double)((float)i3 + f17), (double)(i4 + 1), d11, d13);
				tessellator5.AddVertexWithUV(d18, (double)(i3 + 0), (double)(i4 + 1), d11, d15);
				tessellator5.AddVertexWithUV(d18, (double)(i3 + 0), (double)(i4 + 0), d9, d15);
				tessellator5.AddVertexWithUV(d26, (double)((float)i3 + f17), (double)(i4 + 0), d9, d13);
				tessellator5.AddVertexWithUV(d28, (double)((float)i3 + f17), (double)(i4 + 0), d11, d13);
				tessellator5.AddVertexWithUV(d20, (double)(i3 + 0), (double)(i4 + 0), d11, d15);
				tessellator5.AddVertexWithUV(d20, (double)(i3 + 0), (double)(i4 + 1), d9, d15);
				tessellator5.AddVertexWithUV(d28, (double)((float)i3 + f17), (double)(i4 + 1), d9, d13);
				d9 = (double)((float)i7 / 256.0F);
				d11 = (double)(((float)i7 + 15.99F) / 256.0F);
				d13 = (double)((float)(i8 + 16) / 256.0F);
				d15 = (double)(((float)i8 + 15.99F + 16.0F) / 256.0F);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17), d32, d11, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), d24, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), d24, d9, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17), d32, d9, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17), d30, d11, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), d22, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), d22, d9, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17), d30, d9, d13);
				d18 = (double)i2 + 0.5D - 0.5D;
				d20 = (double)i2 + 0.5D + 0.5D;
				d22 = (double)i4 + 0.5D - 0.5D;
				d24 = (double)i4 + 0.5D + 0.5D;
				d26 = (double)i2 + 0.5D - 0.4D;
				d28 = (double)i2 + 0.5D + 0.4D;
				d30 = (double)i4 + 0.5D - 0.4D;
				d32 = (double)i4 + 0.5D + 0.4D;
				tessellator5.AddVertexWithUV(d26, (double)((float)i3 + f17), (double)(i4 + 0), d9, d13);
				tessellator5.AddVertexWithUV(d18, (double)(i3 + 0), (double)(i4 + 0), d9, d15);
				tessellator5.AddVertexWithUV(d18, (double)(i3 + 0), (double)(i4 + 1), d11, d15);
				tessellator5.AddVertexWithUV(d26, (double)((float)i3 + f17), (double)(i4 + 1), d11, d13);
				tessellator5.AddVertexWithUV(d28, (double)((float)i3 + f17), (double)(i4 + 1), d9, d13);
				tessellator5.AddVertexWithUV(d20, (double)(i3 + 0), (double)(i4 + 1), d9, d15);
				tessellator5.AddVertexWithUV(d20, (double)(i3 + 0), (double)(i4 + 0), d11, d15);
				tessellator5.AddVertexWithUV(d28, (double)((float)i3 + f17), (double)(i4 + 0), d11, d13);
				d9 = (double)((float)i7 / 256.0F);
				d11 = (double)(((float)i7 + 15.99F) / 256.0F);
				d13 = (double)((float)i8 / 256.0F);
				d15 = (double)(((float)i8 + 15.99F) / 256.0F);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17), d32, d9, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), d24, d9, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), d24, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17), d32, d11, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)i3 + f17), d30, d9, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), d22, d9, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), d22, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)i3 + f17), d30, d11, d13);
			}

			return true;
		}

		public virtual bool renderBlockRedstoneWire(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i7 = block1.getBlockTextureFromSideAndMetadata(1, i6);
			if (this.overrideBlockTexture >= 0)
			{
				i7 = this.overrideBlockTexture;
			}

			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f8 = 1.0F;
			float f9 = (float)i6 / 15.0F;
			float f10 = f9 * 0.6F + 0.4F;
			if (i6 == 0)
			{
				f10 = 0.3F;
			}

			float f11 = f9 * f9 * 0.7F - 0.5F;
			float f12 = f9 * f9 * 0.6F - 0.7F;
			if (f11 < 0.0F)
			{
				f11 = 0.0F;
			}

			if (f12 < 0.0F)
			{
				f12 = 0.0F;
			}

			tessellator5.setColorOpaque_F(f10, f11, f12);
			int i13 = (i7 & 15) << 4;
			int i14 = i7 & 240;
			double d15 = (double)((float)i13 / 256.0F);
			double d17 = (double)(((float)i13 + 15.99F) / 256.0F);
			double d19 = (double)((float)i14 / 256.0F);
			double d21 = (double)(((float)i14 + 15.99F) / 256.0F);
			bool z29 = BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2 - 1, i3, i4, 1) || !this.blockAccess.isBlockNormalCube(i2 - 1, i3, i4) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2 - 1, i3 - 1, i4, -1);
			bool z30 = BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2 + 1, i3, i4, 3) || !this.blockAccess.isBlockNormalCube(i2 + 1, i3, i4) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2 + 1, i3 - 1, i4, -1);
			bool z31 = BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2, i3, i4 - 1, 2) || !this.blockAccess.isBlockNormalCube(i2, i3, i4 - 1) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2, i3 - 1, i4 - 1, -1);
			bool z32 = BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2, i3, i4 + 1, 0) || !this.blockAccess.isBlockNormalCube(i2, i3, i4 + 1) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2, i3 - 1, i4 + 1, -1);
			if (!this.blockAccess.isBlockNormalCube(i2, i3 + 1, i4))
			{
				if (this.blockAccess.isBlockNormalCube(i2 - 1, i3, i4) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2 - 1, i3 + 1, i4, -1))
				{
					z29 = true;
				}

				if (this.blockAccess.isBlockNormalCube(i2 + 1, i3, i4) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2 + 1, i3 + 1, i4, -1))
				{
					z30 = true;
				}

				if (this.blockAccess.isBlockNormalCube(i2, i3, i4 - 1) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2, i3 + 1, i4 - 1, -1))
				{
					z31 = true;
				}

				if (this.blockAccess.isBlockNormalCube(i2, i3, i4 + 1) && BlockRedstoneWire.isPowerProviderOrWire(this.blockAccess, i2, i3 + 1, i4 + 1, -1))
				{
					z32 = true;
				}
			}

			float f34 = (float)(i2 + 0);
			float f35 = (float)(i2 + 1);
			float f36 = (float)(i4 + 0);
			float f37 = (float)(i4 + 1);
			sbyte b38 = 0;
			if ((z29 || z30) && !z31 && !z32)
			{
				b38 = 1;
			}

			if ((z31 || z32) && !z30 && !z29)
			{
				b38 = 2;
			}

			if (b38 != 0)
			{
				d15 = (double)((float)(i13 + 16) / 256.0F);
				d17 = (double)(((float)(i13 + 16) + 15.99F) / 256.0F);
				d19 = (double)((float)i14 / 256.0F);
				d21 = (double)(((float)i14 + 15.99F) / 256.0F);
			}

			if (b38 == 0)
			{
				if (!z29)
				{
					f34 += 0.3125F;
				}

				if (!z29)
				{
					d15 += 0.01953125D;
				}

				if (!z30)
				{
					f35 -= 0.3125F;
				}

				if (!z30)
				{
					d17 -= 0.01953125D;
				}

				if (!z31)
				{
					f36 += 0.3125F;
				}

				if (!z31)
				{
					d19 += 0.01953125D;
				}

				if (!z32)
				{
					f37 -= 0.3125F;
				}

				if (!z32)
				{
					d21 -= 0.01953125D;
				}

				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f37, d17, d21);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f36, d17, d19);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f36, d15, d19);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f37, d15, d21);
				tessellator5.setColorOpaque_F(f8, f8, f8);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f37, d17, d21 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f36, d17, d19 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f36, d15, d19 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f37, d15, d21 + 0.0625D);
			}
			else if (b38 == 1)
			{
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f37, d17, d21);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f36, d17, d19);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f36, d15, d19);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f37, d15, d21);
				tessellator5.setColorOpaque_F(f8, f8, f8);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f37, d17, d21 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f36, d17, d19 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f36, d15, d19 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f37, d15, d21 + 0.0625D);
			}
			else if (b38 == 2)
			{
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f37, d17, d21);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f36, d15, d21);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f36, d15, d19);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f37, d17, d19);
				tessellator5.setColorOpaque_F(f8, f8, f8);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f37, d17, d21 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f35, (double)i3 + 0.015625D, (double)f36, d15, d21 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f36, d15, d19 + 0.0625D);
				tessellator5.AddVertexWithUV((double)f34, (double)i3 + 0.015625D, (double)f37, d17, d19 + 0.0625D);
			}

			if (!this.blockAccess.isBlockNormalCube(i2, i3 + 1, i4))
			{
				d15 = (double)((float)(i13 + 16) / 256.0F);
				d17 = (double)(((float)(i13 + 16) + 15.99F) / 256.0F);
				d19 = (double)((float)i14 / 256.0F);
				d21 = (double)(((float)i14 + 15.99F) / 256.0F);
				if (this.blockAccess.isBlockNormalCube(i2 - 1, i3, i4) && this.blockAccess.getBlockId(i2 - 1, i3 + 1, i4) == Block.redstoneWire.blockID)
				{
					tessellator5.setColorOpaque_F(f8 * f10, f8 * f11, f8 * f12);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1), d17, d19);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)(i3 + 0), (double)(i4 + 1), d15, d19);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)(i3 + 0), (double)(i4 + 0), d15, d21);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 0), d17, d21);
					tessellator5.setColorOpaque_F(f8, f8, f8);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1), d17, d19 + 0.0625D);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)(i3 + 0), (double)(i4 + 1), d15, d19 + 0.0625D);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)(i3 + 0), (double)(i4 + 0), d15, d21 + 0.0625D);
					tessellator5.AddVertexWithUV((double)i2 + 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 0), d17, d21 + 0.0625D);
				}

				if (this.blockAccess.isBlockNormalCube(i2 + 1, i3, i4) && this.blockAccess.getBlockId(i2 + 1, i3 + 1, i4) == Block.redstoneWire.blockID)
				{
					tessellator5.setColorOpaque_F(f8 * f10, f8 * f11, f8 * f12);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)(i3 + 0), (double)(i4 + 1), d15, d21);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1), d17, d21);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 0), d17, d19);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)(i3 + 0), (double)(i4 + 0), d15, d19);
					tessellator5.setColorOpaque_F(f8, f8, f8);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)(i3 + 0), (double)(i4 + 1), d15, d21 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1), d17, d21 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 0), d17, d19 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 1) - 0.015625D, (double)(i3 + 0), (double)(i4 + 0), d15, d19 + 0.0625D);
				}

				if (this.blockAccess.isBlockNormalCube(i2, i3, i4 - 1) && this.blockAccess.getBlockId(i2, i3 + 1, i4 - 1) == Block.redstoneWire.blockID)
				{
					tessellator5.setColorOpaque_F(f8 * f10, f8 * f11, f8 * f12);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)i4 + 0.015625D, d15, d21);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 1) + 0.021875F), (double)i4 + 0.015625D, d17, d21);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 1) + 0.021875F), (double)i4 + 0.015625D, d17, d19);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)i4 + 0.015625D, d15, d19);
					tessellator5.setColorOpaque_F(f8, f8, f8);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)i4 + 0.015625D, d15, d21 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 1) + 0.021875F), (double)i4 + 0.015625D, d17, d21 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 1) + 0.021875F), (double)i4 + 0.015625D, d17, d19 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)i4 + 0.015625D, d15, d19 + 0.0625D);
				}

				if (this.blockAccess.isBlockNormalCube(i2, i3, i4 + 1) && this.blockAccess.getBlockId(i2, i3 + 1, i4 + 1) == Block.redstoneWire.blockID)
				{
					tessellator5.setColorOpaque_F(f8 * f10, f8 * f11, f8 * f12);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1) - 0.015625D, d17, d19);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)(i4 + 1) - 0.015625D, d15, d19);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)(i4 + 1) - 0.015625D, d15, d21);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1) - 0.015625D, d17, d21);
					tessellator5.setColorOpaque_F(f8, f8, f8);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1) - 0.015625D, d17, d19 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)(i4 + 1) - 0.015625D, d15, d19 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)(i4 + 1) - 0.015625D, d15, d21 + 0.0625D);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)((float)(i3 + 1) + 0.021875F), (double)(i4 + 1) - 0.015625D, d17, d21 + 0.0625D);
				}
			}

			return true;
		}

		public virtual bool renderBlockMinecartTrack(BlockRail blockRail1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i7 = blockRail1.getBlockTextureFromSideAndMetadata(0, i6);
			if (this.overrideBlockTexture >= 0)
			{
				i7 = this.overrideBlockTexture;
			}

			if (blockRail1.Powered)
			{
				i6 &= 7;
			}

			tessellator5.Brightness = blockRail1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			tessellator5.setColorOpaque_F(1.0F, 1.0F, 1.0F);
			int i8 = (i7 & 15) << 4;
			int i9 = i7 & 240;
			double d10 = (double)((float)i8 / 256.0F);
			double d12 = (double)(((float)i8 + 15.99F) / 256.0F);
			double d14 = (double)((float)i9 / 256.0F);
			double d16 = (double)(((float)i9 + 15.99F) / 256.0F);
			double d18 = 0.0625D;
			double d20 = (double)(i2 + 1);
			double d22 = (double)(i2 + 1);
			double d24 = (double)(i2 + 0);
			double d26 = (double)(i2 + 0);
			double d28 = (double)(i4 + 0);
			double d30 = (double)(i4 + 1);
			double d32 = (double)(i4 + 1);
			double d34 = (double)(i4 + 0);
			double d36 = (double)i3 + d18;
			double d38 = (double)i3 + d18;
			double d40 = (double)i3 + d18;
			double d42 = (double)i3 + d18;
			if (i6 != 1 && i6 != 2 && i6 != 3 && i6 != 7)
			{
				if (i6 == 8)
				{
					d20 = d22 = (double)(i2 + 0);
					d24 = d26 = (double)(i2 + 1);
					d28 = d34 = (double)(i4 + 1);
					d30 = d32 = (double)(i4 + 0);
				}
				else if (i6 == 9)
				{
					d20 = d26 = (double)(i2 + 0);
					d22 = d24 = (double)(i2 + 1);
					d28 = d30 = (double)(i4 + 0);
					d32 = d34 = (double)(i4 + 1);
				}
			}
			else
			{
				d20 = d26 = (double)(i2 + 1);
				d22 = d24 = (double)(i2 + 0);
				d28 = d30 = (double)(i4 + 1);
				d32 = d34 = (double)(i4 + 0);
			}

			if (i6 != 2 && i6 != 4)
			{
				if (i6 == 3 || i6 == 5)
				{
					++d38;
					++d40;
				}
			}
			else
			{
				++d36;
				++d42;
			}

			tessellator5.AddVertexWithUV(d20, d36, d28, d12, d14);
			tessellator5.AddVertexWithUV(d22, d38, d30, d12, d16);
			tessellator5.AddVertexWithUV(d24, d40, d32, d10, d16);
			tessellator5.AddVertexWithUV(d26, d42, d34, d10, d14);
			tessellator5.AddVertexWithUV(d26, d42, d34, d10, d14);
			tessellator5.AddVertexWithUV(d24, d40, d32, d10, d16);
			tessellator5.AddVertexWithUV(d22, d38, d30, d12, d16);
			tessellator5.AddVertexWithUV(d20, d36, d28, d12, d14);
			return true;
		}

		public virtual bool renderBlockLadder(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = block1.getBlockTextureFromSide(0);
			if (this.overrideBlockTexture >= 0)
			{
				i6 = this.overrideBlockTexture;
			}

			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f7 = 1.0F;
			tessellator5.setColorOpaque_F(f7, f7, f7);
			int i22 = (i6 & 15) << 4;
			int i8 = i6 & 240;
			double d9 = (double)((float)i22 / 256.0F);
			double d11 = (double)(((float)i22 + 15.99F) / 256.0F);
			double d13 = (double)((float)i8 / 256.0F);
			double d15 = (double)(((float)i8 + 15.99F) / 256.0F);
			int i17 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			double d18 = 0.0D;
			double d20 = (double)0.05F;
			if (i17 == 5)
			{
				tessellator5.AddVertexWithUV((double)i2 + d20, (double)(i3 + 1) + d18, (double)(i4 + 1) + d18, d9, d13);
				tessellator5.AddVertexWithUV((double)i2 + d20, (double)(i3 + 0) - d18, (double)(i4 + 1) + d18, d9, d15);
				tessellator5.AddVertexWithUV((double)i2 + d20, (double)(i3 + 0) - d18, (double)(i4 + 0) - d18, d11, d15);
				tessellator5.AddVertexWithUV((double)i2 + d20, (double)(i3 + 1) + d18, (double)(i4 + 0) - d18, d11, d13);
			}

			if (i17 == 4)
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d20, (double)(i3 + 0) - d18, (double)(i4 + 1) + d18, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d20, (double)(i3 + 1) + d18, (double)(i4 + 1) + d18, d11, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d20, (double)(i3 + 1) + d18, (double)(i4 + 0) - d18, d9, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d20, (double)(i3 + 0) - d18, (double)(i4 + 0) - d18, d9, d15);
			}

			if (i17 == 3)
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1) + d18, (double)(i3 + 0) - d18, (double)i4 + d20, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 1) + d18, (double)(i3 + 1) + d18, (double)i4 + d20, d11, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 0) - d18, (double)(i3 + 1) + d18, (double)i4 + d20, d9, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 0) - d18, (double)(i3 + 0) - d18, (double)i4 + d20, d9, d15);
			}

			if (i17 == 2)
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1) + d18, (double)(i3 + 1) + d18, (double)(i4 + 1) - d20, d9, d13);
				tessellator5.AddVertexWithUV((double)(i2 + 1) + d18, (double)(i3 + 0) - d18, (double)(i4 + 1) - d20, d9, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 0) - d18, (double)(i3 + 0) - d18, (double)(i4 + 1) - d20, d11, d15);
				tessellator5.AddVertexWithUV((double)(i2 + 0) - d18, (double)(i3 + 1) + d18, (double)(i4 + 1) - d20, d11, d13);
			}

			return true;
		}

		public virtual bool renderBlockVine(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = block1.getBlockTextureFromSide(0);
			if (this.overrideBlockTexture >= 0)
			{
				i6 = this.overrideBlockTexture;
			}

			float f7 = 1.0F;
			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			int i8 = block1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f9 = (float)(i8 >> 16 & 255) / 255.0F;
			float f10 = (float)(i8 >> 8 & 255) / 255.0F;
			float f11 = (float)(i8 & 255) / 255.0F;
			tessellator5.setColorOpaque_F(f7 * f9, f7 * f10, f7 * f11);
			i8 = (i6 & 15) << 4;
			int i21 = i6 & 240;
			double d22 = (double)((float)i8 / 256.0F);
			double d12 = (double)(((float)i8 + 15.99F) / 256.0F);
			double d14 = (double)((float)i21 / 256.0F);
			double d16 = (double)(((float)i21 + 15.99F) / 256.0F);
			double d18 = (double)0.05F;
			int i20 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			if ((i20 & 2) != 0)
			{
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 1), (double)(i4 + 1), d22, d14);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 0), (double)(i4 + 1), d22, d16);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 0), (double)(i4 + 0), d12, d16);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 1), (double)(i4 + 0), d12, d14);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 1), (double)(i4 + 0), d12, d14);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 0), (double)(i4 + 0), d12, d16);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 0), (double)(i4 + 1), d22, d16);
				tessellator5.AddVertexWithUV((double)i2 + d18, (double)(i3 + 1), (double)(i4 + 1), d22, d14);
			}

			if ((i20 & 8) != 0)
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 0), (double)(i4 + 1), d12, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 1), (double)(i4 + 1), d12, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 1), (double)(i4 + 0), d22, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 0), (double)(i4 + 0), d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 0), (double)(i4 + 0), d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 1), (double)(i4 + 0), d22, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 1), (double)(i4 + 1), d12, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1) - d18, (double)(i3 + 0), (double)(i4 + 1), d12, d16);
			}

			if ((i20 & 4) != 0)
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)i4 + d18, d12, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 1), (double)i4 + d18, d12, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 1), (double)i4 + d18, d22, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)i4 + d18, d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)i4 + d18, d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 1), (double)i4 + d18, d22, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 1), (double)i4 + d18, d12, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)i4 + d18, d12, d16);
			}

			if ((i20 & 1) != 0)
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 1), (double)(i4 + 1) - d18, d22, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)(i4 + 1) - d18, d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)(i4 + 1) - d18, d12, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 1), (double)(i4 + 1) - d18, d12, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 1), (double)(i4 + 1) - d18, d12, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 0), (double)(i4 + 1) - d18, d12, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 0), (double)(i4 + 1) - d18, d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 1), (double)(i4 + 1) - d18, d22, d14);
			}

			if (this.blockAccess.isBlockNormalCube(i2, i3 + 1, i4))
			{
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 1) - d18, (double)(i4 + 0), d22, d14);
				tessellator5.AddVertexWithUV((double)(i2 + 1), (double)(i3 + 1) - d18, (double)(i4 + 1), d22, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 1) - d18, (double)(i4 + 1), d12, d16);
				tessellator5.AddVertexWithUV((double)(i2 + 0), (double)(i3 + 1) - d18, (double)(i4 + 0), d12, d14);
			}

			return true;
		}

		public virtual bool renderBlockPane(BlockPane blockPane1, int i2, int i3, int i4)
		{
			int i5 = this.blockAccess.Height;
			Tessellator tessellator6 = Tessellator.instance;
			tessellator6.Brightness = blockPane1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f7 = 1.0F;
			int i8 = blockPane1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f9 = (float)(i8 >> 16 & 255) / 255.0F;
			float f10 = (float)(i8 >> 8 & 255) / 255.0F;
			float f11 = (float)(i8 & 255) / 255.0F;

			tessellator6.setColorOpaque_F(f7 * f9, f7 * f10, f7 * f11);
			bool z64 = false;
			bool z66 = false;
			int i65;
			int i67;
			int i68;
			if (this.overrideBlockTexture >= 0)
			{
				i65 = this.overrideBlockTexture;
				i67 = this.overrideBlockTexture;
			}
			else
			{
				i68 = this.blockAccess.getBlockMetadata(i2, i3, i4);
				i65 = blockPane1.getBlockTextureFromSideAndMetadata(0, i68);
				i67 = blockPane1.SideTextureIndex;
			}

			i68 = (i65 & 15) << 4;
			int i15 = i65 & 240;
			double d16 = (double)((float)i68 / 256.0F);
			double d18 = (double)(((float)i68 + 7.99F) / 256.0F);
			double d20 = (double)(((float)i68 + 15.99F) / 256.0F);
			double d22 = (double)((float)i15 / 256.0F);
			double d24 = (double)(((float)i15 + 15.99F) / 256.0F);
			int i26 = (i67 & 15) << 4;
			int i27 = i67 & 240;
			double d28 = (double)((float)(i26 + 7) / 256.0F);
			double d30 = (double)(((float)i26 + 8.99F) / 256.0F);
			double d32 = (double)((float)i27 / 256.0F);
			double d34 = (double)((float)(i27 + 8) / 256.0F);
			double d36 = (double)(((float)i27 + 15.99F) / 256.0F);
			double d38 = (double)i2;
			double d40 = (double)i2 + 0.5D;
			double d42 = (double)(i2 + 1);
			double d44 = (double)i4;
			double d46 = (double)i4 + 0.5D;
			double d48 = (double)(i4 + 1);
			double d50 = (double)i2 + 0.5D - 0.0625D;
			double d52 = (double)i2 + 0.5D + 0.0625D;
			double d54 = (double)i4 + 0.5D - 0.0625D;
			double d56 = (double)i4 + 0.5D + 0.0625D;
			bool z58 = blockPane1.canThisPaneConnectToThisBlockID(this.blockAccess.getBlockId(i2, i3, i4 - 1));
			bool z59 = blockPane1.canThisPaneConnectToThisBlockID(this.blockAccess.getBlockId(i2, i3, i4 + 1));
			bool z60 = blockPane1.canThisPaneConnectToThisBlockID(this.blockAccess.getBlockId(i2 - 1, i3, i4));
			bool z61 = blockPane1.canThisPaneConnectToThisBlockID(this.blockAccess.getBlockId(i2 + 1, i3, i4));
			bool z62 = blockPane1.shouldSideBeRendered(this.blockAccess, i2, i3 + 1, i4, 1);
			bool z63 = blockPane1.shouldSideBeRendered(this.blockAccess, i2, i3 - 1, i4, 0);
			if ((!z60 || !z61) && (z60 || z61 || z58 || z59))
			{
				if (z60 && !z61)
				{
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 1), d46, d16, d22);
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 0), d46, d16, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d18, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d18, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d16, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d16, d24);
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 0), d46, d18, d24);
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 1), d46, d18, d22);
					if (!z59 && !z58)
					{
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d56, d28, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d56, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d54, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d54, d30, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d54, d28, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d54, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d56, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d56, d30, d32);
					}

					if (z62 || i3 < i5 - 1 && this.blockAccess.isAirBlock(i2 - 1, i3 + 1, i4))
					{
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d34);
					}

					if (z63 || i3 > 1 && this.blockAccess.isAirBlock(i2 - 1, i3 - 1, i4))
					{
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d34);
					}
				}
				else if (!z60 && z61)
				{
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d18, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d18, d24);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 0), d46, d20, d24);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 1), d46, d20, d22);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 1), d46, d18, d22);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 0), d46, d18, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d20, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d20, d22);
					if (!z59 && !z58)
					{
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d54, d28, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d54, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d56, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d56, d30, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d56, d28, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d56, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d54, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d54, d30, d32);
					}

					if (z62 || i3 < i5 - 1 && this.blockAccess.isAirBlock(i2 + 1, i3 + 1, i4))
					{
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d32);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d54, d28, d32);
					}

					if (z63 || i3 > 1 && this.blockAccess.isAirBlock(i2 + 1, i3 - 1, i4))
					{
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d32);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d54, d28, d32);
					}
				}
			}
			else
			{
				tessellator6.AddVertexWithUV(d38, (double)(i3 + 1), d46, d16, d22);
				tessellator6.AddVertexWithUV(d38, (double)(i3 + 0), d46, d16, d24);
				tessellator6.AddVertexWithUV(d42, (double)(i3 + 0), d46, d20, d24);
				tessellator6.AddVertexWithUV(d42, (double)(i3 + 1), d46, d20, d22);
				tessellator6.AddVertexWithUV(d42, (double)(i3 + 1), d46, d16, d22);
				tessellator6.AddVertexWithUV(d42, (double)(i3 + 0), d46, d16, d24);
				tessellator6.AddVertexWithUV(d38, (double)(i3 + 0), d46, d20, d24);
				tessellator6.AddVertexWithUV(d38, (double)(i3 + 1), d46, d20, d22);
				if (z62)
				{
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d56, d30, d36);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d56, d30, d32);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d54, d28, d32);
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d54, d28, d36);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d56, d30, d36);
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d56, d30, d32);
					tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d54, d28, d32);
					tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d54, d28, d36);
				}
				else
				{
					if (i3 < i5 - 1 && this.blockAccess.isAirBlock(i2 - 1, i3 + 1, i4))
					{
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d38, (double)(i3 + 1) + 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d34);
					}

					if (i3 < i5 - 1 && this.blockAccess.isAirBlock(i2 + 1, i3 + 1, i4))
					{
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d32);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)(i3 + 1) + 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d42, (double)(i3 + 1) + 0.01D, d54, d28, d32);
					}
				}

				if (z63)
				{
					tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d56, d30, d36);
					tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d56, d30, d32);
					tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d54, d28, d32);
					tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d54, d28, d36);
					tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d56, d30, d36);
					tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d56, d30, d32);
					tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d54, d28, d32);
					tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d54, d28, d36);
				}
				else
				{
					if (i3 > 1 && this.blockAccess.isAirBlock(i2 - 1, i3 - 1, i4))
					{
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d56, d30, d36);
						tessellator6.AddVertexWithUV(d38, (double)i3 - 0.01D, d54, d28, d36);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d34);
					}

					if (i3 > 1 && this.blockAccess.isAirBlock(i2 + 1, i3 - 1, i4))
					{
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d32);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d56, d30, d32);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d56, d30, d34);
						tessellator6.AddVertexWithUV(d40, (double)i3 - 0.01D, d54, d28, d34);
						tessellator6.AddVertexWithUV(d42, (double)i3 - 0.01D, d54, d28, d32);
					}
				}
			}

			if ((!z58 || !z59) && (z60 || z61 || z58 || z59))
			{
				if (z58 && !z59)
				{
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d44, d16, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d44, d16, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d18, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d18, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d16, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d16, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d44, d18, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d44, d18, d22);
					if (!z61 && !z60)
					{
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d28, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 0), d46, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 0), d46, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d30, d32);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d28, d32);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 0), d46, d28, d36);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 0), d46, d30, d36);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d30, d32);
					}

					if (z62 || i3 < i5 - 1 && this.blockAccess.isAirBlock(i2, i3 + 1, i4 - 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d44, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d44, d28, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d44, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d44, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d28, d32);
					}

					if (z63 || i3 > 1 && this.blockAccess.isAirBlock(i2, i3 - 1, i4 - 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)i3, d44, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d44, d28, d32);
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)i3, d44, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d44, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d28, d32);
					}
				}
				else if (!z58 && z59)
				{
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d18, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d18, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d48, d20, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d48, d20, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d48, d18, d22);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d48, d18, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d46, d20, d24);
					tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d46, d20, d22);
					if (!z61 && !z60)
					{
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d28, d32);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 0), d46, d28, d36);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 0), d46, d30, d36);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d28, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 0), d46, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 0), d46, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d30, d32);
					}

					if (z62 || i3 < i5 - 1 && this.blockAccess.isAirBlock(i2, i3 + 1, i4 + 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d48, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d48, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d30, d34);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d48, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d48, d30, d34);
					}

					if (z63 || i3 > 1 && this.blockAccess.isAirBlock(i2, i3 - 1, i4 + 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)i3, d48, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d48, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d30, d34);
						tessellator6.AddVertexWithUV(d50, (double)i3, d48, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d48, d30, d34);
					}
				}
			}
			else
			{
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d48, d16, d22);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d48, d16, d24);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d44, d20, d24);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d44, d20, d22);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d44, d16, d22);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d44, d16, d24);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 0), d48, d20, d24);
				tessellator6.AddVertexWithUV(d40, (double)(i3 + 1), d48, d20, d22);
				if (z62)
				{
					tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d48, d30, d36);
					tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d44, d30, d32);
					tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d44, d28, d32);
					tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d48, d28, d36);
					tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d44, d30, d36);
					tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d48, d30, d32);
					tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d48, d28, d32);
					tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d44, d28, d36);
				}
				else
				{
					if (i3 < i5 - 1 && this.blockAccess.isAirBlock(i2, i3 + 1, i4 - 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d44, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d44, d28, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d44, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d44, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d28, d32);
					}

					if (i3 < i5 - 1 && this.blockAccess.isAirBlock(i2, i3 + 1, i4 + 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d48, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d48, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d30, d34);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d48, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)(i3 + 1), d46, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d46, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)(i3 + 1), d48, d30, d34);
					}
				}

				if (z63)
				{
					tessellator6.AddVertexWithUV(d52, (double)i3, d48, d30, d36);
					tessellator6.AddVertexWithUV(d52, (double)i3, d44, d30, d32);
					tessellator6.AddVertexWithUV(d50, (double)i3, d44, d28, d32);
					tessellator6.AddVertexWithUV(d50, (double)i3, d48, d28, d36);
					tessellator6.AddVertexWithUV(d52, (double)i3, d44, d30, d36);
					tessellator6.AddVertexWithUV(d52, (double)i3, d48, d30, d32);
					tessellator6.AddVertexWithUV(d50, (double)i3, d48, d28, d32);
					tessellator6.AddVertexWithUV(d50, (double)i3, d44, d28, d36);
				}
				else
				{
					if (i3 > 1 && this.blockAccess.isAirBlock(i2, i3 - 1, i4 - 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)i3, d44, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d44, d28, d32);
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d30, d32);
						tessellator6.AddVertexWithUV(d50, (double)i3, d44, d30, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d44, d28, d34);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d28, d32);
					}

					if (i3 > 1 && this.blockAccess.isAirBlock(i2, i3 - 1, i4 + 1))
					{
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)i3, d48, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d48, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d30, d34);
						tessellator6.AddVertexWithUV(d50, (double)i3, d48, d28, d34);
						tessellator6.AddVertexWithUV(d50, (double)i3, d46, d28, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d46, d30, d36);
						tessellator6.AddVertexWithUV(d52, (double)i3, d48, d30, d34);
					}
				}
			}

			return true;
		}

		public virtual bool renderCrossedSquares(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f6 = 1.0F;
			int i7 = block1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f8 = (float)(i7 >> 16 & 255) / 255.0F;
			float f9 = (float)(i7 >> 8 & 255) / 255.0F;
			float f10 = (float)(i7 & 255) / 255.0F;

			tessellator5.setColorOpaque_F(f6 * f8, f6 * f9, f6 * f10);
			double d19 = (double)i2;
			double d20 = (double)i3;
			double d15 = (double)i4;
			if (block1 == Block.tallGrass)
			{
				long j17 = (long)(i2 * 3129871) ^ (long)i4 * 116129781L ^ (long)i3;
				j17 = j17 * j17 * 42317861L + j17 * 11L;
				d19 += ((double)((float)(j17 >> 16 & 15L) / 15.0F) - 0.5D) * 0.5D;
				d20 += ((double)((float)(j17 >> 20 & 15L) / 15.0F) - 1.0D) * 0.2D;
				d15 += ((double)((float)(j17 >> 24 & 15L) / 15.0F) - 0.5D) * 0.5D;
			}

			this.drawCrossedSquares(block1, this.blockAccess.getBlockMetadata(i2, i3, i4), d19, d20, d15);
			return true;
		}

		public virtual bool renderBlockStem(Block block1, int i2, int i3, int i4)
		{
			BlockStem blockStem5 = (BlockStem)block1;
			Tessellator tessellator6 = Tessellator.instance;
			tessellator6.Brightness = blockStem5.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f7 = 1.0F;
			int i8 = blockStem5.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f9 = (float)(i8 >> 16 & 255) / 255.0F;
			float f10 = (float)(i8 >> 8 & 255) / 255.0F;
			float f11 = (float)(i8 & 255) / 255.0F;
            
			tessellator6.setColorOpaque_F(f7 * f9, f7 * f10, f7 * f11);
			blockStem5.setBlockBoundsBasedOnState(this.blockAccess, i2, i3, i4);
			int i15 = blockStem5.func_35296_f(this.blockAccess, i2, i3, i4);
			if (i15 < 0)
			{
				this.renderBlockStemSmall(blockStem5, this.blockAccess.getBlockMetadata(i2, i3, i4), blockStem5.maxY, (double)i2, (double)i3, (double)i4);
			}
			else
			{
				this.renderBlockStemSmall(blockStem5, this.blockAccess.getBlockMetadata(i2, i3, i4), 0.5D, (double)i2, (double)i3, (double)i4);
				this.renderBlockStemBig(blockStem5, this.blockAccess.getBlockMetadata(i2, i3, i4), i15, blockStem5.maxY, (double)i2, (double)i3, (double)i4);
			}

			return true;
		}

		public virtual bool renderBlockCrops(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			tessellator5.setColorOpaque_F(1.0F, 1.0F, 1.0F);
			this.renderBlockCropsImpl(block1, this.blockAccess.getBlockMetadata(i2, i3, i4), (double)i2, (double)((float)i3 - 0.0625F), (double)i4);
			return true;
		}

		public virtual void renderTorchAtAngle(Block block1, double d2, double d4, double d6, double d8, double d10)
		{
			Tessellator tessellator12 = Tessellator.instance;
			int i13 = block1.getBlockTextureFromSide(0);
			if (this.overrideBlockTexture >= 0)
			{
				i13 = this.overrideBlockTexture;
			}

			int i14 = (i13 & 15) << 4;
			int i15 = i13 & 240;
			float f16 = (float)i14 / 256.0F;
			float f17 = ((float)i14 + 15.99F) / 256.0F;
			float f18 = (float)i15 / 256.0F;
			float f19 = ((float)i15 + 15.99F) / 256.0F;
			double d20 = (double)f16 + 7.0D / 256D;
			double d22 = (double)f18 + 6.0D / 256D;
			double d24 = (double)f16 + 9.0D / 256D;
			double d26 = (double)f18 + 8.0D / 256D;
			d2 += 0.5D;
			d6 += 0.5D;
			double d28 = d2 - 0.5D;
			double d30 = d2 + 0.5D;
			double d32 = d6 - 0.5D;
			double d34 = d6 + 0.5D;
			double d36 = 0.0625D;
			double d38 = 0.625D;
			tessellator12.AddVertexWithUV(d2 + d8 * (1.0D - d38) - d36, d4 + d38, d6 + d10 * (1.0D - d38) - d36, d20, d22);
			tessellator12.AddVertexWithUV(d2 + d8 * (1.0D - d38) - d36, d4 + d38, d6 + d10 * (1.0D - d38) + d36, d20, d26);
			tessellator12.AddVertexWithUV(d2 + d8 * (1.0D - d38) + d36, d4 + d38, d6 + d10 * (1.0D - d38) + d36, d24, d26);
			tessellator12.AddVertexWithUV(d2 + d8 * (1.0D - d38) + d36, d4 + d38, d6 + d10 * (1.0D - d38) - d36, d24, d22);
			tessellator12.AddVertexWithUV(d2 - d36, d4 + 1.0D, d32, (double)f16, (double)f18);
			tessellator12.AddVertexWithUV(d2 - d36 + d8, d4 + 0.0D, d32 + d10, (double)f16, (double)f19);
			tessellator12.AddVertexWithUV(d2 - d36 + d8, d4 + 0.0D, d34 + d10, (double)f17, (double)f19);
			tessellator12.AddVertexWithUV(d2 - d36, d4 + 1.0D, d34, (double)f17, (double)f18);
			tessellator12.AddVertexWithUV(d2 + d36, d4 + 1.0D, d34, (double)f16, (double)f18);
			tessellator12.AddVertexWithUV(d2 + d8 + d36, d4 + 0.0D, d34 + d10, (double)f16, (double)f19);
			tessellator12.AddVertexWithUV(d2 + d8 + d36, d4 + 0.0D, d32 + d10, (double)f17, (double)f19);
			tessellator12.AddVertexWithUV(d2 + d36, d4 + 1.0D, d32, (double)f17, (double)f18);
			tessellator12.AddVertexWithUV(d28, d4 + 1.0D, d6 + d36, (double)f16, (double)f18);
			tessellator12.AddVertexWithUV(d28 + d8, d4 + 0.0D, d6 + d36 + d10, (double)f16, (double)f19);
			tessellator12.AddVertexWithUV(d30 + d8, d4 + 0.0D, d6 + d36 + d10, (double)f17, (double)f19);
			tessellator12.AddVertexWithUV(d30, d4 + 1.0D, d6 + d36, (double)f17, (double)f18);
			tessellator12.AddVertexWithUV(d30, d4 + 1.0D, d6 - d36, (double)f16, (double)f18);
			tessellator12.AddVertexWithUV(d30 + d8, d4 + 0.0D, d6 - d36 + d10, (double)f16, (double)f19);
			tessellator12.AddVertexWithUV(d28 + d8, d4 + 0.0D, d6 - d36 + d10, (double)f17, (double)f19);
			tessellator12.AddVertexWithUV(d28, d4 + 1.0D, d6 - d36, (double)f17, (double)f18);
		}

		public virtual void drawCrossedSquares(Block block1, int i2, double d3, double d5, double d7)
		{
			Tessellator tessellator9 = Tessellator.instance;
			int i10 = block1.getBlockTextureFromSideAndMetadata(0, i2);
			if (this.overrideBlockTexture >= 0)
			{
				i10 = this.overrideBlockTexture;
			}

			int i11 = (i10 & 15) << 4;
			int i12 = i10 & 240;
			double d13 = (double)((float)i11 / 256.0F);
			double d15 = (double)(((float)i11 + 15.99F) / 256.0F);
			double d17 = (double)((float)i12 / 256.0F);
			double d19 = (double)(((float)i12 + 15.99F) / 256.0F);
			double d21 = d3 + 0.5D - 0.45D;
			double d23 = d3 + 0.5D + 0.45D;
			double d25 = d7 + 0.5D - 0.45D;
			double d27 = d7 + 0.5D + 0.45D;
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d25, d13, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d25, d13, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d27, d15, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d27, d15, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d27, d13, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d27, d13, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d25, d15, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d25, d15, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d27, d13, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d27, d13, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d25, d15, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d25, d15, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d25, d13, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d25, d13, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d27, d15, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d27, d15, d17);
		}

		public virtual void renderBlockStemSmall(Block block1, int i2, double d3, double d5, double d7, double d9)
		{
			Tessellator tessellator11 = Tessellator.instance;
			int i12 = block1.getBlockTextureFromSideAndMetadata(0, i2);
			if (this.overrideBlockTexture >= 0)
			{
				i12 = this.overrideBlockTexture;
			}

			int i13 = (i12 & 15) << 4;
			int i14 = i12 & 240;
			double d15 = (double)((float)i13 / 256.0F);
			double d17 = (double)(((float)i13 + 15.99F) / 256.0F);
			double d19 = (double)((float)i14 / 256.0F);
			double d21 = ((double)i14 + 15.989999771118164D * d3) / 256.0D;
			double d23 = d5 + 0.5D - (double)0.45F;
			double d25 = d5 + 0.5D + (double)0.45F;
			double d27 = d9 + 0.5D - (double)0.45F;
			double d29 = d9 + 0.5D + (double)0.45F;
			tessellator11.AddVertexWithUV(d23, d7 + d3, d27, d15, d19);
			tessellator11.AddVertexWithUV(d23, d7 + 0.0D, d27, d15, d21);
			tessellator11.AddVertexWithUV(d25, d7 + 0.0D, d29, d17, d21);
			tessellator11.AddVertexWithUV(d25, d7 + d3, d29, d17, d19);
			tessellator11.AddVertexWithUV(d25, d7 + d3, d29, d15, d19);
			tessellator11.AddVertexWithUV(d25, d7 + 0.0D, d29, d15, d21);
			tessellator11.AddVertexWithUV(d23, d7 + 0.0D, d27, d17, d21);
			tessellator11.AddVertexWithUV(d23, d7 + d3, d27, d17, d19);
			tessellator11.AddVertexWithUV(d23, d7 + d3, d29, d15, d19);
			tessellator11.AddVertexWithUV(d23, d7 + 0.0D, d29, d15, d21);
			tessellator11.AddVertexWithUV(d25, d7 + 0.0D, d27, d17, d21);
			tessellator11.AddVertexWithUV(d25, d7 + d3, d27, d17, d19);
			tessellator11.AddVertexWithUV(d25, d7 + d3, d27, d15, d19);
			tessellator11.AddVertexWithUV(d25, d7 + 0.0D, d27, d15, d21);
			tessellator11.AddVertexWithUV(d23, d7 + 0.0D, d29, d17, d21);
			tessellator11.AddVertexWithUV(d23, d7 + d3, d29, d17, d19);
		}

		public virtual bool renderBlockLilyPad(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = block1.blockIndexInTexture;
			if (this.overrideBlockTexture >= 0)
			{
				i6 = this.overrideBlockTexture;
			}

			int i7 = (i6 & 15) << 4;
			int i8 = i6 & 240;
			float f9 = 0.015625F;
			double d10 = (double)((float)i7 / 256.0F);
			double d12 = (double)(((float)i7 + 15.99F) / 256.0F);
			double d14 = (double)((float)i8 / 256.0F);
			double d16 = (double)(((float)i8 + 15.99F) / 256.0F);
			long j18 = (long)(i2 * 3129871) ^ (long)i4 * 116129781L ^ (long)i3;
			j18 = j18 * j18 * 42317861L + j18 * 11L;
			int i20 = (int)(j18 >> 16 & 3L);
			tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			float f21 = (float)i2 + 0.5F;
			float f22 = (float)i4 + 0.5F;
			float f23 = (float)(i20 & 1) * 0.5F * (float)(1 - i20 / 2 % 2 * 2);
			float f24 = (float)(i20 + 1 & 1) * 0.5F * (float)(1 - (i20 + 1) / 2 % 2 * 2);
			tessellator5.ColorOpaque_I = block1.BlockColor;
			tessellator5.AddVertexWithUV((double)(f21 + f23 - f24), (double)((float)i3 + f9), (double)(f22 + f23 + f24), d10, d14);
			tessellator5.AddVertexWithUV((double)(f21 + f23 + f24), (double)((float)i3 + f9), (double)(f22 - f23 + f24), d12, d14);
			tessellator5.AddVertexWithUV((double)(f21 - f23 + f24), (double)((float)i3 + f9), (double)(f22 - f23 - f24), d12, d16);
			tessellator5.AddVertexWithUV((double)(f21 - f23 - f24), (double)((float)i3 + f9), (double)(f22 + f23 - f24), d10, d16);
			tessellator5.ColorOpaque_I = (block1.BlockColor & 16711422) >> 1;
			tessellator5.AddVertexWithUV((double)(f21 - f23 - f24), (double)((float)i3 + f9), (double)(f22 + f23 - f24), d10, d16);
			tessellator5.AddVertexWithUV((double)(f21 - f23 + f24), (double)((float)i3 + f9), (double)(f22 - f23 - f24), d12, d16);
			tessellator5.AddVertexWithUV((double)(f21 + f23 + f24), (double)((float)i3 + f9), (double)(f22 - f23 + f24), d12, d14);
			tessellator5.AddVertexWithUV((double)(f21 + f23 - f24), (double)((float)i3 + f9), (double)(f22 + f23 + f24), d10, d14);
			return true;
		}

		public virtual void renderBlockStemBig(Block block1, int i2, int i3, double d4, double d6, double d8, double d10)
		{
			Tessellator tessellator12 = Tessellator.instance;
			int i13 = block1.getBlockTextureFromSideAndMetadata(0, i2) + 16;
			if (this.overrideBlockTexture >= 0)
			{
				i13 = this.overrideBlockTexture;
			}

			int i14 = (i13 & 15) << 4;
			int i15 = i13 & 240;
			double d16 = (double)((float)i14 / 256.0F);
			double d18 = (double)(((float)i14 + 15.99F) / 256.0F);
			double d20 = (double)((float)i15 / 256.0F);
			double d22 = ((double)i15 + 15.989999771118164D * d4) / 256.0D;
			double d24 = d6 + 0.5D - 0.5D;
			double d26 = d6 + 0.5D + 0.5D;
			double d28 = d10 + 0.5D - 0.5D;
			double d30 = d10 + 0.5D + 0.5D;
			double d32 = d6 + 0.5D;
			double d34 = d10 + 0.5D;
			if ((i3 + 1) / 2 % 2 == 1)
			{
				double d36 = d18;
				d18 = d16;
				d16 = d36;
			}

			if (i3 < 2)
			{
				tessellator12.AddVertexWithUV(d24, d8 + d4, d34, d16, d20);
				tessellator12.AddVertexWithUV(d24, d8 + 0.0D, d34, d16, d22);
				tessellator12.AddVertexWithUV(d26, d8 + 0.0D, d34, d18, d22);
				tessellator12.AddVertexWithUV(d26, d8 + d4, d34, d18, d20);
				tessellator12.AddVertexWithUV(d26, d8 + d4, d34, d18, d20);
				tessellator12.AddVertexWithUV(d26, d8 + 0.0D, d34, d18, d22);
				tessellator12.AddVertexWithUV(d24, d8 + 0.0D, d34, d16, d22);
				tessellator12.AddVertexWithUV(d24, d8 + d4, d34, d16, d20);
			}
			else
			{
				tessellator12.AddVertexWithUV(d32, d8 + d4, d30, d16, d20);
				tessellator12.AddVertexWithUV(d32, d8 + 0.0D, d30, d16, d22);
				tessellator12.AddVertexWithUV(d32, d8 + 0.0D, d28, d18, d22);
				tessellator12.AddVertexWithUV(d32, d8 + d4, d28, d18, d20);
				tessellator12.AddVertexWithUV(d32, d8 + d4, d28, d18, d20);
				tessellator12.AddVertexWithUV(d32, d8 + 0.0D, d28, d18, d22);
				tessellator12.AddVertexWithUV(d32, d8 + 0.0D, d30, d16, d22);
				tessellator12.AddVertexWithUV(d32, d8 + d4, d30, d16, d20);
			}

		}

		public virtual void renderBlockCropsImpl(Block block1, int i2, double d3, double d5, double d7)
		{
			Tessellator tessellator9 = Tessellator.instance;
			int i10 = block1.getBlockTextureFromSideAndMetadata(0, i2);
			if (this.overrideBlockTexture >= 0)
			{
				i10 = this.overrideBlockTexture;
			}

			int i11 = (i10 & 15) << 4;
			int i12 = i10 & 240;
			double d13 = (double)((float)i11 / 256.0F);
			double d15 = (double)(((float)i11 + 15.99F) / 256.0F);
			double d17 = (double)((float)i12 / 256.0F);
			double d19 = (double)(((float)i12 + 15.99F) / 256.0F);
			double d21 = d3 + 0.5D - 0.25D;
			double d23 = d3 + 0.5D + 0.25D;
			double d25 = d7 + 0.5D - 0.5D;
			double d27 = d7 + 0.5D + 0.5D;
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d25, d13, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d25, d13, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d27, d15, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d27, d15, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d27, d13, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d27, d13, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d25, d15, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d25, d15, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d27, d13, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d27, d13, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d25, d15, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d25, d15, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d25, d13, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d25, d13, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d27, d15, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d27, d15, d17);
			d21 = d3 + 0.5D - 0.5D;
			d23 = d3 + 0.5D + 0.5D;
			d25 = d7 + 0.5D - 0.25D;
			d27 = d7 + 0.5D + 0.25D;
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d25, d13, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d25, d13, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d25, d15, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d25, d15, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d25, d13, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d25, d13, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d25, d15, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d25, d15, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d27, d13, d17);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d27, d13, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d27, d15, d19);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d27, d15, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 1.0D, d27, d13, d17);
			tessellator9.AddVertexWithUV(d21, d5 + 0.0D, d27, d13, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 0.0D, d27, d15, d19);
			tessellator9.AddVertexWithUV(d23, d5 + 1.0D, d27, d15, d17);
		}

		public virtual bool renderBlockFluids(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			int i6 = block1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f7 = (float)(i6 >> 16 & 255) / 255.0F;
			float f8 = (float)(i6 >> 8 & 255) / 255.0F;
			float f9 = (float)(i6 & 255) / 255.0F;
			bool z10 = block1.shouldSideBeRendered(this.blockAccess, i2, i3 + 1, i4, 1);
			bool z11 = block1.shouldSideBeRendered(this.blockAccess, i2, i3 - 1, i4, 0);
			bool[] z12 = new bool[]{block1.shouldSideBeRendered(this.blockAccess, i2, i3, i4 - 1, 2), block1.shouldSideBeRendered(this.blockAccess, i2, i3, i4 + 1, 3), block1.shouldSideBeRendered(this.blockAccess, i2 - 1, i3, i4, 4), block1.shouldSideBeRendered(this.blockAccess, i2 + 1, i3, i4, 5)};
			if (!z10 && !z11 && !z12[0] && !z12[1] && !z12[2] && !z12[3])
			{
				return false;
			}
			else
			{
				bool z13 = false;
				float f14 = 0.5F;
				float f15 = 1.0F;
				float f16 = 0.8F;
				float f17 = 0.6F;
				double d18 = 0.0D;
				double d20 = 1.0D;
				Material material22 = block1.blockMaterial;
				int i23 = this.blockAccess.getBlockMetadata(i2, i3, i4);
				double d24 = (double)this.getFluidHeight(i2, i3, i4, material22);
				double d26 = (double)this.getFluidHeight(i2, i3, i4 + 1, material22);
				double d28 = (double)this.getFluidHeight(i2 + 1, i3, i4 + 1, material22);
				double d30 = (double)this.getFluidHeight(i2 + 1, i3, i4, material22);
				double d32 = 0.0010000000474974513D;
				int i34;
				int i37;
				if (this.renderAllFaces || z10)
				{
					z13 = true;
					i34 = block1.getBlockTextureFromSideAndMetadata(1, i23);
					float f35 = (float)BlockFluid.func_293_a(this.blockAccess, i2, i3, i4, material22);
					if (f35 > -999.0F)
					{
						i34 = block1.getBlockTextureFromSideAndMetadata(2, i23);
					}

					d24 -= d32;
					d26 -= d32;
					d28 -= d32;
					d30 -= d32;
					int i36 = (i34 & 15) << 4;
					i37 = i34 & 240;
					double d38 = ((double)i36 + 8.0D) / 256.0D;
					double d40 = ((double)i37 + 8.0D) / 256.0D;
					if (f35 < -999.0F)
					{
						f35 = 0.0F;
					}
					else
					{
						d38 = (double)((float)(i36 + 16) / 256.0F);
						d40 = (double)((float)(i37 + 16) / 256.0F);
					}

					double d42 = (double)(MathHelper.sin(f35) * 8.0F) / 256.0D;
					double d44 = (double)(MathHelper.cos(f35) * 8.0F) / 256.0D;
					tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
					float f46 = 1.0F;
					tessellator5.setColorOpaque_F(f15 * f46 * f7, f15 * f46 * f8, f15 * f46 * f9);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)i3 + d24, (double)(i4 + 0), d38 - d44 - d42, d40 - d44 + d42);
					tessellator5.AddVertexWithUV((double)(i2 + 0), (double)i3 + d26, (double)(i4 + 1), d38 - d44 + d42, d40 + d44 + d42);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)i3 + d28, (double)(i4 + 1), d38 + d44 + d42, d40 + d44 - d42);
					tessellator5.AddVertexWithUV((double)(i2 + 1), (double)i3 + d30, (double)(i4 + 0), d38 + d44 - d42, d40 - d44 - d42);
				}

				if (this.renderAllFaces || z11)
				{
					tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3 - 1, i4);
					float f64 = 1.0F;
					tessellator5.setColorOpaque_F(f14 * f64, f14 * f64, f14 * f64);
					this.renderBottomFace(block1, (double)i2, (double)i3 + d32, (double)i4, block1.getBlockTextureFromSide(0));
					z13 = true;
				}

				for (i34 = 0; i34 < 4; ++i34)
				{
					int i65 = i2;
					i37 = i4;
					if (i34 == 0)
					{
						i37 = i4 - 1;
					}

					if (i34 == 1)
					{
						++i37;
					}

					if (i34 == 2)
					{
						i65 = i2 - 1;
					}

					if (i34 == 3)
					{
						++i65;
					}

					int i66 = block1.getBlockTextureFromSideAndMetadata(i34 + 2, i23);
					int i39 = (i66 & 15) << 4;
					int i67 = i66 & 240;
					if (this.renderAllFaces || z12[i34])
					{
						double d41;
						double d43;
						double d45;
						double d47;
						double d49;
						double d51;
						if (i34 == 0)
						{
							d41 = d24;
							d43 = d30;
							d45 = (double)i2;
							d49 = (double)(i2 + 1);
							d47 = (double)i4 + d32;
							d51 = (double)i4 + d32;
						}
						else if (i34 == 1)
						{
							d41 = d28;
							d43 = d26;
							d45 = (double)(i2 + 1);
							d49 = (double)i2;
							d47 = (double)(i4 + 1) - d32;
							d51 = (double)(i4 + 1) - d32;
						}
						else if (i34 == 2)
						{
							d41 = d26;
							d43 = d24;
							d45 = (double)i2 + d32;
							d49 = (double)i2 + d32;
							d47 = (double)(i4 + 1);
							d51 = (double)i4;
						}
						else
						{
							d41 = d30;
							d43 = d28;
							d45 = (double)(i2 + 1) - d32;
							d49 = (double)(i2 + 1) - d32;
							d47 = (double)i4;
							d51 = (double)(i4 + 1);
						}

						z13 = true;
						double d53 = (double)((float)(i39 + 0) / 256.0F);
						double d55 = ((double)(i39 + 16) - 0.01D) / 256.0D;
						double d57 = ((double)i67 + (1.0D - d41) * 16.0D) / 256.0D;
						double d59 = ((double)i67 + (1.0D - d43) * 16.0D) / 256.0D;
						double d61 = ((double)(i67 + 16) - 0.01D) / 256.0D;
						tessellator5.Brightness = block1.GetMixedBrightnessForBlock(this.blockAccess, i65, i3, i37);
						float f63 = 1.0F;
						if (i34 < 2)
						{
							f63 *= f16;
						}
						else
						{
							f63 *= f17;
						}

						tessellator5.setColorOpaque_F(f15 * f63 * f7, f15 * f63 * f8, f15 * f63 * f9);
						tessellator5.AddVertexWithUV(d45, (double)i3 + d41, d47, d53, d57);
						tessellator5.AddVertexWithUV(d49, (double)i3 + d43, d51, d55, d59);
						tessellator5.AddVertexWithUV(d49, (double)(i3 + 0), d51, d55, d61);
						tessellator5.AddVertexWithUV(d45, (double)(i3 + 0), d47, d53, d61);
					}
				}

				block1.minY = d18;
				block1.maxY = d20;
				return z13;
			}
		}

		private float getFluidHeight(int i1, int i2, int i3, Material material4)
		{
			int i5 = 0;
			float f6 = 0.0F;

			for (int i7 = 0; i7 < 4; ++i7)
			{
				int i8 = i1 - (i7 & 1);
				int i10 = i3 - (i7 >> 1 & 1);
				if (this.blockAccess.getBlockMaterial(i8, i2 + 1, i10) == material4)
				{
					return 1.0F;
				}

				Material material11 = this.blockAccess.getBlockMaterial(i8, i2, i10);
				if (material11 != material4)
				{
					if (!material11.Solid)
					{
						++f6;
						++i5;
					}
				}
				else
				{
					int i12 = this.blockAccess.getBlockMetadata(i8, i2, i10);
					if (i12 >= 8 || i12 == 0)
					{
						f6 += BlockFluid.getFluidHeightPercent(i12) * 10.0F;
						i5 += 10;
					}

					f6 += BlockFluid.getFluidHeightPercent(i12);
					++i5;
				}
			}

			return 1.0F - f6 / (float)i5;
		}

		public virtual void renderBlockFallingSand(Block block1, World world2, int i3, int i4, int i5)
		{
			float f6 = 0.5F;
			float f7 = 1.0F;
			float f8 = 0.8F;
			float f9 = 0.6F;
			Tessellator tessellator10 = Tessellator.instance;
			tessellator10.startDrawingQuads();
			tessellator10.Brightness = block1.GetMixedBrightnessForBlock(world2, i3, i4, i5);
			float f11 = 1.0F;
			float f12 = 1.0F;
			if (f12 < f11)
			{
				f12 = f11;
			}

			tessellator10.setColorOpaque_F(f6 * f12, f6 * f12, f6 * f12);
			this.renderBottomFace(block1, -0.5D, -0.5D, -0.5D, block1.getBlockTextureFromSide(0));
			f12 = 1.0F;
			if (f12 < f11)
			{
				f12 = f11;
			}

			tessellator10.setColorOpaque_F(f7 * f12, f7 * f12, f7 * f12);
			this.renderTopFace(block1, -0.5D, -0.5D, -0.5D, block1.getBlockTextureFromSide(1));
			f12 = 1.0F;
			if (f12 < f11)
			{
				f12 = f11;
			}

			tessellator10.setColorOpaque_F(f8 * f12, f8 * f12, f8 * f12);
			this.renderEastFace(block1, -0.5D, -0.5D, -0.5D, block1.getBlockTextureFromSide(2));
			f12 = 1.0F;
			if (f12 < f11)
			{
				f12 = f11;
			}

			tessellator10.setColorOpaque_F(f8 * f12, f8 * f12, f8 * f12);
			this.renderWestFace(block1, -0.5D, -0.5D, -0.5D, block1.getBlockTextureFromSide(3));
			f12 = 1.0F;
			if (f12 < f11)
			{
				f12 = f11;
			}

			tessellator10.setColorOpaque_F(f9 * f12, f9 * f12, f9 * f12);
			this.renderNorthFace(block1, -0.5D, -0.5D, -0.5D, block1.getBlockTextureFromSide(4));
			f12 = 1.0F;
			if (f12 < f11)
			{
				f12 = f11;
			}

			tessellator10.setColorOpaque_F(f9 * f12, f9 * f12, f9 * f12);
			this.renderSouthFace(block1, -0.5D, -0.5D, -0.5D, block1.getBlockTextureFromSide(5));
			tessellator10.DrawImmediate();
		}

		public virtual bool renderStandardBlock(Block block1, int x, int y, int z)
		{
			int blockColor = block1.colorMultiplier(this.blockAccess, x, y, z);
			float r = (float)(blockColor >> 16 & 255) / 255.0F;
			float g = (float)(blockColor >> 8 & 255) / 255.0F;
			float b = (float)(blockColor & 255) / 255.0F;
            
            return Minecraft.AmbientOcclusionEnabled && Block.lightValue[block1.blockID] == 0 ? this.RenderStandardBlockWithAmbientOcclusion(block1, x, y, z, r, g, b) : this.renderStandardBlockWithColorMultiplier(block1, x, y, z, r, g, b);
        }

		private static string section1 = "section1";
        private static string section2 = "section2";
        private static string section3 = "section3";
        private static string section4 = "section4";
        private static string section5 = "section5";
        private static string section6 = "section6";
        private static string section7 = "section7";
        private static string section8 = "section8";

        /// <summary>
		/// Queues all the necessary vertex information into the tessellator for a given block in the world. This method provides per vertex light data
		/// as opposed to the per face lighting that is applied when smooth lighting is disabled.
		/// </summary>
		/// <param name="block"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns>whether or not anything was drawn.</returns>
        public virtual bool RenderStandardBlockWithAmbientOcclusion(Block block, int x, int y, int z, float r, float g, float b)
		{
			enableAO = true;
			bool facesDrawn = false;
			float topLeftBrightness = lightValueOwn;
			float bottomLeftBrightness = lightValueOwn;
			float bottomRightBrightness = lightValueOwn;
			float topRightBrightness = lightValueOwn;
			bool bottomFaceColored = true;
			bool topFaceColored = true;
			bool eastFaceColored = true;
			bool westFaceColored = true;
			bool northFaceColored = true;
			bool southFaceColored = true;
			lightValueOwn = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z);
			aoLightValueXNeg = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z);
			aoLightValueYNeg = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z);
			aoLightValueZNeg = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z - 1);
			aoLightValueXPos = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z);
			aoLightValueYPos = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z);
			aoLightValueZPos = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z + 1);


            int blockBrightness = block.GetMixedBrightnessForBlock(blockAccess, x, y, z);
			int minXBrightness = blockBrightness;
			int minYBrightness = blockBrightness;
			int minZBrightness = blockBrightness;
			int maxXBrightness = blockBrightness;
			int maxYBrightness = blockBrightness;
			int maxZBrightness = blockBrightness;
			if (block.minY <= 0.0D)
			{
				minYBrightness = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z);
			}

			if (block.maxY >= 1.0D)
			{
				maxYBrightness = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z);
			}

			if (block.minX <= 0.0D)
			{
				minXBrightness = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z);
			}

			if (block.maxX >= 1.0D)
			{
				maxXBrightness = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z);
			}

			if (block.minZ <= 0.0D)
			{
				minZBrightness = block.GetMixedBrightnessForBlock(blockAccess, x, y, z - 1);
			}

			if (block.maxZ >= 1.0D)
			{
				maxZBrightness = block.GetMixedBrightnessForBlock(blockAccess, x, y, z + 1);
			}

            // Debug mark
            Tessellator tessellator = Tessellator.instance;
			tessellator.Brightness = 983055;
            
			aoGrassXYZPPC = Block.canBlockGrass[blockAccess.getBlockId(x + 1, y + 1, z)];
			aoGrassXYZPNC = Block.canBlockGrass[blockAccess.getBlockId(x + 1, y - 1, z)];
			aoGrassXYZPCP = Block.canBlockGrass[blockAccess.getBlockId(x + 1, y, z + 1)];
			aoGrassXYZPCN = Block.canBlockGrass[blockAccess.getBlockId(x + 1, y, z - 1)];
			aoGrassXYZNPC = Block.canBlockGrass[blockAccess.getBlockId(x - 1, y + 1, z)];
			aoGrassXYZNNC = Block.canBlockGrass[blockAccess.getBlockId(x - 1, y - 1, z)];
			aoGrassXYZNCN = Block.canBlockGrass[blockAccess.getBlockId(x - 1, y, z - 1)];
			aoGrassXYZNCP = Block.canBlockGrass[blockAccess.getBlockId(x - 1, y, z + 1)];
			aoGrassXYZCPP = Block.canBlockGrass[blockAccess.getBlockId(x, y + 1, z + 1)];
			aoGrassXYZCPN = Block.canBlockGrass[blockAccess.getBlockId(x, y + 1, z - 1)];
			aoGrassXYZCNP = Block.canBlockGrass[blockAccess.getBlockId(x, y - 1, z + 1)];
			aoGrassXYZCNN = Block.canBlockGrass[blockAccess.getBlockId(x, y - 1, z - 1)];
            
			if (block.blockIndexInTexture == 3)
			{
				southFaceColored = false;
				northFaceColored = false;
				westFaceColored = false;
				eastFaceColored = false;
				bottomFaceColored = false;
			}

			if (overrideBlockTexture >= 0)
			{
				southFaceColored = false;
				northFaceColored = false;
				westFaceColored = false;
				eastFaceColored = false;
				bottomFaceColored = false;
			}

            #region top face
            if (renderAllFaces || block.shouldSideBeRendered(blockAccess, x, y + 1, z, 1))
            {
                if (aoType > 0)
                {
                    if (block.maxY >= 1.0D)
                    {
                        ++y;
                    }

                    aoBrightnessXYNP = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z);
                    aoBrightnessXYPP = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z);
                    aoBrightnessYZPN = block.GetMixedBrightnessForBlock(blockAccess, x, y, z - 1);
                    aoBrightnessYZPP = block.GetMixedBrightnessForBlock(blockAccess, x, y, z + 1);
                    aoLightValueScratchXYNP = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z);
                    aoLightValueScratchXYPP = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z);
                    aoLightValueScratchYZPN = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z - 1);
                    aoLightValueScratchYZPP = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z + 1);
                    
                    if (!aoGrassXYZCPN && !aoGrassXYZNPC)
                    {
                        aoLightValueScratchXYZNPN = aoLightValueScratchXYNP;
                        aoBrightnessXYZNPN = aoBrightnessXYNP;
                    }
                    else
                    {
                        aoLightValueScratchXYZNPN = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z - 1);
                        aoBrightnessXYZNPN = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z - 1);
                    }

                    if (!aoGrassXYZCPN && !aoGrassXYZPPC)
                    {
                        aoLightValueScratchXYZPPN = aoLightValueScratchXYPP;
                        aoBrightnessXYZPPN = aoBrightnessXYPP;
                    }
                    else
                    {
                        aoLightValueScratchXYZPPN = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z - 1);
                        aoBrightnessXYZPPN = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z - 1);
                    }

                    if (!aoGrassXYZCPP && !aoGrassXYZNPC)
                    {
                        aoLightValueScratchXYZNPP = aoLightValueScratchXYNP;
                        aoBrightnessXYZNPP = aoBrightnessXYNP;
                    }
                    else
                    {
                        aoLightValueScratchXYZNPP = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z + 1);
                        aoBrightnessXYZNPP = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z + 1);
                    }

                    if (!aoGrassXYZCPP && !aoGrassXYZPPC)
                    {
                        aoLightValueScratchXYZPPP = aoLightValueScratchXYPP;
                        aoBrightnessXYZPPP = aoBrightnessXYPP;
                    }
                    else
                    {
                        aoLightValueScratchXYZPPP = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z + 1);
                        aoBrightnessXYZPPP = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z + 1);
                    }

                    if (block.maxY >= 1.0D)
                    {
                        --y;
                    }

                    topRightBrightness = (aoLightValueScratchXYZNPP + aoLightValueScratchXYNP + aoLightValueScratchYZPP + aoLightValueYPos) / 4.0F;
                    topLeftBrightness = (aoLightValueScratchYZPP + aoLightValueYPos + aoLightValueScratchXYZPPP + aoLightValueScratchXYPP) / 4.0F;
                    bottomLeftBrightness = (aoLightValueYPos + aoLightValueScratchYZPN + aoLightValueScratchXYPP + aoLightValueScratchXYZPPN) / 4.0F;
                    bottomRightBrightness = (aoLightValueScratchXYNP + aoLightValueScratchXYZNPN + aoLightValueYPos + aoLightValueScratchYZPN) / 4.0F;
                    brightnessTopRight = getAoBrightness(aoBrightnessXYZNPP, aoBrightnessXYNP, aoBrightnessYZPP, maxYBrightness);
                    brightnessTopLeft = getAoBrightness(aoBrightnessYZPP, aoBrightnessXYZPPP, aoBrightnessXYPP, maxYBrightness);
                    brightnessBottomLeft = getAoBrightness(aoBrightnessYZPN, aoBrightnessXYPP, aoBrightnessXYZPPN, maxYBrightness);
                    brightnessBottomRight = getAoBrightness(aoBrightnessXYNP, aoBrightnessXYZNPN, aoBrightnessYZPN, maxYBrightness);
                }
                else
                {
                    topRightBrightness = aoLightValueYPos;
                    bottomRightBrightness = aoLightValueYPos;
                    bottomLeftBrightness = aoLightValueYPos;
                    topLeftBrightness = aoLightValueYPos;
                    brightnessTopLeft = brightnessBottomLeft = brightnessBottomRight = brightnessTopRight = maxYBrightness;
                }

                colorRedTopLeft = colorRedBottomLeft = colorRedBottomRight = colorRedTopRight = topFaceColored ? r : 1.0F;
                colorGreenTopLeft = colorGreenBottomLeft = colorGreenBottomRight = colorGreenTopRight = topFaceColored ? g : 1.0F;
                colorBlueTopLeft = colorBlueBottomLeft = colorBlueBottomRight = colorBlueTopRight = topFaceColored ? b : 1.0F;
                colorRedTopLeft *= topLeftBrightness;
                colorGreenTopLeft *= topLeftBrightness;
                colorBlueTopLeft *= topLeftBrightness;
                colorRedBottomLeft *= bottomLeftBrightness;
                colorGreenBottomLeft *= bottomLeftBrightness;
                colorBlueBottomLeft *= bottomLeftBrightness;
                colorRedBottomRight *= bottomRightBrightness;
                colorGreenBottomRight *= bottomRightBrightness;
                colorBlueBottomRight *= bottomRightBrightness;
                colorRedTopRight *= topRightBrightness;
                colorGreenTopRight *= topRightBrightness;
                colorBlueTopRight *= topRightBrightness;
                renderTopFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(blockAccess, x, y, z, 1));
                facesDrawn = true;
            }
            #endregion

            #region bottom face
            if (renderAllFaces || block.shouldSideBeRendered(blockAccess, x, y - 1, z, 0))
			{
				if (aoType > 0)
				{
					if (block.minY <= 0.0D)
					{
						--y;
					}

					aoBrightnessXYNN = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z);
					aoBrightnessYZNN = block.GetMixedBrightnessForBlock(blockAccess, x, y, z - 1);
					aoBrightnessYZNP = block.GetMixedBrightnessForBlock(blockAccess, x, y, z + 1);
					aoBrightnessXYPN = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z);
					aoLightValueScratchXYNN = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z);
					aoLightValueScratchYZNN = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z - 1);
					aoLightValueScratchYZNP = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z + 1);
					aoLightValueScratchXYPN = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z);
					if (!aoGrassXYZCNN && !aoGrassXYZNNC)
					{
						aoLightValueScratchXYZNNN = aoLightValueScratchXYNN;
						aoBrightnessXYZNNN = aoBrightnessXYNN;
					}
					else
					{
						aoLightValueScratchXYZNNN = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z - 1);
						aoBrightnessXYZNNN = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z - 1);
					}

					if (!aoGrassXYZCNP && !aoGrassXYZNNC)
					{
						aoLightValueScratchXYZNNP = aoLightValueScratchXYNN;
						aoBrightnessXYZNNP = aoBrightnessXYNN;
					}
					else
					{
						aoLightValueScratchXYZNNP = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z + 1);
						aoBrightnessXYZNNP = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z + 1);
					}

					if (!aoGrassXYZCNN && !aoGrassXYZPNC)
					{
						aoLightValueScratchXYZPNN = aoLightValueScratchXYPN;
						aoBrightnessXYZPNN = aoBrightnessXYPN;
					}
					else
					{
						aoLightValueScratchXYZPNN = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z - 1);
						aoBrightnessXYZPNN = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z - 1);
					}

					if (!aoGrassXYZCNP && !aoGrassXYZPNC)
					{
						aoLightValueScratchXYZPNP = aoLightValueScratchXYPN;
						aoBrightnessXYZPNP = aoBrightnessXYPN;
					}
					else
					{
						aoLightValueScratchXYZPNP = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z + 1);
						aoBrightnessXYZPNP = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z + 1);
					}

					if (block.minY <= 0.0D)
					{
						++y;
					}

					topLeftBrightness = (aoLightValueScratchXYZNNP + aoLightValueScratchXYNN + aoLightValueScratchYZNP + aoLightValueYNeg) / 4.0F;
					topRightBrightness = (aoLightValueScratchYZNP + aoLightValueYNeg + aoLightValueScratchXYZPNP + aoLightValueScratchXYPN) / 4.0F;
					bottomRightBrightness = (aoLightValueYNeg + aoLightValueScratchYZNN + aoLightValueScratchXYPN + aoLightValueScratchXYZPNN) / 4.0F;
					bottomLeftBrightness = (aoLightValueScratchXYNN + aoLightValueScratchXYZNNN + aoLightValueYNeg + aoLightValueScratchYZNN) / 4.0F;
					brightnessTopLeft = getAoBrightness(aoBrightnessXYZNNP, aoBrightnessXYNN, aoBrightnessYZNP, minYBrightness);
					brightnessTopRight = getAoBrightness(aoBrightnessYZNP, aoBrightnessXYZPNP, aoBrightnessXYPN, minYBrightness);
					brightnessBottomRight = getAoBrightness(aoBrightnessYZNN, aoBrightnessXYPN, aoBrightnessXYZPNN, minYBrightness);
					brightnessBottomLeft = getAoBrightness(aoBrightnessXYNN, aoBrightnessXYZNNN, aoBrightnessYZNN, minYBrightness);
				}
				else
				{
					topRightBrightness = aoLightValueYNeg;
					bottomRightBrightness = aoLightValueYNeg;
					bottomLeftBrightness = aoLightValueYNeg;
					topLeftBrightness = aoLightValueYNeg;
					brightnessTopLeft = brightnessBottomLeft = brightnessBottomRight = brightnessTopRight = aoBrightnessXYNN;
				}

				colorRedTopLeft = colorRedBottomLeft = colorRedBottomRight = colorRedTopRight = (bottomFaceColored ? r : 1.0F) * 0.5F;
				colorGreenTopLeft = colorGreenBottomLeft = colorGreenBottomRight = colorGreenTopRight = (bottomFaceColored ? g : 1.0F) * 0.5F;
				colorBlueTopLeft = colorBlueBottomLeft = colorBlueBottomRight = colorBlueTopRight = (bottomFaceColored ? b : 1.0F) * 0.5F;
				colorRedTopLeft *= topLeftBrightness;
				colorGreenTopLeft *= topLeftBrightness;
				colorBlueTopLeft *= topLeftBrightness;
				colorRedBottomLeft *= bottomLeftBrightness;
				colorGreenBottomLeft *= bottomLeftBrightness;
				colorBlueBottomLeft *= bottomLeftBrightness;
				colorRedBottomRight *= bottomRightBrightness;
				colorGreenBottomRight *= bottomRightBrightness;
				colorBlueBottomRight *= bottomRightBrightness;
				colorRedTopRight *= topRightBrightness;
				colorGreenTopRight *= topRightBrightness;
				colorBlueTopRight *= topRightBrightness;
				renderBottomFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(blockAccess, x, y, z, 0));
				facesDrawn = true;
			}
            #endregion

            #region east face
            int i27;
			if (renderAllFaces || block.shouldSideBeRendered(blockAccess, x, y, z - 1, 2))
			{
				if (aoType > 0)
				{
					if (block.minZ <= 0.0D)
					{
						--z;
					}

					aoLightValueScratchXZNN = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z);
					aoLightValueScratchYZNN = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z);
					aoLightValueScratchYZPN = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z);
					aoLightValueScratchXZPN = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z);
					aoBrightnessXZNN = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z);
					aoBrightnessYZNN = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z);
					aoBrightnessYZPN = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z);
					aoBrightnessXZPN = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z);
					if (!aoGrassXYZNCN && !aoGrassXYZCNN)
					{
						aoLightValueScratchXYZNNN = aoLightValueScratchXZNN;
						aoBrightnessXYZNNN = aoBrightnessXZNN;
					}
					else
					{
						aoLightValueScratchXYZNNN = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y - 1, z);
						aoBrightnessXYZNNN = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y - 1, z);
					}

					if (!aoGrassXYZNCN && !aoGrassXYZCPN)
					{
						aoLightValueScratchXYZNPN = aoLightValueScratchXZNN;
						aoBrightnessXYZNPN = aoBrightnessXZNN;
					}
					else
					{
						aoLightValueScratchXYZNPN = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y + 1, z);
						aoBrightnessXYZNPN = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y + 1, z);
					}

					if (!aoGrassXYZPCN && !aoGrassXYZCNN)
					{
						aoLightValueScratchXYZPNN = aoLightValueScratchXZPN;
						aoBrightnessXYZPNN = aoBrightnessXZPN;
					}
					else
					{
						aoLightValueScratchXYZPNN = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y - 1, z);
						aoBrightnessXYZPNN = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y - 1, z);
					}

					if (!aoGrassXYZPCN && !aoGrassXYZCPN)
					{
						aoLightValueScratchXYZPPN = aoLightValueScratchXZPN;
						aoBrightnessXYZPPN = aoBrightnessXZPN;
					}
					else
					{
						aoLightValueScratchXYZPPN = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y + 1, z);
						aoBrightnessXYZPPN = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y + 1, z);
					}

					if (block.minZ <= 0.0D)
					{
						++z;
					}

					topLeftBrightness = (aoLightValueScratchXZNN + aoLightValueScratchXYZNPN + aoLightValueZNeg + aoLightValueScratchYZPN) / 4.0F;
					bottomLeftBrightness = (aoLightValueZNeg + aoLightValueScratchYZPN + aoLightValueScratchXZPN + aoLightValueScratchXYZPPN) / 4.0F;
					bottomRightBrightness = (aoLightValueScratchYZNN + aoLightValueZNeg + aoLightValueScratchXYZPNN + aoLightValueScratchXZPN) / 4.0F;
					topRightBrightness = (aoLightValueScratchXYZNNN + aoLightValueScratchXZNN + aoLightValueScratchYZNN + aoLightValueZNeg) / 4.0F;
					brightnessTopLeft = getAoBrightness(aoBrightnessXZNN, aoBrightnessXYZNPN, aoBrightnessYZPN, minZBrightness);
					brightnessBottomLeft = getAoBrightness(aoBrightnessYZPN, aoBrightnessXZPN, aoBrightnessXYZPPN, minZBrightness);
					brightnessBottomRight = getAoBrightness(aoBrightnessYZNN, aoBrightnessXYZPNN, aoBrightnessXZPN, minZBrightness);
					brightnessTopRight = getAoBrightness(aoBrightnessXYZNNN, aoBrightnessXZNN, aoBrightnessYZNN, minZBrightness);
				}
				else
				{
					topRightBrightness = aoLightValueZNeg;
					bottomRightBrightness = aoLightValueZNeg;
					bottomLeftBrightness = aoLightValueZNeg;
					topLeftBrightness = aoLightValueZNeg;
					brightnessTopLeft = brightnessBottomLeft = brightnessBottomRight = brightnessTopRight = minZBrightness;
				}
                
                colorRedTopLeft = colorRedBottomLeft = colorRedBottomRight = colorRedTopRight = (eastFaceColored ? r : 1.0F) * 0.8F;
				colorGreenTopLeft = colorGreenBottomLeft = colorGreenBottomRight = colorGreenTopRight = (eastFaceColored ? g : 1.0F) * 0.8F;
				colorBlueTopLeft = colorBlueBottomLeft = colorBlueBottomRight = colorBlueTopRight = (eastFaceColored ? b : 1.0F) * 0.8F;
				colorRedTopLeft *= topLeftBrightness;
				colorGreenTopLeft *= topLeftBrightness;
				colorBlueTopLeft *= topLeftBrightness;
				colorRedBottomLeft *= bottomLeftBrightness;
				colorGreenBottomLeft *= bottomLeftBrightness;
				colorBlueBottomLeft *= bottomLeftBrightness;
				colorRedBottomRight *= bottomRightBrightness;
				colorGreenBottomRight *= bottomRightBrightness;
				colorBlueBottomRight *= bottomRightBrightness;
				colorRedTopRight *= topRightBrightness;
				colorGreenTopRight *= topRightBrightness;
				colorBlueTopRight *= topRightBrightness;
				i27 = block.getBlockTexture(blockAccess, x, y, z, 2);
				renderEastFace(block, (double)x, (double)y, (double)z, i27);
				if (fancyGrass && i27 == 3 && overrideBlockTexture < 0)
				{
					colorRedTopLeft *= r;
					colorRedBottomLeft *= r;
					colorRedBottomRight *= r;
					colorRedTopRight *= r;
					colorGreenTopLeft *= g;
					colorGreenBottomLeft *= g;
					colorGreenBottomRight *= g;
					colorGreenTopRight *= g;
					colorBlueTopLeft *= b;
					colorBlueBottomLeft *= b;
					colorBlueBottomRight *= b;
					colorBlueTopRight *= b;
					renderEastFace(block, (double)x, (double)y, (double)z, 38);
				}

				facesDrawn = true;
			}
            #endregion

            #region west face
            if (renderAllFaces || block.shouldSideBeRendered(blockAccess, x, y, z + 1, 3))
			{
				if (aoType > 0)
				{
					if (block.maxZ >= 1.0D)
					{
						++z;
					}

					aoLightValueScratchXZNP = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y, z);
					aoLightValueScratchXZPP = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y, z);
					aoLightValueScratchYZNP = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z);
					aoLightValueScratchYZPP = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z);
					aoBrightnessXZNP = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y, z);
					aoBrightnessXZPP = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y, z);
					aoBrightnessYZNP = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z);
					aoBrightnessYZPP = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z);
					if (!aoGrassXYZNCP && !aoGrassXYZCNP)
					{
						aoLightValueScratchXYZNNP = aoLightValueScratchXZNP;
						aoBrightnessXYZNNP = aoBrightnessXZNP;
					}
					else
					{
						aoLightValueScratchXYZNNP = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y - 1, z);
						aoBrightnessXYZNNP = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y - 1, z);
					}

					if (!aoGrassXYZNCP && !aoGrassXYZCPP)
					{
						aoLightValueScratchXYZNPP = aoLightValueScratchXZNP;
						aoBrightnessXYZNPP = aoBrightnessXZNP;
					}
					else
					{
						aoLightValueScratchXYZNPP = block.GetAmbientOcclusionLightValue(blockAccess, x - 1, y + 1, z);
						aoBrightnessXYZNPP = block.GetMixedBrightnessForBlock(blockAccess, x - 1, y + 1, z);
					}

					if (!aoGrassXYZPCP && !aoGrassXYZCNP)
					{
						aoLightValueScratchXYZPNP = aoLightValueScratchXZPP;
						aoBrightnessXYZPNP = aoBrightnessXZPP;
					}
					else
					{
						aoLightValueScratchXYZPNP = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y - 1, z);
						aoBrightnessXYZPNP = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y - 1, z);
					}

					if (!aoGrassXYZPCP && !aoGrassXYZCPP)
					{
						aoLightValueScratchXYZPPP = aoLightValueScratchXZPP;
						aoBrightnessXYZPPP = aoBrightnessXZPP;
					}
					else
					{
						aoLightValueScratchXYZPPP = block.GetAmbientOcclusionLightValue(blockAccess, x + 1, y + 1, z);
						aoBrightnessXYZPPP = block.GetMixedBrightnessForBlock(blockAccess, x + 1, y + 1, z);
					}

					if (block.maxZ >= 1.0D)
					{
						--z;
					}

					topLeftBrightness = (aoLightValueScratchXZNP + aoLightValueScratchXYZNPP + aoLightValueZPos + aoLightValueScratchYZPP) / 4.0F;
					topRightBrightness = (aoLightValueZPos + aoLightValueScratchYZPP + aoLightValueScratchXZPP + aoLightValueScratchXYZPPP) / 4.0F;
					bottomRightBrightness = (aoLightValueScratchYZNP + aoLightValueZPos + aoLightValueScratchXYZPNP + aoLightValueScratchXZPP) / 4.0F;
					bottomLeftBrightness = (aoLightValueScratchXYZNNP + aoLightValueScratchXZNP + aoLightValueScratchYZNP + aoLightValueZPos) / 4.0F;
					brightnessTopLeft = getAoBrightness(aoBrightnessXZNP, aoBrightnessXYZNPP, aoBrightnessYZPP, maxZBrightness);
					brightnessTopRight = getAoBrightness(aoBrightnessYZPP, aoBrightnessXZPP, aoBrightnessXYZPPP, maxZBrightness);
					brightnessBottomRight = getAoBrightness(aoBrightnessYZNP, aoBrightnessXYZPNP, aoBrightnessXZPP, maxZBrightness);
					brightnessBottomLeft = getAoBrightness(aoBrightnessXYZNNP, aoBrightnessXZNP, aoBrightnessYZNP, maxZBrightness);
				}
				else
				{
					topRightBrightness = aoLightValueZPos;
					bottomRightBrightness = aoLightValueZPos;
					bottomLeftBrightness = aoLightValueZPos;
					topLeftBrightness = aoLightValueZPos;
					brightnessTopLeft = brightnessBottomLeft = brightnessBottomRight = brightnessTopRight = maxZBrightness;
				}

				colorRedTopLeft = colorRedBottomLeft = colorRedBottomRight = colorRedTopRight = (westFaceColored ? r : 1.0F) * 0.8F;
				colorGreenTopLeft = colorGreenBottomLeft = colorGreenBottomRight = colorGreenTopRight = (westFaceColored ? g : 1.0F) * 0.8F;
				colorBlueTopLeft = colorBlueBottomLeft = colorBlueBottomRight = colorBlueTopRight = (westFaceColored ? b : 1.0F) * 0.8F;
				colorRedTopLeft *= topLeftBrightness;
				colorGreenTopLeft *= topLeftBrightness;
				colorBlueTopLeft *= topLeftBrightness;
				colorRedBottomLeft *= bottomLeftBrightness;
				colorGreenBottomLeft *= bottomLeftBrightness;
				colorBlueBottomLeft *= bottomLeftBrightness;
				colorRedBottomRight *= bottomRightBrightness;
				colorGreenBottomRight *= bottomRightBrightness;
				colorBlueBottomRight *= bottomRightBrightness;
				colorRedTopRight *= topRightBrightness;
				colorGreenTopRight *= topRightBrightness;
				colorBlueTopRight *= topRightBrightness;
				i27 = block.getBlockTexture(blockAccess, x, y, z, 3);
				renderWestFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(blockAccess, x, y, z, 3));
				if (fancyGrass && i27 == 3 && overrideBlockTexture < 0)
				{
					colorRedTopLeft *= r;
					colorRedBottomLeft *= r;
					colorRedBottomRight *= r;
					colorRedTopRight *= r;
					colorGreenTopLeft *= g;
					colorGreenBottomLeft *= g;
					colorGreenBottomRight *= g;
					colorGreenTopRight *= g;
					colorBlueTopLeft *= b;
					colorBlueBottomLeft *= b;
					colorBlueBottomRight *= b;
					colorBlueTopRight *= b;
					renderWestFace(block, (double)x, (double)y, (double)z, 38);
				}

				facesDrawn = true;
			}
            #endregion

            #region north face
            if (renderAllFaces || block.shouldSideBeRendered(blockAccess, x - 1, y, z, 4))
			{
				if (aoType > 0)
				{
					if (block.minX <= 0.0D)
					{
						--x;
					}

					aoLightValueScratchXYNN = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z);
					aoLightValueScratchXZNN = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z - 1);
					aoLightValueScratchXZNP = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z + 1);
					aoLightValueScratchXYNP = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z);
					aoBrightnessXYNN = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z);
					aoBrightnessXZNN = block.GetMixedBrightnessForBlock(blockAccess, x, y, z - 1);
					aoBrightnessXZNP = block.GetMixedBrightnessForBlock(blockAccess, x, y, z + 1);
					aoBrightnessXYNP = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z);
					if (!aoGrassXYZNCN && !aoGrassXYZNNC)
					{
						aoLightValueScratchXYZNNN = aoLightValueScratchXZNN;
						aoBrightnessXYZNNN = aoBrightnessXZNN;
					}
					else
					{
						aoLightValueScratchXYZNNN = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z - 1);
						aoBrightnessXYZNNN = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z - 1);
					}

					if (!aoGrassXYZNCP && !aoGrassXYZNNC)
					{
						aoLightValueScratchXYZNNP = aoLightValueScratchXZNP;
						aoBrightnessXYZNNP = aoBrightnessXZNP;
					}
					else
					{
						aoLightValueScratchXYZNNP = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z + 1);
						aoBrightnessXYZNNP = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z + 1);
					}

					if (!aoGrassXYZNCN && !aoGrassXYZNPC)
					{
						aoLightValueScratchXYZNPN = aoLightValueScratchXZNN;
						aoBrightnessXYZNPN = aoBrightnessXZNN;
					}
					else
					{
						aoLightValueScratchXYZNPN = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z - 1);
						aoBrightnessXYZNPN = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z - 1);
					}

					if (!aoGrassXYZNCP && !aoGrassXYZNPC)
					{
						aoLightValueScratchXYZNPP = aoLightValueScratchXZNP;
						aoBrightnessXYZNPP = aoBrightnessXZNP;
					}
					else
					{
						aoLightValueScratchXYZNPP = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z + 1);
						aoBrightnessXYZNPP = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z + 1);
					}

					if (block.minX <= 0.0D)
					{
						++x;
					}

					topRightBrightness = (aoLightValueScratchXYNN + aoLightValueScratchXYZNNP + aoLightValueXNeg + aoLightValueScratchXZNP) / 4.0F;
					topLeftBrightness = (aoLightValueXNeg + aoLightValueScratchXZNP + aoLightValueScratchXYNP + aoLightValueScratchXYZNPP) / 4.0F;
					bottomLeftBrightness = (aoLightValueScratchXZNN + aoLightValueXNeg + aoLightValueScratchXYZNPN + aoLightValueScratchXYNP) / 4.0F;
					bottomRightBrightness = (aoLightValueScratchXYZNNN + aoLightValueScratchXYNN + aoLightValueScratchXZNN + aoLightValueXNeg) / 4.0F;
					brightnessTopRight = getAoBrightness(aoBrightnessXYNN, aoBrightnessXYZNNP, aoBrightnessXZNP, minXBrightness);
					brightnessTopLeft = getAoBrightness(aoBrightnessXZNP, aoBrightnessXYNP, aoBrightnessXYZNPP, minXBrightness);
					brightnessBottomLeft = getAoBrightness(aoBrightnessXZNN, aoBrightnessXYZNPN, aoBrightnessXYNP, minXBrightness);
					brightnessBottomRight = getAoBrightness(aoBrightnessXYZNNN, aoBrightnessXYNN, aoBrightnessXZNN, minXBrightness);
				}
				else
				{
					topRightBrightness = aoLightValueXNeg;
					bottomRightBrightness = aoLightValueXNeg;
					bottomLeftBrightness = aoLightValueXNeg;
					topLeftBrightness = aoLightValueXNeg;
					brightnessTopLeft = brightnessBottomLeft = brightnessBottomRight = brightnessTopRight = minXBrightness;
				}

				colorRedTopLeft = colorRedBottomLeft = colorRedBottomRight = colorRedTopRight = (northFaceColored ? r : 1.0F) * 0.6F;
				colorGreenTopLeft = colorGreenBottomLeft = colorGreenBottomRight = colorGreenTopRight = (northFaceColored ? g : 1.0F) * 0.6F;
				colorBlueTopLeft = colorBlueBottomLeft = colorBlueBottomRight = colorBlueTopRight = (northFaceColored ? b : 1.0F) * 0.6F;
				colorRedTopLeft *= topLeftBrightness;
				colorGreenTopLeft *= topLeftBrightness;
				colorBlueTopLeft *= topLeftBrightness;
				colorRedBottomLeft *= bottomLeftBrightness;
				colorGreenBottomLeft *= bottomLeftBrightness;
				colorBlueBottomLeft *= bottomLeftBrightness;
				colorRedBottomRight *= bottomRightBrightness;
				colorGreenBottomRight *= bottomRightBrightness;
				colorBlueBottomRight *= bottomRightBrightness;
				colorRedTopRight *= topRightBrightness;
				colorGreenTopRight *= topRightBrightness;
				colorBlueTopRight *= topRightBrightness;
				i27 = block.getBlockTexture(blockAccess, x, y, z, 4);
				renderNorthFace(block, (double)x, (double)y, (double)z, i27);
				if (fancyGrass && i27 == 3 && overrideBlockTexture < 0)
				{
					colorRedTopLeft *= r;
					colorRedBottomLeft *= r;
					colorRedBottomRight *= r;
					colorRedTopRight *= r;
					colorGreenTopLeft *= g;
					colorGreenBottomLeft *= g;
					colorGreenBottomRight *= g;
					colorGreenTopRight *= g;
					colorBlueTopLeft *= b;
					colorBlueBottomLeft *= b;
					colorBlueBottomRight *= b;
					colorBlueTopRight *= b;
					renderNorthFace(block, (double)x, (double)y, (double)z, 38);
				}

				facesDrawn = true;
			}
            #endregion

            #region south face
            if (renderAllFaces || block.shouldSideBeRendered(blockAccess, x + 1, y, z, 5))
			{
				if (aoType > 0)
				{
					if (block.maxX >= 1.0D)
					{
						++x;
					}

					aoLightValueScratchXYPN = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z);
					aoLightValueScratchXZPN = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z - 1);
					aoLightValueScratchXZPP = block.GetAmbientOcclusionLightValue(blockAccess, x, y, z + 1);
					aoLightValueScratchXYPP = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z);
					aoBrightnessXYPN = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z);
					aoBrightnessXZPN = block.GetMixedBrightnessForBlock(blockAccess, x, y, z - 1);
					aoBrightnessXZPP = block.GetMixedBrightnessForBlock(blockAccess, x, y, z + 1);
					aoBrightnessXYPP = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z);
					if (!aoGrassXYZPNC && !aoGrassXYZPCN)
					{
						aoLightValueScratchXYZPNN = aoLightValueScratchXZPN;
						aoBrightnessXYZPNN = aoBrightnessXZPN;
					}
					else
					{
						aoLightValueScratchXYZPNN = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z - 1);
						aoBrightnessXYZPNN = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z - 1);
					}

					if (!aoGrassXYZPNC && !aoGrassXYZPCP)
					{
						aoLightValueScratchXYZPNP = aoLightValueScratchXZPP;
						aoBrightnessXYZPNP = aoBrightnessXZPP;
					}
					else
					{
						aoLightValueScratchXYZPNP = block.GetAmbientOcclusionLightValue(blockAccess, x, y - 1, z + 1);
						aoBrightnessXYZPNP = block.GetMixedBrightnessForBlock(blockAccess, x, y - 1, z + 1);
					}

					if (!aoGrassXYZPPC && !aoGrassXYZPCN)
					{
						aoLightValueScratchXYZPPN = aoLightValueScratchXZPN;
						aoBrightnessXYZPPN = aoBrightnessXZPN;
					}
					else
					{
						aoLightValueScratchXYZPPN = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z - 1);
						aoBrightnessXYZPPN = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z - 1);
					}

					if (!aoGrassXYZPPC && !aoGrassXYZPCP)
					{
						aoLightValueScratchXYZPPP = aoLightValueScratchXZPP;
						aoBrightnessXYZPPP = aoBrightnessXZPP;
					}
					else
					{
						aoLightValueScratchXYZPPP = block.GetAmbientOcclusionLightValue(blockAccess, x, y + 1, z + 1);
						aoBrightnessXYZPPP = block.GetMixedBrightnessForBlock(blockAccess, x, y + 1, z + 1);
					}

					if (block.maxX >= 1.0D)
					{
						--x;
					}

					topLeftBrightness = (aoLightValueScratchXYPN + aoLightValueScratchXYZPNP + aoLightValueXPos + aoLightValueScratchXZPP) / 4.0F;
					topRightBrightness = (aoLightValueXPos + aoLightValueScratchXZPP + aoLightValueScratchXYPP + aoLightValueScratchXYZPPP) / 4.0F;
					bottomRightBrightness = (aoLightValueScratchXZPN + aoLightValueXPos + aoLightValueScratchXYZPPN + aoLightValueScratchXYPP) / 4.0F;
					bottomLeftBrightness = (aoLightValueScratchXYZPNN + aoLightValueScratchXYPN + aoLightValueScratchXZPN + aoLightValueXPos) / 4.0F;
					brightnessTopLeft = getAoBrightness(aoBrightnessXYPN, aoBrightnessXYZPNP, aoBrightnessXZPP, maxXBrightness);
					brightnessTopRight = getAoBrightness(aoBrightnessXZPP, aoBrightnessXYPP, aoBrightnessXYZPPP, maxXBrightness);
					brightnessBottomRight = getAoBrightness(aoBrightnessXZPN, aoBrightnessXYZPPN, aoBrightnessXYPP, maxXBrightness);
					brightnessBottomLeft = getAoBrightness(aoBrightnessXYZPNN, aoBrightnessXYPN, aoBrightnessXZPN, maxXBrightness);
				}
				else
				{
					topRightBrightness = aoLightValueXPos;
					bottomRightBrightness = aoLightValueXPos;
					bottomLeftBrightness = aoLightValueXPos;
					topLeftBrightness = aoLightValueXPos;
					brightnessTopLeft = brightnessBottomLeft = brightnessBottomRight = brightnessTopRight = maxXBrightness;
				}

				colorRedTopLeft = colorRedBottomLeft = colorRedBottomRight = colorRedTopRight = (southFaceColored ? r : 1.0F) * 0.6F;
				colorGreenTopLeft = colorGreenBottomLeft = colorGreenBottomRight = colorGreenTopRight = (southFaceColored ? g : 1.0F) * 0.6F;
				colorBlueTopLeft = colorBlueBottomLeft = colorBlueBottomRight = colorBlueTopRight = (southFaceColored ? b : 1.0F) * 0.6F;
				colorRedTopLeft *= topLeftBrightness;
				colorGreenTopLeft *= topLeftBrightness;
				colorBlueTopLeft *= topLeftBrightness;
				colorRedBottomLeft *= bottomLeftBrightness;
				colorGreenBottomLeft *= bottomLeftBrightness;
				colorBlueBottomLeft *= bottomLeftBrightness;
				colorRedBottomRight *= bottomRightBrightness;
				colorGreenBottomRight *= bottomRightBrightness;
				colorBlueBottomRight *= bottomRightBrightness;
				colorRedTopRight *= topRightBrightness;
				colorGreenTopRight *= topRightBrightness;
				colorBlueTopRight *= topRightBrightness;
				i27 = block.getBlockTexture(blockAccess, x, y, z, 5);
				renderSouthFace(block, (double)x, (double)y, (double)z, i27);
				if (fancyGrass && i27 == 3 && overrideBlockTexture < 0)
				{
					colorRedTopLeft *= r;
					colorRedBottomLeft *= r;
					colorRedBottomRight *= r;
					colorRedTopRight *= r;
					colorGreenTopLeft *= g;
					colorGreenBottomLeft *= g;
					colorGreenBottomRight *= g;
					colorGreenTopRight *= g;
					colorBlueTopLeft *= b;
					colorBlueBottomLeft *= b;
					colorBlueBottomRight *= b;
					colorBlueTopRight *= b;
					renderSouthFace(block, (double)x, (double)y, (double)z, 38);
				}

				facesDrawn = true;
			}
			#endregion

			enableAO = false;
			return facesDrawn;
		}

		private int getAoBrightness(int i1, int i2, int i3, int i4)
		{
			if (i1 == 0)
			{
				i1 = i4;
			}

			if (i2 == 0)
			{
				i2 = i4;
			}

			if (i3 == 0)
			{
				i3 = i4;
			}

			return i1 + i2 + i3 + i4 >> 2 & 16711935;
		}

		public virtual bool renderStandardBlockWithColorMultiplier(Block block, int x, int y, int z, float r, float g, float b)
		{
			this.enableAO = false;
			Tessellator tessellator = Tessellator.instance;
			bool z9 = false;
            float colorBlendMultiplier = 1.0F;
            float bottomMultipler = 0.5F;
			float eastMultiplier = 0.8F;
			float northMultiplier = 0.6F;
			float topMultiplerR = colorBlendMultiplier * r;
			float topMultiplierG = colorBlendMultiplier * g;
			float topMultiplierB = colorBlendMultiplier * b;
			float bottomR = bottomMultipler;
			float eastR = eastMultiplier;
			float northR = northMultiplier;
			float bottomG = bottomMultipler;
			float eastG = eastMultiplier;
			float northG = northMultiplier;
			float bottomB = bottomMultipler;
			float eastB = eastMultiplier;
			float northB = northMultiplier;
			if (block != Block.grass)
			{
                // These multipliers are used for their corresponding opposite sides, too.
                // Ex. north is also used for south, bottom is also used for top, etc.
                bottomR = bottomMultipler * r;
				eastR = eastMultiplier * r;
				northR = northMultiplier * r;
				bottomG = bottomMultipler * g;
				eastG = eastMultiplier * g;
				northG = northMultiplier * g;
				bottomB = bottomMultipler * b;
				eastB = eastMultiplier * b;
				northB = northMultiplier * b;
			}

			int blockBrightness = block.GetMixedBrightnessForBlock(this.blockAccess, x, y, z);

			// Bottom face
			if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y - 1, z, 0))
			{
				tessellator.Brightness = block.minY > 0.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y - 1, z);
				tessellator.setColorOpaque_F(bottomR, bottomG, bottomB);
				this.renderBottomFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(this.blockAccess, x, y, z, 0));
				z9 = true;
			}

			// Top face
			if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y + 1, z, 1))
			{
				tessellator.Brightness = block.maxY < 1.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y + 1, z);
				tessellator.setColorOpaque_F(topMultiplerR, topMultiplierG, topMultiplierB);
				this.renderTopFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(this.blockAccess, x, y, z, 1));
				z9 = true;
			}

            // East face
			int i28;
			if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y, z - 1, 2))
			{
				tessellator.Brightness = block.minZ > 0.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y, z - 1);
				tessellator.setColorOpaque_F(eastR, eastG, eastB);
				i28 = block.getBlockTexture(this.blockAccess, x, y, z, 2);
				this.renderEastFace(block, (double)x, (double)y, (double)z, i28);
				if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
				{
					tessellator.setColorOpaque_F(eastR * r, eastG * g, eastB * b);
					this.renderEastFace(block, (double)x, (double)y, (double)z, 38);
				}

				z9 = true;
			}

            // West face
			if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y, z + 1, 3))
			{
				tessellator.Brightness = block.maxZ < 1.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y, z + 1);
				tessellator.setColorOpaque_F(eastR, eastG, eastB);
				i28 = block.getBlockTexture(this.blockAccess, x, y, z, 3);
				this.renderWestFace(block, (double)x, (double)y, (double)z, i28);
				if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
				{
					tessellator.setColorOpaque_F(eastR * r, eastG * g, eastB * b);
					this.renderWestFace(block, (double)x, (double)y, (double)z, 38);
				}

				z9 = true;
			}

			// North face
			if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x - 1, y, z, 4))
			{
				tessellator.Brightness = block.minX > 0.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x - 1, y, z);
				tessellator.setColorOpaque_F(northR, northG, northB);
				i28 = block.getBlockTexture(this.blockAccess, x, y, z, 4);
				this.renderNorthFace(block, (double)x, (double)y, (double)z, i28);
				if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
				{
					tessellator.setColorOpaque_F(northR * r, northG * g, northB * b);
					this.renderNorthFace(block, (double)x, (double)y, (double)z, 38);
				}

				z9 = true;
			}

			// South face
			if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x + 1, y, z, 5))
			{
				tessellator.Brightness = block.maxX < 1.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x + 1, y, z);
				tessellator.setColorOpaque_F(northR, northG, northB);
				i28 = block.getBlockTexture(this.blockAccess, x, y, z, 5);
				this.renderSouthFace(block, (double)x, (double)y, (double)z, i28);
				if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
				{
					tessellator.setColorOpaque_F(northR * r, northG * g, northB * b);
					this.renderSouthFace(block, (double)x, (double)y, (double)z, 38);
				}

				z9 = true;
			}

			return z9;
		}
        
        public virtual bool RenderStandardBlockSmoothAlternate(Block block, int x, int y, int z, float r, float g, float b)
        {
            this.enableAO = true;
            Tessellator tessellator = Tessellator.instance;
            bool z9 = false;
            float colorBlendMultiplier = 1.0F;
            float bottomMultipler = 0.5F;
            float eastMultiplier = 0.8F;
            float northMultiplier = 0.6F;
            float topMultiplerR = colorBlendMultiplier * r;
            float topMultiplierG = colorBlendMultiplier * g;
            float topMultiplierB = colorBlendMultiplier * b;
            float bottomR = bottomMultipler;
            float eastR = eastMultiplier;
            float northR = northMultiplier;
            float bottomG = bottomMultipler;
            float eastG = eastMultiplier;
            float northG = northMultiplier;
            float bottomB = bottomMultipler;
            float eastB = eastMultiplier;
            float northB = northMultiplier;
            if (block != Block.grass)
            {
                // These multipliers are used for their corresponding opposite sides, too.
                // Ex. north is also used for south, west is used for east, with the exception of the top and bottom.
                bottomR = bottomMultipler * r;
                eastR = eastMultiplier * r;
                northR = northMultiplier * r;
                bottomG = bottomMultipler * g;
                eastG = eastMultiplier * g;
                northG = northMultiplier * g;
                bottomB = bottomMultipler * b;
                eastB = eastMultiplier * b;
                northB = northMultiplier * b;
            }

            int blockBrightness = block.GetMixedBrightnessForBlock(this.blockAccess, x, y, z);

            // Bottom face
            if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y - 1, z, 0))
            {
                tessellator.Brightness = block.minY > 0.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y - 1, z);
                tessellator.setColorOpaque_F(bottomR, bottomG, bottomB);
                this.renderBottomFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(this.blockAccess, x, y, z, 0));
                z9 = true;
            }

            // Top face
            if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y + 1, z, 1))
            {
                tessellator.Brightness = block.maxY < 1.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y + 1, z);
                tessellator.setColorOpaque_F(topMultiplerR, topMultiplierG, topMultiplierB);
                this.renderTopFace(block, (double)x, (double)y, (double)z, block.getBlockTexture(this.blockAccess, x, y, z, 1));
                z9 = true;
            }

            // East face
            int i28;
            if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y, z - 1, 2))
            {
                tessellator.Brightness = block.minZ > 0.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y, z - 1);
                tessellator.setColorOpaque_F(eastR, eastG, eastB);
                i28 = block.getBlockTexture(this.blockAccess, x, y, z, 2);
                this.renderEastFace(block, (double)x, (double)y, (double)z, i28);
                if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
                {
                    tessellator.setColorOpaque_F(eastR * r, eastG * g, eastB * b);
                    this.renderEastFace(block, (double)x, (double)y, (double)z, 38);
                }

                z9 = true;
            }

            // West face
            if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x, y, z + 1, 3))
            {
                tessellator.Brightness = block.maxZ < 1.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x, y, z + 1);
                tessellator.setColorOpaque_F(eastR, eastG, eastB);
                i28 = block.getBlockTexture(this.blockAccess, x, y, z, 3);
                this.renderWestFace(block, (double)x, (double)y, (double)z, i28);
                if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
                {
                    tessellator.setColorOpaque_F(eastR * r, eastG * g, eastB * b);
                    this.renderWestFace(block, (double)x, (double)y, (double)z, 38);
                }

                z9 = true;
            }

            // North face
            if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x - 1, y, z, 4))
            {
                tessellator.Brightness = block.minX > 0.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x - 1, y, z);
                tessellator.setColorOpaque_F(northR, northG, northB);
                i28 = block.getBlockTexture(this.blockAccess, x, y, z, 4);
                this.renderNorthFace(block, (double)x, (double)y, (double)z, i28);
                if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
                {
                    tessellator.setColorOpaque_F(northR * r, northG * g, northB * b);
                    this.renderNorthFace(block, (double)x, (double)y, (double)z, 38);
                }

                z9 = true;
            }

            // South face
            if (this.renderAllFaces || block.shouldSideBeRendered(this.blockAccess, x + 1, y, z, 5))
            {
                tessellator.Brightness = block.maxX < 1.0D ? blockBrightness : block.GetMixedBrightnessForBlock(this.blockAccess, x + 1, y, z);
                tessellator.setColorOpaque_F(northR, northG, northB);
                i28 = block.getBlockTexture(this.blockAccess, x, y, z, 5);
                this.renderSouthFace(block, (double)x, (double)y, (double)z, i28);
                if (fancyGrass && i28 == 3 && this.overrideBlockTexture < 0)
                {
                    tessellator.setColorOpaque_F(northR * r, northG * g, northB * b);
                    this.renderSouthFace(block, (double)x, (double)y, (double)z, 38);
                }

                z9 = true;
            }

            return z9;
        }

        public virtual bool renderBlockCactus(Block block1, int i2, int i3, int i4)
		{
			int i5 = block1.colorMultiplier(this.blockAccess, i2, i3, i4);
			float f6 = (float)(i5 >> 16 & 255) / 255.0F;
			float f7 = (float)(i5 >> 8 & 255) / 255.0F;
			float f8 = (float)(i5 & 255) / 255.0F;

			return this.renderBlockCactusImpl(block1, i2, i3, i4, f6, f7, f8);
		}

		public virtual bool renderBlockCactusImpl(Block block1, int i2, int i3, int i4, float f5, float f6, float f7)
		{
			Tessellator tessellator8 = Tessellator.instance;
			bool z9 = false;
			float f10 = 0.5F;
			float f11 = 1.0F;
			float f12 = 0.8F;
			float f13 = 0.6F;
			float f14 = f10 * f5;
			float f15 = f11 * f5;
			float f16 = f12 * f5;
			float f17 = f13 * f5;
			float f18 = f10 * f6;
			float f19 = f11 * f6;
			float f20 = f12 * f6;
			float f21 = f13 * f6;
			float f22 = f10 * f7;
			float f23 = f11 * f7;
			float f24 = f12 * f7;
			float f25 = f13 * f7;
			float f26 = 0.0625F;
			int i28 = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			if (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2, i3 - 1, i4, 0))
			{
				tessellator8.Brightness = block1.minY > 0.0D ? i28 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3 - 1, i4);
				tessellator8.setColorOpaque_F(f14, f18, f22);
				this.renderBottomFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 0));
				z9 = true;
			}

			if (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2, i3 + 1, i4, 1))
			{
				tessellator8.Brightness = block1.maxY < 1.0D ? i28 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3 + 1, i4);
				tessellator8.setColorOpaque_F(f15, f19, f23);
				this.renderTopFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 1));
				z9 = true;
			}

			if (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2, i3, i4 - 1, 2))
			{
				tessellator8.Brightness = block1.minZ > 0.0D ? i28 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4 - 1);
				tessellator8.setColorOpaque_F(f16, f20, f24);
				tessellator8.addTranslation(0.0F, 0.0F, f26);
				this.renderEastFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 2));
				tessellator8.addTranslation(0.0F, 0.0F, -f26);
				z9 = true;
			}

			if (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2, i3, i4 + 1, 3))
			{
				tessellator8.Brightness = block1.maxZ < 1.0D ? i28 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4 + 1);
				tessellator8.setColorOpaque_F(f16, f20, f24);
				tessellator8.addTranslation(0.0F, 0.0F, -f26);
				this.renderWestFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 3));
				tessellator8.addTranslation(0.0F, 0.0F, f26);
				z9 = true;
			}

			if (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2 - 1, i3, i4, 4))
			{
				tessellator8.Brightness = block1.minX > 0.0D ? i28 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2 - 1, i3, i4);
				tessellator8.setColorOpaque_F(f17, f21, f25);
				tessellator8.addTranslation(f26, 0.0F, 0.0F);
				this.renderNorthFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 4));
				tessellator8.addTranslation(-f26, 0.0F, 0.0F);
				z9 = true;
			}

			if (this.renderAllFaces || block1.shouldSideBeRendered(this.blockAccess, i2 + 1, i3, i4, 5))
			{
				tessellator8.Brightness = block1.maxX < 1.0D ? i28 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2 + 1, i3, i4);
				tessellator8.setColorOpaque_F(f17, f21, f25);
				tessellator8.addTranslation(-f26, 0.0F, 0.0F);
				this.renderSouthFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 5));
				tessellator8.addTranslation(f26, 0.0F, 0.0F);
				z9 = true;
			}

			return z9;
		}

		public virtual bool renderBlockFence(BlockFence blockFence1, int i2, int i3, int i4)
		{
			bool z5 = false;
			float f6 = 0.375F;
			float f7 = 0.625F;
			blockFence1.setBlockBounds(f6, 0.0F, f6, f7, 1.0F, f7);
			this.renderStandardBlock(blockFence1, i2, i3, i4);
			z5 = true;
			bool z8 = false;
			bool z9 = false;
			if (blockFence1.canConnectFenceTo(this.blockAccess, i2 - 1, i3, i4) || blockFence1.canConnectFenceTo(this.blockAccess, i2 + 1, i3, i4))
			{
				z8 = true;
			}

			if (blockFence1.canConnectFenceTo(this.blockAccess, i2, i3, i4 - 1) || blockFence1.canConnectFenceTo(this.blockAccess, i2, i3, i4 + 1))
			{
				z9 = true;
			}

			bool z10 = blockFence1.canConnectFenceTo(this.blockAccess, i2 - 1, i3, i4);
			bool z11 = blockFence1.canConnectFenceTo(this.blockAccess, i2 + 1, i3, i4);
			bool z12 = blockFence1.canConnectFenceTo(this.blockAccess, i2, i3, i4 - 1);
			bool z13 = blockFence1.canConnectFenceTo(this.blockAccess, i2, i3, i4 + 1);
			if (!z8 && !z9)
			{
				z8 = true;
			}

			f6 = 0.4375F;
			f7 = 0.5625F;
			float f14 = 0.75F;
			float f15 = 0.9375F;
			float f16 = z10 ? 0.0F : f6;
			float f17 = z11 ? 1.0F : f7;
			float f18 = z12 ? 0.0F : f6;
			float f19 = z13 ? 1.0F : f7;
			if (z8)
			{
				blockFence1.setBlockBounds(f16, f14, f6, f17, f15, f7);
				this.renderStandardBlock(blockFence1, i2, i3, i4);
				z5 = true;
			}

			if (z9)
			{
				blockFence1.setBlockBounds(f6, f14, f18, f7, f15, f19);
				this.renderStandardBlock(blockFence1, i2, i3, i4);
				z5 = true;
			}

			f14 = 0.375F;
			f15 = 0.5625F;
			if (z8)
			{
				blockFence1.setBlockBounds(f16, f14, f6, f17, f15, f7);
				this.renderStandardBlock(blockFence1, i2, i3, i4);
				z5 = true;
			}

			if (z9)
			{
				blockFence1.setBlockBounds(f6, f14, f18, f7, f15, f19);
				this.renderStandardBlock(blockFence1, i2, i3, i4);
				z5 = true;
			}

			blockFence1.setBlockBoundsBasedOnState(this.blockAccess, i2, i3, i4);
			return z5;
		}

		public virtual bool renderBlockDragonEgg(BlockDragonEgg blockDragonEgg1, int i2, int i3, int i4)
		{
			bool z5 = false;
			int i6 = 0;

			for (int i7 = 0; i7 < 8; ++i7)
			{
				sbyte b8 = 0;
				sbyte b9 = 1;
				if (i7 == 0)
				{
					b8 = 2;
				}

				if (i7 == 1)
				{
					b8 = 3;
				}

				if (i7 == 2)
				{
					b8 = 4;
				}

				if (i7 == 3)
				{
					b8 = 5;
					b9 = 2;
				}

				if (i7 == 4)
				{
					b8 = 6;
					b9 = 3;
				}

				if (i7 == 5)
				{
					b8 = 7;
					b9 = 5;
				}

				if (i7 == 6)
				{
					b8 = 6;
					b9 = 2;
				}

				if (i7 == 7)
				{
					b8 = 3;
				}

				float f10 = (float)b8 / 16.0F;
				float f11 = 1.0F - (float)i6 / 16.0F;
				float f12 = 1.0F - (float)(i6 + b9) / 16.0F;
				i6 += b9;
				blockDragonEgg1.setBlockBounds(0.5F - f10, f12, 0.5F - f10, 0.5F + f10, f11, 0.5F + f10);
				this.renderStandardBlock(blockDragonEgg1, i2, i3, i4);
			}

			z5 = true;
			blockDragonEgg1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			return z5;
		}

		public virtual bool renderBlockFenceGate(BlockFenceGate blockFenceGate1, int i2, int i3, int i4)
		{
			bool z5 = true;
			int i6 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			bool z7 = BlockFenceGate.isFenceGateOpen(i6);
			int i8 = BlockDirectional.getDirection(i6);
			float f15;
			float f16;
			float f17;
			float f18;
			if (i8 != 3 && i8 != 1)
			{
				f15 = 0.0F;
				f16 = 0.125F;
				f17 = 0.4375F;
				f18 = 0.5625F;
				blockFenceGate1.setBlockBounds(f15, 0.3125F, f17, f16, 1.0F, f18);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				f15 = 0.875F;
				f16 = 1.0F;
				blockFenceGate1.setBlockBounds(f15, 0.3125F, f17, f16, 1.0F, f18);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
			}
			else
			{
				f15 = 0.4375F;
				f16 = 0.5625F;
				f17 = 0.0F;
				f18 = 0.125F;
				blockFenceGate1.setBlockBounds(f15, 0.3125F, f17, f16, 1.0F, f18);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				f17 = 0.875F;
				f18 = 1.0F;
				blockFenceGate1.setBlockBounds(f15, 0.3125F, f17, f16, 1.0F, f18);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
			}

			if (!z7)
			{
				if (i8 != 3 && i8 != 1)
				{
					f15 = 0.375F;
					f16 = 0.5F;
					f17 = 0.4375F;
					f18 = 0.5625F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					f15 = 0.5F;
					f16 = 0.625F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					f15 = 0.625F;
					f16 = 0.875F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.5625F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					blockFenceGate1.setBlockBounds(f15, 0.75F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					f15 = 0.125F;
					f16 = 0.375F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.5625F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					blockFenceGate1.setBlockBounds(f15, 0.75F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				}
				else
				{
					f15 = 0.4375F;
					f16 = 0.5625F;
					f17 = 0.375F;
					f18 = 0.5F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					f17 = 0.5F;
					f18 = 0.625F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					f17 = 0.625F;
					f18 = 0.875F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.5625F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					blockFenceGate1.setBlockBounds(f15, 0.75F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					f17 = 0.125F;
					f18 = 0.375F;
					blockFenceGate1.setBlockBounds(f15, 0.375F, f17, f16, 0.5625F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
					blockFenceGate1.setBlockBounds(f15, 0.75F, f17, f16, 0.9375F, f18);
					this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				}
			}
			else if (i8 == 3)
			{
				blockFenceGate1.setBlockBounds(0.8125F, 0.375F, 0.0F, 0.9375F, 0.9375F, 0.125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.8125F, 0.375F, 0.875F, 0.9375F, 0.9375F, 1.0F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.5625F, 0.375F, 0.0F, 0.8125F, 0.5625F, 0.125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.5625F, 0.375F, 0.875F, 0.8125F, 0.5625F, 1.0F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.5625F, 0.75F, 0.0F, 0.8125F, 0.9375F, 0.125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.5625F, 0.75F, 0.875F, 0.8125F, 0.9375F, 1.0F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
			}
			else if (i8 == 1)
			{
				blockFenceGate1.setBlockBounds(0.0625F, 0.375F, 0.0F, 0.1875F, 0.9375F, 0.125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.0625F, 0.375F, 0.875F, 0.1875F, 0.9375F, 1.0F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.1875F, 0.375F, 0.0F, 0.4375F, 0.5625F, 0.125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.1875F, 0.375F, 0.875F, 0.4375F, 0.5625F, 1.0F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.1875F, 0.75F, 0.0F, 0.4375F, 0.9375F, 0.125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.1875F, 0.75F, 0.875F, 0.4375F, 0.9375F, 1.0F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
			}
			else if (i8 == 0)
			{
				blockFenceGate1.setBlockBounds(0.0F, 0.375F, 0.8125F, 0.125F, 0.9375F, 0.9375F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.875F, 0.375F, 0.8125F, 1.0F, 0.9375F, 0.9375F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.0F, 0.375F, 0.5625F, 0.125F, 0.5625F, 0.8125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.875F, 0.375F, 0.5625F, 1.0F, 0.5625F, 0.8125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.0F, 0.75F, 0.5625F, 0.125F, 0.9375F, 0.8125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.875F, 0.75F, 0.5625F, 1.0F, 0.9375F, 0.8125F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
			}
			else if (i8 == 2)
			{
				blockFenceGate1.setBlockBounds(0.0F, 0.375F, 0.0625F, 0.125F, 0.9375F, 0.1875F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.875F, 0.375F, 0.0625F, 1.0F, 0.9375F, 0.1875F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.0F, 0.375F, 0.1875F, 0.125F, 0.5625F, 0.4375F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.875F, 0.375F, 0.1875F, 1.0F, 0.5625F, 0.4375F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.0F, 0.75F, 0.1875F, 0.125F, 0.9375F, 0.4375F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
				blockFenceGate1.setBlockBounds(0.875F, 0.75F, 0.1875F, 1.0F, 0.9375F, 0.4375F);
				this.renderStandardBlock(blockFenceGate1, i2, i3, i4);
			}

			blockFenceGate1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			return z5;
		}

		public virtual bool renderBlockStairs(Block block1, int i2, int i3, int i4)
		{
			int i5 = this.blockAccess.getBlockMetadata(i2, i3, i4);
			int i6 = i5 & 3;
			float f7 = 0.0F;
			float f8 = 0.5F;
			float f9 = 0.5F;
			float f10 = 1.0F;
			if ((i5 & 4) != 0)
			{
				f7 = 0.5F;
				f8 = 1.0F;
				f9 = 0.0F;
				f10 = 0.5F;
			}

			block1.setBlockBounds(0.0F, f7, 0.0F, 1.0F, f8, 1.0F);
			this.renderStandardBlock(block1, i2, i3, i4);
			if (i6 == 0)
			{
				block1.setBlockBounds(0.5F, f9, 0.0F, 1.0F, f10, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
			}
			else if (i6 == 1)
			{
				block1.setBlockBounds(0.0F, f9, 0.0F, 0.5F, f10, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
			}
			else if (i6 == 2)
			{
				block1.setBlockBounds(0.0F, f9, 0.5F, 1.0F, f10, 1.0F);
				this.renderStandardBlock(block1, i2, i3, i4);
			}
			else if (i6 == 3)
			{
				block1.setBlockBounds(0.0F, f9, 0.0F, 1.0F, f10, 0.5F);
				this.renderStandardBlock(block1, i2, i3, i4);
			}

			block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			return true;
		}

		public virtual bool renderBlockDoor(Block block1, int i2, int i3, int i4)
		{
			Tessellator tessellator5 = Tessellator.instance;
			BlockDoor blockDoor6 = (BlockDoor)block1;
			bool z7 = false;
			float f8 = 0.5F;
			float f9 = 1.0F;
			float f10 = 0.8F;
			float f11 = 0.6F;
			int i12 = block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4);
			tessellator5.Brightness = block1.minY > 0.0D ? i12 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3 - 1, i4);
			tessellator5.setColorOpaque_F(f8, f8, f8);
			this.renderBottomFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 0));
			z7 = true;
			tessellator5.Brightness = block1.maxY < 1.0D ? i12 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3 + 1, i4);
			tessellator5.setColorOpaque_F(f9, f9, f9);
			this.renderTopFace(block1, (double)i2, (double)i3, (double)i4, block1.getBlockTexture(this.blockAccess, i2, i3, i4, 1));
			z7 = true;
			tessellator5.Brightness = block1.minZ > 0.0D ? i12 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4 - 1);
			tessellator5.setColorOpaque_F(f10, f10, f10);
			int i14 = block1.getBlockTexture(this.blockAccess, i2, i3, i4, 2);
			if (i14 < 0)
			{
				this.flipTexture = true;
				i14 = -i14;
			}

			this.renderEastFace(block1, (double)i2, (double)i3, (double)i4, i14);
			z7 = true;
			this.flipTexture = false;
			tessellator5.Brightness = block1.maxZ < 1.0D ? i12 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2, i3, i4 + 1);
			tessellator5.setColorOpaque_F(f10, f10, f10);
			i14 = block1.getBlockTexture(this.blockAccess, i2, i3, i4, 3);
			if (i14 < 0)
			{
				this.flipTexture = true;
				i14 = -i14;
			}

			this.renderWestFace(block1, (double)i2, (double)i3, (double)i4, i14);
			z7 = true;
			this.flipTexture = false;
			tessellator5.Brightness = block1.minX > 0.0D ? i12 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2 - 1, i3, i4);
			tessellator5.setColorOpaque_F(f11, f11, f11);
			i14 = block1.getBlockTexture(this.blockAccess, i2, i3, i4, 4);
			if (i14 < 0)
			{
				this.flipTexture = true;
				i14 = -i14;
			}

			this.renderNorthFace(block1, (double)i2, (double)i3, (double)i4, i14);
			z7 = true;
			this.flipTexture = false;
			tessellator5.Brightness = block1.maxX < 1.0D ? i12 : block1.GetMixedBrightnessForBlock(this.blockAccess, i2 + 1, i3, i4);
			tessellator5.setColorOpaque_F(f11, f11, f11);
			i14 = block1.getBlockTexture(this.blockAccess, i2, i3, i4, 5);
			if (i14 < 0)
			{
				this.flipTexture = true;
				i14 = -i14;
			}

			this.renderSouthFace(block1, (double)i2, (double)i3, (double)i4, i14);
			z7 = true;
			this.flipTexture = false;
			return z7;
		}

		public virtual void renderBottomFace(Block block1, double d2, double d4, double d6, int i8)
		{
			Tessellator tessellator9 = Tessellator.instance;
			if (this.overrideBlockTexture >= 0)
			{
				i8 = this.overrideBlockTexture;
			}

			int i10 = (i8 & 15) << 4;
			int i11 = i8 & 240;
			double d12 = ((double)i10 + block1.minX * 16.0D) / 256.0D;
			double d14 = ((double)i10 + block1.maxX * 16.0D - 0.01D) / 256.0D;
			double d16 = ((double)i11 + block1.minZ * 16.0D) / 256.0D;
			double d18 = ((double)i11 + block1.maxZ * 16.0D - 0.01D) / 256.0D;
			if (block1.minX < 0.0D || block1.maxX > 1.0D)
			{
				d12 = (double)(((float)i10 + 0.0F) / 256.0F);
				d14 = (double)(((float)i10 + 15.99F) / 256.0F);
			}

			if (block1.minZ < 0.0D || block1.maxZ > 1.0D)
			{
				d16 = (double)(((float)i11 + 0.0F) / 256.0F);
				d18 = (double)(((float)i11 + 15.99F) / 256.0F);
			}

			double d20 = d14;
			double d22 = d12;
			double d24 = d16;
			double d26 = d18;
			if (this.uvRotateBottom == 2)
			{
				d12 = ((double)i10 + block1.minZ * 16.0D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.maxX * 16.0D) / 256.0D;
				d14 = ((double)i10 + block1.maxZ * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.minX * 16.0D) / 256.0D;
				d24 = d16;
				d26 = d18;
				d20 = d12;
				d22 = d14;
				d16 = d18;
				d18 = d24;
			}
			else if (this.uvRotateBottom == 1)
			{
				d12 = ((double)(i10 + 16) - block1.maxZ * 16.0D) / 256.0D;
				d16 = ((double)i11 + block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.minZ * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.maxX * 16.0D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d12 = d14;
				d14 = d22;
				d24 = d18;
				d26 = d16;
			}
			else if (this.uvRotateBottom == 3)
			{
				d12 = ((double)(i10 + 16) - block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.maxX * 16.0D - 0.01D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.minZ * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.maxZ * 16.0D - 0.01D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d24 = d16;
				d26 = d18;
			}

			double d28 = d2 + block1.minX;
			double d30 = d2 + block1.maxX;
			double d32 = d4 + block1.minY;
			double d34 = d6 + block1.minZ;
			double d36 = d6 + block1.maxZ;
			if (this.enableAO)
			{
				tessellator9.setColorOpaque_F(this.colorRedTopLeft, this.colorGreenTopLeft, this.colorBlueTopLeft);
				tessellator9.Brightness = this.brightnessTopLeft;
				tessellator9.AddVertexWithUV(d28, d32, d36, d22, d26);
				tessellator9.setColorOpaque_F(this.colorRedBottomLeft, this.colorGreenBottomLeft, this.colorBlueBottomLeft);
				tessellator9.Brightness = this.brightnessBottomLeft;
				tessellator9.AddVertexWithUV(d28, d32, d34, d12, d16);
				tessellator9.setColorOpaque_F(this.colorRedBottomRight, this.colorGreenBottomRight, this.colorBlueBottomRight);
				tessellator9.Brightness = this.brightnessBottomRight;
				tessellator9.AddVertexWithUV(d30, d32, d34, d20, d24);
				tessellator9.setColorOpaque_F(this.colorRedTopRight, this.colorGreenTopRight, this.colorBlueTopRight);
				tessellator9.Brightness = this.brightnessTopRight;
				tessellator9.AddVertexWithUV(d30, d32, d36, d14, d18);
			}
			else
			{
				tessellator9.AddVertexWithUV(d28, d32, d36, d22, d26);
				tessellator9.AddVertexWithUV(d28, d32, d34, d12, d16);
				tessellator9.AddVertexWithUV(d30, d32, d34, d20, d24);
				tessellator9.AddVertexWithUV(d30, d32, d36, d14, d18);
			}

		}

		public virtual void renderTopFace(Block block1, double d2, double d4, double d6, int i8)
		{
			Tessellator tessellator9 = Tessellator.instance;
			if (this.overrideBlockTexture >= 0)
			{
				i8 = this.overrideBlockTexture;
			}

			int i10 = (i8 & 15) << 4;
			int i11 = i8 & 240;
			double d12 = ((double)i10 + block1.minX * 16.0D) / 256.0D;
			double d14 = ((double)i10 + block1.maxX * 16.0D - 0.01D) / 256.0D;
			double d16 = ((double)i11 + block1.minZ * 16.0D) / 256.0D;
			double d18 = ((double)i11 + block1.maxZ * 16.0D - 0.01D) / 256.0D;
			if (block1.minX < 0.0D || block1.maxX > 1.0D)
			{
				d12 = (double)(((float)i10 + 0.0F) / 256.0F);
				d14 = (double)(((float)i10 + 15.99F) / 256.0F);
			}

			if (block1.minZ < 0.0D || block1.maxZ > 1.0D)
			{
				d16 = (double)(((float)i11 + 0.0F) / 256.0F);
				d18 = (double)(((float)i11 + 15.99F) / 256.0F);
			}

			double d20 = d14;
			double d22 = d12;
			double d24 = d16;
			double d26 = d18;
			if (this.uvRotateTop == 1)
			{
				d12 = ((double)i10 + block1.minZ * 16.0D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.maxX * 16.0D) / 256.0D;
				d14 = ((double)i10 + block1.maxZ * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.minX * 16.0D) / 256.0D;
				d24 = d16;
				d26 = d18;
				d20 = d12;
				d22 = d14;
				d16 = d18;
				d18 = d24;
			}
			else if (this.uvRotateTop == 2)
			{
				d12 = ((double)(i10 + 16) - block1.maxZ * 16.0D) / 256.0D;
				d16 = ((double)i11 + block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.minZ * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.maxX * 16.0D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d12 = d14;
				d14 = d22;
				d24 = d18;
				d26 = d16;
			}
			else if (this.uvRotateTop == 3)
			{
				d12 = ((double)(i10 + 16) - block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.maxX * 16.0D - 0.01D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.minZ * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.maxZ * 16.0D - 0.01D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d24 = d16;
				d26 = d18;
			}

			double d28 = d2 + block1.minX;
			double d30 = d2 + block1.maxX;
			double d32 = d4 + block1.maxY;
			double d34 = d6 + block1.minZ;
			double d36 = d6 + block1.maxZ;
			if (this.enableAO)
			{
				tessellator9.setColorOpaque_F(this.colorRedTopLeft, this.colorGreenTopLeft, this.colorBlueTopLeft);
				tessellator9.Brightness = this.brightnessTopLeft;
				tessellator9.AddVertexWithUV(d30, d32, d36, d14, d18);
				tessellator9.setColorOpaque_F(this.colorRedBottomLeft, this.colorGreenBottomLeft, this.colorBlueBottomLeft);
				tessellator9.Brightness = this.brightnessBottomLeft;
				tessellator9.AddVertexWithUV(d30, d32, d34, d20, d24);
				tessellator9.setColorOpaque_F(this.colorRedBottomRight, this.colorGreenBottomRight, this.colorBlueBottomRight);
				tessellator9.Brightness = this.brightnessBottomRight;
				tessellator9.AddVertexWithUV(d28, d32, d34, d12, d16);
				tessellator9.setColorOpaque_F(this.colorRedTopRight, this.colorGreenTopRight, this.colorBlueTopRight);
				tessellator9.Brightness = this.brightnessTopRight;
				tessellator9.AddVertexWithUV(d28, d32, d36, d22, d26);
			}
			else
			{
				tessellator9.AddVertexWithUV(d30, d32, d36, d14, d18);
				tessellator9.AddVertexWithUV(d30, d32, d34, d20, d24);
				tessellator9.AddVertexWithUV(d28, d32, d34, d12, d16);
				tessellator9.AddVertexWithUV(d28, d32, d36, d22, d26);
			}

		}

		public virtual void renderEastFace(Block block1, double d2, double d4, double d6, int i8)
		{
			Tessellator tessellator9 = Tessellator.instance;
			if (this.overrideBlockTexture >= 0)
			{
				i8 = this.overrideBlockTexture;
			}

			int i10 = (i8 & 15) << 4;
			int i11 = i8 & 240;
			double d12 = ((double)i10 + block1.minX * 16.0D) / 256.0D;
			double d14 = ((double)i10 + block1.maxX * 16.0D - 0.01D) / 256.0D;
			double d16 = ((double)(i11 + 16) - block1.maxY * 16.0D) / 256.0D;
			double d18 = ((double)(i11 + 16) - block1.minY * 16.0D - 0.01D) / 256.0D;
			double d20;
			if (this.flipTexture)
			{
				d20 = d12;
				d12 = d14;
				d14 = d20;
			}

			if (block1.minX < 0.0D || block1.maxX > 1.0D)
			{
				d12 = (double)(((float)i10 + 0.0F) / 256.0F);
				d14 = (double)(((float)i10 + 15.99F) / 256.0F);
			}

			if (block1.minY < 0.0D || block1.maxY > 1.0D)
			{
				d16 = (double)(((float)i11 + 0.0F) / 256.0F);
				d18 = (double)(((float)i11 + 15.99F) / 256.0F);
			}

			d20 = d14;
			double d22 = d12;
			double d24 = d16;
			double d26 = d18;
			if (this.uvRotateEast == 2)
			{
				d12 = ((double)i10 + block1.minY * 16.0D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.minX * 16.0D) / 256.0D;
				d14 = ((double)i10 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.maxX * 16.0D) / 256.0D;
				d24 = d16;
				d26 = d18;
				d20 = d12;
				d22 = d14;
				d16 = d18;
				d18 = d24;
			}
			else if (this.uvRotateEast == 1)
			{
				d12 = ((double)(i10 + 16) - block1.maxY * 16.0D) / 256.0D;
				d16 = ((double)i11 + block1.maxX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.minY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.minX * 16.0D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d12 = d14;
				d14 = d22;
				d24 = d18;
				d26 = d16;
			}
			else if (this.uvRotateEast == 3)
			{
				d12 = ((double)(i10 + 16) - block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.maxX * 16.0D - 0.01D) / 256.0D;
				d16 = ((double)i11 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.minY * 16.0D - 0.01D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d24 = d16;
				d26 = d18;
			}

			double d28 = d2 + block1.minX;
			double d30 = d2 + block1.maxX;
			double d32 = d4 + block1.minY;
			double d34 = d4 + block1.maxY;
			double d36 = d6 + block1.minZ;
			if (this.enableAO)
			{
				tessellator9.setColorOpaque_F(this.colorRedTopLeft, this.colorGreenTopLeft, this.colorBlueTopLeft);
				tessellator9.Brightness = this.brightnessTopLeft;
				tessellator9.AddVertexWithUV(d28, d34, d36, d20, d24);
				tessellator9.setColorOpaque_F(this.colorRedBottomLeft, this.colorGreenBottomLeft, this.colorBlueBottomLeft);
				tessellator9.Brightness = this.brightnessBottomLeft;
				tessellator9.AddVertexWithUV(d30, d34, d36, d12, d16);
				tessellator9.setColorOpaque_F(this.colorRedBottomRight, this.colorGreenBottomRight, this.colorBlueBottomRight);
				tessellator9.Brightness = this.brightnessBottomRight;
				tessellator9.AddVertexWithUV(d30, d32, d36, d22, d26);
				tessellator9.setColorOpaque_F(this.colorRedTopRight, this.colorGreenTopRight, this.colorBlueTopRight);
				tessellator9.Brightness = this.brightnessTopRight;
				tessellator9.AddVertexWithUV(d28, d32, d36, d14, d18);
			}
			else
			{
				tessellator9.AddVertexWithUV(d28, d34, d36, d20, d24);
				tessellator9.AddVertexWithUV(d30, d34, d36, d12, d16);
				tessellator9.AddVertexWithUV(d30, d32, d36, d22, d26);
				tessellator9.AddVertexWithUV(d28, d32, d36, d14, d18);
			}

		}

		public virtual void renderWestFace(Block block1, double d2, double d4, double d6, int i8)
		{
			Tessellator tessellator9 = Tessellator.instance;
			if (this.overrideBlockTexture >= 0)
			{
				i8 = this.overrideBlockTexture;
			}

			int i10 = (i8 & 15) << 4;
			int i11 = i8 & 240;
			double d12 = ((double)i10 + block1.minX * 16.0D) / 256.0D;
			double d14 = ((double)i10 + block1.maxX * 16.0D - 0.01D) / 256.0D;
			double d16 = ((double)(i11 + 16) - block1.maxY * 16.0D) / 256.0D;
			double d18 = ((double)(i11 + 16) - block1.minY * 16.0D - 0.01D) / 256.0D;
			double d20;
			if (this.flipTexture)
			{
				d20 = d12;
				d12 = d14;
				d14 = d20;
			}

			if (block1.minX < 0.0D || block1.maxX > 1.0D)
			{
				d12 = (double)(((float)i10 + 0.0F) / 256.0F);
				d14 = (double)(((float)i10 + 15.99F) / 256.0F);
			}

			if (block1.minY < 0.0D || block1.maxY > 1.0D)
			{
				d16 = (double)(((float)i11 + 0.0F) / 256.0F);
				d18 = (double)(((float)i11 + 15.99F) / 256.0F);
			}

			d20 = d14;
			double d22 = d12;
			double d24 = d16;
			double d26 = d18;
			if (this.uvRotateWest == 1)
			{
				d12 = ((double)i10 + block1.minY * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.minX * 16.0D) / 256.0D;
				d14 = ((double)i10 + block1.maxY * 16.0D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.maxX * 16.0D) / 256.0D;
				d24 = d16;
				d26 = d18;
				d20 = d12;
				d22 = d14;
				d16 = d18;
				d18 = d24;
			}
			else if (this.uvRotateWest == 2)
			{
				d12 = ((double)(i10 + 16) - block1.maxY * 16.0D) / 256.0D;
				d16 = ((double)i11 + block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.minY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.maxX * 16.0D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d12 = d14;
				d14 = d22;
				d24 = d18;
				d26 = d16;
			}
			else if (this.uvRotateWest == 3)
			{
				d12 = ((double)(i10 + 16) - block1.minX * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.maxX * 16.0D - 0.01D) / 256.0D;
				d16 = ((double)i11 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.minY * 16.0D - 0.01D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d24 = d16;
				d26 = d18;
			}

			double d28 = d2 + block1.minX;
			double d30 = d2 + block1.maxX;
			double d32 = d4 + block1.minY;
			double d34 = d4 + block1.maxY;
			double d36 = d6 + block1.maxZ;
			if (this.enableAO)
			{
				tessellator9.setColorOpaque_F(this.colorRedTopLeft, this.colorGreenTopLeft, this.colorBlueTopLeft);
				tessellator9.Brightness = this.brightnessTopLeft;
				tessellator9.AddVertexWithUV(d28, d34, d36, d12, d16);
				tessellator9.setColorOpaque_F(this.colorRedBottomLeft, this.colorGreenBottomLeft, this.colorBlueBottomLeft);
				tessellator9.Brightness = this.brightnessBottomLeft;
				tessellator9.AddVertexWithUV(d28, d32, d36, d22, d26);
				tessellator9.setColorOpaque_F(this.colorRedBottomRight, this.colorGreenBottomRight, this.colorBlueBottomRight);
				tessellator9.Brightness = this.brightnessBottomRight;
				tessellator9.AddVertexWithUV(d30, d32, d36, d14, d18);
				tessellator9.setColorOpaque_F(this.colorRedTopRight, this.colorGreenTopRight, this.colorBlueTopRight);
				tessellator9.Brightness = this.brightnessTopRight;
				tessellator9.AddVertexWithUV(d30, d34, d36, d20, d24);
			}
			else
			{
				tessellator9.AddVertexWithUV(d28, d34, d36, d12, d16);
				tessellator9.AddVertexWithUV(d28, d32, d36, d22, d26);
				tessellator9.AddVertexWithUV(d30, d32, d36, d14, d18);
				tessellator9.AddVertexWithUV(d30, d34, d36, d20, d24);
			}

		}

		public virtual void renderNorthFace(Block block1, double d2, double d4, double d6, int i8)
		{
			Tessellator tessellator9 = Tessellator.instance;
			if (this.overrideBlockTexture >= 0)
			{
				i8 = this.overrideBlockTexture;
			}

			int i10 = (i8 & 15) << 4;
			int i11 = i8 & 240;
			double d12 = ((double)i10 + block1.minZ * 16.0D) / 256.0D;
			double d14 = ((double)i10 + block1.maxZ * 16.0D - 0.01D) / 256.0D;
			double d16 = ((double)(i11 + 16) - block1.maxY * 16.0D) / 256.0D;
			double d18 = ((double)(i11 + 16) - block1.minY * 16.0D - 0.01D) / 256.0D;
			double d20;
			if (this.flipTexture)
			{
				d20 = d12;
				d12 = d14;
				d14 = d20;
			}

			if (block1.minZ < 0.0D || block1.maxZ > 1.0D)
			{
				d12 = (double)(((float)i10 + 0.0F) / 256.0F);
				d14 = (double)(((float)i10 + 15.99F) / 256.0F);
			}

			if (block1.minY < 0.0D || block1.maxY > 1.0D)
			{
				d16 = (double)(((float)i11 + 0.0F) / 256.0F);
				d18 = (double)(((float)i11 + 15.99F) / 256.0F);
			}

			d20 = d14;
			double d22 = d12;
			double d24 = d16;
			double d26 = d18;
			if (this.uvRotateNorth == 1)
			{
				d12 = ((double)i10 + block1.minY * 16.0D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.maxZ * 16.0D) / 256.0D;
				d14 = ((double)i10 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.minZ * 16.0D) / 256.0D;
				d24 = d16;
				d26 = d18;
				d20 = d12;
				d22 = d14;
				d16 = d18;
				d18 = d24;
			}
			else if (this.uvRotateNorth == 2)
			{
				d12 = ((double)(i10 + 16) - block1.maxY * 16.0D) / 256.0D;
				d16 = ((double)i11 + block1.minZ * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.minY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.maxZ * 16.0D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d12 = d14;
				d14 = d22;
				d24 = d18;
				d26 = d16;
			}
			else if (this.uvRotateNorth == 3)
			{
				d12 = ((double)(i10 + 16) - block1.minZ * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.maxZ * 16.0D - 0.01D) / 256.0D;
				d16 = ((double)i11 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.minY * 16.0D - 0.01D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d24 = d16;
				d26 = d18;
			}

			double d28 = d2 + block1.minX;
			double d30 = d4 + block1.minY;
			double d32 = d4 + block1.maxY;
			double d34 = d6 + block1.minZ;
			double d36 = d6 + block1.maxZ;
			if (this.enableAO)
			{
				tessellator9.setColorOpaque_F(this.colorRedTopLeft, this.colorGreenTopLeft, this.colorBlueTopLeft);
				tessellator9.Brightness = this.brightnessTopLeft;
				tessellator9.AddVertexWithUV(d28, d32, d36, d20, d24);
				tessellator9.setColorOpaque_F(this.colorRedBottomLeft, this.colorGreenBottomLeft, this.colorBlueBottomLeft);
				tessellator9.Brightness = this.brightnessBottomLeft;
				tessellator9.AddVertexWithUV(d28, d32, d34, d12, d16);
				tessellator9.setColorOpaque_F(this.colorRedBottomRight, this.colorGreenBottomRight, this.colorBlueBottomRight);
				tessellator9.Brightness = this.brightnessBottomRight;
				tessellator9.AddVertexWithUV(d28, d30, d34, d22, d26);
				tessellator9.setColorOpaque_F(this.colorRedTopRight, this.colorGreenTopRight, this.colorBlueTopRight);
				tessellator9.Brightness = this.brightnessTopRight;
				tessellator9.AddVertexWithUV(d28, d30, d36, d14, d18);
			}
			else
			{
				tessellator9.AddVertexWithUV(d28, d32, d36, d20, d24);
				tessellator9.AddVertexWithUV(d28, d32, d34, d12, d16);
				tessellator9.AddVertexWithUV(d28, d30, d34, d22, d26);
				tessellator9.AddVertexWithUV(d28, d30, d36, d14, d18);
			}

		}

		public virtual void renderSouthFace(Block block1, double d2, double d4, double d6, int i8)
		{
			Tessellator tessellator9 = Tessellator.instance;
			if (this.overrideBlockTexture >= 0)
			{
				i8 = this.overrideBlockTexture;
			}

			int i10 = (i8 & 15) << 4;
			int i11 = i8 & 240;
			double d12 = ((double)i10 + block1.minZ * 16.0D) / 256.0D;
			double d14 = ((double)i10 + block1.maxZ * 16.0D - 0.01D) / 256.0D;
			double d16 = ((double)(i11 + 16) - block1.maxY * 16.0D) / 256.0D;
			double d18 = ((double)(i11 + 16) - block1.minY * 16.0D - 0.01D) / 256.0D;
			double d20;
			if (this.flipTexture)
			{
				d20 = d12;
				d12 = d14;
				d14 = d20;
			}

			if (block1.minZ < 0.0D || block1.maxZ > 1.0D)
			{
				d12 = (double)(((float)i10 + 0.0F) / 256.0F);
				d14 = (double)(((float)i10 + 15.99F) / 256.0F);
			}

			if (block1.minY < 0.0D || block1.maxY > 1.0D)
			{
				d16 = (double)(((float)i11 + 0.0F) / 256.0F);
				d18 = (double)(((float)i11 + 15.99F) / 256.0F);
			}

			d20 = d14;
			double d22 = d12;
			double d24 = d16;
			double d26 = d18;
			if (this.uvRotateSouth == 2)
			{
				d12 = ((double)i10 + block1.minY * 16.0D) / 256.0D;
				d16 = ((double)(i11 + 16) - block1.minZ * 16.0D) / 256.0D;
				d14 = ((double)i10 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)(i11 + 16) - block1.maxZ * 16.0D) / 256.0D;
				d24 = d16;
				d26 = d18;
				d20 = d12;
				d22 = d14;
				d16 = d18;
				d18 = d24;
			}
			else if (this.uvRotateSouth == 1)
			{
				d12 = ((double)(i10 + 16) - block1.maxY * 16.0D) / 256.0D;
				d16 = ((double)i11 + block1.maxZ * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.minY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.minZ * 16.0D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d12 = d14;
				d14 = d22;
				d24 = d18;
				d26 = d16;
			}
			else if (this.uvRotateSouth == 3)
			{
				d12 = ((double)(i10 + 16) - block1.minZ * 16.0D) / 256.0D;
				d14 = ((double)(i10 + 16) - block1.maxZ * 16.0D - 0.01D) / 256.0D;
				d16 = ((double)i11 + block1.maxY * 16.0D) / 256.0D;
				d18 = ((double)i11 + block1.minY * 16.0D - 0.01D) / 256.0D;
				d20 = d14;
				d22 = d12;
				d24 = d16;
				d26 = d18;
			}

			double d28 = d2 + block1.maxX;
			double d30 = d4 + block1.minY;
			double d32 = d4 + block1.maxY;
			double d34 = d6 + block1.minZ;
			double d36 = d6 + block1.maxZ;
			if (this.enableAO)
			{
				tessellator9.setColorOpaque_F(this.colorRedTopLeft, this.colorGreenTopLeft, this.colorBlueTopLeft);
				tessellator9.Brightness = this.brightnessTopLeft;
				tessellator9.AddVertexWithUV(d28, d30, d36, d22, d26);
				tessellator9.setColorOpaque_F(this.colorRedBottomLeft, this.colorGreenBottomLeft, this.colorBlueBottomLeft);
				tessellator9.Brightness = this.brightnessBottomLeft;
				tessellator9.AddVertexWithUV(d28, d30, d34, d14, d18);
				tessellator9.setColorOpaque_F(this.colorRedBottomRight, this.colorGreenBottomRight, this.colorBlueBottomRight);
				tessellator9.Brightness = this.brightnessBottomRight;
				tessellator9.AddVertexWithUV(d28, d32, d34, d20, d24);
				tessellator9.setColorOpaque_F(this.colorRedTopRight, this.colorGreenTopRight, this.colorBlueTopRight);
				tessellator9.Brightness = this.brightnessTopRight;
				tessellator9.AddVertexWithUV(d28, d32, d36, d12, d16);
			}
			else
			{
				tessellator9.AddVertexWithUV(d28, d30, d36, d22, d26);
				tessellator9.AddVertexWithUV(d28, d30, d34, d14, d18);
				tessellator9.AddVertexWithUV(d28, d32, d34, d20, d24);
				tessellator9.AddVertexWithUV(d28, d32, d36, d12, d16);
			}

		}

		public virtual void renderBlockAsItem(Block block1, int i2, float f3)
		{
			Tessellator tessellator4 = Tessellator.instance;
			bool z5 = block1.blockID == Block.grass.blockID;
			int i6;
			float f7;
			float f8;
			float f9;
			if (this.useInventoryTint)
			{
				i6 = block1.getRenderColor(i2);
				if (z5)
				{
					i6 = 0xFFFFFF;
				}

				f7 = (float)(i6 >> 16 & 255) / 255.0F;
				f8 = (float)(i6 >> 8 & 255) / 255.0F;
				f9 = (float)(i6 & 255) / 255.0F;
                Minecraft.renderPipeline.SetColor(f7 * f3, f8 * f3, f9 * f3, 1.0F);
			}

			i6 = block1.RenderType;
			int i14;
			if (i6 != 0 && i6 != 16)
			{
				if (i6 == 1)
				{
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
					this.drawCrossedSquares(block1, i2, -0.5D, -0.5D, -0.5D);
					tessellator4.DrawImmediate();
				}
				else if (i6 == 19)
				{
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
					block1.setBlockBoundsForItemRender();
					this.renderBlockStemSmall(block1, i2, block1.maxY, -0.5D, -0.5D, -0.5D);
					tessellator4.DrawImmediate();
				}
				else if (i6 == 23)
				{
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
					block1.setBlockBoundsForItemRender();
					tessellator4.DrawImmediate();
				}
				else if (i6 == 13)
				{
					block1.setBlockBoundsForItemRender();
                    Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
					f7 = 0.0625F;
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
					this.renderBottomFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(0));
					tessellator4.DrawImmediate();
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, 1.0F, 0.0F);
					this.renderTopFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(1));
					tessellator4.DrawImmediate();
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, 0.0F, -1.0F);
					tessellator4.addTranslation(0.0F, 0.0F, f7);
					this.renderEastFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(2));
					tessellator4.addTranslation(0.0F, 0.0F, -f7);
					tessellator4.DrawImmediate();
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, 0.0F, 1.0F);
					tessellator4.addTranslation(0.0F, 0.0F, -f7);
					this.renderWestFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(3));
					tessellator4.addTranslation(0.0F, 0.0F, f7);
					tessellator4.DrawImmediate();
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(-1.0F, 0.0F, 0.0F);
					tessellator4.addTranslation(f7, 0.0F, 0.0F);
					this.renderNorthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(4));
					tessellator4.addTranslation(-f7, 0.0F, 0.0F);
					tessellator4.DrawImmediate();
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(1.0F, 0.0F, 0.0F);
					tessellator4.addTranslation(-f7, 0.0F, 0.0F);
					this.renderSouthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(5));
					tessellator4.addTranslation(f7, 0.0F, 0.0F);
					tessellator4.DrawImmediate();
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
				}
				else if (i6 == 22)
				{
					ChestItemRenderHelper.instance.func_35609_a(block1, i2, f3);
				}
				else if (i6 == 6)
				{
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
					this.renderBlockCropsImpl(block1, i2, -0.5D, -0.5D, -0.5D);
					tessellator4.DrawImmediate();
				}
				else if (i6 == 2)
				{
					tessellator4.startDrawingQuads();
					tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
					this.renderTorchAtAngle(block1, -0.5D, -0.5D, -0.5D, 0.0D, 0.0D);
					tessellator4.DrawImmediate();
				}
				else if (i6 == 10)
				{
					for (i14 = 0; i14 < 2; ++i14)
					{
						if (i14 == 0)
						{
							block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.5F);
						}

						if (i14 == 1)
						{
							block1.setBlockBounds(0.0F, 0.0F, 0.5F, 1.0F, 0.5F, 1.0F);
						}

                        Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
						this.renderBottomFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(0));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 1.0F, 0.0F);
						this.renderTopFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(1));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 0.0F, -1.0F);
						this.renderEastFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(2));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 0.0F, 1.0F);
						this.renderWestFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(3));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(-1.0F, 0.0F, 0.0F);
						this.renderNorthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(4));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(1.0F, 0.0F, 0.0F);
						this.renderSouthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(5));
						tessellator4.DrawImmediate();
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
					}
				}
				else if (i6 == 27)
				{
					i14 = 0;
                    Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
					tessellator4.startDrawingQuads();

					for (int i15 = 0; i15 < 8; ++i15)
					{
						sbyte b16 = 0;
						sbyte b17 = 1;
						if (i15 == 0)
						{
							b16 = 2;
						}

						if (i15 == 1)
						{
							b16 = 3;
						}

						if (i15 == 2)
						{
							b16 = 4;
						}

						if (i15 == 3)
						{
							b16 = 5;
							b17 = 2;
						}

						if (i15 == 4)
						{
							b16 = 6;
							b17 = 3;
						}

						if (i15 == 5)
						{
							b16 = 7;
							b17 = 5;
						}

						if (i15 == 6)
						{
							b16 = 6;
							b17 = 2;
						}

						if (i15 == 7)
						{
							b16 = 3;
						}

						float f11 = (float)b16 / 16.0F;
						float f12 = 1.0F - (float)i14 / 16.0F;
						float f13 = 1.0F - (float)(i14 + b17) / 16.0F;
						i14 += b17;
						block1.setBlockBounds(0.5F - f11, f13, 0.5F - f11, 0.5F + f11, f12, 0.5F + f11);
						tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
						this.renderBottomFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(0));
						tessellator4.SetNormal(0.0F, 1.0F, 0.0F);
						this.renderTopFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(1));
						tessellator4.SetNormal(0.0F, 0.0F, -1.0F);
						this.renderEastFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(2));
						tessellator4.SetNormal(0.0F, 0.0F, 1.0F);
						this.renderWestFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(3));
						tessellator4.SetNormal(-1.0F, 0.0F, 0.0F);
						this.renderNorthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(4));
						tessellator4.SetNormal(1.0F, 0.0F, 0.0F);
						this.renderSouthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(5));
					}

					tessellator4.DrawImmediate();
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
					block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				}
				else if (i6 == 11)
				{
					for (i14 = 0; i14 < 4; ++i14)
					{
						f8 = 0.125F;
						if (i14 == 0)
						{
							block1.setBlockBounds(0.5F - f8, 0.0F, 0.0F, 0.5F + f8, 1.0F, f8 * 2.0F);
						}

						if (i14 == 1)
						{
							block1.setBlockBounds(0.5F - f8, 0.0F, 1.0F - f8 * 2.0F, 0.5F + f8, 1.0F, 1.0F);
						}

						f8 = 0.0625F;
						if (i14 == 2)
						{
							block1.setBlockBounds(0.5F - f8, 1.0F - f8 * 3.0F, -f8 * 2.0F, 0.5F + f8, 1.0F - f8, 1.0F + f8 * 2.0F);
						}

						if (i14 == 3)
						{
							block1.setBlockBounds(0.5F - f8, 0.5F - f8 * 3.0F, -f8 * 2.0F, 0.5F + f8, 0.5F - f8, 1.0F + f8 * 2.0F);
						}

                        Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
						this.renderBottomFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(0));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 1.0F, 0.0F);
						this.renderTopFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(1));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 0.0F, -1.0F);
						this.renderEastFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(2));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 0.0F, 1.0F);
						this.renderWestFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(3));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(-1.0F, 0.0F, 0.0F);
						this.renderNorthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(4));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(1.0F, 0.0F, 0.0F);
						this.renderSouthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(5));
						tessellator4.DrawImmediate();
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
					}

					block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				}
				else if (i6 == 21)
				{
					for (i14 = 0; i14 < 3; ++i14)
					{
						f8 = 0.0625F;
						if (i14 == 0)
						{
							block1.setBlockBounds(0.5F - f8, 0.3F, 0.0F, 0.5F + f8, 1.0F, f8 * 2.0F);
						}

						if (i14 == 1)
						{
							block1.setBlockBounds(0.5F - f8, 0.3F, 1.0F - f8 * 2.0F, 0.5F + f8, 1.0F, 1.0F);
						}

						f8 = 0.0625F;
						if (i14 == 2)
						{
							block1.setBlockBounds(0.5F - f8, 0.5F, 0.0F, 0.5F + f8, 1.0F - f8, 1.0F);
						}

                        Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
						this.renderBottomFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(0));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 1.0F, 0.0F);
						this.renderTopFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(1));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 0.0F, -1.0F);
						this.renderEastFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(2));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(0.0F, 0.0F, 1.0F);
						this.renderWestFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(3));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(-1.0F, 0.0F, 0.0F);
						this.renderNorthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(4));
						tessellator4.DrawImmediate();
						tessellator4.startDrawingQuads();
						tessellator4.SetNormal(1.0F, 0.0F, 0.0F);
						this.renderSouthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSide(5));
						tessellator4.DrawImmediate();
                        Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
					}

					block1.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				}
			}
			else
			{
				if (i6 == 16)
				{
					i2 = 1;
				}

				block1.setBlockBoundsForItemRender();
                Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
				tessellator4.startDrawingQuads();
				tessellator4.SetNormal(0.0F, -1.0F, 0.0F);
				this.renderBottomFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSideAndMetadata(0, i2));
				tessellator4.DrawImmediate();
				if (z5 && this.useInventoryTint)
				{
					i14 = block1.getRenderColor(i2);
					f8 = (float)(i14 >> 16 & 255) / 255.0F;
					f9 = (float)(i14 >> 8 & 255) / 255.0F;
					float f10 = (float)(i14 & 255) / 255.0F;
                    Minecraft.renderPipeline.SetColor(f8 * f3, f9 * f3, f10 * f3, 1.0F);
				}

				tessellator4.startDrawingQuads();
				tessellator4.SetNormal(0.0F, 1.0F, 0.0F);
				this.renderTopFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSideAndMetadata(1, i2));
				tessellator4.DrawImmediate();
				if (z5 && this.useInventoryTint)
				{
                    Minecraft.renderPipeline.SetColor(f3, f3, f3, 1.0F);
				}

				tessellator4.startDrawingQuads();
				tessellator4.SetNormal(0.0F, 0.0F, -1.0F);
				this.renderEastFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSideAndMetadata(2, i2));
				tessellator4.DrawImmediate();
				tessellator4.startDrawingQuads();
				tessellator4.SetNormal(0.0F, 0.0F, 1.0F);
				this.renderWestFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSideAndMetadata(3, i2));
				tessellator4.DrawImmediate();
				tessellator4.startDrawingQuads();
				tessellator4.SetNormal(-1.0F, 0.0F, 0.0F);
				this.renderNorthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSideAndMetadata(4, i2));
				tessellator4.DrawImmediate();
				tessellator4.startDrawingQuads();
				tessellator4.SetNormal(1.0F, 0.0F, 0.0F);
				this.renderSouthFace(block1, 0.0D, 0.0D, 0.0D, block1.getBlockTextureFromSideAndMetadata(5, i2));
				tessellator4.DrawImmediate();
                Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
			}

		}

		public static bool renderItemIn3d(int i0)
		{
			return i0 == 0 ? true : (i0 == 13 ? true : (i0 == 10 ? true : (i0 == 11 ? true : (i0 == 27 ? true : (i0 == 22 ? true : (i0 == 21 ? true : i0 == 16))))));
		}
	}

}