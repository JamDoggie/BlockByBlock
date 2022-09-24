using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class ComponentNetherBridgeCrossing : ComponentNetherBridgePiece
	{
		public ComponentNetherBridgeCrossing(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.boundingBox = structureBoundingBox3;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			this.getNextComponentNormal((ComponentNetherBridgeStartPiece)structureComponent1, list2, random3, 2, 0, false);
			this.getNextComponentX((ComponentNetherBridgeStartPiece)structureComponent1, list2, random3, 0, 2, false);
			this.getNextComponentZ((ComponentNetherBridgeStartPiece)structureComponent1, list2, random3, 0, 2, false);
		}

		public static ComponentNetherBridgeCrossing createValidComponent(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5, int i6)
		{
			StructureBoundingBox structureBoundingBox7 = StructureBoundingBox.getComponentToAddBoundingBox(i2, i3, i4, -2, 0, 0, 7, 9, 7, i5);
			return isAboveGround(structureBoundingBox7) && StructureComponent.findIntersecting(list0, structureBoundingBox7) == null ? new ComponentNetherBridgeCrossing(i6, random1, structureBoundingBox7, i5) : null;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 0, 0, 6, 1, 6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 2, 0, 6, 7, 6, 0, 0, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 2, 0, 1, 6, 0, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 2, 6, 1, 6, 6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 5, 2, 0, 6, 6, 0, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 5, 2, 6, 6, 6, 6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 2, 0, 0, 6, 1, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 2, 5, 0, 6, 6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 6, 2, 0, 6, 6, 1, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 6, 2, 5, 6, 6, 6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 2, 6, 0, 4, 6, 0, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 2, 5, 0, 4, 5, 0, Block.netherFence.blockID, Block.netherFence.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 2, 6, 6, 4, 6, 6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 2, 5, 6, 4, 5, 6, Block.netherFence.blockID, Block.netherFence.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 6, 2, 0, 6, 4, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 5, 2, 0, 5, 4, Block.netherFence.blockID, Block.netherFence.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 6, 6, 2, 6, 6, 4, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			this.fillWithBlocks(world1, structureBoundingBox3, 6, 5, 2, 6, 5, 4, Block.netherFence.blockID, Block.netherFence.blockID, false);

			for (int i4 = 0; i4 <= 6; ++i4)
			{
				for (int i5 = 0; i5 <= 6; ++i5)
				{
					this.fillCurrentPositionBlocksDownwards(world1, Block.netherBrick.blockID, 0, i4, -1, i5, structureBoundingBox3);
				}
			}

			return true;
		}
	}

}