using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentStrongholdLeftTurn : ComponentStronghold
	{
		protected internal readonly EnumDoor doorType;

		public ComponentStrongholdLeftTurn(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.doorType = this.getRandomDoor(random2);
			this.boundingBox = structureBoundingBox3;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			if (this.coordBaseMode != 2 && this.coordBaseMode != 3)
			{
				this.getNextComponentZ((ComponentStrongholdStairs2)structureComponent1, list2, random3, 1, 1);
			}
			else
			{
				this.getNextComponentX((ComponentStrongholdStairs2)structureComponent1, list2, random3, 1, 1);
			}

		}

		public static ComponentStrongholdLeftTurn findValidPlacement(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5, int i6)
		{
			StructureBoundingBox structureBoundingBox7 = StructureBoundingBox.getComponentToAddBoundingBox(i2, i3, i4, -1, -1, 0, 5, 5, 5, i5);
			return canStrongholdGoDeeper(structureBoundingBox7) && StructureComponent.findIntersecting(list0, structureBoundingBox7) == null ? new ComponentStrongholdLeftTurn(i6, random1, structureBoundingBox7, i5) : null;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			if (this.isLiquidInStructureBoundingBox(world1, structureBoundingBox3))
			{
				return false;
			}
			else
			{
				this.fillWithRandomizedBlocks(world1, structureBoundingBox3, 0, 0, 0, 4, 4, 4, true, random2, StructureStrongholdPieces.StrongholdStones);
				this.placeDoor(world1, random2, structureBoundingBox3, this.doorType, 1, 1, 0);
				if (this.coordBaseMode != 2 && this.coordBaseMode != 3)
				{
					this.fillWithBlocks(world1, structureBoundingBox3, 4, 1, 1, 4, 3, 3, 0, 0, false);
				}
				else
				{
					this.fillWithBlocks(world1, structureBoundingBox3, 0, 1, 1, 0, 3, 3, 0, 0, false);
				}

				return true;
			}
		}
	}

}