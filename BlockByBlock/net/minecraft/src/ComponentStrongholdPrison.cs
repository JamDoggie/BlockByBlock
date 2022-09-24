using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentStrongholdPrison : ComponentStronghold
	{
		protected internal readonly EnumDoor doorType;

		public ComponentStrongholdPrison(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.doorType = this.getRandomDoor(random2);
			this.boundingBox = structureBoundingBox3;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			this.getNextComponentNormal((ComponentStrongholdStairs2)structureComponent1, list2, random3, 1, 1);
		}

		public static ComponentStrongholdPrison findValidPlacement(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5, int i6)
		{
			StructureBoundingBox structureBoundingBox7 = StructureBoundingBox.getComponentToAddBoundingBox(i2, i3, i4, -1, -1, 0, 9, 5, 11, i5);
			return canStrongholdGoDeeper(structureBoundingBox7) && StructureComponent.findIntersecting(list0, structureBoundingBox7) == null ? new ComponentStrongholdPrison(i6, random1, structureBoundingBox7, i5) : null;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			if (this.isLiquidInStructureBoundingBox(world1, structureBoundingBox3))
			{
				return false;
			}
			else
			{
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 0, 0, 0, 8, 4, 10, true, random2, StructureStrongholdPieces.StrongholdStones);
				this.placeDoor(world1, random2, structureBoundingBox3, this.doorType, 1, 1, 0);
				this.fillWithBlocks(world1, structureBoundingBox3, 1, 1, 10, 3, 3, 10, 0, 0, false);
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 4, 1, 1, 4, 3, 1, false, random2, StructureStrongholdPieces.StrongholdStones);
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 4, 1, 3, 4, 3, 3, false, random2, StructureStrongholdPieces.StrongholdStones);
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 4, 1, 7, 4, 3, 7, false, random2, StructureStrongholdPieces.StrongholdStones);
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 4, 1, 9, 4, 3, 9, false, random2, StructureStrongholdPieces.StrongholdStones);
				this.fillWithBlocks(world1, structureBoundingBox3, 4, 1, 4, 4, 3, 6, Block.fenceIron.blockID, Block.fenceIron.blockID, false);
				this.fillWithBlocks(world1, structureBoundingBox3, 5, 1, 5, 7, 3, 5, Block.fenceIron.blockID, Block.fenceIron.blockID, false);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, 4, 3, 2, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, 4, 3, 8, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorSteel.blockID, this.getMetadataWithOffset(Block.doorSteel.blockID, 3), 4, 1, 2, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorSteel.blockID, this.getMetadataWithOffset(Block.doorSteel.blockID, 3) + 8, 4, 2, 2, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorSteel.blockID, this.getMetadataWithOffset(Block.doorSteel.blockID, 3), 4, 1, 8, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorSteel.blockID, this.getMetadataWithOffset(Block.doorSteel.blockID, 3) + 8, 4, 2, 8, structureBoundingBox3);
				return true;
			}
		}
	}

}