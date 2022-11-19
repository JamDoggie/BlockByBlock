using BlockByBlock.java_extensions;
using System;
using System.Collections.Generic;

namespace net.minecraft.src
{

	public class ComponentMineshaftRoom : StructureComponent
	{
		private LinkedList<StructureBoundingBox> chidStructures = new();

		public ComponentMineshaftRoom(int i1, RandomExtended random2, int i3, int i4) : base(i1)
		{
			this.boundingBox = new StructureBoundingBox(i3, 50, i4, i3 + 7 + random2.Next(6), 54 + random2.Next(6), i4 + 7 + random2.Next(6));
		}

		public override void buildComponent(StructureComponent structureComponent1, System.Collections.IList list2, RandomExtended random3)
		{
			int i4 = this.ComponentType;
			int i6 = this.boundingBox.YSize - 3 - 1;
			if (i6 <= 0)
			{
				i6 = 1;
			}

			int i5;
			StructureComponent structureComponent7;
			StructureBoundingBox structureBoundingBox8;
			for (i5 = 0; i5 < this.boundingBox.XSize; i5 += 4)
			{
				i5 += random3.Next(this.boundingBox.XSize);
				if (i5 + 3 > this.boundingBox.XSize)
				{
					break;
				}

				structureComponent7 = StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + random3.Next(i6) + 1, this.boundingBox.minZ - 1, 2, i4);
				if (structureComponent7 != null)
				{
					structureBoundingBox8 = structureComponent7.BoundingBox;
					this.chidStructures.AddLast(new StructureBoundingBox(structureBoundingBox8.minX, structureBoundingBox8.minY, this.boundingBox.minZ, structureBoundingBox8.maxX, structureBoundingBox8.maxY, this.boundingBox.minZ + 1));
				}
			}

			for (i5 = 0; i5 < this.boundingBox.XSize; i5 += 4)
			{
				i5 += random3.Next(this.boundingBox.XSize);
				if (i5 + 3 > this.boundingBox.XSize)
				{
					break;
				}

				structureComponent7 = StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + random3.Next(i6) + 1, this.boundingBox.maxZ + 1, 0, i4);
				if (structureComponent7 != null)
				{
					structureBoundingBox8 = structureComponent7.BoundingBox;
					this.chidStructures.AddLast(new StructureBoundingBox(structureBoundingBox8.minX, structureBoundingBox8.minY, this.boundingBox.maxZ - 1, structureBoundingBox8.maxX, structureBoundingBox8.maxY, this.boundingBox.maxZ));
				}
			}

			for (i5 = 0; i5 < this.boundingBox.ZSize; i5 += 4)
			{
				i5 += random3.Next(this.boundingBox.ZSize);
				if (i5 + 3 > this.boundingBox.ZSize)
				{
					break;
				}

				structureComponent7 = StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + random3.Next(i6) + 1, this.boundingBox.minZ + i5, 1, i4);
				if (structureComponent7 != null)
				{
					structureBoundingBox8 = structureComponent7.BoundingBox;
					this.chidStructures.AddLast(new StructureBoundingBox(this.boundingBox.minX, structureBoundingBox8.minY, structureBoundingBox8.minZ, this.boundingBox.minX + 1, structureBoundingBox8.maxY, structureBoundingBox8.maxZ));
				}
			}

			for (i5 = 0; i5 < this.boundingBox.ZSize; i5 += 4)
			{
				i5 += random3.Next(this.boundingBox.ZSize);
				if (i5 + 3 > this.boundingBox.ZSize)
				{
					break;
				}

				structureComponent7 = StructureMineshaftPieces.getNextComponent(structureComponent1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + random3.Next(i6) + 1, this.boundingBox.minZ + i5, 3, i4);
				if (structureComponent7 != null)
				{
					structureBoundingBox8 = structureComponent7.BoundingBox;
					this.chidStructures.AddLast(new StructureBoundingBox(this.boundingBox.maxX - 1, structureBoundingBox8.minY, structureBoundingBox8.minZ, this.boundingBox.maxX, structureBoundingBox8.maxY, structureBoundingBox8.maxZ));
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
				this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX, this.boundingBox.minY, this.boundingBox.minZ, this.boundingBox.maxX, this.boundingBox.minY, this.boundingBox.maxZ, Block.dirt.blockID, 0, true);
				this.fillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX, this.boundingBox.minY + 1, this.boundingBox.minZ, this.boundingBox.maxX, Math.Min(this.boundingBox.minY + 3, this.boundingBox.maxY), this.boundingBox.maxZ, 0, 0, false);
				System.Collections.IEnumerator iterator4 = this.chidStructures.GetEnumerator();

				while (iterator4.MoveNext())
				{
					StructureBoundingBox structureBoundingBox5 = (StructureBoundingBox)iterator4.Current;
					this.fillWithBlocks(world1, structureBoundingBox3, structureBoundingBox5.minX, structureBoundingBox5.maxY - 2, structureBoundingBox5.minZ, structureBoundingBox5.maxX, structureBoundingBox5.maxY, structureBoundingBox5.maxZ, 0, 0, false);
				}

				this.randomlyRareFillWithBlocks(world1, structureBoundingBox3, this.boundingBox.minX, this.boundingBox.minY + 4, this.boundingBox.minZ, this.boundingBox.maxX, this.boundingBox.maxY, this.boundingBox.maxZ, 0, false);
				return true;
			}
		}
	}

}