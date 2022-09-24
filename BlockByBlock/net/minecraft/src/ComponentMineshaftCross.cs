using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class ComponentMineshaftCross : StructureComponent
	{
		private readonly int corridorDirection;
		private readonly bool isMultipleFloors;

		public ComponentMineshaftCross(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.corridorDirection = i4;
			this.boundingBox = structureBoundingBox3;
			this.isMultipleFloors = structureBoundingBox3.YSize > 3;
		}

		public static StructureBoundingBox findValidPlacement(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5)
		{
			StructureBoundingBox structureBoundingBox6 = new StructureBoundingBox(i2, i3, i4, i2, i3 + 2, i4);
			if (random1.Next(4) == 0)
			{
				structureBoundingBox6.maxY += 4;
			}

			switch (i5)
			{
			case 0:
				structureBoundingBox6.minX = i2 - 1;
				structureBoundingBox6.maxX = i2 + 3;
				structureBoundingBox6.maxZ = i4 + 4;
				break;
			case 1:
				structureBoundingBox6.minX = i2 - 4;
				structureBoundingBox6.minZ = i4 - 1;
				structureBoundingBox6.maxZ = i4 + 3;
				break;
			case 2:
				structureBoundingBox6.minX = i2 - 1;
				structureBoundingBox6.maxX = i2 + 3;
				structureBoundingBox6.minZ = i4 - 4;
				break;
			case 3:
				structureBoundingBox6.maxX = i2 + 4;
				structureBoundingBox6.minZ = i4 - 1;
				structureBoundingBox6.maxZ = i4 + 3;
			break;
			}

			return StructureComponent.findIntersecting(list0, structureBoundingBox6) != null ? null : structureBoundingBox6;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			int i4 = this.ComponentType;
			switch (this.corridorDirection)
			{
			case 0:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.maxZ + 1, 0, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY, this.boundingBox.minZ + 1, 1, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY, this.boundingBox.minZ + 1, 3, i4);
				break;
			case 1:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.minZ - 1, 2, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.maxZ + 1, 0, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY, this.boundingBox.minZ + 1, 1, i4);
				break;
			case 2:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.minZ - 1, 2, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY, this.boundingBox.minZ + 1, 1, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY, this.boundingBox.minZ + 1, 3, i4);
				break;
			case 3:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.minZ - 1, 2, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.maxZ + 1, 0, i4);
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY, this.boundingBox.minZ + 1, 3, i4);
			break;
			}

			if (this.isMultipleFloors)
			{
				if (random3.NextBool())
				{
					StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY + 3 + 1, this.boundingBox.minZ - 1, 2, i4);
				}

				if (random3.NextBool())
				{
					StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + 3 + 1, this.boundingBox.minZ + 1, 1, i4);
				}

				if (random3.NextBool())
				{
					StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + 3 + 1, this.boundingBox.minZ + 1, 3, i4);
				}

				if (random3.NextBool())
				{
					StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + 1, this.boundingBox.minY + 3 + 1, this.boundingBox.maxZ + 1, 0, i4);
				}
			}

		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			if (this.isLiquidInStructureBoundingBox(world1, structureBoundingBox3))
			{
				return false;
			}
			else
			{
				if (this.isMultipleFloors)
				{
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.minZ, this.boundingBox.maxX - 1, this.boundingBox.minY + 3 - 1, this.boundingBox.maxZ, 0, 0, false);
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.minZ + 1, this.boundingBox.maxX, this.boundingBox.minY + 3 - 1, this.boundingBox.maxZ - 1, 0, 0, false);
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX + 1, this.boundingBox.maxY - 2, this.boundingBox.minZ, this.boundingBox.maxX - 1, this.boundingBox.maxY, this.boundingBox.maxZ, 0, 0, false);
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX, this.boundingBox.maxY - 2, this.boundingBox.minZ + 1, this.boundingBox.maxX, this.boundingBox.maxY, this.boundingBox.maxZ - 1, 0, 0, false);
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX + 1, this.boundingBox.minY + 3, this.boundingBox.minZ + 1, this.boundingBox.maxX - 1, this.boundingBox.minY + 3, this.boundingBox.maxZ - 1, 0, 0, false);
				}
				else
				{
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.minZ, this.boundingBox.maxX - 1, this.boundingBox.maxY, this.boundingBox.maxZ, 0, 0, false);
					this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.minZ + 1, this.boundingBox.maxX, this.boundingBox.maxY, this.boundingBox.maxZ - 1, 0, 0, false);
				}

				this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.minZ + 1, this.boundingBox.minX + 1, this.boundingBox.maxY, this.boundingBox.minZ + 1, Block.planks.blockID, 0, false);
				this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX + 1, this.boundingBox.minY, this.boundingBox.maxZ - 1, this.boundingBox.minX + 1, this.boundingBox.maxY, this.boundingBox.maxZ - 1, Block.planks.blockID, 0, false);
				this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.maxX - 1, this.boundingBox.minY, this.boundingBox.minZ + 1, this.boundingBox.maxX - 1, this.boundingBox.maxY, this.boundingBox.minZ + 1, Block.planks.blockID, 0, false);
				this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.maxX - 1, this.boundingBox.minY, this.boundingBox.maxZ - 1, this.boundingBox.maxX - 1, this.boundingBox.maxY, this.boundingBox.maxZ - 1, Block.planks.blockID, 0, false);

				for (int i4 = this.boundingBox.minX; i4 <= this.boundingBox.maxX; ++i4)
				{
					for (int i5 = this.boundingBox.minZ; i5 <= this.boundingBox.maxZ; ++i5)
					{
						int i6 = this.getBlockIdAtCurrentPosition(world1, i4, this.boundingBox.minY - 1, i5, structureBoundingBox3);
						if (i6 == 0)
						{
							this.placeBlockAtCurrentPosition(world1, Block.planks.blockID, 0, i4, this.boundingBox.minY - 1, i5, structureBoundingBox3);
						}
					}
				}

				return true;
			}
		}
	}

}