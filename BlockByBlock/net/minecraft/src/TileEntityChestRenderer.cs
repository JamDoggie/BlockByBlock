namespace net.minecraft.src
{
    using BlockByBlock.net.minecraft.client.entity.render.model;
    using net.minecraft.client;
    using net.minecraft.client.entity.render.model;
    using OpenTK.Graphics.OpenGL;

    public class TileEntityChestRenderer : TileEntitySpecialRenderer
	{
		private ModelChest chestModel = new ModelChest();
		private ModelChest largeChestModel = new ModelLargeChest();

		public virtual void renderTileEntityChestAt(TileEntityChest tileEntityChest1, double d2, double d4, double d6, float f8)
		{
			int i9;
			if (tileEntityChest1.worldObj == null)
			{
				i9 = 0;
			}
			else
			{
				Block block10 = tileEntityChest1.BlockType;
				i9 = tileEntityChest1.BlockMetadata;
				if (block10 != null && i9 == 0)
				{
					((BlockChest)block10).unifyAdjacentChests(tileEntityChest1.worldObj, tileEntityChest1.xCoord, tileEntityChest1.yCoord, tileEntityChest1.zCoord);
					i9 = tileEntityChest1.BlockMetadata;
				}

				tileEntityChest1.checkForAdjacentChests();
			}

			if (tileEntityChest1.adjacentChestZNeg == null && tileEntityChest1.adjacentChestXNeg == null)
			{
				ModelChest modelChest14;
				if (tileEntityChest1.adjacentChestXPos == null && tileEntityChest1.adjacentChestZPos == null)
				{
					modelChest14 = this.chestModel;
					this.bindTextureByName("/item/chest.png");
				}
				else
				{
					modelChest14 = this.largeChestModel;
					this.bindTextureByName("/item/largechest.png");
				}

                Minecraft.renderPipeline.ModelMatrix.PushMatrix();
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate((float)d2, (float)d4 + 1.0F, (float)d6 + 1.0F);
                Minecraft.renderPipeline.ModelMatrix.Scale(1.0F, -1.0F, -1.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(0.5F, 0.5F, 0.5F);
				short s11 = 0;
				if (i9 == 2)
				{
					s11 = 180;
				}

				if (i9 == 3)
				{
					s11 = 0;
				}

				if (i9 == 4)
				{
					s11 = 90;
				}

				if (i9 == 5)
				{
					s11 = -90;
				}

				if (i9 == 2 && tileEntityChest1.adjacentChestXPos != null)
				{
                    Minecraft.renderPipeline.ModelMatrix.Translate(1.0F, 0.0F, 0.0F);
				}

				if (i9 == 5 && tileEntityChest1.adjacentChestZPos != null)
				{
                    Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -1.0F);
				}

                Minecraft.renderPipeline.ModelMatrix.Rotate((float)s11, 0.0F, 1.0F, 0.0F);
                Minecraft.renderPipeline.ModelMatrix.Translate(-0.5F, -0.5F, -0.5F);
				float f12 = tileEntityChest1.prevLidAngle + (tileEntityChest1.lidAngle - tileEntityChest1.prevLidAngle) * f8;
				float f13;
				if (tileEntityChest1.adjacentChestZNeg != null)
				{
					f13 = tileEntityChest1.adjacentChestZNeg.prevLidAngle + (tileEntityChest1.adjacentChestZNeg.lidAngle - tileEntityChest1.adjacentChestZNeg.prevLidAngle) * f8;
					if (f13 > f12)
					{
						f12 = f13;
					}
				}

				if (tileEntityChest1.adjacentChestXNeg != null)
				{
					f13 = tileEntityChest1.adjacentChestXNeg.prevLidAngle + (tileEntityChest1.adjacentChestXNeg.lidAngle - tileEntityChest1.adjacentChestXNeg.prevLidAngle) * f8;
					if (f13 > f12)
					{
						f12 = f13;
					}
				}

				f12 = 1.0F - f12;
				f12 = 1.0F - f12 * f12 * f12;
				modelChest14.chestLid.rotateAngleX = -(f12 * (float)Math.PI / 2.0F);
				modelChest14.renderAll();
                Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			}
		}

		public override void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			this.renderTileEntityChestAt((TileEntityChest)tileEntity1, d2, d4, d6, f8);
		}
	}

}