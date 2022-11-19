using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public abstract class ComponentStronghold : StructureComponent
	{
		protected internal ComponentStronghold(int i1) : base(i1)
		{
		}

		protected internal virtual void placeDoor(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, EnumDoor enumDoor4, int i5, int i6, int i7)
		{
			switch (EnumDoorHelper.doorEnum[(int)enumDoor4])
			{
			case 1:
			default:
				this.fillWithBlocks(world1, structureBoundingBox3, i5, i6, i7, i5 + 3 - 1, i6 + 3 - 1, i7, 0, 0, false);
				break;
			case 2:
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 1, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 2, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 2, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 2, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorWood.blockID, 0, i5 + 1, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorWood.blockID, 8, i5 + 1, i6 + 1, i7, structureBoundingBox3);
				break;
			case 3:
				this.placeBlockAtCurrentPosition(world1, 0, 0, i5 + 1, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, 0, 0, i5 + 1, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5 + 1, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5 + 2, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5 + 2, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.fenceIron.blockID, 0, i5 + 2, i6, i7, structureBoundingBox3);
				break;
			case 4:
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 1, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 2, i6 + 2, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 2, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.stoneBrick.blockID, 0, i5 + 2, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorSteel.blockID, 0, i5 + 1, i6, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.doorSteel.blockID, 8, i5 + 1, i6 + 1, i7, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.button.blockID, this.getMetadataWithOffset(Block.button.blockID, 4), i5 + 2, i6 + 1, i7 + 1, structureBoundingBox3);
				this.placeBlockAtCurrentPosition(world1, Block.button.blockID, this.getMetadataWithOffset(Block.button.blockID, 3), i5 + 2, i6 + 1, i7 - 1, structureBoundingBox3);
			break;
			}

		}

		protected internal virtual EnumDoor getRandomDoor(RandomExtended random1)
		{
			int i2 = random1.Next(5);
			switch (i2)
			{
			case 0:
			case 1:
			default:
				return EnumDoor.OPENING;
			case 2:
				return EnumDoor.WOOD_DOOR;
			case 3:
				return EnumDoor.GRATES;
			case 4:
				return EnumDoor.IRON_DOOR;
			}
		}

		protected internal virtual StructureComponent getNextComponentNormal(ComponentStrongholdStairs2 componentStrongholdStairs21, System.Collections.IList list2, RandomExtended random3, int i4, int i5)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX + i4, this.boundingBox.minY + i5, this.boundingBox.maxZ + 1, this.coordBaseMode, this.ComponentType);
			case 1:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i5, this.boundingBox.minZ + i4, this.coordBaseMode, this.ComponentType);
			case 2:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX + i4, this.boundingBox.minY + i5, this.boundingBox.minZ - 1, this.coordBaseMode, this.ComponentType);
			case 3:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i5, this.boundingBox.minZ + i4, this.coordBaseMode, this.ComponentType);
			default:
				return null;
			}
		}

		protected internal virtual StructureComponent getNextComponentX(ComponentStrongholdStairs2 componentStrongholdStairs21, System.Collections.IList list2, RandomExtended random3, int i4, int i5)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 1, this.ComponentType);
			case 1:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.minZ - 1, 2, this.ComponentType);
			case 2:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 1, this.ComponentType);
			case 3:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.minZ - 1, 2, this.ComponentType);
			default:
				return null;
			}
		}

		protected internal virtual StructureComponent getNextComponentZ(ComponentStrongholdStairs2 componentStrongholdStairs21, System.Collections.IList list2, RandomExtended random3, int i4, int i5)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 3, this.ComponentType);
			case 1:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.maxZ + 1, 0, this.ComponentType);
			case 2:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 3, this.ComponentType);
			case 3:
				return StructureStrongholdPieces.getNextValidComponentAccess(componentStrongholdStairs21, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.maxZ + 1, 0, this.ComponentType);
			default:
				return null;
			}
		}

		protected internal static bool canStrongholdGoDeeper(StructureBoundingBox structureBoundingBox0)
		{
			return structureBoundingBox0 != null && structureBoundingBox0.minY > 10;
		}
	}

}