using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public abstract class ComponentVillage : StructureComponent
	{
		private int villagersSpawned;

		protected internal ComponentVillage(int i1) : base(i1)
		{
		}

		protected internal virtual StructureComponent getNextComponentNN(ComponentVillageStartPiece componentVillageStartPiece1, System.Collections.IList list2, RandomExtended random3, int i4, int i5)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 1, this.ComponentType);
			case 1:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.minZ - 1, 2, this.ComponentType);
			case 2:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 1, this.ComponentType);
			case 3:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.minZ - 1, 2, this.ComponentType);
			default:
				return null;
			}
		}

		protected internal virtual StructureComponent getNextComponentPP(ComponentVillageStartPiece componentVillageStartPiece1, System.Collections.IList list2, RandomExtended random3, int i4, int i5)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 3, this.ComponentType);
			case 1:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.maxZ + 1, 0, this.ComponentType);
			case 2:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 3, this.ComponentType);
			case 3:
				return StructureVillagePieces.getNextStructureComponent(componentVillageStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.maxZ + 1, 0, this.ComponentType);
			default:
				return null;
			}
		}

		protected internal virtual int getAverageGroundLevel(World world1, StructureBoundingBox structureBoundingBox2)
		{
			int i3 = 0;
			int i4 = 0;

			for (int i5 = this.boundingBox.minZ; i5 <= this.boundingBox.maxZ; ++i5)
			{
				for (int i6 = this.boundingBox.minX; i6 <= this.boundingBox.maxX; ++i6)
				{
					if (structureBoundingBox2.isVecInside(i6, 64, i5))
					{
						i3 += Math.Max(world1.getTopSolidOrLiquidBlock(i6, i5), world1.worldProvider.AverageGroundLevel);
						++i4;
					}
				}
			}

			if (i4 == 0)
			{
				return -1;
			}
			else
			{
				return i3 / i4;
			}
		}

		protected internal static bool canVillageGoDeeper(StructureBoundingBox structureBoundingBox0)
		{
			return structureBoundingBox0 != null && structureBoundingBox0.minY > 10;
		}

		protected internal virtual void spawnVillagers(World world1, StructureBoundingBox structureBoundingBox2, int i3, int i4, int i5, int i6)
		{
			if (this.villagersSpawned < i6)
			{
				for (int i7 = this.villagersSpawned; i7 < i6; ++i7)
				{
					int i8 = this.getXWithOffset(i3 + i7, i5);
					int i9 = this.getYWithOffset(i4);
					int i10 = this.getZWithOffset(i3 + i7, i5);
					if (!structureBoundingBox2.isVecInside(i8, i9, i10))
					{
						break;
					}

					++this.villagersSpawned;
					EntityVillager entityVillager11 = new EntityVillager(world1, this.getVillagerType(i7));
					entityVillager11.setLocationAndAngles((double)i8 + 0.5D, (double)i9, (double)i10 + 0.5D, 0.0F, 0.0F);
					world1.spawnEntityInWorld(entityVillager11);
				}

			}
		}

		protected internal virtual int getVillagerType(int i1)
		{
			return 0;
		}
	}

}