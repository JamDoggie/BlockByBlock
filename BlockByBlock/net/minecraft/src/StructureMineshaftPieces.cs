using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class StructureMineshaftPieces
	{
		private static readonly StructurePieceTreasure[] lootArray = new StructurePieceTreasure[]
		{
			new StructurePieceTreasure(Item.ingotIron.shiftedIndex, 0, 1, 5, 10),
			new StructurePieceTreasure(Item.ingotGold.shiftedIndex, 0, 1, 3, 5),
			new StructurePieceTreasure(Item.redstone.shiftedIndex, 0, 4, 9, 5),
			new StructurePieceTreasure(Item.dyePowder.shiftedIndex, 4, 4, 9, 5),
			new StructurePieceTreasure(Item.diamond.shiftedIndex, 0, 1, 2, 3),
			new StructurePieceTreasure(Item.coal.shiftedIndex, 0, 3, 8, 10),
			new StructurePieceTreasure(Item.bread.shiftedIndex, 0, 1, 3, 15),
			new StructurePieceTreasure(Item.pickaxeSteel.shiftedIndex, 0, 1, 1, 1),
			new StructurePieceTreasure(Block.rail.blockID, 0, 4, 8, 1),
			new StructurePieceTreasure(Item.melonSeeds.shiftedIndex, 0, 2, 4, 10),
			new StructurePieceTreasure(Item.pumpkinSeeds.shiftedIndex, 0, 2, 4, 10)
		};

		private static StructureComponent getRandomComponent(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5, int i6)
		{
			int i7 = random1.Next(100);
			StructureBoundingBox structureBoundingBox8;
			if (i7 >= 80)
			{
				structureBoundingBox8 = ComponentMineshaftCross.findValidPlacement(list0, random1, i2, i3, i4, i5);
				if (structureBoundingBox8 != null)
				{
					return new ComponentMineshaftCross(i6, random1, structureBoundingBox8, i5);
				}
			}
			else if (i7 >= 70)
			{
				structureBoundingBox8 = ComponentMineshaftStairs.findValidPlacement(list0, random1, i2, i3, i4, i5);
				if (structureBoundingBox8 != null)
				{
					return new ComponentMineshaftStairs(i6, random1, structureBoundingBox8, i5);
				}
			}
			else
			{
				structureBoundingBox8 = ComponentMineshaftCorridor.findValidPlacement(list0, random1, i2, i3, i4, i5);
				if (structureBoundingBox8 != null)
				{
					return new ComponentMineshaftCorridor(i6, random1, structureBoundingBox8, i5);
				}
			}

			return null;
		}

		private static StructureComponent getNextMineShaftComponent(StructureComponent structureComponent0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			if (i7 > 8)
			{
				return null;
			}
			else if (Math.Abs(i3 - structureComponent0.BoundingBox.minX) <= 80 && Math.Abs(i5 - structureComponent0.BoundingBox.minZ) <= 80)
			{
				StructureComponent structureComponent8 = getRandomComponent(list1, random2, i3, i4, i5, i6, i7 + 1);
				if (structureComponent8 != null)
				{
					list1.Add(structureComponent8);
					structureComponent8.buildComponent(structureComponent0, list1, random2);
				}

				return structureComponent8;
			}
			else
			{
				return null;
			}
		}

		internal static StructureComponent getNextComponent(StructureComponent structureComponent0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			return getNextMineShaftComponent(structureComponent0, list1, random2, i3, i4, i5, i6, i7);
		}

		internal static StructurePieceTreasure[] TreasurePieces
		{
			get
			{
				return lootArray;
			}
		}
	}

}