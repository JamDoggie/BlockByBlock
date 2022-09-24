using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentMineshaftStairs : StructureComponent
	{
		public ComponentMineshaftStairs(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.boundingBox = structureBoundingBox3;
		}

		public static StructureBoundingBox findValidPlacement(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5)
		{
			StructureBoundingBox structureBoundingBox6 = new StructureBoundingBox(i2, i3 - 5, i4, i2, i3 + 2, i4);
			switch (i5)
			{
			case 0:
				structureBoundingBox6.maxX = i2 + 2;
				structureBoundingBox6.maxZ = i4 + 8;
				break;
			case 1:
				structureBoundingBox6.minX = i2 - 8;
				structureBoundingBox6.maxZ = i4 + 2;
				break;
			case 2:
				structureBoundingBox6.maxX = i2 + 2;
				structureBoundingBox6.minZ = i4 - 8;
				break;
			case 3:
				structureBoundingBox6.maxX = i2 + 8;
				structureBoundingBox6.maxZ = i4 + 2;
			break;
			}

			return StructureComponent.findIntersecting(list0, structureBoundingBox6) != null ? null : structureBoundingBox6;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			int i4 = this.ComponentType;
			switch (this.coordBaseMode)
			{
			case 0:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.maxZ + 1, 0, i4);
				break;
			case 1:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY, this.boundingBox.minZ, 1, i4);
				break;
			case 2:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.minZ - 1, 2, i4);
				break;
			case 3:
				StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY, this.boundingBox.minZ, 3, i4);
			break;
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
				this.fillWithBlocks(world1, structureBoundingBox3, 0, 5, 0, 2, 7, 1, 0, 0, false);
				this.fillWithBlocks(world1, structureBoundingBox3, 0, 0, 7, 2, 2, 8, 0, 0, false);

				for (int i4 = 0; i4 < 5; ++i4)
				{
					this.fillWithBlocks(world1, structureBoundingBox3, 0, 5 - i4 - (i4 < 4 ? 1 : 0), 2 + i4, 2, 7 - i4, 2 + i4, 0, 0, false);
				}

				return true;
			}
		}
	}

}