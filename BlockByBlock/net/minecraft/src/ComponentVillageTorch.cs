using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentVillageTorch : ComponentVillage
	{
		private int averageGroundLevel = -1;

		public ComponentVillageTorch(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.boundingBox = structureBoundingBox3;
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
		}

		public static StructureBoundingBox findValidPlacement(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5)
		{
			StructureBoundingBox structureBoundingBox6 = StructureBoundingBox.getComponentToAddBoundingBox(i2, i3, i4, 0, 0, 0, 3, 4, 2, i5);
			return StructureComponent.findIntersecting(list0, structureBoundingBox6) != null ? null : structureBoundingBox6;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
			if (this.averageGroundLevel < 0)
			{
				this.averageGroundLevel = this.getAverageGroundLevel(world1, structureBoundingBox3);
				if (this.averageGroundLevel < 0)
				{
					return true;
				}

				this.boundingBox.offset(0, this.averageGroundLevel - this.boundingBox.maxY + 4 - 1, 0);
			}

			this.fillWithBlocks(world1, structureBoundingBox3, 0, 0, 0, 2, 3, 1, 0, 0, false);
			this.placeBlockAtCurrentPosition(world1, Block.fence.blockID, 0, 1, 0, 0, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.fence.blockID, 0, 1, 1, 0, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.fence.blockID, 0, 1, 2, 0, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.cloth.blockID, 15, 1, 3, 0, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.torchWood.blockID, 15, 0, 3, 0, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.torchWood.blockID, 15, 1, 3, 1, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.torchWood.blockID, 15, 2, 3, 0, structureBoundingBox3);
			this.placeBlockAtCurrentPosition(world1, Block.torchWood.blockID, 15, 1, 3, -1, structureBoundingBox3);
			return true;
		}
	}

}