using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentVillagePathGen : ComponentVillageRoadPiece
	{
		private int averageGroundLevel;

		public ComponentVillagePathGen(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.boundingBox = structureBoundingBox3;
			this.averageGroundLevel = Math.Max(structureBoundingBox3.XSize, structureBoundingBox3.ZSize);
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			bool z4 = false;

			int i5;
			StructureComponent structureComponent6;
			for (i5 = random3.Next(5); i5 < this.averageGroundLevel - 8; i5 += 2 + random3.Next(5))
			{
				structureComponent6 = this.getNextComponentNN((ComponentVillageStartPiece)structureComponent1, list2, random3, 0, i5);
				if (structureComponent6 != null)
				{
					i5 += Math.Max(structureComponent6.boundingBox.XSize, structureComponent6.boundingBox.ZSize);
					z4 = true;
				}
			}

			for (i5 = random3.Next(5); i5 < this.averageGroundLevel - 8; i5 += 2 + random3.Next(5))
			{
				structureComponent6 = this.getNextComponentPP((ComponentVillageStartPiece)structureComponent1, list2, random3, 0, i5);
				if (structureComponent6 != null)
				{
					i5 += Math.Max(structureComponent6.boundingBox.XSize, structureComponent6.boundingBox.ZSize);
					z4 = true;
				}
			}

			if (z4 && random3.Next(3) > 0)
			{
				switch (this.coordBaseMode)
				{
				case 0:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY, this.boundingBox.maxZ - 2, 1, this.ComponentType);
					break;
				case 1:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.minZ - 1, 2, this.ComponentType);
					break;
				case 2:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY, this.boundingBox.minZ, 1, this.ComponentType);
					break;
				case 3:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.maxX - 2, this.boundingBox.minY, this.boundingBox.minZ - 1, 2, this.ComponentType);
				break;
				}
			}

			if (z4 && random3.Next(3) > 0)
			{
				switch (this.coordBaseMode)
				{
				case 0:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY, this.boundingBox.maxZ - 2, 3, this.ComponentType);
					break;
				case 1:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.maxZ + 1, 0, this.ComponentType);
					break;
				case 2:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY, this.boundingBox.minZ, 3, this.ComponentType);
					break;
				case 3:
					StructureVillagePieces.getNextStructureComponentVillagePath((ComponentVillageStartPiece)structureComponent1, list2, random3, this.boundingBox.maxX - 2, this.boundingBox.minY, this.boundingBox.maxZ + 1, 0, this.ComponentType);
				break;
				}
			}

		}

		public static StructureBoundingBox func_35087_a(ComponentVillageStartPiece componentVillageStartPiece0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6)
		{
			for (int i7 = 7 * MathHelper.getRandomIntegerInRange(random2, 3, 5); i7 >= 7; i7 -= 7)
			{
				StructureBoundingBox structureBoundingBox8 = StructureBoundingBox.getComponentToAddBoundingBox(i3, i4, i5, 0, 0, 0, 3, 3, i7, i6);
				if (StructureComponent.findIntersecting(list1, structureBoundingBox8) == null)
				{
					return structureBoundingBox8;
				}
			}

			return null;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			for (int i4 = this.boundingBox.minX; i4 <= this.boundingBox.maxX; ++i4)
			{
				for (int i5 = this.boundingBox.minZ; i5 <= this.boundingBox.maxZ; ++i5)
				{
					if (structureBoundingBox3.isVecInside(i4, 64, i5))
					{
						int i6 = world1.getTopSolidOrLiquidBlock(i4, i5) - 1;
						world1.setBlock(i4, i6, i5, Block.gravel.blockID);
					}
				}
			}

			return true;
		}
	}

}