using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentStrongholdStraight : ComponentStronghold
	{
		private readonly EnumDoor doorType;
		private readonly bool expandsX;
		private readonly bool expandsZ;

		public ComponentStrongholdStraight(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.doorType = this.getRandomDoor(random2);
			this.boundingBox = structureBoundingBox3;
			this.expandsX = random2.Next(2) == 0;
			this.expandsZ = random2.Next(2) == 0;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			this.getNextComponentNormal((ComponentStrongholdStairs2)structureComponent1, list2, random3, 1, 1);
			if (this.expandsX)
			{
				this.getNextComponentX((ComponentStrongholdStairs2)structureComponent1, list2, random3, 1, 2);
			}

			if (this.expandsZ)
			{
				this.getNextComponentZ((ComponentStrongholdStairs2)structureComponent1, list2, random3, 1, 2);
			}

		}

		public static ComponentStrongholdStraight findValidPlacement(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5, int i6)
		{
			StructureBoundingBox structureBoundingBox7 = StructureBoundingBox.getComponentToAddBoundingBox(i2, i3, i4, -1, -1, 0, 5, 5, 7, i5);
			return canStrongholdGoDeeper(structureBoundingBox7) && StructureComponent.findIntersecting(list0, structureBoundingBox7) == null ? new ComponentStrongholdStraight(i6, random1, structureBoundingBox7, i5) : null;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			if (this.isLiquidInStructureBoundingBox(world1, structureBoundingBox3))
			{
				return false;
			}
			else
			{
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 0, 0, 0, 4, 4, 6, true, random2, StructureStrongholdPieces.StrongholdStones);
				this.placeDoor(world1, random2, structureBoundingBox3, this.doorType, 1, 1, 0);
				this.placeDoor(world1, random2, structureBoundingBox3, EnumDoor.OPENING, 1, 1, 6);
				this.randomlyPlaceBlock(world1, structureBoundingBox3, random2, 0.1F, 1, 2, 1, Block.torchWood.blockID, 0);
				this.randomlyPlaceBlock(world1, structureBoundingBox3, random2, 0.1F, 3, 2, 1, Block.torchWood.blockID, 0);
				this.randomlyPlaceBlock(world1, structureBoundingBox3, random2, 0.1F, 1, 2, 5, Block.torchWood.blockID, 0);
				this.randomlyPlaceBlock(world1, structureBoundingBox3, random2, 0.1F, 3, 2, 5, Block.torchWood.blockID, 0);
				if (this.expandsX)
				{
					this.fillWithBlocks(world1, structureBoundingBox3, 0, 1, 2, 0, 3, 4, 0, 0, false);
				}

				if (this.expandsZ)
				{
					this.fillWithBlocks(world1, structureBoundingBox3, 4, 1, 2, 4, 3, 4, 0, 0, false);
				}

				return true;
			}
		}
	}

}