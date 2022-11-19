using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentNetherBridgeEnd : ComponentNetherBridgePiece
	{
		private int fillSeed;

		public ComponentNetherBridgeEnd(int i1, RandomExtended random2, StructureBoundingBox structureBoundingBox3, int i4) : base(i1)
		{
			this.coordBaseMode = i4;
			this.boundingBox = structureBoundingBox3;
			this.fillSeed = random2.Next();
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
		}

		public static ComponentNetherBridgeEnd func_40023_a(System.Collections.IList list0, RandomExtended random1, int i2, int i3, int i4, int i5, int i6)
		{
			StructureBoundingBox structureBoundingBox7 = StructureBoundingBox.getComponentToAddBoundingBox(i2, i3, i4, -1, -3, 0, 5, 10, 8, i5);
			return isAboveGround(structureBoundingBox7) && StructureComponent.findIntersecting(list0, structureBoundingBox7) == null ? new ComponentNetherBridgeEnd(i6, random1, structureBoundingBox7, i5) : null;
		}

		public override bool addComponentParts(World world1, RandomExtended random2, StructureBoundingBox structureBoundingBox3)
		{
            RandomExtended random4 = new RandomExtended((long)this.fillSeed);

			int i5;
			int i6;
			int i7;
			for (i5 = 0; i5 <= 4; ++i5)
			{
				for (i6 = 3; i6 <= 4; ++i6)
				{
					i7 = random4.Next(8);
					this.fillWithBlocks(world1, structureBoundingBox3, i5, i6, 0, i5, i6, i7, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
				}
			}

			i5 = random4.Next(8);
			this.fillWithBlocks(world1, structureBoundingBox3, 0, 5, 0, 0, 5, i5, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			i5 = random4.Next(8);
			this.fillWithBlocks(world1, structureBoundingBox3, 4, 5, 0, 4, 5, i5, Block.netherBrick.blockID, Block.netherBrick.blockID, false);

			for (i5 = 0; i5 <= 4; ++i5)
			{
				i6 = random4.Next(5);
				this.fillWithBlocks(world1, structureBoundingBox3, i5, 2, 0, i5, 2, i6, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
			}

			for (i5 = 0; i5 <= 4; ++i5)
			{
				for (i6 = 0; i6 <= 1; ++i6)
				{
					i7 = random4.Next(3);
					this.fillWithBlocks(world1, structureBoundingBox3, i5, i6, 0, i5, i6, i7, Block.netherBrick.blockID, Block.netherBrick.blockID, false);
				}
			}

			return true;
		}
	}

}